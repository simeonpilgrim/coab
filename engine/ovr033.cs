using Classes;

namespace engine
{
    class ovr033
    {
        const int MaxSize = 4;
        static int[, ,] unk_xxxx = new int[4, MaxSize, 2] { 
            { { 0, 0 }, { -1, -1 }, { -1, -1 }, { -1, -1 } },
            { { 0, 0 }, {  0,  1 }, { -1, -1 }, { -1, -1 } },
            { { 0, 0 }, {  1,  0 }, { -1, -1 }, { -1, -1 } },
            { { 0, 0 }, {  1,  0 }, {  0,  1 }, {  1,  1 } },
        };

        internal static bool GetSizeBasedMapDelta(out int deltaY, out int deltaX, int step, int size) /*sub_7400F*/
        {
            bool var_1 = false;
            deltaY = 0; /* Added because of 'out' attribute */
            deltaX = 0; /* Added because of 'out' attribute */

            if (size != 0)
            {
                deltaX = unk_xxxx[size - 1, step, 0];
                deltaY = unk_xxxx[size - 1, step, 1];

                if (deltaX >= 0)
                {
                    var_1 = true;
                }
            }

            return var_1;
        }


        static void calculatePlayerScreenPositions() /* sub_74077 */
        {
            int playerCount = gbl.CombatantCount;

            for (int i = 1; i <= playerCount; i++)
            {
                gbl.playerScreenX[i] = gbl.CombatMap[i].xPos - gbl.mapToBackGroundTile.mapScreenLeftX;
                gbl.playerScreenY[i] = gbl.CombatMap[i].yPos - gbl.mapToBackGroundTile.mapScreenTopY;
            }
        }


        internal static void Color_0_8_inverse()
        {
            seg040.SetPaletteColor(8, 0);
            seg040.SetPaletteColor(0, 8);
        }


        internal static void Color_0_8_normal()
        {
            seg040.SetPaletteColor(0, 0);
            seg040.SetPaletteColor(8, 8);
        }


        internal static void sub_7416E(int posY, int posX)
        {
            int screenPosX = posX - gbl.mapToBackGroundTile.mapScreenLeftX;
            int screenPosY = posY - gbl.mapToBackGroundTile.mapScreenTopY;

            for (int step = 0; step <= 3; step++)
            {
                int deltaY;
                int deltaX;

                if (GetSizeBasedMapDelta(out deltaY, out deltaX, step, gbl.mapToBackGroundTile.size) == true &&
                    CoordOnScreen(screenPosY + deltaY, screenPosX + deltaX) == true)
                {
                    int i = gbl.mapToBackGroundTile[deltaX + posX, deltaY + posY];

                    ovr034.draw_iso_title(0, gbl.BackGroundTiles[i].tile_index, (screenPosY + deltaY) * 3, (screenPosX + deltaX) * 3);

                    if (gbl.mapToBackGroundTile.draw_target_cursor == true)
                    {
                        // draws grey focus box
                        ovr034.draw_combat_icon(0x19, 0, 0, screenPosY + deltaY, screenPosX + deltaX);
                    }
                }
            }

            byte ground_tile;
            byte player_index;

            AtMapXY(out ground_tile, out player_index, posY, posX);

            if (player_index > 0 &&
                sub_74761(false, gbl.player_array[player_index]) == true)
            {
                // draws the player icon over focus box
                ovr034.draw_combat_icon(gbl.player_array[player_index].icon_id, 0,
                    gbl.player_array[player_index].actions.direction, 
                    gbl.playerScreenY[player_index],
                    gbl.playerScreenX[player_index]);
            }
        }


        internal static void sub_7431C(int mapX, int mapY)
        {
            byte groundTile;
            byte playerIndex;

            int newMapX = mapY - gbl.mapToBackGroundTile.mapScreenLeftX;
            int newMapY = mapX - gbl.mapToBackGroundTile.mapScreenTopY;

            draw_74572(0, newMapY, newMapX);
            AtMapXY(out groundTile, out playerIndex, mapX, mapY);

            if (playerIndex > 0 &&
                sub_74761(false, gbl.player_array[playerIndex]) == true)
            {
                ovr034.draw_combat_icon(gbl.player_array[playerIndex].icon_id,  0, 
                    gbl.player_array[playerIndex].actions.direction,
                    gbl.playerScreenY[playerIndex], 
                    gbl.playerScreenX[playerIndex]);
            }
        }

        static Struct_1D1BC mapToPlayerIndex = new Struct_1D1BC(); /*unk_1CB81*/

        internal static void setup_mapToPlayerIndex_and_playerScreen() /* sub_743E7 */
        {
            mapToPlayerIndex.SetField_7(0x00);

            for (int index = 1; index <= gbl.CombatantCount; index++)
            {
                if (gbl.CombatMap[index].size > 0)
                {
                    for (int step = 0; step <= 3; step++)
                    {
                        int deltaY;
                        int deltaX;
                        if (GetSizeBasedMapDelta(out deltaY, out deltaX, step, gbl.CombatMap[index].size) == true)
                        {
                            int xPos = gbl.CombatMap[index].xPos + deltaX;
                            int yPos = gbl.CombatMap[index].yPos + deltaY;

                            mapToPlayerIndex[xPos, yPos] = index;
                        }
                    }

                    gbl.playerScreenX[index] = gbl.CombatMap[index].xPos - gbl.mapToBackGroundTile.mapScreenLeftX;
                    gbl.playerScreenY[index] = gbl.CombatMap[index].yPos - gbl.mapToBackGroundTile.mapScreenTopY;
                }
            }
        }


        internal static void AtMapXY(out byte groundTile, out byte playerIndex, int posY, int posX) /* sub_74505 */
        {
            if (posX >= 0 && posX <= 0x31 &&
                posY >= 0 && posY <= 0x18 )
            {
                groundTile = (byte)gbl.mapToBackGroundTile[posX, posY];
                playerIndex = (byte)mapToPlayerIndex[posX, posY];
            }
            else
            {
                playerIndex = 0;
                groundTile = 0;
            }
        }


        internal static void draw_74572(byte player_index, int mapY, int mapX) /* sub_74572 */
        {
            int screenY = -120; /*Simeon*/
            int screenX = -120; /*Simeon*/

            if (player_index == 0)
            {
                screenX = mapX + gbl.mapToBackGroundTile.mapScreenLeftX;
                screenY = mapY + gbl.mapToBackGroundTile.mapScreenTopY;

                byte dummyGroundTile;
                AtMapXY(out dummyGroundTile, out player_index, screenY, screenX);
            }
            
            if (player_index > 0)
            {
                mapX = gbl.playerScreenX[player_index];
                mapY = gbl.playerScreenY[player_index];

                screenX = mapX + gbl.mapToBackGroundTile.mapScreenLeftX;
                screenY = mapY + gbl.mapToBackGroundTile.mapScreenTopY;

                int size = gbl.CombatMap[player_index].size;

                for (int size_step = 0; size_step < MaxSize; size_step++)
                {
                    int deltaY;
                    int deltaX;

                    if (GetSizeBasedMapDelta(out deltaY, out deltaX, size_step, size) == true &&
                        CoordOnScreen(deltaY + mapY, deltaX + mapX) == true)
                    {
                        int i1 = gbl.mapToBackGroundTile[screenX + deltaX, deltaY + screenY];
                        //THIS DRAWS BACKGROUND MAP.
                        ovr034.draw_iso_title(0, gbl.BackGroundTiles[i1].tile_index, (mapY + deltaY) * 3, (mapX + deltaX) * 3);
                    }
                }
            }
            else if ( CoordOnScreen(mapY, mapX) == true )
            {
                ovr034.draw_iso_title(0, gbl.BackGroundTiles[gbl.mapToBackGroundTile[screenX, screenY]].tile_index, mapY * 3, mapX * 3);
            }
        }


        internal static bool CoordOnScreen(int screenY, int screenX) /* sub_74730 */
        {
            return (screenX >= 0 && screenX <= 6 && screenY >= 0 && screenY <= 6);
        }


        internal static bool sub_74761(bool arg_0, Player player)
        {
            bool result = true;

            int player_index = get_player_index(player);

            if (gbl.CombatMap[player_index].size == 0)
            {
                result = false;
            }
            else
            {
                for (int step = 0; step <= 3; step++)
                {
                    int deltaY;
                    int deltaX;

                    if (GetSizeBasedMapDelta(out deltaY, out deltaX, step, gbl.CombatMap[player_index].size) == true)
                    {
                        if (CoordOnScreen(gbl.playerScreenY[player_index] + deltaY, gbl.playerScreenX[player_index] + deltaX) == false)
                        {
                            result = false;
                            if (arg_0 == true)
                            {
                                break;
                            }
                        }
                        else
                        {
                            result = true;
                            if (arg_0 == false)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// check's a given position is visable within the current display screen (via a radius)
        /// </summary>
        /// <param name="radius">if this is 0xff the re-check is forced</param>
        /// <returns></returns>
        static bool ScreenMapCheck(byte radius, int yPos, int xPos)
        {
            int screenCentreX = gbl.mapToBackGroundTile.mapScreenLeftX + 3;
            int screenCentreY = gbl.mapToBackGroundTile.mapScreenTopY + 3;

            int var_2 = radius;

            if (radius == 0xff)
            {
                var_2 = 0;
            }

            int leftX = screenCentreX - var_2;
            int rightX = screenCentreX + var_2;

            int topY = screenCentreY - var_2;
            int bottomY = screenCentreY + var_2;

            if (radius == 0xff ||
                xPos < leftX ||
                xPos > rightX ||
                yPos < topY ||
                yPos > bottomY)
            {
                if (xPos < leftX)
                {
                    while (xPos < screenCentreX && screenCentreX > 3)
                    {
                        screenCentreX -= 1;
                    }
                }
                else if (xPos > rightX)
                {
                    while (xPos > screenCentreX && screenCentreX < 0x2E)
                    {
                        screenCentreX += 1;
                    }
                }

                if (yPos < topY)
                {
                    while (yPos < screenCentreY && screenCentreY > 3)
                    {
                        screenCentreY -= 1;
                    }
                }
                else if (yPos > bottomY)
                {
                    while (yPos > screenCentreY && screenCentreY < 0x15)
                    {
                        screenCentreY += 1;
                    }
                }

                gbl.mapToBackGroundTile.mapScreenLeftX = screenCentreX - 3;
                gbl.mapToBackGroundTile.mapScreenTopY = screenCentreY - 3;

                int screenRowY = 0;
                int mapX = gbl.mapToBackGroundTile.mapScreenTopY;
                const int IconColumnSize = 3;

                for (int i = 0; i <= 6; i++)
                {
                    int screenColX = 0;
                    int mapY = gbl.mapToBackGroundTile.mapScreenLeftX;

                    for (int j = 0; j <= 6; j++)
                    {
                        ovr034.draw_iso_title(0, gbl.BackGroundTiles[gbl.mapToBackGroundTile[mapY, mapX]].tile_index, screenRowY, screenColX);

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


        internal static void redrawCombatArea(byte dir, byte radius, int mapY, int mapX) /*sub_749DD*/
        {
            int newXPos = mapX + gbl.MapDirectionXDelta[dir];
            int newYPos = mapY + gbl.MapDirectionYDelta[dir];

            if (ScreenMapCheck(radius, newYPos, newXPos) == true)
            {
                for (int index = 1; index <= gbl.CombatantCount; index++)
                {
                    Player player = gbl.player_array[index];

                    if (player.in_combat == true &&
                        gbl.CombatMap[index].size > 0 &&
                        sub_74761(false, player) == true)
                    {
                        ovr034.draw_combat_icon(player.icon_id, 0, player.actions.direction, gbl.playerScreenY[index], gbl.playerScreenX[index]);
                    }
                }
            }

            sub_7431C(mapY, mapX);

            if (CoordOnScreen(newYPos - gbl.mapToBackGroundTile.mapScreenTopY, newXPos - gbl.mapToBackGroundTile.mapScreenLeftX) == false)
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


        internal static void draw_74B3F(byte arg_0, byte arg_2, byte direction, Player player) /* sub_74B3F */
        {
            byte player_index = get_player_index(player);

            if (sub_74761(true, player) == false &&
                gbl.byte_1D910 == true)
            {
                redrawCombatArea(8, 3, PlayerMapYPos(player), PlayerMapXPos(player));
            }

            if ((direction >> 2) != (player.actions.direction >> 2) ||
                arg_2 != 0 ||
                arg_0 != 0)
            {
                if (gbl.byte_1D910 == true)
                {
                    draw_74572(player_index, 0, 0);
                }
            }

            player.actions.direction = direction;

            if (arg_0 == 0 &&
                sub_74761(false, player) == true &&
                gbl.byte_1D910 == true)
            {
                ovr034.draw_combat_icon(player.icon_id, arg_2, direction, gbl.playerScreenY[player_index], gbl.playerScreenX[player_index]);
                seg040.DrawOverlay();
            }
        }


        internal static int PlayerMapXPos(Player player) /* sub_74C32 */
        {
            return gbl.CombatMap[get_player_index(player)].xPos;
        }


        internal static int PlayerMapYPos(Player player) /* sub_74C5A */
        {
            return gbl.CombatMap[get_player_index(player)].yPos;
        }


        internal static int PlayerMapSize(Player player) /*sub_74C82*/
        {
            return gbl.CombatMap[get_player_index(player)].size;
        }


        internal static byte get_player_index(Player player)
        {
            bool found = false;
            byte loop_var = 0;
            do
            {
                loop_var++;

                if (gbl.player_array[loop_var] == player)
                {
                    found = true;
                }

            } while (found == false && loop_var <= gbl.CombatantCount);

            byte ret_val = (found == true) ? loop_var : (byte)0;

            return ret_val;
        }


        internal static void getGroundInformation(out bool isPoisonousCloud, out bool isNoxiousCloud, out byte groundTile, out byte playerIndex, byte direction, Player player) /* sub_74D04 */
        {
            playerIndex = 0;
            groundTile = 0x17;
            isNoxiousCloud = false;
            isPoisonousCloud = false;

            byte maxMoveCost = 1;
            byte currentPlayerIndex = get_player_index(player);

            int playerPosX = gbl.CombatMap[currentPlayerIndex].xPos;
            int playerPosY = gbl.CombatMap[currentPlayerIndex].yPos;

            for (int step = 0; step <= 3; step++)
            {
                int deltaY;
                int deltaX;

                if (GetSizeBasedMapDelta(out deltaY, out deltaX, step, gbl.CombatMap[currentPlayerIndex].size) == true)
                {
                    int posX = playerPosX + deltaX + gbl.MapDirectionXDelta[direction];
                    int posY = playerPosY + deltaY + gbl.MapDirectionYDelta[direction];

                    byte atGroundTile;
                    byte atPlayerIdx;

                    AtMapXY(out atGroundTile, out atPlayerIdx, posY, posX);

                    if (atPlayerIdx == currentPlayerIndex)
                    {
                        atPlayerIdx = 0;
                    }

                    if (atPlayerIdx > 0)
                    {
                        playerIndex = atPlayerIdx;
                    }

                    if (atGroundTile == 0)
                    {
                        groundTile = 0;
                    }
                    else if (groundTile != 0)
                    {
                        if (atGroundTile == 0x1e) // Noxious_cloud
                        {
                            isNoxiousCloud = true;
                        }
                        else if (atGroundTile == 0x1c) // Poisonous cloud
                        {
                            isPoisonousCloud = true;
                        }
                        else if (gbl.BackGroundTiles[atGroundTile].move_cost >= maxMoveCost)
                        {
                            maxMoveCost = gbl.BackGroundTiles[atGroundTile].move_cost;
                            groundTile = atGroundTile;
                        }
                    }
                }
            }
        }


        internal static void sub_74E6F(Player player)
        {
            if (gbl.game_state != 5)
            {
                seg044.sound_sub_120E0(gbl.sound_5_188C8);
                seg041.GameDelay();
            }
            else
            {
                int var_2 = 1;
                while (var_2 < 9 &&
                    gbl.unk_1D183[var_2].target != player) // First SIS BUG!!!
                {
                    var_2++;
                }

                if (var_2 >= 9)
                {
                    byte player_index = get_player_index(player);
                    int map_x = ovr033.PlayerMapXPos(player);
                    int map_y = ovr033.PlayerMapYPos(player);

                    if (sub_74761(true, player) == false)
                    {
                        redrawCombatArea(8, 3, map_y, map_x);
                    }

                    draw_74572(player_index, 0, 0);
                    seg044.sound_sub_120E0(gbl.sound_5_188C8);

                    for (int var_3 = 0; var_3 <= 8; var_3++)
                    {
                        for (int step = 0; step <= 3; step++)
                        {
                            int delta_y;
                            int delta_x;

                            if (GetSizeBasedMapDelta(out delta_y, out delta_x, step, gbl.CombatMap[player_index].size) == true &&
                                CoordOnScreen(gbl.playerScreenY[player_index] + delta_y, gbl.playerScreenX[player_index] + delta_x) == true)
                            {
                                DaxBlock tmp = ((var_3 & 1) == 0) ? gbl.combat_icons[24, 1] : gbl.combat_icons[25, 0];

                                seg040.OverlayBounded(tmp, 5, 0, (gbl.playerScreenY[player_index] + delta_y) * 3, (gbl.playerScreenX[player_index] + delta_x) * 3);
                            }

                        }

                        seg040.DrawOverlay();
                        seg049.SysDelay(10);
                    }

                    if (player.actions.field_13 == 0)
                    {
                        gbl.byte_1D1BB++;

                        gbl.unk_1D183[gbl.byte_1D1BB].field_6 = (byte)gbl.mapToBackGroundTile[map_x, map_y];

                        if (gbl.mapToBackGroundTile[map_x, map_y] != 0x1E)
                        {
                            gbl.mapToBackGroundTile[map_x, map_y] = 0x1F;
                        }

                        gbl.unk_1D183[gbl.byte_1D1BB].target = player;
                        gbl.unk_1D183[gbl.byte_1D1BB].mapX = map_x;
                        gbl.unk_1D183[gbl.byte_1D1BB].mapY = map_y;
                    }

                    seg041.GameDelay();
                    draw_74572(player_index, 0, 0);

                    gbl.CombatMap[get_player_index(player)].size = 0;

                    setup_mapToPlayerIndex_and_playerScreen();

                    redrawCombatArea(8, 3, gbl.mapToBackGroundTile.mapScreenTopY + 3, gbl.mapToBackGroundTile.mapScreenLeftX + 3);

                    player.actions.delay = 0;
                    player.actions.move = 0;
                    player.actions.spell_id = 0;
                    player.actions.guarding = false;
                }
            }
        }


        internal static bool sub_7515A(bool arg_0, int pos_y, int pos_x, Player player)
        {
            bool ret_val;

            if (gbl.game_state == 5)
            {
                ret_val = false;

                int player_index = get_player_index(player);

                gbl.CombatMap[player_index].size = (byte)(player.field_DE & 0x7F);
                gbl.CombatMap[player_index].xPos = pos_x;
                gbl.CombatMap[player_index].yPos = pos_y;

                byte var_4;
                byte ground_tile;
                bool dummyBoolA, dummyBoolB;
                getGroundInformation(out dummyBoolA, out dummyBoolB, out ground_tile, out var_4, 8, player);

                if (var_4 != 0 ||
                    ground_tile == 0 ||
                    gbl.BackGroundTiles[ground_tile].move_cost == 0xff)
                {
                    gbl.CombatMap[player_index].size = 0;
                }
                else
                {
                    ret_val = true;

                    if (arg_0 == true &&
                        player.actions.field_13 == 0)
                    {
                        for (int i = 1; i <= gbl.byte_1D1BB; i++)
                        {
                            if (gbl.unk_1D183[i].target == player)
                            {
                                if (gbl.unk_1D183[i].field_6 != 0x1F)
                                {
                                    ground_tile = gbl.unk_1D183[i].field_6;
                                }

                                gbl.unk_1D183[i].target = null;
                                gbl.unk_1D183[i].mapX = 0;
                                gbl.unk_1D183[i].mapY = 0;
                                gbl.unk_1D183[i].field_6 = 0;
                            }
                        }

                        bool found = false;

                        for (int i = 1; i <= gbl.byte_1D1BB; i++)
                        {
                            if (gbl.unk_1D183[i].target != null &&
                                gbl.unk_1D183[i].mapX == pos_x &&
                                gbl.unk_1D183[i].mapY == pos_y)
                            {
                                found = true;
                            }
                        }

                        if (found == false)
                        {
                            gbl.mapToBackGroundTile[pos_x, pos_y] = ground_tile;
                        }
                    }

                    setup_mapToPlayerIndex_and_playerScreen();
                }
            }
            else
            {
                ret_val = true;
            }

            return ret_val;
        }


        internal static void sub_75356(bool draw_cursor, byte radius, Player player)
        {
            gbl.mapToBackGroundTile.draw_target_cursor = draw_cursor;
            gbl.mapToBackGroundTile.size = gbl.CombatMap[get_player_index(player)].size;

            if (gbl.byte_1D910 == true)
            {
                redrawCombatArea(8, radius, PlayerMapYPos(player), PlayerMapXPos(player));
            }

            gbl.mapToBackGroundTile.draw_target_cursor = false;
            gbl.mapToBackGroundTile.size = 1;
        }
    }
}
