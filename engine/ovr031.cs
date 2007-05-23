using Classes;

namespace engine
{
    class ovr031
    {

        internal static void DrawAreaMap(int partyDir, int partyMapX, int partyMapY)
        { /* sub_7100F */
            const int displayWidth = 11;
            const int halfDisplayWidth = displayWidth / 2;
            const int displayOffset = 2;

            int offsetY = partyMapY - halfDisplayWidth;
            offsetY = System.Math.Max(offsetY, 0);
            offsetY = System.Math.Min(offsetY, halfDisplayWidth);


            int offsetX = partyMapX - halfDisplayWidth;
            offsetX = System.Math.Max(offsetX, 0);
            offsetX = System.Math.Min(offsetX, halfDisplayWidth);


            for (int x = 0; x < displayWidth; x++)
            {
                int mapX = (sbyte)(x + offsetX);

                for (int y = 0; y < displayWidth; y++)
                {
                    int mapY = (sbyte)(y + offsetY);

                    short var_A = 0x104;

                    if (sub_716A2(0, mapX, mapY) > 0)
                    {
                        var_A += 1;
                    }

                    if (sub_716A2(2, mapX, mapY) > 0)
                    {
                        var_A += 2;
                    }

                    if (sub_716A2(4, mapX, mapY) > 0)
                    {
                        var_A += 4;
                    }

                    if (sub_716A2(6, mapX, mapY) > 0)
                    {
                        var_A += 8;
                    }

                    ovr038.Put8x8Symbol(0, 1, var_A, x + displayOffset, y + displayOffset);
                }
            }

            int partyScreenY = partyMapY - offsetY;
            int partyScreenX = partyMapX - offsetX;

            ovr038.Put8x8Symbol(0, 1, (short)((partyDir >> 1) + 0x100), partyScreenX + displayOffset, partyScreenY + displayOffset);
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

            if (sub_717A5(gbl.mapPosY, gbl.mapPosY) < 0x80 &&
                gbl.byte_1D534 == 11)
            {
                var_3 = 2;
                var_4 = 2;
                var_5 = 0x0C;

                var_2 = gbl.area_ptr.field_192;

                if (var_2 >= 1 && var_2 <= 5)
                {
                    if (gbl.mapDirection == 2)
                    {
                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + 5) - var_2), (short)(var_5 - 3));
                    }
                    else if (gbl.mapDirection == 4 && var_2 > 2)
                    {
                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + 5) - var_2), (short)((var_3 + var_2) - 3));
                    }
                }
                else if (var_2 >= 13 && var_2 <= 18)
                {
                    if (gbl.mapDirection == 6)
                    {

                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + var_2) - 13), var_3);
                    }
                    else if (gbl.mapDirection == 4 && var_2 >= 16)
                    {

                        seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_251, 1, 0, (short)((var_4 + var_2) - 13), (short)((var_3 + var_2) - 8));
                    }
                }

                if (gbl.mapDirection == 0)
                {
                    seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_250, 1, 0, var_4, var_3);
                }
            }

            seg040.sub_E353(gbl.overlayLines, gbl.sky_dax_252, 1, 0, 7, 2);
        }

        /*seg600:0ADA*/
        static byte[] dataOffset = { 0, 2, 6, 10, 22, 38, 54, 110, 132, 154, 1 };

        /*seg600:0AE4*/
        static int[] colOffsets = { 1, 1, 1, 3, 2, 2, 7, 2, 2, 1 };
        /*seg600:0AEE*/
        static int[] rowOffsets = { 2, 4, 4, 4, 8, 8, 8, 11, 11, 2 };
 
        internal static void sub_71434(byte offsetIndex, byte arg_2, int rowStart, int colStart)
        {
            int var_9 = dataOffset[offsetIndex];

            int colMax = colOffsets[offsetIndex] + colStart;
            int rowMax = rowOffsets[offsetIndex] + rowStart;

            int offsetA = (arg_2 - 1) / 5;
            int offsetB = ((arg_2 - 1) % 5) * 0x9C;

            for (int rowY = rowStart; rowY < rowMax; rowY++)
            {
                for (int colX = colStart; colX < colMax; colX++)
                {
                    short v = gbl.stru_1D52C[offsetA][offsetB + var_9];

                    if (rowY >= 0 && rowY <= 10 && colX >= 0 && colX <= 10 && v > 0)
                    {
                        ovr038.Put8x8Symbol(1, 1, v , rowY + 2, colX + 2);

                        Display.Update();
                    }

                    var_9++;
                }
            }
        }

        const int MapSize = 16; // 16x16 so 0-15

        internal static bool MapCoordIsValid(int mapX, int mapY)
        { /*sub_71542*/
            return (mapY < MapSize && mapY >= 0 && mapX < MapSize && mapY >= 0);
        }


        internal static byte WallDoorFlagsGet(int mapDir, int mapX, int mapY) /*sub_71573*/
        {
            byte var_2;
            byte var_1;

            if (MapCoordIsValid(mapX, mapY) == false &&
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                if (mapY > 15)
                {
                    mapY = 0;
                }
                else if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                if (mapX > 15)
                {
                    mapX = 0;
                }
                else if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                var_2 = 1;

                if (sub_716A2(mapDir, mapX, mapY) > 0)
                {
                    switch (mapDir)
                    {
                        case 6:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapY + (mapX << 4)] & 0xC0) >> 6);
                            break;

                        case 4:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapY + (mapX << 4)] & 0x30) >> 4);
                            break;

                        case 2:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapY + (mapX << 4)] & 0x0c) >> 2);
                            break;

                        case 0:
                            var_2 = (byte)(gbl.stru_1D530[0x300 + mapY + (mapX << 4)] & 3);
                            break;
                    }
                }

                var_1 = var_2;
            }

            return var_1;
        }


        internal static byte sub_716A2(int direction, int mapX, int mapY)
        {
            byte var_1;

            if (MapCoordIsValid(mapX, mapY) == false && 
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                if (mapY > 0x0F)
                {
                    mapY = 0;
                }
                else if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                if (mapX > 0x0F)
                {
                    mapX = 0;
                }
                else if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                switch (direction)
                {
                    case 0:
                        var_1 = (byte)((gbl.stru_1D530[mapY + (mapX << 4)] & 0xf0) >> 4);
                        break;

                    case 2:
                        var_1 = (byte)(gbl.stru_1D530[mapY + (mapX << 4)] & 0x0f);
                        break;

                    case 4:
                        var_1 = (byte)((gbl.stru_1D530[0x100 + mapY + (mapX << 4)] & 0xf0) >> 4);
                        break;

                    case 6:
                        var_1 = (byte)(gbl.stru_1D530[0x100 + mapY + (mapX << 4)] & 0x0f);
                        break;

                    default:
                        var_1 = 0;
                        //throw new System.Exception();
                        break;
                }
            }

            return var_1;
        }


        internal static byte sub_717A5(int mapX, int mapY)
        {
            byte var_1;

            if (MapCoordIsValid(mapX, mapY) == false && 
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 0x0A))
            {
                var_1 = 0;
            }
            else
            {
                if (mapY > 0x0F)
                {
                    mapY = 0;
                }
                if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                if (mapX > 0x0F)
                {
                    mapX = 0;
                }
                if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                var_1 = gbl.stru_1D530[0x200 + mapY + (mapX << 4)];
            }

            return var_1;
        }


        internal static void sub_71820(byte partyDir /*arg_0*/, sbyte partyPosX /*arg_2*/, sbyte partyPosY /*arg_4*/)
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

            if (gbl.mapAreaDisplay == true)
            {
                DrawAreaMap(partyDir, partyPosX, partyPosY);
            }
            else
            {
                sub_71184();

                var_1 = (byte)((partyDir + 6) % 8);
                var_3 = (byte)((partyDir + 4) % 8);
                var_2 = (byte)((partyDir + 2) % 8);

                var_5 = partyPosY;
                var_7 = partyPosX;
                var_9 = gbl.MapDirectionXDelta[partyDir];
                var_B = gbl.MapDirectionYDelta[partyDir];
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
                                var_14 = sub_716A2(partyDir, var_F, var_D);

                                if (MapCoordIsValid(var_F, var_D) == false &&
                                    sub_716A2(var_2, var_F, var_D) == 0)
                                {
                                    var_17 = 0;
                                }

                                if (var_14 != 0)
                                {
                                    if (var_17 > 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 + 1);
                                    }

                                    var_17 = var_14;

                                    sub_71434(0, var_14, gbl.byte_16E1C, gbl.word_16E08 + var_12);
                                }
                                else
                                {
                                    if (var_17 > 0 &&
                                        sub_716A2(var_1, var_F - gbl.MapDirectionYDelta[var_1], var_D - gbl.MapDirectionXDelta[var_1]) != 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 + 1);
                                    }

                                    var_17 = 0;
                                }

                                var_10++;

                                var_D += gbl.MapDirectionXDelta[var_1];
                                var_F += gbl.MapDirectionYDelta[var_1];
                            }

                            var_D = var_5;
                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;
                            var_17 = 0;

                            while (var_10 < 4)
                            {
                                var_14 = sub_716A2(partyDir, var_F, var_D);

                                if (MapCoordIsValid(var_F, var_D) == false &&
                                  sub_716A2(var_1, var_F, var_D) == 0)
                                {
                                    var_17 = 0;
                                }

                                if (var_14 != 0)
                                {
                                    if (var_17 > 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 - 1);
                                    }

                                    var_17 = var_14;
                                    sub_71434(0, var_14, gbl.byte_16E1C, gbl.word_16E08 + var_12);
                                }
                                else
                                {
                                    if (var_17 > 0 &&
                                        sub_716A2(var_2, var_F - gbl.MapDirectionYDelta[var_2], var_D - gbl.MapDirectionXDelta[var_2]) != 0)
                                    {
                                        sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 - 1);
                                    }

                                    var_17 = 0;
                                }

                                var_10++;
                                var_12 += 2;

                                var_D += gbl.MapDirectionXDelta[var_2];
                                var_F += gbl.MapDirectionYDelta[var_2];
                            }

                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;

                            while (var_10 < 3)
                            {
                                var_15 = sub_716A2(var_1, var_F, var_D);

                                if (var_15 != 0)
                                {
                                    if (var_10 == 0)
                                    {
                                        sub_71434(1, var_15, gbl.byte_16E1E, gbl.word_16E0A + var_12);
                                    }
                                    else
                                    {
                                        sub_71434(1, var_15, gbl.byte_16E1E, gbl.word_16E0A + var_12 - 1);
                                    }

                                }

                                var_10++;
                                var_12 -= 2;

                                var_D += gbl.MapDirectionXDelta[var_1];
                                var_F += gbl.MapDirectionYDelta[var_1];
                            }

                            var_D = var_5;
                            var_F = var_7;
                            var_10 = 0;
                            var_12 = 0;

                            while (var_10 < 3)
                            {
                                var_15 = sub_716A2(var_2, var_F, var_D);

                                if (var_15 != 0)
                                {
                                    if (var_10 == 0)
                                    {
                                        sub_71434(2, var_15, gbl.byte_16E20, gbl.word_16E0C + var_12);
                                    }
                                    else
                                    {
                                        sub_71434(2, var_15, gbl.byte_16E20, gbl.word_16E0C + var_12 + 1);
                                    }
                                }

                                var_10++;
                                var_12 += 2;

                                var_D += gbl.MapDirectionXDelta[var_2];
                                var_F += gbl.MapDirectionYDelta[var_2];
                            }

                            break;

                        case 1:
                            var_D = (short)(gbl.MapDirectionXDelta[var_1] + var_5);
                            var_D += gbl.MapDirectionXDelta[var_1];

                            var_F = (short)(gbl.MapDirectionYDelta[var_1] + var_7);
                            var_F += gbl.MapDirectionYDelta[var_1];
                            var_10 = 0;
                            var_12 = -6;

                            while (var_10 < 3)
                            {
                                var_14 = sub_716A2(partyDir, var_F, var_D);
                                if (var_14 != 0)
                                {
                                    sub_71434(3, var_14, gbl.byte_16E22, gbl.word_16E0E + var_12);
                                }

                                var_15 = sub_716A2(var_1, var_F, var_D);
                                if (var_15 != 0)
                                {
                                    sub_71434(4, var_15, gbl.byte_16E24, gbl.word_16E10 + var_12);
                                }

                                var_10++;
                                var_12 += 3;
                                var_D += gbl.MapDirectionXDelta[var_2];
                                var_F += gbl.MapDirectionYDelta[var_2];
                            }


                            var_D = (short)(gbl.MapDirectionXDelta[var_2] + gbl.MapDirectionXDelta[var_2] + var_5);
                            var_F = (short)(gbl.MapDirectionYDelta[var_2] + gbl.MapDirectionYDelta[var_2] + var_7);
                            var_10 = 0;
                            var_12 = 6;
                            while (var_10 < 3)
                            {
                                var_14 = sub_716A2(partyDir, var_F, var_D);

                                if (var_14 != 0)
                                {
                                    sub_71434(3, var_14, gbl.byte_16E22, gbl.word_16E0E + var_12);
                                }

                                var_15 = sub_716A2(var_2, var_F, var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(5, var_15, gbl.byte_16E26, gbl.word_16E12 + var_12);
                                }

                                var_10++;
                                var_12 -= 3;

                                var_D += gbl.MapDirectionXDelta[var_1];
                                var_F += gbl.MapDirectionYDelta[var_1];
                            }
                            break;

                        case 0:
                            var_D = (short)(gbl.MapDirectionXDelta[var_1] + var_5);
                            var_F = (short)(gbl.MapDirectionYDelta[var_1] + var_7);
                            var_10 = 0;
                            var_12 = -7;

                            while (var_10 < 2)
                            {
                                var_14 = sub_716A2(partyDir, var_F, var_D);

                                if (var_14 != 0)
                                {
                                    sub_71434(6, var_14, gbl.byte_16E28, gbl.word_16E14 + var_12);
                                }

                                var_15 = sub_716A2(var_1, var_F, var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(7, var_15, gbl.byte_16E2A, gbl.word_16E16 + var_12);
                                }

                                var_10++;

                                var_12 += 7;
                                var_D += gbl.MapDirectionXDelta[var_2];
                                var_F += gbl.MapDirectionYDelta[var_2];
                            }


                            var_D = (short)(var_5 + gbl.MapDirectionXDelta[var_2]);
                            var_F = (short)(var_7 + gbl.MapDirectionYDelta[var_2]);
                            var_10 = 0;
                            var_12 = 7;

                            while (var_10 < 2)
                            {

                                var_14 = sub_716A2(partyDir, var_F, var_D);

                                if (var_14 != 0)
                                {

                                    sub_71434(6, var_14, gbl.byte_16E28, var_12 + gbl.word_16E14);
                                }

                                var_15 = sub_716A2(var_2, var_F, var_D);

                                if (var_15 != 0)
                                {
                                    sub_71434(8, var_15, gbl.byte_16E2C, var_12 + gbl.word_16E18);
                                }

                                var_10++;
                                var_12 -= 7;

                                var_D += gbl.MapDirectionXDelta[var_1];
                                var_F += gbl.MapDirectionYDelta[var_1];
                            }
                            break;
                    }


                    var_5 += gbl.MapDirectionXDelta[var_3];
                    var_7 += gbl.MapDirectionYDelta[var_3];

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
