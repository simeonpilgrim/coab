using Classes;

namespace engine
{
    class ovr011
    {
        // the 1 is made up.
        static byte[, , ,] unk_1AB1C = new byte[1, 4, 6, 11]; //seg600:480C unk_1AB1C

        internal static void sub_37046(byte arg_0, byte arg_2, byte arg_4)
        {
            sbyte var_2;
            sbyte var_1;

            var_1 = (sbyte)((gbl.byte_1AD34 * 6) + 21 + (gbl.byte_1AD35 * 5) + arg_4);
            var_2 = (sbyte)((gbl.byte_1AD35 * 5) + 10 + arg_2);

            if (var_1 >= 0 &&
                var_1 <= 0x31 &&
                var_2 >= 0 &&
                var_2 <= 0x18)
            {
                gbl.stru_1D1BC[var_1, var_2] = (byte)(arg_0 + 1);
            }
        }

        static sbyte[] unk_1665B /*seg600:034C*/ = { 0, 1, 0 - 1 };
        static sbyte[] unk_1665F /*seg600:0350*/ = { -1, 0, 1, 0 };

        internal static void sub_370D3()
        {
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            if (gbl.byte_1AD36 != 1 && gbl.byte_1AD38 != 1 &&
                gbl.byte_1AD39 != 1 && gbl.byte_1AD37 != 1)
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.byte_1AD36 == 1 && gbl.byte_1AD39 == 1 &&
                (gbl.byte_1AD38 != 1 || gbl.byte_1AD37 != 1))
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.byte_1AD38 == 1 && gbl.byte_1AD37 == 1 &&
                (gbl.byte_1AD36 != 1 || gbl.byte_1AD39 != 1))
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.byte_1AD36 == 3 || gbl.byte_1AD38 == 3 ||
                gbl.byte_1AD39 == 3 || gbl.byte_1AD37 == 3)
            {
                gbl.byte_1AD3E = 0;
            }
            else
            {
                gbl.byte_1AD3E = 1;
            }

            for (var_1 = 2; var_1 <= 3; var_1++)
            {
                for (var_2 = 2; var_2 <= 4; var_2++)
                {
                    var_3 = (byte)((gbl.byte_1AD34 * 6) + (gbl.byte_1AD35 * 5) + 0x15 + var_1 + var_2);
                    var_4 = (byte)((gbl.byte_1AD35 * 5) + 0x0a + var_2);

                    if (var_3 >= 0 && var_3 <= 0x31 &&
                        var_4 >= 0 && var_4 <= 0x18)
                    {
                        if (gbl.unk_189B4[gbl.stru_1D1BC[var_3, var_4]].field_3 == 0x16 &&
                            gbl.byte_1AD3D != 0 &&
                            gbl.byte_1AD3E != 0 &&
                            ovr024.roll_dice(10, 1) <= 5)
                        {
                            gbl.stru_1D1BC[var_3, var_4] = 0x1A;

                            for (var_7 = 0; var_7 < 4; var_7++)
                            {
                                var_5 = (byte)(unk_1665B[var_7] + var_3);
                                var_6 = (byte)(unk_1665F[var_7] + var_4);

                                if (var_5 >= 0 && var_5 <= 0x31 &&
                                    var_6 >= 0 && var_6 <= 0x18)
                                {
                                    if (gbl.unk_189B4[gbl.stru_1D1BC[var_5, var_6]].field_3 == 0x16 &&
                                        ovr024.roll_dice(10, 1) <= 9)
                                    {
                                        gbl.stru_1D1BC[var_3, var_4] = 0x1B;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        internal static byte sub_37306(byte arg_2, sbyte arg_4, sbyte arg_6)
        {
            byte var_1;

            if (arg_6 >= 0 && arg_6 <= 15 &&
                arg_4 >= 0 && arg_4 <= 15)
            {
                if (ovr031.WallDoorFlagsGet(arg_2, arg_4, arg_6) == 0)
                {
                    var_1 = 1;
                }
                else
                {
                    if (ovr031.sub_716A2(arg_2, arg_4, arg_6) == 0)
                    {
                        var_1 = 0;
                    }
                    else
                    {
                        var_1 = 3;
                    }
                }
            }
            else
            {
                if (arg_4 == gbl.mapPosY &&
                    (arg_2 == 2 || arg_2 == 6))
                {
                    var_1 = 0;
                }
                else
                {
                    var_1 = 1;
                }
            }

            return var_1;
        }


        internal static byte sub_37388(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            var_4 = (byte)((arg_0 + 4) % 8);

            var_2 = sub_37306(arg_0, arg_2, arg_4);

            sbyte t1 = (sbyte)(gbl.MapDirectionXDelta[arg_0] + arg_4);
            sbyte t2 = (sbyte)(gbl.MapDirectionYDelta[arg_0] + arg_2);

            var_3 = sub_37306(var_4, t2, t1);

            var_1 = (byte)(var_2 | var_3);

            return var_1;
        }


        internal static void sub_373FC()
        {
            byte var_2;
            byte var_1;

            for (var_2 = 2; var_2 <= 4; var_2++)
            {
                for (var_1 = 0; var_1 <= 5; var_1++)
                {
                    sub_37046(0x16, var_2, var_1);
                }
            }

            if (gbl.byte_1AD37 == 1)
            {
                for (var_2 = 2; var_2 <= 4; var_2++)
                {
                    sub_37046(4, var_2, (byte)(var_2 - 1));
                    sub_37046(3, var_2, var_2);
                    sub_37046(13, var_2, (byte)(var_2 + 1));
                }
            }
            else if (gbl.byte_1AD37 == 3)
            {
                sub_37046(8, 2, 1);
                sub_37046(0, 4, 5);
            }
        }


        internal static void sub_374A1()
        {
            if (gbl.byte_1AD36 == 1)
            {
                sub_37046(5, 0, 3);
                sub_37046(5, 0, 4);
                sub_37046(10, 1, 3);
                sub_37046(10, 1, 4);
            }
            else
            {
                sub_37046(0x16, 0, 3);
                sub_37046(0x16, 0, 4);
                sub_37046(0x16, 1, 3);
                sub_37046(0x16, 1, 4);
            }
        }


        internal static void sub_3751E(sbyte arg_1, sbyte arg_2)
        {
            byte var_6;
            byte var_5 = 0; /* simeon added */
            byte var_4 = 0; /* simeon added */
            byte var_3 = 0; /* simeon added */
            byte var_2 = 0; /* simeon added */
            byte var_1;

            if (sub_37388(6, (sbyte)(arg_2 - 1), arg_1) == 0 &&
                sub_37388(0, arg_2, (sbyte)(arg_1 - 1)) == 0)
            {
                var_6 = 1;
            }
            else
            {
                var_6 = 0;
            }

            for (var_1 = 1; var_1 <= 4; var_1++)
            {
                if (var_1 == 1)
                {
                    if (gbl.byte_1AD36 == 0)
                    {
                        switch (gbl.byte_1AD37)
                        {
                            case 0:
                                var_2 = 0x16;
                                break;

                            case 3:
                                var_2 = 0x0D;
                                break;

                            case 1:
                                if (var_6 != 0)
                                {
                                    var_2 = 0;
                                }
                                else
                                {
                                    var_2 = 0x0D;
                                }
                                break;
                        }
                    }
                    else if (gbl.byte_1AD36 == 3 || gbl.byte_1AD36 == 1)
                    {
                        if (gbl.byte_1AD37 == 0)
                        {
                            if (var_6 != 0)
                            {
                                var_2 = 0x0F;
                            }
                            else
                            {
                                var_2 = 5;
                            }
                        }
                        else
                        {
                            if (var_6 != 0)
                            {
                                var_2 = 0x12;
                            }
                            else
                            {
                                var_2 = 2;
                            }
                        }
                    }
                }
                else if (var_1 == 2)
                {
                    if (gbl.byte_1AD36 == 0)
                    {
                        var_4 = 0x16;
                    }
                    else if (gbl.byte_1AD36 == 3)
                    {
                        var_4 = 0x11;
                    }
                    else if (gbl.byte_1AD36 == 1)
                    {
                        var_4 = 5;
                    }
                }
                else if (var_1 == 3)
                {
                    throw new System.NotSupportedException();//mov	al, byte_1AD37
                    throw new System.NotSupportedException();//cmp	al, 0
                    throw new System.NotSupportedException();//jnz	loc_37630
                    throw new System.NotSupportedException();//cmp	byte_1AD36, 0
                    throw new System.NotSupportedException();//jnz	loc_3761E
                    var_3 = 0x16;
                    throw new System.NotSupportedException();//jmp	short loc_3762E
                    throw new System.NotSupportedException();//loc_3761E:
                    throw new System.NotSupportedException();//cmp	[bp+var_6], 0
                    throw new System.NotSupportedException();//jz	loc_3762A
                    var_3 = 0x10;
                    throw new System.NotSupportedException();//jmp	short loc_3762E
                    throw new System.NotSupportedException();//loc_3762A:
                    var_3 = 0x0A;
                    throw new System.NotSupportedException();//loc_3762E:
                    throw new System.NotSupportedException();//jmp	short loc_3765A
                    throw new System.NotSupportedException();//loc_37630:
                    throw new System.NotSupportedException();//cmp	al, 3
                    throw new System.NotSupportedException();//jnz	loc_37646
                    throw new System.NotSupportedException();//cmp	[bp+var_6], 0
                    throw new System.NotSupportedException();//jz	loc_37640
                    var_3 = 0x14;
                    throw new System.NotSupportedException();//jmp	short loc_37644
                    throw new System.NotSupportedException();//loc_37640:
                    var_3 = 7;
                    throw new System.NotSupportedException();//loc_37644:
                    throw new System.NotSupportedException();//jmp	short loc_3765A
                    throw new System.NotSupportedException();//loc_37646:
                    throw new System.NotSupportedException();//cmp	al, 1
                    throw new System.NotSupportedException();//jnz	loc_3765A
                    throw new System.NotSupportedException();//cmp	[bp+var_6], 0
                    throw new System.NotSupportedException();//jz	loc_37656
                    var_3 = 1;
                    throw new System.NotSupportedException();//jmp	short loc_3765A
                    throw new System.NotSupportedException();//loc_37656:
                    var_3 = 3;
                    throw new System.NotSupportedException();//loc_3765A:
                    throw new System.NotSupportedException();//jmp	short loc_376AF
                }
                else if (var_1 == 4)
                {
                    if (gbl.byte_1AD37 == 0 || gbl.byte_1AD37 == 3)
                    {
                        if (gbl.byte_1AD36 == 0)
                        {
                            var_5 = 0x16;
                        }
                        else if (gbl.byte_1AD36 == 3)
                        {
                            var_5 = 0x17;
                        }
                        else if (gbl.byte_1AD36 == 1)
                        {
                            var_5 = 0x0A;
                        }
                    }
                    else if (gbl.byte_1AD37 == 1)
                    {
                        if (gbl.byte_1AD36 == 0)
                        {
                            var_5 = 0x0D;
                        }
                        else if (gbl.byte_1AD36 == 3)
                        {
                            var_5 = 0x15;
                        }
                        else if (gbl.byte_1AD36 == 1)
                        {
                            var_5 = 6;
                        }
                    }
                }
            }

            sub_37046(var_2, 0, 1);
            sub_37046(var_4, 0, 2);
            sub_37046(var_3, 1, 1);
            sub_37046(var_5, 1, 2);
        }


        internal static void sub_376F6(sbyte arg_1, sbyte arg_2)
        {
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5 = 0;
            byte var_4 = 0; /* simeon added */
            byte var_3 = 0; /* simeon added */
            byte var_2 = 0; /* simeon added */
            byte var_1;

            var_7 = sub_37388(2, (sbyte)(arg_2 - 1), arg_1);
            var_8 = sub_37388(0, arg_2, (sbyte)(arg_1 + 1));

            if (var_7 == 0 &&
                var_8 == 0)
            {
                var_6 = 1;
            }
            else
            {
                var_6 = 0;
            }

            for (var_1 = 1; var_1 <= 4; var_1++)
            {
                switch (var_1)
                {
                    case 1:
                        throw new System.NotSupportedException();//mov	al, byte_1AD36
                        throw new System.NotSupportedException();//cmp	al, 0
                        throw new System.NotSupportedException();//jnz	loc_37773
                        throw new System.NotSupportedException();//cmp	[bp+var_7], 1
                        throw new System.NotSupportedException();//jnz	loc_3776D
                        var_2 = 4;
                        throw new System.NotSupportedException();//jmp	short loc_37771
                        throw new System.NotSupportedException();//loc_3776D:
                        var_2 = 0x16;
                        throw new System.NotSupportedException();//loc_37771:
                        throw new System.NotSupportedException();//jmp	short loc_37785
                        throw new System.NotSupportedException();//loc_37773:
                        throw new System.NotSupportedException();//cmp	al, 3
                        throw new System.NotSupportedException();//jnz	loc_3777D
                        var_2 = 0x0F;
                        throw new System.NotSupportedException();//jmp	short loc_37785
                        throw new System.NotSupportedException();//loc_3777D:
                        throw new System.NotSupportedException();//cmp	al, 1
                        throw new System.NotSupportedException();//jnz	loc_37785
                        var_2 = 5;
                        throw new System.NotSupportedException();//loc_37785:
                        break;

                    case 2:
                        if (gbl.byte_1AD36 == 0)
                        {
                            throw new System.NotSupportedException();//mov	al, [bp+var_7]
                            throw new System.NotSupportedException();//cmp	al, 0
                            throw new System.NotSupportedException();//jnz	loc_377A3
                            var_4 = 0x16;
                            throw new System.NotSupportedException();//jmp	short loc_377E1
                            throw new System.NotSupportedException();//loc_377A3:
                            throw new System.NotSupportedException();//cmp	al, 3
                            throw new System.NotSupportedException();//jnz	loc_377C0
                            throw new System.NotSupportedException();//cmp	byte_1AD38, 0
                            throw new System.NotSupportedException();//jnz	loc_377BA
                            throw new System.NotSupportedException();//cmp	[bp+var_8], 0
                            throw new System.NotSupportedException();//jz	loc_377BA
                            var_4 = 0x18;
                            throw new System.NotSupportedException();//jmp	short loc_377BE
                            throw new System.NotSupportedException();//loc_377BA:
                            var_4 = 1;
                            throw new System.NotSupportedException();//loc_377BE:
                            throw new System.NotSupportedException();//jmp	short loc_377E1
                            throw new System.NotSupportedException();//loc_377C0:
                            throw new System.NotSupportedException();//cmp	al, 1
                            throw new System.NotSupportedException();//jnz	loc_377E1
                            throw new System.NotSupportedException();//cmp	byte_1AD38, 0
                            throw new System.NotSupportedException();//jnz	loc_377DD
                            throw new System.NotSupportedException();//cmp	[bp+var_8], 0
                            throw new System.NotSupportedException();//jz	loc_377D7
                            var_4 = 0x0B;
                            throw new System.NotSupportedException();//jmp	short loc_377DB
                            throw new System.NotSupportedException();//loc_377D7:
                            var_4 = 7;
                            throw new System.NotSupportedException();//loc_377DB:
                            throw new System.NotSupportedException();//jmp	short loc_377E1
                            throw new System.NotSupportedException();//loc_377DD:
                            var_4 = 3;
                            throw new System.NotSupportedException();//loc_377E1:
                        }
                        else
                        {
                            if (gbl.byte_1AD38 != 0)
                            {
                                var_4 = 9;
                            }
                            else
                            {
                                if (var_8 != 0)
                                {
                                    var_4 = 5;
                                }
                                else
                                {
                                    if (var_6 != 0)
                                    {
                                        var_4 = 0x11;
                                    }
                                    else
                                    {
                                        var_4 = 0x13;
                                    }
                                }
                            }
                        }
                        break;

                    case 3:
                        if (gbl.byte_1AD36 == 0)
                        {
                            var_3 = 0x16;
                        }
                        else if (gbl.byte_1AD36 == 3)
                        {
                            var_3 = 0x10;
                        }
                        else if (gbl.byte_1AD36 == 1)
                        {
                            var_3 = 0x0A;
                        }
                        break;

                    case 4:
                        if (gbl.byte_1AD36 == 0)
                        {
                            if (var_7 == 0)
                            {
                                var_5 = 0x16;
                            }
                            else
                            {

                                if (gbl.byte_1AD38 != 0)
                                {
                                    var_5 = 4;
                                }
                                else
                                {
                                    if (var_8 == 0)
                                    {
                                        var_5 = 8;
                                    }
                                    else
                                    {
                                        var_5 = 0x0C;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (gbl.byte_1AD38 != 0)
                            {
                                var_5 = 0x0E;
                            }
                            else
                            {
                                if (var_8 == 0)
                                {
                                    var_5 = 0x17;
                                }
                                else
                                {
                                    var_5 = 0x0A;
                                }
                            }
                        }
                        break;
                }
            }

            sub_37046(var_2, 0, 5);
            sub_37046(var_4, 0, 6);
            sub_37046(var_3, 1, 5);
            sub_37046(var_5, 1, 6);
        }


        internal static void sub_378CD()
        {
            sbyte var_2;
            sbyte var_1;

            for (gbl.byte_1AD35 = -2; gbl.byte_1AD35 <= 2; gbl.byte_1AD35++)
            {
                for (gbl.byte_1AD34 = -6; gbl.byte_1AD34 <= 6; gbl.byte_1AD34++)
                {
                    var_1 = (sbyte)(gbl.mapPosX + gbl.byte_1AD34);
                    var_2 = (sbyte)(gbl.mapPosY + gbl.byte_1AD35);

                    gbl.byte_1AD37 = sub_37388(6, var_2, var_1);
                    gbl.byte_1AD36 = sub_37388(0, var_2, var_1);
                    gbl.byte_1AD38 = sub_37388(2, var_2, var_1);
                    gbl.byte_1AD39 = sub_37388(4, var_2, var_1);
                    sub_373FC();
                    sub_374A1();
                    sub_3751E(var_1, var_2);
                    sub_376F6(var_1, var_2);
                    gbl.byte_1AD3D = (byte)(ovr031.sub_717A5(var_2, var_1) & 0x40);
                    sub_370D3();
                }
            }
        }

        static byte[] unk_16664 = { 0, 0x18, 0x11, 0x15, 1, 1 }; /* unk_16B8 = seg600:0354 */

        internal static byte sub_37991()
        {
            byte var_1;

            var_1 = unk_16664[gbl.byte_1AD3C];

            return var_1;
        }


        internal static void sub_379AC(sbyte arg_2, sbyte arg_4)
        {
            if (arg_2 < 0x31)
            {
                gbl.stru_1D1BC[arg_2 + 1, arg_4] = 0x40;
            }

            if (arg_4 < 0x18 && arg_2 < 0x31)
            {
                gbl.stru_1D1BC[arg_2 + 1, arg_4 + 1] = 0x41;
            }
        }


        internal static void sub_37A00()
        {
            sbyte var_4;
            sbyte var_3;
            sbyte var_2;
            byte var_1;

            var_1 = 0;

            if ((sub_37991() & 0x20) != 0)
            {
                var_1 = 0x23;
            }

            if ((sub_37991() & 0x10) != 0)
            {
                var_1 = 0x4B;
            }

            if (ovr024.roll_dice(100, 1) <= var_1)
            {
                var_3 = (sbyte)(0x22 - ovr024.roll_dice(4, 5));

                throw new System.NotSupportedException();//loc_37A50:
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//inc	ax
                throw new System.NotSupportedException();//inc	ax
                throw new System.NotSupportedException();//cwd
                throw new System.NotSupportedException();//mov	cx, 7
                throw new System.NotSupportedException();//idiv	cx
                throw new System.NotSupportedException();//xchg	ax, dx
                throw new System.NotSupportedException();//or	ax, ax
                throw new System.NotSupportedException();//jle	loc_37A66
                throw new System.NotSupportedException();//dec	[bp+var_3]
                throw new System.NotSupportedException();//jmp	short loc_37A50
                throw new System.NotSupportedException();//loc_37A66:
                var_2 = var_3;

                for (var_4 = 0; var_4 <= 0x18; var_4++)
                {
                    if (var_3 <= 0x31)
                    {
                        gbl.stru_1D1BC[var_3, var_4] = (byte)(ovr024.roll_dice(2, 1) + 0x3B);

                        if (var_3 < 0x31)
                        {
                            gbl.stru_1D1BC[var_3 + 1, var_4] = (byte)(ovr024.roll_dice(2, 1) + 0x3D);
                        }

                        if (ovr024.roll_dice(20, 1) == 1)
                        {
                            sub_379AC(var_3, var_4);
                        }

                        var_3++;
                    }
                }
            }
        }


        internal static void sub_37B0B()
        {
            sbyte var_4;
            byte var_3;
            byte var_2;

            if ((sub_37991() & 0x80) == 0)
            {
                var_4 = 10;

                if ((sub_37991() & 2) != 0)
                {
                    var_4 -= 5;
                }

                if ((sub_37991() & 4) != 0)
                {
                    var_4 -= 2;
                }

                if ((sub_37991() & 0x40) != 0)
                {
                    var_4 += 5;
                }

                if ((sub_37991() & 8) != 0)
                {
                    var_4 += 10;
                }

                if (var_4 < 0)
                {
                    var_4 = 1;
                }

                for (var_2 = 0; var_2 <= 0x31; var_2++)
                {
                    for (var_3 = 1; var_3 <= 0x18; var_3++)
                    {
                        if (gbl.unk_189B4[gbl.stru_1D1BC[var_2, var_3]].field_3 == 22 &&
                            gbl.unk_189B4[gbl.stru_1D1BC[var_2, var_3 - 1]].field_3 == 22 &&
                            var_4 >= ovr024.roll_dice(100, 1))
                        {
                            if (var_4 >= ovr024.roll_dice(100, 1))
                            {
                                gbl.stru_1D1BC[var_2, var_3] = (byte)(ovr024.roll_dice(2, 1) + 0x29);
                            }
                            else
                            {
                                gbl.stru_1D1BC[var_2, var_3 - 1] = (byte)(ovr024.roll_dice(5, 1) + 0x1F);
                                gbl.stru_1D1BC[var_2, var_3] = (byte)(ovr024.roll_dice(5, 1) + 0x24);
                            }
                        }
                    }
                }
            }
        }


        internal static void sub_37CA2(byte arg_2, byte arg_4, byte arg_6, byte arg_8, byte arg_A, byte arg_C, byte arg_E)
        {
            byte var_1;

            var_1 = ovr024.roll_dice(100, 1);

            if (var_1 <= arg_A)
            {
                gbl.stru_1D1BC[arg_E, arg_C] = (byte)(ovr024.roll_dice(2, 1) + 0x39);
            }
            else
            {
                throw new System.NotSupportedException();//loc_37CEE:
                throw new System.NotSupportedException();//mov	al, [bp+arg_8]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_A]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jg	loc_37D37
                gbl.stru_1D1BC[arg_E, arg_C] = (byte)(ovr024.roll_dice(2, 1) + 0x2f);
                throw new System.NotSupportedException();//jmp	loc_37E44
                throw new System.NotSupportedException();//loc_37D37:
                throw new System.NotSupportedException();//mov	al, [bp+arg_6]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	cx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_8]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_A]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//add	ax, cx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jg	loc_37D89
                gbl.stru_1D1BC[arg_E, arg_C] = (byte)(ovr024.roll_dice(4, 1) + 0x2B);
                throw new System.NotSupportedException();//jmp	loc_37E44
                throw new System.NotSupportedException();//loc_37D89:
                throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	bx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_6]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	cx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_8]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_A]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//add	ax, cx
                throw new System.NotSupportedException();//add	ax, bx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jg	loc_37DE3
                gbl.stru_1D1BC[arg_E, arg_C] = (byte)(ovr024.roll_dice(3, 1) + 0x36);
                throw new System.NotSupportedException();//jmp	short loc_37E44
                throw new System.NotSupportedException();//loc_37DE3:
                throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	bx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_6]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	cx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_8]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_A]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//add	ax, cx
                throw new System.NotSupportedException();//add	ax, bx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jg	loc_37E44
                gbl.stru_1D1BC[arg_E, arg_C] = (byte)(ovr024.roll_dice(4, 1) + 0x31);
            }
            throw new System.NotSupportedException();//loc_37E44:
        }


        internal static void sub_37E4A()
        {
            short var_4;
            byte var_2;
            byte var_1;

            var_4 = 50;

            if ((sub_37991() & 0x10) != 0)
            {
                var_4 += 10;
            }

            if ((sub_37991() & 0x20) != 0)
            {
                var_4 += 30;
            }

            if ((sub_37991() & 0x40) != 0)
            {
                var_4 += 20;
            }

            if ((sub_37991() & 4) != 0)
            {
                var_4 -= 10;
            }

            if ((sub_37991() & 2) != 0)
            {
                var_4 -= 20;
            }

            if ((sub_37991() & 0x80) != 0)
            {
                var_4 -= 50;
            }

            for (var_1 = 0; var_1 <= 49; var_1++)
            {
                for (var_2 = 0; var_2 <= 24; var_2++)
                {
                    if (gbl.unk_189B4[gbl.stru_1D1BC[var_1, var_2]].field_3 == 22)
                    {
                        if (var_4 >= -30 && var_4 <= 9)
                        {
                            sub_37CA2(15, 30, 0, 0, 0, var_2, var_1);
                        }
                        else if (var_4 >= 10 && var_4 <= 29)
                        {
                            sub_37CA2(10, 14, 5, 1, 0, var_2, var_1);
                        }
                        else if (var_4 >= 30 && var_4 <= 69)
                        {
                            sub_37CA2(5, 10, 5, 2, 0, var_2, var_1);
                        }
                        else if (var_4 >= 60 && var_4 <= 89)
                        {
                            sub_37CA2(1, 10, 10, 2, 10, var_2, var_1);
                        }
                        else if (var_4 >= 90 && var_4 <= 110)
                        {
                            sub_37CA2(1, 10, 15, 5, 15, var_2, var_1);
                        }
                    }
                }
            }
        }


        internal static void sub_37FC8()
        {
            byte var_2;
            byte var_1;

            seg051.FillChar(0x17, 0x4E2, gbl.stru_1D1BC.field_7);

            var_1 = gbl.area_ptr.field_186;
            var_2 = gbl.area_ptr.field_188;
            gbl.byte_1AD3C = gbl.area_ptr.field_342;
            sub_37A00();
            sub_37B0B();
            sub_37E4A();
        }


        internal static void sub_38030()
        {
            if (gbl.area_ptr.field_1CC != 0)
            {
                ovr034.Load24x24Set(0x19, 0, 1, "DungCom");
            }
            else
            {
                ovr034.Load24x24Set(0x21, 0, 1, "WildCom");
            }

            ovr034.Load24x24Set(6, 0x22, 1, "RandCom");

            gbl.stru_1D1BC = new Struct_1D1BC();

            gbl.stru_1D1BC.field_4 = 0;
            gbl.stru_1D1BC.field_5 = 1;
            gbl.stru_1D1BC.field_6 = 0;

            if (gbl.area_ptr.field_1CC != 0)
            {
                sub_378CD();
            }
            else
            {
                sub_37FC8();
            }
        }

        /*seg600:02FC*/
        static byte[] unk_1660C = { 7, 2, 3, 6, 5, 4, 5, 6, 3, 8, 7, 2 };


        internal static void sub_380E0()
        {
            byte var_6;
            byte var_5;
            Player player;

            player = gbl.player_next_ptr;

            var_5 = 0;

            while (player != null)
            {
                ovr025.sub_66C20(player);
                var_5++;

                player.actions = new Action();

                if (var_5 > gbl.area2_ptr.field_67C)
                {
                    player.actions.field_13 = 1;
                }

                player.actions.field_9 = unk_1660C[gbl.mapDirection << 1];

                if (player.combat_team == 1)
                {
                    player.actions.field_9 = (byte)((player.actions.field_9 + 4) % 8);
                }

                var_6 = (byte)(player.field_F7 & 0x7f);

                if (player.combat_team == 0)
                {
                    if (player.actions.field_13 == 1)
                    {
                        if (var_6 == 0 ||
                            var_6 > 0x66)
                        {
                            player.field_F7 = (byte)(gbl.area2_ptr.field_58C + 0x80);
                        }
                    }
                }

                player = player.next_player;
            }
        }


        internal static bool sub_38202(int arg_0, int arg_2)
        {
            bool ret_val;

            if ((arg_2 >= 0 && arg_2 <= 11) ||
                (arg_0 >= 0 && arg_0 <= 6))
            {
                ret_val = false;
            }
            else
            {
                ret_val = true;
            }

            return ret_val;
        }


        internal static bool sub_38233(byte arg_0, sbyte arg_2, sbyte arg_4, sbyte arg_6, sbyte arg_8, byte arg_A)
        {
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            bool var_1;

            if (arg_8 < 0 && arg_8 > 10 &&
                arg_6 < 0 && arg_6 > 5 &&
                unk_1AB1C[gbl.field_197, arg_0, arg_6, arg_8] == 0)
            {
                var_1 = false;
            }
            else
            {
                gbl.stru_1C9CD[arg_A].field_0 = (sbyte)(arg_8 + (arg_4 * 6) + (arg_2 * 5) + 22);
                gbl.stru_1C9CD[arg_A].field_1 = (sbyte)(arg_6 + (arg_2 * 5) + 10);

                ovr033.sub_74D04(out var_5, out var_4, out var_3, out var_2, 8, gbl.player_array[arg_A]);

                if (var_2 == 0 &&
                    var_3 > 0 &&
                    gbl.unk_189B4[var_3].field_0 < 0xFF)
                {
                    var_1 = true;
                    unk_1AB1C[gbl.field_197, arg_0, arg_6, arg_8] = 0;
                }
                else
                {
                    var_1 = false;
                }
            }

            return var_1;
        }

        static byte[] /*seg600:02DC*/ unk_165EC = { 8, 4, 6, 2, 8, 6, 4, 0, 8, 0, 6, 2, 8, 2, 0, 4 };
        static byte[] /*seg600:02EC*/ unk_165FC = { 0, 0, 2, 6, 2, 2, 0, 4, 4, 4, 2, 6, 6, 6, 4, 0 };

        static byte[] /*seg600:0300*/ unk_16610 = { 5, 4, 5, 6, 3, 8, 7, 2 };
        static byte[] /*seg600:0308*/ unk_16618 = { 3, 2, 2, 3, 0, 2, 5, 3 };


        internal static byte sub_38380(byte arg_0)
        {
            sbyte var_18 = 0; /* Simeon */
            sbyte var_17 = 0; /* Simeon */
            sbyte var_16 = 0; /* Simeon */
            sbyte var_15 = 0; /* Simeon */
            byte var_14;
            byte var_13 = 0; /* Simeon */
            sbyte var_12;
            sbyte var_11;
            byte var_10 = 0; /* Simeon */
            byte var_F;
            sbyte var_E;
            sbyte var_D;
            sbyte var_C;
            sbyte var_B;
            byte var_A;
            byte var_9;
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            bool var_2 = false; /* Simeon */
            byte var_1;

            var_3 = 1;
            var_4 = 0;
            var_7 = 1;
            var_F = 0;
            var_14 = 0;

            var_11 = gbl.byte_1AD2C[gbl.field_197];
            var_12 = gbl.byte_1AD2E[gbl.field_197];

            do
            {
                int di = (gbl.byte_1AD32[gbl.field_197] << 2) + var_14;
                var_9 = (byte)(unk_165FC[di] >> 1);

                if (var_7 == 1)
                {
                    var_B = gbl.MapDirectionXDelta[unk_1660C[(var_9 + 2) % 4]];
                    var_C = gbl.MapDirectionYDelta[unk_1660C[(var_9 + 2) % 4]];

                    var_15 = (sbyte)(unk_16610[(var_14 > 0 ? 4 : 0) + var_9] + (var_F * var_B));
                    var_16 = (sbyte)(unk_16618[(var_14 > 0 ? 4 : 0) + var_9] + (var_F * var_C));
                    var_17 = var_15;
                    var_18 = var_16;
                    var_10 = 1;
                    var_7 = 2;
                    var_13 = 1;
                }
                else if (var_7 == 2)
                {
                    var_B = gbl.MapDirectionXDelta[unk_1660C[(var_9 + 1) % 4]];
                    var_C = gbl.MapDirectionYDelta[unk_1660C[(var_9 + 1) % 4]];


                    var_17 = (sbyte)(var_15 + (var_B * var_10));
                    var_18 = (sbyte)(var_16 + (var_C * var_10));
                    var_7 = 3;
                    var_13 += 1;
                }
                else if (var_7 == 3)
                {
                    var_B = gbl.MapDirectionXDelta[unk_1660C[(var_9 + 3) % 4]];
                    var_C = gbl.MapDirectionYDelta[unk_1660C[(var_9 + 3) % 4]];

                    var_17 = (sbyte)(var_15 + (var_B * var_10));
                    var_18 = (sbyte)(var_16 + (var_C * var_10));

                    var_7 = 2;
                    var_10++;
                    var_13++;
                }

                if (var_17 < 0 || var_18 < 0 ||
                    var_17 > 10 || var_18 > 5)
                {
                    var_5 = 1;
                }
                else
                {
                    var_5 = 0;
                }


                if (var_7 > 1)
                {
                    if ((var_5 != 0 && sub_38202(var_18, var_17) == false) ||
                        (var_3 != 0 && var_13 >= gbl.unk_1AD30[gbl.field_197]) ||
                        (var_3 == 0 && var_13 > 11))
                    {
                        var_F++;

                        if (gbl.field_197 == 0 &&
                            (gbl.byte_1AD32[0] & 1) == 1 &&
                            var_14 == 0 &&
                            var_F == 1)
                        {
                            var_D = (sbyte)(gbl.byte_1AD2C[gbl.field_197] + gbl.mapPosX);
                            var_E = (sbyte)(gbl.byte_1AD2E[gbl.field_197] + gbl.mapPosY);
                            var_6 = 0;

                            for (var_A = 1; var_A <= 3; var_A++)
                            {
                                var_8 = unk_165EC[gbl.byte_1AD32[gbl.field_197] + var_A];

                                if (gbl.game_state == 3 ||
                                    sub_37388(var_8, var_E, var_D) != 1)
                                {
                                    var_6 = 1;
                                }
                            }

                            if (var_6 != 0)
                            {
                                var_F++;
                            }

                        }
                        var_7 = 1;
                        var_3 = 0;
                    }
                }


                if (var_5 != 0 &&
                    sub_38202(var_18, var_17) == true)
                {
                    var_2 = false;
                    var_7 = 0;

                    while (var_14 < 3 && var_7 != 1)
                    {
                        var_14++;

                        var_D = (sbyte)(gbl.byte_1AD2C[gbl.field_197] + gbl.mapPosX);
                        var_E = (sbyte)(gbl.byte_1AD2E[gbl.field_197] + gbl.mapPosY);

                        var_8 = unk_165EC[gbl.byte_1AD32[gbl.field_197] + var_14];

                        if (gbl.game_state == 3 ||
                            sub_37388(var_8, var_E, var_D) != 1)
                        {
                            var_11 = (sbyte)(gbl.byte_1AD2C[gbl.field_197] + gbl.MapDirectionXDelta[var_8]);
                            var_12 = (sbyte)(gbl.byte_1AD2E[gbl.field_197] + gbl.MapDirectionYDelta[var_8]);

                            var_F = 0;
                            var_7 = 1;
                        }
                    }

                    if (var_7 != 1)
                    {
                        var_4 = 1;
                    }
                }

                if (var_5 == 0)
                {
                    var_2 = sub_38233(var_14, var_12, var_11, var_18, var_17, arg_0);
                }
            } while (var_2 == false && var_4 == 0);

            if (var_4 != 0)
            {
                var_1 = 0;
            }
            else
            {
                var_1 = 1;
            }

            return var_1;
        }


        internal static void sub_387FE()
        {
            byte var_F;
            byte var_E;
            byte var_D;
            byte var_C;
            byte loop_var;
            Player player_ptr;
            Player player_ptr2;
            sbyte var_2;
            sbyte var_1 = 0;

            ovr025.count_teams();
            var_F = 0;
            var_E = 0;

            for (loop_var = 1; loop_var <= 0xff; loop_var++)
            {
                gbl.stru_1C9CD[loop_var].field_3 = 0;
            }

            ovr033.sub_743E7();
            gbl.byte_1AD2C[0] = 0;
            gbl.byte_1AD2E[0] = 0;

            gbl.byte_1AD32[0] = (byte)(gbl.mapDirection >> 1);

            gbl.byte_1AD2C[1] = (sbyte)((gbl.area2_ptr.field_582 * gbl.MapDirectionXDelta[gbl.mapDirection]) + gbl.byte_1AD2C[0]);
            gbl.byte_1AD2E[1] = (sbyte)((gbl.area2_ptr.field_582 * gbl.MapDirectionYDelta[gbl.mapDirection]) + gbl.byte_1AD2E[0]);

            gbl.byte_1AD32[1] = (byte)(((gbl.mapDirection + 4) % 8) >> 1);

            gbl.unk_1AD30[0] = (byte)((gbl.friends_count + 1) / 2);
            gbl.unk_1AD30[1] = (byte)((gbl.foe_count + 1) / 2);

            for (gbl.field_197 = 0; gbl.field_197 < 2; gbl.field_197++)
            {
                for (var_C = 0; var_C < 4; var_C++)
                {
                    if (var_C == 1)
                    {
                        var_D = 4;
                    }
                    else
                    {
                        var_D = gbl.byte_1AD32[gbl.field_197];
                    }

                    for (var_2 = 0; var_2 <= 5; var_2++)
                    {
                        for (var_1 = 0; var_1 <= 10; var_1++)
                        {
                            // the 1 is made up.
                            byte[, ,] unk_16620 = new byte[1, 6, 2]; // unk_16620 seg600:0310

                            if (unk_16620[var_D, var_2, 0] > var_1 ||
                                unk_16620[var_D, var_2, 1] < var_1)
                            {
                                unk_1AB1C[gbl.field_197, var_C, var_2, var_1] = 0;
                            }
                            else
                            {
                                unk_1AB1C[gbl.field_197, var_C, var_2, var_1] = 1;
                            }
                        }
                    }
                }
            }

            loop_var = 1;
            gbl.stru_1C9CD[0].field_3 = 1;
            player_ptr2 = gbl.player_next_ptr;
            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null)
            {
                seg043.clear_one_keypress();

                gbl.player_array[loop_var] = player_ptr;

                gbl.field_197 = player_ptr.combat_team;

                gbl.stru_1C9CD[loop_var].field_2 = loop_var;
                gbl.stru_1C9CD[loop_var].field_3 = (byte)(player_ptr.field_DE & 7);

                if (sub_38380(loop_var) != 0)
                {
                    player_ptr2 = player_ptr;

                    if (player_ptr.in_combat == false)
                    {
                        gbl.stru_1C9CD[loop_var].field_3 = 0;

                        if (gbl.combat_type == 0 &&
                            player_ptr.actions.field_13 == 0)
                        {
                            var_2 = gbl.stru_1C9CD[loop_var].field_0;
                            var_2 = gbl.stru_1C9CD[loop_var].field_1;
                            gbl.byte_1D1BB++;

                            gbl.unk_1D183[gbl.byte_1D1BB].field_6 = gbl.stru_1D1BC[var_1, var_2];
                            gbl.stru_1D1BC[var_1, var_2] = 0x1F;
                            gbl.unk_1D183[gbl.byte_1D1BB].field_0 = player_ptr;
                            gbl.unk_1D183[gbl.byte_1D1BB].field_4 = var_1;
                            gbl.unk_1D183[gbl.byte_1D1BB].field_5 = var_2;
                        }
                    }

                    ovr033.sub_743E7();
                    loop_var++;
                    gbl.stru_1C9CD[0].field_3++;
                    var_E++;
                }
                else
                {
                    gbl.stru_1C9CD[loop_var].field_3 = 0;

                    if (player_ptr.actions.field_13 == 1)
                    {
                        var_F++;

                        gbl.player_array[loop_var] = null;
                        gbl.player_ptr = player_ptr;
                        ovr018.free_players(0, 1);
                        player_ptr2 = player_ptr;
                    }
                    else
                    {
                        player_ptr2 = player_ptr;
                        gbl.stru_1C9CD[0].field_3++;
                    }
                }

                player_ptr = player_ptr2.next_player;
            }

            player_ptr2.next_player = null;
        }


        internal static void battle_begins()
        {
            Player player;

            gbl.byte_1B2F2 = 0;

            ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            seg040.free_dax_block(ref gbl.headX_dax);
            seg040.free_dax_block(ref gbl.bodyX_dax);
            seg040.free_dax_block(ref gbl.word_1D5B6);
            gbl.byte_1D5BA = 0xff;
            gbl.current_head_id = 0xff;
            gbl.current_body_id = 0xff;
            ovr027.redraw_screen();
            seg041.GameDelay();

            seg041.displayString("A battle begins...", 0, 0x0a, 0x18, 0);

            gbl.byte_1D904 = false;
            gbl.byte_1D8B7 = 0;
            gbl.byte_1D8B8 = 0x0F;
            gbl.byte_1D2C9 = 0;
            gbl.byte_1D1BB = 0;

            gbl.stru_1D885 = null;
            gbl.stru_1D889 = null;
            gbl.item_ptr = null;

            gbl.unk_1D183 = new Struct_1D183[8];
            for (int i = 0; i < 8; i++)
            {
                gbl.unk_1D183[i] = new Struct_1D183();
            }

            gbl.area2_ptr.field_666 = 0;

            sub_38030();
            seg043.clear_one_keypress();
            sub_380E0();
            seg043.clear_one_keypress();
            sub_387FE();
            seg043.clear_one_keypress();
            seg040.init_dax_block(out gbl.dword_1D90A, 1, 4, 3, 0x18);

            gbl.stru_1D1BC.field_2 = (sbyte)(ovr033.sub_74C32(gbl.player_next_ptr) - 3);
            gbl.stru_1D1BC.field_3 = (sbyte)(ovr033.sub_74C5A(gbl.player_next_ptr) - 3);

            ovr025.sub_68DC0();
            player = gbl.player_next_ptr;

            while (player != null)
            {
                ovr024.work_on_00(player, 8);
                ovr024.work_on_00(player, 22);

                player = player.next_player;
            }

            ovr014.sub_40E00();
            gbl.game_state = 5;
        }
    }
}
