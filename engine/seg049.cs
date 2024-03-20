using System.Collections.Generic;
using System.Threading;

namespace engine
{
    public class seg049
    {
        private static byte skipReadFlag;
        private static int SleepUntil = System.Environment.TickCount;
        private static readonly int minTick = 16;

        internal static void SysDelay(int milliseconds)
        {
            int TickCount = System.Environment.TickCount;

            // If the last sleep was in the past, reset
            if (TickCount > SleepUntil)
            {
                SleepUntil = TickCount;
            }

            // If the sleep is long enough, or the accumulated skipped sleeps are long enough, actually sleep
            if ( milliseconds >= minTick || SleepUntil - TickCount >= minTick - milliseconds)
            {
                System.Threading.Thread.Sleep(milliseconds);
                SleepUntil = System.Environment.TickCount;
            }
            else // otherwise track how much sleep time we skipped
            {
                SleepUntil += milliseconds;
            }
        }


        private static Queue<ushort> keysPressed = new Queue<ushort>();
        private static Semaphore WaitForKey = new Semaphore(0, 1);

        static public void AddKey(ushort key)
        {
            lock (keysPressed)
            {
                keysPressed.Enqueue(key);

                if (keysPressed.Count == 1)
                {
                    WaitForKey.Release();
                }
            }
        }


        static ushort int_check_keyPressed()
        {
            //INT 16 - KEYBOARD - CHECK FOR KEYSTROKE
            //AH = 01h
            //Return: ZF set if no keystroke available
            //ZF clear if keystroke available
            //AH = BIOS scan code
            //AL = ASCII character
            //Note:	if a keystroke is present, it is not removed from the keyboard buffer;

            //System.Threading.Thread.Sleep(10);

            lock (keysPressed)
            {
                if (keysPressed.Count > 0)
                {
                    return keysPressed.Peek();
                }

                return 0;
            }
        }

        static ushort int_get_keyPressed()
        {
            //INT 16 - KEYBOARD - GET KEYSTROKE
            //AH = 00h
            //Return: AH = BIOS scan code
            //AL = ASCII character
            //Notes:	on extended keyboards, this function discards any extended keystrokes,
            //returning only when a non-extended keystroke is available

            ushort key = 0;

            WaitForKey.WaitOne();
            lock (keysPressed)
            {
                key = keysPressed.Dequeue();

                if (keysPressed.Count > 0)
                {
                    WaitForKey.Release();
                }
            }

            return key;
        }



        internal static bool KEYPRESSED()
        {
            if (skipReadFlag == 0)
            {
                return (int_check_keyPressed() != 0);
            }
            else
            {
                return true;
            }
        }


        internal static byte READKEY()
        {
            byte lastCode = skipReadFlag;

            skipReadFlag = 0;

            if (lastCode == 0)
            {
                ushort responce = int_get_keyPressed();
                lastCode = (byte)responce;

                if ((responce & 0x00ff) == 0)
                {
                    skipReadFlag = (byte)(responce >> 8);
                    if (skipReadFlag == 0)
                    {
                        lastCode = 3;
                    }
                }
            }

            return lastCode;
        }
    }
}
