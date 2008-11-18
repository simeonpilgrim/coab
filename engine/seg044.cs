using Classes;

namespace engine
{
    class soundblob
    {
        public ushort field_00;
        public ushort field_02;
        public ushort field_04;
        public ushort field_06;
        public ushort field_08;
        public ushort field_0a;
        public ushort field_0c;
        public ushort field_0e;
        public ushort field_10;
        public ushort field_12;
        public ushort field_14;
        public ushort field_16;
        public ushort field_18;
        public ushort field_1a;
        public ushort field_1c;
        public ushort field_1e;
        public ushort field_20;
        public ushort field_22;
        public ushort field_24;
        public ushort field_26;
        public ushort field_28;
        public ushort field_2a;
        public ushort field_2c;
        public ushort field_2e;

        internal void Reset()
        {
            field_00 = 0;
            field_02 = 0;
            field_04 = 0;
            field_06 = 0;
            field_08 = 0;
            field_0a = 0;
            field_0c = 0;
            field_0e = 0;
            field_10 = 0;
            field_12 = 0;
            field_14 = 0;
            field_16 = 0;
            field_18 = 0;
            field_1a = 0;
            field_1c = 0;
            field_1e = 0;
            field_20 = 0;
            field_22 = 0;
            field_24 = 0;
            field_26 = 0;
            field_28 = 0;
            field_2a = 0;
            field_2c = 0;
            field_2e = 0;
        }

        public ushort this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0x00: return field_00;
                    case 0x02: return field_02;
                    case 0x04: return field_04;
                    case 0x06: return field_06;
                    case 0x08: return field_08;
                    case 0x0a: return field_0a;
                    case 0x0c: return field_0c;
                    case 0x0e: return field_0e;
                    case 0x10: return field_10;
                    case 0x12: return field_12;
                    case 0x14: return field_14;
                    case 0x16: return field_16;
                    case 0x18: return field_18;
                    case 0x1a: return field_1a;
                    case 0x1c: return field_1c;
                    case 0x1e: return field_1e;
                    case 0x20: return field_20;
                    case 0x22: return field_22;
                    case 0x24: return field_24;
                    case 0x26: return field_26;
                    case 0x28: return field_28;
                    case 0x2a: return field_2a;
                    case 0x2c: return field_2c;
                    case 0x2e: return field_2e;
                    default: throw new System.Exception();
                }
            }
            set
            {
                switch (index)
                {
                    case 0x00: field_00 = value; break;
                    case 0x02: field_02 = value; break;
                    case 0x04: field_04 = value; break;
                    case 0x06: field_06 = value; break;
                    case 0x08: field_08 = value; break;
                    case 0x0a: field_0a = value; break;
                    case 0x0c: field_0c = value; break;
                    case 0x0e: field_0e = value; break;
                    case 0x10: field_10 = value; break;
                    case 0x12: field_12 = value; break;
                    case 0x14: field_14 = value; break;
                    case 0x16: field_16 = value; break;
                    case 0x18: field_18 = value; break;
                    case 0x1a: field_1a = value; break;
                    case 0x1c: field_1c = value; break;
                    case 0x1e: field_1e = value; break;
                    case 0x20: field_20 = value; break;
                    case 0x22: field_22 = value; break;
                    case 0x24: field_24 = value; break;
                    case 0x26: field_26 = value; break;
                    case 0x28: field_28 = value; break;
                    case 0x2a: field_2a = value; break;
                    case 0x2c: field_2c = value; break;
                    case 0x2e: field_2e = value; break;
                    default: throw new System.Exception();
                }
            }
        }
        
    }

    class soundblobsave
    {
        public soundblob[] Data {get;set;}
        public int Index {get;set;}
    }

    class seg044
    {
        static bool byte_121DB;
        static byte byte_121DC;

        static short word_121DE;

        static int playingSampleId; // word_12202
        static soundblobsave word_12204; 
        static short word_12206;
        //static short word_12208;
        static short word_1220A;

        static byte[] byte_1220C = new byte[5];

        const int unk_12230_len = 0x30 / 2; // each block is 0x30 long, but is words.
        //static ushort[,] unk_12230 = new ushort[4, unk_12230_len]; /* 4 x 0x30 */
        static soundblob[] unk_12230 = { new soundblob(), new soundblob(), new soundblob(), new soundblob() };

        static soundblobsave unk_12230_si; // word_123C8

        static soundblob[] unk_123CA = { new soundblob(), new soundblob(), new soundblob(), new soundblob() }; /* seg044:030a */

        static short[] word_12562 =  /* seg044:04a2 */    { 
            0x0C6E,0x0C6E,0x0C6E,0x0C6E,//0
            0x0C76,0x0C6E,0x0C6E,0x0C6E,//1
            0x0B6E,0x0C6E,0x0C6E,0x0C6E,//2
            0x0BDE,0x0C6E,0x0C6E,0x0C6E,//3
            0x0C06,0x0C6E,0x0C6E,0x0C6E,//4
            0x0BA6,0x0C6E,0x0C6E,0x0C6E,//5
            0x0C3E,0x0C6E,0x0C6E,0x0C6E,//6
            0x0B46,0x0C6E,0x0C6E,0x0C6E,//7
            0x0B02,0x0C6E,0x0C6E,0x0C6E,//8
            0x0B1E,0x0C6E,0x0C6E,0x0C6E,//9
            0x0912,0x0C6E,0x0C6E,0x0C6E,//10
            0x0CBE,0x0C6E,0x0C6E,0x0C6E,//11
            0x0952,0x0C6E,0x0C6E,0x0C6E,//12
            0x0992,0x0C6E,0x0C6E,0x0C6E };


        static ushort[] word_1260A = /* seg044:054A */ { 0x1102, 0x1102, 0x1102, 0x1102 };


        internal static void sound_sub_120E0(Sound arg_0) /*sub_120E0*/
        {
            lock (timer_lock)
            {
                if (arg_0 == Sound.sound_0)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sound_sub_137B1();
                        sound_copy_sub_13745(0);
                    }
                }
                else if (arg_0 == Sound.sound_1)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        byte_1220C[4] = 1;
                    }
                }
                else if (arg_0 == Sound.sound_FF) // off maybe.
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sound_sub_137B1();
                        sound_copy_sub_13745(0);
                        sound_restore_sub_133B4();
                        gbl.soundFlag01 = false;
                    }
                }
                else if (arg_0 >= Sound.sound_2 && arg_0 <= Sound.sound_e)
                {
                    if (gbl.soundType == SoundType.PC)
                    {
                        sound_copy_sub_13745((short)(arg_0 - 1));
                    }
                }
                else if (arg_0 == Sound.sound_f)
                {
                    word_121DE = 5000;
                }
            }
        }


        internal static void sound_sub_12194()
        {
            lock (timer_lock)
            {
                gbl.soundTypeBackup = gbl.soundType;
                gbl.soundFlag01 = true;

                if (gbl.soundType != SoundType.None)
                {
                    sound_start_timer_sub_1337F();
                    sound_copy_sub_13745(0);
                    sound_sub_137B1();
                }
            }
        }

        internal static void sound_sub_121BF()
        {
            word_121DE = 0;
        }

        static System.Timers.Timer aTimer = null;
        static object timer_lock = new object();

        private static void sound_start_timer_sub_1337F()
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


        private static void sound_restore_sub_133B4()
        {
            // Set Original Int 08 Handler (saved into dword_1337B )

            // Speaker Off
            soundOn = false;
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

                if (byte_121DB == false)
                {
                    byte_121DB = true;
                    sound_sub_1347A();
                    sound_sub_134EF();
                    byte_121DB = false;
                }
            }
        }

        private static void SetTone(ushort ax)
        {
            //TODO Set the tone...
            int hz = 1193181;
        }

        static bool soundOn = false;

        private static void sound_sub_1347A()
        {
            int bx = playingSampleId;
            if (bx != 0)
            {
                unk_12230_si = null;
                byte_121DC = 4;
                //word_12208 = 0x02f0;
                word_12206 = 0x0170;

                int si = 0;

                do
                {
                    if (unk_12230[si].field_00 != 0)
                    {
                        sound_sub_1357D(ref si, unk_12230);
                        if (unk_12230_si == null &&
                            unk_12230[si].field_0a != 0 &&
                            unk_12230[si].field_00 != 0)
                        {
                            unk_12230_si = new soundblobsave { Data = unk_12230, Index = si };
                        }
                    }

                    si += 1;
                } while (--byte_121DC != 0);


                if (byte_1220C[4] != 0 &&
                    unk_12230_si != null)
                {
                    si = unk_12230_si.Index;
                    ushort ax = unk_12230[si].field_08;

                    SetTone(ax);

                    byte bl = (byte)(unk_12230[si].field_0a & 3);
                    if (bl == 3)
                    {
                        soundOn = true;
                    }
                    else
                    {
                        soundOn = false;
                    }
                }
                else
                {
                    soundOn = false;
                }

            }
            else
            {
                soundOn = false;
            }
        }

        private static void out_c0(byte val)
        {
            if (soundOn)
            {
                int i = val;

            }
        }

        private static void sound_sub_134EF()
        {
            int bx = playingSampleId;
            if (bx != 0)
            {
                byte_121DC = 4;

                //word_12208 = 0x48A;
                word_12206 = 0x30A;
                int si = 0;

                do
                {
                    if (unk_123CA[si].field_00 != 0)
                    {
                        sound_sub_1357D(ref si, unk_123CA);
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
                        ushort ax = unk_123CA[si].field_08;

                        ax >>= 2;
                        ax = (byte)((ax & 0xFF) >> 4);
                        ax ^= dl;
                        out_c0((byte)ax);

                        if (dl != 0xe0)
                        {
                            out_c0((byte)(ax >> 8));
                        }
                        ax = 0xFFFF;
                        ax -= unk_123CA[si].field_0a;

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
            out_c0(0x9f);
            out_c0(0xbf);
            out_c0(0xdf);
            out_c0(0xff);
        }


        private static void sound_sub_1357D(ref int si, soundblob[] mem)
        {
            mem[si].field_0a = mem[si].field_0c;
            mem[si].field_04 = mem[si].field_06;

            int dx = 0;

            int ax = mem[si].field_1e + mem[si].field_20;
            if (ax != 0)
            {
                if (ax >= mem[si].field_24)
                {
                    ax -= mem[si].field_24;
                }

                unk_12230[si].field_1e = (ushort)ax;

                ax /= 16;
                ax += mem[si].field_1c;

                ax = getbyte(ax) * 256;

                ax *= mem[si].field_22;
                dx = ax >> 16;
            }

            mem[si].field_08 = (ushort)(mem[si].field_04 + dx);

            if (mem[si].field_14 != 0 &&
                --mem[si].field_14 == 0)
            {
                mem[si].field_18 = 0x10;
                mem[si].field_1a = 1;
            }

            if (--mem[si].field_00 == 0)
            {
                sound_sub_1360E(ref si,mem);
            }

            if (mem[si].field_1a != 0 &&
                --mem[si].field_1a == 0)
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
            string s = string.Format("Byte: {0:X}:{1:X} ", di, b);
            return b;
        }

        private static ushort getword(int di)
        {
            ushort us = Sys.ArrayToUshort(bin_data, di);
            string s = string.Format("Word: {0:X}:{1:X} ", di, us);
            return us;
        }

        private static void sound_sub_1360E(ref int si, soundblob[] mem)
        {
            int di = mem[si].field_02;
            if (di == 0)
            {
                return;
            }

            word_12204 = new soundblobsave { Data = mem, Index = si };

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

            if (mem[si][bl] != 0)
            {
                mem[si][bl] -= 1;
                if (mem[si][bl] == 0)
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
            mem[si].field_04 = 0;
            mem[si].field_06  = 0;
            mem[si].field_08  = 0;
            mem[si].field_0a  = 0;
            mem[si].field_0c  = 0;
            mem[si].field_10  = 0;
            mem[si].field_12  = 0;
            mem[si].field_16  = 0;
            mem[si].field_18 = 0;
            mem[si].field_1a  = 0;
            mem[si].field_1c = 0;
            mem[si].field_1e = 0;
            mem[si].field_20 = 0;
            mem[si].field_22 = 0;
            mem[si].field_24 = 0;
            goto loc_1361B;

        bl_5:
            bl = getbyte(di);
            di += 1;
            ax = getword(di);
            di += 2;
            mem[si][bl] = ax;

            if (bl != 0) goto loc_1361B;

        loc_13692:
            si = word_12204.Index;
            mem = word_12204.Data;
            if (mem[si].field_00 == 0)
            {
                bl = 4;
                bl -= byte_121DC;
                byte_1220C[bl] = 0;
                di = 0;
            }
            mem[si].field_02 = (ushort)di;
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


        private static void sound_copy_sub_13745(int sampleId)
        {
            if (sampleId == 12)
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@"C:\sound_d.wav");
                sp.Play();
            }

            lock (timer_lock)
            {
                playingSampleId = sampleId;
                byte_121DB = true;
                byte_121DC = 4;

                int di = 0;
                int si = sampleId * 4;
                do
                {
                    int ax = word_12562[si];
                    if (ax != 0)
                    {
                        unk_12230[di].Reset();

                        unk_12230[di].field_02 = (ushort)ax;
                        byte_1220C[4 - byte_121DC] = (byte)playingSampleId;
                        unk_12230[di].field_00 = 1;
                    }

                    di += 1;
                    si += 1;
                } while (--byte_121DC != 0);

                byte_121DB = false;
            }
        }


        private static void sound_sub_137B1(/* short arg_0 == 0 */)
        {
            playingSampleId = 0;
            byte_121DB = true;
            byte_121DC = 4;

            int si = 0;
            int di = 0;

            do
            {
                ushort ax = word_1260A[si];
                if (ax != 0)
                {
                    unk_123CA[di].Reset();

                    unk_123CA[di].field_02 = ax;
                    byte_1220C[4 - byte_121DC] = (byte)playingSampleId;
                    unk_123CA[di].field_00 = 1;
                }

                di += 1;
                si += 1;
            } while (--byte_121DC != 0);

            byte_121DB = false;
        }
    }
}
