using Classes;
using System;

namespace engine
{
    class ovr032
    {
        internal static int signOfNumer(int arg_0) /*sub_73005*/
        {
            int sign;

            if (arg_0 < 0)
            {
                sign = -1;
            }
            else if (arg_0 > 0)
            {
                sign = 1;
            }
            else
            {
                sign = 0;
            }

            return sign;
        }


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


        internal static void sub_731A5(Struct_XXXX arg_0)
        {
            arg_0.field_0E = arg_0.field_00;
            arg_0.field_10 = arg_0.field_02;
            arg_0.field_0A = Math.Abs(arg_0.field_04 - arg_0.field_00);
            arg_0.field_0C = Math.Abs(arg_0.field_06 - arg_0.field_02);

            arg_0.field_12 = signOfNumer(arg_0.field_04 - arg_0.field_00);
            arg_0.field_14 = signOfNumer(arg_0.field_06 - arg_0.field_02);

            arg_0.field_08 = 0;
            arg_0.field_16 = 0;
        }


        internal static bool sub_7324C(Struct_XXXX arg_0)
        {
            bool var_1 = false;
            sbyte var_2 = 1;
            sbyte var_3 = 1;

            Struct_XXXX var_7 = arg_0;

            if (var_7.field_0A >= var_7.field_0C)
            {
                if (var_7.field_0E != var_7.field_04)
                {
                    var_7.field_0E += var_7.field_12;
                    var_2 = (sbyte)(var_7.field_12 + 1);

                    var_7.field_08 += var_7.field_0C;
                    var_7.field_08 += var_7.field_0C;
                    var_7.field_16 += 2;

                    if (var_7.field_08 >= var_7.field_0A)
                    {
                        var_7.field_08 -= var_7.field_0A;
                        var_7.field_08 -= var_7.field_0A;

                        var_7.field_10 += var_7.field_14;
                        var_3 = (sbyte)(var_7.field_14 + 1);
                        var_7.field_16 += 1;
                    }

                    var_1 = true;
                }
            }
            else if (var_7.field_10 != var_7.field_06)
            {
                var_7.field_10 += var_7.field_14;
                var_3 = (sbyte)(var_7.field_14 + 1);

                var_7.field_08 += var_7.field_0A;
                var_7.field_08 += var_7.field_0A;
                var_7.field_16 += 2;

                if (var_7.field_08 >= var_7.field_0C)
                {
                    var_7.field_08 -= var_7.field_0C;
                    var_7.field_08 -= var_7.field_0C;
                    var_7.field_0E = var_7.field_12;
                    var_2 = (sbyte)(var_7.field_12 + 1);
                    var_7.field_16 += 1;
                }

                var_1 = true;
            }

            var_7.field_17 = unk_1886A[(var_3 * 3) + var_2];


            return var_1;
        }


        static byte[] unk_1886A = {
									  7, 0, 1, 6, 8, 2, 5, 4, 3, 8, 4, 2, 1, 0, 0,  
									  0x55, 0x55, 0xAA, 0xAA, 0xFF, 0xFF, 0, 0, 0,  
									  1, 2, 2, 2, 3, 0, 1, 1, 1, 2, 2, 3, 3 
								  };


        internal static bool sub_733F1(Struct_1D1BC arg_0, ref short arg_4, ref int outY, ref int outX, int mapY, int mapX)
        {
            bool var_33;
            bool var_32;

            Struct_XXXX var_31 = new Struct_XXXX();
            Struct_XXXX var_19 = new Struct_XXXX();

            int var_35 = arg_4 * 2;
            var_19.field_00 = mapX;
            var_19.field_02 = mapY;
            var_19.field_04 = outX;
            var_19.field_06 = outY;

            sub_731A5(var_19);

            var_31.field_00 = 0;

            var_31.field_02 = gbl.BackGroundTiles[arg_0[mapX, mapY]].field_1;

            if (var_19.field_0A > var_19.field_0C)
            {
                var_31.field_04 = var_19.field_0A;
            }
            else
            {
                var_31.field_04 = var_19.field_0C;
            }

            var_31.field_06 = gbl.BackGroundTiles[arg_0[mapX, mapY]].field_1;
            sub_731A5(var_31);
            var_32 = false;

            do
            {
                if ((arg_0.field_6 == 0 && 
                     gbl.BackGroundTiles[arg_0[var_19.field_0E, var_19.field_10]].field_2 > var_31.field_0A) ||
                    var_19.field_16 > var_35)
                {
                    outX = var_19.field_0E;
                    outY = var_19.field_10;
                    arg_4 = (sbyte)var_19.field_16;

                    return false;
                }

                var_33 = sub_7324C(var_31);
                var_32 = !sub_7324C(var_19);
            } while (var_32 == false);

            arg_4 = (sbyte)var_19.field_16;

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
            short var_1F;
            short var_1D;
            int[] combatantMapStepY = new int[4];
            int[] combatantMapStepX = new int[4];
            int[] mapStepY = new int[4];
            int[] mapStepX = new int[4];
            sbyte deltaY;
            sbyte deltaX;
            int var_6;
            int var_5;
            byte playerIndex;

            var_6 = 255; /* put here because of unused error */
            var_5 = 255; /* put here because of unused error */

            for (int step = 0; step <= 3; step++)
            {
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
                    var_1F = 0x0FF;

                    for (int combStep = 0; combStep <= 3; combStep++)
                    {
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
                                    var_1D = arg_8;

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
                        gbl.SortedCombatantList[gbl.sortedCombatantCount].steps = (byte)var_1F;
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
