using Classes;
using Logging;
using System.Collections.Generic;

namespace engine
{
    class ovr003
    {
        internal static void CMD_Exit()
        {
            VmLog.WriteLine("CMD_Exit: byte_1AB0A {0}", gbl.restore_player_ptr);
            VmLog.WriteLine("");

            if (gbl.restore_player_ptr == true)
            {
                gbl.player_ptr = gbl.player_ptr2;
                gbl.restore_player_ptr = false;
            }

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;

            gbl.byte_1EE8C = false;
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
        }


        internal static void CMD_Goto()
        {
            ovr008.vm_LoadCmdSets(1);
            ushort newOffset = gbl.cmd_opps[1].Word;

            VmLog.WriteLine("CMD_Goto: was: 0x{0:X} now: 0x{1:X}", gbl.ecl_offset, newOffset);

            gbl.ecl_offset = newOffset;
        }


        internal static void CMD_Gosub()
        {
            ovr008.vm_LoadCmdSets(1);
            ushort newOffset = gbl.cmd_opps[1].Word;

            VmLog.WriteLine("CMD_Gosub: was: 0x{0:X} now: 0x{1:X}", gbl.ecl_offset, newOffset);

            gbl.vmCallStack.Push(gbl.ecl_offset);
            gbl.ecl_offset = newOffset;
        }


        internal static void CMD_Compare() // sub_2611D
        {
            ovr008.vm_LoadCmdSets(2);

            if (gbl.cmd_opps[1].Code >= 0x80 ||
                gbl.cmd_opps[2].Code >= 0x80)
            {
                VmLog.WriteLine("CMD_Compare: Strings '{0}' '{1}'", gbl.unk_1D972[2], gbl.unk_1D972[1]);

                ovr008.compare_strings(gbl.unk_1D972[2], gbl.unk_1D972[1]);
            }
            else
            {
                ushort value_a = ovr008.vm_GetCmdValue(1);
                ushort value_b = ovr008.vm_GetCmdValue(2);

                VmLog.WriteLine("CMD_Compare: Values: {0} {1}", value_b, value_a);
                ovr008.compare_variables(value_b, value_a);
            }
        }


        internal static void CMD_AddSubDivMulti() // sub_2619A
        {
            ushort value;

            ovr008.vm_LoadCmdSets(3);

            ushort val_a = ovr008.vm_GetCmdValue(1);
            ushort val_b = ovr008.vm_GetCmdValue(2);

            ushort location = gbl.cmd_opps[3].Word;

            switch (gbl.command)
            {
                case 4:
                    value = (ushort)(val_a + val_b);
                    break;

                case 5:
                    value = (ushort)(val_b - val_a);
                    break;

                case 6:
                    value = (ushort)(val_a / val_b);
                    gbl.area2_ptr.field_67E = (short)(val_a % val_b);
                    break;

                case 7:
                    value = (ushort)(val_a * val_b);
                    break;

                default:
                    value = 0;
                    throw (new System.Exception("can't get here."));
            }
            string[] sym = { "", "", "", "", "A + B", "B - A", "A / B", "A * B" };
            VmLog.WriteLine("CMD_AdSubDivMulti: {0} A: {1} B: {2} Loc: {3} Res: {4}",
                sym[gbl.command], val_a, val_b, new MemLoc(location), value);

            ovr008.vm_SetMemoryValue(value, location);
        }


        internal static void CMD_Random() // sub_2623D
        {
            ovr008.vm_LoadCmdSets(2);

            byte rand_max = (byte)ovr008.vm_GetCmdValue(1);

            if (rand_max < 0xff)
            {
                rand_max++;
            }

            ushort loc = gbl.cmd_opps[2].Word;

            byte val = seg051.Random(rand_max);

            VmLog.WriteLine("CMD_Random: Max: {0} Loc: {1} Val: {2}", rand_max, new MemLoc(loc), val);

            ovr008.vm_SetMemoryValue(val, loc);
        }


        internal static void CMD_Save() 
        {
            ovr008.vm_LoadCmdSets(2);

            ushort loc = gbl.cmd_opps[2].Word;

            if (gbl.cmd_opps[1].Code < 0x80)
            {
                ushort val = ovr008.vm_GetCmdValue(1);

                VmLog.WriteLine("CMD_Save: Value {0} Loc: {1}", val, new MemLoc(loc));
                ovr008.vm_SetMemoryValue(val, loc);
            }
            else
            {
                VmLog.WriteLine("CMD_Save: String '{0}' Loc: {1}", gbl.unk_1D972[1], new MemLoc(loc));
                ovr008.vm_WriteStringToMemory(gbl.unk_1D972[1], loc);
            }
        }


        internal static void CMD_LoadCharacter() /* sub_262E9 */
        {
            ovr008.vm_LoadCmdSets(1);

            byte player_index = (byte)ovr008.vm_GetCmdValue(1);
            VmLog.WriteLine("CMD_LoadCharacter: 0x{0:X}", player_index);

            gbl.restore_player_ptr = true;


            byte var_8 = (byte)(player_index & 0x80);
            player_index = (byte)(player_index & 0x7f);

            Player player_ptr = player_index > 0 ? player_ptr = gbl.player_next_ptr[player_index] : null;
 
            if (player_ptr != null)
            {
                gbl.player_ptr = player_ptr;
                gbl.player_not_found = false;
            }
            else
            {
                gbl.player_not_found = true;
            }

            if (var_8 != 0 &&
                gbl.redrawPartySummary1 == true &&
                gbl.redrawPartySummary2 == true)
            {
                if (gbl.player_ptr2 == player_ptr)
                {
                    gbl.restore_player_ptr = false;
                }
                gbl.player_ptr = ovr018.FreeCurrentPlayer(gbl.player_ptr, true, false);

                ovr025.PartySummary(gbl.player_ptr);
                gbl.redrawPartySummary1 = false;
                gbl.redrawPartySummary2 = false;
            }
        }


        internal static void CMD_SetupMonster() /* sub_263C9 */
        {
            ovr008.vm_LoadCmdSets(3);

            byte sprite_id = (byte)ovr008.vm_GetCmdValue(1);
            byte max_distance = (byte)ovr008.vm_GetCmdValue(2);
            byte pic_id = (byte)ovr008.vm_GetCmdValue(3);

            VmLog.WriteLine("CMD_SetupMonster: sprite id: {0} area2_ptr.field_580: {1} pic id: {2}", sprite_id, max_distance, pic_id);

            gbl.sprite_block_id = sprite_id;
            gbl.area2_ptr.max_encounter_distance = max_distance;
            gbl.pic_block_id = pic_id;

            gbl.area2_ptr.encounter_distance = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (gbl.area2_ptr.max_encounter_distance < gbl.area2_ptr.encounter_distance)
            {
                gbl.area2_ptr.encounter_distance = gbl.area2_ptr.max_encounter_distance;
            }
            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
        }

        internal static void CMD_LoadMonster() /* sub_26465 */
        {
            Player current_player_bkup = gbl.player_ptr;
            ovr008.vm_LoadCmdSets(3);

            if (gbl.numLoadedMonsters < 63)
            {
                int mod_id = ovr008.vm_GetCmdValue(1) & 0xFF;

                Player mobMasterCopy = ovr017.load_mob(mod_id);

                Player newMob = mobMasterCopy.ShallowClone();

                int num_copies = ovr008.vm_GetCmdValue(2) & 0xFF;

                if (num_copies <= 0)
                {
                    num_copies = 1;
                }

                int blockId = ovr008.vm_GetCmdValue(3) & 0xFF;
                ovr034.chead_cbody_comspr_icon(gbl.monster_icon_id, blockId, "CPIC");

                newMob.icon_id = gbl.monster_icon_id;
             
                gbl.player_next_ptr.Add(newMob);

                gbl.numLoadedMonsters++;
                int copy_count = 1;

                while (copy_count < num_copies &&
                       gbl.numLoadedMonsters < 63)
                {
                    newMob = mobMasterCopy.ShallowClone();

                    newMob.icon_id = gbl.monster_icon_id;

                    newMob.affects = new List<Affect>();
                    newMob.items = new List<Item>();
                    
                    foreach (Item item in mobMasterCopy.items)
                    {
                        newMob.items.Add(item.ShallowClone());
                    }

                    foreach(Affect affect in mobMasterCopy.affects)
                    {
                        newMob.affects.Add(affect.ShallowClone());
                    }

                    copy_count++;
                    gbl.numLoadedMonsters++;
                    gbl.player_next_ptr.Add(newMob);

                }

                gbl.monster_icon_id++;
                gbl.monstersLoaded = true;
                gbl.player_ptr = current_player_bkup;
            }
        }


        internal static void CMD_Approach() // sub_26835
        {
            if (gbl.area2_ptr.encounter_distance > 0)
            {
                gbl.area2_ptr.encounter_distance--;

                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
            }
            gbl.ecl_offset++;
        }


        internal static void CMD_Picture() /* sub_26873 */
        {
            ovr008.vm_LoadCmdSets(1);
            byte blockId = (byte)ovr008.vm_GetCmdValue(1);

            if (blockId != 0xff)
            {
                gbl.encounter_flags[1] = true;
                gbl.byte_1EE8C = true;

                if (gbl.area2_ptr.HeadBlockId == 0xff)
                {
                    gbl.byte_1EE8D = true;

                    if (blockId >= 0x78)
                    {
                        ovr030.load_bigpic(blockId);
                        ovr030.draw_bigpic();
                        gbl.can_draw_bigpic = false;
                    }
                    else
                    {
                        ovr030.load_pic_final(ref gbl.byte_1D556, 0, blockId, "PIC");
                        ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
                    }
                }
                else
                {
                    ovr008.set_and_draw_head_body(blockId, (byte)gbl.area2_ptr.HeadBlockId);
                }
            }
            else
            {
                if ((gbl.last_game_state != GameState.State4 || gbl.game_state == GameState.State4) &&
                    (gbl.byte_1EE8C == true || gbl.displayPlayerSprite))
                {
                    gbl.can_draw_bigpic = true;
                    ovr029.RedrawView();
                    gbl.byte_1EE8C = false;
                    gbl.displayPlayerSprite = false;
                    gbl.byte_1EE8D = true;
                }
                gbl.encounter_flags[0] = false;
                gbl.encounter_flags[1] = false;
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

            VmLog.WriteLine("CMD_Print: '{0}'",
                gbl.cmd_opps[1].Code < 0x80 ? ovr008.vm_GetCmdValue(1).ToString() : gbl.unk_1D972[1]);

            gbl.bottomTextHasBeenCleared = false;
            gbl.DelayBetweenCharacters = true;

            if (gbl.cmd_opps[1].Code < 0x80)
            {
                gbl.unk_1D972[1] = ovr008.vm_GetCmdValue(1).ToString();
            }

            if (gbl.command == 0x11)
            {
                seg041.press_any_key(gbl.unk_1D972[1], false, 0, 10, TextRegion.NormalBottom);
            }
            else
            {
                gbl.textYCol = 0x11;
                gbl.textXCol = 1;

                seg041.press_any_key(gbl.unk_1D972[1], true, 0, 10, TextRegion.NormalBottom);
            }

            gbl.DelayBetweenCharacters = false;
        }


        internal static void CMD_Return()
        {
            gbl.ecl_offset++;
            if (gbl.vmCallStack.Count > 0)
            {
                ushort newOffset = gbl.vmCallStack.Peek();
                VmLog.WriteLine("CMD_Return: was: {0:X} now: {1:X}", gbl.ecl_offset, newOffset);
                gbl.vmCallStack.Pop();
                gbl.ecl_offset = newOffset;
            }
            else
            {
                VmLog.Write("CMD_Return: call stack empty ");
                CMD_Exit();
            }
        }


        internal static void CMD_CompareAnd() /* sub_26B0C */
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            ovr008.vm_LoadCmdSets(4);

            ushort var_8 = ovr008.vm_GetCmdValue(1);
            ushort var_6 = ovr008.vm_GetCmdValue(2);
            ushort var_4 = ovr008.vm_GetCmdValue(3);
            ushort var_2 = ovr008.vm_GetCmdValue(4);

            if (var_8 == var_6 &&
                var_4 == var_2)
            {
                gbl.compare_flags[0] = true;
            }
            else
            {
                gbl.compare_flags[1] = true;
            }
        }


        internal static void CMD_If()
        {
            gbl.ecl_offset++;

            int index = gbl.command - 0x16;
            string[] types = { "==", "!=", "<", ">", "<=", ">=" };

            VmLog.WriteLine("CMD_if: {0} {1}", types[index], gbl.compare_flags[index]);

            if (gbl.compare_flags[index] == false)
            {
                ovr008.vm_skipNextCommand();
            }
        }


        internal static void CMD_NewECL()
        {
            ovr008.vm_LoadCmdSets(1);

            byte block_id = (byte)ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_NewECL: block_id {0}", block_id);

            gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
            gbl.byte_1EE88 = block_id;

            ovr008.load_ecl_dax(block_id);
            ovr008.vm_init_ecl();
            gbl.stopVM = true;
            gbl.vmFlag01 = true;

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
        }


        internal static void CMD_LoadFiles() /* sub_26C41 */
        {
            ovr008.vm_LoadCmdSets(3);

            gbl.byte_1AB0B = true;

            byte var_3 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(3);

            VmLog.WriteLine("CMD_LoadFile: {0} A: {1} B: {2} C: {3}",
                gbl.command == 0x21 ? "Files" : "Pieces", var_1, var_2, var_3);


            if (gbl.command == 0x21)
            {
                gbl.filesLoaded = true;

                if (var_3 != 0xff &&
                    var_3 != 0x7f &&
                    gbl.area_ptr.inDungeon != 0)
                {
                    gbl.area_ptr.current_3DMap_block_id = var_3;
                    ovr031.Load3DMap(var_3);
                    gbl.area2_ptr.field_592 = 0;
                }

                if (var_1 != 0xff &&
                    gbl.area_ptr.inDungeon == 0 &&
                    gbl.lastDaxBlockId != 0x50)
                {
                    ovr030.load_bigpic(0x79);
                }
            }
            else
            {
                gbl.byte_1AB0C = true;

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
                            gbl.setBlocks[0].blockId = -1;
                            gbl.setBlocks[0].setId = -1;
                        }

                        if (var_2 != 0xff)
                        {
                            ovr031.LoadWalldef(2, var_2);
                        }
                        else
                        {
                            gbl.setBlocks[1].blockId = -1;
                            gbl.setBlocks[1].setId = -1;
                        }

                        if (var_1 != 0xff)
                        {
                            ovr031.LoadWalldef(3, var_1);
                        }
                        else
                        {
                            gbl.setBlocks[2].blockId = -1;
                            gbl.setBlocks[2].setId = -1;
                        }
                    }
                }
            }


            if (gbl.byte_1AB0C == true &&
                gbl.filesLoaded == true &&
                gbl.last_game_state == GameState.WildernessMap)
            {
                if (gbl.game_state != GameState.WildernessMap &&
                    gbl.byte_1EE98 == true)
                {
                    seg037.draw8x8_03();
                    ovr025.PartySummary(gbl.player_ptr);
                    ovr025.display_map_position_time();
                }
                gbl.byte_1EE98 = false;
            }
        }


        internal static void CMD_AndOr() /* sub_26DD0 */
        {
            byte resultant;

            ovr008.vm_LoadCmdSets(3);
            ushort val_a = ovr008.vm_GetCmdValue(1);
            ushort val_b = ovr008.vm_GetCmdValue(2);

            ushort loc = gbl.cmd_opps[3].Word;
            string sym;
            if (gbl.command == 0x2F)
            {
                sym = "And";
                resultant = (byte)(val_a & val_b);
            }
            else
            {
                sym = "Or";
                resultant = (byte)(val_a | val_b);
            }

            VmLog.WriteLine("CMD_AndOr: {0} A: {1} B: {2} Loc: {3} Val: {4}", sym, val_a, val_b, new MemLoc(loc), resultant);

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
            gbl.bottomTextHasBeenCleared = false;

            ovr008.vm_LoadCmdSets(3);
            ushort mem_loc = gbl.cmd_opps[1].Word;

            string delay_text = gbl.unk_1D972[1];

            byte menuCount = (byte)ovr008.vm_GetCmdValue(3);
            gbl.ecl_offset--;
            ovr008.vm_LoadCmdSets(menuCount);

            List<MenuItem> menuList = new List<MenuItem>();

            gbl.textXCol = 1;
            gbl.textYCol = 0x11;

            seg041.press_any_key(delay_text, true, 0, 10, 22, 38, 17, 1);

            for (int i = 0; i < menuCount; i++)
            {
                menuList.Add(new MenuItem(gbl.unk_1D972[i + 1]));
            }

            int index = ovr008.VertMenuSelect(0, true, false, menuList, 0x16, 0x26, gbl.textYCol + 1, 1, 15, 10, 13);

            ovr008.vm_SetMemoryValue((ushort)index, mem_loc);

            menuList.Clear();
            seg037.draw8x8_clear_area(TextRegion.NormalBottom);
        }


        internal static void CMD_HorizontalMenu()
        {
            byte var_3E;
            byte var_3D;
            bool useOverlay;
            byte var_3B;

            ovr008.vm_LoadCmdSets(2);

            ushort loc = gbl.cmd_opps[1].Word;
            byte string_count = (byte)ovr008.vm_GetCmdValue(2);

            gbl.ecl_offset--;

            ovr008.vm_LoadCmdSets(string_count);

            if (string_count == 1)
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

            if (gbl.byte_1EE8C == false ||
                gbl.byte_1EE8D == false)
            {
                useOverlay = false;
            }
            else
            {
                useOverlay = true;
            }

            string text = string.Empty;
            for (int i = 1; i < string_count; i++)
            {
                text += "~" + gbl.unk_1D972[i] + " ";
            }

            text += "~" + gbl.unk_1D972[string_count];

            byte menu_selected = (byte)ovr008.sub_317AA(useOverlay, var_3B, var_3D, var_3E, 0x0d, text, string.Empty);

            ovr008.vm_SetMemoryValue(menu_selected, loc);

            ovr027.ClearPromptAreaNoUpdate();
        }

        /// <summary>
        /// Clears the pooled items and pool money.
        /// </summary>
        internal static void CMD_ClearMonsters() /* sub_27240 */
        {
            gbl.ecl_offset++;
            gbl.numLoadedMonsters = 0;
            gbl.monstersLoaded = false;
            gbl.monster_icon_id = 8;

            VmLog.WriteLine("CMD_ClearMonsters:");

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            gbl.items_pointer.Clear();
        }


        internal static void CMD_PartyStrength() /* sub_272A9 */
        {
            ovr008.vm_LoadCmdSets(1);
            byte power_value = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                int hit_points = player.hit_point_current;
                int armor_class = player.ac;
                int hit_bonus = player.hitBonus;

                int magic_power = player.magic_user_lvl + (ovr026.sub_6B3D1(player) * player.magic_user_old_lvl);
                int cleric_power = player.cleric_lvl + (ovr026.sub_6B3D1(player) * player.cleric_old_lvl);

                if (armor_class > 60)
                {
                    armor_class -= 60;
                }
                else
                {
                    armor_class = 0;
                }

                if (hit_bonus > 39)
                {
                    hit_bonus -= 39;
                }
                else
                {
                    hit_bonus = 0;
                }

                power_value += (byte)(((cleric_power * 4) + hit_points + (armor_class * 5) + (hit_bonus * 5) + (magic_power * 8)) / 10);
            }

            ushort loc = gbl.cmd_opps[1].Word;
            ovr008.vm_SetMemoryValue(power_value, loc);
        }


        internal static void setMemoryFour(bool val_d, byte val_c, byte val_b, byte val_a,
            ushort loc_a, ushort loc_b, ushort loc_c, ushort loc_d) /* sub_273F6 */
        {
            ovr008.vm_SetMemoryValue(val_a, loc_a);
            ovr008.vm_SetMemoryValue(val_b, loc_b);
            ovr008.vm_SetMemoryValue(val_c, loc_c);
            ovr008.vm_SetMemoryValue(val_d ? (ushort)1 : (ushort)0, loc_d);
        }


        internal static void CMD_CheckParty() /* sub_27454 */
        {
            int var_4;
            ushort var_2;

            ovr008.vm_LoadCmdSets(6);

            if (gbl.cmd_opps[1].Code == 1)
            {
                var_2 = gbl.cmd_opps[1].Word;
            }
            else
            {
                var_2 = ovr008.vm_GetCmdValue(1);
            }

            Affects affect_id = (Affects)ovr008.vm_GetCmdValue(2);

            var loc_a = gbl.cmd_opps[3].Word;
            var loc_b = gbl.cmd_opps[4].Word;
            var loc_c = gbl.cmd_opps[5].Word;
            var loc_d = gbl.cmd_opps[6].Word;

            var_4 = 0;
            byte val_a = 0x0FF;
            byte val_b = 0;
            byte val_c;

            var_2 -= 0x7fff;

            if (var_2 == 8001)
            {
                bool affect_found = gbl.player_next_ptr.Exists(player => player.HasAffect(affect_id));

                setMemoryFour(affect_found, 0, 0, 0, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 >= 0x00A5 && var_2 <= 0x00AC)
            {
                int index = var_2 - 0xA4;
                int count = 0;
                foreach (Player player in gbl.player_next_ptr)
                {
                    count++;

                    if (player.field_EA[index - 1] < val_a)
                    {
                        val_a = player.field_EA[index - 1];
                    }

                    if (player.field_EA[index - 1] > val_b)
                    {
                        val_b = player.field_EA[index - 1];
                    }

                    var_4 += player.field_EA[index - 1];
                }

                val_c = (byte)(var_4 / count);

                setMemoryFour(false, val_c, val_b, val_a, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 == 0x9f)
            {
                int count = 0;
                foreach (Player player in gbl.player_next_ptr)
                {
                    count++;

                    if (player.movement < val_a)
                    {
                        val_a = player.movement;
                    }

                    if (player.movement > val_b)
                    {
                        val_b = player.movement;
                    }

                    var_4 += player.movement;
                }

                val_c = (byte)(var_4 / count);

                setMemoryFour(false, val_c, val_b, val_a, loc_a, loc_b, loc_c, loc_d);
            }
        }


        internal static void CMD_PartySurprise() /* sub_2767E */
        {
            ovr008.vm_LoadCmdSets(2);

            byte val_a = 0;
            byte val_b = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player._class == ClassId.ranger ||
                    player._class == ClassId.mc_c_r)
                {
                    val_a = 1;
                }
            }

            ushort loc_a = gbl.cmd_opps[1].Word;
            ushort loc_b = gbl.cmd_opps[2].Word;

            ovr008.vm_SetMemoryValue(val_a, loc_a);
            ovr008.vm_SetMemoryValue(val_b, loc_b);
        }


        internal static void CMD_Surprise() /* sub_2771E */
        {
            ovr008.vm_LoadCmdSets(4);
            byte val_a = 0;

            byte var_8 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_7 = (byte)ovr008.vm_GetCmdValue(2);
            byte var_6 = (byte)ovr008.vm_GetCmdValue(3);
            byte var_5 = (byte)ovr008.vm_GetCmdValue(4);

            byte var_9 = (byte)((var_5 + 2) - var_8);
            byte var_A = (byte)((var_7 + 2) - var_6);

            byte var_1 = ovr024.roll_dice(6, 1);
            byte var_2 = ovr024.roll_dice(6, 1);

            if (var_1 <= var_9)
            {
                if (var_2 <= var_A)
                {
                    val_a = 3;
                }
                else
                {
                    val_a = 1;
                }
            }

            if (var_2 <= var_A)
            {
                val_a = 2;
            }

            ovr008.vm_SetMemoryValue(val_a, 0x2cb);
        }


        internal static void CMD_Combat() // sub_277E4
        {
            gbl.ecl_offset++;

            if (gbl.monstersLoaded == false &&
                gbl.combat_type == CombatType.normal)
            {
                if (gbl.area2_ptr.field_6D8 == 1)
                {
                    gbl.area2_ptr.field_6D8 = 0;

                    ovr007.sub_2F6E7();
                }
                else if (gbl.area2_ptr.field_5C4 == 1)
                {
                    gbl.area2_ptr.field_5C4 = 0;

                    ovr005.temple_shop();
                }
                else
                {
                    ovr006.sub_2E7A2();
                }
            }
            else
            {

                ushort var_2 = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

                if (var_2 < gbl.area2_ptr.encounter_distance)
                {
                    gbl.area2_ptr.encounter_distance = var_2;
                }

                ovr009.MainCombatLoop();

                ovr006.sub_2E7A2();

                if (gbl.area_ptr.inDungeon == 0)
                {
                    ovr030.load_bigpic(0x79);
                }
            }

            if (gbl.area_ptr.inDungeon != 0)
            {
                gbl.game_state = GameState.State4;
            }
            else
            {
                gbl.game_state = GameState.WildernessMap;
            }

            gbl.area2_ptr.search_flags &= 1;
            
            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
            gbl.byte_1EE8C = false;
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
                ushort newloc = gbl.cmd_opps[var_1 + 1].Word;
                VmLog.WriteLine("CMD_OnGotoGoSub: {4} A: {0} B: {1} Was: 0x{2:X} Now: 0x{3:X}",
                    var_1, var_2, gbl.ecl_offset, newloc,
                    gbl.command == 0x25 ? "Goto" : "Gosub");

                if (gbl.command == 0x25)
                {
                    // Goto
                    gbl.ecl_offset = newloc;
                }
                else
                {
                    // Gosub
                    gbl.vmCallStack.Push(gbl.ecl_offset);
                    gbl.ecl_offset = newloc;
                }
            }
            else
            {
                VmLog.WriteLine("CMD_OnGotoGoSub: {0} A: {1} B: {2}",
                    gbl.command == 0x25 ? "Goto" : "Gosub", var_1, var_2);
            }
        }



        internal static void CMD_Treasure() /* load_item */
        {
            byte[] data;
            short dataSize;
            int item_type = 0;

            ovr008.vm_LoadCmdSets(8);

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = ovr008.vm_GetCmdValue(i + 1);
            }

            byte var_2 = (byte)ovr008.vm_GetCmdValue(8);

            if (var_2 < 0x80)
            {
                string filename = string.Format("ITEM{0}.dax", gbl.game_area);
                seg042.load_decode_dax(out data, out dataSize, var_2, filename);

                if (dataSize == 0)
                {
                    Logger.LogAndExit("Unable to find item file: {0}", filename);
                }

                for (int offset = 0; offset < dataSize; offset += Item.StructSize)
                {
                    gbl.items_pointer.Add(new Item(data, offset));
                }

                data = null;
            }
            else if (var_2 != 0xff)
            {
                for (int count = 0; count < (var_2 - 0x80); count++)
                {
                    int var_63 = ovr024.roll_dice(100, 1);

                    if (var_63 >= 1 && var_63 <= 60)
                    {
                        int var_64 = ovr024.roll_dice(100, 1);

                        if ((var_64 >= 1 && var_64 <= 47) ||
                            (var_64 >= 50 && var_64 <= 59))
                        {
                            if (var_64 == 45)
                            {
                                item_type = 59;
                            }
                            else
                            {
                                item_type = var_64;
                            }
                        }
                        else if (var_64 >= 60 && var_64 <= 90)
                        {
                            var_64 = ovr024.roll_dice(10, 1);

                            if (var_64 >= 1 && var_64 <= 4)
                            {
                                item_type = 0x24;
                            }
                            else if (var_64 >= 5 && var_64 <= 7)
                            {
                                item_type = 0x23;
                            }
                            else if (var_64 == 8)
                            {
                                item_type = 0x22;
                            }
                            else if (var_64 == 9)
                            {
                                item_type = 0x25;
                            }
                            else if (var_64 == 10)
                            {
                                item_type = 0x26;
                            }
                        }
                        else if (var_64 >= 0x5B && var_64 <= 0x5E)
                        {
                            item_type = 0x49;
                        }
                        else if (var_64 >= 0x5F && var_64 <= 0x61)
                        {
                            item_type = 0x5D;
                        }
                        else if (var_64 >= 0x62 && var_64 <= 0x64)
                        {
                            item_type = 0x4D;
                        }
                        else
                        {
                            item_type = 0x3B;
                        }
                    }
                    else if (var_63 >= 0x3d && var_63 <= 0x55)
                    {
                        item_type = 0x3D;
                    }
                    else if (var_63 >= 0x56 && var_63 <= 0x5C)
                    {
                        item_type = 0x3E;
                    }
                    else if (var_63 >= 0x5B && var_63 <= 0x62)
                    {
                        int var_62 = ovr024.roll_dice(15, 1);

                        if (var_62 >= 1 && var_62 <= 9)
                        {
                            item_type = 0x47;
                        }
                        else if (var_62 == 10)
                        {
                            item_type = 0x54;
                        }
                        else if (var_62 >= 11 && var_62 <= 15)
                        {
                            item_type = 0x4F;
                        }
                    }
                    else if (var_63 == 99 || var_63 == 100)
                    {
                        item_type = 0x3B;
                    }

                    gbl.items_pointer.Add(ovr022.create_item(item_type));
                }

                gbl.items_pointer.ForEach(item => ovr025.ItemDisplayNameBuild(false, false, 0, 0, item));
            }
        }


        internal static void CMD_Rob() /* sub_27F76*/
        {
            ovr008.vm_LoadCmdSets(3);
            byte allParty = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);

            double percentage = (100 - var_2) / 100.0;
            byte var_3 = (byte)ovr008.vm_GetCmdValue(3);

            if (allParty == 0)
            {
                ovr008.RobMoney(gbl.player_ptr, percentage);
                ovr008.RobItems(gbl.player_ptr, var_3);
            }
            else
            {
                foreach (Player player in gbl.player_next_ptr)
                {
                    ovr008.RobMoney(player, percentage);
                    ovr008.RobItems(player, var_3);
                }
            }
        }


        internal static void CMD_EncounterMenu()
        {
            ushort var_43D;
            int var_43B;
            byte var_43A;
            string displayText;
            bool useOverlay;
            bool clearTextArea;
            byte init_max;
            byte init_min;
            byte var_40A;
            byte var_408;
            byte var_407;
            string text = string.Empty; /* Simeon */
            string[] strings = new string[3];
            byte[] var_6 = new byte[5];
            int menu_selected;

            gbl.byte_1EE95 = true;
            gbl.bottomTextHasBeenCleared = false;
            gbl.DelayBetweenCharacters = true;

            ovr008.calc_group_movement(out init_min, out var_40A);

            ovr008.vm_LoadCmdSets(0x0e);

            gbl.sprite_block_id = (byte)ovr008.vm_GetCmdValue(1);
            gbl.area2_ptr.max_encounter_distance = ovr008.vm_GetCmdValue(2);
            gbl.pic_block_id = (byte)ovr008.vm_GetCmdValue(3);

            var_43D = gbl.cmd_opps[4].Word;

            for (int i = 0; i < 5; i++)
            {
                var_6[i] = (byte)ovr008.vm_GetCmdValue(i + 5);
            }

            for (int i = 0; i < 3; i++)
            {
                strings[i] = gbl.unk_1D972[i + 1];
            }

            var_407 = (byte)ovr008.vm_GetCmdValue(0x0d);
            var_408 = (byte)ovr008.vm_GetCmdValue(0x0e);

            gbl.area2_ptr.encounter_distance = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (gbl.area2_ptr.max_encounter_distance < gbl.area2_ptr.encounter_distance)
            {
                gbl.area2_ptr.encounter_distance = gbl.area2_ptr.max_encounter_distance;
            }

            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);

            do
            {
                if (gbl.byte_1EE8C == false ||
                    gbl.byte_1EE8D == false ||
                    gbl.area_ptr.inDungeon == 0 ||
                    gbl.lastDaxBlockId == 0x50)
                {
                    useOverlay = false;
                }
                else
                {
                    useOverlay = true;
                }

                clearTextArea = (gbl.area_ptr.inDungeon != 0);

                init_max = 0;
                gbl.textXCol = 1;
                gbl.textYCol = 0x11;

                switch (gbl.area2_ptr.encounter_distance)
                {
                    case 0:
                        var_43B = 0;

                        do
                        {
                            text = strings[var_43B];
                            var_43B++;
                        } while (text.Length == 0 && var_43B < 3);
                        break;

                    case 1:
                        var_43B = 1;

                        do
                        {
                            text = strings[var_43B];
                            var_43B++;

                            if (var_43B > 2)
                            {
                                var_43B = 0;
                            }
                        } while (text.Length == 0 && var_43B != 1);
                        break;

                    case 2:
                        var_43B = 2;

                        do
                        {
                            text = strings[var_43B];

                            var_43B++;
                            if (var_43B > 2)
                            {
                                var_43B = 0;
                            }

                        } while (text.Length == 0 && var_43B != 2);
                        break;
                }

                if (text.Length == 0)
                {
                    clearTextArea = false;
                }

                seg041.press_any_key(text, clearTextArea, 0, 10, TextRegion.NormalBottom);

                if (gbl.area2_ptr.encounter_distance == 0 ||
                    gbl.area_ptr.inDungeon == 0)
                {
                    displayText = "~COMBAT ~WAIT ~FLEE ~PARLAY";
                }
                else
                {
                    displayText = "~COMBAT ~WAIT ~FLEE ~ADVANCE";
                }

                menu_selected = ovr008.sub_317AA(useOverlay, 0, 15, 10, 13, displayText, string.Empty);

                if (gbl.area2_ptr.encounter_distance == 0 ||
                    gbl.area_ptr.inDungeon == 0)
                {
                    if (menu_selected == 3)
                    {
                        menu_selected = 4;
                    }
                }

                var_43A = var_6[menu_selected];

                switch (var_43A)
                {
                    case 0:
                        if (menu_selected != 2)
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
                        if (menu_selected == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (menu_selected == 1)
                        {
                            init_max = 1;
                            seg041.press_any_key("Both sides wait.", true, 0, 10, TextRegion.NormalBottom);
                        }
                        else if (menu_selected == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }
                        else if (menu_selected == 3)
                        {
                            if (gbl.area2_ptr.encounter_distance != 0)
                            {
                                gbl.area2_ptr.encounter_distance--;

                                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                            }
                            else
                            {
                                seg041.press_any_key("Both sides wait.", true, 0, 10, TextRegion.NormalBottom);
                            }

                            init_max = 1;
                        }
                        else if (menu_selected == 4)
                        {
                            if (gbl.area2_ptr.encounter_distance > 0)
                            {
                                gbl.area2_ptr.encounter_distance--;
                                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                                init_max = 1;
                            }
                            else
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                        }
                        break;

                    case 2:
                        if (menu_selected == 0)
                        {
                            if (var_408 > var_40A)
                            {
                                ovr008.vm_SetMemoryValue(0, var_43D);

                                gbl.textXCol = 1;
                                gbl.textYCol = 0x11;
                                seg041.press_any_key("The monsters flee.", true, 0, 10, TextRegion.NormalBottom);
                            }
                            else
                            {
                                ovr008.vm_SetMemoryValue(1, var_43D);
                            }
                        }
                        else if (menu_selected >= 1 && menu_selected <= 4)
                        {
                            ovr008.vm_SetMemoryValue(0, var_43D);

                            gbl.textXCol = 1;
                            gbl.textYCol = 0x11;
                            seg041.press_any_key("The monsters flee.", true, 0, 10, TextRegion.NormalBottom);
                        }
                        break;

                    case 3:
                        if (menu_selected == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (menu_selected == 1 || menu_selected == 3)
                        {
                            if (gbl.area2_ptr.encounter_distance != 0)
                            {
                                gbl.area2_ptr.encounter_distance--;

                                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                            }
                            else
                            {
                                seg041.press_any_key("Both sides wait.", true, 0, 10, TextRegion.NormalBottom);
                            }

                            init_max = 1;
                        }
                        else if (menu_selected == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }
                        else if (menu_selected == 4)
                        {
                            if (gbl.area2_ptr.encounter_distance <= 0)
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                            else
                            {
                                gbl.area2_ptr.encounter_distance--;

                                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                                init_max = 1;
                            }
                        }
                        break;

                    case 4:
                        if (menu_selected == 0)
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                        else if (menu_selected == 1 || menu_selected == 3 || menu_selected == 4)
                        {

                            if (gbl.area2_ptr.encounter_distance <= 0)
                            {
                                ovr008.vm_SetMemoryValue(3, var_43D);
                            }
                            else
                            {
                                gbl.area2_ptr.encounter_distance -= 1;

                                ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                                init_max = 1;
                            }
                        }
                        else if (menu_selected == 2)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }

                        break;
                }
            } while (init_max != 0);

            ovr027.ClearPromptArea();
            gbl.DelayBetweenCharacters = false;
            gbl.byte_1EE95 = false;
        }


        internal static void CMD_Parlay() /* talk_style */
        {
            ovr008.vm_LoadCmdSets(6);

            byte[] values = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                values[i] = (byte)ovr008.vm_GetCmdValue(i + 1);
            }

            int menu_selected = ovr008.sub_317AA(false, 0, 15, 10, 13, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", " ");

            ushort location = gbl.cmd_opps[6].Word;

            byte value = values[menu_selected];

            ovr008.vm_SetMemoryValue(value, location);
        }


        internal static void CMD_FindItem() // sub_28856
        {
            ovr008.vm_LoadCmdSets(1);

            byte item_type = (byte)ovr008.vm_GetCmdValue(1);

            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            gbl.compare_flags[1] = true;

            foreach (Player player in gbl.player_next_ptr)
            {
                foreach (Item item in player.items)
                {
                    if (item_type == item.type)
                    {
                        gbl.compare_flags[0] = true;
                        gbl.compare_flags[1] = false;
                        return;
                    }
                }
            }
        }


        internal static void CMD_Delay()
        {
            gbl.ecl_offset++;
            seg041.GameDelay();
        }


        internal static void CMD_Damage() /* sub_28958 */
        {
            byte var_8 = 0; /* Simeon */

            Player currentPlayerBackup = gbl.player_ptr;
            /*byte var_19 = 0; */
            byte var_1A = 0;
            ovr008.vm_LoadCmdSets(5);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);
            byte var_3 = (byte)ovr008.vm_GetCmdValue(3);
            byte var_7 = (byte)ovr008.vm_GetCmdValue(4);
            byte var_6 = (byte)ovr008.vm_GetCmdValue(5);


            int damage = ovr024.roll_dice(var_3, var_2) + var_7;

            byte var_1B = (byte)(var_1 & 0x10);

            if ((var_1 & 0x40) != 0)
            {
                var_1A = 1;
            }
            else
            {
                var_8 = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
            }

            if ((var_1 & 0x80) != 0)
            {
                int saveBonus = var_1 & 0x1f;
                int bonusType = var_6 & 7;

                if (var_1A != 0)
                {
                    foreach (Player player03 in gbl.player_next_ptr)
                    {
                        if ((var_1 & 0x20) != 0)
                        {
                            ovr008.sub_32200(player03, damage);
                        }
                        else if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, player03) == false)
                        {
                            ovr008.sub_32200(player03, damage);
                        }
                        else if (var_1B != 0)
                        {
                            ovr008.sub_32200(player03, damage);
                        }
                    }
                }
                else
                {
                    if ((var_6 & 0x80) != 0)
                    {
                        if (bonusType == 0 ||
                            ovr024.RollSavingThrow(saveBonus, (SaveVerseType)(bonusType - 1), gbl.player_ptr) == false)
                        {
                            ovr008.sub_32200(gbl.player_ptr, damage);
                        }
                        else if (var_1B != 0)
                        {
                            ovr008.sub_32200(gbl.player_ptr, damage);
                        }
                    }
                    else
                    {
                        Player target = gbl.player_next_ptr[var_8 - 1];

                        if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, target) == false)
                        {
                            ovr008.sub_32200(target, damage);
                        }
                        else if (var_1B != 0)
                        {
                            ovr008.sub_32200(target, damage);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < var_1; i++)
                {
                    var_8 = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
                    Player player03 = gbl.player_next_ptr[var_8 - 1];

                    if (ovr024.CanHitTarget(var_6, player03) == true)
                    {
                        ovr008.sub_32200(player03, damage);
                    }

                    damage = ovr024.roll_dice(var_3, var_2) + var_7;
                }
            }

            gbl.party_killed = true;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.in_combat == true)
                {
                    gbl.party_killed = false;
                }
            }

            if (gbl.party_killed == true)
            {
                seg037.DrawFrame_Outer();
                gbl.textXCol = 2;
                gbl.textYCol = 2;

                seg041.press_any_key("The entire party is killed!", true, 0, 10, 0x16, 0x26, 1, 1);
                seg049.SysDelay(3000);
            }

            gbl.player_ptr = currentPlayerBackup;
            seg041.displayAndDebug("press <enter>/<return> to continue", 0, 15);
        }


        internal static void CMD_SpriteOff() /* sub_28CB6 */
        {
            gbl.ecl_offset++;
            if (gbl.displayPlayerSprite)
            {
                gbl.can_draw_bigpic = true;
                ovr029.RedrawView();
                gbl.displayPlayerSprite = false;
                gbl.byte_1EE8C = false;
            }
        }


        internal static void CMD_EclClock() /* sub_28CDA */
        {
            ovr008.vm_LoadCmdSets(2);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);
            byte var_2 = (byte)ovr008.vm_GetCmdValue(2);

            ovr021.step_game_time(var_2, var_1);
        }


        internal static void CMD_PrintReturn() // sub_28D0F
        {
            gbl.ecl_offset++;

            VmLog.WriteLine("CMD_PrintReturn:");

                gbl.textXCol = 1;
                gbl.textYCol++;
        }


        internal static void CMD_ClearBox() // sub_28D38 
        {
            gbl.ecl_offset++;

            VmLog.WriteLine("CMD_ClearBox:");

            seg037.draw8x8_03();
            ovr025.PartySummary(gbl.player_ptr);
            ovr025.display_map_position_time();

            ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
            ovr025.display_map_position_time();
            gbl.byte_1EE98 = false;
        }


        internal static void CMD_Who() // sub_28D7F
        {
            ovr008.vm_LoadCmdSets(1);
            string prompt = gbl.unk_1D972[1];

            VmLog.WriteLine("CMD_Who: Prompt: '{0}'", prompt);

            seg037.draw8x8_clear_area(TextRegion.NormalBottom);
            ovr025.selectAPlayer(ref gbl.player_ptr, false, prompt);
        }


        internal static void CMD_AddNPC() // sub_28DCA
        {
            ovr008.vm_LoadCmdSets(2);
            int npc_id = (byte)ovr008.vm_GetCmdValue(1);

            ovr017.load_npc(npc_id);

            byte var_6 = (byte)ovr008.vm_GetCmdValue(2);

            var_6 >>= 1;
            var_6 |= 0x80;

            gbl.player_ptr.field_F7 = var_6;

            ovr025.reclac_player_values(gbl.player_ptr);
            ovr025.PartySummary(gbl.player_ptr);
        }


        internal static void CMD_Spell()
        {
            ovr008.vm_LoadCmdSets(3);

            byte spell_id = (byte)ovr008.vm_GetCmdValue(1);
            ushort loc_a = gbl.cmd_opps[2].Word;
            ushort loc_b = gbl.cmd_opps[3].Word;
            
            byte spell_index = 1;
            byte player_index = 0;

            bool spell_found = false;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (spell_found) break;
                spell_index = 1;
                bool not_found = false;

                do
                {
                    if (player.spell_list[spell_index] == spell_id)
                    {
                        spell_found = true;
                    }
                    else
                    {
                        if (spell_index <= 100)
                        {
                            spell_index++;
                        }
                        else
                        {
                            not_found = true;
                        }
                    }
                } while (not_found == false && spell_found == false);

                if (spell_found == false)
                {
                    player_index++;
                }
            }

            if (spell_found == false)
            {
                player_index--;
            }

            if (spell_index > 100)
            {
                spell_index = 0x0FF;
            }

            VmLog.WriteLine("CMD_Spell: spell_id: {0} loc a: {1} val a: {2} loc b: {3} val b: {4}",
                spell_id, new MemLoc(loc_a), spell_index, new MemLoc(loc_b), player_index);

            ovr008.vm_SetMemoryValue(spell_index, loc_a);
            ovr008.vm_SetMemoryValue(player_index, loc_b);
        }


        internal static void CMD_Call()
        {
            ovr008.vm_LoadCmdSets(1);

            ushort var_2 = gbl.cmd_opps[1].Word;
            ushort var_4 = (ushort)(var_2 - 0x7fff);

            VmLog.WriteLine("CMD_Call: {0:X}", var_4);

            switch (var_4)
            {
                case 0xAE11:
                    gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                    if (gbl.byte_1AB0B == true)
                    {
                        if (gbl.byte_1EE8C == true || 
                            gbl.displayPlayerSprite || 
                            gbl.byte_1EE91 == true ||
                            gbl.positionChanged == true ||
                            gbl.byte_1EE94 == true)
                        {
                            gbl.can_draw_bigpic = true;
                            ovr029.RedrawView();
                            ovr025.display_map_position_time();
                            gbl.byte_1EE94 = false;
                            gbl.byte_1EE91 = false;
                            gbl.positionChanged = false;
                            gbl.byte_1EE8C = false;
                            gbl.displayPlayerSprite = false;

                            gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                        }
                    }
                    break;

                case 1:
                    ovr008.SetupDuel(true);
                    break;

                case 2:
                    ovr008.SetupDuel(false);
                    break;

                case 0x3201:
                    if (gbl.word_1EE76 == 8)
                    {
                        seg044.sound_sub_120E0(Sound.sound_a);
                    }
                    else if (gbl.word_1EE76 == 10)
                    {
                        seg044.sound_sub_120E0(Sound.sound_b);
                    }
                    else
                    {
                        seg044.sound_sub_120E0(Sound.sound_a);
                    }
                    break;

                case 0x401F:
                    ovr008.MovePositionForward();
                    break;

                case 0x4019:
                    if (gbl.area_ptr.inDungeon == 0)
                    {
                        gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                    }
                    break;

                case 0xE804:
                    ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[gbl.byte_1D556.curFrame - 1].picture, true, 3, 3);
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
            bool action_interrupted = false;

            RunEclVm(gbl.vm_run_addr_3);
            ovr016.make_camp(out action_interrupted);

            if (action_interrupted == true)
            {
                ovr025.load_pic();
                RunEclVm(gbl.CampInterruptedAddr);
            }
            gbl.can_draw_bigpic = true;
            ovr029.RedrawView();
            gbl.gameSaved = false;
        }


        internal static void CMD_Program() //YourHaveWon
        {
            ovr008.vm_LoadCmdSets(1);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);

            if (gbl.restore_player_ptr == true)
            {
                gbl.player_ptr = gbl.player_ptr2;
                gbl.restore_player_ptr = false;
            }


            if (var_1 == 0)
            {
                ovr018.startGameMenu();
                if (gbl.lastDaxBlockId != 0x50 &&
                    gbl.area_ptr.inDungeon == 0)
                {
                    ovr025.load_pic();
                }
            }
            else if (var_1 == 8)
            {
                ovr019.end_game_text();
                gbl.gameWon = true;
                gbl.area_ptr.field_3FA = 0xff;
                gbl.area2_ptr.training_class_mask = 0xff;

                foreach (Player player in gbl.player_next_ptr)
                {
                    Player play_ptr = player;
                    play_ptr.hit_point_current = play_ptr.hit_point_max;
                    play_ptr.health_status = Status.okey;
                    play_ptr.in_combat = true;
                }

                ovr018.startGameMenu();
                char saveYes = ovr027.yes_no(15, 10, 13, "You've won. Save before quitting? ");

                if (saveYes == 'Y')
                {
                    ovr017.SaveGame();
                }

                seg043.print_and_exit();
            }
            else if (var_1 == 9)
            {
                ushort ecl_bkup = gbl.ecl_offset;
                sub_29094();
                gbl.ecl_offset = ecl_bkup;
                CMD_Exit();
            }
            else if (var_1 == 3)
            {
                gbl.party_killed = true;
                CMD_Exit();
            }
        }


        internal static void CMD_Protection() // sub_2923F
        {
            VmLog.WriteLine("CMD_Protection:");

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
            gbl.byte_1EE8C = false;
            ovr008.vm_LoadCmdSets(1);
            if (Cheats.skip_copy_protection == false)
            {
                ovr004.copy_protection();
            }
            ovr025.load_pic();
        }


        internal static void CMD_Dump() // sub_29271
        {
            gbl.ecl_offset++;

            VmLog.WriteLine("CMD_Dump: Player: {0}", gbl.player_ptr);

            gbl.player_ptr = ovr018.FreeCurrentPlayer(gbl.player_ptr, true, false);

            gbl.player_ptr2 = gbl.player_ptr;

            ovr025.PartySummary(gbl.player_ptr);
        }


        internal static void CMD_FindSpecial() // sub_292A5
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            ovr008.vm_LoadCmdSets(1);
            Affects affect_type = (Affects)ovr008.vm_GetCmdValue(1);

            if (gbl.player_ptr.HasAffect(affect_type) == true)
            {
                gbl.compare_flags[0] = true;
            }
            else
            {
                gbl.compare_flags[1] = true;
            }
        }


        internal static void CMD_DestroyItems() // sub_292F9
        {
            ovr008.vm_LoadCmdSets(1);
            byte item_type = (byte)ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_DestroyItems: type: {0}", item_type);

            foreach (Player player in gbl.player_next_ptr)
            {
                player.items.RemoveAll(item => item.type == item_type);

                ovr025.reclac_player_values(player);
            }
        }


        internal static void commandTable()
        {
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

            //uncomment to debug end-game 
            //gbl.game_area = 6;
            //ovr019.end_game_text(); 

            //System.Console.Out.WriteLine("RunEclVm {0,4:X} start", offset);

            while (gbl.stopVM == false &&
                   gbl.party_killed == false)
            {
                //byte byte_1D928 = gbl.command;

                gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

                VmLog.Write("0x{0:X} ", gbl.ecl_offset);

                if (gbl.printCommands == true)
                {
                    DebugCommand();
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
                gbl.vmFlag01 = false;
                gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                gbl.area2_ptr.tried_to_exit_map = false;

                gbl.player_ptr2 = gbl.player_ptr;

                RunEclVm(gbl.ecl_initial_entryPoint);

                if (gbl.vmFlag01 == false)
                {
                    gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                }

                if (gbl.vmFlag01 == false)
                {
                    if (((gbl.last_game_state != GameState.State4 || gbl.game_state == GameState.State4) && gbl.byte_1AB0B == true) ||
                        (gbl.last_game_state == GameState.State4 && gbl.game_state == GameState.State4))
                    {
                        ovr029.RedrawView();
                    }
                    gbl.vmFlag01 = false;

                    RunEclVm(gbl.vm_run_addr_1);

                    if (gbl.vmFlag01 == false)
                    {
                        RunEclVm(gbl.vm_run_addr_2);

                        if (gbl.vmFlag01 == false)
                        {
                            gbl.player_ptr = gbl.player_ptr2;
                            ovr025.PartySummary(gbl.player_ptr);
                        }
                    }

                }
            } while (gbl.vmFlag01 == true);

            gbl.last_game_state = gbl.game_state;
        }


        internal static void sub_29758()
        {
            gbl.player_ptr2 = gbl.player_ptr;

            gbl.can_draw_bigpic = true;
            gbl.byte_1AB0C = false;
            gbl.filesLoaded = false;
            gbl.restore_player_ptr = false;
            gbl.byte_1AB0B = false;
            gbl.byte_1EE98 = true;
            gbl.game_state = GameState.State4;
            gbl.vmFlag01 = false;

            if (gbl.area_ptr.field_1E4 == 0)
            {
                gbl.byte_1EE98 = false;

                if (gbl.inDemo == true)
                {
                    gbl.byte_1EE88 = 0x52;
                }
                else
                {
                    gbl.byte_1EE88 = 1;

                    ovr025.PartySummary(gbl.player_ptr);
                }
            }
            else
            {
                gbl.byte_1EE88 = (byte)(gbl.area_ptr.field_1E4);
            }

            if (gbl.area_ptr.inDungeon == 0)
            {
                gbl.game_state = GameState.WildernessMap;
            }

            if (gbl.reload_ecl_and_pictures == true ||
                gbl.area_ptr.field_1E4 == 0)
            {
                ovr008.load_ecl_dax(gbl.byte_1EE88);
            }
            else
            {
                gbl.byte_1AB0B = true;
            }

            ovr008.vm_init_ecl();

            RunEclVm(gbl.ecl_initial_entryPoint);

            if (gbl.inDemo == true)
            {
                while(gbl.player_next_ptr.Count > 0)
                {
                    ovr018.FreeCurrentPlayer(gbl.player_next_ptr[0], true, true);
                }
                gbl.player_ptr = null;
            }
            else
            {
                if (gbl.vmFlag01 == false)
                {
                    gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                }
                else
                {
                    sub_29677();
                }

                if (gbl.game_state != GameState.WildernessMap &&
                    gbl.reload_ecl_and_pictures == true)
                {
                    if (gbl.byte_1EE98 == true)
                    {
                        ovr025.load_pic();
                    }

                    gbl.can_draw_bigpic = true;
                    ovr029.RedrawView();
                }

                gbl.reload_ecl_and_pictures = false;

                do
                {
                    char var_1 = ovr015.main_3d_world_menu();

                    gbl.player_ptr2 = gbl.player_ptr;

                    if (gbl.vmFlag01 == false)
                    {
                        gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                    }

                    while ((gbl.area2_ptr.search_flags > 1 || char.ToUpper(var_1) == 'E') &&
                        gbl.party_killed == false)
                    {
                        if (char.ToUpper(var_1) == 'E')
                        {
                            sub_29094();
                        }
                        else
                        {
                            gbl.search_flag_bkup = gbl.area2_ptr.search_flags & 1;
                            gbl.area2_ptr.search_flags = 1;
                            gbl.can_draw_bigpic = true;
                            ovr029.RedrawView();

                            RunEclVm(gbl.vm_run_addr_2);

                            if (gbl.vmFlag01 == true)
                            {
                                sub_29677();
                            }

                            gbl.area2_ptr.search_flags = (ushort)gbl.search_flag_bkup;
                        }

                        if (gbl.party_killed == false)
                        {
                            var_1 = ovr015.main_3d_world_menu();
                            gbl.player_ptr2 = gbl.player_ptr;
                        }
                    }


                    if (gbl.party_killed == false)
                    {
                        RunEclVm(gbl.vm_run_addr_1);
                    }

                    if (gbl.vmFlag01 == true)
                    {
                        sub_29677();
                    }
                    else
                    {
                        if (gbl.party_killed == false)
                        {
                            gbl.area_ptr.lastXPos = (short)gbl.mapPosX;
                            gbl.area_ptr.lastYPos = (short)gbl.mapPosY;

                            ovr015.locked_door();
                            ovr029.RedrawView();

                            if (gbl.area_ptr.lastXPos != gbl.mapPosX ||
                                gbl.area_ptr.lastYPos != gbl.mapPosY)
                            {
                                seg044.sound_sub_120E0(Sound.sound_a);
                            }

                            gbl.byte_1EE8C = false;
                            gbl.byte_1EE8D = true;
                            RunEclVm(gbl.vm_run_addr_2);
                            if (gbl.vmFlag01 == true)
                            {
                                sub_29677();
                            }
                        }
                    }
                } while (gbl.party_killed == false);

                gbl.party_killed = false;
            }
        }

        internal static void DebugCommand()
        {
            string name;

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

            Logger.Debug("{1} 0x{0:X}", gbl.command, name);
        }
    }
}
