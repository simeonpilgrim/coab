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

        void UpdateDisplayCallback()
        {
            displayArea.Image = (Image)Classes.Display.bm.Clone();
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            engine.seg049.keyCode = Keyboard.KeyToIBMKey(e.KeyCode);
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
    }
}