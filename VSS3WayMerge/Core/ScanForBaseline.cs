using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SourceSafeTypeLib;
using Vss3WayMerge.VJP;
using vsslib;

namespace Vss3WayMerge.Core
{
	class ScanForBaseline
	{
		readonly VSSDatabase _vss;
		readonly CancellationToken _ct;
		readonly Regex _exclude;

		VssFileCache _cache;
		DateTime _baseTime;

		public ScanForBaseline(VSSDatabase vss, Regex exclude, CancellationToken ct)
		{
			_vss = vss;
			_ct = ct;
			_exclude = exclude;
		}

		class ItemHistoryEntry
		{
			public int VssVersion;
			public DateTime Modified;
			public VssChangeType Action;

			public ItemHistoryEntry()
			{
			}

			public ItemHistoryEntry(string s)
			{
				var arr = s.Split('\t');
				VssVersion = Int32.Parse(arr[0]);
				Action = (VssChangeType)Enum.Parse(typeof(VssChangeType), arr[1]);
				Modified = new DateTime(long.Parse(arr[2]), DateTimeKind.Utc);
			}

			public override string ToString()
			{
				return string.Format("{0}	{1}	{2}", VssVersion, Action, Modified.Ticks);
			}
		}

		class Item
		{
			public string Spec;
			public List<ItemHistoryEntry> History;

			public Item()
			{
			}

			public Item(string spec, string serialized)
			{
				Spec = spec;
				History = serialized
					.Split('\n')
					.Select(l => l.Trim())
					.Where(l => l != "")
					.Select(l => new ItemHistoryEntry(l))
					.ToList()
				;
			}

			public override string ToString()
			{
				var sb = new StringBuilder();

				History.ForEach(e => sb.AppendLine(e.ToString()));

				return sb.ToString();
			}
		}

		public string CurrentItemSpec;

		public List<string> Scan(string project, DateTime baseTime)
		{
			_baseTime = baseTime;

			if (project == "$")
				project = "$/";

			var ret = new List<string>();
			
			var vssItem = _vss.VSSItem[project];
			using (_cache = new VssFileCache(".rev-cache", _vss.SrcSafeIni))
			{
				ScanForHistory(vssItem.Spec, vssItem, ret);
			}

			return ret;
		}

		void ScanForHistory(string spec, IVSSItem vssItem, ICollection<string> ret)
		{
			_ct.ThrowIfCancellationRequested();

			CurrentItemSpec = spec;

			if (_exclude != null && _exclude.IsMatch(spec))
				return;

			if (vssItem.Type == 0)
			{
				foreach (IVSSItem child in vssItem.Items)
				{
					ScanForHistory(child.Spec, child, ret);
				}
			}
			else
			{
				var versionNumber = vssItem.VersionNumber;
				var timestamp = vssItem.VSSVersion.Date.Ticks;

				Item item;

				var cachedData = _cache.GetContent(spec, versionNumber, timestamp);
				if (cachedData != null)
				{
					item = new Item(spec, cachedData);
				}
				else
				{
					item = new Item {
						Spec = spec,
						History = LoadHistory(vssItem)
					};

					_cache.AddContent(spec, versionNumber, timestamp, item.ToString());
				}

				var newChanges = item.History.Where(h => h.Modified >= _baseTime).ToArray();

				if (newChanges.Length > 0)
				{
					var last = newChanges.Last();
					var cl = string.Format("{0}	{1}", spec, last.VssVersion);
					if (newChanges.Any(c => c.Action != VssChangeType.CheckIn && c.Action != VssChangeType.Branched))
					{
						cl += "\t" + string.Join(", ", newChanges.Where(c => c.Action != VssChangeType.CheckIn && c.Action != VssChangeType.Branched).Select(c => c.Action.ToString()).Distinct());
					}
					ret.Add(cl);
				}
			}
		}

		List<ItemHistoryEntry> LoadHistory(IVSSItem vssItem)
		{
			try
			{
				var ret = new List<ItemHistoryEntry>(vssItem.Versions.Count);

				foreach (IVSSVersion vssVersion in vssItem.Versions)
				{
					_ct.ThrowIfCancellationRequested();

					//var spec = vssItem.Spec;
					var action = vssVersion.Action;

					VssChangeType ct;

					if (action.StartsWith("Checked in "))
						ct = VssChangeType.CheckIn;
					else if (action.StartsWith("Created "))
						ct = VssChangeType.Created;
					else if (action.StartsWith("Labeled "))
						ct = VssChangeType.Labeled;
					else if (action.StartsWith("Branched at version "))
						ct = VssChangeType.Branched;
					else if (action.StartsWith("Archived versions of "))
						ct = VssChangeType.Branched;
					else if (action.StartsWith("Rollback to version "))
						ct = VssChangeType.Branched;
					else
					{
						File.AppendAllText("_unknown-actions", action + "\r\n");
						continue;
					}

					if (ct == VssChangeType.Labeled)
						continue;

					var histEnt = new ItemHistoryEntry
					{
						Action = ct,
						Modified = vssVersion.Date,
						VssVersion = vssVersion.VersionNumber
					};

					ret.Add(histEnt);
				}
				return ret;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				File.AppendAllText("_errors", string.Format("{0}: {1}\r\n\r\n\r\n", vssItem.Spec, ex));
				return new List<ItemHistoryEntry>();
			}
		}
	}
}
