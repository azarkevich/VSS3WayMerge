namespace Vss3WayMerge
{
	partial class Vss3WayMerge
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vss3WayMerge));
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageForMergeFiles = new System.Windows.Forms.TabPage();
			this.label12 = new System.Windows.Forms.Label();
			this.tabControlDetectChanges = new System.Windows.Forms.TabControl();
			this.tabPageDetectChangesByJournal = new System.Windows.Forms.TabPage();
			this.buttonFindJournal = new System.Windows.Forms.Button();
			this.buttonParseJournal = new System.Windows.Forms.Button();
			this.textBoxJournal = new System.Windows.Forms.TextBox();
			this.tabPageDetectChangesByDatabase = new System.Windows.Forms.TabPage();
			this.linkLabelScanRules = new System.Windows.Forms.LinkLabel();
			this.dateTimePickerBaseTime = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.dateTimePickerBaseDate = new System.Windows.Forms.DateTimePicker();
			this.textBoxScanProject = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonLoadVSSDB = new System.Windows.Forms.Button();
			this.radioButtonVssConnected = new System.Windows.Forms.RadioButton();
			this.radioButtonDetached = new System.Windows.Forms.RadioButton();
			this.buttonParseForMergeList = new System.Windows.Forms.Button();
			this.textBoxDetachedMergeDestination = new System.Windows.Forms.TextBox();
			this.textBoxTheirsPwd = new System.Windows.Forms.TextBox();
			this.textBoxForMergeUnparsedList = new System.Windows.Forms.TextBox();
			this.textBoxTheirsUser = new System.Windows.Forms.TextBox();
			this.textBoxVssIniTheirs = new System.Windows.Forms.TextBox();
			this.textBoxMinePwd = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxMineUser = new System.Windows.Forms.TextBox();
			this.textBoxVssIniMine = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tabPageDiffs = new System.Windows.Forms.TabPage();
			this.listViewChanged = new System.Windows.Forms.ListView();
			this.colDiffSpec = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDiffStats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDiffMergeStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colError = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMergeDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStripDiffs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mergeNonInteractiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.threeWayMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.threeWayDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOtherDiffs = new System.Windows.Forms.ToolStripMenuItem();
			this.mineNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.theirsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.theirsChnagesAsUnifiedDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.baseDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.headsDiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.checkMergedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.resolveAsMineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resolveAsTheirsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.revertresetOrRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.excludeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBoxPathsFilter = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.onlySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterexludeSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemCopyParseableToClip = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.tabPageOprions = new System.Windows.Forms.TabPage();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBoxCustomMerge = new System.Windows.Forms.TextBox();
			this.textBoxCustomDiff = new System.Windows.Forms.TextBox();
			this.textBoxCustomMergeExe = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBoxMerger = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tabControlSourceDestination = new System.Windows.Forms.TabControl();
			this.tabPageMergeSource = new System.Windows.Forms.TabPage();
			this.tabPageMergeDestination = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.tabPageForMergeFiles.SuspendLayout();
			this.tabControlDetectChanges.SuspendLayout();
			this.tabPageDetectChangesByJournal.SuspendLayout();
			this.tabPageDetectChangesByDatabase.SuspendLayout();
			this.tabPageDiffs.SuspendLayout();
			this.contextMenuStripDiffs.SuspendLayout();
			this.tabPageLog.SuspendLayout();
			this.tabPageOprions.SuspendLayout();
			this.tabControlSourceDestination.SuspendLayout();
			this.tabPageMergeSource.SuspendLayout();
			this.tabPageMergeDestination.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Mine VSS DB:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Theirs VSS DB:";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageForMergeFiles);
			this.tabControl.Controls.Add(this.tabPageDiffs);
			this.tabControl.Controls.Add(this.tabPageLog);
			this.tabControl.Controls.Add(this.tabPageOprions);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(1063, 556);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageForMergeFiles
			// 
			this.tabPageForMergeFiles.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageForMergeFiles.Controls.Add(this.tabControlSourceDestination);
			this.tabPageForMergeFiles.Controls.Add(this.tabControlDetectChanges);
			this.tabPageForMergeFiles.Controls.Add(this.buttonParseForMergeList);
			this.tabPageForMergeFiles.Controls.Add(this.textBoxForMergeUnparsedList);
			this.tabPageForMergeFiles.Location = new System.Drawing.Point(4, 22);
			this.tabPageForMergeFiles.Name = "tabPageForMergeFiles";
			this.tabPageForMergeFiles.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageForMergeFiles.Size = new System.Drawing.Size(1055, 530);
			this.tabPageForMergeFiles.TabIndex = 4;
			this.tabPageForMergeFiles.Text = "Files for merge";
			// 
			// label12
			// 
			this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(365, 9);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(87, 13);
			this.label12.TabIndex = 19;
			this.label12.Text = "User + Password";
			// 
			// tabControlDetectChanges
			// 
			this.tabControlDetectChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlDetectChanges.Controls.Add(this.tabPageDetectChangesByJournal);
			this.tabControlDetectChanges.Controls.Add(this.tabPageDetectChangesByDatabase);
			this.tabControlDetectChanges.Location = new System.Drawing.Point(6, 108);
			this.tabControlDetectChanges.Name = "tabControlDetectChanges";
			this.tabControlDetectChanges.SelectedIndex = 0;
			this.tabControlDetectChanges.Size = new System.Drawing.Size(1043, 66);
			this.tabControlDetectChanges.TabIndex = 18;
			// 
			// tabPageDetectChangesByJournal
			// 
			this.tabPageDetectChangesByJournal.Controls.Add(this.buttonFindJournal);
			this.tabPageDetectChangesByJournal.Controls.Add(this.buttonParseJournal);
			this.tabPageDetectChangesByJournal.Controls.Add(this.textBoxJournal);
			this.tabPageDetectChangesByJournal.Location = new System.Drawing.Point(4, 22);
			this.tabPageDetectChangesByJournal.Name = "tabPageDetectChangesByJournal";
			this.tabPageDetectChangesByJournal.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDetectChangesByJournal.Size = new System.Drawing.Size(1035, 40);
			this.tabPageDetectChangesByJournal.TabIndex = 0;
			this.tabPageDetectChangesByJournal.Text = "By journal file (fast)";
			this.tabPageDetectChangesByJournal.UseVisualStyleBackColor = true;
			// 
			// buttonFindJournal
			// 
			this.buttonFindJournal.Location = new System.Drawing.Point(6, 6);
			this.buttonFindJournal.Name = "buttonFindJournal";
			this.buttonFindJournal.Size = new System.Drawing.Size(96, 23);
			this.buttonFindJournal.TabIndex = 13;
			this.buttonFindJournal.Text = "Find journal =>";
			this.toolTip.SetToolTip(this.buttonFindJournal, "Find journal file in \'theirs\' VSS database");
			this.buttonFindJournal.UseVisualStyleBackColor = true;
			this.buttonFindJournal.Click += new System.EventHandler(this.buttonFindJournal_Click);
			// 
			// buttonParseJournal
			// 
			this.buttonParseJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonParseJournal.Location = new System.Drawing.Point(915, 6);
			this.buttonParseJournal.Name = "buttonParseJournal";
			this.buttonParseJournal.Size = new System.Drawing.Size(114, 23);
			this.buttonParseJournal.TabIndex = 15;
			this.buttonParseJournal.Text = "Parse journal(s) ...";
			this.toolTip.SetToolTip(this.buttonParseJournal, "Parse journal files and produce serialized differences definitions");
			this.buttonParseJournal.UseVisualStyleBackColor = true;
			this.buttonParseJournal.Click += new System.EventHandler(this.buttonParseJournal_Click);
			// 
			// textBoxJournal
			// 
			this.textBoxJournal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxJournal.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "VssJournalFiles", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxJournal.Location = new System.Drawing.Point(108, 8);
			this.textBoxJournal.Name = "textBoxJournal";
			this.textBoxJournal.Size = new System.Drawing.Size(801, 20);
			this.textBoxJournal.TabIndex = 14;
			this.textBoxJournal.Text = global::Vss3WayMerge.Properties.Settings.Default.VssJournalFiles;
			this.toolTip.SetToolTip(this.textBoxJournal, "One or mode journal files for parse.\r\n\r\nSeparator ;");
			// 
			// tabPageDetectChangesByDatabase
			// 
			this.tabPageDetectChangesByDatabase.Controls.Add(this.linkLabelScanRules);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.dateTimePickerBaseTime);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.label3);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.dateTimePickerBaseDate);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.textBoxScanProject);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.label2);
			this.tabPageDetectChangesByDatabase.Controls.Add(this.buttonLoadVSSDB);
			this.tabPageDetectChangesByDatabase.Location = new System.Drawing.Point(4, 22);
			this.tabPageDetectChangesByDatabase.Name = "tabPageDetectChangesByDatabase";
			this.tabPageDetectChangesByDatabase.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDetectChangesByDatabase.Size = new System.Drawing.Size(1035, 40);
			this.tabPageDetectChangesByDatabase.TabIndex = 1;
			this.tabPageDetectChangesByDatabase.Text = "By VSS database (slow, precise)";
			this.tabPageDetectChangesByDatabase.UseVisualStyleBackColor = true;
			// 
			// linkLabelScanRules
			// 
			this.linkLabelScanRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelScanRules.AutoSize = true;
			this.linkLabelScanRules.Location = new System.Drawing.Point(513, 11);
			this.linkLabelScanRules.Name = "linkLabelScanRules";
			this.linkLabelScanRules.Size = new System.Drawing.Size(84, 13);
			this.linkLabelScanRules.TabIndex = 6;
			this.linkLabelScanRules.TabStop = true;
			this.linkLabelScanRules.Text = "Exclude Rules...";
			this.linkLabelScanRules.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelScanRules_LinkClicked);
			// 
			// dateTimePickerBaseTime
			// 
			this.dateTimePickerBaseTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePickerBaseTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePickerBaseTime.Location = new System.Drawing.Point(835, 8);
			this.dateTimePickerBaseTime.Name = "dateTimePickerBaseTime";
			this.dateTimePickerBaseTime.Size = new System.Drawing.Size(83, 20);
			this.dateTimePickerBaseTime.TabIndex = 5;
			this.dateTimePickerBaseTime.Value = new System.DateTime(2014, 9, 9, 0, 0, 0, 0);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(616, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Base point:";
			// 
			// dateTimePickerBaseDate
			// 
			this.dateTimePickerBaseDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimePickerBaseDate.Location = new System.Drawing.Point(682, 8);
			this.dateTimePickerBaseDate.Name = "dateTimePickerBaseDate";
			this.dateTimePickerBaseDate.Size = new System.Drawing.Size(147, 20);
			this.dateTimePickerBaseDate.TabIndex = 3;
			this.dateTimePickerBaseDate.Value = new System.DateTime(2013, 9, 1, 0, 0, 0, 0);
			// 
			// textBoxScanProject
			// 
			this.textBoxScanProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxScanProject.Location = new System.Drawing.Point(79, 8);
			this.textBoxScanProject.Name = "textBoxScanProject";
			this.textBoxScanProject.Size = new System.Drawing.Size(428, 20);
			this.textBoxScanProject.TabIndex = 2;
			this.textBoxScanProject.Text = "$/Common/Converters2";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "VSS Project:";
			// 
			// buttonLoadVSSDB
			// 
			this.buttonLoadVSSDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLoadVSSDB.Location = new System.Drawing.Point(924, 6);
			this.buttonLoadVSSDB.Name = "buttonLoadVSSDB";
			this.buttonLoadVSSDB.Size = new System.Drawing.Size(105, 23);
			this.buttonLoadVSSDB.TabIndex = 0;
			this.buttonLoadVSSDB.Text = "Load Baselines";
			this.toolTip.SetToolTip(this.buttonLoadVSSDB, "Determine chnaged files and base version based on DateTime.\r\nDelete \'.rev-cache\' " +
        "for drop cache");
			this.buttonLoadVSSDB.UseVisualStyleBackColor = true;
			this.buttonLoadVSSDB.Click += new System.EventHandler(this.buttonLoadVSSDB_Click);
			// 
			// radioButtonVssConnected
			// 
			this.radioButtonVssConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButtonVssConnected.AutoSize = true;
			this.radioButtonVssConnected.Checked = true;
			this.radioButtonVssConnected.ForeColor = System.Drawing.Color.Red;
			this.radioButtonVssConnected.Location = new System.Drawing.Point(110, 40);
			this.radioButtonVssConnected.Name = "radioButtonVssConnected";
			this.radioButtonVssConnected.Size = new System.Drawing.Size(170, 17);
			this.radioButtonVssConnected.TabIndex = 10;
			this.radioButtonVssConnected.TabStop = true;
			this.radioButtonVssConnected.Text = "VSS Connect 2 mine (Warn !!!)";
			this.toolTip.SetToolTip(this.radioButtonVssConnected, resources.GetString("radioButtonVssConnected.ToolTip"));
			this.radioButtonVssConnected.UseVisualStyleBackColor = true;
			// 
			// radioButtonDetached
			// 
			this.radioButtonDetached.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.radioButtonDetached.AutoSize = true;
			this.radioButtonDetached.Location = new System.Drawing.Point(286, 40);
			this.radioButtonDetached.Name = "radioButtonDetached";
			this.radioButtonDetached.Size = new System.Drawing.Size(72, 17);
			this.radioButtonDetached.TabIndex = 11;
			this.radioButtonDetached.Text = "Detached";
			this.toolTip.SetToolTip(this.radioButtonDetached, "Detached mode.\r\n\r\nMerged files will be stored on disk with specified root.\r\n\r\nAft" +
        "er utility work this files can be loaded to VSS");
			this.radioButtonDetached.UseVisualStyleBackColor = true;
			// 
			// buttonParseForMergeList
			// 
			this.buttonParseForMergeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonParseForMergeList.Location = new System.Drawing.Point(935, 499);
			this.buttonParseForMergeList.Name = "buttonParseForMergeList";
			this.buttonParseForMergeList.Size = new System.Drawing.Size(114, 23);
			this.buttonParseForMergeList.TabIndex = 17;
			this.buttonParseForMergeList.Text = "Parse list ...";
			this.toolTip.SetToolTip(this.buttonParseForMergeList, "Parse serialized differences definitions and load them to \'Differences\' tab for f" +
        "uture work");
			this.buttonParseForMergeList.UseVisualStyleBackColor = true;
			this.buttonParseForMergeList.Click += new System.EventHandler(this.buttonParseForMergeList_Click);
			// 
			// textBoxDetachedMergeDestination
			// 
			this.textBoxDetachedMergeDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDetachedMergeDestination.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "DetachedMergeDestination", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxDetachedMergeDestination.Location = new System.Drawing.Point(364, 39);
			this.textBoxDetachedMergeDestination.Name = "textBoxDetachedMergeDestination";
			this.textBoxDetachedMergeDestination.Size = new System.Drawing.Size(191, 20);
			this.textBoxDetachedMergeDestination.TabIndex = 12;
			this.textBoxDetachedMergeDestination.Text = global::Vss3WayMerge.Properties.Settings.Default.DetachedMergeDestination;
			this.toolTip.SetToolTip(this.textBoxDetachedMergeDestination, "Merged results root. Directory will be created.\r\n\r\nExisting directory will not be" +
        " cleaned up.");
			// 
			// textBoxTheirsPwd
			// 
			this.textBoxTheirsPwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "vssTheirsPassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxTheirsPwd.Location = new System.Drawing.Point(555, 6);
			this.textBoxTheirsPwd.Name = "textBoxTheirsPwd";
			this.textBoxTheirsPwd.PasswordChar = '*';
			this.textBoxTheirsPwd.Size = new System.Drawing.Size(75, 20);
			this.textBoxTheirsPwd.TabIndex = 4;
			this.textBoxTheirsPwd.Text = global::Vss3WayMerge.Properties.Settings.Default.vssTheirsPassword;
			// 
			// textBoxForMergeUnparsedList
			// 
			this.textBoxForMergeUnparsedList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxForMergeUnparsedList.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "LastParseableText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxForMergeUnparsedList.Location = new System.Drawing.Point(6, 180);
			this.textBoxForMergeUnparsedList.MaxLength = 1000000000;
			this.textBoxForMergeUnparsedList.Multiline = true;
			this.textBoxForMergeUnparsedList.Name = "textBoxForMergeUnparsedList";
			this.textBoxForMergeUnparsedList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxForMergeUnparsedList.Size = new System.Drawing.Size(923, 342);
			this.textBoxForMergeUnparsedList.TabIndex = 16;
			this.textBoxForMergeUnparsedList.Text = global::Vss3WayMerge.Properties.Settings.Default.LastParseableText;
			this.textBoxForMergeUnparsedList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxForMergeUnparsedList_KeyDown);
			// 
			// textBoxTheirsUser
			// 
			this.textBoxTheirsUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "vssTheirsUser", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxTheirsUser.Location = new System.Drawing.Point(473, 6);
			this.textBoxTheirsUser.Name = "textBoxTheirsUser";
			this.textBoxTheirsUser.Size = new System.Drawing.Size(76, 20);
			this.textBoxTheirsUser.TabIndex = 3;
			this.textBoxTheirsUser.Text = global::Vss3WayMerge.Properties.Settings.Default.vssTheirsUser;
			// 
			// textBoxVssIniTheirs
			// 
			this.textBoxVssIniTheirs.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "VssTheirs", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxVssIniTheirs.Location = new System.Drawing.Point(92, 6);
			this.textBoxVssIniTheirs.Name = "textBoxVssIniTheirs";
			this.textBoxVssIniTheirs.Size = new System.Drawing.Size(257, 20);
			this.textBoxVssIniTheirs.TabIndex = 1;
			this.textBoxVssIniTheirs.Text = global::Vss3WayMerge.Properties.Settings.Default.VssTheirs;
			this.toolTip.SetToolTip(this.textBoxVssIniTheirs, "VSS database path.\r\n\r\nThis VSS contains changes which will be applied.");
			// 
			// textBoxMinePwd
			// 
			this.textBoxMinePwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMinePwd.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "vssMinePassword", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxMinePwd.Location = new System.Drawing.Point(567, 6);
			this.textBoxMinePwd.Name = "textBoxMinePwd";
			this.textBoxMinePwd.PasswordChar = '*';
			this.textBoxMinePwd.Size = new System.Drawing.Size(99, 20);
			this.textBoxMinePwd.TabIndex = 8;
			this.textBoxMinePwd.Text = global::Vss3WayMerge.Properties.Settings.Default.vssMinePassword;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(10, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Merge destination:";
			// 
			// textBoxMineUser
			// 
			this.textBoxMineUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMineUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "vssMineUser", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxMineUser.Location = new System.Drawing.Point(458, 6);
			this.textBoxMineUser.Name = "textBoxMineUser";
			this.textBoxMineUser.Size = new System.Drawing.Size(103, 20);
			this.textBoxMineUser.TabIndex = 7;
			this.textBoxMineUser.Text = global::Vss3WayMerge.Properties.Settings.Default.vssMineUser;
			// 
			// textBoxVssIniMine
			// 
			this.textBoxVssIniMine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxVssIniMine.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vss3WayMerge.Properties.Settings.Default, "VssMine", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxVssIniMine.Location = new System.Drawing.Point(87, 6);
			this.textBoxVssIniMine.Name = "textBoxVssIniMine";
			this.textBoxVssIniMine.Size = new System.Drawing.Size(250, 20);
			this.textBoxVssIniMine.TabIndex = 6;
			this.textBoxVssIniMine.Text = global::Vss3WayMerge.Properties.Settings.Default.VssMine;
			this.toolTip.SetToolTip(this.textBoxVssIniMine, "VSS database path.\r\n\r\nThis VSS will be modified for accept changes from \'theirs\'." +
        "");
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(380, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "User + Password";
			// 
			// tabPageDiffs
			// 
			this.tabPageDiffs.Controls.Add(this.listViewChanged);
			this.tabPageDiffs.Location = new System.Drawing.Point(4, 22);
			this.tabPageDiffs.Name = "tabPageDiffs";
			this.tabPageDiffs.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDiffs.Size = new System.Drawing.Size(1055, 530);
			this.tabPageDiffs.TabIndex = 1;
			this.tabPageDiffs.Text = "Differences";
			this.tabPageDiffs.UseVisualStyleBackColor = true;
			// 
			// listViewChanged
			// 
			this.listViewChanged.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDiffSpec,
            this.colDiffStats,
            this.colDiffMergeStatus,
            this.colError,
            this.colMergeDestination});
			this.listViewChanged.ContextMenuStrip = this.contextMenuStripDiffs;
			this.listViewChanged.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewChanged.FullRowSelect = true;
			this.listViewChanged.GridLines = true;
			this.listViewChanged.Location = new System.Drawing.Point(3, 3);
			this.listViewChanged.Name = "listViewChanged";
			this.listViewChanged.Size = new System.Drawing.Size(1049, 524);
			this.listViewChanged.TabIndex = 1;
			this.listViewChanged.UseCompatibleStateImageBehavior = false;
			this.listViewChanged.View = System.Windows.Forms.View.Details;
			this.listViewChanged.VirtualMode = true;
			this.listViewChanged.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewChanged_ColumnClick);
			this.listViewChanged.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewChanged_RetrieveVirtualItem);
			this.listViewChanged.DoubleClick += new System.EventHandler(this.listViewChanged_DoubleClick);
			this.listViewChanged.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewChanged_KeyDown);
			// 
			// colDiffSpec
			// 
			this.colDiffSpec.Text = "File Spec";
			this.colDiffSpec.Width = 246;
			// 
			// colDiffStats
			// 
			this.colDiffStats.Text = "Diff";
			this.colDiffStats.Width = 113;
			// 
			// colDiffMergeStatus
			// 
			this.colDiffMergeStatus.Text = "Merge status";
			this.colDiffMergeStatus.Width = 84;
			// 
			// colError
			// 
			this.colError.Text = "Error";
			this.colError.Width = 229;
			// 
			// colMergeDestination
			// 
			this.colMergeDestination.Text = "Merge destination";
			this.colMergeDestination.Width = 294;
			// 
			// contextMenuStripDiffs
			// 
			this.contextMenuStripDiffs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeNonInteractiveToolStripMenuItem,
            this.threeWayMergeToolStripMenuItem,
            this.threeWayDiffToolStripMenuItem,
            this.toolStripMenuItemOtherDiffs,
            this.toolStripSeparator3,
            this.checkMergedToolStripMenuItem,
            this.toolStripSeparator4,
            this.resolveAsMineToolStripMenuItem,
            this.resolveAsTheirsToolStripMenuItem,
            this.revertresetOrRemoveToolStripMenuItem,
            this.toolStripSeparator5,
            this.excludeFromListToolStripMenuItem,
            this.toolStripSeparator6,
            this.filterToolStripMenuItem,
            this.moreToolStripMenuItem});
			this.contextMenuStripDiffs.Name = "contextMenuStripDiffs";
			this.contextMenuStripDiffs.Size = new System.Drawing.Size(259, 270);
			this.contextMenuStripDiffs.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripDiffs_Opening);
			// 
			// mergeNonInteractiveToolStripMenuItem
			// 
			this.mergeNonInteractiveToolStripMenuItem.Name = "mergeNonInteractiveToolStripMenuItem";
			this.mergeNonInteractiveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.mergeNonInteractiveToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.mergeNonInteractiveToolStripMenuItem.Text = "Merge non-interactive";
			this.mergeNonInteractiveToolStripMenuItem.Click += new System.EventHandler(this.mergeNoninteractiveToolStripMenuItem_Click);
			// 
			// threeWayMergeToolStripMenuItem
			// 
			this.threeWayMergeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.threeWayMergeToolStripMenuItem.Name = "threeWayMergeToolStripMenuItem";
			this.threeWayMergeToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.threeWayMergeToolStripMenuItem.Text = "3-Way Merge...";
			this.threeWayMergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
			// 
			// threeWayDiffToolStripMenuItem
			// 
			this.threeWayDiffToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.threeWayDiffToolStripMenuItem.Name = "threeWayDiffToolStripMenuItem";
			this.threeWayDiffToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.threeWayDiffToolStripMenuItem.Text = "3-Way Diff...";
			this.threeWayDiffToolStripMenuItem.Click += new System.EventHandler(this.threeWayDiffToolStripMenuItem_Click);
			// 
			// toolStripMenuItemOtherDiffs
			// 
			this.toolStripMenuItemOtherDiffs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mineNewToolStripMenuItem,
            this.theirsNewToolStripMenuItem,
            this.theirsChnagesAsUnifiedDiffToolStripMenuItem,
            this.toolStripSeparator1,
            this.baseDiffToolStripMenuItem,
            this.headsDiffToolStripMenuItem});
			this.toolStripMenuItemOtherDiffs.Name = "toolStripMenuItemOtherDiffs";
			this.toolStripMenuItemOtherDiffs.Size = new System.Drawing.Size(258, 22);
			this.toolStripMenuItemOtherDiffs.Text = "Other diffs";
			// 
			// mineNewToolStripMenuItem
			// 
			this.mineNewToolStripMenuItem.Name = "mineNewToolStripMenuItem";
			this.mineNewToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.mineNewToolStripMenuItem.Text = "Mine changes ...";
			this.mineNewToolStripMenuItem.Click += new System.EventHandler(this.mineNewToolStripMenuItem_Click);
			// 
			// theirsNewToolStripMenuItem
			// 
			this.theirsNewToolStripMenuItem.Name = "theirsNewToolStripMenuItem";
			this.theirsNewToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.theirsNewToolStripMenuItem.Text = "Theirs changes ...";
			this.theirsNewToolStripMenuItem.Click += new System.EventHandler(this.theirsNewToolStripMenuItem_Click);
			// 
			// theirsChnagesAsUnifiedDiffToolStripMenuItem
			// 
			this.theirsChnagesAsUnifiedDiffToolStripMenuItem.Name = "theirsChnagesAsUnifiedDiffToolStripMenuItem";
			this.theirsChnagesAsUnifiedDiffToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.theirsChnagesAsUnifiedDiffToolStripMenuItem.Text = "Theirs chnages as unified diff ...";
			this.theirsChnagesAsUnifiedDiffToolStripMenuItem.Click += new System.EventHandler(this.theirsChnagesAsUnifiedDiffToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
			// 
			// baseDiffToolStripMenuItem
			// 
			this.baseDiffToolStripMenuItem.Name = "baseDiffToolStripMenuItem";
			this.baseDiffToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.baseDiffToolStripMenuItem.Text = "Bases diff ...";
			this.baseDiffToolStripMenuItem.Click += new System.EventHandler(this.basesDiffToolStripMenuItem_Click);
			// 
			// headsDiffToolStripMenuItem
			// 
			this.headsDiffToolStripMenuItem.Name = "headsDiffToolStripMenuItem";
			this.headsDiffToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.headsDiffToolStripMenuItem.Text = "Heads diff ...";
			this.headsDiffToolStripMenuItem.Click += new System.EventHandler(this.headsDiffToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(255, 6);
			// 
			// checkMergedToolStripMenuItem
			// 
			this.checkMergedToolStripMenuItem.Name = "checkMergedToolStripMenuItem";
			this.checkMergedToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.checkMergedToolStripMenuItem.Text = "Merged diff (intend for check in) ...";
			this.checkMergedToolStripMenuItem.Click += new System.EventHandler(this.mergedDiffToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(255, 6);
			// 
			// resolveAsMineToolStripMenuItem
			// 
			this.resolveAsMineToolStripMenuItem.Name = "resolveAsMineToolStripMenuItem";
			this.resolveAsMineToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
			this.resolveAsMineToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.resolveAsMineToolStripMenuItem.Text = "Resolve as mine";
			this.resolveAsMineToolStripMenuItem.Click += new System.EventHandler(this.resolveAsMineToolStripMenuItem_Click);
			// 
			// resolveAsTheirsToolStripMenuItem
			// 
			this.resolveAsTheirsToolStripMenuItem.Name = "resolveAsTheirsToolStripMenuItem";
			this.resolveAsTheirsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
			this.resolveAsTheirsToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.resolveAsTheirsToolStripMenuItem.Text = "Resolve as theirs";
			this.resolveAsTheirsToolStripMenuItem.Click += new System.EventHandler(this.resolveAsTheirsToolStripMenuItem_Click);
			// 
			// revertresetOrRemoveToolStripMenuItem
			// 
			this.revertresetOrRemoveToolStripMenuItem.Name = "revertresetOrRemoveToolStripMenuItem";
			this.revertresetOrRemoveToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.revertresetOrRemoveToolStripMenuItem.Text = "Reset merged";
			this.revertresetOrRemoveToolStripMenuItem.Click += new System.EventHandler(this.revertResetOrRemoveToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(255, 6);
			// 
			// excludeFromListToolStripMenuItem
			// 
			this.excludeFromListToolStripMenuItem.Name = "excludeFromListToolStripMenuItem";
			this.excludeFromListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.excludeFromListToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.excludeFromListToolStripMenuItem.Text = "Remove from list";
			this.excludeFromListToolStripMenuItem.Click += new System.EventHandler(this.excludeFromListToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(255, 6);
			// 
			// filterToolStripMenuItem
			// 
			this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.toolStripSeparator7,
            this.toolStripMenuItem1,
            this.toolStripTextBoxPathsFilter,
            this.toolStripSeparator2,
            this.onlySelectedToolStripMenuItem,
            this.filterexludeSelectionToolStripMenuItem});
			this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
			this.filterToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.filterToolStripMenuItem.Text = "Filter";
			this.filterToolStripMenuItem.DropDownOpening += new System.EventHandler(this.filterToolStripMenuItem_DropDownOpening);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(257, 6);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
			this.toolStripMenuItem1.Text = "Files filter:";
			// 
			// toolStripTextBoxPathsFilter
			// 
			this.toolStripTextBoxPathsFilter.ForeColor = System.Drawing.Color.Silver;
			this.toolStripTextBoxPathsFilter.Name = "toolStripTextBoxPathsFilter";
			this.toolStripTextBoxPathsFilter.Size = new System.Drawing.Size(200, 23);
			this.toolStripTextBoxPathsFilter.Text = "file path regex filter";
			this.toolStripTextBoxPathsFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBoxPathsFilter_KeyUp);
			this.toolStripTextBoxPathsFilter.Click += new System.EventHandler(this.toolStripTextBoxPathsFilter_Click);
			this.toolStripTextBoxPathsFilter.TextChanged += new System.EventHandler(this.toolStripTextBoxPathsFilter_TextChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(257, 6);
			// 
			// onlySelectedToolStripMenuItem
			// 
			this.onlySelectedToolStripMenuItem.Name = "onlySelectedToolStripMenuItem";
			this.onlySelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
			this.onlySelectedToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.onlySelectedToolStripMenuItem.Text = "Filter to selection";
			this.onlySelectedToolStripMenuItem.Click += new System.EventHandler(this.onlySelectedToolStripMenuItem_Click);
			// 
			// filterexludeSelectionToolStripMenuItem
			// 
			this.filterexludeSelectionToolStripMenuItem.Name = "filterexludeSelectionToolStripMenuItem";
			this.filterexludeSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
			this.filterexludeSelectionToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
			this.filterexludeSelectionToolStripMenuItem.Text = "Exclude selection";
			this.filterexludeSelectionToolStripMenuItem.Click += new System.EventHandler(this.filterToSelectionInversionToolStripMenuItem_Click);
			// 
			// moreToolStripMenuItem
			// 
			this.moreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statsToolStripMenuItem1,
            this.toolStripMenuItemCopyParseableToClip});
			this.moreToolStripMenuItem.Name = "moreToolStripMenuItem";
			this.moreToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
			this.moreToolStripMenuItem.Text = "More";
			// 
			// statsToolStripMenuItem1
			// 
			this.statsToolStripMenuItem1.Name = "statsToolStripMenuItem1";
			this.statsToolStripMenuItem1.Size = new System.Drawing.Size(377, 22);
			this.statsToolStripMenuItem1.Text = "Stats ...";
			this.statsToolStripMenuItem1.Click += new System.EventHandler(this.statsToolStripMenuItem_Click);
			// 
			// toolStripMenuItemCopyParseableToClip
			// 
			this.toolStripMenuItemCopyParseableToClip.Name = "toolStripMenuItemCopyParseableToClip";
			this.toolStripMenuItemCopyParseableToClip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.toolStripMenuItemCopyParseableToClip.Size = new System.Drawing.Size(377, 22);
			this.toolStripMenuItemCopyParseableToClip.Text = "Copy selected changes definitions to Clipboard ...";
			this.toolStripMenuItemCopyParseableToClip.Click += new System.EventHandler(this.copySelectedItemsOriginals_Click);
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.textBoxLog);
			this.tabPageLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(1055, 530);
			this.tabPageLog.TabIndex = 0;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// textBoxLog
			// 
			this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxLog.Location = new System.Drawing.Point(3, 3);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxLog.Size = new System.Drawing.Size(1049, 524);
			this.textBoxLog.TabIndex = 0;
			// 
			// tabPageOprions
			// 
			this.tabPageOprions.Controls.Add(this.label11);
			this.tabPageOprions.Controls.Add(this.textBox4);
			this.tabPageOprions.Controls.Add(this.textBoxCustomMerge);
			this.tabPageOprions.Controls.Add(this.textBoxCustomDiff);
			this.tabPageOprions.Controls.Add(this.textBoxCustomMergeExe);
			this.tabPageOprions.Controls.Add(this.label10);
			this.tabPageOprions.Controls.Add(this.label9);
			this.tabPageOprions.Controls.Add(this.label8);
			this.tabPageOprions.Controls.Add(this.comboBoxMerger);
			this.tabPageOprions.Controls.Add(this.label7);
			this.tabPageOprions.Location = new System.Drawing.Point(4, 22);
			this.tabPageOprions.Name = "tabPageOprions";
			this.tabPageOprions.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageOprions.Size = new System.Drawing.Size(1055, 530);
			this.tabPageOprions.TabIndex = 3;
			this.tabPageOprions.Text = "Options";
			this.tabPageOprions.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.ForeColor = System.Drawing.Color.Red;
			this.label11.Location = new System.Drawing.Point(616, 66);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(180, 13);
			this.label11.TabIndex = 5;
			this.label11.Text = "Save settings not implemented";
			// 
			// textBox4
			// 
			this.textBox4.Enabled = false;
			this.textBox4.Location = new System.Drawing.Point(98, 123);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(346, 136);
			this.textBox4.TabIndex = 4;
			this.textBox4.Text = "Use next placeholders:\r\n$LEFT$ -  for left source or theirs\r\n$RIGHT$ - for right " +
    "source or mine\r\n$BASE$ - for base version\r\n$MERGED$ - for merged destination";
			// 
			// textBoxCustomMerge
			// 
			this.textBoxCustomMerge.Location = new System.Drawing.Point(99, 97);
			this.textBoxCustomMerge.Name = "textBoxCustomMerge";
			this.textBoxCustomMerge.Size = new System.Drawing.Size(345, 20);
			this.textBoxCustomMerge.TabIndex = 3;
			this.textBoxCustomMerge.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCustomMergeExe_KeyUp);
			// 
			// textBoxCustomDiff
			// 
			this.textBoxCustomDiff.Location = new System.Drawing.Point(99, 71);
			this.textBoxCustomDiff.Name = "textBoxCustomDiff";
			this.textBoxCustomDiff.Size = new System.Drawing.Size(345, 20);
			this.textBoxCustomDiff.TabIndex = 3;
			this.textBoxCustomDiff.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCustomMergeExe_KeyUp);
			// 
			// textBoxCustomMergeExe
			// 
			this.textBoxCustomMergeExe.Location = new System.Drawing.Point(99, 45);
			this.textBoxCustomMergeExe.Name = "textBoxCustomMergeExe";
			this.textBoxCustomMergeExe.Size = new System.Drawing.Size(345, 20);
			this.textBoxCustomMergeExe.TabIndex = 3;
			this.textBoxCustomMergeExe.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCustomMergeExe_KeyUp);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 100);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(87, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "Merge Template:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 74);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(73, 13);
			this.label9.TabIndex = 2;
			this.label9.Text = "Diff Template:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 48);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(63, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Executable:";
			// 
			// comboBoxMerger
			// 
			this.comboBoxMerger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMerger.FormattingEnabled = true;
			this.comboBoxMerger.Location = new System.Drawing.Point(76, 6);
			this.comboBoxMerger.Name = "comboBoxMerger";
			this.comboBoxMerger.Size = new System.Drawing.Size(257, 21);
			this.comboBoxMerger.TabIndex = 1;
			this.comboBoxMerger.SelectedIndexChanged += new System.EventHandler(this.comboBoxMerger_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 9);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Merge Tool:";
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 100;
			this.toolTip.AutoPopDelay = 10000;
			this.toolTip.InitialDelay = 100;
			this.toolTip.ReshowDelay = 20;
			// 
			// tabControlSourceDestination
			// 
			this.tabControlSourceDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlSourceDestination.Controls.Add(this.tabPageMergeSource);
			this.tabControlSourceDestination.Controls.Add(this.tabPageMergeDestination);
			this.tabControlSourceDestination.Location = new System.Drawing.Point(4, 6);
			this.tabControlSourceDestination.Name = "tabControlSourceDestination";
			this.tabControlSourceDestination.SelectedIndex = 0;
			this.tabControlSourceDestination.Size = new System.Drawing.Size(1048, 96);
			this.tabControlSourceDestination.TabIndex = 20;
			// 
			// tabPageMergeSource
			// 
			this.tabPageMergeSource.Controls.Add(this.textBoxVssIniTheirs);
			this.tabPageMergeSource.Controls.Add(this.label5);
			this.tabPageMergeSource.Controls.Add(this.label4);
			this.tabPageMergeSource.Controls.Add(this.textBoxTheirsUser);
			this.tabPageMergeSource.Controls.Add(this.textBoxTheirsPwd);
			this.tabPageMergeSource.Location = new System.Drawing.Point(4, 22);
			this.tabPageMergeSource.Name = "tabPageMergeSource";
			this.tabPageMergeSource.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMergeSource.Size = new System.Drawing.Size(1040, 105);
			this.tabPageMergeSource.TabIndex = 0;
			this.tabPageMergeSource.Text = "Merge from (theirs)";
			this.tabPageMergeSource.UseVisualStyleBackColor = true;
			// 
			// tabPageMergeDestination
			// 
			this.tabPageMergeDestination.Controls.Add(this.label1);
			this.tabPageMergeDestination.Controls.Add(this.label12);
			this.tabPageMergeDestination.Controls.Add(this.textBoxVssIniMine);
			this.tabPageMergeDestination.Controls.Add(this.textBoxMineUser);
			this.tabPageMergeDestination.Controls.Add(this.radioButtonVssConnected);
			this.tabPageMergeDestination.Controls.Add(this.label6);
			this.tabPageMergeDestination.Controls.Add(this.radioButtonDetached);
			this.tabPageMergeDestination.Controls.Add(this.textBoxMinePwd);
			this.tabPageMergeDestination.Controls.Add(this.textBoxDetachedMergeDestination);
			this.tabPageMergeDestination.Location = new System.Drawing.Point(4, 22);
			this.tabPageMergeDestination.Name = "tabPageMergeDestination";
			this.tabPageMergeDestination.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMergeDestination.Size = new System.Drawing.Size(1040, 70);
			this.tabPageMergeDestination.TabIndex = 1;
			this.tabPageMergeDestination.Text = "Merge to (mine)";
			this.tabPageMergeDestination.UseVisualStyleBackColor = true;
			// 
			// Vss3WayMerge
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1063, 556);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Vss3WayMerge";
			this.Text = "VSS 3-Way Merge";
			this.Load += new System.EventHandler(this.Vss3WayMerge_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageForMergeFiles.ResumeLayout(false);
			this.tabPageForMergeFiles.PerformLayout();
			this.tabControlDetectChanges.ResumeLayout(false);
			this.tabPageDetectChangesByJournal.ResumeLayout(false);
			this.tabPageDetectChangesByJournal.PerformLayout();
			this.tabPageDetectChangesByDatabase.ResumeLayout(false);
			this.tabPageDetectChangesByDatabase.PerformLayout();
			this.tabPageDiffs.ResumeLayout(false);
			this.contextMenuStripDiffs.ResumeLayout(false);
			this.tabPageLog.ResumeLayout(false);
			this.tabPageLog.PerformLayout();
			this.tabPageOprions.ResumeLayout(false);
			this.tabPageOprions.PerformLayout();
			this.tabControlSourceDestination.ResumeLayout(false);
			this.tabPageMergeSource.ResumeLayout(false);
			this.tabPageMergeSource.PerformLayout();
			this.tabPageMergeDestination.ResumeLayout(false);
			this.tabPageMergeDestination.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBoxVssIniMine;
		private System.Windows.Forms.TextBox textBoxVssIniTheirs;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.TabPage tabPageDiffs;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.ListView listViewChanged;
		private System.Windows.Forms.ColumnHeader colDiffSpec;
		private System.Windows.Forms.ColumnHeader colDiffMergeStatus;
		private System.Windows.Forms.ColumnHeader colDiffStats;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDiffs;
		private System.Windows.Forms.ToolStripMenuItem threeWayMergeToolStripMenuItem;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxMineUser;
		private System.Windows.Forms.TextBox textBoxMinePwd;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxTheirsUser;
		private System.Windows.Forms.TextBox textBoxTheirsPwd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ColumnHeader colError;
		private System.Windows.Forms.TabPage tabPageOprions;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboBoxMerger;
		private System.Windows.Forms.TextBox textBoxCustomMerge;
		private System.Windows.Forms.TextBox textBoxCustomDiff;
		private System.Windows.Forms.TextBox textBoxCustomMergeExe;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem resolveAsMineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resolveAsTheirsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem threeWayDiffToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mergeNonInteractiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkMergedToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageForMergeFiles;
		private System.Windows.Forms.Button buttonParseJournal;
		private System.Windows.Forms.TextBox textBoxForMergeUnparsedList;
		private System.Windows.Forms.Button buttonParseForMergeList;
		private System.Windows.Forms.TextBox textBoxDetachedMergeDestination;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem excludeFromListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem revertresetOrRemoveToolStripMenuItem;
		private System.Windows.Forms.TextBox textBoxJournal;
		private System.Windows.Forms.Button buttonFindJournal;
		private System.Windows.Forms.ColumnHeader colMergeDestination;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyParseableToClip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOtherDiffs;
		private System.Windows.Forms.ToolStripMenuItem mineNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem theirsNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem theirsChnagesAsUnifiedDiffToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem baseDiffToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem headsDiffToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBoxPathsFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem onlySelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterexludeSelectionToolStripMenuItem;
		private System.Windows.Forms.RadioButton radioButtonDetached;
		private System.Windows.Forms.RadioButton radioButtonVssConnected;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.TabControl tabControlDetectChanges;
		private System.Windows.Forms.TabPage tabPageDetectChangesByJournal;
		private System.Windows.Forms.TabPage tabPageDetectChangesByDatabase;
		private System.Windows.Forms.Button buttonLoadVSSDB;
		private System.Windows.Forms.DateTimePicker dateTimePickerBaseDate;
		private System.Windows.Forms.TextBox textBoxScanProject;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker dateTimePickerBaseTime;
		private System.Windows.Forms.LinkLabel linkLabelScanRules;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabControl tabControlSourceDestination;
		private System.Windows.Forms.TabPage tabPageMergeSource;
		private System.Windows.Forms.TabPage tabPageMergeDestination;
	}
}

