using Classes;
using Classes.Combat;

namespace engine
{
    class ovr033
    {
        const int MaxSize = 4;

        static Point[][] Steps = new Point[][] {
            new Point[] { },
            new Point[] { new Point( 0, 0 ) },
            new Point[] { new Point( 0, 0 ), new Point(  0,  1 ) },
            new Point[] { new Point( 0, 0 ), new Point(  1,  0 ) },
            new Point[] { new Point( 0, 0 ), new Point(  1,  0 ), new Point( 0,  1 ), new Point(  1,  1 ) }   
        };

        internal static Point[] GetSizeBasedMapDeltas(int size) /*sub_7400F*/
        {
            return Steps[size];
        }

        internal static Point[] BuildSizeMap(int size, Point pos)
        {
            var map = new System.Collections.Generic.List<Point>();
            foreach (var delta in Steps[size])
            {
                map.Add(pos + delta);
            }

            return map.ToArray();
        }


        static void calculatePlayerScreenPositions() /* sub_74077 */
        {
            for (int i = 1; i <= gbl.CombatantCount; i++)
            {
                gbl.CombatMap[i].screenPos = gbl.CombatMap[i].pos - gbl.mapToBackGroundTile.mapScreenTopLeft;
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


        internal static void sub_7416E(Point pos) // sub_7416E
        {
            var screenPos = pos - gbl.mapToBackGroundTile.mapScreenTopLeft;

            var map = BuildSizeMap(gbl.mapToBackGroundTile.size, new Point(0, 0));

            foreach (var p in map)
            {
                if (CoordOnScreen(screenPos + p) == true)
                {
                    int i = gbl.mapToBackGroundTile[p + pos];

                    ovr034.DrawIsoTile(gbl.BackGroundTiles[i].tile_index, (screenPos.y + p.y) * 3, (screenPos.x + p.x) * 3);

                    if (gbl.mapToBackGroundTile.drawTargetCursor == true)
                    {
                        // draws grey focus box
                        ovr034.draw_combat_icon(0x19, Icon.Normal, 0, screenPos.y + p.y, screenPos.x + p.x);
                    }
                }
            }

            int player_index = PlayerIndexAtMapXY(pos.y, pos.x);

            DrawPlayerIconIfOnScreen(player_index);
        }

        private static void DrawPlayerIconIfOnScreen(int player_index)
        {
            if (player_index > 0 &&
                PlayerOnScreen(false, player_index) == true)
            {
                // draws the player icon over focus box
                ovr034.draw_combat_icon(gbl.player_array[player_index].icon_id, 
                    Icon.Normal,
                    gbl.player_array[player_index].actions.direction,
                    gbl.CombatMap[player_index].screenPos.y,
                    gbl.CombatMap[player_index].screenPos.x);
            }
        }


        internal static void RedrawPosition(Point pos) // sub_7431C
        {
            int playerIndex = PlayerIndexAtMapXY(pos.y, pos.x);

            RedrawPlayerBackground(playerIndex, pos);

            DrawPlayerIconIfOnScreen(playerIndex);
        }

        static int[,] mapToPlayerIndex = new int[Point.MapMaxY, Point.MapMaxX];

        internal static void setup_mapToPlayerIndex_and_playerScreen() /* sub_743E7 */
        {
            for (int y = 0; y < Point.MapMaxY; y++)
            {
                for (int x = 0; x < Point.MapMaxX; x++)
                {
                    mapToPlayerIndex[y, x] = 0;
                }
            }

            for (int index = 1; index <= gbl.CombatantCount; index++)
            {
                var combatantMap = gbl.CombatMap[index];
                if (combatantMap.size > 0)
                {
                    foreach (var pos in BuildSizeMap(combatantMap.size, combatantMap.pos))
                    {
                        if (pos.x < Point.MapMaxX && pos.y < Point.MapMaxY)
                        {
                            mapToPlayerIndex[pos.y, pos.x] = index;
                        }
                    }

                    combatantMap.screenPos = combatantMap.pos - gbl.mapToBackGroundTile.mapScreenTopLeft;
                }
            }
        }

        internal static int PlayerIndexAtMapXY(int posY, int posX) /* sub_74505 */
        {
            if (posX >= Point.MapMinX && posX < Point.MapMaxX &&
                posY >= Point.MapMinY && posY < Point.MapMaxY)
            {
                return mapToPlayerIndex[posY, posX];
            }
            else
            {
                return 0;
            }
        }

        internal static void AtMapXY(out int groundTile, out int playerIndex, Point pos)
        {
            AtMapXY(out groundTile, out playerIndex, pos.y, pos.x);
        }

        internal static void AtMapXY(out int groundTile, out int playerIndex, int posY, int posX) /* sub_74505 */
        {
            if (gbl.mapToBackGroundTile != null &&
                posX >= Point.MapMinX && posX < Point.MapMaxX &&
                posY >= Point.MapMinY && posY < Point.MapMaxY)
            {
                groundTile = gbl.mapToBackGroundTile[posX, posY];
                playerIndex = mapToPlayerIndex[posY, posX];
            }
            else
            {
                playerIndex = 0;
                groundTile = 0;
            }
        }


        internal static void RedrawPlayerBackground(int player_index, Point map) /* sub_74572 */
        {
            var screen = map - gbl.mapToBackGroundTile.mapScreenTopLeft;

            if (player_index > 0)
            {
                RedrawPlayerBackground(player_index);
            }
            else if (CoordOnScreen(screen) == true)
            {
                var tileIdx = gbl.mapToBackGroundTile[map];
                ovr034.DrawIsoTile(gbl.BackGroundTiles[tileIdx].tile_index, screen.y * 3, screen.x * 3);
            }
        }

        internal static void RedrawPlayerBackground(int player_index) /* sub_74572 */
        {
            if (player_index != 0)
            {
                var combatmap = gbl.CombatMap[player_index];
                var screen = combatmap.screenPos;

                var map = screen + gbl.mapToBackGroundTile.mapScreenTopLeft;

                int size = gbl.CombatMap[player_index].size;

                foreach (var delta in GetSizeBasedMapDeltas(size))
                {
                    if (CoordOnScreen(delta + screen) == true)
                    {
                        int tileIdx = gbl.mapToBackGroundTile[map + delta];
                        //THIS DRAWS BACKGROUND MAP.
                        ovr034.DrawIsoTile(gbl.BackGroundTiles[tileIdx].tile_index, (screen.y + delta.y) * 3, (screen.x + delta.x) * 3);
                    }
                }
            }
        }


        internal static bool CoordOnScreen(Point pos) /* sub_74730 */
        {
            return (pos.x >= 0 && pos.x <= Point.ScreenMaxX && pos.y >= 0 && pos.y <= Point.ScreenMaxY);
        }


        internal static bool PlayerOnScreen(bool AllOnScreen, Player player) // sub_74761
        {
            int player_index = GetPlayerIndex(player);

            return PlayerOnScreen(AllOnScreen, player_index);
        }


        internal static bool PlayerOnScreen(bool AllOnScreen, int player_index) // sub_74761
        {
            var combatmap = gbl.CombatMap[player_index];
            if (combatmap.size == 0)
            {
                return false;
            }

            bool result = true;

            foreach (var pos in BuildSizeMap(combatmap.size, combatmap.screenPos))
            {
                if (CoordOnScreen(pos) == false)
                {
                    result = false;
                    if (AllOnScreen == true)
                    {
                        return false;
                    }
                }
                else
                {
                    result = true;
                    if (AllOnScreen == false)
                    {
                        return true;
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
        internal static bool ScreenMapCheck(int radius, Point pos)
        {
            Point screenCentre = gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter;

            int var_2 = (radius == 0xff) ? 0 : radius;

            int minX = screenCentre.x - var_2;
            int maxX = screenCentre.x + var_2;

            int minY = screenCentre.y - var_2;
            int maxY = screenCentre.y + var_2;

            if (radius == 0xff ||
                pos.x < minX ||
                pos.x > maxX ||
                pos.y < minY ||
                pos.y > maxY)
            {
                if (pos.x < minX)
                {
                    while (pos.x < screenCentre.x && screenCentre.x > (Point.MapMinX + Point.ScreenHalfX))
                    {
                        screenCentre.x -= 1;
                    }
                }
                else if (pos.x > maxX)
                {
                    while (pos.x > screenCentre.x && screenCentre.x < (Point.MapMaxX - Point.ScreenHalfX - 1))
                    {
                        screenCentre.x += 1;
                    }
                }

                if (pos.y < minY)
                {
                    while (pos.y < screenCentre.y && screenCentre.y > (Point.MapMinY + Point.ScreenHalfY))
                    {
                        screenCentre.y -= 1;
                    }
                }
                else if (pos.y > maxY)
                {
                    while (pos.y > screenCentre.y && screenCentre.y < (Point.MapMaxY - Point.ScreenHalfY - 1))
                    {
                        screenCentre.y += 1;
                    }
                }

                gbl.mapToBackGroundTile.mapScreenTopLeft = screenCentre - Point.ScreenCenter;

                int screenRowY = 0;
                int mapY = gbl.mapToBackGroundTile.mapScreenTopLeft.y;
                const int IconColumnSize = 3;

                for (int i = 0; i <= 6; i++)
                {
                    int screenColX = 0;
                    int mapX = gbl.mapToBackGroundTile.mapScreenTopLeft.x;

                    for (int j = 0; j <= 6; j++)
                    {
                        ovr034.DrawIsoTile(gbl.BackGroundTiles[gbl.mapToBackGroundTile[mapX, mapY]].tile_index, screenRowY, screenColX);

                        screenColX += IconColumnSize;
                        mapX++;
                    }
                    screenRowY += IconColumnSize;
                    mapY++;
                }
                calculatePlayerScreenPositions();

                return true;
            }

            return false;
        }


        internal static void redrawCombatArea(int dir, int radius, Point map) /*sub_749DD*/
        {
            var newPos = map + gbl.MapDirectionDelta[dir];

            if (ScreenMapCheck(radius, newPos) == true)
            {
                for (int index = 1; index <= gbl.CombatantCount; index++)
                {
                    Player player = gbl.player_array[index];

                    if (player.in_combat == true &&
                        gbl.CombatMap[index].size > 0 &&
                        PlayerOnScreen(false, player) == true)
                    {
                        var pos = gbl.CombatMap[index].screenPos;
                        ovr034.draw_combat_icon(player.icon_id, 0, player.actions.direction, pos.y, pos.x);
                    }
                }
            }

            RedrawPosition(map);

            if (CoordOnScreen(newPos - gbl.mapToBackGroundTile.mapScreenTopLeft) == false)
            {
                newPos.MapBoundaryTrunc();
            }

            sub_7416E(newPos);
            seg040.DrawOverlay();
        }


        internal static void draw_74B3F(bool arg_0, Icon iconState, int direction, Player player) /* sub_74B3F */
        {
            int player_index = GetPlayerIndex(player);

            if (PlayerOnScreen(true, player) == false &&
                gbl.focusCombatAreaOnPlayer == true)
            {
                redrawCombatArea(8, 3, PlayerMapPos(player));
            }

            if ((direction >> 2) != (player.actions.direction >> 2) ||
                iconState == Icon.Attack ||
                arg_0 == true)
            {
                if (gbl.focusCombatAreaOnPlayer == true)
                {
                    RedrawPlayerBackground(player_index);
                }
            }

            player.actions.direction = direction;

            if (arg_0 == false &&
                PlayerOnScreen(false, player) == true &&
                gbl.focusCombatAreaOnPlayer == true)
            {
                var pos = gbl.CombatMap[player_index].screenPos;
                ovr034.draw_combat_icon(player.icon_id, iconState, direction, pos.y, pos.x);
                seg040.DrawOverlay();
            }
        }

        internal static Point PlayerMapPos(Player player) /* sub_74C5A */
        {
            return gbl.CombatMap[GetPlayerIndex(player)].pos;
        }


        internal static int PlayerMapSize(Player player) /*sub_74C82*/
        {
            return gbl.CombatMap[GetPlayerIndex(player)].size;
        }


        internal static int GetPlayerIndex(Player player)
        {
            int index = System.Array.FindIndex(gbl.player_array, p => p == player);

            if (index == -1)
            {
                index = 0;
            }

            return index;
        }


        internal static void getGroundInformation(out int groundTile, out int playerIndex, byte direction, Player player) /* sub_74D04 */
        {
            playerIndex = 0;
            groundTile = 0x17;

            byte maxMoveCost = 1;
            int currentPlayerIndex = GetPlayerIndex(player);

            var map = gbl.CombatMap[currentPlayerIndex];
            var playerPos = map.pos;

            foreach (var pos in BuildSizeMap(map.size, map.pos))
            {
                var tmpPos = pos + gbl.MapDirectionDelta[direction];

                int atGroundTile;
                int atPlayerIdx;

                AtMapXY(out atGroundTile, out atPlayerIdx, tmpPos);

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
                    if (gbl.BackGroundTiles[atGroundTile].move_cost >= maxMoveCost)
                    {
                        maxMoveCost = gbl.BackGroundTiles[atGroundTile].move_cost;
                        groundTile = atGroundTile;
                    }
                }
            }
        }

        internal static void getGroundInformation(out bool isPoisonousCloud, out bool isNoxiousCloud, out int groundTile, out int playerIndex, int direction, Player player) /* sub_74D04 */
        {
            playerIndex = 0;
            groundTile = 0x17;
            isNoxiousCloud = false;
            isPoisonousCloud = false;

            byte maxMoveCost = 1;
            int currentPlayerIndex = GetPlayerIndex(player);

            var map = gbl.CombatMap[currentPlayerIndex];
            var playerPos = map.pos;

            foreach (var pos in BuildSizeMap(map.size, map.pos))
            {
                var tmpPos = pos + gbl.MapDirectionDelta[direction];

                int atGroundTile;
                int atPlayerIdx;

                AtMapXY(out atGroundTile, out atPlayerIdx, tmpPos);

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


        internal static void CombatantKilled(Player player) // sub_74E6F
        {
            if (gbl.game_state != GameState.Combat)
            {
                seg044.PlaySound(Sound.sound_5);
                seg041.GameDelay();
            }
            else
            {
                bool downedPlayer = gbl.downedPlayers.Exists(cell => cell.target == player);

                if (!downedPlayer)
                {
                    int player_index = GetPlayerIndex(player);
                    var map = ovr033.PlayerMapPos(player);

                    if (PlayerOnScreen(true, player) == false)
                    {
                        redrawCombatArea(8, 3, map);
                    }

                    RedrawPlayerBackground(player_index);
                    seg044.PlaySound(Sound.sound_5);

                    // Draw skull overlay
                    DaxBlock attackIcon = gbl.combat_icons[24].GetIcon(Icon.Attack, 0);
                    DaxBlock normalIcon = gbl.combat_icons[25].GetIcon(Icon.Normal, 0);
                    var points = BuildSizeMap(gbl.CombatMap[player_index].size, gbl.CombatMap[player_index].screenPos);
                    for (int var_3 = 0; var_3 <= 8; var_3++)
                    {
                        foreach (var pos in points)
                        {
                            if (CoordOnScreen(pos) == true)
                            {
                                DaxBlock tmp = ((var_3 & 1) == 0) ? attackIcon : normalIcon;

                                seg040.OverlayBounded(tmp, 5, 0, (pos.y) * 3, (pos.x) * 3);
                            }
                        }

                        seg040.DrawOverlay();
                        seg049.SysDelay(10);
                    }

                    // Add downed corpse for team players.
                    if (player.actions.nonTeamMember == false)
                    {
                        var b = new Struct_1D183();

                        gbl.downedPlayers.Add(b);
                        b.originalBackgroundTile = gbl.mapToBackGroundTile[map];
                        b.target = player;
                        b.map = map;

                        if (gbl.mapToBackGroundTile[map] != 0x1E)
                        {
                            gbl.mapToBackGroundTile[map] = 0x1F;
                        }
                    }


                    // clean-up combat stuff
                    seg041.GameDelay();
                    RedrawPlayerBackground(player_index);

                    gbl.CombatMap[GetPlayerIndex(player)].size = 0;

                    setup_mapToPlayerIndex_and_playerScreen();

                    redrawCombatArea(8, 3, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);

                    player.actions.delay = 0;
                    player.actions.move = 0;
                    player.actions.spell_id = 0;
                    player.actions.guarding = false;
                }
            }
        }


        internal static bool sub_7515A(bool arg_0, Point pos, Player player)
        {
            bool ret_val;

            if (gbl.game_state == GameState.Combat)
            {
                ret_val = false;

                int player_index = GetPlayerIndex(player);

                gbl.CombatMap[player_index].size = (byte)(player.field_DE & 0x7F);
                gbl.CombatMap[player_index].pos = pos;

                int playerIdx;
                int ground_tile;
                getGroundInformation(out ground_tile, out playerIdx, 8, player);

                if (playerIdx != 0 ||
                    ground_tile == 0 ||
                    gbl.BackGroundTiles[ground_tile].move_cost == 0xff)
                {
                    gbl.CombatMap[player_index].size = 0;
                }
                else
                {
                    ret_val = true;

                    if (arg_0 == true &&
                        player.actions.nonTeamMember == false)
                    {
                        var downed = gbl.downedPlayers.FindLast(cell => cell.target == player && cell.originalBackgroundTile != 0x1f);
                        if (downed != null)
                        {
                            ground_tile = downed.originalBackgroundTile;
                        }
                        gbl.downedPlayers.RemoveAll(cell => cell.target == player);

                        bool found = gbl.downedPlayers.Exists(cell => cell.target != null && cell.map == pos);

                        if (found == false)
                        {
                            gbl.mapToBackGroundTile[pos] = ground_tile;
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


        internal static void RedrawCombatIfFocusOn(bool draw_cursor, byte radius, Player player) // sub_75356
        {
            gbl.mapToBackGroundTile.drawTargetCursor = draw_cursor;
            gbl.mapToBackGroundTile.size = gbl.CombatMap[GetPlayerIndex(player)].size;

            if (gbl.focusCombatAreaOnPlayer == true)
            {
                redrawCombatArea(8, radius, PlayerMapPos(player));
            }

            gbl.mapToBackGroundTile.drawTargetCursor = false;
            gbl.mapToBackGroundTile.size = 1;
        }
    }
}
