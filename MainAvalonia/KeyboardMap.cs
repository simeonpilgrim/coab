using Avalonia.Input;

namespace MainAvalonia
{
    // Avalonia port of Main.Keyboard: maps UI key events to the IBM PC
    // keyboard codes the original engine consumes
    public class KeyboardMap
    {
        public static ushort KeyToIBMKey(Key key)
        {
            if (key >= Key.D0 && key <= Key.D9)
            {
                return (ushort)((key - Key.D0) + '0');
            }
            if (key >= Key.A && key <= Key.Z)
            {
                return (ushort)((key - Key.A) + 'A');
            }

            if (key == Key.Enter)
            {
                return 0x1C0D;
            }

            if (key == Key.Space)
            {
                return 0x20;
            }

            if (key == Key.Delete)
            {
                return 0x5300;
            }

            if (key == Key.Back)
            {
                return 0x08;
            }

            if (key == Key.Home || key == Key.NumPad7 || key == Key.OemOpenBrackets)
            {
                return 0x4700;
            }

            if (key == Key.Up || key == Key.NumPad8)
            {
                return 0x4800;
            }

            if (key == Key.PageUp || key == Key.NumPad9)
            {
                return 0x4900;
            }

            if (key == Key.Left || key == Key.NumPad4)
            {
                return 0x4B00;
            }

            if (key == Key.NumPad5)
            {
                return 0x4C00;
            }

            if (key == Key.Right || key == Key.NumPad6)
            {
                return 0x4D00;
            }

            if (key == Key.End || key == Key.NumPad1 || key == Key.OemCloseBrackets)
            {
                return 0x4F00;
            }

            if (key == Key.Down || key == Key.NumPad2)
            {
                return 0x5000;
            }

            if (key == Key.PageDown || key == Key.NumPad3)
            {
                return 0x5100;
            }

            if (key == Key.OemMinus)
            {
                return 0x2d00;
            }

            if (key == Key.Escape)
            {
                return 0x1b;
            }

            if (key == Key.OemComma)
            {
                return 0x2c;
            }

            if (key == Key.OemPeriod)
            {
                return 0x2e;
            }

            return 0;
        }
    }
}
