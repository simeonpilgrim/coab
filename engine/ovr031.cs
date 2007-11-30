using Classes;

namespace engine
{
    class ovr031
    {

        internal static void DrawAreaMap(int partyDir, int partyMapY, int partyMapX)
        { /* sub_7100F */
            const int displayWidth = 11;
            const int halfDisplayWidth = displayWidth / 2;
            const int displayOffset = 2;

            int offsetX = partyMapX - halfDisplayWidth;
            offsetX = System.Math.Max(offsetX, 0);
            offsetX = System.Math.Min(offsetX, halfDisplayWidth);


            int offsetY = partyMapY - halfDisplayWidth;
            offsetY = System.Math.Max(offsetY, 0);
            offsetY = System.Math.Min(offsetY, halfDisplayWidth);


            for (int y = 0; y < displayWidth; y++)
            {
                int mapY = y + offsetY;

                for (int x = 0; x < displayWidth; x++)
                {
                    int mapX = x + offsetX;

                    short var_A = 0x104;

                    if (getMap_XXX(0, mapY, mapX) > 0)
                    {
                        var_A += 1;
                    }

                    if (getMap_XXX(2, mapY, mapX) > 0)
                    {
                        var_A += 2;
                    }

                    if (getMap_XXX(4, mapY, mapX) > 0)
                    {
                        var_A += 4;
                    }

                    if (getMap_XXX(6, mapY, mapX) > 0)
                    {
                        var_A += 8;
                    }

                    ovr038.Put8x8Symbol(0, true, var_A, y + displayOffset, x + displayOffset);
                }
            }

            int partyScreenY = partyMapX - offsetX;
            int partyScreenX = partyMapY - offsetY;

            ovr038.Put8x8Symbol(0, true, (partyDir >> 1) + 0x100, partyScreenX + displayOffset, partyScreenY + displayOffset);
            seg040.DrawOverlay();
        }


        internal static void Draw3dWorldBackground()
        {
            byte var_5;
            byte var_4;
            byte var_3;
            ushort var_2;

            seg040.DrawColorBlock(gbl.byte_1D534, 0x2c, 11, 16, 2);
            seg040.DrawColorBlock(0, 2, 11, 0x3c, 2);
            seg040.DrawColorBlock(8, 0x2a, 11, 0x3e, 2);

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
                        seg040.OverlayBounded(gbl.sky_dax_251, 1, 0, (var_4 + 5) - var_2, var_5 - 3);
                    }
                    else if (gbl.mapDirection == 4 && var_2 > 2)
                    {
                        seg040.OverlayBounded(gbl.sky_dax_251, 1, 0, (var_4 + 5) - var_2, (var_3 + var_2) - 3);
                    }
                }
                else if (var_2 >= 13 && var_2 <= 18)
                {
                    if (gbl.mapDirection == 6)
                    {
                        seg040.OverlayBounded(gbl.sky_dax_251, 1, 0, (var_4 + var_2) - 13, var_3);
                    }
                    else if (gbl.mapDirection == 4 && var_2 >= 16)
                    {
                        seg040.OverlayBounded(gbl.sky_dax_251, 1, 0, (var_4 + var_2) - 13, (var_3 + var_2) - 8);
                    }
                }

                if (gbl.mapDirection == 0)
                {
                    seg040.OverlayBounded(gbl.sky_dax_250, 1, 0, var_4, var_3);
                }
            }

            seg040.OverlayBounded(gbl.sky_dax_252, 1, 0, 7, 2);
        }

        /*seg600:0ADA*/
        static byte[] dataOffset = { 0, 2, 6, 10, 22, 38, 54, 110, 132, 154, 1 };

        /*seg600:0AE4*/
        static int[] colOffsets = { 1, 1, 1, 3, 2, 2, 7, 2, 2, 1 };
        /*seg600:0AEE*/
        static int[] rowOffsets = { 2, 4, 4, 4, 8, 8, 8, 11, 11, 2 };
 
        internal static void sub_71434(int offsetIndex, int arg_2, int rowStart, int colStart)
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
                    int symbolId = gbl.stru_1D52C[offsetA][offsetB + var_9];

                    if (rowY >= 0 && rowY <= 10 && colX >= 0 && colX <= 10 && symbolId > 0)
                    {
                        ovr038.Put8x8Symbol(1, true, symbolId , rowY + 2, colX + 2);

                        Display.Update();
                    }

                    var_9++;
                }
            }
        }

        const int MapSize = 16; // 16x16 so 0-15

        internal static bool MapCoordIsValid(int mapY, int mapX)
        { /*sub_71542*/
            return (mapX < MapSize && mapX >= 0 && mapY < MapSize && mapX >= 0);
        }


        internal static byte WallDoorFlagsGet(int mapDir, int mapY, int mapX) /*sub_71573*/
        {
            byte var_2;
            byte var_1;

            if (MapCoordIsValid(mapY, mapX) == false &&
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                if (mapX > 15)
                {
                    mapX = 0;
                }
                else if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                if (mapY > 15)
                {
                    mapY = 0;
                }
                else if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                var_2 = 1;

                if (getMap_XXX(mapDir, mapY, mapX) > 0)
                {
                    switch (mapDir)
                    {
                        case 6:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapX + (mapY << 4)] & 0xC0) >> 6);
                            break;

                        case 4:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapX + (mapY << 4)] & 0x30) >> 4);
                            break;

                        case 2:
                            var_2 = (byte)((gbl.stru_1D530[0x300 + mapX + (mapY << 4)] & 0x0c) >> 2);
                            break;

                        case 0:
                            var_2 = (byte)(gbl.stru_1D530[0x300 + mapX + (mapY << 4)] & 3);
                            break;
                    }
                }

                var_1 = var_2;
            }

            return var_1;
        }


        internal static byte getMap_XXX(int direction, int mapY, int mapX)
        {
            byte var_1;

            if (MapCoordIsValid(mapY, mapX) == false && 
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 10))
            {
                var_1 = 0;
            }
            else
            {
                if (mapX > 0x0F)
                {
                    mapX = 0;
                }
                else if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                if (mapY > 0x0F)
                {
                    mapY = 0;
                }
                else if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                switch (direction)
                {
                    case 0:
                        var_1 = (byte)((gbl.stru_1D530[mapX + (mapY << 4)] & 0xf0) >> 4);
                        break;

                    case 2:
                        var_1 = (byte)(gbl.stru_1D530[mapX + (mapY << 4)] & 0x0f);
                        break;

                    case 4:
                        var_1 = (byte)((gbl.stru_1D530[0x100 + mapX + (mapY << 4)] & 0xf0) >> 4);
                        break;

                    case 6:
                        var_1 = (byte)(gbl.stru_1D530[0x100 + mapX + (mapY << 4)] & 0x0f);
                        break;

                    default:
                        var_1 = 0;
                        //throw new System.Exception();
                        break;
                }
            }

            return var_1;
        }


        internal static byte sub_717A5(int mapY, int mapX)
        {
            byte var_1;

            if (MapCoordIsValid(mapY, mapX) == false && 
                (gbl.byte_1EE88 == 0 || gbl.byte_1EE88 == 0x0A))
            {
                var_1 = 0;
            }
            else
            {
                if (mapX > 0x0F)
                {
                    mapX = 0;
                }
                if (mapX < 0)
                {
                    mapX = 0x0F;
                }

                if (mapY > 0x0F)
                {
                    mapY = 0;
                }
                if (mapY < 0)
                {
                    mapY = 0x0F;
                }

                var_1 = gbl.stru_1D530[0x200 + mapX + (mapY << 4)];
            }

            return var_1;
        }


        internal static void Draw3dWorld(byte partyDir, int partyPosY, int partyPosX) /* sub_71820 */
        {
            Display.UpdateStop();

            if (gbl.mapAreaDisplay == true)
            {
                DrawAreaMap(partyDir, partyPosY, partyPosX);
            }
            else
            {
                Draw3dWorldBackground();

                int dir_left = (partyDir + 6) % 8;
                int dir_behind = (partyDir + 4) % 8;
                int dir_right = (partyDir + 2) % 8;

                int drawStep = 2;

                int drawX = partyPosX + (drawStep * gbl.MapDirectionXDelta[partyDir]);
                int drawY = partyPosY + (drawStep * gbl.MapDirectionYDelta[partyDir]);

                do
                {
                    switch (drawStep)
                    {
                        case 2:
                            Draw3dWorldFar(partyDir, dir_left, dir_right, drawX, drawY);
                            break;

                        case 1:
                            Draw3dWorldMid(partyDir, dir_left, dir_right, drawX, drawY);
                            break;

                        case 0:
                            Draw3dWorldNear(partyDir, dir_left, dir_right, drawX, drawY);
                            break;
                    }


                    drawX += gbl.MapDirectionXDelta[dir_behind];
                    drawY += gbl.MapDirectionYDelta[dir_behind];

                    drawStep -= 1;

                } while (drawStep >= 0);


                Display.UpdateStart();
                seg040.DrawOverlay();
            }
        }


        private static void Draw3dWorldFar(byte partyDir, int dir_left, int dir_right, int drawX, int drawY)
        {
            int tmpX = drawX;
            int tmpY = drawY;
            int var_10 = 0;
            int var_12 = 0;
            byte var_17 = 0;

            while (var_10 < 4)
            {
                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);

                if (MapCoordIsValid(tmpY, tmpX) == false &&
                    getMap_XXX(dir_right, tmpY, tmpX) == 0)
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
                        getMap_XXX(dir_left, tmpY - gbl.MapDirectionYDelta[dir_left], tmpX - gbl.MapDirectionXDelta[dir_left]) != 0)
                    {
                        //sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 + 1);
                    }

                    var_17 = 0;
                }

                var_10++;
                var_12 -= 2;

                tmpX += gbl.MapDirectionXDelta[dir_left];
                tmpY += gbl.MapDirectionYDelta[dir_left];
            }

            tmpX = drawX;
            tmpY = drawY;
            var_10 = 0;
            var_12 = 0;
            var_17 = 0;

            while (var_10 < 4)
            {
                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);

                if (MapCoordIsValid(tmpY, tmpX) == false &&
                  getMap_XXX(dir_left, tmpY, tmpX) == 0)
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
                        getMap_XXX(dir_right, tmpY - gbl.MapDirectionYDelta[dir_right], tmpX - gbl.MapDirectionXDelta[dir_right]) != 0)
                    {
                        sub_71434(9, var_17, gbl.byte_16E2E, gbl.word_16E1A + var_12 - 1);
                    }

                    var_17 = 0;
                }

                var_10++;
                var_12 += 2;

                tmpX += gbl.MapDirectionXDelta[dir_right];
                tmpY += gbl.MapDirectionYDelta[dir_right];
            }

            tmpX = drawX;
            tmpY = drawY;
            var_10 = 0;
            var_12 = 0;

            while (var_10 < 3)
            {
                byte var_15 = getMap_XXX(dir_left, tmpY, tmpX);

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

                tmpX += gbl.MapDirectionXDelta[dir_left];
                tmpY += gbl.MapDirectionYDelta[dir_left];
            }

            tmpX = drawX;
            tmpY = drawY;
            var_10 = 0;
            var_12 = 0;

            while (var_10 < 3)
            {
                byte var_15 = getMap_XXX(dir_right, tmpY, tmpX);

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

                tmpX += gbl.MapDirectionXDelta[dir_right];
                tmpY += gbl.MapDirectionYDelta[dir_right];
            }
        }


        private static void Draw3dWorldMid(byte partyDir, int dir_left, int dir_right, int var_5, int var_7)
        {
            int tmpX = gbl.MapDirectionXDelta[dir_left] + var_5 + gbl.MapDirectionXDelta[dir_left];
            int tmpY = gbl.MapDirectionYDelta[dir_left] + var_7 + gbl.MapDirectionYDelta[dir_left];
            int var_10 = 0;
            int var_12 = -6;

            while (var_10 < 3)
            {
                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);
                if (var_14 != 0)
                {
                    sub_71434(3, var_14, gbl.byte_16E22, gbl.word_16E0E + var_12);
                }

                byte var_15 = getMap_XXX(dir_left, tmpY, tmpX);
                if (var_15 != 0)
                {
                    sub_71434(4, var_15, gbl.byte_16E24, gbl.word_16E10 + var_12);
                }

                var_10++;
                var_12 += 3;
                tmpX += gbl.MapDirectionXDelta[dir_right];
                tmpY += gbl.MapDirectionYDelta[dir_right];
            }


            tmpX = gbl.MapDirectionXDelta[dir_right] + gbl.MapDirectionXDelta[dir_right] + var_5;
            tmpY = gbl.MapDirectionYDelta[dir_right] + gbl.MapDirectionYDelta[dir_right] + var_7;
            var_10 = 0;
            var_12 = 6;
            while (var_10 < 3)
            {
                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);

                if (var_14 != 0)
                {
                    sub_71434(3, var_14, gbl.byte_16E22, gbl.word_16E0E + var_12);
                }

                byte var_15 = getMap_XXX(dir_right, tmpY, tmpX);

                if (var_15 != 0)
                {
                    sub_71434(5, var_15, gbl.byte_16E26, gbl.word_16E12 + var_12);
                }

                var_10++;
                var_12 -= 3;

                tmpX += gbl.MapDirectionXDelta[dir_left];
                tmpY += gbl.MapDirectionYDelta[dir_left];
            }
        }


        private static void Draw3dWorldNear(byte partyDir, int dir_left, int dir_right, int var_5, int var_7)
        {
            int tmpX = gbl.MapDirectionXDelta[dir_left] + var_5;
            int tmpY = gbl.MapDirectionYDelta[dir_left] + var_7;
            int var_10 = 0;
            int var_12 = -7;

            while (var_10 < 2)
            {
                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);

                if (var_14 != 0)
                {
                    sub_71434(6, var_14, gbl.byte_16E28, gbl.word_16E14 + var_12);
                }

                byte var_15 = getMap_XXX(dir_left, tmpY, tmpX);

                if (var_15 != 0)
                {
                    sub_71434(7, var_15, gbl.byte_16E2A, gbl.word_16E16 + var_12);
                }

                var_10++;

                var_12 += 7;
                tmpX += gbl.MapDirectionXDelta[dir_right];
                tmpY += gbl.MapDirectionYDelta[dir_right];
            }


            tmpX = var_5 + gbl.MapDirectionXDelta[dir_right];
            tmpY = var_7 + gbl.MapDirectionYDelta[dir_right];
            var_10 = 0;
            var_12 = 7;

            while (var_10 < 2)
            {

                byte var_14 = getMap_XXX(partyDir, tmpY, tmpX);

                if (var_14 != 0)
                {

                    sub_71434(6, var_14, gbl.byte_16E28, var_12 + gbl.word_16E14);
                }

                byte var_15 = getMap_XXX(dir_right, tmpY, tmpX);

                if (var_15 != 0)
                {
                    sub_71434(8, var_15, gbl.byte_16E2C, var_12 + gbl.word_16E18);
                }

                var_10++;
                var_12 -= 7;

                tmpX += gbl.MapDirectionXDelta[dir_left];
                tmpY += gbl.MapDirectionYDelta[dir_left];
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

                    seg043.GetInputKey();
                    seg043.print_and_exit();
                }

                var_A = (short)(gbl.symbol_set_fix[arg_0] - gbl.symbol_set_fix[1]);

                var_8 = 0;
                var_D = 1;

                do
                {
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
            byte[] var_6;
            short bytesRead;

            seg042.load_decode_dax(out var_6, out bytesRead, blockId, "GEO" + gbl.game_area.ToString() + ".dax");

            if (bytesRead == 0 || bytesRead != 0x402)
            {
                seg051.Write(0, "Unable to load geo in Load3DMap.", gbl.known01_02);
                seg051.WriteLn(gbl.known01_02);
                seg043.GetInputKey();
                seg043.print_and_exit();
            }

            System.Array.Copy(var_6, 2, gbl.stru_1D530, 0, 0x400);

            seg051.FreeMem(bytesRead, var_6);

            gbl.area_ptr.field_18A = blockId;
        }
    }
}
