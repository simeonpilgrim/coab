using NUnit.Framework;
using System.IO;
using GoldBox.Classes;
using GoldBox.Classes.DaxFiles;
using GoldBox.Logging;

namespace GoldBox.Engine
{
    [TestFixture]
    public class TitleScreenTests
    {
        private int _counter;
        [SetUp]
        public void Setup()
        {
            RecreateDisplayDirectory();
        }

        [Test]
        public void DisplayTitleScreen()
        {
            Logger.Setup("");

            gbl.symbol_8x8_set = new DaxBlock[5];
            gbl.dax_8x8d1_201 = new byte[177, 8];
            seg041.Load8x8Tiles();

            Display.UpdateCallback = DisplayCallback;

            var titleScreen = new TitleScreen {KeyboardInput = new FakeKeyboardInput()};

            titleScreen.DisplayTitleScreen();
        }

        private void DisplayCallback()
        {
            Display.bm.Save($@"Display\Screen{_counter++}.png");
        }

        private static void RecreateDisplayDirectory()
        {
            if (Directory.Exists("Display"))
                Directory.Delete("Display", true);
            Directory.CreateDirectory("Display");
        }

        private class FakeKeyboardInput : IKeyboardInput
        {
            public void PressAnyKey(int secondsTimeout){}
        }
    }
}
