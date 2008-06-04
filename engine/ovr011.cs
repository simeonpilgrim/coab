using Classes;

namespace engine
{
    class ovr011
    {
        // the 2 is made up.
        static byte[, , ,] unk_1AB1C = new byte[2, 4, 6, 11]; //seg600:480C unk_1AB1C

        internal static void set_background_tile(int tileId, int y, int x) /* sub_37046 */
        {
            int tmpX = (gbl.byte_1AD34 * 6) + (gbl.byte_1AD35 * 5) + 21 + x;
            int tmpY = (gbl.byte_1AD35 * 5) + 10 + y;

            if (tmpX >= 0 &&
                tmpX <= 0x31 &&
                tmpY >= 0 &&
                tmpY <= 0x18)
            {
                gbl.mapToBackGroundTile[tmpX, tmpY] = tileId + 1;
            }
        }

        static int[] dir_x_offset /*seg600:034C unk_1665B*/ = { 0, 1, 0, -1 };
        static int[] dir_y_offset /*seg600:0350 unk_1665F*/ = { -1, 0, 1, 0 };

        internal static void sub_370D3()
        {
            if (gbl.dir_0_flags != 1 && gbl.dir_2_flags != 1 &&
                gbl.dir_4_flags != 1 && gbl.dir_6_flags != 1)
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.dir_0_flags == 1 && gbl.dir_4_flags == 1 &&
                (gbl.dir_2_flags != 1 || gbl.dir_6_flags != 1))
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.dir_2_flags == 1 && gbl.dir_6_flags == 1 &&
                (gbl.dir_0_flags != 1 || gbl.dir_4_flags != 1))
            {
                gbl.byte_1AD3E = 0;
            }
            else if (gbl.dir_0_flags == 3 || gbl.dir_2_flags == 3 ||
                gbl.dir_4_flags == 3 || gbl.dir_6_flags == 3)
            {
                gbl.byte_1AD3E = 0;
            }
            else
            {
                gbl.byte_1AD3E = 1;
            }

            for (int var_1 = 2; var_1 <= 3; var_1++)
            {
                for (int var_2 = 2; var_2 <= 4; var_2++)
                {
                    int posX = (gbl.byte_1AD34 * 6) + (gbl.byte_1AD35 * 5) + 0x15 + var_1 + var_2;
                    int posY = (gbl.byte_1AD35 * 5) + 0x0a + var_2;

                    if (posX >= 0 && posX <= 0x31 &&
                        posY >= 0 && posY <= 0x18)
                    {
                        if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[posX, posY]].tile_index == 0x16 &&
                            gbl.byte_1AD3D != 0 &&
                            gbl.byte_1AD3E != 0 &&
                            ovr024.roll_dice(10, 1) <= 5)
                        {
                            gbl.mapToBackGroundTile[posX, posY] = 0x1A; // Table

                            for (int var_7 = 0; var_7 < 4; var_7++)
                            {
                                int tmpX = dir_x_offset[var_7] + posX;
                                int tmpY = dir_y_offset[var_7] + posY;

                                if (tmpX >= 0 && tmpX <= 0x31 &&
                                    tmpY >= 0 && tmpY <= 0x18)
                                {
                                    if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[tmpX, tmpY]].tile_index == 0x16 &&
                                        ovr024.roll_dice(10, 1) <= 9)
                                    {
                                        gbl.mapToBackGroundTile[posX, posY] = 0x1B; // Chair
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        internal static int sub_37306(int dir, int mapY, int mapX)
        {
            int flags;

            if (mapX >= 0 && mapX <= 15 &&
                mapY >= 0 && mapY <= 15)
            {
                if (ovr031.WallDoorFlagsGet(dir, mapY, mapX) == 0)
                {
                    flags = 1;
                }
                else if (ovr031.getMap_XXX(dir, mapY, mapX) == 0)
                {
                    flags = 0;
                }
                else
                {
                    flags = 3;
                }
            }
            else
            {
                if (mapY == gbl.mapPosY &&
                    (dir == 2 || dir == 6))
                {
                    flags = 0;
                }
                else
                {
                    flags = 1;
                }
            }

            return flags;
        }


        /// <summary>
        /// result can be 0, 1, 3
        /// </summary>
        internal static int get_dir_flags(int dir, int mapX, int mapY) /* sub_37388 */
        {
            int oppositeDir = (dir + 4) % 8;
            int newMapY = gbl.MapDirectionXDelta[dir] + mapY;
            int newMapX = gbl.MapDirectionYDelta[dir] + mapX;

            int var_2 = sub_37306(dir, mapX, mapY);
            int var_3 = sub_37306(oppositeDir, newMapX, newMapY);

            return (var_2 | var_3);
        }


        internal static void build_background_tiles_1() /* sub_373FC */
        {
            for (int var_2 = 2; var_2 <= 4; var_2++)
            {
                for (int var_1 = 0; var_1 <= 5; var_1++)
                {
                    set_background_tile(0x16, var_2, var_1);
                }
            }

            if (gbl.dir_6_flags == 1)
            {
                for (int var_2 = 2; var_2 <= 4; var_2++)
                {
                    set_background_tile(4, var_2, var_2 - 1);
                    set_background_tile(3, var_2, var_2);
                    set_background_tile(13, var_2, var_2 + 1);
                }
            }
            else if (gbl.dir_6_flags == 3)
            {
                set_background_tile(8, 2, 1);
                set_background_tile(0, 4, 5);
            }
        }


        internal static void build_background_tiles_2() /* sub_374A1 */
        {
            if (gbl.dir_0_flags == 1)
            {
                set_background_tile(5, 0, 3);
                set_background_tile(5, 0, 4);
                set_background_tile(10, 1, 3);
                set_background_tile(10, 1, 4);
            }
            else
            {
                set_background_tile(0x16, 0, 3);
                set_background_tile(0x16, 0, 4);
                set_background_tile(0x16, 1, 3);
                set_background_tile(0x16, 1, 4);
            }
        }


        internal static void build_backgrould_tiles_3(int mapX, int mapY) /* sub_3751E */
        {
            byte var_5 = 0; /* simeon added */
            byte var_4 = 0; /* simeon added */
            byte var_3 = 0; /* simeon added */
            byte var_2 = 0; /* simeon added */
            byte var_1;

            bool var_6 = (get_dir_flags(6, mapY - 1, mapX) == 0 &&
                            get_dir_flags(0, mapY, mapX - 1) == 0);

            for (var_1 = 1; var_1 <= 4; var_1++)
            {
                if (var_1 == 1)
                {
                    if (gbl.dir_0_flags == 0)
                    {
                        switch (gbl.dir_6_flags)
                        {
                            case 0:
                                var_2 = 0x16;
                                break;

                            case 3:
                                var_2 = 0x0D;
                                break;

                            case 1:
                                if (var_6 == true)
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
                    else if (gbl.dir_0_flags == 3 || gbl.dir_0_flags == 1)
                    {
                        if (gbl.dir_6_flags == 0)
                        {
                            if (var_6 == true)
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
                            if (var_6 == true)
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
                    if (gbl.dir_0_flags == 0)
                    {
                        var_4 = 0x16;
                    }
                    else if (gbl.dir_0_flags == 3)
                    {
                        var_4 = 0x11;
                    }
                    else if (gbl.dir_0_flags == 1)
                    {
                        var_4 = 5;
                    }
                }
                else if (var_1 == 3)
                {
                    switch (gbl.dir_6_flags)
                    {
                        case 0:
                            if (gbl.dir_0_flags == 0)
                            {
                                var_3 = 0x16;
                            }
                            else if (var_6 == true)
                            {
                                var_3 = 0x10;
                            }
                            else
                            {
                                var_3 = 0x0A;
                            }
                            break;

                        case 3:
                            if (var_6 == true)
                            {
                                var_3 = 0x14;
                            }
                            else
                            {
                                var_3 = 7;
                            }
                            break;

                        case 1:
                            if (var_6 == true)
                            {
                                var_3 = 1;
                            }
                            else
                            {
                                var_3 = 3;
                            }
                            break;
                    }
                }
                else if (var_1 == 4)
                {
                    if (gbl.dir_6_flags == 0 || gbl.dir_6_flags == 3)
                    {
                        if (gbl.dir_0_flags == 0)
                        {
                            var_5 = 0x16;
                        }
                        else if (gbl.dir_0_flags == 3)
                        {
                            var_5 = 0x17;
                        }
                        else if (gbl.dir_0_flags == 1)
                        {
                            var_5 = 0x0A;
                        }
                    }
                    else if (gbl.dir_6_flags == 1)
                    {
                        if (gbl.dir_0_flags == 0)
                        {
                            var_5 = 0x0D;
                        }
                        else if (gbl.dir_0_flags == 3)
                        {
                            var_5 = 0x15;
                        }
                        else if (gbl.dir_0_flags == 1)
                        {
                            var_5 = 6;
                        }
                    }
                }
            }

            set_background_tile(var_2, 0, 1);
            set_background_tile(var_4, 0, 2);
            set_background_tile(var_3, 1, 1);
            set_background_tile(var_5, 1, 2);
        }


        internal static void build_background_tiles_4(int mapX, int mapY) /* sub_376F6 */
        {
            byte var_5;
            byte var_4;
            byte var_3; 
            byte var_2;

            int var_7 = get_dir_flags(2, mapY - 1, mapX);
            int var_8 = get_dir_flags(0, mapY, mapX + 1);

            bool var_6 = (var_7 == 0 && var_8 == 0);

            //case 1:
            if (gbl.dir_0_flags == 0)
            {
                if (var_7 == 1)
                {
                    var_2 = 4; // bottom of north-south wall
                }
                else
                {
                    var_2 = 0x16; // bottom west edge of west-east wall
                }
            }
            else if (gbl.dir_0_flags == 3)
            {
                var_2 = 0x0F; // top west edge of west-east wall
            }
            else // gbl.dir_0_flags == 1
            {
                var_2 = 5; // top of east-west wall
            }

            //case 2:
            if (gbl.dir_0_flags == 0)
            {
                if (var_7 == 0)
                {
                    var_4 = 0x16; // bottow west edge of west-east wall
                }
                else if (var_7 == 3)
                {
                    if (gbl.dir_2_flags == 0 && var_8 != 0)
                    {
                        var_4 = 0x18;
                    }
                    else
                    {
                        var_4 = 1;
                    }
                }
                else // var_7 == 1 
                {
                    if (gbl.dir_2_flags == 0)
                    {
                        if (var_8 != 0)
                        {
                            var_4 = 0x0B;
                        }
                        else
                        {
                            var_4 = 7;
                        }
                    }
                    else
                    {
                        var_4 = 3;
                    }
                }
            }
            else if (gbl.dir_2_flags != 0)
            {
                var_4 = 9;
            }
            else if (var_8 != 0)
            {
                var_4 = 5;
            }
            else if (var_6 == true)
            {
                var_4 = 0x11;
            }
            else
            {
                var_4 = 0x13;
            }

            //case 3:
            if (gbl.dir_0_flags == 0)
            {
                var_3 = 0x16;
            }
            else if (gbl.dir_0_flags == 3)
            {
                var_3 = 0x10;
            }
            else // gbl.dir_0_flags == 1
            {
                var_3 = 0x0A;
            }

            //case 4:
            if (gbl.dir_0_flags == 0)
            {
                if (var_7 == 0)
                {
                    var_5 = 0x16;
                }
                else if (gbl.dir_2_flags != 0)
                {
                    var_5 = 4;
                }
                else if (var_8 == 0)
                {
                    var_5 = 8;
                }
                else
                {
                    var_5 = 0x0C;
                }
            }
            else if (gbl.dir_2_flags != 0)
            {
                var_5 = 0x0E;
            }
            else if (var_8 == 0)
            {
                var_5 = 0x17;
            }
            else
            {
                var_5 = 0x0A;
            }

            set_background_tile(var_2, 0, 5);
            set_background_tile(var_4, 0, 6);
            set_background_tile(var_3, 1, 5);
            set_background_tile(var_5, 1, 6);
        }


        internal static void sub_378CD()
        {
            for (gbl.byte_1AD35 = -2; gbl.byte_1AD35 <= 2; gbl.byte_1AD35++)
            {
                for (gbl.byte_1AD34 = -6; gbl.byte_1AD34 <= 6; gbl.byte_1AD34++)
                {
                    int mapX = gbl.mapPosX + gbl.byte_1AD34;
                    int mapY = gbl.mapPosY + gbl.byte_1AD35;

                    gbl.dir_0_flags = get_dir_flags(0, mapY, mapX);
                    gbl.dir_6_flags = get_dir_flags(6, mapY, mapX);
                    gbl.dir_2_flags = get_dir_flags(2, mapY, mapX);
                    gbl.dir_4_flags = get_dir_flags(4, mapY, mapX);

                    build_background_tiles_1();
                    build_background_tiles_2();
                    build_backgrould_tiles_3(mapX, mapY);
                    build_background_tiles_4(mapX, mapY);
                    gbl.byte_1AD3D = (byte)(ovr031.sub_717A5(mapY, mapX) & 0x40);
                    sub_370D3();
                }
            }
        }

        static byte[] unk_16664 = { /* unk_16664 seg600:0354 */
            0x00, 0x18, 0x11, 0x15, 0x01, 0x01, 0x60, 0x14, // 354 - 35B
            0x08, 0x01, 0x00, 0x21, 0x71, 0x09, 0x06, 0x04, // 35C - 363
            0x01, 0x09, 0x09, 0x08, 0x59, 0x00, 0x11, 0x11, // 364 - 36B
            0x00, 0x00, 0x01, 0x11, 0x00, 0x00, 0x20, 0x20, // 36C - 373
            0x0A}; 

        internal static byte sub_37991()
        {
            byte var_1 = unk_16664[gbl.byte_1AD3C];

            return var_1;
        }


        internal static void sub_379AC(int arg_2, int arg_4)
        {
            if (arg_2 < 0x31)
            {
                gbl.mapToBackGroundTile[arg_2 + 1, arg_4] = 0x40;
            }

            if (arg_4 < 0x18 && arg_2 < 0x31)
            {
                gbl.mapToBackGroundTile[arg_2 + 1, arg_4 + 1] = 0x41;
            }
        }


        internal static void sub_37A00()
        {
            sbyte var_3;
            sbyte var_2;

            byte var_1 = 0;

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

                for (int var_4 = 0; var_4 <= 0x18; var_4++)
                {
                    if (var_3 <= 0x31)
                    {
                        gbl.mapToBackGroundTile[var_3, var_4] = (byte)(ovr024.roll_dice(2, 1) + 0x3B);

                        if (var_3 < 0x31)
                        {
                            gbl.mapToBackGroundTile[var_3 + 1, var_4] = (byte)(ovr024.roll_dice(2, 1) + 0x3D);
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
            if ((sub_37991() & 0x80) == 0)
            {
                sbyte var_4 = 10;

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

                for (int var_2 = 0; var_2 <= 0x31; var_2++)
                {
                    for (int var_3 = 1; var_3 <= 0x18; var_3++)
                    {
                        if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[var_2, var_3]].tile_index == 22 &&
                            gbl.BackGroundTiles[gbl.mapToBackGroundTile[var_2, var_3 - 1]].tile_index == 22 &&
                            var_4 >= ovr024.roll_dice(100, 1))
                        {
                            if (var_4 >= ovr024.roll_dice(100, 1))
                            {
                                gbl.mapToBackGroundTile[var_2, var_3] = (byte)(ovr024.roll_dice(2, 1) + 0x29);
                            }
                            else
                            {
                                gbl.mapToBackGroundTile[var_2, var_3 - 1] = (byte)(ovr024.roll_dice(5, 1) + 0x1F);
                                gbl.mapToBackGroundTile[var_2, var_3] = (byte)(ovr024.roll_dice(5, 1) + 0x24);
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
                gbl.mapToBackGroundTile[arg_E, arg_C] = (byte)(ovr024.roll_dice(2, 1) + 0x39);
            }
            else if (var_1 <= arg_A + arg_8)
            {
                gbl.mapToBackGroundTile[arg_E, arg_C] = (byte)(ovr024.roll_dice(2, 1) + 0x2f);

            }
            else if( var_1 <= arg_A + arg_8 + arg_6 )
            {
                gbl.mapToBackGroundTile[arg_E, arg_C] = (byte)(ovr024.roll_dice(4, 1) + 0x2B);
            }
            else if( var_1 <= arg_A + arg_8 + arg_6 + arg_4 )
            {
                gbl.mapToBackGroundTile[arg_E, arg_C] = (byte)(ovr024.roll_dice(3, 1) + 0x36);
            }
            else if (var_1 <= arg_A + arg_8 + arg_6 + arg_4 + arg_2)
            {
                gbl.mapToBackGroundTile[arg_E, arg_C] = (byte)(ovr024.roll_dice(4, 1) + 0x31);
            }
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
                    if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[var_1, var_2]].tile_index == 22)
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
            gbl.mapToBackGroundTile.SetField_7(0x17);

            //byte var_1 = gbl.area_ptr.field_186;
            //byte var_2 = gbl.area_ptr.field_188;
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

            gbl.mapToBackGroundTile = new Struct_1D1BC();

            gbl.mapToBackGroundTile.field_4 = false;
            gbl.mapToBackGroundTile.size = 1;
            gbl.mapToBackGroundTile.field_6 = 0;

            if (gbl.area_ptr.field_1CC != 0)
            {
                sub_378CD();
            }
            else
            {
                sub_37FC8();
            }
        }

        internal static void sub_380E0()
        {
            Player player = gbl.player_next_ptr;

            int var_5 = 0;

            while (player != null)
            {
                ovr025.sub_66C20(player);
                var_5++;

                player.actions = new Action();

                if (var_5 > gbl.area2_ptr.field_67C)
                {
                    player.actions.field_13 = 1;
                }

                player.actions.direction = unk_1660C[gbl.mapDirection >> 1];

                if (player.combat_team == CombatTeam.Enemy)
                {
                    player.actions.direction = (byte)((player.actions.direction + 4) % 8);
                }

                int var_6 = player.field_F7 & 0x7f;

                if (player.combat_team == CombatTeam.Ours)
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


        internal static bool offset_invalid(int arg_0, int arg_2) /* sub_38202 */
        {
            bool ret_val;

            if ((arg_2 >= 0 && arg_2 <= 10) ||
                (arg_0 >= 0 && arg_0 <= 5))
            {
                ret_val = false;
            }
            else
            {
                ret_val = true;
            }

            return ret_val;
        }


        internal static bool sub_38233(byte arg_0, int arg_2, int arg_4, int arg_6, int arg_8, int player_index)
        {
            bool var_1;

            if (arg_8 < 0 || arg_8 > 10 ||
                arg_6 < 0 || arg_6 > 5 ||
                unk_1AB1C[gbl.currentTeam, arg_0, arg_6, arg_8] == 0)
            {
                var_1 = false;
            }
            else
            {
                //TODO workout why players are put on same spot (arg_4, arg_2, & arg_5 == 0)
                gbl.CombatMap[player_index].xPos = arg_8 + (arg_4 * 6) + (arg_2 * 5) + 22;
                gbl.CombatMap[player_index].yPos = arg_6 + (arg_2 * 5) + 10;

                bool dummyBoolA, dummyBoolB;
                byte groundTile;
                byte tmp_player_index;
                ovr033.getGroundInformation(out dummyBoolA, out dummyBoolB, out groundTile, out tmp_player_index, 8, gbl.player_array[player_index]);

                if (tmp_player_index == 0 &&
                    groundTile > 0 &&
                    gbl.BackGroundTiles[groundTile].move_cost < 0xFF)
                {
                    var_1 = true;
                    unk_1AB1C[gbl.currentTeam, arg_0, arg_6, arg_8] = 0;
                }
                else
                {
                    var_1 = false;
                }
            }

            return var_1;
        }

        static int[] direction_165EC = { 8, 4, 6, 2, 8, 6, 4, 0, 8, 0, 6, 2, 8, 2, 0, 4 }; /*seg600:02DC unk_165EC*/ 
        static int[] direction_165FC = { 0, 0, 2, 6, 2, 2, 0, 4, 4, 4, 2, 6, 6, 6, 4, 0 }; /*seg600:02EC unk_165FC*/

        static byte[] unk_1660C = { 7, 2, 3, 6 }; /*seg600:02FC*/

        static byte[] /*seg600:0300*/ unk_16610 = { 5, 4, 5, 6, 3, 8, 7, 2 };
        static byte[] /*seg600:0308*/ unk_16618 = { 3, 2, 2, 3, 0, 2, 5, 3 };

        static byte[, ,] unk_16620 = new byte[5, 6, 2] { // unk_16620 seg600:0310 
                {{1,0},{1,0},{1,0},{2,9},{3,10},{4,10}}, // 310 - 31B
                {{0,2},{0,3},{1,4},{2,5},{3,6},{4,7}}, // 31C - 327
                {{0,6},{0,7},{1,8},{1,0},{1,0},{1,0}}, // 328 - 333
                {{3,6},{4,7},{5,8},{6,9},{7,10},{8,10}}, // 334 - 33F
                {{0,6},{0,7},{1,8},{2,9},{3,10},{4,10}}, // 340 - 31B
        };

        internal static bool sub_38380(byte loop_count)
        {
            sbyte var_18 = 0; /* Simeon */
            sbyte var_17 = 0; /* Simeon */
            sbyte var_16 = 0; /* Simeon */
            sbyte var_15 = 0; /* Simeon */
            byte var_13 = 0; /* Simeon */
            byte var_10 = 0; /* Simeon */
            bool var_2 = false; /* Simeon */

            bool var_3 = true;
            bool var_4 = false;
            byte var_7 = 1;
            int var_F = 0;
            byte var_14 = 0;

            int pos_x = gbl.team_start_x[gbl.currentTeam];
            int pos_y = gbl.team_start_y[gbl.currentTeam];

            do
            {
                int tempIndex = (gbl.team_direction[gbl.currentTeam] << 2) + var_14;
                int direction = direction_165FC[tempIndex] / 2;

                if (var_7 == 1)
                {
                    int tmpX = gbl.MapDirectionXDelta[unk_1660C[(direction + 2) % 4]];
                    int tmpY = gbl.MapDirectionYDelta[unk_1660C[(direction + 2) % 4]];

                    var_15 = (sbyte)(unk_16610[(var_14 > 0 ? 4 : 0) + direction] + (var_F * tmpX));
                    var_16 = (sbyte)(unk_16618[(var_14 > 0 ? 4 : 0) + direction] + (var_F * tmpY));
                    var_17 = var_15;
                    var_18 = var_16;
                    var_10 = 1;
                    var_7 = 2;
                    var_13 = 1;
                }
                else if (var_7 == 2)
                {
                    int tmpX = gbl.MapDirectionXDelta[unk_1660C[(direction + 1) % 4]];
                    int tmpY = gbl.MapDirectionYDelta[unk_1660C[(direction + 1) % 4]];

                    var_17 = (sbyte)(var_15 + (tmpX * var_10));
                    var_18 = (sbyte)(var_16 + (tmpY * var_10));
                    var_7 = 3;
                    var_13 += 1;
                }
                else if (var_7 == 3)
                {
                    int tmpX = gbl.MapDirectionXDelta[unk_1660C[(direction + 3) % 4]];
                    int tmpY = gbl.MapDirectionYDelta[unk_1660C[(direction + 3) % 4]];

                    var_17 = (sbyte)(var_15 + (tmpX * var_10));
                    var_18 = (sbyte)(var_16 + (tmpY * var_10));

                    var_7 = 2;
                    var_10++;
                    var_13++;
                }

                bool var_5 = (var_17 < 0 || var_18 < 0 || var_17 > 10 || var_18 > 5);

                if (var_7 > 1)
                {
                    if ((var_5 == true && offset_invalid(var_18, var_17) == false) ||
                        (var_3 == true && var_13 >= gbl.half_team_count[gbl.currentTeam]) ||
                        (var_3 == false && var_13 > 11))
                    {
                        var_F++;

                        if (gbl.currentTeam == 0 &&
                            (gbl.team_direction[0] & 1) == 1 &&
                            var_14 == 0 &&
                            var_F == 1)
                        {
                            int tmpX = gbl.team_start_x[gbl.currentTeam] + gbl.mapPosX;
                            int tmpY = gbl.team_start_y[gbl.currentTeam] + gbl.mapPosY;
                            bool found = false;

                            for (int var_A = 1; var_A <= 3; var_A++)
                            {
                                int tmpDir = direction_165EC[gbl.team_direction[gbl.currentTeam] + var_A];

                                if (gbl.game_state == 3 ||
                                    get_dir_flags(tmpDir, tmpY, tmpX) != 1)
                                {
                                    found = true;
                                }
                            }

                            if (found)
                            {
                                var_F++;
                            }
                        }
                        var_7 = 1;
                        var_3 = false;
                    }
                }


                if (var_5 == true &&
                    offset_invalid(var_18, var_17) == true)
                {
                    var_2 = false;
                    var_7 = 0;

                    while (var_14 < 3 && var_7 != 1)
                    {
                        var_14++;

                        int tmpX = gbl.team_start_x[gbl.currentTeam] + gbl.mapPosX;
                        int tmpY = gbl.team_start_y[gbl.currentTeam] + gbl.mapPosY;

                        int tmpDir = direction_165EC[gbl.team_direction[gbl.currentTeam] + var_14];

                        if (gbl.game_state == 3 ||
                            get_dir_flags(tmpDir, tmpY, tmpX) != 1)
                        {
                            pos_x = gbl.team_start_x[gbl.currentTeam] + gbl.MapDirectionXDelta[tmpDir];
                            pos_y = gbl.team_start_y[gbl.currentTeam] + gbl.MapDirectionYDelta[tmpDir];

                            var_F = 0;
                            var_7 = 1;
                        }
                    }

                    if (var_7 != 1)
                    {
                        var_4 = true;
                    }
                }

                if (var_5 == false)
                {
                    var_2 = sub_38233(var_14, pos_y, pos_x, var_18, var_17, loop_count);
                }
            } while (var_2 == false && var_4 == false);


            return var_4 == false;
        }


        internal static void sub_387FE()
        {
            int direction;
            byte loop_var;
            Player player_ptr;
            Player player_ptr2;

            ovr025.count_teams();
            byte var_F = 0;
            byte var_E = 0;

            for (int i = 1; i <= gbl.MaxCombatantCount; i++)
            {
                gbl.CombatMap[i].size = 0;
            }

            ovr033.sub_743E7();
            gbl.team_start_x[0] = 0;
            gbl.team_start_y[0] = 0;
            gbl.team_direction[0] = (byte)(gbl.mapDirection >> 1);

            gbl.team_start_x[1] = (gbl.area2_ptr.field_582 * gbl.MapDirectionXDelta[gbl.mapDirection]) + gbl.team_start_x[0];
            gbl.team_start_y[1] = (gbl.area2_ptr.field_582 * gbl.MapDirectionYDelta[gbl.mapDirection]) + gbl.team_start_y[0];
            gbl.team_direction[1] = ((gbl.mapDirection + 4) % 8) / 2;

            gbl.half_team_count[0] = (gbl.friends_count + 1) / 2;
            gbl.half_team_count[1] = (gbl.foe_count + 1) / 2;

            for (gbl.currentTeam = 0; gbl.currentTeam < 2; gbl.currentTeam++)
            {
                for (int var_C = 0; var_C < 4; var_C++)
                {
                    if (var_C == 1)
                    {
                        direction = 4;
                    }
                    else
                    {
                        direction = gbl.team_direction[gbl.currentTeam];
                    }

                    for (int var_2 = 0; var_2 <= 5; var_2++)
                    {
                        for (int var_1 = 0; var_1 <= 10; var_1++)
                        {
                            if (unk_16620[direction, var_2, 0] > var_1 ||
                                unk_16620[direction, var_2, 1] < var_1)
                            {
                                unk_1AB1C[gbl.currentTeam, var_C, var_2, var_1] = 0;
                            }
                            else
                            {
                                unk_1AB1C[gbl.currentTeam, var_C, var_2, var_1] = 1;
                            }
                        }
                    }
                }
            }

            loop_var = 1;
            gbl.CombatantCount = 0;
            player_ptr2 = gbl.player_next_ptr;
            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null)
            {
                seg043.clear_one_keypress();

                gbl.player_array[loop_var] = player_ptr;

                gbl.currentTeam = (sbyte)player_ptr.combat_team;

                gbl.CombatMap[loop_var].field_2 = loop_var;
                gbl.CombatMap[loop_var].size = (byte)(player_ptr.field_DE & 7);

                if (sub_38380(loop_var) == true)
                {
                    player_ptr2 = player_ptr;

                    if (player_ptr.in_combat == false)
                    {
                        gbl.CombatMap[loop_var].size = 0;

                        if (gbl.combat_type == gbl.combatType.normal &&
                            player_ptr.actions.field_13 == 0)
                        {
                            int tmpX = gbl.CombatMap[loop_var].xPos;
                            int tmpY = gbl.CombatMap[loop_var].yPos;
                            gbl.byte_1D1BB++;

                            gbl.unk_1D183[gbl.byte_1D1BB].field_6 = (byte)gbl.mapToBackGroundTile[tmpX, tmpY];
                            gbl.mapToBackGroundTile[tmpX, tmpY] = 0x1F;
                            gbl.unk_1D183[gbl.byte_1D1BB].target = player_ptr;
                            gbl.unk_1D183[gbl.byte_1D1BB].mapX = tmpX;
                            gbl.unk_1D183[gbl.byte_1D1BB].mapY = tmpY;
                        }
                    }

                    gbl.CombatantCount++;
                    ovr033.sub_743E7();
                    loop_var++;
                    var_E++;
                }
                else
                {
                    gbl.CombatMap[loop_var].size = 0;

                    if (player_ptr.actions.field_13 == 1)
                    {
                        var_F++;

                        gbl.player_array[loop_var] = null;
                        gbl.player_ptr = player_ptr;
                        ovr018.free_players(0, true);
                        player_ptr2 = player_ptr;
                    }
                    else
                    {
                        player_ptr2 = player_ptr;
                        gbl.CombatantCount++;
                    }
                }

                player_ptr = player_ptr2.next_player;
            }

            player_ptr2.next_player = null;
        }


        internal static void battle_begins()
        {
            Player player;

            gbl.DelayBetweenCharacters = false;

            ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            seg040.free_dax_block(ref gbl.headX_dax);
            seg040.free_dax_block(ref gbl.bodyX_dax);
            seg040.free_dax_block(ref gbl.bigpic_dax);
            gbl.bigpic_block_id = 0xff;
            gbl.current_head_id = 0xff;
            gbl.current_body_id = 0xff;
            ovr027.redraw_screen();
            seg041.GameDelay();

            seg041.displayString("A battle begins...", 0, 0x0a, 0x18, 0);

            gbl.magicOn = false;
            gbl.byte_1D8B7 = 0;
            gbl.byte_1D8B8 = 0x0F;
            gbl.byte_1D2C9 = 0;
            gbl.byte_1D1BB = 0;

            gbl.stru_1D885 = null;
            gbl.stru_1D889 = null;
            gbl.item_ptr = null;

            gbl.unk_1D183 = new Struct_1D183[8 + 1];
            for (int i = 0; i < 8 + 1; i++)
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
            seg040.init_dax_block(out gbl.missile_dax, 1, 4, 3, 0x18);

            gbl.mapToBackGroundTile.mapScreenLeftX = ovr033.PlayerMapXPos(gbl.player_next_ptr) - 3;
            gbl.mapToBackGroundTile.mapScreenTopY = ovr033.PlayerMapYPos(gbl.player_next_ptr) - 3;

            ovr025.sub_68DC0();
            player = gbl.player_next_ptr;

            while (player != null)
            {
                ovr024.work_on_00(player, 8);
                ovr024.work_on_00(player, 22);

                player = player.next_player;
            }

            ovr014.calc_enemy_health_percentage();
            gbl.game_state = 5;
        }
    }
}
