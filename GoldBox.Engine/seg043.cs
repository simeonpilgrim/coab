using GoldBox.Classes;
using GoldBox.Logging;

namespace GoldBox.Engine
{
    public class seg043
    {
        static bool in_print_and_exit = false;
        public static void print_and_exit()
        {
            if (in_print_and_exit) return;

            in_print_and_exit = true;

            seg044.PlaySound(Sound.sound_FF);

            Logger.Close();

            ItemLibrary.Write();

            seg001.EngineStop();
        }

        internal static byte GetInputKey()
        {
            byte key;

            if (gbl.inDemo == true)
            {
                if (seg049.KEYPRESSED() == true)
                {
                    key = seg049.READKEY();
                }
                else
                {
                    key = 0;
                }
            }
            else
            {
                key = seg049.READKEY();
            }

            if (key == 0x13)
            {
                seg044.PlaySound(Sound.sound_0);
            }

            if (Cheats.allow_keyboard_exit && key == 3)
            {
                print_and_exit();
            }

            if (key != 0)
            {
                while (seg049.KEYPRESSED() == true)
                {
                    key = seg049.READKEY();
                }
            }

            return key;
        }

        internal static void clear_keyboard()
        {
            while (seg049.KEYPRESSED())
            {
                GetInputKey();
            }
        }

        internal static void clear_one_keypress()
        {
            if (seg049.KEYPRESSED())
            {
                GetInputKey();
            }
        }
    }
}
