using System;
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

            if (key == Keys.Enter)
            {
                return 0x1C0D;
            }

            if (key == Keys.Space)
            {
                return 0x20;
            }

            if (key == Keys.Delete)
            {
                return 0x08;
            }

            if (key == Keys.Back)
            {
                return 0x08;
            }

            if (key == Keys.Home || key == Keys.NumPad7)
            {
                return 0x4700;
            }

            if (key == Keys.Up || key == Keys.NumPad8)
            {
                return 0x4800;
            }

            if (key == Keys.PageUp || key == Keys.NumPad9)
            {
                return 0x4900;
            }

            if (key == Keys.Left || key == Keys.NumPad4)
            {
                return 0x4B00;
            }

            if (key == Keys.NumPad5)
            {
                return 0x4C00;
            }

            if (key == Keys.Right || key == Keys.NumPad6)
            {
                return 0x4D00;
            }


            if (key == Keys.End || key == Keys.NumPad1)
            {
                return 0x4F00;
            }

            if (key == Keys.Down || key == Keys.NumPad2)
            {
                return 0x5000;
            }

            if (key == Keys.PageDown || key == Keys.NumPad3)
            {
                return 0x5100;
            }

            return 0x0020;
        }
    }
}
