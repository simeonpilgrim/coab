using Classes;
using System.Collections.Generic;

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
                else if (ovr031.getMap_wall_type(dir, mapY, mapX) == 0)
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
            for (int y_pos = 2; y_pos <= 4; y_pos++)
            {
                for (int x_pos = 0; x_pos <= 5; x_pos++)
                {
                    set_background_tile(22, y_pos, x_pos);
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
                set_background_tile(22, 0, 3);
                set_background_tile(22, 0, 4);
                set_background_tile(22, 1, 3);
                set_background_tile(22, 1, 4);
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
                    gbl.byte_1AD3D = (byte)(ovr031.get_wall_x2(mapY, mapX) & 0x40);
                    sub_370D3();
                }
            }
        }

        static int[] CityInfo = { /* unk_16664 seg600:0354 */
            0x00, 0x18, 0x11, 0x15, 0x01, 0x01, 0x60, 0x14, // 354 - 35B
            0x08, 0x01, 0x00, 0x21, 0x71, 0x09, 0x06, 0x04, // 35C - 363
            0x01, 0x09, 0x09, 0x08, 0x59, 0x00, 0x11, 0x11, // 364 - 36B
            0x00, 0x00, 0x01, 0x11, 0x00, 0x00, 0x20, 0x20, // 36C - 373
            0x0A};

        static int GetCityInfo() // sub_37991
        {
            return CityInfo[gbl.current_city];
        }


        static void SetGroundTile_40(int map_x, int map_y) // sub_379AC
        {
            if (map_x < 0x31)
            {
                gbl.mapToBackGroundTile[map_x + 1, map_y] = 0x40;
            }

            if (map_y < 0x18 && map_x < 0x31)
            {
                gbl.mapToBackGroundTile[map_x + 1, map_y + 1] = 0x41;
            }
        }


        internal static void sub_37A00()
        {
            byte var_1 = 0;

            if ((GetCityInfo() & 0x20) != 0)
            {
                var_1 = 0x23;
            }

            if ((GetCityInfo() & 0x10) != 0)
            {
                var_1 = 0x4B;
            }

            if (ovr024.roll_dice(100, 1) <= var_1)
            {
                int map_x = 0x22 - ovr024.roll_dice(4, 5);

                while (((map_x + 2) % 7) > 0)
                {
                    map_x--;
                }

                for (int map_y = 0; map_y <= 0x18; map_y++)
                {
                    if (map_x <= 0x31)
                    {
                        gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(2, 1) + 0x3B;

                        if (map_x < 0x31)
                        {
                            gbl.mapToBackGroundTile[map_x + 1, map_y] = ovr024.roll_dice(2, 1) + 0x3D;
                        }

                        if (ovr024.roll_dice(20, 1) == 1)
                        {
                            SetGroundTile_40(map_x, map_y);
                        }

                        map_x++;
                    }
                }
            }
        }


        static void BuildGroundMap01() // sub_37B0B
        {
            int cityFlags = GetCityInfo();
            if ((cityFlags & 0x80) == 0)
            {
                int neededRoll = 10;

                if ((cityFlags & 2) != 0)
                {
                    neededRoll -= 5;
                }

                if ((cityFlags & 4) != 0)
                {
                    neededRoll -= 2;
                }

                if ((cityFlags & 0x40) != 0)
                {
                    neededRoll += 5;
                }

                if ((cityFlags & 8) != 0)
                {
                    neededRoll += 10;
                }

                if (neededRoll < 0)
                {
                    neededRoll = 1;
                }

                for (int mapX = 0; mapX <= 0x31; mapX++)
                {
                    for (int mapY = 1; mapY <= 0x18; mapY++)
                    {
                        if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[mapX, mapY]].tile_index == 22 &&
                            gbl.BackGroundTiles[gbl.mapToBackGroundTile[mapX, mapY - 1]].tile_index == 22 &&
                            neededRoll >= ovr024.roll_dice(100, 1))
                        {
                            if (neededRoll >= ovr024.roll_dice(100, 1))
                            {
                                gbl.mapToBackGroundTile[mapX, mapY] = ovr024.roll_dice(2, 1) + 0x29;
                            }
                            else
                            {
                                gbl.mapToBackGroundTile[mapX, mapY - 1] = ovr024.roll_dice(5, 1) + 0x1F;
                                gbl.mapToBackGroundTile[mapX, mapY] = ovr024.roll_dice(5, 1) + 0x24;
                            }
                        }
                    }
                }
            }
        }


        static void SetGroupMapStepped(int stepE, int stepD, int stepC, int stepB, int stepA, int map_y, int map_x) // sub_37CA2
        {
            int roll = ovr024.roll_dice(100, 1);

            if (roll <= stepA)
            {
                gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(2, 1) + 0x39;
            }
            else if (roll <= stepA + stepB)
            {
                gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(2, 1) + 0x2f;
            }
            else if (roll <= stepA + stepB + stepC)
            {
                gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(4, 1) + 0x2B;
            }
            else if (roll <= stepA + stepB + stepC + stepD)
            {
                gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(3, 1) + 0x36;
            }
            else if (roll <= stepA + stepB + stepC + stepD + stepE)
            {
                gbl.mapToBackGroundTile[map_x, map_y] = ovr024.roll_dice(4, 1) + 0x31;
            }
        }


        static void sub_37E4A()
        {
            int var_4 = 50;

            if ((GetCityInfo() & 0x10) != 0)
            {
                var_4 += 10;
            }

            if ((GetCityInfo() & 0x20) != 0)
            {
                var_4 += 30;
            }

            if ((GetCityInfo() & 0x40) != 0)
            {
                var_4 += 20;
            }

            if ((GetCityInfo() & 4) != 0)
            {
                var_4 -= 10;
            }

            if ((GetCityInfo() & 2) != 0)
            {
                var_4 -= 20;
            }

            if ((GetCityInfo() & 0x80) != 0)
            {
                var_4 -= 50;
            }

            for (int map_x = 0; map_x <= 49; map_x++)
            {
                for (int map_y = 0; map_y <= 24; map_y++)
                {
                    if (gbl.BackGroundTiles[gbl.mapToBackGroundTile[map_x, map_y]].tile_index == 22)
                    {
                        if (var_4 >= -30 && var_4 <= 9)
                        {
                            SetGroupMapStepped(15, 30, 0, 0, 0, map_y, map_x);
                        }
                        else if (var_4 >= 10 && var_4 <= 29)
                        {
                            SetGroupMapStepped(10, 14, 5, 1, 0, map_y, map_x);
                        }
                        else if (var_4 >= 30 && var_4 <= 69)
                        {
                            SetGroupMapStepped(5, 10, 5, 2, 0, map_y, map_x);
                        }
                        else if (var_4 >= 60 && var_4 <= 89)
                        {
                            SetGroupMapStepped(1, 10, 10, 2, 10, map_y, map_x);
                        }
                        else if (var_4 >= 90 && var_4 <= 110)
                        {
                            SetGroupMapStepped(1, 10, 15, 5, 15, map_y, map_x);
                        }
                    }
                }
            }
        }


        static void sub_37FC8()
        {
            gbl.mapToBackGroundTile.SetField_7(23);

            gbl.current_city = gbl.area_ptr.current_city;
            sub_37A00();
            BuildGroundMap01();
            sub_37E4A();
        }


        static void SetupGroundTiles() // sub_38030
        {
            if (gbl.area_ptr.inDungeon != 0)
            {
                ovr034.Load24x24Set(0x19, 0, 1, "DungCom");
            }
            else
            {
                ovr034.Load24x24Set(0x21, 0, 1, "WildCom");
            }

            ovr034.Load24x24Set(6, 0x22, 1, "RandCom");

            gbl.mapToBackGroundTile = new Struct_1D1BC();

            gbl.mapToBackGroundTile.drawTargetCursor = false;
            gbl.mapToBackGroundTile.size = 1;
            gbl.mapToBackGroundTile.field_6 = false;

            if (gbl.area_ptr.inDungeon != 0)
            {
                sub_378CD();
            }
            else
            {
                sub_37FC8();
            }
        }


        static void SetupCombatActions() // sub_380E0
        {
            int playerCount = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                ovr025.reclac_player_values(player);
                playerCount++;

                player.actions = new Action();

                if (playerCount > gbl.area2_ptr.party_size)
                {
                    player.actions.nonTeamMember = true;
                }

                player.actions.direction = HalfDirToIso[gbl.mapDirection / 2];

                if (player.combat_team == CombatTeam.Enemy)
                {
                    player.actions.direction = (player.actions.direction + 4) % 8;
                }

                int morale = player.control_morale & Control.PC_Mask;

                if (player.combat_team == CombatTeam.Ours)
                {
                    if (player.actions.nonTeamMember)
                    {
                        if (morale == 0 ||
                            morale > 0x66)
                        {
                            player.control_morale = (byte)(gbl.area2_ptr.field_58C + Control.NPC_Base);
                        }
                    }
                }
            }
        }


        static bool both_invalid(int arg_0, int arg_2) /* sub_38202 */
        {
            if ((arg_2 >= 0 && arg_2 < 11) ||
                (arg_0 >= 0 && arg_0 < 6))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        static bool try_place_combatant(int arg_0, int arg_2, int arg_4, int arg_6, int arg_8, int player_index) /* sub_38233 */
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
                gbl.CombatMap[player_index].pos.x = arg_8 + (arg_4 * 6) + (arg_2 * 5) + 22;
                gbl.CombatMap[player_index].pos.y = arg_6 + (arg_2 * 5) + 10;

                int groundTile;
                int tmp_player_index;
                ovr033.getGroundInformation(out groundTile, out tmp_player_index, 8, gbl.player_array[player_index]);

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

        static int[,] direction_165EC = { { 8, 4, 6, 2 }, { 8, 6, 4, 0 }, { 8, 0, 6, 2 }, { 8, 2, 0, 4 } }; /*seg600:02DC unk_165EC*/
        static int[,] direction_165FC = { { 0, 0, 2, 6 }, { 2, 2, 0, 4 }, { 4, 4, 2, 6 }, { 6, 6, 4, 0 } }; /*seg600:02EC unk_165FC*/

        static int[] HalfDirToIso = { 7, 2, 3, 6 }; /*seg600:02FC unk_1660C */

        static byte[] /*seg600:0300*/ unk_16610 = { 5, 4, 5, 6, 3, 8, 7, 2 };
        static byte[] /*seg600:0308*/ unk_16618 = { 3, 2, 2, 3, 0, 2, 5, 3 };

        static byte[, ,] unk_16620 = new byte[5, 6, 2] { // unk_16620 seg600:0310 
                {{1,0},{1,0},{1,0},{2,9},{3,10},{4,10}}, // 310 - 31B
                {{0,2},{0,3},{1,4},{2,5},{3,6},{4,7}}, // 31C - 327
                {{0,6},{0,7},{1,8},{1,0},{1,0},{1,0}}, // 328 - 333
                {{3,6},{4,7},{5,8},{6,9},{7,10},{8,10}}, // 334 - 33F
                {{0,6},{0,7},{1,8},{2,9},{3,10},{4,10}}, // 340 - 31B
        };

        enum tri_state
        {
            start = 1,
            right = 2,
            left = 3
        }

        internal static bool place_combatant(int player_index) /* sub_38380 */
        {
            int cur_y = 0; /* Simeon */
            int cur_x = 0; /* Simeon */
            int base_y = 0; /* Simeon */
            int base_x = 0; /* Simeon */
            byte var_13 = 0; /* Simeon */

            bool placed = false;
            bool first_row = true;
            bool var_4 = false;

            tri_state state = tri_state.start;
            int row_scale = 0;
            int col_scale = 0;
            int var_14 = 0;

            int team_x = gbl.team_start_x[gbl.currentTeam];
            int team_y = gbl.team_start_y[gbl.currentTeam];

            do
            {
                int half_dir = direction_165FC[gbl.team_direction[gbl.currentTeam], var_14] / 2;

                switch (state)
                {
                    case tri_state.start:
                        {
                            int iso_dir = HalfDirToIso[(half_dir + 2) % 4];
                            int delta_x = gbl.MapDirectionXDelta[iso_dir];
                            int delta_y = gbl.MapDirectionYDelta[iso_dir];

                            base_x = unk_16610[(var_14 > 0 ? 4 : 0) + half_dir] + (row_scale * delta_x);
                            base_y = unk_16618[(var_14 > 0 ? 4 : 0) + half_dir] + (row_scale * delta_y);
                            cur_x = base_x;
                            cur_y = base_y;
                            col_scale = 1;
                            state = tri_state.right;
                            var_13 = 1;
                        }
                        break;

                    case tri_state.right:
                        {
                            int delta_x = gbl.MapDirectionXDelta[HalfDirToIso[(half_dir + 1) % 4]];
                            int delta_y = gbl.MapDirectionYDelta[HalfDirToIso[(half_dir + 1) % 4]];

                            cur_x = base_x + (delta_x * col_scale);
                            cur_y = base_y + (delta_y * col_scale);
                            state = tri_state.left;
                            var_13 += 1;
                        }
                        break;

                    case tri_state.left:
                        {
                            int delta_x = gbl.MapDirectionXDelta[HalfDirToIso[(half_dir + 3) % 4]];
                            int delta_y = gbl.MapDirectionYDelta[HalfDirToIso[(half_dir + 3) % 4]];

                            cur_x = base_x + (delta_x * col_scale);
                            cur_y = base_y + (delta_y * col_scale);

                            state = tri_state.right;
                            col_scale += 1;
                            var_13 += 1;
                        }
                        break;
                }

                bool any_cur_invalid = (cur_x < 0 || cur_y < 0 || cur_x > 10 || cur_y > 5);

                if (state > tri_state.start)
                {
                    if ((any_cur_invalid == true && both_invalid(cur_y, cur_x) == false) ||
                        (first_row == true && var_13 >= gbl.half_team_count[gbl.currentTeam]) ||
                        (first_row == false && var_13 > 11))
                    {
                        row_scale++;

                        if (gbl.currentTeam == 0 &&
                            (gbl.team_direction[0] & 1) == 1 &&
                            var_14 == 0 &&
                            row_scale == 1)
                        {
                            int tmpX = gbl.team_start_x[gbl.currentTeam] + gbl.mapPosX;
                            int tmpY = gbl.team_start_y[gbl.currentTeam] + gbl.mapPosY;
                            bool found = false;

                            for (int var_A = 1; var_A <= 3; var_A++)
                            {
                                int tmpDir = direction_165EC[gbl.team_direction[gbl.currentTeam], var_A];

                                if (gbl.game_state == GameState.WildernessMap ||
                                    get_dir_flags(tmpDir, tmpY, tmpX) != 1)
                                {
                                    found = true;
                                }
                            }

                            if (found)
                            {
                                row_scale++;
                            }
                        }
                        state = tri_state.start;
                        first_row = false;
                    }
                }


                if (any_cur_invalid == true &&
                    both_invalid(cur_y, cur_x) == true)
                {
                    placed = false;
                    state = 0;

                    while (var_14 < 3 && state != tri_state.start)
                    {
                        var_14++;

                        int tmpX = gbl.team_start_x[gbl.currentTeam] + gbl.mapPosX;
                        int tmpY = gbl.team_start_y[gbl.currentTeam] + gbl.mapPosY;

                        int tmpDir = direction_165EC[gbl.team_direction[gbl.currentTeam], var_14];

                        if (gbl.game_state == GameState.WildernessMap ||
                            get_dir_flags(tmpDir, tmpY, tmpX) != 1)
                        {
                            team_x = gbl.team_start_x[gbl.currentTeam] + gbl.MapDirectionXDelta[tmpDir];
                            team_y = gbl.team_start_y[gbl.currentTeam] + gbl.MapDirectionYDelta[tmpDir];

                            row_scale = 0;
                            state = tri_state.start;
                        }
                    }

                    if (state != tri_state.start)
                    {
                        var_4 = true;
                    }
                }

                if (any_cur_invalid == false)
                {
                    placed = try_place_combatant(var_14, team_y, team_x, cur_y, cur_x, player_index);
                }
            } while (placed == false && var_4 == false);


            return var_4 == false;
        }


        static void PlaceCombatants() /* sub_387FE */
        {
            ovr025.CountCombatTeamMembers();

            for (int i = 1; i <= gbl.MaxCombatantCount; i++)
            {
                gbl.CombatMap[i].size = 0;
            }
            ovr033.setup_mapToPlayerIndex_and_playerScreen();

            gbl.team_start_x[0] = 0;
            gbl.team_start_y[0] = 0;
            gbl.team_direction[0] = gbl.mapDirection / 2;

            gbl.team_start_x[1] = (gbl.area2_ptr.encounter_distance * gbl.MapDirectionXDelta[gbl.mapDirection]) + gbl.team_start_x[0];
            gbl.team_start_y[1] = (gbl.area2_ptr.encounter_distance * gbl.MapDirectionYDelta[gbl.mapDirection]) + gbl.team_start_y[0];
            gbl.team_direction[1] = ((gbl.mapDirection + 4) % 8) / 2;

            gbl.half_team_count[0] = (gbl.friends_count + 1) / 2;
            gbl.half_team_count[1] = (gbl.foe_count + 1) / 2;

            for (gbl.currentTeam = 0; gbl.currentTeam < 2; gbl.currentTeam++)
            {
                for (int var_C = 0; var_C < 4; var_C++)
                {
                    int direction;
                    if (var_C == 1)
                    {
                        direction = 4;
                    }
                    else
                    {
                        direction = gbl.team_direction[gbl.currentTeam];
                    }

                    for (int var_2 = 0; var_2 < 6; var_2++)
                    {
                        for (int var_1 = 0; var_1 < 11; var_1++)
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

            int loop_var = 1;
            gbl.CombatantCount = 0;

            List<Player> to_remove = new List<Player>();
            foreach (Player player_ptr in gbl.player_next_ptr)
            {
                seg043.clear_one_keypress();

                gbl.player_array[loop_var] = player_ptr;

                gbl.currentTeam = (sbyte)player_ptr.combat_team;

                //gbl.CombatMap[loop_var].player_index = loop_var;
                gbl.CombatMap[loop_var].size = player_ptr.field_DE & 7;

                if (place_combatant(loop_var) == true)
                {
                    if (player_ptr.in_combat == false)
                    {
                        gbl.CombatMap[loop_var].size = 0;

                        if (gbl.combat_type == CombatType.normal &&
                            player_ptr.actions.nonTeamMember == false)
                        {
                            var pos = gbl.CombatMap[loop_var].pos;
                            
                            var b = new Struct_1D183();
                            gbl.downedPlayers.Add(b);

                            b.originalBackgroundTile = gbl.mapToBackGroundTile[pos];
                            b.target = player_ptr;
                            b.map = pos;

                            gbl.mapToBackGroundTile[pos] = 0x1F;
                            
                        }
                    }

                    gbl.CombatantCount++;
                    ovr033.setup_mapToPlayerIndex_and_playerScreen();
                    loop_var++;
                }
                else
                {
                    gbl.CombatMap[loop_var].size = 0;

                    if (player_ptr.actions.nonTeamMember == true)
                    {
                        gbl.player_array[loop_var] = null;
                        to_remove.Add(player_ptr);
                    }
                    else
                    {
                        gbl.CombatantCount++;
                    }
                }
            }

            foreach (Player player in to_remove)
            {
                gbl.player_ptr = ovr018.FreeCurrentPlayer(player, false, true);
            }
        }


        internal static void battle_begins()
        {
            gbl.DelayBetweenCharacters = false;

            ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            gbl.headX_dax = null;
            gbl.bodyX_dax = null;
            gbl.bigpic_dax = null;
            gbl.bigpic_block_id = 0xff;
            gbl.current_head_id = 0xff;
            gbl.current_body_id = 0xff;
            ovr027.ClearPromptArea();
            seg041.GameDelay();

            seg041.displayString("A battle begins...", 0, 0x0a, 0x18, 0);

            gbl.magicOn = false;
            gbl.combat_round = 0;
            gbl.combat_round_no_action_limit = 15;
            gbl.attack_roll = 0;

            gbl.NoxiousCloud = new List<GasCloud>();
            gbl.PoisonousCloud = new List<GasCloud>();
            gbl.item_ptr = null;

            gbl.downedPlayers = new List<Struct_1D183>();

            gbl.area2_ptr.field_666 = 0;

            SetupGroundTiles();

            SetupCombatActions();
            PlaceCombatants();

            seg043.clear_one_keypress();

            gbl.missile_dax = new DaxBlock( 1, 4, 3, 0x18);

            Point pos = ovr033.PlayerMapPos(gbl.player_next_ptr[0]);
            gbl.mapToBackGroundTile.mapScreenTopLeft = pos - Point.ScreenCenter;

            ovr025.RedrawCombatScreen();
            foreach (Player player in gbl.player_next_ptr)
            {
                ovr024.CheckAffectsEffect(player, CheckType.Type_8);
                ovr024.CheckAffectsEffect(player, CheckType.Type_22);
            }

            ovr014.calc_enemy_health_percentage();
            gbl.game_state = GameState.Combat;
        }
    }
}
