using Classes;

namespace engine
{
    public class seg049
    {
        private static byte skipReadFlag;

        internal static void __CRTInit()
        {
            ASSIGNCRT(gbl.known01_01);

            ASSIGNCRT(gbl.known01_02);
            seg051.Rewrite(gbl.known01_02);
        }


        volatile static bool keyboard_read = false;

        internal static void keyboard_emptybuffer()
        {
            if (keyboard_read == false)
            {
                return;
            }

            keyboard_read = false;

            while (int_check_keyPressed() != 0)
            {
                int_get_keyPressed();
            }

            System.Console.Out.WriteLine("^C");
            throw new System.ApplicationException("Exit");
        }


        internal static void SysDelay(int arg_0)
        {
            if (arg_0 != 0)
            {
                System.Threading.Thread.Sleep(arg_0);
            }
        }

        public static ushort keyCode = 0;

        static ushort int_check_keyPressed()
        {
            //INT 16 - KEYBOARD - CHECK FOR KEYSTROKE
            //AH = 01h
            //Return: ZF set if no keystroke available
            //ZF clear if keystroke available
            //AH = BIOS scan code
            //AL = ASCII character
            //Note:	if a keystroke is present, it is not removed from the keyboard buffer;

            System.Threading.Thread.Sleep(10);

            return keyCode;
        }

        static ushort int_get_keyPressed()
        {
            //INT 16 - KEYBOARD - GET KEYSTROKE
            //AH = 00h
            //Return: AH = BIOS scan code
            //AL = ASCII character
            //Notes:	on extended keyboards, this function discards any extended keystrokes,
            //returning only when a non-extended keystroke is available

            ushort key = keyCode;
            keyCode = 0;

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

            keyboard_emptybuffer();

            return lastCode;
        }


        internal static void ASSIGNCRT(Text arg_0)
        {
            arg_0.field_2 = 0xD7B0;
            arg_0.field_4 = 0xD7B0;
            arg_0.field_C = arg_0.field_80;
            arg_0.field_30 = 0;
        }

        static void call_int10()
        {
            throw new System.NotSupportedException();//push	si
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//push	bp
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//int	0x10
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	si
            throw new System.NotSupportedException();//retn
        }

    }
}
