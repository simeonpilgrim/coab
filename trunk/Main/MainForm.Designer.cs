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
            this.screenCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersAlwayMakeSavingThrowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freeTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwayAllowAreaMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipCopyProtectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cheatsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 70);
            // 
            // ddfsdToolStripMenuItem
            // 
            this.ddfsdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandDebuggingToolStripMenuItem,
            this.dumpPlayerAffectsToolStripMenuItem1});
            this.ddfsdToolStripMenuItem.Name = "ddfsdToolStripMenuItem";
            this.ddfsdToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ddfsdToolStripMenuItem.Text = "Debugging";
            // 
            // commandDebuggingToolStripMenuItem
            // 
            this.commandDebuggingToolStripMenuItem.CheckOnClick = true;
            this.commandDebuggingToolStripMenuItem.Name = "commandDebuggingToolStripMenuItem";
            this.commandDebuggingToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.commandDebuggingToolStripMenuItem.Text = "Command Debugging";
            this.commandDebuggingToolStripMenuItem.CheckedChanged += new System.EventHandler(this.commandDebuggingToolStripMenuItem_CheckedChanged);
            // 
            // dumpPlayerAffectsToolStripMenuItem1
            // 
            this.dumpPlayerAffectsToolStripMenuItem1.Name = "dumpPlayerAffectsToolStripMenuItem1";
            this.dumpPlayerAffectsToolStripMenuItem1.Size = new System.Drawing.Size(248, 22);
            this.dumpPlayerAffectsToolStripMenuItem1.Text = "Dump Player Affects";
            this.dumpPlayerAffectsToolStripMenuItem1.Click += new System.EventHandler(this.dumpPlayerAffectsToolStripMenuItem1_Click);
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
            this.playersAlwayMakeSavingThrowToolStripMenuItem,
            this.freeTrainingToolStripMenuItem,
            this.alwayAllowAreaMapToolStripMenuItem,
            this.skipCopyProtectionToolStripMenuItem});
            this.cheatsToolStripMenuItem.Name = "cheatsToolStripMenuItem";
            this.cheatsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.cheatsToolStripMenuItem.Text = "Cheats";
            // 
            // playersAlwayMakeSavingThrowToolStripMenuItem
            // 
            this.playersAlwayMakeSavingThrowToolStripMenuItem.CheckOnClick = true;
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Name = "playersAlwayMakeSavingThrowToolStripMenuItem";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Text = "Players Alway Make Saving Throw";
            this.playersAlwayMakeSavingThrowToolStripMenuItem.Click += new System.EventHandler(this.playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged);
            // 
            // freeTrainingToolStripMenuItem
            // 
            this.freeTrainingToolStripMenuItem.CheckOnClick = true;
            this.freeTrainingToolStripMenuItem.Name = "freeTrainingToolStripMenuItem";
            this.freeTrainingToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.freeTrainingToolStripMenuItem.Text = "Free Training";
            this.freeTrainingToolStripMenuItem.Click += new System.EventHandler(this.freeTrainingToolStripMenuItem_Click);
            // 
            // alwayAllowAreaMapToolStripMenuItem
            // 
            this.alwayAllowAreaMapToolStripMenuItem.CheckOnClick = true;
            this.alwayAllowAreaMapToolStripMenuItem.Name = "alwayAllowAreaMapToolStripMenuItem";
            this.alwayAllowAreaMapToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.alwayAllowAreaMapToolStripMenuItem.Text = "Alway Allow Area Map";
            this.alwayAllowAreaMapToolStripMenuItem.Click += new System.EventHandler(this.alwayAllowAreaMapToolStripMenuItem_Click);
            // 
            // skipCopyProtectionToolStripMenuItem
            // 
            this.skipCopyProtectionToolStripMenuItem.Checked = true;
            this.skipCopyProtectionToolStripMenuItem.CheckOnClick = true;
            this.skipCopyProtectionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skipCopyProtectionToolStripMenuItem.Name = "skipCopyProtectionToolStripMenuItem";
            this.skipCopyProtectionToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.skipCopyProtectionToolStripMenuItem.Text = "Skip Copy Protection";
            this.skipCopyProtectionToolStripMenuItem.Click += new System.EventHandler(this.skipCopyProtectionToolStripMenuItem_Click);
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
    }
}

