using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr032
    {
        internal static bool canReachTarget(Struct_1D1BC groundTilesMap, ref int range, Point target, Point attacker)
        {
            var tmpPos = target;
            return canReachTarget(groundTilesMap, ref range, ref tmpPos, attacker);
        }

        internal static bool canReachTarget(Struct_1D1BC groundTilesMap, ref int range, ref Point outPos, Point attacker) /* sub_733F1 */
        {
            SteppingPath var_31 = new SteppingPath();
            SteppingPath var_19 = new SteppingPath();

            int max_range = (range * 2) + 1;
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

                if ((groundTilesMap.field_6 == false && s189.field_2 > var_31.current.y) ||
                    var_19.steps > max_range)
                {
                    outPos = var_19.current;
                    range = var_19.steps;

                    return false;
                }

                var_31.Step();
                finished = !var_19.Step();
            } while (finished == false);

            range = var_19.steps;

            return true;
        }

        /// <summary>
        /// Returns if playerB can see playerA
        /// </summary>
        internal static bool CanSeeCombatant(int direction, Point playerA, Point playerB) /* sub_7354A */
        {
            if( playerA.MapInBounds() == false && playerB.MapInBounds() == false )
            {
                return false;
            }

            if (direction == 0xff)
            {
                direction = 8;
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


        internal static List<SortedCombatant> Rebuild_SortedCombatantList(Struct_1D1BC groundTileMap, int size, byte dir, int max_range, Point pos) /* sub_738D8 */
        {
            var deltas = ovr033.GetSizeBasedMapDeltas(size);
            var attackerMap = ovr033.BuildSizeMap(size, pos);

            var sortedCombatants = new List<SortedCombatant>();

            for (int playerIndex = 1; playerIndex <= gbl.CombatantCount; playerIndex++)
            {
                if (gbl.CombatMap[playerIndex].size > 0)
                {
                    var combatantMap = gbl.CombatMap[playerIndex];
                    var targetMap = ovr033.BuildSizeMap(combatantMap.size, combatantMap.pos);

                    bool found = false;
                    int found_range = max_range;
                    Point found_target = new Point();
                    Point found_attacker = new Point();

                    foreach (var targetPos in targetMap)
                    {
                        foreach (var attackerPos in attackerMap)
                        {
                            if (CanSeeCombatant(dir, targetPos, attackerPos) == true)
                            {
                                int tmp_range = max_range;

                                if (canReachTarget(groundTileMap, ref tmp_range, targetPos, attackerPos) == true)
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
                    }

                    if (found == true)
                    {
                        byte tmpDir = 0;

                        if (dir < 8)
                        {
                            tmpDir = dir;
                        }
                        else
                        {
                            tmpDir = FindCombatantDirection(found_target, found_attacker);
                        }

                        var combatant = new SortedCombatant(gbl.player_array[playerIndex], gbl.CombatMap[playerIndex].pos, found_range, tmpDir);
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

            while (CanSeeCombatant(dir, target, attacker) == false)
            {
                dir++;
            }

            return dir;
        }
    
    
    }


}

