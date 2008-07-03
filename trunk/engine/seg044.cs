using Classes;

namespace engine
{
    class seg044
    {
        static byte byte_121DB;
        static byte byte_121DC;

        static short word_121DE;

        static short word_12202;
        static short word_12204;
        static short word_12206;
        static short word_12208;
        static short word_1220A;

        static byte[] byte_1220C = new byte[5];

        const int unk_12230_len = 0x30 / 2; // each block is 0x30 long, but is words.
        static ushort[,] unk_12230 = new ushort[4, unk_12230_len]; /* 4 x 0x30 */

        static short word_123C8;
        static ushort[,] unk_123CA = new ushort[8,24]; /* seg044:030a */

        static short[] word_12562 =  /* seg044:04a2 */    { 
            0x0C6E,0x0C6E,0x0C6E,0x0C6E,0x0C76,0x0C6E,0x0C6E,0x0C6E,0x0B6E,
            0x0C6E,0x0C6E,0x0C6E,0x0BDE,0x0C6E,0x0C6E,0x0C6E,0x0C06,0x0C6E,
            0x0C6E,0x0C6E,0x0BA6,0x0C6E,0x0C6E,0x0C6E,0x0C3E,0x0C6E,0x0C6E,
            0x0C6E,0x0B46,0x0C6E,0x0C6E,0x0C6E,0x0B02,0x0C6E,0x0C6E,0x0C6E,
            0x0B1E,0x0C6E,0x0C6E,0x0C6E,0x912,0x0C6E,0x0C6E,0x0C6E,0x0CBE,
            0x0C6E,0x0C6E,0x0C6E,0x952,0x0C6E,0x0C6E,0x0C6E,0x992,0x0C6E,
            0x0C6E,0x0C6E };


        static ushort[] word_1260A = /* seg044:054A */ { 0x1102,0x1102,0x1102,0x1102 };


        static Set set_01 = new Set(0x0020, new byte[]{ 0xFF, 0xFF, 0 , 0 , 0, 0 ,  0 ,  
            0 , 0 , 0 , 0 ,0 ,0 , 0 ,0 , 0 ,0 , 0 ,0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0x80 });

        internal static void sound_sub_120E0(short arg_0) /*sub_120E0*/
        {
            if (set_01.MemberOf((byte)arg_0) == true)
            {
                if (arg_0 == 0)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sound_sub_137B1();
                        sound_sub_13745(0);
                    }
                }
                else if (arg_0 == 1)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        byte_1220C[4] = 1;
                    }
                }
                else if (arg_0 == 0xff)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sound_sub_137B1();
                        sound_sub_13745(0);
                        sound_sub_133B4();
                        gbl.gameFlag01 = false;
                    }
                }
                else if (arg_0 >= 2 && arg_0 <= 14)
                {
                    if (gbl.soundType == SoundType.PC)
                    {
                        sound_sub_13745((short)(arg_0 - 1));
                    }
                }
                else if (arg_0 == 15)
                {
                    word_121DE = 0x1388;
                }
            }
        }


        internal static void sound_sub_12194()
        {
            gbl.soundTypeBackup = gbl.soundType;
            gbl.gameFlag01 = true;

            if (gbl.soundType != SoundType.None)
            {
                sound_sub_1337F();
                sound_sub_13745(0);
                sound_sub_137B1();
            }
        }

        internal static void sound_sub_121BF()
        {
            word_121DE = 0;
        }

        static System.Timers.Timer aTimer = null;
        static object timer_lock = new object();

        private static void sound_sub_1337F()
        {
            lock (timer_lock)
            {
                if (aTimer == null)
                {
                    aTimer = new System.Timers.Timer();
                    // Hook up the Elapsed event for the timer.
                    aTimer.Elapsed += new System.Timers.ElapsedEventHandler(sound_sub_133ED);

                    // Set the Interval to 5 milliseconds.
                    aTimer.Interval = 5;
                    aTimer.Enabled = true;

                    // Keep the timer alive until the end of Main.
                    System.GC.KeepAlive(aTimer);
                }
            }
        }


        private static void sound_sub_133B4()
        {
            //assume ds:seg044

            // Set Original Int 08 Handler (saved into dword_1337B )

            // Speaker Off
        }

        /* called with 236.6Hz clock */
        private static void sound_sub_133ED(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (timer_lock)
            {
                if (word_121DE != 0)
                {
                    word_121DE--;

                    if (word_121DE == 0)
                    {
                        throw new System.ApplicationException("Exit: -1");
                    }
                }

                if (byte_121DB == 0)
                {
                    byte_121DB = 1;
                    sound_sub_1347A();
                    sound_sub_134EF();
                    byte_121DB = 0;
                }
            }
        }

        private static void SetTone(ushort ax)
        {
            //TODO Set the tone...
        }


        private static void sound_sub_1347A()
        {
            short bx = word_12202;
            if (bx != 0)
            {
                word_123C8 = 0;
                byte_121DC = 4;
                word_12208 = 0x02f0;
                word_12206 = 0x0170;
                
                int si = 0;

                do
                {
                    if (unk_12230[si,0] != 0)
                    {
                        sound_sub_1357D(si);
                        if (word_123C8 == 0 &&
                            unk_12230[si, 5] != 0 &&
                            unk_12230[si, 0] != 0)
                        {
                            word_123C8 = (short)((si*0x30) + 0x170);
                        }
                    }

                    si += 1;
                } while (--byte_121DC != 0);


                if (byte_1220C[4] != 0 &&
                    word_123C8 != 0)
                {
                    si = word_123C8;
                    int i = (si-0x170)/0x30;
                    ushort ax = unk_12230[i, 8];
                    
                    SetTone(ax);

                    byte bl = (byte)(unk_12230[i, 0xA] & 3);
                    if (bl == 3)
                    {
                        // Speaker On
                    }
                    else
                    {
                        // Speaker Off
                    }
                }
                else
                {
                    // Speaker Off
                }

            }
        }

        private static void out_c0(byte val)
        {
        }

        private static void sound_sub_134EF()
        {
            short bx = word_12202;
            if (bx != 0)
            {
                byte_121DC = 4;

                word_12208 = 0x48A;
                word_12206 = 0x30A;
                short si = 0;

                do
                {
                    if (unk_123CA[si,0] != 0)
                    {
                        sound_sub_1357D(si);
                    }

                    si += 1;
                } while (--byte_121DC != 0);

                if (byte_1220C[4] != 0)
                {
                    byte_121DC = 4;

                    si = 0; //0x30A;
                    byte dl = 0x80;
 
                    do
                    {
                        ushort ax = unk_123CA[si, 8 / 2];

                        ax >>= 2;
                        ax = (byte)((ax & 0xFF) >> 4);
                        ax ^= dl;
                        out_c0((byte)ax);

                        if (dl != 0xe0)
                        {
                            out_c0((byte)(ax >> 8));
                        }
                        ax = 0xFFFF;
                        ax -= unk_123CA[si, 0xA / 2];

                        ax = (ushort)(ax >> 12);
                        ax ^= dl;
                        ax += 0x10;
                        out_c0((byte)ax);

                        dl += 0x20;
                        si += 1;
                    } while (--byte_121DC != 0);

                    return;
                }
            }
        }


        private static void sound_sub_1357D(int si)
        {
            unk_12230[si, 5] = unk_12230[si, 6];
            unk_12230[si, 2] = unk_12230[si, 3];
            
            int dx = 0;

            int ax = unk_12230[si, 0x1E / 2] + unk_12230[si, 0x20 / 2];
            if (ax != 0)
            {
                if (ax >= unk_12230[si, 0x24 / 2])
                {
                    ax -= unk_12230[si, 0x24 / 2];
                }

                unk_12230[si, 0x1E / 2] = (ushort)ax;

                ax >>= 4;
                ax += unk_12230[si, 0x1C / 2];


                //mov	di, ax
                //mov	ah, [di]
                //mov	al, 0
                ax = getbyte(ax) << 8;

                ax *= unk_12230[si, 0x22 / 2];
            }

            unk_12230[si, 0x8 / 2] = (ushort)(unk_12230[si, 0x4 / 2] + (ax >> 16));

            if (unk_12230[si, 0x14 / 2] != 0 &&
                --unk_12230[si, 0x14 / 2] == 0)
            {
                unk_12230[si, 0x18 / 2] = 0x10;
                unk_12230[si, 0x1A / 2] = 1;
            }

            if (--unk_12230[si, 0] == 0)
            {
                sound_sub_1360E(si);
            }

            if (unk_12230[si, 0x1A / 2] != 0 &&
                --unk_12230[si, 0x1A / 2] == 0)
            {
                throw new System.NotSupportedException();//loc_135DD:
                throw new System.NotSupportedException();//mov	bx, [si+16h]
                throw new System.NotSupportedException();//add	bx, [si+18h]
                throw new System.NotSupportedException();//cmp	short ptr [bx+2], 0x0FFFF
                throw new System.NotSupportedException();//jnz	loc_135FC
                throw new System.NotSupportedException();//mov	ax, [bx]
                throw new System.NotSupportedException();//mov	[si+0Ah], ax
                throw new System.NotSupportedException();//cmp	ax, 0
                throw new System.NotSupportedException();//jnz	loc_135F6
                throw new System.NotSupportedException();//mov	[si+0Ch], ax
                throw new System.NotSupportedException();//loc_135F6:
                throw new System.NotSupportedException();//add	short ptr [si+18h], 4
                throw new System.NotSupportedException();//jmp	short loc_135DD
                throw new System.NotSupportedException();//loc_135FC:
                throw new System.NotSupportedException();//mov	ax, [bx]
                throw new System.NotSupportedException();//mov	[si+0Ch], ax
                throw new System.NotSupportedException();//mov	ax, [bx+2]
                throw new System.NotSupportedException();//mov	[si+1Ah], ax
                throw new System.NotSupportedException();//add	short ptr [si+18h], 4
            }
        }

        static byte[] bin_data;
        internal static bool load_dump_bin()
        {
            if (System.IO.File.Exists("dump.bin"))
            {
                System.IO.FileStream fs = System.IO.File.Open("dump.bin", System.IO.FileMode.Open);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                br.BaseStream.Seek(0x6A90, System.IO.SeekOrigin.Begin);
                bin_data = br.ReadBytes(0x1300);

                return true;         
            }

            return false;
        }

        private static byte getbyte(int di)
        {
            byte b = bin_data[di];
            return b;
        }

        private static ushort getword(int di)
        {
            ushort us = Sys.ArrayToUshort(bin_data, di);
            return us;
        }

        private static void sound_sub_1360E(int si)
        {
            int di = unk_12230[si, 0x2 / 2];
            if (di == 0)
            {
                return;
            }

            word_12204 = (short)si;

        loc_1361B:

            byte bl = getbyte(di);
            di += 1;

            if (bl < 0xFA)
            {
                goto loc_136C7;
            }


            bl -= 0xFA;

            //ushort[] jt = { 0x1591, 0x15EF, 0x15f6, 0x1588, 0x1571, 0x15c2 };
            //jump jt[bl]

            if (bl == 5) goto bl_5;
            if (bl == 4) goto bl_4;
            if (bl == 3) goto bl_3;
            if (bl == 2) goto bl_2;
            if (bl == 1) goto bl_1;
            if (bl == 0) goto bl_0;


        bl_4:
            bl = getbyte(di);
            di += 1;

            ushort ax = getword(di);
            di += 2;

            if (unk_12230[si, bl / 2] != 0)
            {
                unk_12230[si, bl / 2] -= 1;
                if (unk_12230[si, bl / 2] == 0)
                {
                    goto loc_1361B;
                }
            }

            di = ax;
            goto loc_1361B;

        bl_3:
            si = getword(di);
            di += 2;
            si += word_12206;

        bl_0:
            unk_12230[si, 0x04 / 2] = 0;
            unk_12230[si, 0x06 / 2] = 0;
            unk_12230[si, 0x08 / 2] = 0;
            unk_12230[si, 0x0A / 2] = 0;
            unk_12230[si, 0x0C / 2] = 0;
            unk_12230[si, 0x10 / 2] = 0;
            unk_12230[si, 0x12 / 2] = 0;
            unk_12230[si, 0x16 / 2] = 0;
            unk_12230[si, 0x18 / 2] = 0;
            unk_12230[si, 0x1A / 2] = 0;
            unk_12230[si, 0x1C / 2] = 0;
            unk_12230[si, 0x1E / 2] = 0;
            unk_12230[si, 0x20 / 2] = 0;
            unk_12230[si, 0x22 / 2] = 0;
            unk_12230[si, 0x24 / 2] = 0;
            goto loc_1361B;

        bl_5:
            bl = getbyte(di);
            di += 1;
            ax = getword(di);
            di += 2;
            unk_12230[si, bl / 2] = ax;

            if (bl != 0) goto loc_1361B;

        loc_13692:
            si = word_12204;
            if (unk_12230[si, 0x0 / 2] == 0)
            {
                bl = 4;
                bl -= byte_121DC;
                byte_1220C[bl] = 0;
                di = 0;
            }
            unk_12230[si, 0x2 / 2] = (ushort)di;
            return;

        bl_1:
            di = word_1220A;
            goto loc_1361B;

        bl_2:
            ax = getword(di);
            di += 2;

            word_1220A = (short)di;
            di = ax;
            goto loc_1361B;

        loc_136C4:
            bl = getbyte(di);
            di += 1;

        loc_136C7:

            int push_di = di;
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	al, bl
            throw new System.NotSupportedException();//mov	cl, 5
            throw new System.NotSupportedException();//shr	al, cl
            throw new System.NotSupportedException();//mov	cl, al
            throw new System.NotSupportedException();//mul	byte_1642C+1
            throw new System.NotSupportedException();//add	ax, ds:word_12206
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//pop	bx
            throw new System.NotSupportedException();//push	bx
            throw new System.NotSupportedException();//mov	ax, [si+0Eh]
            throw new System.NotSupportedException();//cmp	al, 1
            throw new System.NotSupportedException();//ja	loc_136EA
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//mov	al, [bx+1]
            throw new System.NotSupportedException();//jmp	short loc_136ED
            throw new System.NotSupportedException();//loc_136EA:
            throw new System.NotSupportedException();//mul	byte ptr [bx+1]
            throw new System.NotSupportedException();//loc_136ED:
            throw new System.NotSupportedException();//mov	[si], ax
            throw new System.NotSupportedException();//mov	al, [bx]
            throw new System.NotSupportedException();//and	ax, 0x7F
            throw new System.NotSupportedException();//cmp	ax, 0x7F
            throw new System.NotSupportedException();//jz	loc_13737
            throw new System.NotSupportedException();//cmp	cl, 0
            throw new System.NotSupportedException();//ja	loc_13737
            throw new System.NotSupportedException();//mov	cx, [si]
            throw new System.NotSupportedException();//mov	[di], cx
            throw new System.NotSupportedException();//mov	[di+14h], cx
            throw new System.NotSupportedException();//add	ax, [di+12h]
            throw new System.NotSupportedException();//mov	short ptr [di+18h], 0
            throw new System.NotSupportedException();//mov	short ptr [di+1Ah], 1
            throw new System.NotSupportedException();//mov	cx, 0x0FF
            throw new System.NotSupportedException();//loc_13715:
            throw new System.NotSupportedException();//inc	cl
            throw new System.NotSupportedException();//sub	ax, 0x0C
            throw new System.NotSupportedException();//jnb	loc_13715
            throw new System.NotSupportedException();//add	ax, 0x0C
            throw new System.NotSupportedException();//mov	bx, ax
            throw new System.NotSupportedException();//add	bx, bx
            throw new System.NotSupportedException();//cmp	di, 0x39A
            throw new System.NotSupportedException();//jnz	$+2

            throw new System.NotSupportedException();//add	bx, ds:word_12208
            throw new System.NotSupportedException();//mov	ax, [bx]
            throw new System.NotSupportedException();//shr	ax, cl
            throw new System.NotSupportedException();//mov	[di+4],	ax
            throw new System.NotSupportedException();//mov	[di+8],	ax
            throw new System.NotSupportedException();//loc_13737:
            throw new System.NotSupportedException();//pop	di

            byte al = getbyte(di);
            di += 2;
            if ((al & 0x80) != 0)
            {
                goto loc_136C4;
            }
            goto loc_13692;
        }


        private static void sound_sub_13745(short arg_0)
        {
            word_12202 = arg_0;
            byte_121DB = 1;
            byte_121DC = 4;

            int di = 0;
            int si = arg_0 * 4;
            do
            {
                int ax = word_12562[si];
                if (ax != 0)
                {
                    for (int i = 0; i < unk_12230_len; i++)
                    {
                        unk_12230[di, i] = 0;
                    }

                    unk_12230[di, 1] = (ushort)ax;
                    unk_12230[di, 0] = 1;

                    byte_1220C[4 - byte_121DC] = (byte)word_12202;
                }

                di += 1;
                si += 1;
            } while (--byte_121DC != 0);

            byte_121DB = 0;
        }


        private static void sound_sub_137B1(/* short arg_0 == 0 */)
        {
            word_12202 = 0;
            byte_121DB = 1;
            byte_121DC = 4;

            int si = 0;
            int di = 0;

            do
            {
                ushort ax = word_1260A[si];
                if (ax != 0)
                {
                    for (int i = 0; i < 24; i++)
                        unk_123CA[di, i] = 0;

                    unk_123CA[di,2/2] = ax;

                    byte_1220C[4 - byte_121DC] = (byte)word_12202;

                    unk_123CA[di,0] = 1;
                }

                di += 1;
                si += 1;
            } while (--byte_121DC != 0);

            byte_121DB = 0;
        }
    }
}
