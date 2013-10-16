using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using SharpSvn.Diff;
using SourceSafeTypeLib;
using System.ComponentModel;
using Vss3WayMerge.Drivers;
using Vss3WayMerge.Interfaces;
using Vss3WayMerge.Properties;
using System.Collections.Generic;
using Vss3WayMerge.Tools;
using Vss3WayMerge.VJP;

namespace Vss3WayMerge
{
	public partial class Vss3WayMerge : Form, IErrorNotification
	{
		// ReSharper disable InconsistentNaming
		// P/Invoke constants
		const int WM_SYSCOMMAND = 0x112;
		const int MF_STRING = 0x0;
		const int MF_SEPARATOR = 0x800;

		// ID for the About item on the system menu
		const int SYSMENU_ABOUT_ID = 0x1;

		// ReSharper restore InconsistentNaming

		// P/Invoke declarations
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		readonly string _tempDir;
		readonly SHA1Managed _hash = new SHA1Managed();
		IMergeDestinationDriver _driver;

		public Vss3WayMerge()
		{
			InitializeComponent();

			var rootTempDir = Path.Combine(Path.GetTempPath(), "VSS3WayMerge");

			if (!Directory.Exists(rootTempDir))
				Directory.CreateDirectory(rootTempDir);

			_tempDir = Path.Combine(rootTempDir, Process.GetCurrentProcess().Id.ToString());

			PrepareTemp(rootTempDir);
		}

		void PrepareTemp(string rootTempDir)
		{
			var thisProcessId = Process.GetCurrentProcess().Id;

			// silently clear previous temp files
			foreach (var fse in Directory.EnumerateFileSystemEntries(rootTempDir))
			{
				try
				{
					if (File.Exists(fse))
					{
						File.SetAttributes(fse, FileAttributes.Normal);
						File.Delete(fse);
					}
					else if (Directory.Exists(fse))
					{
						var dirName = Path.GetFileName(fse);
						int id;
						if (Int32.TryParse(dirName, out id) && id != thisProcessId)
						{
							// do not delete temp of already running instances
							try
							{
								var p = Process.GetProcessById(id);
								if (!p.HasExited)
									continue;
							}
							catch
							{
							}
						}

						foreach (var subfse in Directory.EnumerateFileSystemEntries(fse, "*.*", SearchOption.AllDirectories))
						{
							if (File.Exists(subfse))
								File.SetAttributes(subfse, FileAttributes.Normal);
						}

						Directory.Delete(fse, true);
					}
				}
				catch
				{
				}
			}

			if (!Directory.Exists(_tempDir))
				Directory.CreateDirectory(_tempDir);
		}

		IDisposable StartBulkOperation()
		{
			_bulkOperation = true;

			return Disposable.Create(() => _bulkOperation = false);
		}

		VSSDatabase _mineVss;
		VSSDatabase _theirsVss;

		List<VssChangeAtom> _listItems = new List<VssChangeAtom>();
		List<VssChangeAtom> _listItemsNonFiltered = new List<VssChangeAtom>();

		void buttonParseForMergeList_Click(object sender, EventArgs e)
		{
			try
			{
				using (new Waiter(this))
				using (StartBulkOperation())
				{
					Settings.Default.Save();

					tabControl.SelectedIndex = 1;

					ParseChanges();
				}
			}
			catch (Exception ex)
			{
				ShowError(ex.Message);
			}
		}

		void ParseChanges()
		{
			_driver = null;

			// cleanup
			textBoxLog.Clear();

			if (_mineVss != null)
			{
				_mineVss.Close();
				_mineVss = null;
			}

			if (_theirsVss != null)
			{
				_theirsVss.Close();
				_theirsVss = null;
			}

			var theirsSsIni = GetIniPath(textBoxVssIniTheirs.Text, "'theirs'");
			if (theirsSsIni == null)
				return;

			var mineSsIni = GetIniPath(textBoxVssIniMine.Text, "'mine'");
			if (mineSsIni == null)
				return;

			_mineVss = new VSSDatabase();
			_mineVss.Open(mineSsIni, textBoxMineUser.Text, textBoxMinePwd.Text);

			_theirsVss = new VSSDatabase();
			_theirsVss.Open(theirsSsIni, textBoxTheirsUser.Text, textBoxTheirsPwd.Text);

			// start driver
			if (radioButtonVssConnected.Checked)
			{
				_driver = new VssDriver(this, _mineVss);
			}
			else if (radioButtonDetached.Checked)
			{
				_driver = new DetachedDriver(this, textBoxDetachedMergeDestination.Text);
			}

			if (_driver == null || !_driver.InitDriver())
				return;

			var hasParsingErrors = false;

			var newListItems = new List<VssChangeAtom>();
			foreach (var line in textBoxForMergeUnparsedList.Text.Split('\n').Select(l => l.Trim()).Where(l => l != ""))
			{
				try
				{
					var arr = line.Split('	');
					var ca = new VssChangeAtom { Spec = arr[0], BaseVersion = Int32.Parse(arr[1]) - 1 };
					if (arr.Length > 2)
					{
						ca.Status = Status.Unmergeable;
						ca.StatusDetails = arr[2];
					}

					ca.OriginalLine = line;

					_driver.Init(ca);
					newListItems.Add(ca);
				}
				catch (Exception ex)
				{
					AddLog("Invalid line: " + line + "\r\n" + ex.Message + "\r\n");
					hasParsingErrors = true;
				}
			}

			if (hasParsingErrors)
			{
				ShowError("Some lines parsed with errors. See log.");
			}

			_listItemsNonFiltered = newListItems;
			_listItems = _listItemsNonFiltered.ToList();

			listViewChanged.VirtualListSize = _listItems.Count;

			AddLog("Scan completes.", false);
		}

		void AddLog(string msg, bool activateLog = true)
		{
			textBoxLog.Text += msg.Replace("\r", "").Replace("\n", "\r\n") + "\r\n";
			if (activateLog)
				tabControl.SelectedIndex = 2;
		}

		bool CompareBases(VssChangeAtom ca)
		{
			using (var mineStream = File.OpenRead(ca.MineBasePath))
			using (var theirsStream = File.OpenRead(ca.TheirsBasePath))
			{
				var mineHash = _hash.ComputeHash(mineStream);
				var theirsHash = _hash.ComputeHash(theirsStream);

				var mineHashS = Convert.ToBase64String(mineHash);
				var theirsHashS = Convert.ToBase64String(theirsHash);

				if (mineHashS != theirsHashS)
				{
					ca.BasesDiffer = true;
					if (!_bulkOperation)
					{
						ShowError(@"Mine and Theirs bases different.

This means that split time selected incorrectly(later then real split)
or file was recreated. Compare bases for analyze difference.
For merge will be used mine base.
", ca.Spec, "");
					}
					else
					{
						ca.Status = Status.Error;
						ca.StatusDetails = "Mine and Theirs bases different! Perform manual merge.";
						return false;
					}
				}
			}
			return true;
		}

		bool EnsureTheirsBase(VssChangeAtom ca)
		{
			try
			{
				if (ca.TheirsBasePath == null)
				{
					if (!EnsureTempPath(ca))
						return false;

					ca.TheirsBasePath = LoadContent(ca, _theirsVss, ca.Spec, ca.TempDir, "base.of.theirs", ca.BaseVersion);

					if (ca.MineBasePath != null && ca.TheirsBasePath != null)
						return CompareBases(ca);
				}
			}
			catch (Exception ex)
			{
				if (!_bulkOperation)
				{
					ShowError(ex.Message, ca.Spec, "TheirsBase");
				}

				ca.Status = Status.Error;
				ca.StatusDetails = "TheirsBase: " + ex.Message;

				return false;
			}
			return ca.TheirsBasePath != null;
		}

		bool EnsureMineBase(VssChangeAtom ca)
		{
			try
			{
				if (ca.MineBasePath == null)
				{
					if (!EnsureTempPath(ca))
						return false;

					ca.MineBasePath = LoadContent(ca, _mineVss, ca.Spec, ca.TempDir, "base.of.mine", ca.BaseVersion);

					if (ca.TheirsBasePath != null && ca.MineBasePath != null)
						return CompareBases(ca);
				}
			}
			catch (Exception ex)
			{
				if (!_bulkOperation)
				{
					ShowError(ex.Message, ca.Spec, "MineBase");
				}

				ca.Status = Status.Error;
				ca.StatusDetails = "MineBase: " + ex.Message;

				return false;
			}
			return ca.MineBasePath != null;
		}

		bool EnsureTheirs(VssChangeAtom ca)
		{
			if (ca.TheirsPath != null)
				return true;

			try
			{
				if (!EnsureTempPath(ca))
					return false;

				if (ca.TheirsHead == 0)
					ca.TheirsHead = _theirsVss.VSSItem[ca.Spec].VersionNumber;

				ca.TheirsPath = LoadContent(ca, _theirsVss, ca.Spec, ca.TempDir, "theirs", ca.TheirsHead);

				return ca.TheirsPath != null;
			}
			catch (Exception ex)
			{
				if (!_bulkOperation)
				{
					ShowError(ex.Message, ca.Spec, "Theirs");
				}

				ca.Status = Status.Error;
				ca.StatusDetails = "Theirs: " + ex.Message;
				return false;
			}
		}

		bool EnsureMine(VssChangeAtom ca)
		{
			if (ca.MinePath != null)
				return true;

			try
			{
				if (!EnsureTempPath(ca))
					return false;

				if (ca.MineHead == 0)
					ca.MineHead = _mineVss.VSSItem[ca.Spec].VersionNumber;

				ca.MinePath = LoadContent(ca, _mineVss, ca.Spec, ca.TempDir, "mine", ca.MineHead);

				return ca.MinePath != null;
			}
			catch (Exception ex)
			{
				if (!_bulkOperation)
				{
					ShowError(ex.Message, ca.Spec, "Mine");
				}

				ca.Status = Status.Error;
				ca.StatusDetails = "Mine: " + ex.Message;

				return false;
			}
		}

		bool EnsureTempPath(VssChangeAtom ca)
		{
			if (ca.TempDir != null)
				return true;

			try
			{
				ca.TempDir = Path.Combine(_tempDir, Guid.NewGuid().ToString("N"));
				Directory.CreateDirectory(ca.TempDir);

				return true;
			}
			catch (Exception ex)
			{
				ca.Status = Status.Error;
				ca.StatusDetails = "Temp: " + ex.Message;

				if (!_bulkOperation)
				{
					ShowError(ex.Message, ca.Spec, "Temp");
				}

				return false;
			}
		}

		bool EnsureMergeDestination(VssChangeAtom ca)
		{
			return _driver.EnsureMergeDestination(ca);
		}

		void ShowError(string error, string spec = null, string ssIni = null)
		{
			var sb = new StringBuilder();
			if (!string.IsNullOrWhiteSpace(ssIni))
			{
				AddLog(ssIni, false);
				sb.AppendLine(ssIni);
				sb.AppendLine();
			}

			if (!string.IsNullOrWhiteSpace(spec))
			{
				AddLog(spec, false);
				sb.AppendLine(spec);
				sb.AppendLine();
			}

			sb.AppendLine(error);

			AddLog("	" + error, false);
			AddLog("", false);

			MessageBox.Show(this, sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		string LoadContent(VssChangeAtom ca, IVSSDatabase vss, string spec, string temp, string infix, int version = -1)
		{
			var item = vss.VSSItem[spec];

			if (version != -1)
			{
				if (item.VersionNumber != version)
				{
					if (item.VersionNumber < version)
					{
						var err = string.Format("Verson {0} as '{1}' absent. Head version: {2}", version, infix, item.VersionNumber);
						if (_bulkOperation)
						{
							ca.Status = Status.Error;
							ca.StatusDetails = err;
						}
						else
						{
							ShowError(err, spec, vss.SrcSafeIni);
						}
						return null;
					}
					item = item.Version[version];
				}
			}

			var localSpec = Path.Combine(temp, Path.GetFileNameWithoutExtension(spec) + "." + infix + Path.GetExtension(spec));

			item.Get(localSpec, (int)VSSFlags.VSSFLAG_FORCEDIRNO | (int)VSSFlags.VSSFLAG_USERRONO | (int)VSSFlags.VSSFLAG_REPREPLACE);

			File.SetAttributes(localSpec, FileAttributes.ReadOnly);

			return localSpec;
		}

		public string GetIniPath(string enteredValue, string hint)
		{
			var ssIni = enteredValue;
			if (!string.IsNullOrWhiteSpace(ssIni) && Directory.Exists(ssIni))
			{
				ssIni = Path.Combine(ssIni, "srcsafe.ini");

				if (!File.Exists(ssIni))
				{
					ShowError("srcsafe.ini not found in '" + enteredValue + "' path for " + hint);
					return null;
				}
			}

			if (Path.GetFileName(ssIni).ToLowerInvariant() != "srcsafe.ini")
			{
				ShowError("Should be specified VSS dir or scrsafe.ini path for " + hint);
				return null;
			}

			return ssIni;
		}

		void contextMenuStripDiffs_Opening(object sender, CancelEventArgs e)
		{
			var singleItemSelected = listViewChanged.SelectedIndices.Count == 1;
			var oneOrMoreSelected = listViewChanged.SelectedIndices.Count >= 1;

			var singleMergedSelected = (singleItemSelected && _listItems[listViewChanged.SelectedIndices[0]].IsMerged);
			var singleWithMergeDstSelected = (singleItemSelected && _listItems[listViewChanged.SelectedIndices[0]].MergedPath != null);
			var singleMergeableSelected = (singleItemSelected && _listItems[listViewChanged.SelectedIndices[0]].Status != Status.Unmergeable);
			var oneOrMoreMergeableSelected = listViewChanged.SelectedIndices.Cast<int>().Count(i => _listItems[i].Status != Status.Unmergeable) > 0;

			mergeNonInteractiveToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;

			threeWayMergeToolStripMenuItem.Visible = singleMergeableSelected && singleWithMergeDstSelected;
			threeWayDiffToolStripMenuItem.Visible = singleMergeableSelected && !singleWithMergeDstSelected;
	
			mineNewToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			theirsNewToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			theirsChnagesAsUnifiedDiffToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			baseDiffToolStripMenuItem.Enabled = singleMergeableSelected;
			headsDiffToolStripMenuItem.Enabled = singleMergeableSelected;
			
			checkMergedToolStripMenuItem.Enabled = singleMergedSelected;

			resolveAsMineToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			resolveAsTheirsToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			revertresetOrRemoveToolStripMenuItem.Enabled = oneOrMoreMergeableSelected;
			excludeFromListToolStripMenuItem.Enabled = oneOrMoreSelected;
		}

		VssChangeAtom GetFocusedMergeableItem()
		{
			if (listViewChanged.FocusedItem == null)
				return null;

			var lvi = listViewChanged.FocusedItem;

			var ca = (lvi.Tag as VssChangeAtom);
			if (ca == null)
			{
				ShowError("Can't perform operation on 'unmergeable' item");
				return null;
			}

			_bulkOperation = false;

			return ca;
		}

		void mergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null && EnsureMine(ca) && EnsureTheirs(ca) && EnsureMineBase(ca) && EnsureTheirsBase(ca) && EnsureMergeDestination(ca))
			{
				if (StartMerger(ca.TheirsPath, ca.MinePath, ca.TheirsBasePath, ca.MergedPath))
				{
					ca.Status = Status.Merged;
					ca.StatusDetails = null;
				}
			}

			listViewChanged.Refresh();
		}

		void mineNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var pairs = GetSelectedMergeableItems()
				.Select(ca => {
					EnsureMineBase(ca);
					EnsureMine(ca);
					return Tuple.Create(ca.MineBasePath, ca.MinePath);
				})
				.ToArray()
			;

			StartDiff("mine", pairs);

			listViewChanged.Refresh();
		}

		void StartDiff(string fileNameBase, IList<Tuple<string, string>> pairs)
		{
			if (pairs.Count > 0 && pairs.All(p => p.Item1 != null && p.Item2 != null))
			{
				if (pairs.Count == 1)
				{
					StartMerger(pairs[0].Item1, pairs[0].Item2);
				}
				else
				{
					StartMultidiff(fileNameBase, pairs);
				}
			}
		}

		void theirsNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var pairs = GetSelectedMergeableItems()
				.Select(ca => {
					EnsureTheirsBase(ca);
					EnsureTheirs(ca);
					return Tuple.Create(ca.TheirsBasePath, ca.TheirsPath);
				})
				.ToArray()
			;

			StartDiff("theirs", pairs);

			listViewChanged.Refresh();
		}

		void StartMultidiff(string baseFileName, IEnumerable<Tuple<string, string>> pairs)
		{
			// join all files in single 'portianka'
			var temp = Path.Combine(_tempDir, Guid.NewGuid().ToString("N"));
			Directory.CreateDirectory(temp);

			var left = Path.Combine(temp, baseFileName + ".base.txt");
			var right = Path.Combine(temp, baseFileName + ".txt");

			using (var leftStream = File.Create(left))
			using (var rightStream = File.Create(right))
			{
				foreach (var pair in pairs)
				{
					using (var s = File.Open(pair.Item1, FileMode.Open, FileAccess.Read))
					{
						s.CopyTo(leftStream);
					}
					using (var s = File.Open(pair.Item2, FileMode.Open, FileAccess.Read))
					{
						s.CopyTo(rightStream);
					}
					var guid = Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N") + " " + Guid.NewGuid().ToString("N");

					AddPadding(leftStream, guid);
					AddPadding(rightStream, guid);
				}
			}

			StartMerger(left, right);
		}

		static void AddPadding(FileStream leftStream, string guid)
		{
			var tw = new StreamWriter(leftStream);
			tw.WriteLine();
			tw.WriteLine("======================================= PADDING =======================================");
			for (var i = 0; i < 50; i++)
			{
				tw.WriteLine(guid);
			}
			tw.WriteLine("======================================= PADDING =======================================");
			tw.Flush();
		}

		void basesDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null && EnsureTheirsBase(ca) && EnsureMineBase(ca))
			{
				StartMerger(ca.TheirsBasePath, ca.MineBasePath);
			}

			listViewChanged.Refresh();
		}

		void headsDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null && EnsureTheirs(ca) && EnsureMine(ca))
			{
				StartMerger(ca.TheirsPath, ca.MinePath);
			}

			listViewChanged.Refresh();
		}

		bool StartMerger(string leftPath, string rightPath, string basePath = null, string mergePath = null)
		{
			var mtool = comboBoxMerger.SelectedItem as MergerToolDefinition;

			if (mtool == null)
			{
				mtool = new MergerToolDefinition {
					Exe = textBoxCustomMergeExe.Text,
					Diff = textBoxCustomDiff.Text,
					Merge = textBoxCustomMerge.Text,
				};
			}

			if (mergePath != null && !File.Exists(mergePath))
				File.Create(mergePath).Close();

			string strTemplate;
			if (basePath == null)
			{
				strTemplate = mtool.Diff;
			}
			else
			{
				strTemplate = mtool.Merge;
			}


			var args = strTemplate
				.Replace("$LEFT$", '"' + (leftPath ?? "") + '"')
				.Replace("$RIGHT$", '"' + (rightPath ?? "") + '"')
				.Replace("$BASE$", '"' + (basePath ?? "") + '"')
				.Replace("$MERGED$", '"' + (mergePath ?? "") + '"')
			;

			// remember modification time
			var mergeDestLastMod = DateTime.Now;
			if(mergePath != null)
				mergeDestLastMod = File.GetLastWriteTimeUtc(mergePath);

			try
			{
				var p = Process.Start(mtool.Exe, args);
				p.WaitForExit();

				if (p.ExitCode == 0 && mergePath != null)
				{
					// merge was saved ?
					return File.GetLastWriteTimeUtc(mergePath) > mergeDestLastMod;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}

			return false;
		}

		void mergedDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null && EnsureMine(ca))
			{
				StartMerger(ca.MinePath, ca.MergedPath);
			}

			listViewChanged.Refresh();
		}

		void Vss3WayMerge_Load(object sender, EventArgs e)
		{
			// enable .NET 2.0 (SharpSvn)
			try
			{
				var clrRuntimeInfo = (ICLRRuntimeInfo)RuntimeEnvironment.GetRuntimeInterfaceAsObject(Guid.Empty, typeof(ICLRRuntimeInfo).GUID);
				clrRuntimeInfo.BindAsLegacyV2Runtime();
			}
			catch (Exception)
			{
			}

			// Get a handle to a copy of this form's system (window) menu
			var hSysMenu = GetSystemMenu(this.Handle, false);

			// Add a separator
			AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

			// Add the About menu item
			AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About");

			comboBoxMerger.Items.Clear();
			comboBoxMerger.Items.Add("Custom...");
			foreach (var mtool in new MergerToolsManager().GetMergerTools())
			{
				comboBoxMerger.Items.Add(mtool);
			}
			
			comboBoxMerger.SelectedIndex = 1;
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			// Test if the About item was selected from the system menu
			if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
			{
				new AboutBox().Show(this);
			}
		}

		void comboBoxMerger_SelectedIndexChanged(object sender, EventArgs e)
		{
			var mtool = comboBoxMerger.SelectedItem as MergerToolDefinition;

			if (mtool == null)
				return;

			textBoxCustomMergeExe.Text = mtool.Exe;
			textBoxCustomDiff.Text = mtool.Diff;
			textBoxCustomMerge.Text = mtool.Merge;
		}

		void textBoxCustomMergeExe_KeyUp(object sender, KeyEventArgs e)
		{
			comboBoxMerger.SelectedIndex = 0;
		}

		void resolveAsMineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var ca in GetSelectedMergeableItems())
			{
				try{
					if (!EnsureMine(ca) || !_driver.EnsureMergeDestination(ca))
						continue;

					File.Copy(ca.MinePath, ca.MergedPath, true);
					File.SetAttributes(ca.MergedPath, FileAttributes.Normal); 
					ca.Status = Status.ResolvedAsMine;
					ca.StatusDetails = null;
				}
				catch(Exception ex)
				{
					ca.Status = Status.Error;
					ca.StatusDetails = ex.Message;
				}
				/*
				try
				{
					var item = _mineVss.VSSItem[ca.Spec];

					if (item.IsCheckedOut != 0)
					{
						// replace content with mine head
						item.UndoCheckout();

						ca.MergedPath = null;

						selectedItem.SubItems[5].Text = "";
					}
					selectedItem.SubItems[4].Text = "Mine";
				}
				catch (Exception ex)
				{
					if (MessageBox.Show("Item " + ca.Spec + " error:\n" + ex.Message, "Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
						return;
				}
				*/
			}

			listViewChanged.Refresh();
		}

		void resolveAsTheirsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var ca in GetSelectedMergeableItems())
			{
				try
				{
					if (!EnsureTheirs(ca) || !_driver.EnsureMergeDestination(ca))
						continue;

					File.Copy(ca.TheirsPath, ca.MergedPath, true);
					File.SetAttributes(ca.MergedPath, FileAttributes.Normal);
					ca.Status = Status.ResolvedAsTheirs;
					ca.StatusDetails = null;
				}
				catch (Exception ex)
				{
					ca.Status = Status.Error;
					ca.StatusDetails = ex.Message;
				}
				/*
				try
				{
					if (!EnsureTheirs(ca))
						continue;

					var item = _mineVss.VSSItem[ca.Spec];

					if (item.IsCheckedOut == 0)
						item.Checkout();

					ca.MergedPath = item.LocalSpec;

					File.Copy(ca.TheirsPath, ca.MergedPath, true);

					selectedItem.SubItems[4].Text = "Theirs";
					selectedItem.SubItems[5].Text = "ChOut: " + item.LocalSpec;
				}
				catch (Exception ex)
				{
					if (MessageBox.Show("Item " + ca.Spec + " error:\n" + ex.Message, "Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
						return;
				}
				 * */
			}
			listViewChanged.Refresh();
		}

		void threeWayDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null && EnsureMine(ca) && EnsureTheirs(ca) && EnsureMineBase(ca) && EnsureTheirsBase(ca))
			{
				StartMerger(ca.TheirsPath, ca.MinePath, ca.TheirsBasePath, ca.MergedPath);
			}

			listViewChanged.Refresh();
		}

		void listViewChanged_DoubleClick(object sender, EventArgs e)
		{
			var ca = GetFocusedMergeableItem();

			if (ca != null)
			{
				if (ca.MergedPath != null)
					mergeToolStripMenuItem_Click(sender, e);
				else
					threeWayDiffToolStripMenuItem_Click(sender, e);
			}
		}

		bool _bulkOperation;

		VssChangeAtom[] GetSelectedMergeableItems()
		{
			var unmergeableCount = 0;
			var lastUnmergeableSpec = "";

			var selected = listViewChanged.SelectedIndices
				.Cast<int>()
				.Select(i => _listItems[i])
				.Where(ca => {
					if(ca.Status == Status.Unmergeable)
					{
						lastUnmergeableSpec = ca.Spec;
						unmergeableCount++;
						return false;
					}

					return true;
				})
				.ToArray()
			;

			if (selected.Length == 0 && unmergeableCount > 0)
			{
				ShowError("Can't perform operation on 'unmergeable' item", lastUnmergeableSpec);
			}

			_bulkOperation = selected.Length > 1;

			return selected;
		}

		void mergeNoninteractiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (new Waiter(this))
			{
				foreach (var ca in GetSelectedMergeableItems())
				{
					try
					{
						SvnAutoMerge(ca);
					}
					catch (Exception ex)
					{
						ca.Status = Status.Error;
						ca.MergedPath = null;
						ca.StatusDetails = ex.Message;
					}
				}

				listViewChanged.Refresh();
			}
		}

		void SvnAutoMerge(VssChangeAtom ca)
		{
			if (!EnsureMine(ca) || !EnsureTheirs(ca) || !EnsureMineBase(ca) || !EnsureTheirsBase(ca))
				return;

			if (!EnsureMergeDestination(ca))
				return;

			SvnFileDiff diff;
			SvnFileDiff.TryCreate(ca.TheirsBasePath, ca.TheirsPath, ca.MinePath, new SvnFileDiffArgs(), out diff);

			var margs = new SvnDiffWriteMergedArgs {
				OriginalPath = ca.TheirsBasePath,
				ModifiedPath = ca.TheirsPath,
				LatestPath = ca.MinePath,
				ConflictModified = "<<<<<<< " + Path.GetFileName(ca.TheirsPath),
				ConflictLatest = ">>>>>>> " + Path.GetFileName(ca.MinePath),
			};

			// by some reason WriteMerge do not accept FileStream (at least some time...)
			using (var fs = File.Create(ca.MergedPath))
				diff.WriteMerged(fs, margs);

			if (diff.HasConflicts)
				ca.Status = Status.Conflicted;
			else if (!diff.HasDifferences)
				ca.Status = Status.MergedNoChanges;
			else
				ca.Status = Status.Merged;

			ca.StatusDetails = null;
		}

		void theirsChnagesAsUnifiedDiffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = GetSelectedMergeableItems().ToArray();
			if (selected.Length == 0)
				return;

			var patchPath = Path.Combine(_tempDir, Guid.NewGuid().ToString("N") + ".patch");
			using (var ms = File.Create(patchPath))
			{
				foreach (var ca in selected)
				{
					if (!EnsureTheirs(ca) || !EnsureTheirsBase(ca))
						continue;

					SvnFileDiff diff;
					SvnFileDiff.TryCreate(ca.TheirsBasePath, ca.TheirsPath, new SvnFileDiffArgs(), out diff);

					var rel = ca.Spec.TrimStart("$/".ToCharArray()).Replace('\\', '/');

					var tw = new StreamWriter(ms);
					tw.WriteLine("Index: {0}", rel);
					tw.WriteLine("===================================================================");
					tw.Flush();

					var dargs = new SvnDiffWriteDifferencesArgs {
						OriginalHeader = rel,
						ModifiedHeader = rel
					};
					diff.WriteDifferences(ms, dargs);
				}
			}

			listViewChanged.Refresh();
		
			Process.Start(patchPath);
		}

		void excludeFromListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			listViewChanged.SelectedIndices
				.Cast<int>()
				.OrderBy(i => -i)
				.ToList()
				.ForEach(i => {
					var ca = _listItems[i];
					_listItems.Remove(ca);
					_listItemsNonFiltered.Remove(ca);
				})
			;
			listViewChanged.VirtualListSize = _listItems.Count;
			listViewChanged.SelectedIndices.Clear();
			listViewChanged.Refresh();
		}

		void revertResetOrRemoveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var ca in GetSelectedMergeableItems())
			{
				_driver.Reset(ca);
			}
			listViewChanged.Refresh();
		}

		void listViewChanged_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			var ca = _listItems[e.ItemIndex];

			if (ca.Status == Status.Unmergeable)
			{
				e.Item = new ListViewItem(new[] { ca.Spec, "", "Unmergeable", ca.StatusDetails ?? "", "" });
				e.Item.ForeColor = Color.Gray;
			}
			else
			{
				var sb = new StringBuilder();
				if (ca.BasesDiffer)
				{
					sb.Append("BASES DIFFER! ");
				}

				if (ca.TheirsHead != 0)
				{
					// it is impossible have theirs without changes
					Debug.Assert(ca.TheirsHead != ca.BaseVersion);
					sb.AppendFormat("{0} <- ", ca.TheirsHead);
				}
				sb.AppendFormat("{0}", ca.BaseVersion);

				var trivialDiff = false;
				if (ca.MineHead != 0)
				{
					if (ca.MineHead == ca.BaseVersion)
					{
						// trivial change (only in theirs)
						sb.AppendFormat(" -> *");
						sb.Insert(0, "T: ");
						trivialDiff = true;
					}
					else
					{
						sb.AppendFormat(" -> {0}", ca.MineHead);
					}
				}

				var foreColor = Color.Black;
				string status;
				switch(ca.Status)
				{
					case Status.Nothing:
						status = "";
						break;
					case Status.Error:
						status = "ERROR";
						foreColor = Color.Red;
						break;
					case Status.Conflicted:
						status = "Conflict";
						foreColor = Color.Magenta;
						break;
					case Status.Merged:
						status = "Merged";
						foreColor = trivialDiff ? Color.SteelBlue : Color.DarkGreen;
						break;
					case Status.MergedNoChanges:
						status = "Merged, no changes";
						foreColor = Color.LightGreen;
						break;
					case Status.ResolvedAsMine:
						status = "Resolved to mine";
						foreColor = Color.MediumAquamarine;
						break;
					case Status.ResolvedAsTheirs:
						status = "Resolved to thirs";
						foreColor = Color.MediumAquamarine;
						break;
					default:
						status = ca.Status.ToString();
						break;
				}

				e.Item = new ListViewItem(new[] { ca.Spec, sb.ToString(), status, ca.StatusDetails ?? "", ca.MergedPath ?? "" }) { Tag = ca };
				e.Item.ForeColor = foreColor;
			}
		}

		void buttonFindJournal_Click(object sender, EventArgs e)
		{
			using (new Waiter(this))
			{
				var ssIniPath = GetIniPath(textBoxVssIniTheirs.Text, "'theirs'");
				if (ssIniPath == null)
					return;

				// find journal file:
				textBoxJournal.Text = File.ReadAllLines(ssIniPath)
					.Select(l => l.Trim().ToLowerInvariant())
					.Where(l => l.StartsWith("journal_file"))
					.Select(l => l.Substring(l.IndexOf('=') + 1).Trim())
					.FirstOrDefault()
				;
				Settings.Default.Save();
			}
		}

		void buttonParseJournal_Click(object sender, EventArgs e)
		{
			if (textBoxJournal.Text.Trim() == "")
			{
				MessageBox.Show("Journal file(s) not specified.");
				return;
			}

			Settings.Default.Save();

			using (new Waiter(this))
			{
				var journals = textBoxJournal.Text.Split(';');

				var nodes = new VssJournalPlayer().Play(journals);

				var str = nodes
					.Aggregate(new StringBuilder(), (sb, n) =>
					{

						sb.AppendFormat("{0}	{1}", n.Spec, n.FirstChange);
						if (!n.IsPureModified)
						{
							sb.AppendFormat("	");

							if (n.IsAdded)
								sb.AppendFormat(" added");
							if (n.IsArchived)
								sb.AppendFormat(" archived");
							if (n.IsBranched)
								sb.AppendFormat(" branched");
							if (n.IsDeleted)
							{
								if (n.IsDeletedByMove)
									sb.AppendFormat(" deleted_by_move");
								else
									sb.AppendFormat(" deleted");
							}
							if (n.IsModified)
								sb.AppendFormat(" modified");
							if (n.IsRecovered)
								sb.AppendFormat(" recovered");
							if (n.IsRolledback)
								sb.AppendFormat(" rolledback");
							if (n.MovedFrom != null)
								sb.AppendFormat(" moved_from({0})", n.MovedFrom);
							if (n.SharedFrom != null)
								sb.AppendFormat(" shared_from({0})", n.SharedFrom);
							if (n.CopiedFrom != null)
								sb.AppendFormat(" copied_from({0})", n.CopiedFrom);
						}

						sb.AppendFormat("\r\n");

						return sb;
					})
				;

				textBoxForMergeUnparsedList.Text = str.ToString();
			}
		}

		void textBoxForMergeUnparsedList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyCode == Keys.A))
			{
				((TextBox)sender).SelectAll();
				e.Handled = true;
			}
		}

		void statsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var sb = new StringBuilder();
			sb.AppendLine("=== All items ===");
			CalcStats(_listItemsNonFiltered, sb);

			if (_listItems.Count != _listItemsNonFiltered.Count)
			{
				sb.AppendLine("\n");
				sb.AppendLine("=== Filtered view ===");
				CalcStats(_listItems, sb);
			}
			if (listViewChanged.SelectedIndices.Count != 0 && listViewChanged.SelectedIndices.Count != _listItems.Count)
			{
				sb.AppendLine("\n");
				sb.AppendLine("=== Selected items ===");
				CalcStats(listViewChanged.SelectedIndices.Cast<int>().Select(i => _listItems[i]).ToList(), sb);
			}

			AddLog(sb.ToString());

			MessageBox.Show(this, sb.ToString(), "Stats", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		static void CalcStats(ICollection<VssChangeAtom> list, StringBuilder sb)
		{
			var total = list.Count;
			var unmergeable = list.Count(ca => ca.Status == Status.Unmergeable);
			var mergeable = list.Count(ca => ca.Status != Status.Unmergeable);
			var mergedAndResolved =
				list.Count(
					ca =>
					ca.Status == Status.Merged || ca.Status == Status.MergedNoChanges || ca.Status == Status.ResolvedAsMine ||
					ca.Status == Status.ResolvedAsTheirs);
			var conflicted = list.Count(ca => ca.Status == Status.Conflicted);
			var pending = list.Count(ca => ca.Status == Status.Nothing);
			var errored = list.Count(ca => ca.Status == Status.Error);

			sb.AppendFormat("Total items: {0}:\r\n", total);
			sb.AppendFormat("    Unmergeable: {0}\r\n", unmergeable);
			sb.AppendFormat("    Mergeable: {0}:\r\n", mergeable);
			sb.AppendFormat("        Merged or resolved: {0}\r\n", mergedAndResolved);
			sb.AppendFormat("        Conflicted: {0}\r\n", conflicted);
			sb.AppendFormat("        Erroneous: {0}\r\n", errored);
			sb.AppendFormat("        Pending for merge: {0}", pending);
		}

		#region Files filter
		void toolStripTextBoxPathsFilter_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				toolStripTextBoxPathsFilter.ForeColor = Color.Black;

				if (string.IsNullOrWhiteSpace(toolStripTextBoxPathsFilter.Text))
				{
					_listItems = _listItemsNonFiltered.ToList();
					toolStripTextBoxPathsFilter.Text = "file path regex filter";
					toolStripTextBoxPathsFilter.ForeColor = Color.Silver;
				}
				else
				{
					var rx = new Regex(toolStripTextBoxPathsFilter.Text, RegexOptions.IgnoreCase);

					_listItems = _listItemsNonFiltered.Where(ca => rx.IsMatch(ca.Spec)).ToList();
				}

				listViewChanged.VirtualListSize = _listItems.Count;
				listViewChanged.Refresh();

				contextMenuStripDiffs.Close(ToolStripDropDownCloseReason.Keyboard);
			}
		}

		void toolStripTextBoxPathsFilter_Click(object sender, EventArgs e)
		{
			if (toolStripTextBoxPathsFilter.ForeColor != Color.Black)
			{
				toolStripTextBoxPathsFilter.Text = "";
				toolStripTextBoxPathsFilter.ForeColor = Color.Black;
			}
		}

		void toolStripTextBoxPathsFilter_TextChanged(object sender, EventArgs e)
		{
			toolStripTextBoxPathsFilter.ForeColor = Color.Black;
		}

		void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_listItems = _listItemsNonFiltered.ToList();
			listViewChanged.VirtualListSize = _listItems.Count;
			listViewChanged.Refresh();
			toolStripTextBoxPathsFilter.Text = "file path regex filter";
			toolStripTextBoxPathsFilter.ForeColor = Color.Silver;
		}

		void filterToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			clearToolStripMenuItem.Enabled = _listItems.Count != _listItemsNonFiltered.Count;
			onlySelectedToolStripMenuItem.Enabled = listViewChanged.SelectedIndices.Count > 0;
			filterexludeSelectionToolStripMenuItem.Enabled = listViewChanged.SelectedIndices.Count > 0;
		}

		void onlySelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_listItems = _listItems
				.Select((ca, i) => listViewChanged.SelectedIndices.Contains(i) ? ca : null)
				.Where(ca => ca != null)
				.ToList()
			;

			listViewChanged.VirtualListSize = _listItems.Count;
			listViewChanged.Refresh();
			listViewChanged.SelectedIndices.Clear();
		}

		void filterToSelectionInversionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_listItems = _listItems
				.Select((ca, i) => !listViewChanged.SelectedIndices.Contains(i) ? ca : null)
				.Where(ca => ca != null)
				.ToList()
			;

			listViewChanged.VirtualListSize = _listItems.Count;
			listViewChanged.Refresh();
			listViewChanged.SelectedIndices.Clear();
		}
		#endregion

		void copySelectedItemsOriginals_Click(object sender, EventArgs e)
		{
			var text = listViewChanged
				.SelectedIndices
				.Cast<int>()
				.Aggregate(new StringBuilder(), (sb, i) => { sb.AppendLine(_listItems[i].OriginalLine); return sb; })
				.ToString()
			;

			Clipboard.SetText(text);

			MessageBox.Show(this, "Parseable lines for selected items copied to clipboard", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		void listViewChanged_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && (e.KeyCode == Keys.A))
			{
				listViewChanged.SelectedIndices.Clear();
				for (var i = 0; i < _listItems.Count; i++)
				{
					listViewChanged.SelectedIndices.Add(i);
				}
				e.Handled = true;
			}
		}

		readonly Dictionary<int, bool> _sortOrder = new Dictionary<int, bool>();

		void listViewChanged_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (_listItems.Count < 2)
				return;

			if (e.Column != 0 && e.Column != 1 && e.Column != 2 && e.Column != 3 && e.Column != 4)
				return;

			Func<VssChangeAtom, string> keySelector = a => {

				switch (e.Column)
				{
					case 0:
						return a.Spec.ToLowerInvariant();
					case 1:
						return a.BaseVersion.ToString("D5");
					case 2:
						return a.Status.ToString();
					case 3:
						return a.StatusDetails ?? "";
					case 4:
						return a.MergedPath ?? "";
				}

				return "";
			};

			bool ascending;
			_sortOrder.TryGetValue(e.Column, out ascending);
			_sortOrder[e.Column] = !ascending;

			if(ascending)
			{
				_listItems = _listItems
					.OrderBy(keySelector)
					.ToList()
				;
				_listItemsNonFiltered = _listItemsNonFiltered
					.OrderBy(keySelector)
					.ToList()
				;
			}
			else
			{
				_listItems = _listItems
					.OrderByDescending(keySelector)
					.ToList()
				;
				_listItemsNonFiltered = _listItemsNonFiltered
					.OrderByDescending(keySelector)
					.ToList()
				;
			}

			listViewChanged.Refresh();
		}

		public void NotifyError(string message, string spec, string hint)
		{
			if (!_bulkOperation)
			{
				ShowError(message, spec, hint);
			}
			else
			{
				AddLog(string.Format("\r\nError: '{0}' on {1}\r\n	{2}", spec, hint, message));
			}
		}

		public DialogResult GetConfirmation(string message, string spec, string hints, MessageBoxButtons requestedButtons, MessageBoxDefaultButton defaultButton)
		{
			throw new NotImplementedException();
		}

		void Vss3WayMerge_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
			if (e.KeyCode == Keys.Enter)
			{
				threeWayMergeToolStripMenuItem.PerformClick();
			}
		}
	}
}
