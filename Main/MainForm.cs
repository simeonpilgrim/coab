using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Main.Properties;
using GoldBox.Engine;
using GoldBox.Classes;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            setSettings();

            InitializeComponent();

            Display.UpdateCallback = UpdateDisplayCallback;
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
                displayArea.Image = (Image)Display.bm.Clone();
            }
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Display.ForceUpdate();
            }

            seg049.AddKey(Keyboard.KeyToIBMKey(e.KeyCode));
        }

        private void commandDebugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            seg043.ToggleCommandDebugging();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            seg043.print_and_exit();
        }

        private void dumpPlayerAffectsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            seg043.DumpPlayerAffects();
        }

        private void commandDebuggingToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            seg043.ToggleCommandDebugging();
        }

        string Picture_Prefix = "Curse - ";

        private void screenCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            int largest = 0;
            foreach (string filename in Directory.GetFiles(path, Picture_Prefix + "*.png", SearchOption.TopDirectoryOnly))
            {
                int num;
                string substr = Path.GetFileNameWithoutExtension(filename).Substring(Picture_Prefix.Length);
                if (Int32.TryParse(substr, out num))
                {
                    largest = Math.Max(num, largest);
                }
            }
            largest++;

            string newfilepath = Path.Combine(path, Picture_Prefix + largest.ToString("D4") + ".png");
            displayArea.Image.Save(newfilepath, System.Drawing.Imaging.ImageFormat.Png);
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
            Settings.Default.Upgrade();
            Settings.Default.Save();

            Cheats.PlayerAlwaysSavesSet(Settings.Default.PlayerAlwaysSaves);
            Cheats.AlwayShowAreaMapSet(Settings.Default.AlwayShowAreaMap);
            Cheats.FreeTrainingSet(Settings.Default.FreeTraining);
            Cheats.SkipCopyProtectionSet(Settings.Default.SkipCopyProtection);
            Cheats.AllowGodsInterveneSet(Settings.Default.AllowGodsIntervene);
            Cheats.DisplayFullItemNamesSet(Settings.Default.DisplayFullItemNames);
            Cheats.ViewItemStatsSet(Settings.Default.ViewItemsStats);
            Cheats.SkipTitleScreenSet(Settings.Default.SkipTitleScreen);
            Cheats.ImprovedAreaMapSet(Settings.Default.ImprovedAreaMap);
            Cheats.NoRaceLevelLimits(Settings.Default.NoRaceClassLimits);
            Cheats.NoRaceClassRestrictions(Settings.Default.NoRaceClassLimits);
            Cheats.SortTreasureSet(Settings.Default.SortTreasure);

            seg044.SetSound(Settings.Default.SoundOn);
            seg044.SetPicture(Settings.Default.PictureOn);
            seg044.SetAnimation(Settings.Default.AnimationOn);
        }

        private void playersAlwayMakeSavingThrowToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.PlayerAlwaysSaves;
            Settings.Default.PlayerAlwaysSaves = flipped;
            Settings.Default.Save();

            Cheats.PlayerAlwaysSavesSet(flipped);
        }

        private void alwayAllowAreaMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AlwayShowAreaMap = !Settings.Default.AlwayShowAreaMap;
            Settings.Default.Save();

            Cheats.AlwayShowAreaMapSet(Settings.Default.AlwayShowAreaMap);
        }

        private void freeTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.FreeTraining;
            Settings.Default.FreeTraining = flipped;
            Settings.Default.Save();

            Cheats.FreeTrainingSet(flipped);
        }

        private void skipCopyProtectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SkipCopyProtection;
            Settings.Default.SkipCopyProtection = flipped;
            Settings.Default.Save();

            Cheats.SkipCopyProtectionSet(flipped);
        }

        private void skipTitleScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SkipTitleScreen;
            Settings.Default.SkipTitleScreen = flipped;
            Settings.Default.Save();

            Cheats.SkipTitleScreenSet(flipped);
        }

        private void allowGodsInterveneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.AllowGodsIntervene;
            Settings.Default.AllowGodsIntervene = flipped;
            Settings.Default.Save();

            Cheats.AllowGodsInterveneSet(flipped);
        }

        private void displayItemsFullNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.DisplayFullItemNames;
            Settings.Default.DisplayFullItemNames = flipped;
            Settings.Default.Save();

            Cheats.DisplayFullItemNamesSet(flipped);
        }

        private void viewItemsStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.ViewItemsStats;
            Settings.Default.ViewItemsStats = flipped;
            Settings.Default.Save();

            Cheats.ViewItemStatsSet(flipped);
        }

        private void improvedAreaMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.ImprovedAreaMap;
            Settings.Default.ImprovedAreaMap = flipped;
            Settings.Default.Save();

            Cheats.ImprovedAreaMapSet(flipped);
        }

        private void noRaceLevelLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.NoRaceLevelLimits;
            Settings.Default.NoRaceLevelLimits = flipped;
            Settings.Default.Save();

            Cheats.NoRaceLevelLimits(flipped);
        }

        private void noRaceClassLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.NoRaceClassLimits;
            Settings.Default.NoRaceClassLimits = flipped;
            Settings.Default.Save();

            Cheats.NoRaceClassRestrictions(flipped);
        }

        private void dumpMonstersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seg043.DumpMonsters();
        }

        private void dumpTreasureItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            seg043.DumpTreasureItems();
        }

        private void soundOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SoundOn;
            Settings.Default.SoundOn = flipped;
            Settings.Default.Save();

            seg044.SetSound(flipped);
        }

        private void PictureOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.PictureOn;
            Settings.Default.PictureOn = flipped;
            if (flipped == false)
            {
                Settings.Default.AnimationOn = false;
                seg044.SetAnimation(false);
            }
            Settings.Default.Save();

            seg044.SetPicture(flipped);

        }

        private void AnimationOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.AnimationOn;
            Settings.Default.AnimationOn = flipped;
            Settings.Default.Save();

            seg044.SetAnimation(flipped);
        }

        private void sortTreasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flipped = !Settings.Default.SortTreasure;
            Settings.Default.SortTreasure = flipped;
            Settings.Default.Save();

            Cheats.SortTreasureSet(flipped);
        }

    }
}