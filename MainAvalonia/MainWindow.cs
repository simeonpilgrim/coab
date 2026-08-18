using System;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
// aliased because the Window base class exposes a "Classes" property that
// shadows the game's Classes namespace
using GameDisplay = Classes.Display;
using GameCheats = Classes.Cheats;

namespace MainAvalonia
{
    public class MainWindow : Window
    {
        readonly WriteableBitmap bitmap;
        readonly Image displayArea;
        readonly byte[] frameBuffer = new byte[GameDisplay.FrameSize];
        readonly byte[] pendingFrame = new byte[GameDisplay.FrameSize];
        readonly object frameLock = new object();
        readonly UserSettings settings;

        Thread engineThread;
        bool engineStarted;
        bool renderQueued;

        public MainWindow()
        {
            settings = UserSettings.Load();
            settings.Apply();

            engine.seg044.SetSoundPlayer(new ProcessSoundPlayer());

            Title = "Curse Of The Azure Bonds";
            Width = GameDisplay.Width * 2;
            Height = GameDisplay.Height * 2;

            bitmap = new WriteableBitmap(
                new PixelSize(GameDisplay.Width, GameDisplay.Height),
                new Vector(96, 96),
                PixelFormat.Bgra8888,
                AlphaFormat.Opaque);

            displayArea = new Image
            {
                Source = bitmap,
                Stretch = Stretch.Fill,
            };
            RenderOptions.SetBitmapInterpolationMode(displayArea, BitmapInterpolationMode.None);

            Content = displayArea;
            ContextMenu = BuildContextMenu();

            GameDisplay.UpdateCallback = UpdateDisplayCallback;

            KeyDown += OnKeyDown;
            Opened += OnOpened;
            Closing += (sender, args) => engine.seg043.print_and_exit();
        }

        void OnOpened(object sender, EventArgs args)
        {
            if (engineStarted)
            {
                return;
            }
            engineStarted = true;

            engineThread = new Thread(EngineThread);
            engineThread.Name = "Engine";
            engineThread.IsBackground = true;
            engineThread.Start();
        }

        void EngineThread()
        {
            try
            {
                engine.seg001.__SystemInit(EngineStopped);
                engine.seg001.PROGRAM();
            }
            catch (engine.EngineStopException)
            {
                // EngineStop already ran the stopped callback
                return;
            }

            EngineStopped();
        }

        void EngineStopped()
        {
            Dispatcher.UIThread.Post(Close);
        }

        void OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.F5)
            {
                GameDisplay.ForceUpdate();
                args.Handled = true;
                return;
            }

            ushort key = KeyboardMap.KeyToIBMKey(args.Key);
            if (key != 0)
            {
                engine.seg049.AddKey(key);
                args.Handled = true;
            }
        }

        void UpdateDisplayCallback()
        {
            bool shouldPost;
            lock (frameLock)
            {
                GameDisplay.CopyFrameTo(pendingFrame);
                shouldPost = renderQueued == false;
                renderQueued = true;
            }

            if (shouldPost)
            {
                Dispatcher.UIThread.Post(RenderFrame);
            }
        }

        void RenderFrame()
        {
            lock (frameLock)
            {
                Array.Copy(pendingFrame, frameBuffer, frameBuffer.Length);
                renderQueued = false;
            }

            using (var fb = bitmap.Lock())
            {
                int width = GameDisplay.Width;
                int height = GameDisplay.Height;
                int srcStride = GameDisplay.Stride;

                byte[] row = new byte[width * 4];
                for (int y = 0; y < height; y++)
                {
                    int src = y * srcStride;
                    for (int x = 0; x < width; x++)
                    {
                        row[(x * 4) + 0] = frameBuffer[src + (x * 3) + 0];
                        row[(x * 4) + 1] = frameBuffer[src + (x * 3) + 1];
                        row[(x * 4) + 2] = frameBuffer[src + (x * 3) + 2];
                        row[(x * 4) + 3] = 0xFF;
                    }

                    System.Runtime.InteropServices.Marshal.Copy(
                        row, 0, fb.Address + (y * fb.RowBytes), row.Length);
                }
            }

            displayArea.InvalidateVisual();
        }

        ContextMenu BuildContextMenu()
        {
            var debugMenu = new MenuItem { Header = "Debugging" };
            debugMenu.Items.Add(ToggleItem("Command Debugging",
                () => false,
                value => engine.seg043.ToggleCommandDebugging()));
            debugMenu.Items.Add(ActionItem("Dump Player Affects", engine.seg043.DumpPlayerAffects));
            debugMenu.Items.Add(ActionItem("Dump Monsters", engine.seg043.DumpMonsters));
            debugMenu.Items.Add(ActionItem("Dump Treasure Items", engine.seg043.DumpTreasureItems));

            var cheatsMenu = new MenuItem { Header = "Cheats" };
            cheatsMenu.Items.Add(ToggleItem("Allow Area Map",
                () => settings.AlwayShowAreaMap,
                value => { settings.AlwayShowAreaMap = value; GameCheats.AlwayShowAreaMapSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Allow Gods Intervene",
                () => settings.AllowGodsIntervene,
                value => { settings.AllowGodsIntervene = value; GameCheats.AllowGodsInterveneSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Display Item's Full Name",
                () => settings.DisplayFullItemNames,
                value => { settings.DisplayFullItemNames = value; GameCheats.DisplayFullItemNamesSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Free Training",
                () => settings.FreeTraining,
                value => { settings.FreeTraining = value; GameCheats.FreeTrainingSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Improved Area Map",
                () => settings.ImprovedAreaMap,
                value => { settings.ImprovedAreaMap = value; GameCheats.ImprovedAreaMapSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Players Alway Make Saving Throw",
                () => settings.PlayerAlwaysSaves,
                value => { settings.PlayerAlwaysSaves = value; GameCheats.PlayerAlwaysSavesSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Skip Copy Protection",
                () => settings.SkipCopyProtection,
                value => { settings.SkipCopyProtection = value; GameCheats.SkipCopyProtectionSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("Skip Title Screen",
                () => settings.SkipTitleScreen,
                value => { settings.SkipTitleScreen = value; GameCheats.SkipTitleScreenSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("View Items Stats",
                () => settings.ViewItemsStats,
                value => { settings.ViewItemsStats = value; GameCheats.ViewItemStatsSet(value); }));
            cheatsMenu.Items.Add(ToggleItem("No Race Class Restrictions",
                () => settings.NoRaceClassLimits,
                value => { settings.NoRaceClassLimits = value; GameCheats.NoRaceClassRestrictions(value); }));
            cheatsMenu.Items.Add(ToggleItem("No Race Level Limits",
                () => settings.NoRaceLevelLimits,
                value => { settings.NoRaceLevelLimits = value; GameCheats.NoRaceLevelLimits(value); }));
            cheatsMenu.Items.Add(ToggleItem("Sort Treasure",
                () => settings.SortTreasure,
                value => { settings.SortTreasure = value; GameCheats.SortTreasureSet(value); }));

            var optionsMenu = new MenuItem { Header = "Options" };
            optionsMenu.Items.Add(ToggleItem("Sound On",
                () => settings.SoundOn,
                value => { settings.SoundOn = value; engine.seg044.SetSound(value); }));
            optionsMenu.Items.Add(ToggleItem("Animations On",
                () => settings.AnimationOn,
                value => { settings.AnimationOn = value; engine.seg044.SetAnimation(value); }));
            optionsMenu.Items.Add(ToggleItem("Pictures On",
                () => settings.PictureOn,
                value =>
                {
                    settings.PictureOn = value;
                    if (value == false)
                    {
                        settings.AnimationOn = false;
                        engine.seg044.SetAnimation(false);
                    }
                    engine.seg044.SetPicture(value);
                }));

            var menu = new ContextMenu();
            menu.Items.Add(debugMenu);
            menu.Items.Add(ActionItem("Screen Capture", ScreenCapture));
            menu.Items.Add(cheatsMenu);
            menu.Items.Add(optionsMenu);

            return menu;
        }

        static MenuItem ActionItem(string header, Action action)
        {
            var item = new MenuItem { Header = header };
            item.Click += (sender, args) => action();
            return item;
        }

        MenuItem ToggleItem(string header, Func<bool> get, Action<bool> set)
        {
            var item = new MenuItem
            {
                Header = header,
                ToggleType = MenuItemToggleType.CheckBox,
                IsChecked = get(),
            };
            item.Click += (sender, args) =>
            {
                bool flipped = !get();
                item.IsChecked = flipped;
                set(flipped);
                settings.Save();
            };
            return item;
        }

        const string Picture_Prefix = "Curse - ";

        void ScreenCapture()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (string.IsNullOrEmpty(path) || Directory.Exists(path) == false)
            {
                path = Logging.Config.GetBasePath();
            }

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
            bitmap.Save(newfilepath);
        }
    }
}
