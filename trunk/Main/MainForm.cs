using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Main.Properties;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Classes.Display.LowLevelDisplay = new Win32.Win32Display();
            Classes.Display.UpdateCallback = UpdateDisplayCallback;
            setSettings();
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
                displayArea.Image = (Image)Win32.Win32Display.bm.Clone();
            }
        }      

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            engine.seg049.AddKey( Keyboard.KeyToIBMKey(e.KeyCode) );
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

        private void setSettings()
        {
            Classes.Cheats.PlayerAlwaysSavesSet(Settings.Default.PlayerAlwaysSaves);
            Classes.Cheats.AlwayShowAreaMapSet(Settings.Default.AlwayShowAreaMap);
            Classes.Cheats.FreeTrainingSet(Settings.Default.FreeTraining);
            Classes.Cheats.SkipCopyProtectionSet(Settings.Default.SkipCopyProtection);
            Classes.Cheats.AllowGodsInterveneSet(Settings.Default.AllowGodsIntervene);
            Classes.Cheats.DisplayFullItemNamesSet(Settings.Default.DisplayFullItemNames);
            Classes.Cheats.ViewItemStatsSet(Settings.Default.ViewItemsStats);
            Classes.Cheats.SkipTitleScreenSet(Settings.Default.SkipTitleScreen);
            Classes.Cheats.ImprovedAreaMapSet(Settings.Default.ImprovedAreaMap);
            Classes.Cheats.NoRaceClassLimits(Settings.Default.NoRaceClassLimits);

            engine.seg044.SetSound(Settings.Default.SoundOn);
        }

        private void playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.PlayerAlwaysSaves;
            Settings.Default.PlayerAlwaysSaves = flipped;
            Settings.Default.Save();

            Classes.Cheats.PlayerAlwaysSavesSet(flipped);
        }

        private void alwayAllowAreaMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AlwayShowAreaMap = !Settings.Default.AlwayShowAreaMap;
            Settings.Default.Save();

            Classes.Cheats.AlwayShowAreaMapSet(Settings.Default.AlwayShowAreaMap);
        }

        private void freeTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.FreeTraining;
            Settings.Default.FreeTraining = flipped;
            Settings.Default.Save();

            Classes.Cheats.FreeTrainingSet(flipped);
        }

        private void skipCopyProtectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SkipCopyProtection;
            Settings.Default.SkipCopyProtection = flipped;
            Settings.Default.Save();

            Classes.Cheats.SkipCopyProtectionSet(flipped);
        }

        private void skipTitleScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SkipTitleScreen;
            Settings.Default.SkipTitleScreen = flipped;
            Settings.Default.Save();

            Classes.Cheats.SkipTitleScreenSet(flipped);
        }

        private void allowGodsInterveneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.AllowGodsIntervene;
            Settings.Default.AllowGodsIntervene = flipped;
            Settings.Default.Save();

            Classes.Cheats.AllowGodsInterveneSet(flipped);
        }

        private void displayItemsFullNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.DisplayFullItemNames;
            Settings.Default.DisplayFullItemNames = flipped;
            Settings.Default.Save();

            Classes.Cheats.DisplayFullItemNamesSet(flipped);
        }

        private void viewItemsStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.ViewItemsStats;
            Settings.Default.ViewItemsStats = flipped;
            Settings.Default.Save();

            Classes.Cheats.ViewItemStatsSet(flipped);
        }

        private void improvedAreaMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.ImprovedAreaMap;
            Settings.Default.ImprovedAreaMap = flipped;
            Settings.Default.Save();

            Classes.Cheats.ImprovedAreaMapSet(flipped);
        }

        private void noRaceClassLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.NoRaceClassLimits;
            Settings.Default.NoRaceClassLimits = flipped;
            Settings.Default.Save();

            Classes.Cheats.NoRaceClassLimits(flipped);
        }

        private void dumpMonstersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.seg043.DumpMonsters();
        }

        private void dumpTreasureItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.seg043.DumpTreasureItems();
        }

        private void soundOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SoundOn;
            Settings.Default.SoundOn = flipped;
            Settings.Default.Save();

            engine.seg044.SetSound(flipped);
        }
    }
}