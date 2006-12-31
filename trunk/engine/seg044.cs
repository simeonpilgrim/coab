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

        static byte[] byte_1220C = new byte[1];

        static byte byte_12210;
        static short[] unk_12230 = new short[1];

        static short word_123C8;
        static short[] unk_123CA = new short[2];

        static short[] word_12562 = new short[4];

        static short[] word_1260A = new short[4];

        static Set set_01 = new Set(0x0020, new byte[]{ 0xFF, 0xFF, 0 , 0 , 0, 0 ,  0 ,  
            0 , 0 , 0 , 0 ,0 ,0 , 0 ,0 , 0 ,0 , 0 ,0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0x80 });

        internal static void sub_120E0(short arg_0)
        {
            if (set_01.MemberOf((byte)arg_0) == true)
            {
                if (arg_0 == 0)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sub_137B1(0);
                        sub_13745(0);
                    }
                }
                else if (arg_0 == 1)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sub_1381D(1);
                    }
                }
                else if (arg_0 == 0xff)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        sub_137B1(0);
                        sub_13745(0);
                        sub_133B4();
                        gbl.gameFlag01 = false;
                    }
                }
                else if (arg_0 >= 2 && arg_0 <= 14)
                {
                    if (gbl.soundType != SoundType.None)
                    {
                        if (gbl.soundType == 0)
                        {
                            sub_13745((short)(arg_0 - 1));
                        }
                        else if (gbl.soundType == SoundType.Tandy)
                        {
                            sub_137B1((short)(arg_0 - 1));
                        }
                    }
                }
                else if (arg_0 == 15)
                {
                    word_121DE = 0x1388;
                }
            }
        }


        internal static void sub_12194()
        {
            gbl.soundTypeBackup = gbl.soundType;
            gbl.gameFlag01 = true;

            if (gbl.soundType != SoundType.None)
            {
                sub_1337F();
                sub_13745(0);
                sub_137B1(0);
            }
        }

        internal static void sub_121BF()
        {
            word_121DE = 0;
        }


        internal static void sub_1337F()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(sub_133ED);

            // Set the Interval to 5 milliseconds.
            aTimer.Interval = 5;
            aTimer.Enabled = true;

            // Keep the timer alive until the end of Main.
            System.GC.KeepAlive(aTimer);
        }


        internal static void sub_133B4()
        //Complex
        {
            throw new System.NotSupportedException();//push	ds
            throw new System.NotSupportedException();//mov	ax, 0x120C
            throw new System.NotSupportedException();//mov	ds, ax
            throw new System.NotSupportedException();//assume ds:seg044
            throw new System.NotSupportedException();//push	ds
            throw new System.NotSupportedException();//mov	dx, short ptr cs:dword_1337B
            throw new System.NotSupportedException();//mov	ax, short ptr cs:dword_1337B+2
            throw new System.NotSupportedException();//mov	ds, ax
            throw new System.NotSupportedException();//assume ds:seg600
            throw new System.NotSupportedException();//mov	ax, 0x2508
            throw new System.NotSupportedException();//int	0x21
            throw new System.NotSupportedException();//pop	ds
            throw new System.NotSupportedException();//mov	ax, 0x0FFFF
            throw new System.NotSupportedException();//out	0x40, al
            throw new System.NotSupportedException();//mov	al, ah
            throw new System.NotSupportedException();//out	0x40, al
            throw new System.NotSupportedException();//in	al, 0x61
            throw new System.NotSupportedException();//and	al, 0x0FC
            throw new System.NotSupportedException();//out	0x61, al
            throw new System.NotSupportedException();//mov	al, 0x9F
            throw new System.NotSupportedException();//out	0x0C0, al
            throw new System.NotSupportedException();//mov	al, 0x0BF
            throw new System.NotSupportedException();//out	0x0C0, al
            throw new System.NotSupportedException();//mov	al, 0x0DF
            throw new System.NotSupportedException();//out	0x0C0, al
            throw new System.NotSupportedException();//mov	al, 0x0FF
            throw new System.NotSupportedException();//out	0x0C0, al
            throw new System.NotSupportedException();//pop	ds
            throw new System.NotSupportedException();//retf
        }

        /* called with 236.6Hz clock */
        internal static void sub_133ED(object sender, System.Timers.ElapsedEventArgs e)
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
                sub_1347A();
                sub_134EF();
                byte_121DB = 0;
            }
        }


        static void sub_1347A()
        {
            short bx = word_12202;
            if (bx != 0)
            {
                word_123C8 = 0;
                byte_121DC = 4;
                word_12208 = 0x02f0;
                word_12206 = 0x0170;
                short si = word_12206;

                do
                {
                    if (unk_12230[si] != 0)
                    {
                        sub_1357D();
                        if (word_123C8 == 0 &&
                            unk_12230[si + 0x0A] != 0 &&
                            unk_12230[si] != 0)
                        {
                            word_123C8 = (short)(si + 0x170);
                        }
                    }

                    si += 30;
                } while (--byte_121DC != 0);

                if (byte_12210 != 0 &&
                    word_123C8 != 0)
                {
                    si = byte_12210;
                    throw new System.NotSupportedException();//mov	si, bx
                    throw new System.NotSupportedException();//mov	ax, [si+8]
                    throw new System.NotSupportedException();//out	0x42, al
                    throw new System.NotSupportedException();//mov	al, ah
                    throw new System.NotSupportedException();//out	0x42, al
                    throw new System.NotSupportedException();//mov	bx, [si+0Ah]
                    throw new System.NotSupportedException();//and	bl, 3
                }
            }
        }


        static void sub_134EF()
        {
            short bx = word_12202;
            if (bx != 0)
            {
                gbl.byte_1642C = 4;

                word_12208 = 0x48A;
                word_12206 = 0x30A;
                short si = 0;

                throw new System.NotSupportedException();//mov	si, 0x30A
                do
                {
                    if (unk_123CA[si / 2] != 0)
                    {
                        sub_1357D();
                    }

                    si += 0x30;
                } while (--gbl.byte_1642C != 0);

                if (byte_12210 != 0)
                {
                    byte_121DC = 4;

                    si = 0x30A;
                    byte dl = 0x80;
 
                    do
                    {
                        throw new System.NotSupportedException();//loc_1352D:
                        throw new System.NotSupportedException();//mov	ax, [si+8]
                        throw new System.NotSupportedException();//shr	ax, 1
                        throw new System.NotSupportedException();//shr	ax, 1
                        throw new System.NotSupportedException();//shr	al, 1
                        throw new System.NotSupportedException();//shr	al, 1
                        throw new System.NotSupportedException();//shr	al, 1
                        throw new System.NotSupportedException();//shr	al, 1
                        throw new System.NotSupportedException();//xor	al, dl
                        throw new System.NotSupportedException();//out	0x0C0, al
                        throw new System.NotSupportedException();//cmp	dl, 0x0E0
                        throw new System.NotSupportedException();//jz	loc_13549
                        throw new System.NotSupportedException();//mov	al, ah
                        throw new System.NotSupportedException();//out	0x0C0, al
                        throw new System.NotSupportedException();//loc_13549:
                        throw new System.NotSupportedException();//mov	ax, 0x0FFFF
                        throw new System.NotSupportedException();//sub	ax, [si+0Ah]
                        throw new System.NotSupportedException();//shr	ah, 1
                        throw new System.NotSupportedException();//shr	ah, 1
                        throw new System.NotSupportedException();//shr	ah, 1
                        throw new System.NotSupportedException();//shr	ah, 1
                        throw new System.NotSupportedException();//mov	al, ah
                        throw new System.NotSupportedException();//xor	al, dl
                        throw new System.NotSupportedException();//add	al, 0x10
                        throw new System.NotSupportedException();//out	0x0C0, al

                        dl += 0x20;
                        si += 0x30;
                    } while (--gbl.byte_1642C != 0);

                    return;
                }
            }
        }


        static void sub_1357D()
        {
            throw new System.NotSupportedException();//push	cx
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, [si+0Ch]
            throw new System.NotSupportedException();//add	[si+0Ah], ax
            throw new System.NotSupportedException();//mov	ax, [si+6]
            throw new System.NotSupportedException();//add	[si+4],	ax
            throw new System.NotSupportedException();//sub	dx, dx
            throw new System.NotSupportedException();//mov	ax, [si+1Eh]
            throw new System.NotSupportedException();//add	ax, [si+20h]
            throw new System.NotSupportedException();//jz	loc_135B0
            throw new System.NotSupportedException();//cmp	ax, [si+24h]
            throw new System.NotSupportedException();//jb	loc_1359D
            throw new System.NotSupportedException();//sub	ax, [si+24h]
            throw new System.NotSupportedException();//loc_1359D:
            throw new System.NotSupportedException();//mov	[si+1Eh], ax
            throw new System.NotSupportedException();//mov	cl, 4
            throw new System.NotSupportedException();//sar	ax, cl
            throw new System.NotSupportedException();//add	ax, [si+1Ch]
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	ah, [di]
            throw new System.NotSupportedException();//mov	al, 0
            throw new System.NotSupportedException();//imul	short ptr [si+22h]
            throw new System.NotSupportedException();//loc_135B0:
            throw new System.NotSupportedException();//add	dx, [si+4]
            throw new System.NotSupportedException();//mov	[si+8],	dx
            throw new System.NotSupportedException();//cmp	short ptr [si+14h], 0
            throw new System.NotSupportedException();//jz	loc_135CB
            throw new System.NotSupportedException();//dec	short ptr [si+14h]
            throw new System.NotSupportedException();//jnz	loc_135CB
            throw new System.NotSupportedException();//mov	short ptr [si+18h], 0x10
            throw new System.NotSupportedException();//mov	short ptr [si+1Ah], 1
            throw new System.NotSupportedException();//loc_135CB:
            throw new System.NotSupportedException();//dec	short ptr [si]
            throw new System.NotSupportedException();//jnz	loc_135D2
            throw new System.NotSupportedException();//call	sub_1360E
            throw new System.NotSupportedException();//loc_135D2:
            throw new System.NotSupportedException();//cmp	short ptr [si+1Ah], 0
            throw new System.NotSupportedException();//jz	loc_1360B
            throw new System.NotSupportedException();//dec	short ptr [si+1Ah]
            throw new System.NotSupportedException();//jnz	loc_1360B
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
            throw new System.NotSupportedException();//loc_1360B:
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	cx
            throw new System.NotSupportedException();//retn
        }


        static void sub_1360E()
        {
            throw new System.NotSupportedException();//mov	di, [si+2]
            throw new System.NotSupportedException();//cmp	di, 0
            throw new System.NotSupportedException();//jnz	loc_13617
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//loc_13617:
            throw new System.NotSupportedException();//mov	ds:word_12204, si
            throw new System.NotSupportedException();//loc_1361B:
            throw new System.NotSupportedException();//mov	bl, [di]
            throw new System.NotSupportedException();//inc	di
            throw new System.NotSupportedException();//cmp	bl, 0x0FA
            throw new System.NotSupportedException();//jnb	loc_13626
            throw new System.NotSupportedException();//jmp	loc_136C7
            throw new System.NotSupportedException();//loc_13626:
            throw new System.NotSupportedException();//sub	bh, bh
            throw new System.NotSupportedException();//sub	bl, 0x0FA
            throw new System.NotSupportedException();//add	bl, bl
            throw new System.NotSupportedException();//jmp	short ptr [bx+164h]
            throw new System.NotSupportedException();//db  0x8A
            throw new System.NotSupportedException();//db  0x1D
            throw new System.NotSupportedException();//db  0x47
            throw new System.NotSupportedException();//db 0x0B7
            throw new System.NotSupportedException();//db    0
            throw new System.NotSupportedException();//db  0x8B
            throw new System.NotSupportedException();//db    5
            throw new System.NotSupportedException();//db  0x83
            throw new System.NotSupportedException();//db 0x0C7
            throw new System.NotSupportedException();//db    2
            throw new System.NotSupportedException();//db  0x83
            throw new System.NotSupportedException();//db  0x38
            throw new System.NotSupportedException();//db    0
            throw new System.NotSupportedException();//db  0x74
            throw new System.NotSupportedException();//db    4
            throw new System.NotSupportedException();//db 0x0FF
            throw new System.NotSupportedException();//db    8
            throw new System.NotSupportedException();//db  0x74
            throw new System.NotSupportedException();//db 0x0D7
            throw new System.NotSupportedException();//db  0x8B
            throw new System.NotSupportedException();//db 0x0F8
            throw new System.NotSupportedException();//db 0x0EB
            throw new System.NotSupportedException();//db 0x0D3
            throw new System.NotSupportedException();//db  0x8B
            throw new System.NotSupportedException();//db  0x35
            throw new System.NotSupportedException();//db  0x83
            throw new System.NotSupportedException();//db 0x0C7
            throw new System.NotSupportedException();//db    2
            throw new System.NotSupportedException();//db    3
            throw new System.NotSupportedException();//db  0x36
            throw new System.NotSupportedException();//db  0x46
            throw new System.NotSupportedException();//db    1
            throw new System.NotSupportedException();//db  0x2B
            throw new System.NotSupportedException();//db 0x0DB
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db    4
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db    6
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db    8
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x0A
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x0C
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x10
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x12
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x16
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x18
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x1A
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x1C
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x1E
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x20
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x22
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db  0x5C
            throw new System.NotSupportedException();//db  0x24
            throw new System.NotSupportedException();//db 0x0EB
            throw new System.NotSupportedException();//db  0x99
            throw new System.NotSupportedException();//db  0x8A
            throw new System.NotSupportedException();//db  0x1D
            throw new System.NotSupportedException();//db  0x47
            throw new System.NotSupportedException();//db 0x0B7
            throw new System.NotSupportedException();//db    0
            throw new System.NotSupportedException();//db  0x8B
            throw new System.NotSupportedException();//db    5
            throw new System.NotSupportedException();//db  0x47
            throw new System.NotSupportedException();//db  0x47
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//db    0
            throw new System.NotSupportedException();//db  0x80
            throw new System.NotSupportedException();//db 0x0FB
            throw new System.NotSupportedException();//db    0
            throw new System.NotSupportedException();//db  0x75
            throw new System.NotSupportedException();//db  0x89
            throw new System.NotSupportedException();//loc_13692:
            throw new System.NotSupportedException();//mov	si, ds:word_12204
            throw new System.NotSupportedException();//cmp	short ptr [si], 0
            throw new System.NotSupportedException();//jnz	loc_136AB
            throw new System.NotSupportedException();//mov	bl, 4
            throw new System.NotSupportedException();//sub	bl, byte_1642C
            throw new System.NotSupportedException();//sub	bh, bh
            throw new System.NotSupportedException();//mov	byte ptr [bx+14Ch], 0
            throw new System.NotSupportedException();//mov	di, 0
            throw new System.NotSupportedException();//loc_136AB:
            throw new System.NotSupportedException();//mov	[si+2],	di
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//mov	di, short ptr byte_1645A
            throw new System.NotSupportedException();//jmp	loc_1361B
            throw new System.NotSupportedException();//mov	ax, [di]
            throw new System.NotSupportedException();//add	di, 2
            throw new System.NotSupportedException();//mov	short ptr byte_1645A, di
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//jmp	loc_1361B
            throw new System.NotSupportedException();//loc_136C4:
            throw new System.NotSupportedException();//mov	bl, [di]
            throw new System.NotSupportedException();//inc	di
            throw new System.NotSupportedException();//loc_136C7:
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
            throw new System.NotSupportedException();//mov	al, [di]
            throw new System.NotSupportedException();//inc	di
            throw new System.NotSupportedException();//inc	di
            throw new System.NotSupportedException();//and	al, 0x80
            throw new System.NotSupportedException();//jnz	loc_13742
            throw new System.NotSupportedException();//jmp	short loc_136C4
            throw new System.NotSupportedException();//loc_13742:
            throw new System.NotSupportedException();//jmp	loc_13692
        }


        internal static void sub_13745(short arg_0)
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
                    for (int bx = 0x2E; bx >= 0; bx -= 2)
                    {
                        unk_12230[(bx + di) / 2] = 0;
                    }

                    unk_12230[(di + 2) / 2] = (short)ax;

                    byte_1220C[4 - (byte)byte_121DC] = (byte)word_12202;
                    unk_12230[di / 2] = 1;
                }

                di += 0x30;
                si += 1;
            } while (--byte_121DC != 0);

            byte_121DB = 0;
        }


        internal static void sub_137B1(short arg_0)
        {

            word_12202 = arg_0;
            byte_121DB = 1;
            byte_121DC = 4;

            short si = (short)(arg_0 * 8);
            short di = 0;

            do
            {
                short ax = word_1260A[si / 2];
                if (ax != 0)
                {
                    short bx = 0x2E;
                    do
                    {
                        unk_123CA[(bx + di) / 2] = 0;
                        bx -= 2;
                    } while (bx >= 0);

                    unk_123CA[(di + 2) / 2] = ax;

                    byte_1220C[4 - byte_121DC] = (byte)word_12202;

                    unk_123CA[di / 2] = 1;
                }

                di += 0x30;
                si += 2;
            } while (--byte_121DC != 0);

            byte_121DB = 0;
        }


        internal static void sub_1381D(byte arg_0)
        {
            byte_12210 = arg_0;
        }
    }
}
