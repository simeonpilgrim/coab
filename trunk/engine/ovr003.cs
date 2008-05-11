using Classes;

namespace engine
{
    class ovr003
    {
        internal static void CMD_Exit()
        {
            if (gbl.byte_1EE8E != 0)
            {
                seg037.draw8x8_03();
            }

            
            if (gbl.byte_1AB0A != 0)
            {
                gbl.player_ptr = gbl.player_ptr2;
                gbl.byte_1AB0A = 0;
            }

            gbl.byte_1EE72[0] = 0;
            gbl.byte_1EE72[1] = 0;

            gbl.byte_1EE8C = 0;
            gbl.byte_1EE8E = 0;
            gbl.stopVM = true;

            gbl.ecl_offset++;

            if (gbl.vmCallStack.Count > 0)
            {
                //System.Console.Write("  vmCallStack:");
                //foreach (ushort us in gbl.vmCallStack)
                //{
                //    System.Console.Write(" {0,4:X", us);
                //}
                //System.Console.WriteLine();

                gbl.vmCallStack.Clear();
            }

            gbl.textYCol = 0x11;
            gbl.textXCol = 1;
            gbl.byte_1EE8A = 0;
        }


        internal static void CMD_Goto()
        {
            ovr008.vm_LoadCmdSets(1);
            ushort newOffset = gbl.cmd_opps[1].Word;

            //System.Console.WriteLine("  CMD_Goto: was: 0x{0:X} now: 0x{1:X}", gbl.ecl_offset, newOffset);

            gbl.ecl_offset = newOffset;
        }


        internal static void CMD_Gosub()
        {
            ovr008.vm_LoadCmdSets(1);

            ovr008.vm_gosub(1);
        }


        internal static void CMD_Compare() // sub_2611D
        {
            ovr008.vm_LoadCmdSets(2);

            if (gbl.cmd_opps[1].Code >= 0x80 ||
                gbl.cmd_opps[2].Code >= 0x80)
            {
                ovr008.sub_3193B(gbl.unk_1D972[2], gbl.unk_1D972[1]);
            }
            else
            {
                ushort var_2 = ovr008.vm_GetCmdValue(1);
                ushort var_4 = ovr008.vm_GetCmdValue(2);

                ovr008.compare_variables(var_4, var_2);
            }
        }


        internal static void CMD_AddSubDivMulti() // sub_2619A
        {
            ushort value;
            ushort var_6;
            ushort var_4;
            ushort location;

            ovr008.vm_LoadCmdSets(3);

            var_6 = ovr008.vm_GetCmdValue(1);
            var_4 = ovr008.vm_GetCmdValue(2);

            location = gbl.cmd_opps[3].Word;

            switch (gbl.command)
            {
                case 4:
                    value = (ushort)(var_6 + var_4);
                    break;

                case 5:
                    value = (ushort)(var_4 - var_6);
                    break;

                case 6:
                    value = (ushort)(var_6 / var_4);
                    gbl.area2_ptr.field_67E = (short)(var_6 % var_4);
                    break;

                case 7:
                    value = (ushort)(var_6 * var_4);
                    break;

                default:
                    value = 0;
                    throw (new System.Exception("can't get here."));
            }

            ovr008.vm_SetMemoryValue(value, location);
        }


        internal static void CMD_Random() // sub_2623D
        {
            ushort var_4;
            byte var_2;
            byte var_1;

            ovr008.vm_LoadCmdSets(2);

            var_1 = (byte)ovr008.vm_GetCmdValue(1);

            if (var_1 < 0xff)
            {
                var_1++;
            }

            var_4 = gbl.cmd_opps[2].Word;

            var_2 = (byte)(seg051.Random(var_1));

            ovr008.vm_SetMemoryValue(var_2, var_4);
        }


        internal static void CMD_Save()
        {
            ushort var_4;
            ushort var_2;

            ovr008.vm_LoadCmdSets(2);

            var_4 = gbl.cmd_opps[2].Word;

            if (gbl.cmd_opps[1].Code < 0x80)
            {
                var_2 = ovr008.vm_GetCmdValue(1);

                ovr008.vm_SetMemoryValue(var_2, var_4);
            }
            else
            {
                ovr008.vm_WriteStringToMemory(gbl.unk_1D972[1], var_4);
            }
        }


        internal static void CMD_LoadCharacter() /* sub_262E9 */
        {
            gbl.byte_1AB0A = 1;

            ovr008.vm_LoadCmdSets(1);
            byte var_7 = 0;

            byte var_6 = (byte)ovr008.vm_GetCmdValue(1);

            Player player_ptr = gbl.player_next_ptr;
            byte var_8 = (byte)(var_6 & 0x80);

            var_6 = (byte)(var_6 & 0x7f);

            if (var_6 > 0)
            {
                while (var_7 <= var_6 &&
                    player_ptr != null)
                {
                    player_ptr = player_ptr.next_player;
                    var_7++;
                }
            }

            if (player_ptr != null)
            {
                gbl.player_ptr = player_ptr;
                gbl.byte_1EE97 = 0;
            }
            else
            {
                gbl.byte_1EE97 = 1;
            }

            if (var_8 != 0 &&
                gbl.byte_1EE7C != 0 &&
                gbl.byte_1EE7D != 0)
            {
                if (gbl.player_ptr2 == player_ptr)
                {
                    gbl.byte_1AB0A = 0;
                }
                ovr018.free_players(1, false);

                ovr025.Player_Summary(gbl.player_ptr);
                gbl.byte_1EE7C = 0;
                gbl.byte_1EE7D = 0;
            }
        }


        internal static void CMD_SetupMonster() /* sub_263C9 */
        {
            ovr008.vm_LoadCmdSets(3);
            gbl.byte_1D92B = (byte)ovr008.vm_GetCmdValue(1);

            gbl.area2_ptr.field_580 = (byte)ovr008.vm_GetCmdValue(2);

            gbl.byte_1D92C = (byte)ovr008.vm_GetCmdValue(3);

            gbl.area2_ptr.field_582 = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (gbl.area2_ptr.field_580 < gbl.area2_ptr.field_582)
            {
                gbl.area2_ptr.field_582 = gbl.area2_ptr.field_580;
            }
            ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
        }


        internal static void CMD_LoadMonster() /* sub_26465 */
        {
            Affect affect_ptr2;
            Affect affect_ptr;
            Item item_ptr;
            Player player_struct;
            Player playerC;

            Player player_ptr_bkup = gbl.player_ptr;
            byte var_4 = 1;
            ovr008.vm_LoadCmdSets(3);

            if (gbl.byte_1AB0E < 0x3f)
            {
                Player playerB = null;
                Player playerA = null;
                byte var_1 = (byte)ovr008.vm_GetCmdValue(1);

                ovr008.load_mob(out affect_ptr, out item_ptr, out playerC, var_1);

                player_struct = playerC.ShallowClone();
                Item item_ptr2 = item_ptr;
                affect_ptr2 = affect_ptr;

                byte var_2 = (byte)ovr008.vm_GetCmdValue(2);

                if (var_2 <= 0)
                {
                    var_2 = 1;
                }

                byte var_3 = (byte)ovr008.vm_GetCmdValue(3);
                playerB = gbl.player_next_ptr;

                ovr034.chead_cbody_comspr_icon(gbl.byte_1D92D, var_3, "CPIC");

                while (playerB.next_player != null)
                {
                    playerB = playerB.next_player;
                }

                playerB.next_player = playerC;
                playerB = playerB.next_player;

                playerB.icon_id = gbl.byte_1D92D;
                playerB.next_player = null;

                gbl.byte_1AB0E++;
                var_4++;


                while (var_4 <= var_2 &&
                       gbl.byte_1AB0E < 0x3f)
                {
                    playerA = player_struct.ShallowClone();

                    playerB.next_player = playerA;

                    playerB = playerB.next_player;
                    playerB.icon_id = gbl.byte_1D92D;

                    playerB.next_player = null;
                    playerB.affect_ptr = null;
                    playerB.itemsPtr = null;

                    var_4++;
                    gbl.byte_1AB0E++;

                    while (item_ptr != null)
                    {
                        if (playerB.itemsPtr == null)
                        {
                            playerB.itemsPtr = item_ptr.ShallowClone();
                            playerB.itemsPtr.next = null;
                        }
                        else
                        {
                            Item var_1CF = playerB.itemsPtr;
                            playerB.itemsPtr = item_ptr.ShallowClone();
                            playerB.itemsPtr.next = var_1CF;
                        }

                        item_ptr = item_ptr.next;
                    }

                    item_ptr = item_ptr2;

                    while (affect_ptr != null)
                    {
                        if (playerB.affect_ptr == null)
                        {
                            playerB.affect_ptr = affect_ptr.ShallowClone();
                            playerB.affect_ptr.next = null;
                        }
                        else
                        {
                            Affect var_1DB = playerB.affect_ptr;
                            playerB.affect_ptr = affect_ptr.ShallowClone();
                            playerB.affect_ptr.next = var_1DB;

                        }
                        affect_ptr = affect_ptr.next;
                    }
                    affect_ptr = affect_ptr2;
                }
                gbl.byte_1D92D++;
                gbl.byte_1EE93 = 1;
                gbl.player_ptr = player_ptr_bkup;
            }
        }


        internal static void CMD_Approach() // sub_26835
        {
            if (gbl.area2_ptr.field_582 > 0)
            {
                gbl.area2_ptr.field_582--;

                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
            }
            gbl.ecl_offset++;
        }


        internal static void CMD_Picture() /* sub_26873 */
        {
            ovr008.vm_LoadCmdSets(1);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);

            if (var_1 != 0xff)
            {
                gbl.byte_1EE72[1] = 1;
                gbl.byte_1EE8C = 1;

                if (gbl.area2_ptr.field_5C2 == 0xff)
                {
                    gbl.byte_1EE8D = 1;

                    if (var_1 >= 0x78)
                    {
                        ovr030.bigpic(var_1);
                        ovr030.sub_7087A();
                        gbl.byte_1D8AA = 0;
                    }
                    else
                    {
                        ovr030.load_pic_final(ref gbl.byte_1D556, 0, var_1, "PIC");
                        ovr030.sub_7000A(gbl.byte_1D556.ptrs[0].field_4, true, 3, 3);
                    }
                }
                else
                {
                    ovr008.sub_30543(var_1, (byte)gbl.area2_ptr.field_5C2);
                }
            }
            else
            {
                if ((gbl.last_game_state != 4 ||
                      gbl.game_state == 4) &&
                    (gbl.byte_1EE8C != 0 ||
                      gbl.displayPlayerSprite))
                {
                    gbl.byte_1D8AA = 1;
                    ovr029.sub_6F0BA();
                    gbl.byte_1EE8C = 0;
                    gbl.displayPlayerSprite = false;
                    gbl.byte_1EE8D = 1;
                }
                gbl.byte_1EE72[0] = 0;
                gbl.byte_1EE72[1] = 0;
            }
        }


        internal static void CMD_InputNumber() /* sub_2695E */
        {
            ovr008.vm_LoadCmdSets(2);

            ushort loc = gbl.cmd_opps[2].Word;

            ushort var_4 = seg041.getUserInputShort(0, 0x0a, string.Empty);

            ovr008.vm_SetMemoryValue(var_4, loc);
        }


        internal static void CMD_InputString() /* sub_269A4 */
        {
            ovr008.vm_LoadCmdSets(2);

            ushort loc = gbl.cmd_opps[2].Word;

            string str = seg041.getUserInputString(0x28, 0, 10, string.Empty);

            if (str.Length == 0)
            {
                str = " ";
            }

            ovr008.vm_WriteStringToMemory(str, loc);
        }


        internal static void CMD_Print()
        {
            ovr008.vm_LoadCmdSets(1);
            gbl.byte_1EE90 = 0;
            gbl.DelayBetweenCharacters = true;

            if (gbl.cmd_opps[1].Code < 0x80)
            {
                gbl.unk_1D972[1] = ovr008.vm_GetCmdValue(1).ToString();
            }

            if (gbl.command == 0x11)
            {
                seg041.press_any_key(gbl.unk_1D972[1], false, 0, 10, 0x16, 0x26, 0x11, 1);
            }
            else
            {
                gbl.textYCol = 0x11;
                gbl.textXCol = 1;

                seg041.press_any_key(gbl.unk_1D972[1], true, 0, 10, 0x16, 0x26, 0x11, 1);
            }

            gbl.DelayBetweenCharacters = false;
        }


        internal static void CMD_Return()
        {
            gbl.ecl_offset++;
            if (gbl.vmCallStack.Count > 0)
            {
                ushort newOffset = gbl.vmCallStack.Pop();
                //System.Console.WriteLine("  CMD_Return: was: {0:X} now: {1:X}", gbl.ecl_offset - 1, newOffset);
                gbl.ecl_offset = newOffset;
            }
            else
            {
                //System.Console.WriteLine("  CMD_Return: call stack empty. ecl_offset: {0:X}", gbl.ecl_offset - 1);
                CMD_Exit();
            }
        }


        internal static void CMD_CompareAnd() /* sub_26B0C */
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            ovr008.vm_LoadCmdSets(4);

            ushort var_8 = ovr008.vm_GetCmdValue(1);
            ushort var_6 = ovr008.vm_GetCmdValue(2);
            ushort var_4 = ovr008.vm_GetCmdValue(3);
            ushort var_2 = ovr008.vm_GetCmdValue(4);

            if (var_8 == var_6 &&
                var_4 == var_2)
            {
                gbl.item_find[0] = true;
            }
            else
            {
                gbl.item_find[1] = true;
            }
        }


        internal static void CMD_If()
        {
            gbl.ecl_offset++;

            int index = gbl.command - 0x16;

            //System.Console.WriteLine("  CMD_if: {0}", gbl.item_find[index]);
            
            if (gbl.item_find[index] == false)
            {
                ovr008.vm_skipNextCommand();
            }
        }


        internal static void CMD_NewECL()
        {
            ovr008.vm_LoadCmdSets(1);
            gbl.area_ptr.field_1E4 = gbl.byte_1EE88;

            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);

            gbl.byte_1EE88 = var_1;

            ovr008.load_ecl_dax(var_1);
            ovr008.vm_init_ecl();
            gbl.stopVM = true;
            gbl.byte_1AB09 = 1;

            seg051.FillChar(0, 2, gbl.byte_1EE72);
        }


        internal static void CMD_LoadFiles()
        {
            ovr008.vm_LoadCmdSets(3);
            gbl.byte_1AB0B = 1;

            byte var_3 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(3);

            if (gbl.command == 0x21)
            {
                gbl.byte_1AB0D = 1;

                if (var_3 != 0xff &&
                    var_3 != 0x7f &&
                    gbl.area_ptr.field_1CC != 0)
                {
                    gbl.area_ptr.field_18A = var_3;
                    ovr031.Load3DMap(var_3);
                    gbl.area2_ptr.field_592 = 0;
                }

                if (var_1 != 0xff &&
                    gbl.area_ptr.field_1CC == 0 &&
                    gbl.byte_1D5B4 != 0x50)
                {
                    ovr030.bigpic(0x79);
                }
            }
            else
            {
                gbl.byte_1AB0C = 1;

                if (var_3 == 0x7F)
                {
                    ovr031.LoadWalldef(1, 0);
                }
                else
                {
                    if (gbl.area_ptr.field_1CE != 0 &&
                        gbl.area_ptr.field_1D0 != 0)
                    {
                        if (var_3 != 0xff)
                        {
                            ovr031.LoadWalldef(1, var_3);
                        }

                        if (var_1 != 0xff)
                        {
                            ovr031.LoadWalldef(3, var_1);
                        }
                    }
                    else
                    {
                        if (var_3 != 0xff)
                        {
                            ovr031.LoadWalldef(1, var_3);
                        }
                        else
                        {
                            gbl.word_1D53E = -1;
                            gbl.word_1D540 = -1;
                        }

                        if (var_2 != 0xff)
                        {
                            ovr031.LoadWalldef(2, var_2);
                        }
                        else
                        {
                            gbl.word_1D542 = -1;
                            gbl.word_1D544 = -1;
                        }

                        if (var_1 != 0xff)
                        {
                            ovr031.LoadWalldef(3, var_1);
                        }
                        else
                        {
                            gbl.word_1D546 = -1;
                            gbl.word_1D548 = -1;
                        }
                    }
                }
            }


            if (gbl.byte_1AB0C != 0 &&
                gbl.byte_1AB0D != 0 &&
                gbl.last_game_state == 3)
            {
                if (gbl.game_state != 3 &&
                    gbl.byte_1EE98 != 0)
                {
                    seg037.draw8x8_03();
                    ovr025.Player_Summary(gbl.player_ptr);
                    ovr025.display_map_position_time();
                }
                gbl.byte_1EE98 = 0;
            }
        }


        internal static void CMD_AndOr() /* sub_26DD0 */
        {
            byte resultant;

            ovr008.vm_LoadCmdSets(3);
            ushort var_6 = ovr008.vm_GetCmdValue(1);
            ushort var_4 = ovr008.vm_GetCmdValue(2);

            ushort loc = gbl.cmd_opps[3].Word;

            if (gbl.command == 0x2F)
            {
                resultant = (byte)(var_6 & var_4);
            }
            else
            {
                resultant = (byte)(var_6 | var_4);
            }

            ovr008.compare_variables(resultant, 0);
            ovr008.vm_SetMemoryValue(resultant, loc);
        }


        internal static void CMD_GetTable() /* sub_26E3F */
        {
            ovr008.vm_LoadCmdSets(3);

            ushort var_2 = gbl.cmd_opps[1].Word;
            byte var_9 = (byte)ovr008.vm_GetCmdValue(2);

            ushort result_loc = gbl.cmd_opps[3].Word;

            ushort var_6 = (ushort)(var_9 + var_2);

            ushort var_8 = ovr008.vm_GetMemoryValue(var_6);
            ovr008.vm_SetMemoryValue(var_8, result_loc);
        }


        internal static void CMD_SaveTable() /* sub_26E9D */
        {
            ovr008.vm_LoadCmdSets(3);

            ushort var_6 = ovr008.vm_GetCmdValue(1);

            ushort result_loc = gbl.cmd_opps[2].Word;
            result_loc += ovr008.vm_GetCmdValue(3);
            
            ovr008.vm_SetMemoryValue(var_6, result_loc);
        }


        internal static void CMD_VertMenu() /* sub_26EE9 */
        {
            StringList var_10A;
            StringList var_106;

            gbl.byte_1EE90 = 0;
            bool var_10F = true;
            byte var_2 = 1;
            ovr008.vm_LoadCmdSets(3);
            ushort var_111 = gbl.cmd_opps[1].Word;

            string var_102 = gbl.unk_1D972[1];

            byte var_1 = (byte)ovr008.vm_GetCmdValue(3);
            gbl.ecl_offset--;
            ovr008.vm_LoadCmdSets(var_1);

            ovr027.alloc_stringList(out var_106, var_1);

            var_10A = var_106;

            gbl.textXCol = 1;
            gbl.textYCol = 0x11;

            seg041.press_any_key(var_102, true, 0, 10, 22, 34, 17, 1);

            while (var_106 != null)
            {
                var_106.s = gbl.unk_1D972[var_2];
                var_106.field_29 = 0;

                var_106 = var_106.next;

                var_2++;
            }

            var_106 = var_10A;

            short var_10E = 0;

            ovr008.sub_318AE(var_106, ref var_10E, ref var_10F, false, var_106, 0x16, 0x26, gbl.textYCol + 1,
                1, 15, 10, 13, string.Empty, string.Empty);
            ovr008.vm_SetMemoryValue((ushort)var_10E, var_111);

            ovr027.free_stringList(ref var_106);
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
        }


        internal static void CMD_HorizontalMenu()
        {
            byte var_3E;
            byte var_3D;
            bool useOverlay;
            byte var_3B;
            ushort var_3A;
            byte var_2;

            string var_38 = string.Empty;
            ovr008.vm_LoadCmdSets(2);

            var_3A = gbl.cmd_opps[1].Word;
            byte var_1 = (byte)ovr008.vm_GetCmdValue(2);

            gbl.ecl_offset--;

            ovr008.vm_LoadCmdSets(var_1);

            if (var_1 == 1)
            {
                var_3B = 1;
                var_3D = 0x0F;
                var_3E = 0x0F;

                if (gbl.unk_1D972[1] == "PRESS BUTTON OR RETURN TO CONTINUE.")
                {
                    gbl.unk_1D972[1] = "PRESS <ENTER>/<RETURN> TO CONTINUE";
                }
            }
            else
            {
                var_3B = 0;
                var_3D = 0x0F;
                var_3E = 0x0A;
            }

            if (gbl.byte_1EE8C == 0 ||
                gbl.byte_1EE8D == 0)
            {
                useOverlay = false;
            }
            else
            {
                useOverlay = true;
            }

            for (int var_3 = 1; var_3 <= (var_1 - 1); var_3++)
            {
                var_38 = var_38 + "~" + gbl.unk_1D972[var_3] + " ";
            }

            var_38 += "~" + gbl.unk_1D972[var_1];

            var_2 = ovr008.sub_317AA(useOverlay, var_3B, var_3D, var_3E, 0x0d, var_38, string.Empty);

            ovr008.vm_SetMemoryValue(var_2, var_3A);

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
        }

        /// <summary>
        /// Clears the pooled items and pool money.
        /// </summary>
        internal static void CMD_ClearMonsters() /* sub_27240 */
        {
            gbl.ecl_offset++;
            gbl.byte_1AB0E = 0;
            gbl.byte_1EE93 = 0;
            gbl.byte_1D92D = 8;

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            while (gbl.item_pointer != null)
            {
                Item item = gbl.item_pointer.next;

                gbl.item_pointer = item;
            }
        }


        internal static void CMD_PartyStrength() /* sub_272A9 */
        {
            byte var_6;
            byte var_4;
            byte var_3;

            ovr008.vm_LoadCmdSets(1);
            var_6 = 0;
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                byte hit_points = player.hit_point_current;
                byte armor_class = player.ac;
                int hit_bonus = player.hitBonus;

                var_3 = (byte)(player.magic_user_lvl + (ovr026.sub_6B3D1(player) * player.field_116));
                var_4 = (byte)(player.cleric_lvl + (ovr026.sub_6B3D1(player) * player.turn_undead));

                if (armor_class > 0x3c)
                {
                    armor_class -= 0x3c;
                }
                else
                {
                    armor_class = 0;
                }

                if (hit_bonus > 0x27)
                {
                    hit_bonus -= 0x27;
                }
                else
                {
                    hit_bonus = 0;
                }

                var_6 = (byte)(((var_4 * 4) + hit_points + (armor_class * 5) + (hit_bonus * 5) + (var_3 * 8)) / 10);

                player = player.next_player;
            }

            ushort loc = gbl.cmd_opps[1].Word;
            ovr008.vm_SetMemoryValue(var_6, loc);
        }


        internal static void setMemoryFour(byte val_d, byte val_c, byte val_b, byte val_a,
            ushort loc_a, ushort loc_b, ushort loc_c, ushort loc_d) /* sub_273F6 */
        {
            ovr008.vm_SetMemoryValue(val_a, loc_a);
            ovr008.vm_SetMemoryValue(val_b, loc_b);
            ovr008.vm_SetMemoryValue(val_c, loc_c);
            ovr008.vm_SetMemoryValue(val_d, loc_d);
        }


        internal static void CMD_CheckParty() /* sub_27454 */
        {
            byte var_1B;
            byte var_1A;
            ushort loc_d;
            ushort loc_c;
            ushort loc_b;
            ushort loc_a;
            byte var_11;
            byte var_10;
            byte var_F;
            Affect var_E;
            bool var_A;
            Player player_ptr;
            Affects affect_id;
            short var_4;
            ushort var_2;

            ovr008.vm_LoadCmdSets(6);
            player_ptr = gbl.player_next_ptr;

            var_A = false;
            if (gbl.cmd_opps[1].Code == 1)
            {
                var_2 = gbl.cmd_opps[1].Word;
            }
            else
            {
                var_2 = ovr008.vm_GetCmdValue(1);
            }

            affect_id = (Affects)ovr008.vm_GetCmdValue(2);

            loc_a = gbl.cmd_opps[3].Word;
            loc_b = gbl.cmd_opps[4].Word;
            loc_c = gbl.cmd_opps[5].Word;
            loc_d = gbl.cmd_opps[6].Word;

            var_4 = 0;
            var_1A = 0;
            var_11 = 0x0FF;
            var_10 = 0;

            var_2 -= 0x7fff;

            if (var_2 == 8001)
            {
                while (player_ptr != null && var_A == false)
                {
                    var_A = ovr025.find_affect(out var_E, affect_id, player_ptr);
                    player_ptr = player_ptr.next_player;
                }

                var_11 = 0;
                var_10 = 0;
                var_F = 0;

                setMemoryFour((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 >= 0x00A5 && var_2 <= 0x00AC)
            {
                var_1B = (byte)(var_2 - 0xA4);

                while (player_ptr != null)
                {
                    var_1A++;

                    if (player_ptr.field_EA[var_1B - 1] < var_11)
                    {
                        var_11 = player_ptr.field_EA[var_1B - 1];
                    }

                    if (player_ptr.field_EA[var_1B - 1] > var_10)
                    {
                        var_10 = player_ptr.field_EA[var_1B - 1];
                    }

                    var_4 += player_ptr.field_EA[var_1B - 1];

                    player_ptr = player_ptr.next_player;
                }

                var_F = (byte)(var_4 / var_1A);

                setMemoryFour((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 == 0x9f)
            {
                while (player_ptr != null)
                {
                    var_1A++;

                    if (player_ptr.initiative < var_11)
                    {
                        var_11 = player_ptr.initiative;
                    }

                    if (player_ptr.initiative > var_10)
                    {
                        var_10 = player_ptr.initiative;
                    }

                    var_4 += player_ptr.initiative;

                    player_ptr = player_ptr.next_player;
                }

                var_F = (byte)(var_4 / var_1A);

                setMemoryFour((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, loc_a, loc_b, loc_c, loc_d);
            }
        }


        internal static void CMD_PartySurprise() /* sub_2767E */
        {
            ushort var_B;
            ushort var_9;
            byte var_6;
            byte var_5;
            Player player;

            ovr008.vm_LoadCmdSets(2);

            player = gbl.player_next_ptr;

            var_5 = 0;
            var_6 = 0;

            while (player != null)
            {
                if (player._class == ClassId.ranger ||
                    player._class == ClassId.mc_c_r)
                {
                    var_5 = 1;
                }
                player = player.next_player;
            }

            var_9 = gbl.cmd_opps[1].Word;
            var_B = gbl.cmd_opps[2].Word;

            ovr008.vm_SetMemoryValue(var_5, var_9);
            ovr008.vm_SetMemoryValue(var_6, var_B);
        }


        internal static void CMD_Surprise() /* sub_2771E */
        {
            byte var_A;
            byte var_9;
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_2;
            byte var_1;

            ovr008.vm_LoadCmdSets(4);
            var_4 = 0;

            var_8 = (byte)ovr008.vm_GetCmdValue(1);
            var_7 = (byte)ovr008.vm_GetCmdValue(2);
            var_6 = (byte)ovr008.vm_GetCmdValue(3);
            var_5 = (byte)ovr008.vm_GetCmdValue(4);

            var_9 = (byte)((var_5 + 2) - var_8);
            var_A = (byte)((var_7 + 2) - var_6);

            var_1 = ovr024.roll_dice(6, 1);
            var_2 = ovr024.roll_dice(6, 1);

            if (var_1 <= var_9)
            {
                if (var_2 <= var_A)
                {
                    var_4 = 3;
                }
                else
                {
                    var_4 = 1;
                }
            }

            if (var_2 <= var_A)
            {
                var_4 = 2;
            }

            ovr008.vm_SetMemoryValue(var_4, 0x2cb);
        }


        internal static void CMD_Combat() // sub_277E4
        {
            ushort var_2;

            gbl.ecl_offset++;

            if (gbl.byte_1EE93 == 0 &&
                gbl.combat_type == gbl.combatType.normal)
            {
                if (gbl.area2_ptr.field_6D8 == 1)
                {
                    gbl.area2_ptr.field_6D8 = 0;

                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sound_sub_12194();
                    }

                    ovr007.sub_2F6E7();

                    if (gbl.area_ptr.field_1CC == 0)
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sound_sub_12194();
                        }
                    }
                    else
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sound_sub_12194();
                        }
                    }
                }
                else
                {
                    if (gbl.area2_ptr.field_5C4 == 1)
                    {
                        gbl.area2_ptr.field_5C4 = 0;

                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sound_sub_12194();
                        }

                        ovr005.temple_shop();

                        if (gbl.area_ptr.field_1CC == 0)
                        {

                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sound_sub_12194();
                            }
                        }
                        else
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sound_sub_12194();
                            }
                        }
                    }
                    else
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sound_sub_12194();
                        }

                        ovr006.sub_2E7A2();

                        if (gbl.area_ptr.field_1CC == 0)
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sound_sub_12194();
                            }
                        }
                        else
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sound_sub_12194();
                            }
                        }
                    }
                }
            }
            else
            {
                if (gbl.gameFlag01 == true)
                {
                    seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                }

                if (gbl.gameFlag01 == false)
                {
                    seg044.sound_sub_12194();
                }

                var_2 = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

                if (var_2 < gbl.area2_ptr.field_582)
                {
                    gbl.area2_ptr.field_582 = var_2;
                }

                ovr009.sub_33100();

                if (gbl.gameFlag01 == true)
                {
                    seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                }

                ovr006.sub_2E7A2();

                if (gbl.gameFlag01 == false)
                {
                    seg044.sound_sub_12194();
                }

                if (gbl.area_ptr.field_1CC == 0)
                {
                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sound_sub_12194();
                    }
                    ovr030.bigpic(0x79);
                }
                else
                {
                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sound_sub_120E0(gbl.sound_FF_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sound_sub_12194();
                    }
                }

                if (gbl.combat_type == gbl.combatType.duel)
                {
                    gbl.combat_type = gbl.combatType.normal;
                }
            }

            if (gbl.area_ptr.field_1CC != 0)
            {
                gbl.game_state = 4;
            }
            else
            {
                gbl.game_state = 3;
            }

            gbl.area2_ptr.field_594 &= 1;

            seg051.FillChar(0, 2, gbl.byte_1EE72);
            gbl.byte_1EE8C = 0;
            ovr025.load_pic();
        }


        internal static void CMD_OnGotoGoSub() /* sub_27AE5 */
        {
            ovr008.vm_LoadCmdSets(2);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);
            gbl.ecl_offset--;
            ovr008.vm_LoadCmdSets(var_2);

            if (var_1 < var_2)
            {
                if (gbl.command == 0x25)
                {
                    // Goto
                    gbl.ecl_offset = gbl.cmd_opps[var_1 + 1].Word;
                }
                else
                {
                    // Gosub
                    ovr008.vm_gosub(var_1 + 1);
                }
            }
        }


        internal static void CMD_Treasure() /* load_item */
        {
            byte[] data;
            short dataSize;
            byte var_65 = 0;
            byte var_64;
            byte var_63;
            byte var_62;
            byte var_2;

            ovr008.vm_LoadCmdSets(8);

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = ovr008.vm_GetCmdValue(i + 1);
            }

            var_2 = (byte)ovr008.vm_GetCmdValue(8);

            if (var_2 < 0x80)
            {
                seg042.load_decode_dax(out data, out dataSize, var_2, string.Format("ITEM{0}.dax", gbl.game_area));

                if (dataSize == 0)
                {
                    seg041.displayAndDebug("Unable to find item file", 0, 15);

                    seg043.print_and_exit();
                }

                for( int offset = 0; offset < dataSize; offset += Item.StructSize )
                {
                    Item item = new Item(data, offset);

                    if (gbl.item_pointer == null)
                    {
                        gbl.item_pointer = item;
                    }
                    else
                    {
                        Item tmpItem = gbl.item_pointer;
                        gbl.item_pointer = item;
                        gbl.item_pointer.next = tmpItem;
                    }
                } 

                data = null;
            }
            else if (var_2 != 0xff)
            {
                for (int var_3 = 1; var_3 <= (var_2 - 0x80); var_3++)
                {
                    var_63 = ovr024.roll_dice(100, 1);

                    if (var_63 >= 1 && var_63 <= 0x3c)
                    {
                        var_64 = ovr024.roll_dice(100, 1);

                        if ((var_64 >= 1 && var_64 <= 0x2F) ||
                            (var_64 >= 0x32 && var_64 <= 0x3b))
                        {
                            if (var_64 == 0x2D)
                            {
                                var_65 = 0x3B;
                            }
                            else
                            {
                                var_65 = var_64;
                            }
                        }
                        else if (var_64 >= 0x3C && var_64 <= 0x5A)
                        {
                            var_64 = ovr024.roll_dice(10, 1);

                            if (var_64 >= 1 && var_64 <= 4)
                            {
                                var_65 = 0x24;
                            }
                            else if (var_64 >= 5 && var_64 <= 7)
                            {
                                var_65 = 0x23;
                            }
                            else if (var_64 == 8)
                            {
                                var_65 = 0x22;
                            }
                            else if (var_64 == 9)
                            {
                                var_65 = 0x25;
                            }
                            else if (var_64 == 10)
                            {
                                var_65 = 0x26;
                            }
                        }
                        else if (var_64 >= 0x5B && var_64 <= 0x5E)
                        {
                            var_65 = 0x49;
                        }
                        else if (var_64 >= 0x5F && var_64 <= 0x61)
                        {
                            var_65 = 0x5D;
                        }
                        else if (var_64 >= 0x62 && var_64 <= 0x64)
                        {
                            var_65 = 0x4D;
                        }
                        else
                        {
                            var_65 = 0x3B;
                        }
                    }
                    else if (var_63 >= 0x3d && var_63 <= 0x55)
                    {
                        var_65 = 0x3D;
                    }
                    else if (var_63 >= 0x56 && var_63 <= 0x5C)
                    {
                        var_65 = 0x3E;
                    }
                    else if (var_63 >= 0x5B && var_63 <= 0x62)
                    {
                        var_62 = ovr024.roll_dice(15, 1);

                        if (var_62 >= 1 && var_62 <= 9)
                        {
                            var_65 = 0x47;
                        }
                        else if (var_62 == 10)
                        {
                            var_65 = 0x54;
                        }
                        else if (var_62 >= 11 && var_62 <= 15)
                        {
                            var_65 = 0x4F;
                        }
                    }
                    else if (var_63 == 0x63 || var_63 == 0x64)
                    {
                        var_65 = 0x3B;
                    }

                    Item item;
                    ovr022.sub_5A007(out item, var_65);

                    if (gbl.item_pointer == null)
                    {
                        gbl.item_pointer = item.ShallowClone();
                        gbl.item_pointer.next = null;
                    }
                    else
                    {
                        Item var_5B = gbl.item_pointer;

                        gbl.item_pointer = item.ShallowClone();
                        gbl.item_pointer.next = var_5B;
                    }
                }

                Item var_61 = gbl.item_pointer;

                while (var_61 != null)
                {
                    ovr025.ItemDisplayNameBuild(0, false, 0, 0, var_61, gbl.player_ptr);
                    var_61 = var_61.next;
                }
            }
        }


        internal static void CMD_Rob() /* sub_27F76*/
        {
            double var_11;
            object var_B;
            Player player;
            byte var_3;
            byte var_2;

            ovr008.vm_LoadCmdSets(3);
            byte allParty = (byte)ovr008.vm_GetCmdValue(1);
            var_2 = (byte)ovr008.vm_GetCmdValue(2);

            var_11 = (100 - var_2) / 100.0;
            var_3 = (byte)ovr008.vm_GetCmdValue(3);

            if (allParty == 0)
            {
                ovr008.RobMoney(gbl.player_ptr, var_11);
                ovr008.RobItems(gbl.player_ptr, var_3);
            }
            else
            {
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    var_B = player.itemsPtr;
                    ovr008.RobMoney(player, var_11);
                    ovr008.RobItems(player, var_3);
                    player = player.next_player;
                }
            }
        }


        internal static void CMD_EncounterMenu()
        {
            ushort var_43D;
            int var_43B;
            byte var_43A;
            string var_439;
            string var_437;
            bool useOverlay;
            bool var_40D;
            byte init_max;
            byte init_min;
            byte var_40A;
            byte var_409;
            byte var_408;
            byte var_407;
            string var_406 = string.Empty; /* Simeon */
            string[] var_405 = new string[3];
            byte[] var_6 = new byte[5];
            byte var_1;

            gbl.byte_1EE95 = 1;
            gbl.byte_1EE90 = 0;
            gbl.DelayBetweenCharacters = true;

            ovr008.calc_group_inituative(out init_min, out var_40A);

            var_439 = string.Empty;
            ovr008.vm_LoadCmdSets(0x0e);

            gbl.byte_1D92B = (byte)ovr008.vm_GetCmdValue(1);
            gbl.area2_ptr.field_580 = ovr008.vm_GetCmdValue(2);
            gbl.byte_1D92C = (byte)ovr008.vm_GetCmdValue(3);

            var_43D = gbl.cmd_opps[4].Word;

            for (int i = 0; i < 5; i++)
            {
                var_6[i] = (byte)ovr008.vm_GetCmdValue(i + 5);
            }

            for (var_409 = 0; var_409 < 3; var_409++)
            {
                var_405[var_409] = gbl.unk_1D972[var_409 + 1];
            }

            var_407 = (byte)ovr008.vm_GetCmdValue(0x0d);
            var_408 = (byte)ovr008.vm_GetCmdValue(0x0e);

            gbl.area2_ptr.field_582 = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (gbl.area2_ptr.field_580 < gbl.area2_ptr.field_582)
            {
                gbl.area2_ptr.field_582 = gbl.area2_ptr.field_580;
            }

            ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);

            do
            {
                if (gbl.byte_1EE8C == 0 ||
                    gbl.byte_1EE8D == 0 ||
                    gbl.area_ptr.field_1CC == 0 ||
                    gbl.byte_1D5B4 == 0x50)
                {
                    useOverlay = false;
                }
                else
                {
                    useOverlay = true;
                }

                var_40D = (gbl.area_ptr.field_1CC != 0);

                init_max = 0;
                gbl.textXCol = 1;
                gbl.textYCol = 0x11;

                switch (gbl.area2_ptr.field_582)
                {
                    case 0:
                        var_43B = 0;

                        do
                        {
                            var_406 = var_405[var_43B];
                            var_43B++;
                        } while (var_406.Length == 0 && var_43B < 3);
                        break;

                    case 1:
                        var_43B = 1;

                        do
                        {
                            var_406 = var_405[var_43B];
                            var_43B++;

                            if (var_43B > 2)
                            {
                                var_43B = 0;
                            }
                        } while (var_406.Length == 0 && var_43B != 1);
                        break;

                    case 2:
                        var_43B = 2;

                        do
                        {
                            var_406 = var_405[var_43B];

                            var_43B++;
                            if (var_43B > 2)
                            {
                                var_43B = 0;
                            }

                        } while (var_406.Length == 0 && var_43B != 2);
                        break;
                }

                if (var_406.Length == 0)
                {
                    var_40D = false;
                }

                seg041.press_any_key(var_406, var_40D, 0, 10, 0x16, 0x26, 0x11, 1);

                if (gbl.area2_ptr.field_582 == 0 ||
                    gbl.area_ptr.field_1CC == 0)
                {
                    var_437 = "~COMBAT ~WAIT ~FLEE ~PARLAY";
                }
                else
                {
                    var_437 = "~COMBAT ~WAIT ~FLEE ~ADVANCE";
                }

                var_1 = ovr008.sub_317AA(useOverlay, 0, 15, 10, 13, var_437, var_439);

                if (gbl.area2_ptr.field_582 == 0 ||
                    gbl.area_ptr.field_1CC == 0)
                {
                    if (var_1 == 3)
                    {
                        var_1 = 4;
                    }
                }

                var_43A = var_6[var_1];

                switch (var_43A)
                {
                    case 0:
                        if (var_1 != 2)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else
                        {
                            if (init_min >= var_407)
                            {
                                ovr008.vm_SetMemoryValue(2, var_43D);
                            }
                            else
                            {
                                ovr008.vm_SetMemoryValue(1, var_43D);
                            }
                        }
                        break;

                    case 1:
                        if (var_1 == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (var_1 == 1)
                        {
                            init_max = 1;
                            seg041.press_any_key("Both sides wait.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                        }
                        else if (var_1 == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }
                        else if (var_1 == 3)
                        {
                            if (gbl.area2_ptr.field_582 != 0)
                            {
                                gbl.area2_ptr.field_582--;

                                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
                            }
                            else
                            {
                                seg041.press_any_key("Both sides wait.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            }

                            init_max = 1;
                        }
                        else if (var_1 == 4)
                        {
                            if (gbl.area2_ptr.field_582 > 0)
                            {
                                gbl.area2_ptr.field_582--;
                                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
                                init_max = 1;
                            }
                            else
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                        }
                        break;

                    case 2:
                        if (var_1 == 0)
                        {
                            if (var_408 > var_40A)
                            {
                                ovr008.vm_SetMemoryValue(0, var_43D);

                                gbl.textXCol = 1;
                                gbl.textYCol = 0x11;
                                seg041.press_any_key("The monsters flee.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            }
                            else
                            {
                                ovr008.vm_SetMemoryValue(1, var_43D);
                            }
                        }
                        else if (var_1 >= 1 && var_1 <= 4)
                        {
                            ovr008.vm_SetMemoryValue(0, var_43D);

                            gbl.textXCol = 1;
                            gbl.textYCol = 0x11;
                            seg041.press_any_key("The monsters flee.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                        }
                        break;

                    case 3:
                        if (var_1 == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (var_1 == 1 || var_1 == 3)
                        {
                            if (gbl.area2_ptr.field_582 != 0)
                            {
                                gbl.area2_ptr.field_582--;

                                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
                            }
                            else
                            {
                                seg041.press_any_key("Both sides wait.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            }

                            init_max = 1;
                        }
                        else if (var_1 == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }
                        else if (var_1 == 4)
                        {
                            if (gbl.area2_ptr.field_582 <= 0)
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                            else
                            {
                                gbl.area2_ptr.field_582--;

                                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
                                init_max = 1;
                            }
                        }
                        break;

                    case 4:
                        if (var_1 == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (var_1 == 1 || var_1 == 3 || var_1 == 4)
                        {

                            if (gbl.area2_ptr.field_582 <= 0)
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                            else
                            {
                                gbl.area2_ptr.field_582 -= 1;

                                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
                                init_max = 1;
                            }
                        }
                        else if (var_1 == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }

                        break;
                }
            } while (init_max != 0);

            ovr027.redraw_screen();
            gbl.DelayBetweenCharacters = false;
            gbl.byte_1EE95 = 0;
        }


        internal static void CMD_Parlay()
        {
            char var_B;
            ushort var_A;

            byte[] var_8 = new byte[5];

            byte var_2;
            byte var_1;

            ovr008.vm_LoadCmdSets(6);
            var_B = ' ';

            for (int i = 0; i < 5; i++)
            {
                var_8[i] = (byte)ovr008.vm_GetCmdValue(i + 1);
            }

            var_1 = ovr008.sub_317AA(false, 0, 15, 10, 13, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", var_B.ToString());
            var_A = gbl.cmd_opps[6].Word;

            var_2 = var_8[var_1];

            ovr008.vm_SetMemoryValue(var_2, var_A);
        }


        internal static void CMD_FindItem() // sub_28856
        {
            bool found;
            Item item_ptr;
            Player player;
            byte var_1;

            ovr008.vm_LoadCmdSets(1);
            found = false;
            var_1 = (byte)ovr008.vm_GetCmdValue(1);

            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            gbl.item_find[1] = true;
            player = gbl.player_next_ptr;

            while (player != null && found == false)
            {
                item_ptr = player.itemsPtr;

                while (item_ptr != null && found == false)
                {
                    if (var_1 == item_ptr.type)
                    {
                        gbl.item_find[0] = true;
                        gbl.item_find[1] = false;
                        found = true;
                    }

                    item_ptr = item_ptr.next;
                }
                player = player.next_player;
            }
        }


        internal static void CMD_Delay()
        {
            gbl.ecl_offset++;
            seg041.GameDelay();
        }


        internal static void CMD_Damage() /* sub_28958 */
        {
            byte var_1B;
            byte var_1A;
            /* byte var_19; */
            Player player01;
            Player player02;
            Player player03;
            short var_C;
            byte var_A;
            byte var_9;
            byte var_8 = 0; /* Simeon */
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            player01 = gbl.player_ptr;
            /* var_19 = 0; */
            var_1A = 0;
            player03 = gbl.player_next_ptr;
            ovr008.vm_LoadCmdSets(5);
            var_1 = (byte)ovr008.vm_GetCmdValue(1);
            var_2 = (byte)ovr008.vm_GetCmdValue(2);
            var_3 = (byte)ovr008.vm_GetCmdValue(3);
            var_7 = (byte)ovr008.vm_GetCmdValue(4);
            var_6 = (byte)ovr008.vm_GetCmdValue(5);


            var_C = (short)(ovr024.roll_dice(var_3, var_2) + var_7);

            var_1B = (byte)(var_1 & 0x10);

            if ((var_1 & 0x40) != 0)
            {
                var_1A = 1;
            }
            else
            {
                var_8 = ovr024.roll_dice(gbl.area2_ptr.field_67C, 1);
            }

            if ((var_1 & 0x80) != 0)
            {
                var_5 = (byte)(var_1 & 0x1f);
                var_9 = (byte)(var_6 & 7);

                if (var_1A != 0)
                {

                    while (player03 != null)
                    {
                        if ((var_1 & 0x20) != 0)
                        {
                            ovr008.sub_32200(player03, var_C);
                        }
                        else
                        {
                            if (ovr024.do_saving_throw((sbyte)var_5, var_9, player03) == false)
                            {
                                ovr008.sub_32200(player03, var_C);
                            }
                            else if (var_1B != 0)
                            {
                                ovr008.sub_32200(player03, var_C);
                            }
                        }
                        player03 = player03.next_player;
                    }
                }
                else
                {
                    if ((var_6 & 0x80) != 0)
                    {
                        if (var_9 == 0 ||
                            ovr024.do_saving_throw((sbyte)var_5, (byte)(var_9 - 1), gbl.player_ptr) == false)
                        {
                            ovr008.sub_32200(gbl.player_ptr, var_C);
                        }
                        else
                        {
                            if (var_1B != 0)
                            {
                                ovr008.sub_32200(gbl.player_ptr, var_C);
                            }
                        }
                    }
                    else
                    {
                        for (var_4 = 2; var_4 <= var_8; var_4++)
                        {
                            player03 = player03.next_player;
                        }

                        if (ovr024.do_saving_throw((sbyte)var_5, var_9, player03) == false)
                        {
                            ovr008.sub_32200(player03, var_C);
                        }
                        else if (var_1B != 0)
                        {
                            ovr008.sub_32200(player03, var_C);
                        }
                    }
                }
            }
            else
            {
                for (var_4 = 1; var_4 <= var_1; var_4++)
                {
                    player03 = gbl.player_next_ptr;
                    var_8 = ovr024.roll_dice(gbl.area2_ptr.field_67C, 1);

                    var_A = 1;

                    while (var_A < var_8)
                    {
                        player03 = player03.next_player;
                        var_A++;
                    }

                    if (ovr024.sub_641DD(var_6, player03) == true)
                    {
                        ovr008.sub_32200(player03, var_C);
                    }

                    var_C = (short)(var_7 + ovr024.roll_dice(var_3, var_2));

                }

            }

            player02 = gbl.player_next_ptr;
            gbl.byte_1B2F0 = 1;

            while (player02 != null)
            {
                if (player02.in_combat == true)
                {
                    gbl.byte_1B2F0 = 0;
                }

                player02 = player02.next_player;
            }

            if (gbl.byte_1B2F0 != 0)
            {
                seg037.draw8x8_01();
                gbl.textXCol = 2;
                gbl.textYCol = 2;

                seg041.press_any_key("The entire party is killed!", true, 0, 10, 0x16, 0x26, 1, 1);
                seg049.SysDelay(0x0BB8);
            }

            gbl.player_ptr = player01;
            seg041.displayAndDebug("press <enter>/<return> to continue", 0, 15);
        }


        internal static void CMD_SpriteOff() /* sub_28CB6 */
        {
            gbl.ecl_offset++;
            if (gbl.displayPlayerSprite)
            {
                gbl.byte_1D8AA = 1;
                ovr029.sub_6F0BA();
                gbl.displayPlayerSprite = false;
                gbl.byte_1EE8C = 0;
            }
        }


        internal static void CMD_EclClock() /* sub_28CDA */
        {
            ovr008.vm_LoadCmdSets(2);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);

            ovr021.sub_583FA(var_2, var_1);
        }


        internal static void CMD_PrintReturn() // sub_28D0F
        {
            gbl.ecl_offset++;

            if (gbl.byte_1EE8B != 0)
            {
                gbl.textXCol = 1;
                gbl.textYCol++;
                gbl.byte_1EE8B = 0;
            }
            else
            {
                gbl.textXCol = 1;
                gbl.textYCol++;
            }
        }


        internal static void CMD_ClearBox() // sub_28D38 
        {
            gbl.ecl_offset++;
            seg037.draw8x8_03();
            ovr025.Player_Summary(gbl.player_ptr);
            ovr025.display_map_position_time();

            ovr030.sub_7000A(gbl.byte_1D556.ptrs[0].field_4, true, 3, 3);
            ovr025.display_map_position_time();
            gbl.byte_1EE98 = 0;
            gbl.byte_1EE8A = 1;
        }


        internal static void CMD_Who() // sub_28D7F
        {
            ovr008.vm_LoadCmdSets(1);
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

            string var_100 = gbl.unk_1D972[1];

            ovr025.selectAPlayer(ref gbl.player_ptr, false, var_100);
        }


        internal static void CMD_AddNPC() // sub_28DCA
        {
            byte var_6;
            byte var_5;

            ovr008.vm_LoadCmdSets(2);
            var_5 = (byte)ovr008.vm_GetCmdValue(1);

            ovr017.sub_4A57D(var_5);

            var_6 = (byte)ovr008.vm_GetCmdValue(2);

            var_6 >>= 1;
            var_6 |= 0x80;

            gbl.player_ptr.field_F7 = var_6;

            ovr025.sub_66C20(gbl.player_ptr);
            ovr025.Player_Summary(gbl.player_ptr);
        }


        internal static void CMD_Spell()
        {
            ushort var_D;
            ushort var_B;
            byte var_9;
            byte var_8;
            Player var_7;
            byte var_3;
            byte var_2;
            byte var_1;

            var_9 = 0;
            var_8 = 0;
            ovr008.vm_LoadCmdSets(3);
            var_1 = (byte)ovr008.vm_GetCmdValue(1);
            var_B = gbl.cmd_opps[2].Word;
            var_D = gbl.cmd_opps[3].Word;
            var_2 = 1;
            var_3 = 0;
            var_7 = gbl.player_next_ptr;

            while (var_7 != null && var_8 == 0)
            {
                var_2 = 1;

                do
                {
                    if (var_7.spell_list[var_2] == var_1)
                    {
                        var_8 = 1;
                    }
                    else
                    {
                        if (var_2 <= 100)
                        {
                            var_2++;
                        }
                        else
                        {
                            var_9 = 1;
                        }
                    }
                } while (var_9 == 0 && var_8 == 0);

                var_9 = 0;
                var_7 = var_7.next_player;

                if (var_7 != null &&
                    var_8 == 0)
                {
                    var_3++;
                }
            }

            if (var_2 > 100)
            {
                var_2 = 0x0FF;
            }

            ovr008.vm_SetMemoryValue(var_2, var_B);
            ovr008.vm_SetMemoryValue(var_3, var_D);
        }


        internal static void CMD_Call()
        {
            ovr008.vm_LoadCmdSets(1);

            ushort var_2 = gbl.cmd_opps[1].Word;
            ushort var_4 = (ushort)(var_2 - 0x7fff);

            //System.Console.WriteLine("  CMD_Call: {0:X}", var_4);

            switch (var_4)
            {
                case 0xAE11:
                    gbl.byte_1D53D = ovr031.sub_717A5(gbl.mapPosY, gbl.mapPosX);

                    if (gbl.byte_1AB0B != 0)
                    {
                        if (gbl.byte_1EE8C != 0 || gbl.displayPlayerSprite || gbl.byte_1EE91 != 0 ||
                            gbl.byte_1EE92 != 0 || gbl.byte_1EE94 != 0)
                        {
                            gbl.byte_1D8AA = 1;
                            ovr029.sub_6F0BA();
                            ovr025.display_map_position_time();
                            gbl.byte_1EE94 = 0;
                            gbl.byte_1EE91 = 0;
                            gbl.byte_1EE92 = 0;
                            gbl.byte_1EE8C = 0;
                            gbl.displayPlayerSprite = false;

                            gbl.byte_1D53C = ovr031.getMap_XXX(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                        }
                    }
                    break;

                case 1:
                    ovr008.duel(1);
                    break;

                case 2:
                    ovr008.duel(0);
                    break;

                case 0x3201:

                    if (gbl.word_1EE76 == 8)
                    {
                        seg044.sound_sub_120E0(gbl.sound_a_188D2);
                    }
                    else if (gbl.word_1EE76 == 10)
                    {
                        seg044.sound_sub_120E0(gbl.sound_b_188D4);
                    }
                    else
                    {
                        seg044.sound_sub_120E0(gbl.sound_a_188D2);
                    }
                    break;

                case 0x401F:
                    ovr008.sub_31B01();
                    break;

                case 0x4019:
                    if (gbl.area_ptr.field_1CC == 0)
                    {
                        gbl.byte_1D53C = ovr031.getMap_XXX(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                    }
                    break;

                case 0xE804:
                    ovr030.sub_7000A(gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_4, true, 3, 3);
                    gbl.byte_1D556.curFrame++;

                    if (gbl.byte_1D556.curFrame > gbl.byte_1D556.numFrames)
                    {
                        gbl.byte_1D556.curFrame = 1;
                    }

                    seg041.GameDelay();
                    break;
            }
        }


        internal static void sub_29094()
        {
            bool var_1 = false;

            RunEclVm(gbl.word_1B2D7);
            ovr016.make_camp(out var_1);

            if (var_1 == true)
            {
                ovr025.load_pic();
                RunEclVm(gbl.word_1B2D9);
            }
            gbl.byte_1D8AA = 1;
            ovr029.sub_6F0BA();
            gbl.byte_1C01B = 0;
        }


        internal static void CMD_Program() //YourHaveWon
        {
            Player play_ptr;
            Player player;
            char saveYes;
            ushort var_3;
            byte var_1;

            if (gbl.byte_1AB0A != 0)
            {
                gbl.player_ptr = gbl.player_ptr2;
                gbl.byte_1AB0A = 0;
            }

            ovr008.vm_LoadCmdSets(1);
            var_1 = (byte)ovr008.vm_GetCmdValue(1);

            if (var_1 == 0)
            {
                ovr018.startGameMenu();
                if (gbl.byte_1D5B4 != 0x50 &&
                    gbl.area_ptr.field_1CC == 0)
                {
                    ovr025.load_pic();
                }
            }
            else if (var_1 == 8)
            {
                ovr019.end_game_text();
                gbl.gameWon = true;
                gbl.area_ptr.field_3FA = 0xff;
                gbl.area2_ptr.field_550 = 0xff;
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    play_ptr = player;
                    play_ptr.hit_point_current = play_ptr.hit_point_max;
                    play_ptr.health_status = Status.okey;
                    play_ptr.in_combat = true;

                    player = player.next_player;
                }

                ovr018.startGameMenu();
                saveYes = ovr027.yes_no(15, 10, 13, "You've won. Save before quitting? ");

                if (saveYes == 'Y')
                {
                    ovr017.SaveGame();
                }

                seg043.print_and_exit();
            }
            else if (var_1 == 9)
            {
                var_3 = gbl.ecl_offset;
                sub_29094();
                gbl.ecl_offset = var_3;
                CMD_Exit();
            }
            else if (var_1 == 3)
            {
                gbl.byte_1B2F0 = 1;
                CMD_Exit();
            }
        }


        internal static void CMD_Protection() // sub_2923F
        {
            seg051.FillChar(0, 2, gbl.byte_1EE72);
            gbl.byte_1EE8C = 0;
            ovr008.vm_LoadCmdSets(1);

            //ovr004.copy_protection();
            ovr025.load_pic();
        }


        internal static void CMD_Dump() // sub_29271
        {
            gbl.ecl_offset++;

            ovr018.free_players(1, false);

            gbl.player_ptr2 = gbl.player_ptr;

            ovr025.Player_Summary(gbl.player_ptr);
        }


        internal static void CMD_FindSpecial() // sub_292A5
        {
            Affects var_5;
            Affect var_4;

            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            ovr008.vm_LoadCmdSets(1);
            var_5 = (Affects)ovr008.vm_GetCmdValue(1);

            if (ovr025.find_affect(out var_4, var_5, gbl.player_ptr) == true)
            {
                gbl.item_find[0] = true;
            }
            else
            {
                gbl.item_find[1] = true;
            }
        }


        internal static void CMD_DestroyItems() // sub_292F9
        {
            ovr008.vm_LoadCmdSets(1);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);

            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                Item item = player.itemsPtr;
                while (item != null)
                {
                    Item var_D = item.next;

                    if (var_1 == item.type)
                    {
                        ovr025.lose_item(item, player);
                    }
                    item = var_D;
                }

                ovr025.sub_66C20(player);
                player = player.next_player;
            }
        }


        internal static void commandTable()
        {
            byte al = gbl.command;

            switch (gbl.command)
            {
                case 0x00: CMD_Exit(); break;
                case 0x01: CMD_Goto(); break;
                case 0x02: CMD_Gosub(); break;
                case 0x03: CMD_Compare(); break;
                case 0x04: CMD_AddSubDivMulti(); break;
                case 0x05: CMD_AddSubDivMulti(); break;
                case 0x06: CMD_AddSubDivMulti(); break;
                case 0x07: CMD_AddSubDivMulti(); break;
                case 0x08: CMD_Random(); break;
                case 0x09: CMD_Save(); break;
                case 0x0A: CMD_LoadCharacter(); break;
                case 0x0B: CMD_LoadMonster(); break;
                case 0x0C: CMD_SetupMonster(); break;
                case 0x0D: CMD_Approach(); break;
                case 0x0E: CMD_Picture(); break;
                case 0x0F: CMD_InputNumber(); break;
                case 0x10: CMD_InputString(); break;
                case 0x11: CMD_Print(); break;
                case 0x12: CMD_Print(); break;
                case 0x13: CMD_Return(); break;
                case 0x14: CMD_CompareAnd(); break;
                case 0x15: CMD_VertMenu(); break;
                case 0x16: CMD_If(); break;
                case 0x17: CMD_If(); break;
                case 0x18: CMD_If(); break;
                case 0x19: CMD_If(); break;
                case 0x1A: CMD_If(); break;
                case 0x1B: CMD_If(); break;
                case 0x1C: CMD_ClearMonsters(); break;
                case 0x1D: CMD_PartyStrength(); break;
                case 0x1E: CMD_CheckParty(); break;
                case 0x20: CMD_NewECL(); break;
                case 0x21: CMD_LoadFiles(); break;
                case 0x22: CMD_PartySurprise(); break;
                case 0x23: CMD_Surprise(); break;
                case 0x24: CMD_Combat(); break;
                case 0x25: CMD_OnGotoGoSub(); break;
                case 0x26: CMD_OnGotoGoSub(); break;
                case 0x27: CMD_Treasure(); break;
                case 0x28: CMD_Rob(); break;
                case 0x29: CMD_EncounterMenu(); break;
                case 0x2A: CMD_GetTable(); break;
                case 0x2B: CMD_HorizontalMenu(); break;
                case 0x2C: CMD_Parlay(); break;
                case 0x2D: CMD_Call(); break;
                case 0x2E: CMD_Damage(); break;
                case 0x2F: CMD_AndOr(); break;
                case 0x30: CMD_AndOr(); break;
                case 0x31: CMD_SpriteOff(); break;
                case 0x32: CMD_FindItem(); break;
                case 0x33: CMD_PrintReturn(); break;
                case 0x34: CMD_EclClock(); break;
                case 0x35: CMD_SaveTable(); break;
                case 0x36: CMD_AddNPC(); break;
                case 0x37: CMD_LoadFiles(); break;
                case 0x38: CMD_Program(); break;
                case 0x39: CMD_Who(); break;
                case 0x3A: CMD_Delay(); break;
                case 0x3B: CMD_Spell(); break;
                case 0x3C: CMD_Protection(); break;
                case 0x3D: CMD_ClearBox(); break;
                case 0x3E: CMD_Dump(); break;
                case 0x3F: CMD_FindSpecial(); break;
                case 0x40: CMD_DestroyItems(); break;
                default:
                    break;
            }
        }


        internal static void RunEclVm(ushort offset) // sub_29607
        {
            gbl.ecl_offset = offset;
            gbl.stopVM = false;

            //System.Console.Out.WriteLine("RunEclVm {0,4:X} start", offset);
            
            while (gbl.stopVM == false &&
                   gbl.byte_1B2F0 == 0)
            {
                gbl.byte_1D928 = gbl.command;

                gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];
                
                if (gbl.printCommands == true)
                {
                    print_command();
                }

                commandTable();
            }

            gbl.stopVM = false;
            //System.Console.Out.WriteLine("RunEclVm {0,4:X} end", offset);
        }


        internal static void sub_29677()
        {
            do
            {
                ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);
                gbl.byte_1D5AB = string.Empty;
                gbl.byte_1D5B5 = 0x0FF;
                gbl.byte_1AB09 = 0;
                gbl.byte_1D53D = ovr031.sub_717A5(gbl.mapPosY, gbl.mapPosX);

                gbl.area2_ptr.field_5AA = 0;

                gbl.player_ptr2 = gbl.player_ptr;

                RunEclVm(gbl.ecl_initial_entryPoint);

                if (gbl.byte_1AB09 == 0)
                {
                    gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                }

                if (gbl.byte_1AB09 == 0)
                {
                    if ((gbl.last_game_state == 4 && gbl.game_state == 4) ||
                        (gbl.last_game_state != 4 && gbl.byte_1AB0B != 0))
                    {
                        ovr029.sub_6F0BA();
                    }
                    gbl.byte_1AB09 = 0;

                    RunEclVm(gbl.word_1B2D3);

                    if (gbl.byte_1AB09 == 0)
                    {
                        RunEclVm(gbl.word_1B2D5);

                        if (gbl.byte_1AB09 == 0)
                        {
                            gbl.player_ptr = gbl.player_ptr2;
                            ovr025.Player_Summary(gbl.player_ptr);
                        }
                    }

                }
            } while (gbl.byte_1AB09 != 0);

            gbl.last_game_state = gbl.game_state;
        }


        internal static void sub_29758()
        {
            char var_1;

            gbl.player_ptr2 = gbl.player_ptr;

            gbl.byte_1D8AA = 1;
            gbl.byte_1EE8E = 0;
            gbl.byte_1AB0C = 0;
            gbl.byte_1AB0D = 0;
            gbl.byte_1AB0A = 0;
            gbl.byte_1AB0B = 0;
            gbl.byte_1EE98 = 1;
            gbl.game_state = 4;
            gbl.byte_1AB09 = 0;

            if (gbl.area_ptr.field_1E4 == 0)
            {
                gbl.byte_1EE98 = 0;

                if (gbl.inDemo == true)
                {
                    gbl.byte_1EE88 = 0x52;
                }
                else
                {
                    gbl.byte_1EE88 = 1;

                    ovr025.Player_Summary(gbl.player_ptr);
                }
            }
            else
            {
                gbl.byte_1EE88 = (byte)(gbl.area_ptr.field_1E4);
            }

            if (gbl.area_ptr.field_1CC == 0)
            {
                gbl.game_state = 3;
            }

            if (gbl.byte_1B2EB != 0 ||
                gbl.area_ptr.field_1E4 == 0)
            {
                ovr008.load_ecl_dax(gbl.byte_1EE88);
            }
            else
            {
                gbl.byte_1AB0B = 1;
            }

            ovr008.vm_init_ecl();

            RunEclVm(gbl.ecl_initial_entryPoint);

            if (gbl.inDemo == true)
            {
                do
                {
                    ovr018.free_players(1, true);
                } while (gbl.player_next_ptr != null);
            }
            else
            {
                if (gbl.byte_1AB09 == 0)
                {
                    gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                }
                else
                {
                    sub_29677();
                }

                if (gbl.game_state != 3 &&
                    gbl.byte_1B2EB != 0)
                {
                    if (gbl.byte_1EE98 == 1)
                    {
                        ovr025.load_pic();
                    }

                    gbl.byte_1D8AA = 1;
                    ovr029.sub_6F0BA();
                }

                gbl.byte_1B2EB = 0;

                do
                {
                    var_1 = ovr015.sub_438DF();

                    gbl.player_ptr2 = gbl.player_ptr;

                    if (gbl.byte_1AB09 == 0)
                    {
                        gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                    }

                    while ((gbl.area2_ptr.field_594 > 1 || char.ToUpper(var_1) == 'E') &&
                        gbl.byte_1B2F0 == 0)
                    {
                        if (char.ToUpper(var_1) == 'E')
                        {
                            sub_29094();
                        }
                        else
                        {
                            gbl.byte_1EE89 = (byte)(gbl.area2_ptr.field_594 & 1);
                            gbl.area2_ptr.field_594 = 1;
                            gbl.byte_1D8AA = 1;
                            ovr029.sub_6F0BA();

                            RunEclVm(gbl.word_1B2D5);

                            if (gbl.byte_1AB09 != 0)
                            {
                                sub_29677();
                            }

                            gbl.area2_ptr.field_594 = gbl.byte_1EE89;
                        }

                        if (gbl.byte_1B2F0 == 0)
                        {
                            var_1 = ovr015.sub_438DF();
                            gbl.player_ptr2 = gbl.player_ptr;
                        }
                    }


                    if (gbl.byte_1B2F0 == 0)
                    {
                        RunEclVm(gbl.word_1B2D3);
                    }

                    if (gbl.byte_1AB09 != 0)
                    {
                        sub_29677();
                    }
                    else
                    {
                        if (gbl.byte_1B2F0 == 0)
                        {
                            gbl.area_ptr.field_1E0 = gbl.mapPosX;
                            gbl.area_ptr.field_1E2 = gbl.mapPosY;

                            ovr015.locked_door();
                            ovr029.sub_6F0BA();

                            if (gbl.area_ptr.field_1E0 != gbl.mapPosX ||
                                gbl.area_ptr.field_1E2 != gbl.mapPosY)
                            {
                                seg044.sound_sub_120E0(gbl.sound_a_188D2);
                            }

                            gbl.byte_1EE8C = 0;
                            gbl.byte_1EE8D = 1;
                            RunEclVm(gbl.word_1B2D5);
                            if (gbl.byte_1AB09 != 0)
                            {
                                sub_29677();
                            }
                        }
                    }
                } while (gbl.byte_1B2F0 == 0);

                gbl.byte_1B2F0 = 0;
            }
        }

        internal static void print_command()
        {
            string name;
            /*
            ^:b+throw new System.NotSupportedException();//cmp:b+al,:b{0x:h}\n:b+throw new System.NotSupportedException();//jnz:b+:i\n:b+throw new System.NotSupportedException();//mov:b+di,:boffset:b{:i}\n:b+throw new System.NotSupportedException();//push:b+cs\n:b+throw new System.NotSupportedException();//push:b+di\n:b+throw new System.NotSupportedException();//les:b+di,:b+\[bp\+arg_2\]\n:b+throw new System.NotSupportedException();//push:b+es\n:b+throw new System.NotSupportedException();//push:b+di\n:b+throw new System.NotSupportedException();//mov:b+ax,:b0x0FF\n:b+throw new System.NotSupportedException();//push:b+ax\n:b+throw new System.NotSupportedException();//call:b+operator=\(String:b&,String:b&,Byte\)\n:b+throw new System.NotSupportedException();//jmp.*$
            case \1: arg_2 = \2; break;
            */
            switch (gbl.command)
            {
                case 0: name = "EXIT"; break;
                case 1: name = "GOTO"; break;
                case 2: name = "GOSUB"; break;
                case 3: name = "COMPARE"; break;
                case 4: name = "ADD"; break;
                case 5: name = "SUBTRAT"; break;
                case 6: name = "DIVIDE"; break;
                case 7: name = "MULTIPLY"; break;
                case 8: name = "RANDOM"; break;
                case 9: name = "SAVE"; break;
                case 0x0A: name = "LOAD CHARACTER"; break;
                case 0x0B: name = "LOAD MONSTER"; break;
                case 0x0C: name = "SETUP MONSTER"; break;
                case 0x0D: name = "APPROACH"; break;
                case 0x0E: name = "PICTURE"; break;
                case 0x0F: name = "INPUT NUMBER"; break;
                case 0x10: name = "INPUT STRING"; break;
                case 0x11: name = "PRINT"; break;
                case 0x12: name = "PRINTCLEAR"; break;
                case 0x13: name = "RETURN"; break;
                case 0x14: name = "COMPARE AND"; break;
                case 0x15: name = "VERTICAL MENU"; break;
                case 0x16: name = "IF = "; break;
                case 0x17: name = "IF <>"; break;
                case 0x18: name = "IF <"; break;
                case 0x19: name = "IF >"; break;
                case 0x1A: name = "IF <="; break;
                case 0x1B: name = "IF >="; break;
                case 0x1C: name = "CLEARMONSTERS"; break;
                case 0x1D: name = "PARTYSTRENGTH"; break;
                case 0x1E: name = "CHECKPARTY"; break;
                case 0x20: name = "NEWECL"; break;
                case 0x21: name = "LOAD FILES"; break;
                case 0x37: name = "LOAD PIECES"; break;
                case 0x22: name = "PARTY SURPRISE"; break;
                case 0x23: name = "SURPRISE"; break;
                case 0x24: name = "COMBAT"; break;
                case 0x25: name = "ON GOTO"; break;
                case 0x26: name = "ON GOSUB"; break;
                case 0x27: name = "TREASURE"; break;
                case 0x28: name = "ROB"; break;
                case 0x29: name = "ENCOUNTER MENU"; break;
                case 0x2A: name = "GETTABLE"; break;
                case 0x2B: name = "HORIZONTAL MENU"; break;
                case 0x2C: name = "PARLAY"; break;
                case 0x2D: name = "CALL"; break;
                case 0x2E: name = "DAMAGE"; break;
                case 0x2F: name = "AND"; break;
                case 0x30: name = "OR"; break;
                case 0x31: name = "SPRITE OFF"; break;
                case 0x32: name = "FIND ITEM"; break;
                case 0x33: name = "PRINT RETURN"; break;
                case 0x34: name = "ECL CLOCK"; break;
                case 0x35: name = "SAVE TABLE"; break;
                case 0x36: name = "ADD NPC"; break;
                case 0x38: name = "PROGRAM"; break;
                case 0x39: name = "WHO"; break;
                case 0x3A: name = "DELAY"; break;
                case 0x3B: name = "SPELL"; break;
                case 0x3C: name = "PROTECTION"; break;
                case 0x3D: name = "CLEAR BOX"; break;
                case 0x3E: name = "DUMP"; break;
                case 0x3F: name = "FIND SPECIAL"; break;
                case 0x40: name = "DESTROY ITEMS"; break;
                default:
                    name = "Unknown Command";
                    break;
            }

            System.Console.Out.WriteLine("{1} 0x{0:X}", gbl.command, name);

        }
    }
}
