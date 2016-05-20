using System.Windows.Forms;

namespace Main
{
    public class Keyboard
    {
        public static ushort KeyToIBMKey(Keys key)
        {
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                return (ushort)((key - Keys.D0) + '0');
            }
            if (key >= Keys.A && key <= Keys.Z)
            {
                return (ushort)key;
            }

            switch (key)
            {
                case Keys.Enter: return 0x1C0D;
                case Keys.Space: return 0x20;
                case Keys.Delete: return 0x5300;
                case Keys.Back: return 0x08;
                case Keys.OemOpenBrackets: return 0x4700;
                case Keys.NumPad7: return 0x4700;
                case Keys.Home: return 0x4700;
                case Keys.Up: return 0x4800;
                case Keys.NumPad8: return 0x4800;
                case Keys.PageUp: return 0x4900;
                case Keys.NumPad9: return 0x4900;
                case Keys.Left: return 0x4B00;
                case Keys.NumPad4: return 0x4B00;
                case Keys.NumPad5: return 0x4C00;
                case Keys.NumPad6: return 0x4D00;
                case Keys.Right: return 0x4D00;
                case Keys.End: return 0x4F00;
                case Keys.NumPad1: return 0x4F00;
                case Keys.OemCloseBrackets: return 0x4F00;
                case Keys.Down: return 0x5000;
                case Keys.NumPad2: return 0x5000;
                case Keys.PageDown: return 0x5100;
                case Keys.NumPad3: return 0x5100;
                case Keys.OemMinus: return 0x2d00;
                case Keys.Escape: return 0x1b;
                case Keys.Oemcomma: return 0x2c;
                case Keys.OemPeriod: return 0x2e;
                default: return 0x20;
            }
        }
    }
}
