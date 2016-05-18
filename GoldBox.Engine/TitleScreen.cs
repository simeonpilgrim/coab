using GoldBox.Classes;
using GoldBox.Classes.DaxFiles;

namespace GoldBox.Engine
{
    public class TitleScreen
    {
        public IKeyboardInput KeyboardInput { set { _keyboardInput = value; } }
        private static IKeyboardInput _keyboardInput = new KeyboardInput();
        private static readonly HardCodedText[] HardCodedTexts;

        static TitleScreen()
        {
            HardCodedTexts = new[]
            {
                new HardCodedText {X=0x02, Y=0x01, ForegroundColor = DisplayColors.Green, Value = "based on the tsr novel 'azure bonds'"},
                new HardCodedText {X=0x06, Y=0x02, ForegroundColor = DisplayColors.Green, Value = "by:"},
                new HardCodedText {X=0x09, Y=0x02, ForegroundColor = DisplayColors.LightBlue, Value = "kate novak"},
                new HardCodedText {X=0x14, Y=0x02, ForegroundColor = DisplayColors.Green, Value = "and"},
                new HardCodedText {X=0x18, Y=0x02, ForegroundColor = DisplayColors.LightBlue, Value = "jeff grubb"},
                new HardCodedText {X=0x0a, Y=0x04, ForegroundColor = DisplayColors.Green, Value = "scenario created by:"},
                new HardCodedText {X=0x0b, Y=0x05, ForegroundColor = DisplayColors.Yellow, Value = "tsr, inc."},
                new HardCodedText {X=0x15, Y=0x05, ForegroundColor = DisplayColors.Green, Value = "and"},
                new HardCodedText {X=0x19, Y=0x05, ForegroundColor = DisplayColors.Yellow, Value = "ssi"},
                new HardCodedText {X=0x0e, Y=0x06, ForegroundColor = DisplayColors.LightBlue, Value = "jeff grubb"},
                new HardCodedText {X=0x0B, Y=0x07, ForegroundColor = DisplayColors.LightBlue, Value = "george mac donald"},
                new HardCodedText {X=0x01, Y=0x09, ForegroundColor = DisplayColors.Green, Value = "game created by:"},
                new HardCodedText {X=0x12, Y=0x09, ForegroundColor = DisplayColors.Yellow, Value = "ssi special projects"},
                new HardCodedText {X=0x02, Y=0x0B, ForegroundColor = DisplayColors.Yellow, Value = "project leader:"},
                new HardCodedText {X=0x02, Y=0x0C, ForegroundColor = DisplayColors.LightBlue, Value = "george mac donald"},
                new HardCodedText {X=0x02, Y=0x0E, ForegroundColor = DisplayColors.Yellow, Value = "programming:"},
                new HardCodedText {X=0x02, Y=0x0F, ForegroundColor = DisplayColors.LightBlue, Value = "scot bayless"},
                new HardCodedText {X=0x02, Y=0x10, ForegroundColor = DisplayColors.LightBlue, Value = "russ brown"},
                new HardCodedText {X=0x02, Y=0x11, ForegroundColor = DisplayColors.LightBlue, Value = "michael mancuso"},
                new HardCodedText {X=0x02, Y=0x13, ForegroundColor = DisplayColors.Yellow, Value = "development:"},
                new HardCodedText {X=0x02, Y=0x14, ForegroundColor = DisplayColors.LightBlue, Value = "david shelley"},
                new HardCodedText {X=0x02, Y=0x15, ForegroundColor = DisplayColors.LightBlue, Value = "michael mancuso"},
                new HardCodedText {X=0x02, Y=0x16, ForegroundColor = DisplayColors.LightBlue, Value = "oran kangas"},
                new HardCodedText {X=0x16, Y=0x0B, ForegroundColor = DisplayColors.Yellow, Value = "graphic arts:"},
                new HardCodedText {X=0x16, Y=0x0C, ForegroundColor = DisplayColors.LightBlue, Value = "tom wahl"},
                new HardCodedText {X=0x16, Y=0x0D, ForegroundColor = DisplayColors.LightBlue, Value = "fred butts"},
                new HardCodedText {X=0x16, Y=0x0E, ForegroundColor = DisplayColors.LightBlue, Value = "susan manley"},
                new HardCodedText {X=0x16, Y=0x0F, ForegroundColor = DisplayColors.LightBlue, Value = "mark johnson"},
                new HardCodedText {X=0x16, Y=0x10, ForegroundColor = DisplayColors.LightBlue, Value = "cyrus lum"},
                new HardCodedText {X=0x16, Y=0x12, ForegroundColor = DisplayColors.Yellow, Value = "playtesting:"},
                new HardCodedText {X=0x16, Y=0x13, ForegroundColor = DisplayColors.LightBlue, Value = "jim jennings"},
                new HardCodedText {X=0x16, Y=0x14, ForegroundColor = DisplayColors.LightBlue, Value = "james kucera"},
                new HardCodedText {X=0x16, Y=0x15, ForegroundColor = DisplayColors.LightBlue, Value = "rick white"},
                new HardCodedText {X=0x16, Y=0x16, ForegroundColor = DisplayColors.LightBlue, Value = "robert daly"},
            };
        }
        public void DisplayTitleScreen()
        {
            DaxBlock dax_ptr;

            dax_ptr = seg040.LoadDax(0, 0, 1, "Title");
            seg040.draw_picture(dax_ptr, 0, 0, 0);
            _keyboardInput.PressAnyKey(5);

            dax_ptr = seg040.LoadDax(0, 0, 2, "Title");
            seg040.draw_picture(dax_ptr, 0, 0, 0);

            dax_ptr = seg040.LoadDax(0, 0, 3, "Title");
            seg040.draw_picture(dax_ptr, 0x0b, 6, 0);
            _keyboardInput.PressAnyKey(10);

            seg044.PlaySound(Sound.sound_d);
            dax_ptr = seg040.LoadDax(0, 0, 4, "Title");
            seg040.draw_picture(dax_ptr, 0x0b, 0, 0);
            _keyboardInput.PressAnyKey(10);

            seg041.ClearScreen();
            ShowCredits();
            _keyboardInput.PressAnyKey(10);

            seg041.ClearScreen();
        }

        private static void ShowCredits()
        {
            Display.UpdateStop();

            seg037.draw8x8_02();

            foreach (var text in HardCodedTexts)
                DisplayString(text.X, text.Y, text.ForegroundColor, text.Value);

            Display.UpdateStart();
        }

        private static void DisplayString(int x, int y, DisplayColors fgColor, string text)
        {
            seg041.displayString(text, DisplayColors.Black, fgColor, y, x);
        }
    }
}
