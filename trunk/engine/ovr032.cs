using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr032
    {
        class MapReach
        {
            public bool reach;
            public Point target;
            public int range;

            public MapReach( bool r, int ra, Point p) 
            {
                reach = r; target = p; range = ra;
            }
        }

        static MapReach[, ,] mapReachCache = new MapReach[Point.MapMaxY * Point.MapMaxX, Point.MapMaxY * Point.MapMaxX, 2];

        //internal static void buildMapCache()
        //{
        //    for (int y1 = 0; y1 < Point.MapMaxY; y1++)
        //    {
        //        for (int x1 = 0; x1 < Point.MapMaxX; x1++)
        //        {
        //            var p1 = new Point(x1,y1);
        //            for (int y2 = 0; y2 < Point.MapMaxY; y2++)
        //            {
        //                for (int x2 = 0; x2 < Point.MapMaxX; x2++)
        //                {
        //                    gbl.mapToBackGroundTile.field_6 = false;
        //                    Point p2 = new Point(x2, y2);
        //                    int range = -1;
        //                    bool reach = canReachTargetCalc(gbl.mapToBackGroundTile, ref range, ref p2, p1);
        //                    
        //                    mapReachCache[(y1 * Point.MapMaxX) + x1, (y2 * Point.MapMaxX) + x2, 0] = new MapReach(reach, range, p2);
        //                    
        //                    gbl.mapToBackGroundTile.field_6 = true;
        //                    p2 = new Point(x2, y2);
        //                    range = -1;
        //                    reach = canReachTargetCalc(gbl.mapToBackGroundTile, ref range, ref p2, p1);
        //
        //                    mapReachCache[(y1 * Point.MapMaxX) + x1, (y2 * Point.MapMaxX) + x2, 1] = new MapReach(reach, range, p2);
        //                }
        //            }
        //        }
        //    }
        //}

        static MapReach MapCacheGet(Point p1, Point p2, int b)
        {
            MapReach mr = mapReachCache[(p2.y * Point.MapMaxX) + p2.x, (p1.y * Point.MapMaxX) + p1.x, b];
            if (mr != null) return mr;

            bool tmp = gbl.mapToBackGroundTile.field_6;
            gbl.mapToBackGroundTile.field_6 = true;

            mr = canReachTargetCalc(gbl.mapToBackGroundTile, p2, p1);

            gbl.mapToBackGroundTile.field_6 = tmp;

            mapReachCache[(p2.y * Point.MapMaxX) + p2.x, (p1.y * Point.MapMaxX) + p1.x, b] = mr;
            return mr;
        }

        internal static void canReachTarget(ref Point target, Point attacker)
        {
            MapReach mr = MapCacheGet(attacker, target, gbl.mapToBackGroundTile.field_6 ? 1 : 0);

            target = new Point(mr.target);
        }

        internal static bool canReachTarget(ref int range, Point target, Point attacker)
        {
            MapReach mr = MapCacheGet(attacker, target, gbl.mapToBackGroundTile.field_6 ? 1 : 0);

            if (mr.range > (range*2)+1)
            {
                return false;
            }
            else
            {
                range = mr.range;
                return mr.reach;
            }
        }

        static MapReach canReachTargetCalc(Struct_1D1BC groundTilesMap, Point outPos, Point attacker) /* sub_733F1 */
        {
            SteppingPath var_31 = new SteppingPath();
            SteppingPath var_19 = new SteppingPath();

            int max_range = (256 * 2) + 1;
            var_19.attacker = attacker;
            var_19.target = outPos;

            var_19.CalculateDeltas();

            var_31.attacker.x = 0;
            var_31.attacker.y = gbl.BackGroundTiles[groundTilesMap[attacker]].field_1;

            if (var_19.diff_x > var_19.diff_y)
            {
                var_31.target.x = var_19.diff_x;
            }
            else
            {
                var_31.target.x = var_19.diff_y;
            }

            var_31.target.y = gbl.BackGroundTiles[groundTilesMap[attacker]].field_1;
            var_31.CalculateDeltas();
            bool finished = false;

            do
            {
                int gt = groundTilesMap[var_19.current];
                Struct_189B4 s189 = gbl.BackGroundTiles[gt];

                if (groundTilesMap.field_6 == false && s189.field_2 > var_31.current.y)
                {
                    return new MapReach(false, var_19.steps, var_19.current);
                }

                // range is for cache hard coded to 256, thus max_range = 513, so skip this.
                if ( var_19.steps > max_range)
                {
                    return new MapReach(false, var_19.steps, var_19.current);
                }

                var_31.Step();
                finished = !var_19.Step();
            } while (finished == false);

            return new MapReach(true, var_19.steps, outPos);
        }

        /// <summary>
        /// Returns if playerB can see playerA
        /// </summary>
        internal static bool CanSeeCombatant(int direction, Point playerA, Point playerB) /* sub_7354A */
        {
            if( playerA.MapInBounds() == false || playerB.MapInBounds() == false )
            {
                return false;
            }

            if (direction == 0xff || direction == 8 )
            {
                return true;
            }

            int facingX = playerB.x + gbl.MapDirectionXDelta[direction];
            int facingY = playerB.y + gbl.MapDirectionYDelta[direction];

            if (playerB == playerA ||
                (facingX == playerA.x && facingY == playerA.y))
            {
                return true;
            }

            bool canSee;

            switch (direction)
            {
                case 0:
                    canSee = ((playerA.x >= facingX && playerA.y <= ((facingX - playerA.x) + facingY)) ||
                        (playerA.x <= facingX && playerA.y <= ((playerA.x - facingX) + facingY)));
                    break;

                case 1:
                    canSee = ((playerA.x >= facingX && playerA.y <= ((facingX - playerA.x) + facingY)) ||
                        (playerA.x >= ((facingX - facingY) + playerA.y) && playerA.y <= facingY));
                    break;

                case 2:
                    canSee = ((playerA.x >= (facingX + facingY - playerA.y) && playerA.y <= facingY) ||
                        (playerA.x >= (facingX + playerA.y - facingY) && playerA.y >= facingY));
                    break;

                case 3:
                    canSee = ((playerA.x >= ((facingX + playerA.y) - facingY) && playerA.y >= facingY) ||
                        (playerA.x >= facingX && playerA.y >= ((playerA.x - facingX) + facingY)));
                    break;

                case 4:
                    canSee = ((playerA.x >= facingX && playerA.y >= ((playerA.x - facingX) + facingY)) ||
                        (playerA.x <= facingX && playerA.y >= ((facingX - playerA.x) + facingY)));
                    break;

                case 5:
                    canSee = ((playerA.x <= facingX && playerA.y >= ((facingX - playerA.x) + facingY)) ||
                        (playerA.x <= ((facingX + facingY) - playerA.y) && playerA.y >= facingY));
                    break;

                case 6:
                    canSee = ((playerA.x <= ((facingX + facingY) - playerA.y) && playerA.y >= facingY) ||
                        (playerA.x <= ((facingX + playerA.y) - facingY) && playerA.y <= facingY));
                    break;

                case 7:
                    canSee = ((playerA.x <= ((facingX + playerA.y) - facingY) && playerA.y <= facingY) ||
                        (playerA.x <= facingX && playerA.y <= ((playerA.x - facingX) + facingY)));
                    break;

                case 8:
                    canSee = true;
                    break;

                default:
                    throw new System.ApplicationException("Switch value unexpected");
            }

            return canSee;
        }

        internal static List<SortedCombatant> Rebuild_SortedCombatantList(Player player, int max_range, Predicate<Player> filter) /* sub_738D8 */
        {
            var cm = gbl.CombatMap[ovr033.GetPlayerIndex(player)];

            return Rebuild_SortedCombatantList(cm.size, max_range, cm.pos, filter);
        }

        internal static List<SortedCombatant> Rebuild_SortedCombatantList(int size, int max_range, Point pos, Predicate<Player> filter) /* sub_738D8 */
        {
            var deltas = ovr033.GetSizeBasedMapDeltas(size);
            var attackerMap = ovr033.BuildSizeMap(size, pos);

            var sortedCombatants = new List<SortedCombatant>();

            for (int playerIndex = 1; playerIndex <= gbl.CombatantCount; playerIndex++)
            {
                var combatantMap = gbl.CombatMap[playerIndex];
                if (combatantMap.size > 0 && filter(gbl.player_array[playerIndex]))
                {
                    var targetMap = ovr033.BuildSizeMap(combatantMap.size, combatantMap.pos);

                    bool found = false;
                    int found_range = max_range;
                    Point found_target = new Point();
                    Point found_attacker = new Point();

                    foreach (var targetPos in targetMap)
                    {
                        foreach (var attackerPos in attackerMap)
                        {
                            int tmp_range = max_range;

                            if (canReachTarget(ref tmp_range, targetPos, attackerPos) == true)
                            {
                                found = true;

                                if (tmp_range < found_range)
                                {
                                    found_range = tmp_range;
                                    found_attacker = attackerPos;
                                    found_target = targetPos;
                                }
                            }
                        }
                    }

                    if (found == true)
                    {
                        byte tmpDir = FindCombatantDirection(found_target, found_attacker);

                        var combatant = new SortedCombatant(gbl.player_array[playerIndex], combatantMap.pos, found_range, tmpDir);
                        sortedCombatants.Add(combatant);
                    }
                }
            }

            sortedCombatants.Sort();

            return sortedCombatants;
        }


        internal static byte FindCombatantDirection(Point target, Point attacker)
        {
            byte dir = 0;

            while (CanSeeCombatant(dir, target, attacker) == false && dir < 8)
            {
                dir++;
            }

            return dir;
        }
    
    
    }


}

