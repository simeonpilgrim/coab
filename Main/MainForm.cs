using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Classes.Display.UpdateCallback = UpdateDisplayCallback;
        }

        object obj = new object();

        public void UpdateDisplayCallback()
        {
            if (displayArea.InvokeRequired)
            {
                displayArea.Invoke(new MethodInvoker(UpdateDisplayCallback));
            }
            else
            {
                displayArea.Image = (Image)Classes.Display.bm.Clone();
            }
        }      

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            engine.seg049.AddKey( Keyboard.KeyToIBMKey(e.KeyCode) );
        }

        private void dumpPlayerAffectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.seg043.DumpPlayerAffects();
        }

        private void commandDebugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            engine.seg043.ToggleCommandDebugging();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine.seg043.print_and_exit();
        }

        private void dumpPlayerAffectsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            engine.seg043.DumpPlayerAffects();
        }

        private void commandDebuggingToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            engine.seg043.ToggleCommandDebugging();
        }

        private void screenCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.ChangeExtension(Path.GetTempFileName(), ".jpg");
            displayArea.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (obj)
            {
                base.OnPaint(e);
            }
        }

        private void playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            engine.ovr024.TogglePlayerAlwaysSaves();
        }

        private void alwayAllowAreaMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Classes.gbl.AlwayShowAreaMapToggle();
        }

        private void freeTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Classes.gbl.FreeTrainingToggle();
        }

        private void skipCopyProtectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Classes.gbl.SkipCopyProtection();
        }
    }
}