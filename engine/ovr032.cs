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
                            SortedCombatant var_7 = gbl.SortedCombatantList[indexA];
                            gbl.SortedCombatantList[indexA] = gbl.SortedCombatantList[indexB];
                            gbl.SortedCombatantList[indexB] = var_7;
                        }
                    }
                }
            }
        }


        internal static bool sub_733F1(Struct_1D1BC arg_0, ref int range, ref int outY, ref int outX, int mapY, int mapX)
        {
            Struct_XXXX var_31 = new Struct_XXXX();
            Struct_XXXX var_19 = new Struct_XXXX();

            int max_range = range * 2;
            var_19.attacker_x = mapX;
            var_19.attacker_y = mapY;
            var_19.target_x = outX;
            var_19.target_y = outY;

            var_19.init_struct_xxxx();

            var_31.attacker_x = 0;
            var_31.attacker_y = gbl.BackGroundTiles[arg_0[mapX, mapY]].field_1;

            if (var_19.diff_x > var_19.diff_y)
            {
                var_31.target_x = var_19.diff_x;
            }
            else
            {
                var_31.target_x = var_19.diff_y;
            }

            var_31.target_y = gbl.BackGroundTiles[arg_0[mapX, mapY]].field_1;
            var_31.init_struct_xxxx();
            bool finished = false;

            do
            {
                if ((arg_0.field_6 == 0 && 
                     gbl.BackGroundTiles[arg_0[var_19.current_x, var_19.current_y]].field_2 > var_31.diff_x) ||
                    var_19.steps > max_range)
                {
                    outX = var_19.current_x;
                    outY = var_19.current_y;
                    range = var_19.steps;

                    return false;
                }

                var_31.step();
                finished = !var_19.step();
            } while (finished == false);

            range = var_19.steps;

            return true;
        }

        /// <summary>
        /// Returns if playerB can see playerA
        /// </summary>
        internal static bool CanSeeCombatant(byte direction, int playerAY, int playerAX, int playerBY, int playerBX) /* sub_7354A */
        {
            bool var_1;

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


        internal static void Rebuild_SortedCombatantList(Struct_1D1BC arg_0, int size, byte dir, short arg_8, int mapY, int mapX) /* sub_738D8 */
        {
            int[] combatantMapStepY = new int[4];
            int[] combatantMapStepX = new int[4];
            int[] mapStepY = new int[4];
            int[] mapStepX = new int[4];
            int var_6;
            int var_5;
            byte playerIndex;

            var_6 = 255; /* put here because of unused error */
            var_5 = 255; /* put here because of unused error */

            for (int step = 0; step <= 3; step++)
            {
                int deltaY;
                int deltaX;

                if (ovr033.GetSizeBasedMapDelta(out deltaY, out deltaX, step, size) == true)
                {
                    mapStepX[step] = mapX + deltaX;
                    mapStepY[step] = mapY + deltaY;
                }
                else
                {
                    mapStepX[step] = -1;
                }
            }

            gbl.sortedCombatantCount = 0;

            for (playerIndex = 1; playerIndex <= gbl.CombatantCount; playerIndex++)
            {
                if (gbl.CombatMap[playerIndex].size > 0)
                {
                    bool found = false;
                    int var_1F = 0x0FF;

                    for (int combStep = 0; combStep <= 3; combStep++)
                    {
                        int deltaY;
                        int deltaX;

                        if (ovr033.GetSizeBasedMapDelta(out deltaY, out deltaX, combStep, gbl.CombatMap[playerIndex].size) == true)
                        {
                            combatantMapStepX[combStep] = gbl.CombatMap[playerIndex].xPos + deltaX;
                            combatantMapStepY[combStep] = gbl.CombatMap[playerIndex].yPos + deltaY;
                        }
                        else
                        {
                            combatantMapStepX[combStep] = -1;
                        }
                    }

                    for (int combStep = 0; combStep <= 3; combStep++)
                    {
                        if (combatantMapStepX[combStep] >= 0)
                        {
                            for (int step = 0; step <= 3; step++)
                            {
                                if (mapStepX[step] >= 0 &&
                                    CanSeeCombatant(dir, combatantMapStepY[combStep], combatantMapStepX[combStep], mapStepY[step], mapStepX[step]) == true)
                                {
                                    int var_8 = combatantMapStepX[combStep];
                                    int var_9 = combatantMapStepY[combStep];
                                    int var_1D = arg_8;

                                    if (sub_733F1(arg_0, ref var_1D, ref var_9, ref var_8, mapStepY[step], mapStepX[step]) == true)
                                    {
                                        found = true;

                                        if (var_1D < var_1F)
                                        {
                                            var_1F = var_1D;
                                            var_5 = step;
                                            var_6 = combStep;
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
                        gbl.SortedCombatantList[gbl.sortedCombatantCount].steps = var_1F;
                        byte tmpDir = 0;

                        if (dir < 8)
                        {
                            tmpDir = dir;
                        }
                        else
                        {
                            while (CanSeeCombatant(tmpDir, combatantMapStepY[var_6], combatantMapStepX[var_6], mapStepY[var_5], mapStepX[var_5]) == false)
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
