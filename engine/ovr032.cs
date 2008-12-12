using Classes;
using System;

namespace engine
{
    class ovr032
    {
        static void Sort_SortedCombatabtList() /* sub_73033 */
        {
            if (gbl.sortedCombatantCount > 1)
            {
                for (int indexA = 1; indexA <= (gbl.sortedCombatantCount - 1); indexA++)
                {
                    for (int indexB = indexA + 1; indexB <= gbl.sortedCombatantCount; indexB++)
                    {
                        int dirA = gbl.SortedCombatantList[indexA].direction;
                        int dirB = gbl.SortedCombatantList[indexB].direction;

                        if (gbl.SortedCombatantList[indexB].steps < gbl.SortedCombatantList[indexA].steps ||
                            (gbl.SortedCombatantList[indexB].steps == gbl.SortedCombatantList[indexA].steps &&
                              dirB < dirA && (dirB % 2) <= (dirA % 2)))
                        {
                            // swap them.
                            SortedCombatant tmp = gbl.SortedCombatantList[indexA];
                            gbl.SortedCombatantList[indexA] = gbl.SortedCombatantList[indexB];
                            gbl.SortedCombatantList[indexB] = tmp;
                        }
                    }
                }
            }
        }


        internal static bool canReachTarget(Struct_1D1BC groundTilesMap, ref int range, ref int outY, ref int outX, int mapY, int mapX) /* sub_733F1 */
        {
            SteppingPath var_31 = new SteppingPath();
            SteppingPath var_19 = new SteppingPath();

            int max_range = (range * 2) + 1;
            var_19.attacker_x = mapX;
            var_19.attacker_y = mapY;
            var_19.target_x = outX;
            var_19.target_y = outY;

            var_19.CalculateDeltas();

            var_31.attacker_x = 0;
            var_31.attacker_y = gbl.BackGroundTiles[groundTilesMap[mapX, mapY]].field_1;

            if (var_19.diff_x > var_19.diff_y)
            {
                var_31.target_x = var_19.diff_x;
            }
            else
            {
                var_31.target_x = var_19.diff_y;
            }

            var_31.target_y = gbl.BackGroundTiles[groundTilesMap[mapX, mapY]].field_1;
            var_31.CalculateDeltas();
            bool finished = false;

            do
            {
                int gt = groundTilesMap[var_19.current_x, var_19.current_y];
                Struct_189B4 s189 = gbl.BackGroundTiles[gt];

                if ((groundTilesMap.field_6 == false && s189.field_2 > var_31.current_y) ||
                    var_19.steps > max_range)
                {
                    outX = var_19.current_x;
                    outY = var_19.current_y;
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
        internal static bool CanSeeCombatant(byte direction, int playerAY, int playerAX, int playerBY, int playerBX) /* sub_7354A */
        {
            if (playerBX < 0 ||
                playerBX > 0x31 ||
                playerBY < 0 ||
                playerBY > 0x18 ||
                playerAX < 0 ||
                playerAX > 0x31 ||
                playerAY < 0 ||
                playerAY > 0x18)
            {
                return false;
            }

            if (direction == 0xff)
            {
                direction = 8;
            }

            int facingX = playerBX + gbl.MapDirectionXDelta[direction];
            int facingY = playerBY + gbl.MapDirectionYDelta[direction];

            if ((playerBX == playerAX && playerBY == playerAY) ||
                (facingX == playerAX && facingY == playerAY))
            {
                return true;
            }

            bool var_1;

            switch (direction)
            {
                case 0:
                    if ((playerAX >= facingX && playerAY <= ((facingX - playerAX) + facingY)) ||
                        (playerAX <= facingX && playerAY <= ((playerAX - facingX) + facingY)))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 1:
                    if ((playerAX >= facingX && playerAY <= ((facingX - playerAX) + facingY)) ||
                        (playerAX >= ((facingX - facingY) + playerAY) && playerAY <= facingY))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 2:
                    if ((playerAX >= (facingX + facingY - playerAY) && playerAY <= facingY) ||
                        (playerAX >= (facingX + playerAY - facingY) && playerAY >= facingY))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 3:
                    if ((playerAX >= ((facingX + playerAY) - facingY) && playerAY >= facingY) ||
                        (playerAX >= facingX && playerAY >= ((playerAX - facingX) + facingY)))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 4:
                    if ((playerAX >= facingX && playerAY >= ((playerAX - facingX) + facingY)) ||
                        (playerAX <= facingX && playerAY >= ((facingX - playerAX) + facingY)))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 5:
                    if ((playerAX <= facingX && playerAY >= ((facingX - playerAX) + facingY)) ||
                        (playerAX <= ((facingX + facingY) - playerAY) && playerAY >= facingY))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 6:
                    if ((playerAX <= ((facingX + facingY) - playerAY) && playerAY >= facingY) ||
                        (playerAX <= ((facingX + playerAY) - facingY) && playerAY <= facingY))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 7:
                    if ((playerAX <= ((facingX + playerAY) - facingY) && playerAY <= facingY) ||
                        (playerAX <= facingX && playerAY <= ((playerAX - facingX) + facingY)))
                    {
                        var_1 = true;
                    }
                    else
                    {
                        var_1 = false;
                    }
                    break;

                case 8:
                    var_1 = true;
                    break;

                default:
                    throw new System.ApplicationException("Switch value unexpected");
            }

            return var_1;
        }


        internal static void Rebuild_SortedCombatantList(Struct_1D1BC groundTileMap, int size, byte dir, int max_range, int mapY, int mapX) /* sub_738D8 */
        {
            int[] targetMapSizeY = new int[4];
            int[] targetMapSizeX = new int[4];
            int[] attackerMapSizeY = new int[4];
            int[] attackerMapSizeX = new int[4];

            for (int attackerSize = 0; attackerSize <= 3; attackerSize++)
            {
                int deltaY;
                int deltaX;

                if (ovr033.GetSizeBasedMapDelta(out deltaY, out deltaX, attackerSize, size) == true)
                {
                    attackerMapSizeX[attackerSize] = mapX + deltaX;
                    attackerMapSizeY[attackerSize] = mapY + deltaY;
                }
                else
                {
                    attackerMapSizeX[attackerSize] = -1;
                }
            }

            gbl.sortedCombatantCount = 0;

            for (int playerIndex = 1; playerIndex <= gbl.CombatantCount; playerIndex++)
            {
                if (gbl.CombatMap[playerIndex].size > 0)
                {
                    for (int targetSize = 0; targetSize <= 3; targetSize++)
                    {
                        int deltaY;
                        int deltaX;

                        if (ovr033.GetSizeBasedMapDelta(out deltaY, out deltaX, targetSize, gbl.CombatMap[playerIndex].size) == true)
                        {
                            targetMapSizeX[targetSize] = gbl.CombatMap[playerIndex].xPos + deltaX;
                            targetMapSizeY[targetSize] = gbl.CombatMap[playerIndex].yPos + deltaY;
                        }
                        else
                        {
                            targetMapSizeX[targetSize] = -1;
                        }
                    }

                    bool found = false;
                    int found_range = 0xFFFF;
                    int found_target_size = 0xFFFF;
                    int found_attacker_size = 0xFFFF;

                    for (int targetSize = 0; targetSize <= 3; targetSize++)
                    {
                        if (targetMapSizeX[targetSize] >= 0)
                        {
                            for (int attackerSize = 0; attackerSize <= 3; attackerSize++)
                            {
                                if (attackerMapSizeX[attackerSize] >= 0 &&
                                    CanSeeCombatant(dir, targetMapSizeY[targetSize], targetMapSizeX[targetSize], attackerMapSizeY[attackerSize], attackerMapSizeX[attackerSize]) == true)
                                {
                                    int target_x = targetMapSizeX[targetSize];
                                    int target_y = targetMapSizeY[targetSize];
                                    int tmp_range = max_range;

                                    if (canReachTarget(groundTileMap, ref tmp_range, ref target_y, ref target_x, attackerMapSizeY[attackerSize], attackerMapSizeX[attackerSize]) == true)
                                    {
                                        found = true;

                                        if (tmp_range < found_range)
                                        {
                                            found_range = tmp_range;
                                            found_attacker_size = attackerSize;
                                            found_target_size = targetSize;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (found == true)
                    {
                        gbl.sortedCombatantCount++;

                        gbl.SortedCombatantList[gbl.sortedCombatantCount].player_index = playerIndex;
                        gbl.SortedCombatantList[gbl.sortedCombatantCount].steps = found_range;
                        byte tmpDir = 0;

                        if (dir < 8)
                        {
                            tmpDir = dir;
                        }
                        else
                        {
                            while (CanSeeCombatant(tmpDir, targetMapSizeY[found_target_size], targetMapSizeX[found_target_size], attackerMapSizeY[found_attacker_size], attackerMapSizeX[found_attacker_size]) == false)
                            {
                                tmpDir++;
                            }
                        }

                        gbl.SortedCombatantList[gbl.sortedCombatantCount].direction = tmpDir;
                    }
                }
            }

            Sort_SortedCombatabtList();
        }
    }
}
