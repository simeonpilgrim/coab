using Classes;

namespace engine
{
    class ovr033
    {
        static sbyte[, ,] unk_xxxx = new sbyte[2, 4, 2] { { { 0, 0 }, { -1, -1 }, { -1, -1 }, { -1, -1 } }, { { 0, 0 }, { 0, 1 }, { -1, -1 }, { -1, -1 } } };

        internal static bool sub_7400F(out sbyte arg_0, out sbyte arg_4, int arg_8, int arg_A)
        {
            bool var_1 = false;
            arg_0 = 0; /* Added because of 'out' attribute */
            arg_4 = 0; /* Added because of 'out' attribute */

            if (arg_A != 0)
            {
                arg_4 = unk_xxxx[arg_A - 1, arg_8, 0];
                arg_0 = unk_xxxx[arg_A - 1, arg_8, 1];

                if (arg_4 >= 0)
                {
                    var_1 = true;
                }
            }

            return var_1;
        }


        static void calculatePlayerScreenPositions() /* sub_74077 */
        {
            int playerCount = gbl.stru_1C9CD[0].field_3;

            for (int i = 1; i <= playerCount; i++)
            {
                gbl.playerScreenX[i] = gbl.stru_1C9CD[i].xPos - gbl.stru_1D1BC.mapScreenLeftX;
                gbl.playerScreenY[i] = gbl.stru_1C9CD[i].yPos - gbl.stru_1D1BC.mapScreenTopY;
            }
        }


        internal static void sub_740F2()
        {
            seg040.sub_FAD2(8, 0);
            seg040.sub_FAD2(0, 8);
        }


        internal static void sub_74130()
        {
            seg040.sub_FAD2(0, 0);
            seg040.sub_FAD2(8, 8);
        }


        internal static void sub_7416E(int posY, int posX)
        {
            byte var_7;
            byte var_6;
            sbyte var_4;
            sbyte var_3;

            int screenPosX = posX - gbl.stru_1D1BC.mapScreenLeftX;
            int screenPosY = posY - gbl.stru_1D1BC.mapScreenTopY;

            for (byte var_5 = 0; var_5 <= 3; var_5++)
            {
                if (sub_7400F(out var_4, out var_3, var_5, gbl.stru_1D1BC.field_5) == true &&
                    sub_74730(screenPosY + var_4, screenPosX + var_3) == true)
                {
                    int i = gbl.stru_1D1BC[var_3 + posX, var_4 + posY];

                    ovr034.sub_760F7(0, gbl.unk_189B4[i].field_3, (screenPosY + var_4) * 3, (screenPosX + var_3) * 3);

                    if (gbl.stru_1D1BC.field_4 != 0)
                    {
                        ovr034.sub_76504(0x19, 0, 0, screenPosY + var_4, screenPosX + var_3);
                    }
                }
            }

            sub_74505(out var_7, out var_6, posY, posX);

            if (var_6 > 0 &&
                sub_74761(0, gbl.player_array[var_6]) == true)
            {
                ovr034.sub_76504(gbl.player_array[var_6].icon_id, 0,
                    gbl.player_array[var_6].actions.field_9, gbl.playerScreenY[var_6],
                    gbl.playerScreenX[var_6]);
            }
        }


        internal static void sub_7431C(int mapX, int mapY)
        {
            byte var_4;
            byte var_3;

            int newMapX = mapY - gbl.stru_1D1BC.mapScreenLeftX;
            int newMapY = mapX - gbl.stru_1D1BC.mapScreenTopY;

            sub_74572(0, newMapY, newMapX);
            sub_74505(out var_4, out var_3, mapX, mapY);

            if (var_3 > 0 &&
                sub_74761(0, gbl.player_array[var_3]) == true)
            {
                ovr034.sub_76504(gbl.player_array[var_3].icon_id,  0, 
                    gbl.player_array[var_3].actions.field_9,
                    gbl.playerScreenY[var_3], 
                    gbl.playerScreenX[var_3]);
            }
        }

        static Struct_1D1BC unk_1CB81 = new Struct_1D1BC();

        internal static void sub_743E7()
        {
            sbyte var_4;
            sbyte var_3;
            byte var_2;

            seg051.FillChar(0, 0x4E2, unk_1CB81.field_7);

            int var_5 = gbl.stru_1C9CD[0].field_3;

            for (int var_1 = 1; var_1 <= var_5; var_1++)
            {
                if (gbl.stru_1C9CD[var_1].field_3 > 0)
                {
                    for (var_2 = 0; var_2 <= 3; var_2++)
                    {
                        if (sub_7400F(out var_4, out var_3, var_2, gbl.stru_1C9CD[var_1].field_3) == true)
                        {
                            int cx = gbl.stru_1C9CD[var_1].xPos + var_3;
                            int ax = gbl.stru_1C9CD[var_1].yPos + var_4;

                            unk_1CB81[cx, ax] = (byte)var_1;
                        }
                    }

                    gbl.playerScreenX[var_1] = gbl.stru_1C9CD[var_1].xPos - gbl.stru_1D1BC.mapScreenLeftX;
                    gbl.playerScreenY[var_1] = gbl.stru_1C9CD[var_1].yPos - gbl.stru_1D1BC.mapScreenTopY;
                }
            }
        }


        internal static void sub_74505(out byte arg_0, out byte arg_4, int arg_8, int arg_A)
        {
            if (arg_A >= 0 && arg_A <= 0x31 &&
                arg_8 >= 0 && arg_8 <= 0x18)
            {
                arg_0 = gbl.stru_1D1BC[arg_A, arg_8];
                arg_4 = unk_1CB81[arg_A, arg_8];
            }
            else
            {
                arg_4 = 0;
                arg_0 = 0;
            }
        }


        internal static void sub_74572(byte player_index, int arg_2, int arg_4)
        {
            byte var_7;
            byte var_6;
            byte var_5;
            sbyte var_4;
            sbyte var_3;
            sbyte var_2 = -120; /*Simeon*/
            sbyte var_1 = -120; /*Simeon*/

            if (player_index == 0)
            {
                var_1 = (sbyte)(arg_4 + gbl.stru_1D1BC.mapScreenLeftX);
                var_2 = (sbyte)(arg_2 + gbl.stru_1D1BC.mapScreenTopY);

                sub_74505(out var_6, out player_index, var_2, var_1);
            }

            if (player_index > 0)
            {
                arg_4 = gbl.playerScreenX[player_index];
                arg_2 = gbl.playerScreenY[player_index];

                var_1 = (sbyte)(arg_4 + gbl.stru_1D1BC.mapScreenLeftX);
                var_2 = (sbyte)(arg_2 + gbl.stru_1D1BC.mapScreenTopY);

                var_7 = gbl.stru_1C9CD[player_index].field_3;

                for (var_5 = 0; var_5 <= 3; var_5++)
                {
                    if (sub_7400F(out var_4, out var_3, var_5, var_7) == true &&
                        sub_74730(var_4 + arg_2, var_3 + arg_4) == true)
                    {
                        int i1 = gbl.stru_1D1BC[var_1 + var_3, var_4 + var_2];

                        ovr034.sub_760F7(0, gbl.unk_189B4[i1].field_3, (arg_2 + var_4) * 3, (arg_4 + var_3) * 3);
                    }
                }
            }
            else if ( sub_74730(arg_2, arg_4) == true )
            {
                ovr034.sub_760F7(0, gbl.unk_189B4[gbl.stru_1D1BC[var_1, var_2]].field_3, arg_2 * 3, arg_4 * 3);
            }
        }


        internal static bool sub_74730(int arg_0, int arg_2)
        {
            bool var_1;

            if (arg_2 < 0 ||
                arg_2 > 6 ||
                arg_0 < 0 ||
                arg_0 > 6)
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
            }

            return var_1;
        }


        internal static bool sub_74761(byte arg_0, Player player)
        {
            sbyte var_5;
            sbyte var_4;
            byte player_index;
            byte var_2;
            bool var_1;

            var_1 = true;

            player_index = get_player_index(player);

            if (gbl.stru_1C9CD[player_index].field_3 == 0)
            {
                var_1 = false;
            }
            else
            {
                for (var_2 = 0; var_2 <= 3; var_2++)
                {
                    if (sub_7400F(out var_5, out var_4, var_2, gbl.stru_1C9CD[player_index].field_3) == true)
                    {
                        if (sub_74730(gbl.playerScreenY[player_index] + var_5, gbl.playerScreenX[player_index] + var_4) == false)
                        {
                            var_1 = false;
                            if (arg_0 != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            var_1 = true;
                            if (arg_0 == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return var_1;
        }


        static bool sub_7481B(byte arg_0, byte xPos, byte yPos)
        {
            sbyte var_7 = (sbyte)(gbl.stru_1D1BC.mapScreenLeftX + 3);
            sbyte var_8 = (sbyte)(gbl.stru_1D1BC.mapScreenTopY + 3);

            byte var_2 = arg_0;

            if (arg_0 == 0xff)
            {
                var_2 = 0;
            }

            byte var_9 = (byte)(var_7 - var_2);
            byte var_A = (byte)(var_7 + var_2);

            byte var_B = (byte)(var_8 - var_2);
            byte var_C = (byte)(var_8 + var_2);

            if (arg_0 == 0xff ||
                yPos < var_9 ||
                yPos > var_A ||
                xPos < var_B ||
                xPos > var_C)
            {
                if (yPos < var_9)
                {
                    while (yPos < var_7 && var_7 > 3)
                    {
                        var_7 -= 1;
                    }
                }
                else if (yPos > var_A)
                {
                    while (yPos > var_7 && var_7 < 0x2E)
                    {
                        var_7 += 1;
                    }
                }

                if (xPos < var_B)
                {
                    while (xPos < var_8 && var_8 > 3)
                    {
                        var_8 -= 1;
                    }
                }
                else if (xPos > var_C)
                {
                    while (xPos > var_8 && var_8 < 0x15)
                    {
                        var_8 += 1;
                    }
                }

                gbl.stru_1D1BC.mapScreenLeftX = (sbyte)(var_7 - 3);
                gbl.stru_1D1BC.mapScreenTopY = (sbyte)(var_8 - 3);

                int screenRowY = 0;
                int mapX = gbl.stru_1D1BC.mapScreenTopY;
                const int IconColumnSize = 3;

                for (int i = 0; i <= 6; i++)
                {
                    int screenColX = 0;
                    int mapY = gbl.stru_1D1BC.mapScreenLeftX;

                    for (int j = 0; j <= 6; j++)
                    {
                        byte AX = gbl.unk_189B4[gbl.stru_1D1BC[mapY, mapX]].field_3;

                        ovr034.sub_760F7(0, AX, screenRowY, screenColX);

                        screenColX += IconColumnSize;
                        mapY++;
                    }
                    screenRowY += IconColumnSize;
                    mapX++;
                }
                calculatePlayerScreenPositions();

                return true;
            }

            return false;
        }


        internal static void sub_749DD(byte dir, byte arg_2, int mapY, int mapX)
        {
            byte var_8;
            byte var_5;
            Player var_4;

            sbyte newXPos = (sbyte)(mapX + gbl.MapDirectionXDelta[dir]);
            sbyte newYPos = (sbyte)(mapY + gbl.MapDirectionYDelta[dir]);

            if (sub_7481B(arg_2, (byte)newYPos, (byte)newXPos) == true)
            {
                var_8 = gbl.stru_1C9CD[0].field_3;

                for (var_5 = 1; var_5 <= var_8; var_5++)
                {
                    var_4 = gbl.player_array[var_5];

                    if (var_4.in_combat == true &&
                        gbl.stru_1C9CD[var_5].field_3 > 0 &&
                        sub_74761(0, var_4) == true)
                    {
                        ovr034.sub_76504(var_4.icon_id, 0, var_4.actions.field_9, gbl.playerScreenY[var_5], gbl.playerScreenX[var_5]);
                    }
                }
            }

            sub_7431C(mapY, mapX);

            if (sub_74730(newYPos - gbl.stru_1D1BC.mapScreenTopY, newXPos - gbl.stru_1D1BC.mapScreenLeftX) == false)
            {
                if (newXPos > 0x31)
                {
                    newXPos = 0x31;
                }

                if (newXPos < 0)
                {
                    newXPos = 0;
                }

                if (newYPos > 0x18)
                {
                    newYPos = 0x18;
                }

                if (newYPos < 0)
                {
                    newYPos = 0;
                }
            }

            sub_7416E(newYPos, newXPos);
            seg040.DrawOverlay();
        }


        internal static void sub_74B3F(byte arg_0, byte arg_2, byte arg_4, Player player)
        {
            byte player_index;

            player_index = get_player_index(player);

            if (sub_74761(1, player) == false &&
                gbl.byte_1D910 == true)
            {

                sub_749DD(8, 3, PlayerMapYPos(player), PlayerMapXPos(player));
            }

            if ((arg_4 >> 2) != (player.actions.field_9 >> 2) ||
                arg_2 != 0 ||
                arg_0 != 0)
            {
                if (gbl.byte_1D910 == true)
                {
                    sub_74572(player_index, 0, 0);
                }
            }

            player.actions.field_9 = arg_4;

            if (arg_0 == 0 &&
                sub_74761(0, player) == true &&
                gbl.byte_1D910 == true)
            {
                ovr034.sub_76504(player.icon_id, arg_2, arg_4, gbl.playerScreenY[player_index], gbl.playerScreenX[player_index]);
                seg040.DrawOverlay();
            }
        }


        internal static int PlayerMapXPos(Player player) /* sub_74C32 */
        {
            return gbl.stru_1C9CD[get_player_index(player)].xPos;
        }


        internal static int PlayerMapYPos(Player player) /* sub_74C5A */
        {
            return gbl.stru_1C9CD[get_player_index(player)].yPos;
        }


        internal static byte sub_74C82(Player player)
        {
            byte ret_val;

            ret_val = gbl.stru_1C9CD[get_player_index(player)].field_3;

            return ret_val;
        }


        internal static byte get_player_index(Player player)
        {
            byte loop_var;
            bool match_found;
            byte ret_val;

            match_found = false;
            ret_val = 0;
            loop_var = 0;

            do
            {
                loop_var++;

                if (gbl.player_array[loop_var] == player)
                {
                    match_found = true;
                }

            } while (match_found == false && loop_var <= gbl.stru_1C9CD[0].field_3);

            if (match_found == true)
            {
                ret_val = loop_var;
            }

            return ret_val;
        }


        internal static void sub_74D04(out byte arg_0, out byte arg_4, out byte arg_8, out byte arg_C, byte arg_10, Player arg_12)
        {
            sbyte var_B;
            sbyte var_A;
            sbyte var_9;
            sbyte var_8;
            sbyte var_7;
            sbyte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            arg_C = 0;
            arg_8 = 0x17;

            var_5 = 1;
            arg_4 = 0;
            arg_0 = 0;

            var_2 = get_player_index(arg_12);

            var_8 = (sbyte)gbl.stru_1C9CD[var_2].xPos;
            var_9 = (sbyte)gbl.stru_1C9CD[var_2].yPos;

            for (var_1 = 0; var_1 <= 3; var_1++)
            {
                if (sub_7400F(out var_B, out var_A, var_1, gbl.stru_1C9CD[var_2].field_3) == true)
                {
                    var_6 = (sbyte)(var_8 + var_A + gbl.MapDirectionXDelta[arg_10]);
                    var_7 = (sbyte)(var_9 + var_B + gbl.MapDirectionYDelta[arg_10]);

                    sub_74505(out var_4, out var_3, var_7, var_6);

                    if (var_3 == var_2)
                    {
                        var_3 = 0;
                    }

                    if (var_3 > 0)
                    {
                        arg_C = var_3;
                    }

                    if (var_4 == 0)
                    {
                        arg_8 = 0;
                    }
                    else if (arg_8 != 0)
                    {
                        if (var_4 == 0x1e)
                        {
                            arg_4 = 1;
                        }
                        else if (var_4 == 0x1c)
                        {
                            arg_0 = 1;
                        }
                        else if (gbl.unk_189B4[var_4].field_0 >= var_5)
                        {
                            var_5 = gbl.unk_189B4[var_4].field_0;
                            arg_8 = var_4;
                        }
                    }
                }
            }
        }


        internal static void sub_74E6F(Player player)
        {
            int var_7;
            int var_6;
            sbyte var_5;
            sbyte var_4;
            byte var_3 = 0xf0; /* Simeon */
            byte var_2;
            byte var_1;

            if (gbl.game_state != 5)
            {
                seg044.sub_120E0(gbl.word_188C8);
                seg041.GameDelay();
            }
            else
            {
                var_2 = 1;
                while (var_2 < 9 &&
                    gbl.unk_1D183[var_3].field_0 != player)
                {
                    var_2++;
                }

                if (var_2 >= 9)
                {
                    var_1 = get_player_index(player);
                    var_6 = ovr033.PlayerMapXPos(player);
                    var_7 = ovr033.PlayerMapYPos(player);

                    if (sub_74761(1, player) == false)
                    {
                        sub_749DD(8, 3, var_7, var_6);
                    }

                    sub_74572(var_1, 0, 0);
                    seg044.sub_120E0(gbl.word_188C8);

                    for (var_3 = 0; var_3 <= 8; var_3++)
                    {
                        for (var_2 = 0; var_2 <= 3; var_2++)
                        {
                            if (sub_7400F(out var_5, out var_4, var_2, gbl.stru_1C9CD[var_1].field_3) == true)
                            {
                                throw new System.NotSupportedException();//mov	al, [bp+var_4]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, byte ptr unk_1CAF0[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//push	ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, byte ptr unk_1CB38[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//push	ax
                                throw new System.NotSupportedException();//push	cs
                                throw new System.NotSupportedException();//call	near ptr sub_74730
                                throw new System.NotSupportedException();//or	al, al
                                throw new System.NotSupportedException();//jz	loc_74FF4

                              

                                DaxBlock tmp = ((var_3 & 1) == 0 )? gbl.combat_icons[24,1]:gbl.combat_icons[25,0];

                                seg040.sub_E353(gbl.overlayLines, tmp, 5, 0, (gbl.playerScreenY[var_1] + var_5) * 3,(gbl.playerScreenX[var_1] + var_4) * 3);
                            }
                            //loc_74FF4:
                        }

                        seg040.DrawOverlay();

                        seg049.SysDelay(10);
                    }

                    if (player.actions.field_13 == 0)
                    {
                        gbl.byte_1D1BB++;

                        gbl.unk_1D183[gbl.byte_1D1BB].field_6 = gbl.stru_1D1BC[var_6, var_7];

                        if (gbl.stru_1D1BC[var_6, var_7] != 0x1E)
                        {
                            gbl.stru_1D1BC[var_6, var_7] = 0x1F;
                        }

                        gbl.unk_1D183[gbl.byte_1D1BB].field_0 = player;
                        gbl.unk_1D183[gbl.byte_1D1BB].mapX = var_6;
                        gbl.unk_1D183[gbl.byte_1D1BB].mapY = var_7;
                    }

                    seg041.GameDelay();
                    sub_74572(var_1, 0, 0);

                    gbl.stru_1C9CD[get_player_index(player)].field_3 = 0;

                    sub_743E7();


                    sub_749DD(8, 3, gbl.stru_1D1BC.mapScreenTopY + 3, gbl.stru_1D1BC.mapScreenLeftX + 3);

                    player.actions.delay = 0;
                    player.actions.move = 0;
                    player.actions.spell_id = 0;
                    player.actions.guarding = false;
                }
            }
        }


        internal static byte sub_7515A(byte arg_0, int arg_2, int arg_4, Player player)
        {
            byte var_9;
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte ret_val;

            if (gbl.game_state == 5)
            {
                ret_val = 0;

                var_2 = get_player_index(player);

                gbl.stru_1C9CD[var_2].field_3 = (byte)(player.field_DE & 0x7F);
                gbl.stru_1C9CD[var_2].xPos = arg_4;
                gbl.stru_1C9CD[var_2].yPos = arg_2;

                sub_74D04(out var_8, out var_7, out var_5, out var_4, 8, player);

                if (var_4 != 0 ||
                    var_5 == 0 ||
                    gbl.unk_189B4[var_5].field_0 == 0xff)
                {
                    gbl.stru_1C9CD[var_2].field_3 = 0;
                }
                else
                {
                    ret_val = 1;

                    if (arg_0 != 0 &&
                        player.actions.field_13 == 0)
                    {
                        var_9 = gbl.byte_1D1BB;

                        for (var_3 = 1; var_3 <= var_9; var_3++)
                        {
                            if (gbl.unk_1D183[var_3].field_0 == player)
                            {
                                if (gbl.unk_1D183[var_3].field_6 != 0x1F)
                                {
                                    var_5 = gbl.unk_1D183[var_3].field_6;
                                }

                                gbl.unk_1D183[var_3].field_0 = null;
                                gbl.unk_1D183[var_3].mapX = 0;
                                gbl.unk_1D183[var_3].mapY = 0;
                                gbl.unk_1D183[var_3].field_6 = 0;
                            }
                        }

                        var_6 = 0;
                        var_9 = gbl.byte_1D1BB;

                        for (var_3 = 1; var_3 <= var_9; var_3++)
                        {
                            if (gbl.unk_1D183[var_3].field_0 != null &&
                                gbl.unk_1D183[var_3].mapX == arg_4 &&
                                gbl.unk_1D183[var_3].mapY == arg_2)
                            {
                                var_6 = 1;
                            }
                        }

                        if (var_6 == 0)
                        {

                            gbl.stru_1D1BC[arg_4, arg_2] = var_5;
                        }
                    }

                    sub_743E7();
                }
            }
            else
            {
                ret_val = 1;
            }

            return ret_val;
        }


        internal static void sub_75356(byte arg_0, byte arg_2, Player player)
        {
            gbl.stru_1D1BC.field_4 = arg_0;
            gbl.stru_1D1BC.field_5 = gbl.stru_1C9CD[get_player_index(player)].field_3;

            if (gbl.byte_1D910 == true)
            {
                sub_749DD(8, arg_2, PlayerMapYPos(player), PlayerMapXPos(player));
            }

            gbl.stru_1D1BC.field_4 = 0;
            gbl.stru_1D1BC.field_5 = 1;
        }
    }
}
