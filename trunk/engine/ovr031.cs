using Classes;

namespace engine
{
    class ovr031
    {
        internal static void sub_7100F(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            short var_A;
            sbyte var_8;
            sbyte var_7;
            sbyte var_6;
            sbyte var_5;
            sbyte var_4;
            sbyte var_3;
            sbyte var_2;
            sbyte var_1;

            var_5 = (sbyte)(arg_4 - 5);

            if (var_5 < 0)
            {
                var_5 = 0;
            }

            if (var_5 > 5)
            {
                var_5 = 5;
            }

            var_1 = (sbyte)(arg_4 - var_5);

            var_6 = (sbyte)(arg_2 - 5);

            if (var_6 < 0)
            {
                var_6 = 0;
            }

            if (var_6 > 5)
            {
                var_6 = 5;
            }

            var_2 = (sbyte)(arg_2 - var_6);

            for (var_4 = 0; var_4 <= 10; var_4++)
            {
                var_8 = (sbyte)(var_4 + var_6);

                for (var_3 = 0; var_3 <= 10; var_3++)
                {
                    var_7 = (sbyte)(var_3 + var_5);

                    var_A = 0x104;

                    if (sub_716A2(0, var_8, var_7) > 0)
                    {
                        var_A += 1;
                    }

                    if (sub_716A2(2, var_8, var_7) > 0)
                    {
                        var_A += 2;
                    }

                    if (sub_716A2(4, var_8, var_7) > 0)
                    {
                        var_A += 4;
                    }

                    if (sub_716A2(6, var_8, var_7) > 0)
                    {
                        var_A += 8;
                    }

                    ovr038.Put8x8Symbol(0, 1, var_A, (byte)(var_4 + 2), (byte)(var_3 + 2));
                }
            }

            ovr038.Put8x8Symbol(0, 1, (short)((arg_0 >> 1) + 0x100), (byte)(var_2 + 2), (byte)(var_1 + 2));
            seg040.DrawOverlay();
        }


        internal static void sub_71165(byte arg_0, byte arg_2, byte arg_4, byte arg_6)
        {
            gbl.byte_1D534 = arg_6;
            gbl.byte_1D535 = arg_4;
            gbl.byte_1D536 = arg_2;
            gbl.byte_1D537 = arg_0;
        }


        internal static void sub_71184()
        {
            byte var_5;
            byte var_4;
            byte var_3;
            ushort var_2;

            seg040.sub_F6F7(gbl.overlayLines, gbl.unk_188B4, gbl.byte_1D534, 0x2c, 11, 16, 2);
            seg040.sub_F6F7(gbl.overlayLines, gbl.unk_188B4, gbl.unk_188B4[0], 2, 11, 0x3c, 2);
            seg040.sub_F6F7(gbl.overlayLines, gbl.unk_188B4, gbl.byte_1D537, 0x2a, 11, 0x3e, 2);

            if (sub_717A5(gbl.byte_1D53A, gbl.byte_1D53A) < 0x80 &&
                gbl.byte_1D534 == 11)
            {
                var_3 = 2;
                var_4 = 2;
                var_5 = 0x0C;

                var_2 = gbl.area_ptr.field_192;

                if (var_2 >= 1 && var_2 <= 5)
                {

                    if (gbl.byte_1D53B == 2)
                    {
                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + 5) - var_2), (short)(var_5 - 3));
                    }
                    else if (gbl.byte_1D53B == 4 && var_2 > 2)
                    {
                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + 5) - var_2), (short)((var_3 + var_2) - 3));
                    }
                }
                else if (var_2 >= 13 && var_2 <= 18)
                {
                    if (gbl.byte_1D53B == 6)
                    {

                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + var_2) - 13), var_3);
                    }
                    else if (gbl.byte_1D53B == 4 && var_2 >= 16)
                    {

                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + var_2) - 13), (short)((var_3 + var_2) - 8));
                    }
                }

                if (gbl.byte_1D53B == 0)
                {
                    seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_250, 1, 0, var_4, var_3);
                }
            }

            seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_252, 1, 0, 7, 2);
        }

        /*seg600:0ADA*/
        static byte[] unk_16DEA = { 0, 2, 6, 0xA, 0x16, 0x26, 0x36, 0x6E, 0x84, 0x9A, 1 };

        /*seg600:0AE4*/
        static byte[] unk_16DF4 = { 1, 1, 1, 3, 2, 2, 7, 2, 2, 1 };
        /*seg600:0AEE*/
        static byte[] unk_16DFE = { 2, 4, 4, 4, 8, 8, 8, 0xB, 0xB, 2 };
 
        internal static void sub_71434(byte arg_0, byte arg_2, sbyte arg_4, sbyte arg_6)
        {
            byte var_9;
            short var_8;
            short var_6;
            short var_4;
            short var_2;

            var_9 = unk_16DEA[arg_0];

            var_2 = (short)(unk_16DF4[arg_0] + arg_6 - 1);
            var_4 = (short)(unk_16DFE[arg_0] + arg_4 - 1);

            for (var_8 = arg_4; var_8 <= var_4; var_8++)
            {
                for (var_6 = arg_6; var_6 <= var_2; var_6++)
                {
                    if (var_8 >= 0 && var_8 <= 10 && var_6 >= 0 && var_6 <= 10 &&
                        gbl.stru_1D52C[(arg_2 - 1) / 5][var_9 + (((arg_2 - 1) % 5) * 0x9C)] > 0)
                    {
                        ovr038.Put8x8Symbol(1, 1, 
                            gbl.stru_1D52C[(arg_2 - 1) / 5][var_9 + (((arg_2 - 1) % 5) * 0x9C)], (byte)(var_8 + 2), (byte)(var_6 + 2));
                    }

                    var_9++;
                }
            }
        }


        internal static bool sub_71542(sbyte arg_0, sbyte arg_2)
        {
            bool var_1;

            if (arg_2 > 15 ||
                arg_2 < 0 ||
                arg_0 > 15 ||
                arg_0 < 0)
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
            }

            return var_1;
        }


        internal static byte sub_71573(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            byte var_2;
            byte var_1;

            if (sub_71542(arg_2, arg_4) == false &&
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                throw new System.NotSupportedException();//cmp	[bp+arg_4], 0x0F
                throw new System.NotSupportedException();//jle	loc_715AA
                arg_4 = 0;
                throw new System.NotSupportedException();//jmp	short loc_715B4
                throw new System.NotSupportedException();//loc_715AA:
                throw new System.NotSupportedException();//cmp	[bp+arg_4], 0
                throw new System.NotSupportedException();//jge	loc_715B4
                arg_4 = 0x0F;
                throw new System.NotSupportedException();//loc_715B4:
                throw new System.NotSupportedException();//cmp	[bp+arg_2], 0x0F
                throw new System.NotSupportedException();//jle	loc_715C0
                arg_2 = 0;
                throw new System.NotSupportedException();//jmp	short loc_715CA
                throw new System.NotSupportedException();//loc_715C0:
                throw new System.NotSupportedException();//cmp	[bp+arg_2], 0
                throw new System.NotSupportedException();//jge	loc_715CA
                arg_2 = 0x0F;
                throw new System.NotSupportedException();//loc_715CA:
                var_2 = 1;

                if (sub_716A2(arg_0, arg_2, arg_4) > 0)
                {
                    switch (arg_0)
                    {
                        case 6:
                            throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	cl, 4
                            throw new System.NotSupportedException();//shl	ax, cl
                            throw new System.NotSupportedException();//les	di, int ptr stru_1D530.offset
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//add	di, dx
                            throw new System.NotSupportedException();//mov	al, es:[di+300h]
                            throw new System.NotSupportedException();//and	al, 0x0C0
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	cx, 6
                            throw new System.NotSupportedException();//shr	ax, cl
                            throw new System.NotSupportedException();//mov	[bp+var_2], al
                            break;

                        case 4:
                            throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	cl, 4
                            throw new System.NotSupportedException();//shl	ax, cl
                            throw new System.NotSupportedException();//les	di, int ptr stru_1D530.offset
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//add	di, dx
                            throw new System.NotSupportedException();//mov	al, es:[di+300h]
                            throw new System.NotSupportedException();//and	al, 0x30
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	cx, 4
                            throw new System.NotSupportedException();//shr	ax, cl
                            throw new System.NotSupportedException();//mov	[bp+var_2], al
                            break;

                        case 2:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + arg_4 + (arg_2 << 4)] & 0x0c) >> 2);
                            break;

                        case 0:
                            var_2 = (byte)(gbl.stru_1D530[0x300 + arg_4 + (arg_2 << 4)] & 3);
                            break;
                    }
                }

                var_1 = var_2;
            }

            return var_1;
        }


        internal static byte sub_716A2(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            byte var_1;

            if (sub_71542(arg_2, arg_4) == false && (gbl.byte_1EE88 == 0 | gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                if (arg_4 > 0x0F)
                {
                    arg_4 = 0;
                }
                else if (arg_4 < 0)
                {
                    arg_4 = 0x0F;
                }

                if (arg_2 > 0x0F)
                {
                    arg_2 = 0;
                }
                else if (arg_2 < 0)
                {
                    arg_2 = 0x0F;
                }

                switch (arg_0)
                {
                    case 0:
                        var_1 = (byte)((gbl.stru_1D530[arg_4 + (arg_2 << 4)] & 0xf0) >> 4);
                        break;

                    case 2:
                        var_1 = (byte)(gbl.stru_1D530[arg_4 + (arg_2 << 4)] & 0x0f);
                        break;

                    case 4:
                        var_1 = (byte)((gbl.stru_1D530[0x100 + arg_4 + (arg_2 << 4)] & 0xf0) >> 4);
                        break;

                    case 6:
                        var_1 = (byte)(gbl.stru_1D530[0x100 + arg_4 + (arg_2 << 4)] & 0x0f);
                        break;

                    default:
                        var_1 = 0;
                        throw new System.Exception();
                }
            }

            return var_1;
        }


        internal static byte sub_717A5(sbyte arg_0, sbyte arg_2)
        {
            byte var_1;

            if (sub_71542(arg_0, arg_2) == false && (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 0x0A))
            {
                var_1 = 0;
            }
            else
            {
                if (arg_2 > 0x0F)
                {
                    arg_2 = 0;
                }
                if (arg_2 < 0)
                {
                    arg_2 = 0x0F;
                }

                if (arg_0 > 0x0F)
                {
                    arg_0 = 0;
                }
                if (arg_0 < 0)
                {
                    arg_0 = 0x0F;
                }

                var_1 = gbl.stru_1D530[0x200 + arg_2 + (arg_0 << 4)];
            }

            return var_1;
        }


        internal static void sub_71820(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            byte var_17;
            byte var_15;
            byte var_14;
            sbyte var_13;
            short var_12;
            byte var_10;
            short var_F;
            short var_D;
            short var_B;
            short var_9;
            short var_7;
            short var_5;
            byte var_3;
            byte var_2;
            byte var_1;

            if (gbl.byte_1D538 == true)
            {
                sub_7100F(arg_0, arg_2, arg_4);
            }
            else
            {
                sub_71184();

                var_1 = (byte)((arg_0 + 6) % 8);
                var_3 = (byte)((arg_0 + 4) % 8);
                var_2 = (byte)((arg_0 + 2) % 8);

                var_5 = arg_4;
                var_7 = arg_2;
                var_9 = gbl.unk_189A6[arg_0];
                var_B = gbl.unk_189AF[arg_0];
                var_13 = 2;
                var_5 += (short)(var_13 * var_9);
                var_7 += (short)(var_13 * var_B);

                do
                {
                    switch (var_13)
                    {
                        case 2:
                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;
                            var_17 = 0;

                            while (var_10 < 4)
                            {
                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);

                                if (sub_71542((sbyte)var_F, (sbyte)var_D) == false &&
                                    sub_716A2(var_2, (sbyte)var_F, (sbyte)var_D) == 0)
                                {
                                    var_17 = 0;
                                }

                                if (var_14 != 0)
                                {
                                    if (var_17 > 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, (sbyte)(gbl.word_16E1A + var_12 + 1));
                                    }

                                    var_17 = var_14;

                                    sub_71434(0, var_14, gbl.byte_16E1C, (sbyte)(gbl.word_16E08 + var_12));
                                }
                                else
                                {
                                    if (var_17 > 0 &&
                                        sub_716A2(var_1, (sbyte)(var_F - gbl.unk_189AF[var_1]), (sbyte)(var_D - gbl.unk_189A6[var_1])) != 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, (sbyte)(gbl.word_16E1A + var_12 + 1));
                                    }

                                    var_17 = 0;
                                }

                                var_10++;

                                var_D += gbl.unk_189A6[var_1];
                                var_F += gbl.unk_189AF[var_1];
                            }

                            var_D = var_5;
                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;
                            var_17 = 0;

                            while (var_10 < 4)
                            {
                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);

                                if (sub_71542((sbyte)var_F, (sbyte)var_D) == false &&
                                  sub_716A2(var_1, (sbyte)var_F, (sbyte)var_D) == 0)
                                {
                                    var_17 = 0;
                                }

                                if (var_14 != 0)
                                {
                                    if (var_17 > 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, (sbyte)(gbl.word_16E1A + var_12 - 1));
                                    }

                                    var_17 = var_14;
                                    sub_71434(0, var_14, gbl.byte_16E1C, (sbyte)(gbl.word_16E08 + var_12));
                                }
                                else
                                {
                                    if (var_17 > 0 &&
                                        sub_716A2(var_2, (sbyte)(var_F - gbl.unk_189AF[var_2]), (sbyte)(var_D - gbl.unk_189A6[var_2])) != 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, (sbyte)(gbl.word_16E1A + var_12 - 1));
                                    }

                                    var_17 = 0;
                                }

                                var_10++;
                                var_12 += 2;

                                var_D += gbl.unk_189A6[var_2];
                                var_F += gbl.unk_189AF[var_2];
                            }

                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;

                            while (var_10 < 3)
                            {
                                var_15 = sub_716A2(var_1, (sbyte)var_F, (sbyte)var_D);

                                if (var_15 != 0)
                                {
                                    if (var_10 == 0)
                                    {
                                        sub_71434(1, var_15, gbl.byte_16E1E, (sbyte)(gbl.word_16E0A + var_12));
                                    }
                                    else
                                    {
                                        sub_71434(1, var_15, gbl.byte_16E1E, (sbyte)(gbl.word_16E0A + var_12 - 1));
                                    }

                                }

                                var_10++;
                                var_12 -= 2;

                                var_D += gbl.unk_189A6[var_1];
                                var_F += gbl.unk_189AF[var_1];
                            }

                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;

                            while (var_10 < 3)
                            {
                                var_15 = sub_716A2(var_2, (sbyte)var_F, (sbyte)var_D);

                                if (var_15 != 0)
                                {
                                    if (var_10 == 0)
                                    {
                                        sub_71434(2, var_15, gbl.byte_16E20, (sbyte)(gbl.word_16E0C + var_12));
                                    }
                                    else
                                    {
                                        sub_71434(2, var_15, gbl.byte_16E20, (sbyte)(gbl.word_16E0C + var_12 + 1));
                                    }
                                }

                                var_10++;
                                var_12 += 2;

                                var_D += gbl.unk_189A6[var_2];
                                var_F += gbl.unk_189AF[var_2];
                            }

                            break;

                        case 1:
                            var_D = (short)(gbl.unk_189A6[var_1] + var_5);
                            var_D += gbl.unk_189A6[var_1];

                            var_F = (short)(gbl.unk_189AF[var_1] + var_7);
                            var_F += gbl.unk_189AF[var_1];
                            var_10 = 0;
                            var_12 = -6;

                            while (var_10 < 3)
                            {
                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);
                                if (var_14 != 0)
                                {
                                    sub_71434(3, var_14, (sbyte)gbl.byte_16E22, (sbyte)(gbl.word_16E0E + var_12));
                                }

                                var_15 = sub_716A2(var_1, (sbyte)var_F, (sbyte)var_D);
                                if (var_15 != 0)
                                {
                                    sub_71434(4, var_15, (sbyte)gbl.byte_16E24, (sbyte)(gbl.word_16E10 + var_12));
                                }

                                var_10++;
                                var_12 += 3;
                                var_D += gbl.unk_189A6[var_2];
                                var_F += gbl.unk_189AF[var_2];
                            }


                            var_D = (short)(gbl.unk_189A6[var_2] + gbl.unk_189A6[var_2] + var_5);
                            var_F = (short)(gbl.unk_189AF[var_2] + gbl.unk_189AF[var_2] + var_7);
                            var_10 = 0;
                            var_12 = 6;
                            while (var_10 < 3)
                            {
                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);

                                if (var_14 != 0)
                                {
                                    sub_71434(3, var_14, gbl.byte_16E22, (sbyte)(gbl.word_16E0E + var_12));
                                }

                                var_15 = sub_716A2(var_2, (sbyte)var_F, (sbyte)var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(5, var_15, gbl.byte_16E26, (sbyte)(gbl.word_16E12 + var_12));
                                }

                                var_10++;
                                var_12 -= 3;

                                var_D += gbl.unk_189A6[var_1];
                                var_F += gbl.unk_189AF[var_1];
                            }
                            break;

                        case 0:
                            var_D = (short)(gbl.unk_189A6[var_1] + var_5);
                            var_F = (short)(gbl.unk_189AF[var_1] + var_7);
                            var_10 = 0;
                            var_12 = -7;

                            while (var_10 < 2)
                            {
                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);

                                if (var_14 != 0)
                                {
                                    sub_71434(6, var_14, gbl.byte_16E28, (sbyte)(gbl.word_16E14 + var_12));
                                }

                                var_15 = sub_716A2(var_1, (sbyte)var_F, (sbyte)var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(7, var_15, gbl.byte_16E2A, (sbyte)(gbl.word_16E16 + var_12));
                                }

                                var_10++;

                                var_12 += 7;
                                var_D += gbl.unk_189A6[var_2];
                                var_F += gbl.unk_189AF[var_2];
                            }


                            var_D = (short)(var_5 + gbl.unk_189A6[var_2]);
                            var_F = (short)(var_7 + gbl.unk_189AF[var_2]);
                            var_10 = 0;
                            var_12 = 7;

                            while (var_10 < 2)
                            {

                                var_14 = sub_716A2(arg_0, (sbyte)var_F, (sbyte)var_D);

                                if (var_14 != 0)
                                {

                                    sub_71434(6, var_14, (sbyte)gbl.byte_16E28, (sbyte)(var_12 + gbl.word_16E14));
                                }

                                var_15 = sub_716A2(var_2, (sbyte)var_F, (sbyte)var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(8, var_15, (sbyte)gbl.byte_16E2C, (sbyte)(var_12 + gbl.word_16E18));
                                }

                                var_10++;
                                var_12 -= 7;

                                var_D += gbl.unk_189A6[var_1];
                                var_F += gbl.unk_189AF[var_1];
                            }
                            break;
                    }


                    var_5 += gbl.unk_189A6[var_3];
                    var_7 += gbl.unk_189AF[var_3];

                    var_13 -= 1;

                } while (var_13 >= 0);

                seg040.DrawOverlay();
            }
        }

        static Set unk_72005 = new Set(0x0001, new byte[] { 0xE });

        internal static void LoadWalldef(byte arg_0, byte arg_2)
        {
            string var_12;
            byte var_10;
            short var_F;
            short var_D;
            byte var_B;
            short var_A;
            short var_8;
            byte[] var_6;
            short var_2;

            if (arg_0 >= 1 && arg_0 <= 3)
            {
                seg051.Str(1, out var_12, 0, gbl.game_area);

                seg042.load_decode_dax(out var_6, out var_2, arg_2, "WALLDEF" + var_12 + ".dax");

                if (var_2 == 0 ||
                    ((var_2 / 0x30C) + arg_0) > 4)
                {
                    seg051.Write(0, "Unable to load ", gbl.known01_02);
                    seg051.Write(0, arg_2, gbl.known01_02);
                    seg051.Write(0, " from WALLDEF" + var_12 + ".", gbl.known01_02);
                    seg051.WriteLn(gbl.known01_02);

                    seg043.debug_txt();
                    seg043.print_and_exit();
                }

                var_A = (short)(gbl.symbol_set_fix[arg_0] - gbl.symbol_set_fix[1]);

                var_8 = 0;
                var_D = 1;

                do
                {
                    /* seg051.Move(0x30C, gbl.stru_1D52C[((arg_0 + var_D - 1) - 1) * 0x30C], var_6[var_8]); */
                    System.Array.Copy(var_6, var_8, gbl.stru_1D52C[arg_0 + var_D-2], 0, 0x30C);

                    var_8 += 0x30C;
                    var_D += 1;
                } while (var_8 < var_2);

                seg051.FreeMem(var_2, var_6);

                var_10 = (byte)(var_D - 1);

                for (var_D = 1; var_D <= var_10; var_D++)
                {
                    if (unk_72005.MemberOf((byte)((arg_0 + var_D) - 1)) == true)
                    {
                        gbl.wordSetArray_1D53A((arg_0 + var_D - 1), -1);
                        gbl.wordSetArray_1D53C((arg_0 + var_D - 1), -1);

                        for (var_B = 1; var_B <= 5; var_B++)
                        {
                            for (var_F = 0; var_F <= 0x9B; var_F++)
                            {
                                if (gbl.stru_1D52C[arg_0 + var_D - 2][var_F + ((var_B - 1) * 0x9C)] >= gbl.word_1899C)
                                {
                                    gbl.stru_1D52C[arg_0 + var_D - 2][var_F + ((var_B - 1) * 0x9C)] += (byte)var_A;
                                }
                            }
                        }

                        if (var_10 > 1)
                        {
                            ovr038.Load8x8D((byte)(arg_0 + var_D - 1), (byte)((arg_2 * 10) + var_D));
                        }
                        else
                        {
                            ovr038.Load8x8D((byte)(arg_0 + var_D - 1), arg_2);
                        }

                    }
                }

                gbl.wordSetArray_1D53A(arg_0, arg_2);
                gbl.wordSetArray_1D53C(arg_0, arg_0);
            }
        }


        internal static void Load3DMap(byte blockId)
        {
            int var_8;
            byte[] var_6;
            short bytesRead;

            seg042.load_decode_dax(out var_6, out bytesRead, blockId, "GEO" + gbl.game_area.ToString() + ".dax");

            if (bytesRead == 0 || bytesRead != 0x402)
            {
                seg051.Write(0, "Unable to load geo in Load3DMap.", gbl.known01_02);
                seg051.WriteLn(gbl.known01_02);
                seg043.debug_txt();
                seg043.print_and_exit();
            }

            var_8 = 2;

            System.Array.Copy(var_6, var_8, gbl.stru_1D530, 0, 0x100);

            var_8 += 0x100;

            System.Array.Copy(var_6, var_8, gbl.stru_1D530, 0x100, 0x100);

            var_8 += 0x100;

            System.Array.Copy(var_6, var_8, gbl.stru_1D530, 0x200, 0x100);

            var_8 += 0x100;

            System.Array.Copy(var_6, var_8, gbl.stru_1D530, 0x300, 0x100);

            seg051.FreeMem(bytesRead, var_6);

            gbl.area_ptr.field_18A = blockId;
        }
    }
}
