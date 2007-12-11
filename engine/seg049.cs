using Classes;
using System.Collections.Generic;
using System.Threading;

namespace engine
{
    public class seg049
    {
        private static byte skipReadFlag;

        internal static void SysDelay(int arg_0)
        {
            if (arg_0 != 0)
            {
                System.Threading.Thread.Sleep(arg_0);
            }
        }


        private static Queue<ushort> keysPressed = new Queue<ushort>();
        private static Semaphore WaitForKey = new Semaphore(0, 1);

        static public void AddKey(ushort key)
        {
            lock(keysPressed)
            {
                keysPressed.Enqueue(key);

                if( keysPressed.Count == 1 )
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
