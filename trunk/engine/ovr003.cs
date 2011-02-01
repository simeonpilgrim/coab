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
                gbl.SelectedPlayer = gbl.LastSelectedPlayer;
                gbl.restore_player_ptr = false;
            }

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;

            gbl.spriteChanged = false;
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

            int player_index = (byte)ovr008.vm_GetCmdValue(1);
            VmLog.WriteLine("CMD_LoadCharacter: 0x{0:X}", player_index);

            gbl.restore_player_ptr = true;

            bool high_bit_set = (player_index & 0x80) != 0;
            player_index = player_index & 0x7f;

            Player player_ptr = player_index > 0 && player_index < gbl.TeamList.Count ? gbl.TeamList[player_index] : null;

            if (player_ptr != null)
            {
                gbl.SelectedPlayer = player_ptr;
                gbl.player_not_found = false;
            }
            else
            {
                gbl.player_not_found = true;
            }

            if (high_bit_set == true &&
                gbl.redrawPartySummary1 == true &&
                gbl.redrawPartySummary2 == true)
            {
                if (gbl.LastSelectedPlayer == player_ptr)
                {
                    gbl.restore_player_ptr = false;
                }
                gbl.SelectedPlayer = ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);

                ovr025.PartySummary(gbl.SelectedPlayer);
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
            Player current_player_bkup = gbl.SelectedPlayer;
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

                gbl.TeamList.Add(newMob);

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

                    foreach (Affect affect in mobMasterCopy.affects)
                    {
                        newMob.affects.Add(affect.ShallowClone());
                    }

                    copy_count++;
                    gbl.numLoadedMonsters++;
                    gbl.TeamList.Add(newMob);
                }

                gbl.monster_icon_id++;
                gbl.monstersLoaded = true;
                gbl.SelectedPlayer = current_player_bkup;
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
                gbl.spriteChanged = true;

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
                if ((gbl.last_game_state != GameState.DungeonMap || gbl.game_state == GameState.DungeonMap) &&
                    (gbl.spriteChanged == true || gbl.displayPlayerSprite))
                {
                    gbl.can_draw_bigpic = true;
                    ovr029.RedrawView();
                    gbl.spriteChanged = false;
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
                seg041.press_any_key(gbl.unk_1D972[1], false, 10, TextRegion.NormalBottom);
            }
            else
            {
                gbl.textYCol = 0x11;
                gbl.textXCol = 1;

                seg041.press_any_key(gbl.unk_1D972[1], true, 10, TextRegion.NormalBottom);
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
                SkipNextCommand();
            }
        }


        internal static void CMD_NewECL()
        {
            ovr008.vm_LoadCmdSets(1);

            byte block_id = (byte)ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_NewECL: block_id {0}", block_id);

            gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
            gbl.EclBlockId = block_id;

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
                            gbl.setBlocks[0].Reset();
                        }

                        if (var_2 != 0xff)
                        {
                            ovr031.LoadWalldef(2, var_2);
                        }
                        else
                        {
                            gbl.setBlocks[1].Reset();
                        }

                        if (var_1 != 0xff)
                        {
                            ovr031.LoadWalldef(3, var_1);
                        }
                        else
                        {
                            gbl.setBlocks[2].Reset();
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
                    ovr025.PartySummary(gbl.SelectedPlayer);
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

            seg041.press_any_key(delay_text, true, 10, 22, 38, 17, 1);

            for (int i = 0; i < menuCount; i++)
            {
                menuList.Add(new MenuItem(gbl.unk_1D972[i + 1]));
            }

            int index = ovr008.VertMenuSelect(0, true, false, menuList, 0x16, 0x26, gbl.textYCol + 1, 1);

            ovr008.vm_SetMemoryValue((ushort)index, mem_loc);

            menuList.Clear();
            seg037.draw8x8_clear_area(TextRegion.NormalBottom);
        }


        internal static void CMD_HorizontalMenu()
        {
            bool useOverlay;
            bool var_3B;

            ovr008.vm_LoadCmdSets(2);

            ushort loc = gbl.cmd_opps[1].Word;
            byte string_count = (byte)ovr008.vm_GetCmdValue(2);

            gbl.ecl_offset--;

            ovr008.vm_LoadCmdSets(string_count);

            MenuColorSet colors;
            if (string_count == 1)
            {
                var_3B = true;
                colors = new MenuColorSet(15, 15, 13);

                if (gbl.unk_1D972[1] == "PRESS BUTTON OR RETURN TO CONTINUE.")
                {
                    gbl.unk_1D972[1] = "PRESS <ENTER>/<RETURN> TO CONTINUE";
                }
            }
            else
            {
                colors = new MenuColorSet(1, 15, 15);
                var_3B = false;
                colors = gbl.defaultMenuColors;
            }

            if (gbl.spriteChanged == false ||
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

            byte menu_selected = (byte)ovr008.sub_317AA(useOverlay, var_3B, colors, text, "");

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

            gbl.pooled_money.ClearAll();
            gbl.items_pointer.Clear();
        }


        internal static void CMD_PartyStrength() /* sub_272A9 */
        {
            ovr008.vm_LoadCmdSets(1);
            byte power_value = 0;

            foreach (Player player in gbl.TeamList)
            {
                int hit_points = player.hit_point_current;
                int armor_class = player.ac;
                int hit_bonus = player.hitBonus;

                int magic_power = player.SkillLevel(SkillType.MagicUser);
                int cleric_power = player.SkillLevel(SkillType.Cleric);

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
                bool affect_found = gbl.TeamList.Exists(player => player.HasAffect(affect_id));

                setMemoryFour(affect_found, 0, 0, 0, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 >= 0x00A5 && var_2 <= 0x00AC)
            {
                int index = var_2 - 0xA4;
                int count = 0;
                foreach (Player player in gbl.TeamList)
                {
                    count++;

                    if (player.thief_skills[index - 1] < val_a)
                    {
                        val_a = player.thief_skills[index - 1];
                    }

                    if (player.thief_skills[index - 1] > val_b)
                    {
                        val_b = player.thief_skills[index - 1];
                    }

                    var_4 += player.thief_skills[index - 1];
                }

                val_c = (byte)(var_4 / count);

                setMemoryFour(false, val_c, val_b, val_a, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 == 0x9f)
            {
                int count = 0;
                foreach (Player player in gbl.TeamList)
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

            foreach (Player player in gbl.TeamList)
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
                if (gbl.area2_ptr.EnterShop == 1)
                {
                    gbl.area2_ptr.EnterShop = 0;

                    ovr007.CityShop();
                }
                else if (gbl.area2_ptr.EnterTemple == 1)
                {
                    gbl.area2_ptr.EnterTemple = 0;

                    ovr005.temple_shop();
                }
                else
                {
                    ovr006.AfterCombatExpAndTreasure();
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

                ovr006.AfterCombatExpAndTreasure();

                if (gbl.area_ptr.inDungeon == 0)
                {
                    ovr030.load_bigpic(0x79);
                }
            }

            if (gbl.area_ptr.inDungeon != 0)
            {
                gbl.game_state = GameState.DungeonMap;
            }
            else
            {
                gbl.game_state = GameState.WildernessMap;
            }

            gbl.area2_ptr.search_flags &= 1;

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
            gbl.spriteChanged = false;
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

            for (int coin = 0; coin < 7; coin++)
            {
                gbl.pooled_money.SetCoins(coin, ovr008.vm_GetCmdValue(coin + 1));
            }

            byte block_id = (byte)ovr008.vm_GetCmdValue(8);

            if (block_id < 0x80)
            {
                string filename = string.Format("ITEM{0}.dax", gbl.game_area);
                seg042.load_decode_dax(out data, out dataSize, block_id, filename);

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
            else if (block_id != 0xff)
            {
                for (int count = 0; count < (block_id - 0x80); count++)
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
                ovr008.RobMoney(gbl.SelectedPlayer, percentage);
                ovr008.RobItems(gbl.SelectedPlayer, var_3);
            }
            else
            {
                foreach (Player player in gbl.TeamList)
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
                if (gbl.spriteChanged == false ||
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

                seg041.press_any_key(text, clearTextArea, 10, TextRegion.NormalBottom);

                if (gbl.area2_ptr.encounter_distance == 0 ||
                    gbl.area_ptr.inDungeon == 0)
                {
                    displayText = "~COMBAT ~WAIT ~FLEE ~PARLAY";
                }
                else
                {
                    displayText = "~COMBAT ~WAIT ~FLEE ~ADVANCE";
                }

                menu_selected = ovr008.sub_317AA(useOverlay, false, gbl.defaultMenuColors, displayText, "");

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
                            seg041.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
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
                                seg041.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
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
                                seg041.press_any_key("The monsters flee.", true, 10, TextRegion.NormalBottom);
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
                            seg041.press_any_key("The monsters flee.", true, 10, TextRegion.NormalBottom);
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
                                seg041.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
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

            int menu_selected = ovr008.sub_317AA(false, false, gbl.defaultMenuColors, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", " ");

            ushort location = gbl.cmd_opps[6].Word;

            byte value = values[menu_selected];

            ovr008.vm_SetMemoryValue(value, location);
        }


        internal static void CMD_FindItem() // sub_28856
        {
            ovr008.vm_LoadCmdSets(1);

            ItemType item_type = (ItemType)ovr008.vm_GetCmdValue(1);

            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            gbl.compare_flags[1] = true;

            foreach (Player player in gbl.TeamList)
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
            Player currentPlayerBackup = gbl.SelectedPlayer;

            ovr008.vm_LoadCmdSets(5);
            byte var_1 = (byte)ovr008.vm_GetCmdValue(1);
            int dice_count = ovr008.vm_GetCmdValue(2);
            int dice_size = ovr008.vm_GetCmdValue(3);
            int dam_plus = ovr008.vm_GetCmdValue(4);
            byte var_6 = (byte)ovr008.vm_GetCmdValue(5);

            int damage = ovr024.roll_dice(dice_size, dice_count) + dam_plus;

            byte rnd_player_id = 0;
            if ((var_1 & 0x40) == 0)
            {
                rnd_player_id = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
            }

            if ((var_1 & 0x80) != 0)
            {
                int saveBonus = var_1 & 0x1f;
                int bonusType = var_6 & 7;

                if ((var_1 & 0x40) != 0)
                {
                    foreach (Player player03 in gbl.TeamList)
                    {
                        if ((var_1 & 0x20) != 0)
                        {
                            ovr008.sub_32200(player03, damage);
                        }
                        else if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, player03) == false)
                        {
                            ovr008.sub_32200(player03, damage);
                        }
                        else if ((var_1 & 0x10) != 0)
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
                            ovr024.RollSavingThrow(saveBonus, (SaveVerseType)(bonusType - 1), gbl.SelectedPlayer) == false)
                        {
                            ovr008.sub_32200(gbl.SelectedPlayer, damage);
                        }
                        else if ((var_1 & 0x10) != 0)
                        {
                            ovr008.sub_32200(gbl.SelectedPlayer, damage);
                        }
                    }
                    else
                    {
                        Player target = gbl.TeamList[rnd_player_id - 1];

                        if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, target) == false)
                        {
                            ovr008.sub_32200(target, damage);
                        }
                        else if ((var_1 & 0x10) != 0)
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
                    rnd_player_id = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
                    Player player03 = gbl.TeamList[rnd_player_id - 1];

                    if (ovr024.CanHitTarget(var_6, player03) == true)
                    {
                        ovr008.sub_32200(player03, damage);
                    }

                    damage = ovr024.roll_dice(dice_size, dice_count) + dam_plus;
                }
            }

            gbl.party_killed = true;

            foreach (Player player in gbl.TeamList)
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

                seg041.press_any_key("The entire party is killed!", true, 10, 0x16, 0x26, 1, 1);
                seg049.SysDelay(3000);
            }

            gbl.SelectedPlayer = currentPlayerBackup;
            seg041.DisplayAndPause("press <enter>/<return> to continue", 15);
        }


        internal static void CMD_SpriteOff() /* sub_28CB6 */
        {
            gbl.ecl_offset++;
            if (gbl.displayPlayerSprite)
            {
                gbl.can_draw_bigpic = true;
                ovr029.RedrawView();
                gbl.displayPlayerSprite = false;
                gbl.spriteChanged = false;
            }
        }


        internal static void CMD_EclClock() /* sub_28CDA */
        {
            ovr008.vm_LoadCmdSets(2);
            int timeStep = ovr008.vm_GetCmdValue(1) & 0xff;
            int timeSlot = ovr008.vm_GetCmdValue(2) & 0xff;

            ovr021.step_game_time(timeSlot, timeStep);
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
            ovr025.PartySummary(gbl.SelectedPlayer);
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
            ovr025.selectAPlayer(ref gbl.SelectedPlayer, false, prompt);
        }


        internal static void CMD_AddNPC() // sub_28DCA
        {
            ovr008.vm_LoadCmdSets(2);
            int npc_id = (byte)ovr008.vm_GetCmdValue(1);

            ovr017.load_npc(npc_id);

            byte morale = (byte)ovr008.vm_GetCmdValue(2);

            gbl.SelectedPlayer.control_morale = (byte)((morale >> 1) + 0x80);

            ovr025.reclac_player_values(gbl.SelectedPlayer);
            ovr025.PartySummary(gbl.SelectedPlayer);
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

            foreach (Player player in gbl.TeamList)
            {
                spell_index = 1;

                foreach (int id in player.spellList.IdList())
                {
                    if (id == spell_id)
                    {
                        spell_found = true;
                        break;
                    }

                    spell_index += 1;
                }

                if (spell_found) break;

                player_index++;
            }

            if (spell_found == false)
            {
                player_index--;
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
                        if (gbl.spriteChanged == true ||
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
                            gbl.spriteChanged = false;
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
                    ovr030.DrawMaybeOverlayed(gbl.byte_1D556.CurrentPicture(), true, 3, 3);

                    gbl.byte_1D556.NextFrame();

                    seg041.GameDelay();
                    break;
            }
        }


        internal static void TryEncamp()
        {
            RunEclVm(gbl.PreCampCheckAddr);

            if (ovr016.make_camp() == true)
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
                gbl.SelectedPlayer = gbl.LastSelectedPlayer;
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

                foreach (Player player in gbl.TeamList)
                {
                    Player play_ptr = player;
                    play_ptr.hit_point_current = play_ptr.hit_point_max;
                    play_ptr.health_status = Status.okey;
                    play_ptr.in_combat = true;
                }

                ovr018.startGameMenu();
                char saveYes = ovr027.yes_no(gbl.defaultMenuColors, "You've won. Save before quitting? ");

                if (saveYes == 'Y')
                {
                    ovr017.SaveGame();
                }

                seg043.print_and_exit();
            }
            else if (var_1 == 9)
            {
                ushort ecl_bkup = gbl.ecl_offset;
                TryEncamp();
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
            gbl.spriteChanged = false;
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

            VmLog.WriteLine("CMD_Dump: Player: {0}", gbl.SelectedPlayer);

            gbl.SelectedPlayer = ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);

            gbl.LastSelectedPlayer = gbl.SelectedPlayer;

            ovr025.PartySummary(gbl.SelectedPlayer);
        }


        internal static void CMD_FindSpecial() // sub_292A5
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            ovr008.vm_LoadCmdSets(1);
            Affects affect_type = (Affects)ovr008.vm_GetCmdValue(1);

            if (gbl.SelectedPlayer.HasAffect(affect_type) == true)
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
            ItemType item_type = (ItemType)ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_DestroyItems: type: {0}", item_type);

            foreach (Player player in gbl.TeamList)
            {
                player.items.RemoveAll(item => item.type == item_type);

                ovr025.reclac_player_values(player);
            }
        }



        static Dictionary<int, CmdItem> CommandTable = new Dictionary<int, CmdItem>();

        public static void SetupCommandTable()
        {
            CommandTable.Add(0x00, new CmdItem(0, "EXIT", CMD_Exit));
            CommandTable.Add(0x01, new CmdItem(1, "GOTO", CMD_Goto));
            CommandTable.Add(0x02, new CmdItem(1, "GOSUB", CMD_Gosub));
            CommandTable.Add(0x03, new CmdItem(2, "COMPARE", CMD_Compare));
            CommandTable.Add(0x04, new CmdItem(3, "ADD", CMD_AddSubDivMulti));
            CommandTable.Add(0x05, new CmdItem(3, "SUBTRACT", CMD_AddSubDivMulti));
            CommandTable.Add(0x06, new CmdItem(3, "DIVIDE", CMD_AddSubDivMulti));
            CommandTable.Add(0x07, new CmdItem(3, "MULTIPLY", CMD_AddSubDivMulti));
            CommandTable.Add(0x08, new CmdItem(2, "RANDOM", CMD_Random));
            CommandTable.Add(0x09, new CmdItem(2, "SAVE", CMD_Save));
            CommandTable.Add(0x0A, new CmdItem(1, "LOAD CHARACTER", CMD_LoadCharacter));
            CommandTable.Add(0x0B, new CmdItem(3, "LOAD MONSTER", CMD_LoadMonster));
            CommandTable.Add(0x0C, new CmdItem(3, "SETUP MONSTER", CMD_SetupMonster));
            CommandTable.Add(0x0D, new CmdItem(0, "APPROACH", CMD_Approach));
            CommandTable.Add(0x0E, new CmdItem(1, "PICTURE", CMD_Picture));
            CommandTable.Add(0x0F, new CmdItem(2, "INPUT NUMBER", CMD_InputNumber));
            CommandTable.Add(0x10, new CmdItem(2, "INPUT STRING", CMD_InputString));
            CommandTable.Add(0x11, new CmdItem(1, "PRINT", CMD_Print));
            CommandTable.Add(0x12, new CmdItem(1, "PRINTCLEAR", CMD_Print));
            CommandTable.Add(0x13, new CmdItem(0, "RETURN", CMD_Return));
            CommandTable.Add(0x14, new CmdItem(4, "COMPARE AND", CMD_CompareAnd));
            CommandTable.Add(0x15, new CmdItem(0, "VERTICAL MENU", CMD_VertMenu));
            CommandTable.Add(0x16, new CmdItem(0, "IF =", CMD_If));
            CommandTable.Add(0x17, new CmdItem(0, "IF <>", CMD_If));
            CommandTable.Add(0x18, new CmdItem(0, "IF <", CMD_If));
            CommandTable.Add(0x19, new CmdItem(0, "IF >", CMD_If));
            CommandTable.Add(0x1A, new CmdItem(0, "IF <=", CMD_If));
            CommandTable.Add(0x1B, new CmdItem(0, "IF >=", CMD_If));
            CommandTable.Add(0x1C, new CmdItem(0, "CLEARMONSTERS", CMD_ClearMonsters));
            CommandTable.Add(0x1D, new CmdItem(1, "PARTYSTRENGTH", CMD_PartyStrength));
            CommandTable.Add(0x1E, new CmdItem(6, "CHECKPARTY", CMD_CheckParty));
            CommandTable.Add(0x1F, new CmdItem(2, "notsure 0x1f", null));
            CommandTable.Add(0x20, new CmdItem(1, "NEWECL", CMD_NewECL));
            CommandTable.Add(0x21, new CmdItem(3, "LOAD FILES", CMD_LoadFiles));
            CommandTable.Add(0x22, new CmdItem(2, "PARTY SURPRISE", CMD_PartySurprise));
            CommandTable.Add(0x23, new CmdItem(4, "SURPRISE", CMD_Surprise));
            CommandTable.Add(0x24, new CmdItem(0, "COMBAT", CMD_Combat));
            CommandTable.Add(0x25, new CmdItem(0, "ON GOTO", CMD_OnGotoGoSub));
            CommandTable.Add(0x26, new CmdItem(0, "ON GOSUB", CMD_OnGotoGoSub));
            CommandTable.Add(0x27, new CmdItem(8, "TREASURE", CMD_Treasure));
            CommandTable.Add(0x28, new CmdItem(3, "ROB", CMD_Rob));
            CommandTable.Add(0x29, new CmdItem(14, "ENCOUNTER MENU", CMD_EncounterMenu));
            CommandTable.Add(0x2A, new CmdItem(3, "GETTABLE", CMD_GetTable));
            CommandTable.Add(0x2B, new CmdItem(0, "HORIZONTAL MENU", CMD_HorizontalMenu));
            CommandTable.Add(0x2C, new CmdItem(6, "PARLAY", CMD_Parlay));
            CommandTable.Add(0x2D, new CmdItem(1, "CALL", CMD_Call));
            CommandTable.Add(0x2E, new CmdItem(5, "DAMAGE", CMD_Damage));
            CommandTable.Add(0x2F, new CmdItem(3, "AND", CMD_AndOr));
            CommandTable.Add(0x30, new CmdItem(3, "OR", CMD_AndOr));
            CommandTable.Add(0x31, new CmdItem(0, "SPRITE OFF", CMD_SpriteOff));
            CommandTable.Add(0x32, new CmdItem(1, "FIND ITEM", CMD_FindItem));
            CommandTable.Add(0x33, new CmdItem(0, "PRINT RETURN", CMD_PrintReturn));
            CommandTable.Add(0x34, new CmdItem(1, "ECL CLOCK", CMD_EclClock));
            CommandTable.Add(0x35, new CmdItem(3, "SAVE TABLE", CMD_SaveTable));
            CommandTable.Add(0x36, new CmdItem(1, "ADD NPC", CMD_AddNPC));
            CommandTable.Add(0x37, new CmdItem(3, "LOAD PIECES", CMD_LoadFiles));
            CommandTable.Add(0x38, new CmdItem(1, "PROGRAM", CMD_Program));
            CommandTable.Add(0x39, new CmdItem(1, "WHO", CMD_Who));
            CommandTable.Add(0x3A, new CmdItem(0, "DELAY", CMD_Delay));
            CommandTable.Add(0x3B, new CmdItem(3, "SPELL", CMD_Spell));
            CommandTable.Add(0x3C, new CmdItem(1, "PROTECTION", CMD_Protection));
            CommandTable.Add(0x3D, new CmdItem(0, "CLEAR BOX", CMD_ClearBox));
            CommandTable.Add(0x3E, new CmdItem(0, "DUMP", CMD_Dump));
            CommandTable.Add(0x3F, new CmdItem(1, "FIND SPECIAL", CMD_FindSpecial));
            CommandTable.Add(0x40, new CmdItem(1, "DESTROY ITEMS", CMD_DestroyItems));
        }

        static void SkipNextCommand()
        {
            gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

            CmdItem cmd;
            if (CommandTable.TryGetValue(gbl.command, out cmd))
            {
                cmd.Skip();
            }
            else
            {
                Logger.Log("Skipping Unknown command id {0}", gbl.command);
                gbl.ecl_offset += 1;
            }
        }


        internal static void RunEclVm(ushort offset) // sub_29607
        {
            gbl.ecl_offset = offset;
            gbl.stopVM = false;

            //System.Console.Out.WriteLine("RunEclVm {0,4:X} start", offset);

            while (gbl.stopVM == false &&
                   gbl.party_killed == false)
            {
                gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

                VmLog.Write("0x{0:X} ", gbl.ecl_offset);

                CmdItem cmd;
                if (CommandTable.TryGetValue(gbl.command, out cmd))
                {
                    if (gbl.printCommands)
                    {
                        Logger.Debug("{0} 0x{1:X}", cmd.Name(), gbl.command);
                    }
                    cmd.Run();
                }
                else
                {
                    Logger.Log("Unknown command id {0}", gbl.command);
                }
            }

            gbl.stopVM = false;
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

                gbl.LastSelectedPlayer = gbl.SelectedPlayer;

                RunEclVm(gbl.ecl_initial_entryPoint);

                if (gbl.vmFlag01 == false)
                {
                    gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
                }

                if (gbl.vmFlag01 == false)
                {
                    if (((gbl.last_game_state != GameState.DungeonMap || gbl.game_state == GameState.DungeonMap) && gbl.byte_1AB0B == true) ||
                        (gbl.last_game_state == GameState.DungeonMap && gbl.game_state == GameState.DungeonMap))
                    {
                        ovr029.RedrawView();
                    }
                    gbl.vmFlag01 = false;

                    RunEclVm(gbl.vm_run_addr_1);

                    if (gbl.vmFlag01 == false)
                    {
                        RunEclVm(gbl.SearchLocationAddr);

                        if (gbl.vmFlag01 == false)
                        {
                            gbl.SelectedPlayer = gbl.LastSelectedPlayer;
                            ovr025.PartySummary(gbl.SelectedPlayer);
                        }
                    }

                }
            } while (gbl.vmFlag01 == true);

            gbl.last_game_state = gbl.game_state;
        }


        internal static void sub_29758()
        {
            gbl.LastSelectedPlayer = gbl.SelectedPlayer;

            gbl.can_draw_bigpic = true;
            gbl.byte_1AB0C = false;
            gbl.filesLoaded = false;
            gbl.restore_player_ptr = false;
            gbl.byte_1AB0B = false;
            gbl.byte_1EE98 = true;
            gbl.game_state = GameState.DungeonMap;
            gbl.vmFlag01 = false;

            if (gbl.area_ptr.LastEclBlockId == 0)
            {
                gbl.byte_1EE98 = false;

                if (gbl.inDemo == true)
                {
                    gbl.EclBlockId = 0x52;
                }
                else
                {
                    gbl.EclBlockId = 1;

                    ovr025.PartySummary(gbl.SelectedPlayer);
                }
            }
            else
            {
                gbl.EclBlockId = (byte)(gbl.area_ptr.LastEclBlockId);
            }

            if (gbl.area_ptr.inDungeon == 0)
            {
                gbl.game_state = GameState.WildernessMap;
            }

            if (gbl.reload_ecl_and_pictures == true ||
                gbl.area_ptr.LastEclBlockId == 0)
            {
                ovr008.load_ecl_dax(gbl.EclBlockId);
            }
            else
            {
                gbl.byte_1AB0B = true;
            }

            ovr008.vm_init_ecl();

            RunEclVm(gbl.ecl_initial_entryPoint);

            if (gbl.inDemo == true)
            {
                while (gbl.TeamList.Count > 0)
                {
                    ovr018.FreeCurrentPlayer(gbl.TeamList[0], true, true);
                }
                gbl.SelectedPlayer = null;
            }
            else
            {
                if (gbl.vmFlag01 == false)
                {
                    gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
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

                    gbl.LastSelectedPlayer = gbl.SelectedPlayer;

                    if (gbl.vmFlag01 == false)
                    {
                        gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
                    }

                    while ((gbl.area2_ptr.search_flags > 1 || char.ToUpper(var_1) == 'E') &&
                        gbl.party_killed == false)
                    {
                        if (char.ToUpper(var_1) == 'E')
                        {
                            TryEncamp();
                        }
                        else
                        {
                            gbl.search_flag_bkup = gbl.area2_ptr.search_flags & 1;
                            gbl.area2_ptr.search_flags = 1;
                            gbl.can_draw_bigpic = true;
                            ovr029.RedrawView();

                            RunEclVm(gbl.SearchLocationAddr);

                            if (gbl.vmFlag01 == true)
                            {
                                sub_29677();
                            }

                            gbl.area2_ptr.search_flags = (ushort)gbl.search_flag_bkup;
                        }

                        if (gbl.party_killed == false)
                        {
                            var_1 = ovr015.main_3d_world_menu();
                            gbl.LastSelectedPlayer = gbl.SelectedPlayer;
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

                            gbl.spriteChanged = false;
                            gbl.byte_1EE8D = true;
                            RunEclVm(gbl.SearchLocationAddr);
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
    }

    internal class CmdItem
    {
        public delegate void CmdDelegate();

        int size;
        string name;
        CmdDelegate cmd;

        public CmdItem(int Size, string Name, CmdDelegate Cmd)
        {
            size = Size;
            name = Name;
            cmd = Cmd;
        }

        public void Run()
        {
            cmd();
        }

        public string Name()
        {
            return name;
        }

        internal void Skip()
        {
            if (gbl.printCommands == true)
            {
                Logger.Debug("SKIPPING: {0}", name);
            }

            if (size == 0)
            {
                gbl.ecl_offset += 1;
            }
            else
            {
                ovr008.vm_LoadCmdSets(size);
            }
        }
    }
}
