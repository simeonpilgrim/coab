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
            arg_0.fnc = sub_13E7A;
            arg_0.field_30 = 0;
        }

        static void sub_13F90(Text arg_0)
        {
            throw new System.NotSupportedException();//xor	    ax,	ax
            throw new System.NotSupportedException();//retf    4
        }

        static void sub_13EB2(Text arg_0)
        {
            //
            throw new System.NotSupportedException();//arg_0	    = dword ptr	 6
            //
            throw new System.NotSupportedException();//push    bp
            throw new System.NotSupportedException();//mov	    bp,	sp
            throw new System.NotSupportedException();//les	    di,	[bp+arg_0]
            throw new System.NotSupportedException();//assume es:nothing
            throw new System.NotSupportedException();//mov	    dx,	es:[di+4]
            throw new System.NotSupportedException();//dec	    dx
            throw new System.NotSupportedException();//dec	    dx
            throw new System.NotSupportedException();//mov	    si,	es:[di+8]
            throw new System.NotSupportedException();//les	    di,	es:[di+0Ch]
            throw new System.NotSupportedException();//xor	    bx,	bx
            //
            throw new System.NotSupportedException();//loc_13EC8:
            skipReadFlag = 0;
            throw new System.NotSupportedException();//al = READKEY();
            throw new System.NotSupportedException();//mov	    cx,	1
            throw new System.NotSupportedException();//cmp	    al,	8
            throw new System.NotSupportedException();//jz	    loc_13F0C
            throw new System.NotSupportedException();//cmp	    al,	13h
            throw new System.NotSupportedException();//jz	    loc_13F0C
            throw new System.NotSupportedException();//cmp	    al,	4
            throw new System.NotSupportedException();//jz	    loc_13F24
            throw new System.NotSupportedException();//dec	    cx
            throw new System.NotSupportedException();//cmp	    al,	1Bh
            throw new System.NotSupportedException();//jz	    loc_13F0C
            throw new System.NotSupportedException();//cmp	    al,	1
            throw new System.NotSupportedException();//jz	    loc_13F0C
            throw new System.NotSupportedException();//cmp	    al,	6
            throw new System.NotSupportedException();//jz	    loc_13F24
            throw new System.NotSupportedException();//cmp	    al,	1Ah
            throw new System.NotSupportedException();//jz	    loc_13F37
            throw new System.NotSupportedException();//cmp	    al,	0Dh
            throw new System.NotSupportedException();//jz	    loc_13F44
            throw new System.NotSupportedException();//cmp	    al,	20h ; ' '
            throw new System.NotSupportedException();//jb	    loc_13EC8
            throw new System.NotSupportedException();//cmp	    bx,	dx
            throw new System.NotSupportedException();//jz	    loc_13EC8
            throw new System.NotSupportedException();//mov	    es:[bx+di],	al
            throw new System.NotSupportedException();//inc	    bx
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//cmp	    bx,	si
            throw new System.NotSupportedException();//jbe	    loc_13EC8
            throw new System.NotSupportedException();//mov	    si,	bx
            throw new System.NotSupportedException();//jmp	    short loc_13EC8
            throw new System.NotSupportedException();//; 컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴�
            //
            throw new System.NotSupportedException();//loc_13F0C:				    ; CODE XREF: sub_13EB2+24j
            throw new System.NotSupportedException();//; sub_13EB2+28j ...
            throw new System.NotSupportedException();//or	    bx,	bx
            throw new System.NotSupportedException();//jz	    loc_13EC8
            throw new System.NotSupportedException();//mov	    al,	8
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//mov	    al,	' '
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//mov	    al,	8
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//dec	    bx
            throw new System.NotSupportedException();//loop    loc_13F0C
            throw new System.NotSupportedException();//jmp	    short loc_13EC8
            throw new System.NotSupportedException();//; 컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴�
            //
            throw new System.NotSupportedException();//loc_13F24:				    ; CODE XREF: sub_13EB2+2Cj
            throw new System.NotSupportedException();//; sub_13EB2+39j ...
            throw new System.NotSupportedException();//cmp	    bx,	si
            throw new System.NotSupportedException();//jz	    loc_13EC8
            throw new System.NotSupportedException();//mov	    al,	es:[bx+di]
            throw new System.NotSupportedException();//cmp	    al,	' '
            throw new System.NotSupportedException();//jb	    loc_13EC8
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//inc	    bx
            throw new System.NotSupportedException();//loop    loc_13F24
            throw new System.NotSupportedException();//jmp	    short loc_13EC8
            throw new System.NotSupportedException();//; 컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴�
            //
            throw new System.NotSupportedException();//loc_13F37:				    ; CODE XREF: sub_13EB2+3Dj
            throw new System.NotSupportedException();//cmp	    byte_1EFBF,	0
            throw new System.NotSupportedException();//jz	    loc_13EC8
            throw new System.NotSupportedException();//mov	    es:[bx+di],	al
            throw new System.NotSupportedException();//inc	    bx
            throw new System.NotSupportedException();//jmp	    short loc_13F4E
            throw new System.NotSupportedException();//; 컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴�
            //
            throw new System.NotSupportedException();//loc_13F44:				    ; CODE XREF: sub_13EB2+41j
            throw new System.NotSupportedException();//call    display_CR_LN
            throw new System.NotSupportedException();//mov	    word ptr es:[bx+di], 0A0Dh
            throw new System.NotSupportedException();//inc	    bx
            throw new System.NotSupportedException();//inc	    bx
            //
            throw new System.NotSupportedException();//loc_13F4E:				    ; CODE XREF: sub_13EB2+90j
            throw new System.NotSupportedException();//les	    di,	[bp+arg_0]
            throw new System.NotSupportedException();//xor	    ax,	ax
            throw new System.NotSupportedException();//mov	    es:[di+8], ax
            throw new System.NotSupportedException();//mov	    es:[di+0Ah], bx
            throw new System.NotSupportedException();//pop	    bp
            throw new System.NotSupportedException();//retf    4
        }

        static void sub_13F5F(Text arg_0)
        {
            throw new System.NotSupportedException();//mov	    bx,	sp
            throw new System.NotSupportedException();//les	    di,	ss:[bx+arg_0]
            throw new System.NotSupportedException();//mov	    cx,	es:[di+8]
            throw new System.NotSupportedException();//sub	    es:[di+8], cx
            throw new System.NotSupportedException();//jcxz    loc_13F88
            throw new System.NotSupportedException();//les	    di,	es:[di+0Ch]
            throw new System.NotSupportedException();//cmp	    textWndFlag, 0
            throw new System.NotSupportedException();//jnz	    loc_13F85
            //
            throw new System.NotSupportedException();//loc_13F7A:				    ; CODE XREF: sub_13F5F+22j
            throw new System.NotSupportedException();//mov	    al,	es:[di]
            throw new System.NotSupportedException();//call    display_char    ; outputs character	AX in graphic mode
            throw new System.NotSupportedException();//inc	    di
            throw new System.NotSupportedException();//loop    loc_13F7A
            throw new System.NotSupportedException();//jmp	    short loc_13F88
            throw new System.NotSupportedException();//; 컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴컴�
            //
            throw new System.NotSupportedException();//loc_13F85:				    ; CODE XREF: sub_13F5F+19j
            throw new System.NotSupportedException();//call    sub_14024
            //
            throw new System.NotSupportedException();//loc_13F88:				    ; CODE XREF: sub_13F5F+Ej
            throw new System.NotSupportedException();//; sub_13F5F+24j
            keyboard_emptybuffer();
            throw new System.NotSupportedException();//xor	    ax,	ax
            throw new System.NotSupportedException();//retf    4
        }

        internal static void sub_13E7A(Text arg_0)
        {
            TextDelegate ax = sub_13EB2;
            TextDelegate bx = sub_13F90;
            TextDelegate cx = bx;

            if (arg_0.field_2 != 0xD7B1)
            {
                arg_0.field_2 = 0xD7B2;

                ax = sub_13F5F;
                bx = sub_13F5F;
            }

            arg_0.field_14 = ax;
            arg_0.field_18 = bx;
            arg_0.field_1C = cx;
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
