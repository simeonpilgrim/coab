namespace Main
{
    partial class MainForm
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
            this.displayArea = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ddfsdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandDebuggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpPlayerAffectsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpMonstersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpTreasureItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwayAllowAreaMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowGodsInterveneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayItemsFullNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freeTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.improvedAreaMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersAlwayMakeSavingThrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipCopyProtectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipTitleScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewItemsStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noRaceClassLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noRaceLevelLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortTreasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationsOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picturesOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.displayArea)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayArea
            // 
            this.displayArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayArea.Location = new System.Drawing.Point(0, 0);
            this.displayArea.Name = "displayArea";
            this.displayArea.Size = new System.Drawing.Size(640, 400);
            this.displayArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.displayArea.TabIndex = 0;
            this.displayArea.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddfsdToolStripMenuItem,
            this.screenCaptureToolStripMenuItem,
            this.cheatsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 114);
            // 
            // ddfsdToolStripMenuItem
            // 
            this.ddfsdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandDebuggingToolStripMenuItem,
            this.dumpPlayerAffectsToolStripMenuItem1,
            this.dumpMonstersToolStripMenuItem,
            this.dumpTreasureItemsToolStripMenuItem});
            this.ddfsdToolStripMenuItem.Name = "ddfsdToolStripMenuItem";
            this.ddfsdToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.ddfsdToolStripMenuItem.Text = "Debugging";
            // 
            // commandDebuggingToolStripMenuItem
            // 
            this.commandDebuggingToolStripMenuItem.CheckOnClick = true;
            this.commandDebuggingToolStripMenuItem.Name = "commandDebuggingToolStripMenuItem";
            this.commandDebuggingToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.commandDebuggingToolStripMenuItem.Text = "Command Debugging";
            this.commandDebuggingToolStripMenuItem.CheckedChanged += new System.EventHandler(this.commandDebuggingToolStripMenuItem_CheckedChanged);
            // 
            // dumpPlayerAffectsToolStripMenuItem1
            // 
            this.dumpPlayerAffectsToolStripMenuItem1.Name = "dumpPlayerAffectsToolStripMenuItem1";
            this.dumpPlayerAffectsToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.dumpPlayerAffectsToolStripMenuItem1.Text = "Dump Player Affects";
            this.dumpPlayerAffectsToolStripMenuItem1.Click += new System.EventHandler(this.dumpPlayerAffectsToolStripMenuItem1_Click);
            // 
            // dumpMonstersToolStripMenuItem
            // 
            this.dumpMonstersToolStripMenuItem.Name = "dumpMonstersToolStripMenuItem";
            this.dumpMonstersToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.dumpMonstersToolStripMenuItem.Text = "Dump Monsters";
            this.dumpMonstersToolStripMenuItem.Click += new System.EventHandler(this.dumpMonstersToolStripMenuItem_Click);
            // 
            // dumpTreasureItemsToolStripMenuItem
            // 
            this.dumpTreasureItemsToolStripMenuItem.Name = "dumpTreasureItemsToolStripMenuItem";
            this.dumpTreasureItemsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.dumpTreasureItemsToolStripMenuItem.Text = "Dump Treasure Items";
            this.dumpTreasureItemsToolStripMenuItem.Click += new System.EventHandler(this.dumpTreasureItemsToolStripMenuItem_Click);
            // 
            // screenCaptureToolStripMenuItem
            // 
            this.screenCaptureToolStripMenuItem.Name = "screenCaptureToolStripMenuItem";
            this.screenCaptureToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.screenCaptureToolStripMenuItem.Text = "Screen Capture";
            this.screenCaptureToolStripMenuItem.Click += new System.EventHandler(this.screenCaptureToolStripMenuItem_Click);
            // 
            // cheatsToolStripMenuItem
            // 
            this.cheatsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwayAllowAreaMapToolStripMenuItem,
            this.allowGodsInterveneToolStripMenuItem,
            this.displayItemsFullNameToolStripMenuItem,
            this.freeTrainingToolStripMenuItem,
            this.improvedAreaMapToolStripMenuItem,
            this.playersAlwayMakeSavingThrowToolStripMenuItem,
            this.skipCopyProtectionToolStripMenuItem,
            this.skipTitleScreenToolStripMenuItem,
            this.viewItemsStatsToolStripMenuItem,
            this.noRaceClassLimitsToolStripMenuItem,
            this.noRaceLevelLimitsToolStripMenuItem,
            this.sortTreasureToolStripMenuItem});
            this.cheatsToolStripMenuItem.Name = "cheatsToolStripMenuItem";
            this.cheatsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.cheatsToolStripMenuItem.Text = "Cheats";
            // 
            // alwayAllowAreaMapToolStripMenuItem
            // 
            this.alwayAllowAreaMapToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.AlwayShowAreaMap;
            this.alwayAllowAreaMapToolStripMenuItem.CheckOnClick = true;
            this.alwayAllowAreaMapToolStripMenuItem.Name = "alwayAllowAreaMapToolStripMenuItem";
            this.alwayAllowAreaMapToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.alwayAllowAreaMapToolStripMenuItem.Text = "Allow Area Map";
            this.alwayAllowAreaMapToolStripMenuItem.Click += new System.EventHandler(this.alwayAllowAreaMapToolStripMenuItem_Click);
            // 
            // allowGodsInterveneToolStripMenuItem
            // 
            this.allowGodsInterveneToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.AllowGodsIntervene;
            this.allowGodsInterveneToolStripMenuItem.CheckOnClick = true;
            this.allowGodsInterveneToolStripMenuItem.Name = "allowGodsInterveneToolStripMenuItem";
            this.allowGodsInterveneToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.allowGodsInterveneToolStripMenuItem.Text = "Allow Gods Intervene";
            this.allowGodsInterveneToolStripMenuItem.Click += new System.EventHandler(this.allowGodsInterveneToolStripMenuItem_Click);
            // 
            // displayItemsFullNameToolStripMenuItem
            // 
            this.displayItemsFullNameToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.DisplayFullItemNames;
            this.displayItemsFullNameToolStripMenuItem.CheckOnClick = true;
            this.displayItemsFullNameToolStripMenuItem.Name = "displayItemsFullNameToolStripMenuItem";
            this.displayItemsFullNameToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.displayItemsFullNameToolStripMenuItem.Text = "Display Item\'s Full Name";
            this.displayItemsFullNameToolStripMenuItem.Click += new System.EventHandler(this.displayItemsFullNameToolStripMenuItem_Click);
            // 
            // freeTrainingToolStripMenuItem
            // 
            this.freeTrainingToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.FreeTraining;
            this.freeTrainingToolStripMenuItem.CheckOnClick = true;
            this.freeTrainingToolStripMenuItem.Name = "freeTrainingToolStripMenuItem";
            this.freeTrainingToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.freeTrainingToolStripMenuItem.Text = "Free Training";
            this.freeTrainingToolStripMenuItem.Click += new System.EventHandler(this.freeTrainingToolStripMenuItem_Click);
            // 
            // improvedAreaMapToolStripMenuItem
            // 
            this.improvedAreaMapToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.ImprovedAreaMap;
            this.improvedAreaMapToolStripMenuItem.CheckOnClick = true;
            this.improvedAreaMapToolStripMenuItem.Name = "improvedAreaMapToolStripMenuItem";
            this.improvedAreaMapToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.improvedAreaMapToolStripMenuItem.Text = "Improved Area Map";
            this.improvedAreaMapToolStripMenuItem.Click += new System.EventHandler(this.improvedAreaMapToolStripMenuItem_Click);
            // 
            // playersAlwayMakeSavingThrowToolStripMenuItem
            // 
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.PlayerAlwaysSaves;
            this.playersAlwayMakeSavingThrowToolStripMenuItem.CheckOnClick = true;
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Name = "playersAlwayMakeSavingThrowToolStripMenuItem";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Text = "Players Alway Make Saving Throw";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Click += new System.EventHandler(this.playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged);
            // 
            // skipCopyProtectionToolStripMenuItem
            // 
            this.skipCopyProtectionToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SkipCopyProtection;
            this.skipCopyProtectionToolStripMenuItem.CheckOnClick = true;
            this.skipCopyProtectionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skipCopyProtectionToolStripMenuItem.Name = "skipCopyProtectionToolStripMenuItem";
            this.skipCopyProtectionToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.skipCopyProtectionToolStripMenuItem.Text = "Skip Copy Protection";
            this.skipCopyProtectionToolStripMenuItem.Click += new System.EventHandler(this.skipCopyProtectionToolStripMenuItem_Click);
            // 
            // skipTitleScreenToolStripMenuItem
            // 
            this.skipTitleScreenToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SkipTitleScreen;
            this.skipTitleScreenToolStripMenuItem.CheckOnClick = true;
            this.skipTitleScreenToolStripMenuItem.Name = "skipTitleScreenToolStripMenuItem";
            this.skipTitleScreenToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.skipTitleScreenToolStripMenuItem.Text = "Skip Title Screen";
            this.skipTitleScreenToolStripMenuItem.Click += new System.EventHandler(this.skipTitleScreenToolStripMenuItem_Click);
            // 
            // viewItemsStatsToolStripMenuItem
            // 
            this.viewItemsStatsToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.ViewItemsStats;
            this.viewItemsStatsToolStripMenuItem.CheckOnClick = true;
            this.viewItemsStatsToolStripMenuItem.Name = "viewItemsStatsToolStripMenuItem";
            this.viewItemsStatsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.viewItemsStatsToolStripMenuItem.Text = "View Items Stats";
            this.viewItemsStatsToolStripMenuItem.Click += new System.EventHandler(this.viewItemsStatsToolStripMenuItem_Click);
            // 
            // noRaceClassLimitsToolStripMenuItem
            // 
            this.noRaceClassLimitsToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.NoRaceClassLimits;
            this.noRaceClassLimitsToolStripMenuItem.CheckOnClick = true;
            this.noRaceClassLimitsToolStripMenuItem.Name = "noRaceClassLimitsToolStripMenuItem";
            this.noRaceClassLimitsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.noRaceClassLimitsToolStripMenuItem.Text = "No Race Class Restrictions";
            this.noRaceClassLimitsToolStripMenuItem.Click += new System.EventHandler(this.noRaceClassLimitsToolStripMenuItem_Click);
            // 
            // noRaceLevelLimitsToolStripMenuItem
            // 
            this.noRaceLevelLimitsToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.NoRaceLevelLimits;
            this.noRaceLevelLimitsToolStripMenuItem.CheckOnClick = true;
            this.noRaceLevelLimitsToolStripMenuItem.Name = "noRaceLevelLimitsToolStripMenuItem";
            this.noRaceLevelLimitsToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.noRaceLevelLimitsToolStripMenuItem.Text = "No Race Level Limits";
            this.noRaceLevelLimitsToolStripMenuItem.Click += new System.EventHandler(this.noRaceLevelLimitsToolStripMenuItem_Click);
            // 
            // sortTreasureToolStripMenuItem
            // 
            this.sortTreasureToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SortTreasure;
            this.sortTreasureToolStripMenuItem.CheckOnClick = true;
            this.sortTreasureToolStripMenuItem.Name = "sortTreasureToolStripMenuItem";
            this.sortTreasureToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.sortTreasureToolStripMenuItem.Text = "Sort Treasure";
            this.sortTreasureToolStripMenuItem.Click += new System.EventHandler(this.sortTreasureToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundOnToolStripMenuItem,
            this.animationsOnToolStripMenuItem,
            this.picturesOnToolStripMenuItem,
            this.gameSpeedToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // soundOnToolStripMenuItem
            // 
            this.soundOnToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SoundOn;
            this.soundOnToolStripMenuItem.CheckOnClick = true;
            this.soundOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soundOnToolStripMenuItem.Name = "soundOnToolStripMenuItem";
            this.soundOnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.soundOnToolStripMenuItem.Text = "Sound On";
            this.soundOnToolStripMenuItem.Click += new System.EventHandler(this.soundOnToolStripMenuItem_Click);
            // 
            // animationsOnToolStripMenuItem
            // 
            this.animationsOnToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.AnimationOn;
            this.animationsOnToolStripMenuItem.CheckOnClick = true;
            this.animationsOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animationsOnToolStripMenuItem.Name = "animationsOnToolStripMenuItem";
            this.animationsOnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.animationsOnToolStripMenuItem.Text = "Animations On";
            this.animationsOnToolStripMenuItem.Click += new System.EventHandler(this.AnimationOnToolStripMenuItem_Click);
            // 
            // picturesOnToolStripMenuItem
            // 
            this.picturesOnToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.PictureOn;
            this.picturesOnToolStripMenuItem.CheckOnClick = true;
            this.picturesOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.picturesOnToolStripMenuItem.Name = "picturesOnToolStripMenuItem";
            this.picturesOnToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.picturesOnToolStripMenuItem.Text = "Pictures On";
            this.picturesOnToolStripMenuItem.Click += new System.EventHandler(this.PictureOnToolStripMenuItem_Click);
            // 
            // gameSpeedToolStripMenuItem
            // 
            this.gameSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.gameSpeedToolStripMenuItem.Name = "gameSpeedToolStripMenuItem";
            this.gameSpeedToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.gameSpeedToolStripMenuItem.Text = "Game Speed";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem3.Text = "2";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem4.Text = "3";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem5.Text = "4";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem6.Text = "5";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem7.Text = "6";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem8.Text = "7";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem9.Text = "8";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(80, 22);
            this.toolStripMenuItem10.Text = "9";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.displayArea);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Curse Of The Azure Bonds";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.displayArea)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox displayArea;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ddfsdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandDebuggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpPlayerAffectsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem screenCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cheatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playersAlwayMakeSavingThrowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freeTrainingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwayAllowAreaMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skipCopyProtectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowGodsInterveneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayItemsFullNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewItemsStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skipTitleScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem improvedAreaMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noRaceClassLimitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpMonstersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpTreasureItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noRaceLevelLimitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortTreasureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationsOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem picturesOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
    }
}

