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
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 92);
            // 
            // ddfsdToolStripMenuItem
            // 
            this.ddfsdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandDebuggingToolStripMenuItem,
            this.dumpPlayerAffectsToolStripMenuItem1,
            this.dumpMonstersToolStripMenuItem,
            this.dumpTreasureItemsToolStripMenuItem});
            this.ddfsdToolStripMenuItem.Name = "ddfsdToolStripMenuItem";
            this.ddfsdToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ddfsdToolStripMenuItem.Text = "Debugging";
            // 
            // commandDebuggingToolStripMenuItem
            // 
            this.commandDebuggingToolStripMenuItem.CheckOnClick = true;
            this.commandDebuggingToolStripMenuItem.Name = "commandDebuggingToolStripMenuItem";
            this.commandDebuggingToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.commandDebuggingToolStripMenuItem.Text = "Command Debugging";
            this.commandDebuggingToolStripMenuItem.CheckedChanged += new System.EventHandler(this.commandDebuggingToolStripMenuItem_CheckedChanged);
            // 
            // dumpPlayerAffectsToolStripMenuItem1
            // 
            this.dumpPlayerAffectsToolStripMenuItem1.Name = "dumpPlayerAffectsToolStripMenuItem1";
            this.dumpPlayerAffectsToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.dumpPlayerAffectsToolStripMenuItem1.Text = "Dump Player Affects";
            this.dumpPlayerAffectsToolStripMenuItem1.Click += new System.EventHandler(this.dumpPlayerAffectsToolStripMenuItem1_Click);
            // 
            // dumpMonstersToolStripMenuItem
            // 
            this.dumpMonstersToolStripMenuItem.Name = "dumpMonstersToolStripMenuItem";
            this.dumpMonstersToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dumpMonstersToolStripMenuItem.Text = "Dump Monsters";
            this.dumpMonstersToolStripMenuItem.Click += new System.EventHandler(this.dumpMonstersToolStripMenuItem_Click);
            // 
            // dumpTreasureItemsToolStripMenuItem
            // 
            this.dumpTreasureItemsToolStripMenuItem.Name = "dumpTreasureItemsToolStripMenuItem";
            this.dumpTreasureItemsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.dumpTreasureItemsToolStripMenuItem.Text = "Dump Treasure Items";
            this.dumpTreasureItemsToolStripMenuItem.Click += new System.EventHandler(this.dumpTreasureItemsToolStripMenuItem_Click);
            // 
            // screenCaptureToolStripMenuItem
            // 
            this.screenCaptureToolStripMenuItem.Name = "screenCaptureToolStripMenuItem";
            this.screenCaptureToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
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
            this.noRaceClassLimitsToolStripMenuItem});
            this.cheatsToolStripMenuItem.Name = "cheatsToolStripMenuItem";
            this.cheatsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cheatsToolStripMenuItem.Text = "Cheats";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundOnToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // soundOnToolStripMenuItem
            // 
            this.soundOnToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SoundOn;
            this.soundOnToolStripMenuItem.CheckOnClick = true;
            this.soundOnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soundOnToolStripMenuItem.Name = "soundOnToolStripMenuItem";
            this.soundOnToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.soundOnToolStripMenuItem.Text = "Sound On";
            this.soundOnToolStripMenuItem.Click += new System.EventHandler(this.soundOnToolStripMenuItem_Click);
            // 
            // alwayAllowAreaMapToolStripMenuItem
            // 
            this.alwayAllowAreaMapToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.AlwayShowAreaMap;
            this.alwayAllowAreaMapToolStripMenuItem.CheckOnClick = true;
            this.alwayAllowAreaMapToolStripMenuItem.Name = "alwayAllowAreaMapToolStripMenuItem";
            this.alwayAllowAreaMapToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.alwayAllowAreaMapToolStripMenuItem.Text = "Allow Area Map";
            this.alwayAllowAreaMapToolStripMenuItem.Click += new System.EventHandler(this.alwayAllowAreaMapToolStripMenuItem_Click);
            // 
            // allowGodsInterveneToolStripMenuItem
            // 
            this.allowGodsInterveneToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.AllowGodsIntervene;
            this.allowGodsInterveneToolStripMenuItem.CheckOnClick = true;
            this.allowGodsInterveneToolStripMenuItem.Name = "allowGodsInterveneToolStripMenuItem";
            this.allowGodsInterveneToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.allowGodsInterveneToolStripMenuItem.Text = "Allow Gods Intervene";
            this.allowGodsInterveneToolStripMenuItem.Click += new System.EventHandler(this.allowGodsInterveneToolStripMenuItem_Click);
            // 
            // displayItemsFullNameToolStripMenuItem
            // 
            this.displayItemsFullNameToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.DisplayFullItemNames;
            this.displayItemsFullNameToolStripMenuItem.CheckOnClick = true;
            this.displayItemsFullNameToolStripMenuItem.Name = "displayItemsFullNameToolStripMenuItem";
            this.displayItemsFullNameToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.displayItemsFullNameToolStripMenuItem.Text = "Display Item\'s Full Name";
            this.displayItemsFullNameToolStripMenuItem.Click += new System.EventHandler(this.displayItemsFullNameToolStripMenuItem_Click);
            // 
            // freeTrainingToolStripMenuItem
            // 
            this.freeTrainingToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.FreeTraining;
            this.freeTrainingToolStripMenuItem.CheckOnClick = true;
            this.freeTrainingToolStripMenuItem.Name = "freeTrainingToolStripMenuItem";
            this.freeTrainingToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.freeTrainingToolStripMenuItem.Text = "Free Training";
            this.freeTrainingToolStripMenuItem.Click += new System.EventHandler(this.freeTrainingToolStripMenuItem_Click);
            // 
            // improvedAreaMapToolStripMenuItem
            // 
            this.improvedAreaMapToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.ImprovedAreaMap;
            this.improvedAreaMapToolStripMenuItem.CheckOnClick = true;
            this.improvedAreaMapToolStripMenuItem.Name = "improvedAreaMapToolStripMenuItem";
            this.improvedAreaMapToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.improvedAreaMapToolStripMenuItem.Text = "Improved Area Map";
            this.improvedAreaMapToolStripMenuItem.Click += new System.EventHandler(this.improvedAreaMapToolStripMenuItem_Click);
            // 
            // playersAlwayMakeSavingThrowToolStripMenuItem
            // 
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.PlayerAlwaysSaves;
            this.playersAlwayMakeSavingThrowToolStripMenuItem.CheckOnClick = true;
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Name = "playersAlwayMakeSavingThrowToolStripMenuItem";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Text = "Players Alway Make Saving Throw";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Click += new System.EventHandler(this.playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged);
            // 
            // skipCopyProtectionToolStripMenuItem
            // 
            this.skipCopyProtectionToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SkipCopyProtection;
            this.skipCopyProtectionToolStripMenuItem.CheckOnClick = true;
            this.skipCopyProtectionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skipCopyProtectionToolStripMenuItem.Name = "skipCopyProtectionToolStripMenuItem";
            this.skipCopyProtectionToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.skipCopyProtectionToolStripMenuItem.Text = "Skip Copy Protection";
            this.skipCopyProtectionToolStripMenuItem.Click += new System.EventHandler(this.skipCopyProtectionToolStripMenuItem_Click);
            // 
            // skipTitleScreenToolStripMenuItem
            // 
            this.skipTitleScreenToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.SkipTitleScreen;
            this.skipTitleScreenToolStripMenuItem.CheckOnClick = true;
            this.skipTitleScreenToolStripMenuItem.Name = "skipTitleScreenToolStripMenuItem";
            this.skipTitleScreenToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.skipTitleScreenToolStripMenuItem.Text = "Skip Title Screen";
            this.skipTitleScreenToolStripMenuItem.Click += new System.EventHandler(this.skipTitleScreenToolStripMenuItem_Click);
            // 
            // viewItemsStatsToolStripMenuItem
            // 
            this.viewItemsStatsToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.ViewItemsStats;
            this.viewItemsStatsToolStripMenuItem.CheckOnClick = true;
            this.viewItemsStatsToolStripMenuItem.Name = "viewItemsStatsToolStripMenuItem";
            this.viewItemsStatsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.viewItemsStatsToolStripMenuItem.Text = "View Items Stats";
            this.viewItemsStatsToolStripMenuItem.Click += new System.EventHandler(this.viewItemsStatsToolStripMenuItem_Click);
            // 
            // noRaceClassLimitsToolStripMenuItem
            // 
            this.noRaceClassLimitsToolStripMenuItem.Checked = global::Main.Properties.Settings.Default.NoRaceClassLimits;
            this.noRaceClassLimitsToolStripMenuItem.CheckOnClick = true;
            this.noRaceClassLimitsToolStripMenuItem.Name = "noRaceClassLimitsToolStripMenuItem";
            this.noRaceClassLimitsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.noRaceClassLimitsToolStripMenuItem.Text = "No Race Class Limits";
            this.noRaceClassLimitsToolStripMenuItem.Click += new System.EventHandler(this.noRaceClassLimitsToolStripMenuItem_Click);
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
    }
}

