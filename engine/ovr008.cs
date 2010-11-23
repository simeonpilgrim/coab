using Classes;
using Logging;
using System.Collections.Generic;

namespace engine
{
    class ovr008
    {
        internal static void vm_LoadCmdSets(int numberOfSets) // parse_command_sub
        {
            byte strIndex = 0;

            foreach (Opperation opp in gbl.cmd_opps)
            {
                opp.Clear();
            }

            for (int loop_var = 1; loop_var <= numberOfSets; loop_var++)
            {
                byte code = gbl.ecl_ptr[0x8000 + gbl.ecl_offset + 1];
                byte low = gbl.ecl_ptr[0x8000 + gbl.ecl_offset + 2];


                gbl.cmd_opps[loop_var].Code = code;
                gbl.cmd_opps[loop_var].Low = low;

                gbl.ecl_offset += 2;

                if (code == 1 || code == 2 || code == 3)
                {
                    gbl.ecl_offset++;
                    byte high = gbl.ecl_ptr[0x8000 + gbl.ecl_offset];

                    gbl.cmd_opps[loop_var].High = high;

                    //System.Console.WriteLine("   code: {0,2:X} low: {1,2:X} high: {2,2:X}",
                    //   code, low, high);
                }
                else if (code == 0x80) // Load compressed string
                {
                    strIndex++;

                    short strLen = low;

                    if (strLen > 0)
                    {
                        LoadCompressedEclString(strIndex, strLen);
                    }
                    else
                    {
                        gbl.unk_1D972[strIndex] = string.Empty;
                    }

                    //System.Console.WriteLine("   code: {0,2:X} strIndex: {1} strLen: {2} str: '{3}'",
                    //    code, strIndex, strLen, gbl.unk_1D972[strIndex]);
                }
                else if (code == 0x81)
                {
                    strIndex++;
                    gbl.ecl_offset++;
                    byte high = gbl.ecl_ptr[0x8000 + gbl.ecl_offset];

                    gbl.cmd_opps[loop_var].High = high;

                    ushort loc = gbl.cmd_opps[loop_var].Word;

                    vm_CopyStringFromMemory(loc, strIndex);

                    //System.Console.WriteLine("   code: {0,2:X} strIndex: {1} loc: {2} str: '{3}'",
                    //    code, strIndex, loc, gbl.unk_1D972[strIndex]);
                }
                else
                {
                    //System.Console.WriteLine("   code: {0,2:X} low: 0x{1,2:X}",
                    //    code, low);
                }
            }

            gbl.ecl_offset++;
        }

        internal static ushort vm_GetCmdValue(int arg_0) // sub_30168
        {
            return gbl.cmd_opps[arg_0].GetCmdValue();
            //TODO replace calls to vm_GetCmdValue function with gbl.cmd_opps[arg_0].GetCmdValue();
        }


        internal static void vm_init_ecl() // sub_301E8
        {
            gbl.spriteChanged = false;
            gbl.redrawPartySummary1 = false;
            gbl.redrawPartySummary2 = false;
            gbl.byte_1EE91 = true;

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
            gbl.monster_icon_id = 8;
            gbl.ecl_offset = 0x8000;
            gbl.byte_1DA70 = false;

            gbl.vmCallStack.Clear();

            for (int i = 0; i < 6; i++)
            {
                gbl.compare_flags[i] = false;
            }

            gbl.area2_ptr.HeadBlockId = 0xFF;

            gbl.area2_ptr.rest_incounter_period = 0;
            gbl.area2_ptr.rest_incounter_percentage = 0;
            gbl.area_ptr.can_cast_spells = false;

            vm_LoadCmdSets(1);
            gbl.vm_run_addr_1 = gbl.cmd_opps[1].Word;
            vm_LoadCmdSets(1);
            gbl.SearchLocationAddr = gbl.cmd_opps[1].Word;
            vm_LoadCmdSets(1);
            gbl.PreCampCheckAddr = gbl.cmd_opps[1].Word;
            vm_LoadCmdSets(1);
            gbl.CampInterruptedAddr = gbl.cmd_opps[1].Word;
            vm_LoadCmdSets(1);
            gbl.ecl_initial_entryPoint = gbl.cmd_opps[1].Word;

            gbl.area_ptr.inDungeon = 1;

            if (gbl.reload_ecl_and_pictures == false)
            {
                gbl.area_ptr.RestField200Values();
                gbl.area2_ptr.RestField6F2Values();
            }
        }


        internal static void load_ecl_dax(byte block_id)
        {
            byte[] block_mem;
            short block_size = 0;

            gbl.ecl_ptr.Clear();

            do
            {
                ovr027.ClearPromptArea();
                seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);

                seg042.load_decode_dax(out block_mem, out block_size, block_id, string.Format("ECL{0}.dax", gbl.game_area));
            } while (block_size < 2);

            gbl.ecl_ptr.SetData(block_mem, 2, block_size - 2);

            ovr027.ClearPromptArea();
        }


        internal static byte sub_304B4(int map_dir, int map_y, int map_x)
        {
            byte var_1 = 0;

            if (gbl.area_ptr.inDungeon == 0)
            {
                var_1 = 2;
                gbl.area2_ptr.encounter_distance = 2;
            }
            else
            {
                bool var_2 = false;
                byte var_3 = 0;

                while (var_3 < 2 && var_2 == false)
                {
                    if (ovr031.getMap_wall_type(map_dir, map_y, map_x) == 0)
                    {
                        var_3++;
                        var_1 = var_3;

                        switch (map_dir)
                        {
                            case 0:
                                map_y--;
                                break;

                            case 2:
                                map_x++;
                                break;

                            case 4:
                                map_y++;
                                break;

                            case 6:
                                map_x--;
                                break;
                        }
                    }
                    else
                    {
                        var_2 = true;
                    }
                }
            }

            return var_1;
        }


        internal static void set_and_draw_head_body(byte body_id, byte head_id) /* sub_30543 */
        {
            gbl.byte_1EE8D = false;

            gbl.head_block_id = head_id;
            gbl.body_block_id = body_id;

            ovr030.head_body(body_id, head_id);
            ovr030.draw_head_and_body(true, 3, 3);
        }


        internal static void sub_30580(bool[] flags, int encounter_distance, byte pic_block_id, byte sprite_block_id)
        {
            if (flags[1] == false)
            {
                if (flags[0] == false)
                {
                    if (gbl.mapAreaDisplay == true)
                    {
                        gbl.mapAreaDisplay = false;
                        gbl.can_draw_bigpic = true;
                        ovr029.RedrawView();
                    }

                    if (gbl.area_ptr.inDungeon != 0)
                    {
                        ovr030.load_pic_final(ref gbl.byte_1D556, 1, sprite_block_id, "SPRIT");
                        flags[0] = true;
                        gbl.displayPlayerSprite = true;
                    }
                }
                else
                {
                    gbl.can_draw_bigpic = true;
                    ovr029.RedrawView();
                }

                if (gbl.game_state == GameState.DungeonMap)
                {
                    ovr030.Show3DSprite(gbl.byte_1D556, encounter_distance + 1);
                }
            }

            if (flags[1] == false ||
                gbl.byte_1EE96 != gbl.area2_ptr.HeadBlockId)
            {
                if (encounter_distance == 0 &&
                    gbl.game_state == GameState.DungeonMap &&
                    gbl.byte_1EE95 == false)
                {
                    gbl.byte_1EE96 = (byte)gbl.area2_ptr.HeadBlockId;
                    gbl.spriteChanged = true;
                    if (gbl.area2_ptr.HeadBlockId == 0xff)
                    {
                        ovr030.load_pic_final(ref gbl.byte_1D556, 0, pic_block_id, "PIC");
                        flags[1] = true;

                        ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
                    }
                    else
                    {
                        set_and_draw_head_body(pic_block_id, (byte)gbl.area2_ptr.HeadBlockId);
                        flags[1] = true;
                        gbl.byte_1EE8D = false;
                    }
                }
            }
        }


        internal static char inflateChar(uint arg_0)
        {
            if (arg_0 <= 0x1f)
            {
                arg_0 += 0x40;
            }

            return (char)arg_0;
        }

        internal static uint deflateChar(char ch)
        {
            uint output = (uint)ch;

            if (output >= 0x40)
            {
                output -= 0x40;
            }
            return output;
        }

        internal static int vm_GetMemoryValueType(ushort arg_0) // sub_30723
        {
            int var_1 = 4;

            if (arg_0 >= 0x4B00 && arg_0 <= 0x4EFF)
            {
                var_1 = 0;
            }

            if (arg_0 >= 0x7C00 && arg_0 <= 0x7FFF)
            {
                var_1 = 1;
            }

            if (arg_0 >= 0x7A00 && arg_0 <= 0x7BFF)
            {
                var_1 = 2;
            }

            if (arg_0 >= 0x8000 && arg_0 <= 0x9DFF)
            {
                var_1 = 3;
            }

            return var_1;
        }


        internal static ushort find_gbl_player_index(Player player)
        {
            int index = gbl.TeamList.IndexOf(player);

            if (index == -1)
                index = gbl.TeamList.Count;

            return (ushort)index;
        }


        internal static ushort get_player_values(ref bool arg_0, ushort arg_4)
        {
            ushort return_val;

            arg_0 = true;

            arg_4 -= 0x7c00;

            if (arg_4 == 0x15)
            {
                return_val = gbl.SelectedPlayer._int;
            }
            else if (arg_4 == 0x18)
            {
                return_val = gbl.SelectedPlayer.con;
            }
            else if (arg_4 == 0x72)
            {
                return_val = (ushort)gbl.SelectedPlayer.race;
            }
            else if (arg_4 == 0x73)
            {
                return_val = (ushort)gbl.SelectedPlayer._class;
            }
            else if (arg_4 == 0x9b)
            {
                return_val = gbl.SelectedPlayer.saveVerse[1];
            }
            else if (arg_4 == 0xa0)
            {
                return_val = gbl.SelectedPlayer.HitDice;
            }
            else if (arg_4 >= 0xA5 && arg_4 <= 0xAC)
            {
                int var_3 = arg_4 - 0xA4;

                return_val = gbl.SelectedPlayer.field_EA[var_3 - 1];
            }
            else if (arg_4 == 0xb8)
            {
                return_val = gbl.SelectedPlayer.control_morale;
            }
            else if (arg_4 == 0xBB)
            {
                return_val = (ushort)gbl.SelectedPlayer.Money.GetCoins(Money.Copper);
            }
            else if (arg_4 == 0xBD)
            {
                return_val = (ushort)gbl.SelectedPlayer.Money.GetCoins(Money.Electrum);
            }
            else if (arg_4 == 0xBF)
            {
                return_val = (ushort)gbl.SelectedPlayer.Money.GetCoins(Money.Silver);
            }
            else if (arg_4 == 0xC1)
            {
                return_val = (ushort)gbl.SelectedPlayer.Money.GetCoins(Money.Gold);
            }
            else if (arg_4 == 0xC3)
            {
                return_val = (ushort)gbl.SelectedPlayer.Money.GetCoins(Money.Platinum);
            }
            else if (arg_4 == 0xC9)
            {
                return_val = (ushort)gbl.SelectedPlayer.SkillLevel(SkillType.MagicUser);
            }
            else if (arg_4 == 0xD6)
            {
                return_val = gbl.SelectedPlayer.sex;
            }
            else if (arg_4 == 0xD8)
            {
                return_val = gbl.SelectedPlayer.alignment;
            }
            else if (arg_4 == 0xE4)
            {
                return_val = (ushort)(gbl.SelectedPlayer.field_192 & 1);
            }
            else if (arg_4 == 0xF7)
            {
                return_val = (ushort)gbl.SelectedPlayer.field_13C;
            }
            else if (arg_4 == 0xF9)
            {
                return_val = gbl.SelectedPlayer.field_13E;
            }
            else if (arg_4 == 0x100)
            {
                if (gbl.SelectedPlayer.in_combat == true)
                {
                    return_val = 1;
                }
                else
                {
                    return_val = 0x80;
                }

                if (gbl.player_not_found == true)
                {
                    return_val = 0;
                }

                gbl.player_not_found = false;
            }
            else if (arg_4 == 0x10C)
            {
                if (gbl.SelectedPlayer.combat_team == CombatTeam.Ours &&
                    gbl.SelectedPlayer.quick_fight == QuickFight.True)
                {
                    return_val = 0x80;
                }
                else if (gbl.SelectedPlayer.combat_team == CombatTeam.Enemy)
                {
                    return_val = 0x81;
                }
                else
                {
                    return_val = 0;
                }
            }
            else if (arg_4 == 0x10D)
            {
                return_val = 0; /* Simeon */
                throw new System.NotImplementedException("Not sure what should happening this case");
                //jmp	func_end
            }
            else if (arg_4 == 0x11B)
            {
                return_val = gbl.SelectedPlayer.movement;
            }
            else if (arg_4 == 0x2B1)
            {
                return_val = find_gbl_player_index(gbl.SelectedPlayer);
            }
            else if (arg_4 == 0x2B4)
            {
                return_val = find_gbl_player_index(gbl.SelectedPlayer);
            }
            else if (arg_4 == 0x2CF)
            {
                switch (gbl.SelectedPlayer.charisma)
                {
                    case 3:
                        return_val = 0;
                        break;

                    case 4:
                        return_val = 5;
                        break;

                    case 5:
                        return_val = 0x0A;
                        break;

                    case 6:
                        return_val = 0x0F;
                        break;

                    case 7:
                        return_val = 0x14;
                        break;

                    case 8:
                    case 9:
                    case 0x0a:
                    case 0x0b:
                    case 0x0c:
                        return_val = 0x19;
                        break;

                    case 0x0d:
                        return_val = 0x1E;
                        break;

                    case 0x0e:
                        return_val = 0x23;
                        break;

                    case 0x0f:
                        return_val = 0x28;
                        break;

                    case 0x10:
                        return_val = 0x32;
                        break;

                    case 0x11:
                        return_val = 0x37;
                        break;

                    case 0x12:
                    case 0x13:
                    case 0x14:
                    case 0x15:
                    case 0x16:
                    case 0x17:
                    case 0x18:
                    case 0x19:
                        return_val = 0x3C;
                        break;

                    default:
                        return_val = 0;
                        break;
                }
            }
            else if (arg_4 == 0x312)
            {
                return_val = gbl.game_area;
            }
            else if (arg_4 == 0x33E)
            {
                return_val = gbl.area2_ptr.party_size;
            }
            else
            {
                return_val = 0; /* value not read if arg_0 is false */
                arg_0 = false;
            }

            return return_val;
        }


        internal static void alter_character(ushort set_value, ushort switch_var)
        {
            switch_var -= 0x7c00;

            if (switch_var == 0)
            {
                if (set_value == 0)
                {
                    gbl.redrawPartySummary2 = true;
                }
            }
            else if (switch_var >= 0x20 && switch_var <= 0x70)
            {
                int var_1 = switch_var - 0x1f;
                Logger.DebugWrite("Set Spell for: {0} slot: {1} to: {2}", gbl.SelectedPlayer, var_1, (byte)set_value);
                gbl.SelectedPlayer.spellList.AddLearnt(set_value & 0x0ff);
                //gbl.SelectedPlayer.spell_list[var_1] = (byte)(set_value);
            }
            else if (switch_var == 0xb8)
            {
                if (set_value > 0xb2)
                {
                    set_value -= 0x32;
                }

                gbl.SelectedPlayer.control_morale = (byte)(set_value);
            }
            else if (switch_var == 0xbb)
            {
                gbl.SelectedPlayer.Money.SetCoins(Money.Copper, set_value);
            }
            else if (switch_var == 0xbd)
            {
                gbl.SelectedPlayer.Money.SetCoins(Money.Electrum, set_value);
            }
            else if (switch_var == 0xbf)
            {
                gbl.SelectedPlayer.Money.SetCoins(Money.Silver, set_value);
            }
            else if (switch_var == 0xc1)
            {
                gbl.SelectedPlayer.Money.SetCoins(Money.Gold, set_value);
            }
            else if (switch_var == 0xc3)
            {
                gbl.SelectedPlayer.Money.SetCoins(Money.Platinum, set_value);
            }
            else if (switch_var == 0xf7)
            {
                gbl.SelectedPlayer.field_13C = (short)(set_value);
            }
            else if (switch_var == 0xf9)
            {
                gbl.SelectedPlayer.field_13E = (byte)(set_value);
            }
            else if (switch_var == 0x100)
            {
                if (set_value >= 0x80)
                {
                    gbl.SelectedPlayer.in_combat = false;
                    if (set_value == 0x87)
                    {
                        gbl.SelectedPlayer.health_status = Status.stoned;
                    }
                }

                if (set_value == 0)
                {
                    gbl.redrawPartySummary1 = true;
                }
            }
            else if (switch_var == 0x10c)
            {
                switch (set_value)
                {
                    case 0:
                        gbl.SelectedPlayer.combat_team = CombatTeam.Ours;
                        gbl.SelectedPlayer.quick_fight = QuickFight.False;
                        break;

                    case 0x80:
                        gbl.SelectedPlayer.combat_team = CombatTeam.Ours;
                        gbl.SelectedPlayer.quick_fight = QuickFight.True;
                        break;

                    case 0x81:
                        gbl.SelectedPlayer.combat_team = CombatTeam.Enemy;
                        gbl.SelectedPlayer.quick_fight = QuickFight.True;
                        break;
                }
            }
            else if (switch_var == 0x312)
            {
                seg042.set_game_area((byte)(set_value));
            }
            else if (switch_var == 0x322)
            {
                if (set_value > 0x80)
                {
                    set_value &= 0x7f;

                    ovr031.LoadWalldef(1, set_value & 0xFF);
                }
            }
            else if (switch_var == 0x324)
            {
                if (set_value > 0x80)
                {
                    set_value &= 0x7f;

                    ovr031.LoadWalldef(2, set_value & 0xFF);
                }
            }
            else if (switch_var == 0x326)
            {
                if (set_value > 0x80)
                {
                    set_value &= 0x7f;

                    ovr031.LoadWalldef(3, set_value & 0xFF);
                }
            }
        }


        internal static void vm_SetMemoryValue(ushort value, ushort location) // cmd_table01
        {
            byte var_2;

            int memType = vm_GetMemoryValueType(location);

            //System.Console.WriteLine("  vm_SetMemoryValue: value: {0:X} loc: {1:X} type: {2:X}",
            //    value, location, memType);

            if (memType == 0)
            {
                if ((location - 0x4B00) == 0x0FD || (location - 0x4B00) == 0x0FE)
                {
                    //System.Console.WriteLine("    gbl.byte_1EE94 = 1");
                    gbl.byte_1EE94 = true;
                }
                else if ((location - 0x4B00) == 0x0E6 && gbl.area_ptr.inDungeon != value)
                {
                    gbl.last_game_state = gbl.game_state;
                    if (value == 0)
                    {
                        gbl.game_state = GameState.WildernessMap;
                    }
                    else
                    {
                        gbl.game_state = GameState.DungeonMap;
                    }
                }

                gbl.area_ptr.field_6A00_Set(0x6A00 + (location * 2), value);
            }
            else if (memType == 1)
            {
                gbl.area2_ptr.field_800_Set((location * 2) + 0x800, value);
                alter_character(value, location);
            }
            else if (memType == 2)
            {
                gbl.stru_1B2CA[(location << 1) + 0x0C00] = value;
            }
            else if (memType == 3)
            {
                gbl.ecl_ptr[location + 0x8000] = (byte)value;
            }
            else if (memType == 4)
            {
                if (location < 0xBF68)
                {
                    switch (location)
                    {
                        case 0xFB:
                            break;

                        case 0xFC:
                            break;

                        case 0xB1:
                            break;

                        case 0x3DE:
                            gbl.word_1EE76 = value;
                            break;

                        case 0xB8:
                            gbl.word_1EE78 = value;
                            break;

                        case 0xB9:
                            gbl.word_1EE7A = value;
                            break;
                    }
                }
                else
                {
                    location -= 0xBF68;

                    switch (location)
                    {
                        case 0xE3:
                            gbl.positionChanged = true;
                            gbl.mapPosX = (sbyte)(value);
                            break;

                        case 0xE4:
                            gbl.mapPosY = (sbyte)(value);
                            gbl.positionChanged = true;
                            break;

                        case 0xE5:
                            do
                            {
                                var_2 = 1;
                                switch (value)
                                {
                                    case 0:
                                        gbl.mapDirection = 0;
                                        break;

                                    case 1:
                                        gbl.mapDirection = 2;
                                        break;

                                    case 2:
                                        gbl.mapDirection = 4;
                                        break;

                                    case 3:
                                        gbl.mapDirection = 6;
                                        break;

                                    default:
                                        var_2 = 0;
                                        value -= 4;
                                        break;
                                }
                            } while (var_2 != 1);

                            gbl.positionChanged = true;
                            break;

                        case 0xF1:
                            gbl.byte_1EE91 = true;
                            break;

                        case 0xF7:
                            gbl.byte_1EE91 = true;
                            break;
                    }
                }
            }
        }


        internal static ushort vm_GetMemoryValue(ushort loc) // sub_30F16
        {
            ushort val = 0;

            int mem_type = vm_GetMemoryValueType(loc);

            switch (mem_type)
            {
                case 0:
                    val = gbl.area_ptr.field_6A00_Get(0x6A00 + (loc * 2));
                    break;

                case 1:
                    bool var_4 = false;
                    val = get_player_values(ref var_4, loc);

                    if (var_4 == false)
                    {
                        val = gbl.area2_ptr.field_800_Get((loc * 2) + 0x800);
                    }
                    break;

                case 2:
                    val = gbl.stru_1B2CA[(loc << 1) + 0x0C00];
                    break;

                case 3:
                    val = gbl.ecl_ptr[loc + 0x8000]; // When does this happen?
                    break;

                case 4:
                    if (loc < 0xC04B)
                    {
                        switch (loc)
                        {
                            case 0x00B1:
                                val = (ushort)gbl.word_1D918;
                                break;

                            case 0x00FB:
                                val = (ushort)gbl.word_1D914;
                                break;

                            case 0x00FC:
                                val = (ushort)gbl.word_1D916;
                                break;

                            case 0x033D:
                                val = gbl.mapDirection;
                                break;

                            case 0x035F:
                                break;
                        }
                    }
                    else
                    {
                        loc -= 0xC04B;

                        switch (loc)
                        {
                            case 0:
                                val = (ushort)gbl.mapPosX;
                                break;

                            case 0x01:
                                val = (ushort)gbl.mapPosY;
                                break;

                            case 0x02:
                                val = (ushort)(gbl.mapDirection / 2);
                                break;

                            case 0x03:
                                val = gbl.mapWallType;
                                break;

                            case 0x04:
                                val = gbl.mapWallRoof;
                                break;

                            case 0x0E:
                                break;
                        }
                    }
                    break;
            }

            return val;
        }


        internal static void vm_WriteStringToMemory(string text, ushort loc) // sub_3105D
        {
            byte var_104;

            int mem_type = vm_GetMemoryValueType(loc);

            int text_len = text.Length;

            //System.Console.WriteLine("  vm_WriteStringToMemory: str: '{0}' loc: {1:X} type: {2:X}",
            //    arg_0, arg_4, var_101);


            if (mem_type == 0)
            {
                if (text_len > 0)
                {
                    var_104 = (byte)(text_len - 1);

                    for (int i = 0; i <= var_104; i++)
                    {
                        gbl.area_ptr.field_6A00_Set(0x6A00 + ((loc + i) * 2), text[i]);
                    }
                }

                gbl.area_ptr.field_6A00_Set(0x6A00 + ((text_len + loc) * 2), 0);
            }
            else if (mem_type == 1)
            {
                if (loc == 0x7C00)
                {
                    gbl.SelectedPlayer.name = text;
                }
                else
                {
                    if (text_len > 0)
                    {
                        for (int i = 0; i <= text_len - 1; i++)
                        {
                            gbl.area2_ptr.field_800_Set(((loc + i) * 2) + 0x800, text[i]);
                        }
                    }

                    gbl.area2_ptr.field_800_Set(((text_len + loc) * 2) + 0x800, 0);
                }
            }
            else if (mem_type == 2)
            {
                if (text_len > 0)
                {
                    var_104 = (byte)(text_len - 1);
                    for (int i = 0; i <= var_104; i++)
                    {
                        gbl.stru_1B2CA[((i + loc) * 2) + 0x0C00] = text[i];
                    }
                }

                gbl.stru_1B2CA[((text_len + loc) * 2) + 0x0C00] = 0;
            }
            else if (mem_type == 3)
            {
                if (text_len > 0)
                {
                    for (int i = 0; i <= text_len - 1; i++)
                    {
                        gbl.ecl_ptr[0x8000 + i + loc] = (byte)text[i];
                    }
                }

                gbl.ecl_ptr[0x8000 + text_len + loc] = 0;
            }
        }

        internal static byte[] compressString(string input)
        {
            byte[] data = new byte[((input.Length * 3) / 4) + 1];
            int state = 1;
            int last = 0;
            int curr = 0;

            foreach (char ch in input)
            {
                uint bits = deflateChar(ch) & 0x3F;
                if (state == 1)
                {
                    data[curr] = (byte)(bits << 2);
                    last = curr++;
                    state = 2;
                }
                else if (state == 2)
                {
                    data[last] |= (byte)(bits >> 4);
                    data[curr] = (byte)(bits << 4);
                    last = curr++;
                    state = 3;
                }
                else if (state == 3)
                {
                    data[last] |= (byte)(bits >> 2);
                    data[curr] = (byte)(bits << 6);
                    last = curr++;
                    state = 4;
                }
                else //if (state == 4)
                {
                    data[last] |= (byte)(bits);
                    state = 1;
                }

            }

            return data;
        }

        internal static string DecompressString(byte[] data)
        {
            var sb = new System.Text.StringBuilder();
            int state = 1;
            uint lastByte = 0;

            foreach (uint thisByte in data)
            {
                uint curr = 0;
                switch (state)
                {
                    case 1:
                        curr = (thisByte >> 2) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 2;
                        break;

                    case 2:
                        curr = ((lastByte << 4) | (thisByte >> 4)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 3;
                        break;

                    case 3:
                        curr = ((lastByte << 2) | (thisByte >> 6)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));

                        curr = thisByte & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 1;
                        break;
                }
                lastByte = thisByte;
            }

            return sb.ToString();
        }

        internal static void LoadCompressedEclString(int strIndex, int inputLength)
        {
            byte[] data = new byte[inputLength];

            for (int i = 0; i < inputLength; i++)
            {
                data[i] = gbl.ecl_ptr[gbl.ecl_offset + 0x8000 + 1 + i];
            }

            gbl.ecl_offset += (ushort)inputLength;

            gbl.unk_1D972[strIndex] = DecompressString(data);
        }


        internal static void vm_CopyStringFromMemory(ushort location, byte strIndex) // sub_31421
        {
            int offset = 0;
            var sb = new System.Text.StringBuilder();

            switch (vm_GetMemoryValueType(location))
            {
                case 0:
                    while (gbl.area_ptr.field_6A00_Get(((offset + location) * 2) + 0x6A00) != 0)
                    {
                        sb.Append((char)((byte)gbl.area_ptr.field_6A00_Get(((offset + location) * 2) + 0x6A00)));
                        offset++;
                    }
                    break;

                case 1:
                    if (location == 0x7C00)
                    {
                        sb.Append(gbl.SelectedPlayer.name);
                    }
                    else
                    {
                        while (gbl.area2_ptr.field_800_Get(((offset + location) << 1) + 0x800) != 0)
                        {
                            sb.Append((char)((byte)gbl.area2_ptr.field_800_Get(((offset + location) << 1) + 0x800)));
                            offset++;
                        }
                    }
                    break;

                case 2:
                    while (gbl.stru_1B2CA[((offset + location) << 1) + 0x0C00] != 0)
                    {
                        sb.Append((char)gbl.stru_1B2CA[((offset + location) << 1) + 0x0C00]);
                        offset++;
                    }
                    break;

                case 3:
                    while (gbl.ecl_ptr[offset + location + 0x8000] != 0)
                    {
                        sb.Append((char)gbl.ecl_ptr[offset + location + 0x8000]);
                        offset++;
                    }
                    break;
            }

            gbl.unk_1D972[strIndex] = sb.ToString();
        }

        static Set unk_31673 = new Set(0x0606, new byte[] { 0xff, 0x03, 0xfe, 0xff, 0xff, 0x07 });

        internal static string buildMenuStrings(ref string MenuString)
        {
            System.Text.StringBuilder sbA = new System.Text.StringBuilder();
            System.Text.StringBuilder sbB = new System.Text.StringBuilder();

            bool mFlag = false;

            for (int i = 0; i < MenuString.Length; i++)
            {
                char ch = MenuString[i];

                if (unk_31673.MemberOf(ch) == true)
                {
                    ch += ' ';
                }

                if (ch == '~')
                {
                    mFlag = true;
                    continue;
                }

                if (mFlag == true)
                {
                    mFlag = false;
                    ch = char.ToUpper(ch);
                    sbA.Append(ch);
                }

                sbB.Append(ch);
            }

            MenuString = sbB.ToString();
            return sbA.ToString();
        }

        static Set validkeys = new Set(0x0606, new byte[] { 0xff, 0x03, 0xfe, 0xff, 0xff, 0x07 }); // unk_3178A

        internal static int sub_317AA(bool useOverlay, bool acceptReturn, MenuColorSet colors, string displayString, string extraString)
        {
            char key_pressed;
            int ret_val;

            string menu_keys = buildMenuStrings(ref displayString);

            do
            {
                bool special_key_pressed;
                key_pressed = ovr027.displayInput(out special_key_pressed, useOverlay, 1, colors, displayString, extraString);

                if (special_key_pressed == true)
                {
                    ovr020.scroll_team_list(key_pressed);
                    ovr025.PartySummary(gbl.SelectedPlayer);
                    key_pressed = '\0';
                }
            } while (validkeys.MemberOf(key_pressed) == false && (key_pressed != '\r' || acceptReturn == false));

            if (key_pressed == '\r')
            {
                ret_val = 0;
            }
            else
            {
                int var_154 = 0;

                while (var_154 < menu_keys.Length && menu_keys[var_154] != key_pressed)
                {
                    var_154++;
                }

                if (var_154 < menu_keys.Length)
                {
                    ret_val = var_154;
                }
                else
                {
                    ret_val = -1;
                }
            }

            return ret_val;
        }


        internal static int VertMenuSelect(int index, bool menuRedraw, bool showExit,
            List<MenuItem> list, sbyte endY, sbyte endX, int startY, sbyte startX)
        {
            MenuItem dummyMenuItem;

            ovr027.sl_select_item(out dummyMenuItem, ref index, ref menuRedraw, showExit, list, endY, endX,
                startY, startX, gbl.defaultMenuColors, string.Empty, string.Empty);

            return index;
        }


        internal static void compare_strings(string string_a, string string_b) /* sub_3193B */
        {
            gbl.compare_flags[0] = (string_b.CompareTo(string_a) == 0);
            gbl.compare_flags[1] = (string_b.CompareTo(string_a) != 0);
            gbl.compare_flags[2] = (string_b.CompareTo(string_a) < 0);
            gbl.compare_flags[3] = (string_b.CompareTo(string_a) > 0);
            gbl.compare_flags[4] = (string_b.CompareTo(string_a) <= 0);
            gbl.compare_flags[5] = (string_b.CompareTo(string_a) >= 0);
        }

        /// <summary>
        /// sets global based of arg_0 and arg_2 relation.
        /// sub_31A11
        /// </summary>
        internal static void compare_variables(ushort arg_0, ushort arg_2) /* sub_31A11 */
        {
            //System.Console.WriteLine("  Compare_variables: {0} {1}", arg_2, arg_0);

            gbl.compare_flags[0] = arg_2 == arg_0;
            gbl.compare_flags[1] = arg_2 != arg_0;
            gbl.compare_flags[2] = arg_2 < arg_0;
            gbl.compare_flags[3] = arg_2 > arg_0;
            gbl.compare_flags[4] = arg_2 <= arg_0;
            gbl.compare_flags[5] = arg_2 >= arg_0;
        }


        internal static void MovePositionForward() // sub_31B01
        {
            if (gbl.mapDirection == 0)
            {
                gbl.mapPosY = DecrimentWrap(gbl.mapPosY, 15);
            }
            else if (gbl.mapDirection == 2)
            {
                gbl.mapPosX = IncrementWrap(gbl.mapPosX, 15);
            }
            else if (gbl.mapDirection == 4)
            {
                gbl.mapPosY = IncrementWrap(gbl.mapPosY, 15);
            }
            else if (gbl.mapDirection == 6)
            {
                gbl.mapPosX = DecrimentWrap(gbl.mapPosX, 15);
            }

            gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);
            gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            gbl.positionChanged = true;
        }

        static int DecrimentWrap(int value, int max)
        {
            if (value > 0)
            {
                return value - 1;
            }
            else
            {
                return max;
            }
        }

        static int IncrementWrap(int value, int max)
        {
            if (value < max)
            {
                return value + 1;
            }
            else
            {
                return 0;
            }
        }

        internal static void SetupDuel(bool isDuel)
        {
            gbl.combat_type = CombatType.duel;

            gbl.area2_ptr.isDuel = isDuel;
            Player dueler = gbl.SelectedPlayer;

            foreach (Player player in gbl.TeamList)
            {
                if (player.name != dueler.name)
                {
                    player.in_combat = false;
                }
            }

            if (isDuel)
            {
                ovr034.chead_cbody_comspr_icon(gbl.monster_icon_id, 11, "CPIC");

                Player DuelMaster = dueler.ShallowClone();
                DuelMaster.in_combat = true;
                DuelMaster.name = "ROLF";
                DuelMaster.quick_fight = QuickFight.True;
                DuelMaster.combat_team = CombatTeam.Enemy;

                DuelMaster.control_morale = Control.NPC_Berzerk;
                DuelMaster.icon_id = gbl.monster_icon_id;

                DuelMaster.affects = new List<Affect>();
                DuelMaster.items = new List<Item>();

                gbl.TeamList.Add(DuelMaster);

                foreach (Item item in dueler.items)
                {
                    DuelMaster.items.Add(item.ShallowClone());
                }
            }
        }


        internal static void RobMoney(Player player, double scale) /* sub_31DEF */
        {
            player.Money.ScaleAll(scale);
        }


        internal static void RobItems(Player player, int arg_4) /* sub_31F1C */
        {
            player.items.RemoveAll(item =>
            {
                if (item.weight > 255)
                {
                    arg_4 = (arg_4 > 90) ? arg_4 - 90 : 0;
                }
                else if (item.weight > 24)
                {
                    arg_4 = (arg_4 > 50) ? arg_4 - 50 : 0;
                }

                return (ovr024.roll_dice(100, 1) <= arg_4);
            });
        }


        internal static void calc_group_movement(out byte mov_min, out byte mov_max) /* calc_group_inituative */
        {
            mov_max = byte.MinValue;
            mov_min = byte.MaxValue;

            foreach (Player player in gbl.TeamList)
            {
                byte movement = player.movement;

                if (player.HasAffect(Affects.haste) == true)
                {
                    movement *= 2;
                }
                else if (player.HasAffect(Affects.slow) == true)
                {
                    movement /= 2;
                }

                if (movement > mov_max)
                {
                    mov_max = movement;
                }

                if (movement < mov_min)
                {
                    mov_min = movement;
                }
            }
        }


        internal static void sub_32200(Player player, int damage) /* sub_32200 */
        {
            if (player.health_status != Status.dead)
            {
                string text;
                bool clear_text_area = false;

                if ((player.hit_point_current + 10) < damage)
                {
                    text = string.Format("  {0} dies. ", player.name);
                }
                else
                {
                    text = string.Format("  {0} is hit FOR {1} points of Damage.", player.name, damage);
                }

                if (gbl.textYCol > 0x16)
                {
                    gbl.textYCol = 0x11;
                    clear_text_area = true;
                    seg041.DisplayAndPause("press <enter>/<return> to continue", 0, 15);
                }
                else
                {
                    clear_text_area = false;
                }

                gbl.textXCol = 0x26;

                seg041.press_any_key(text, clear_text_area, 0, 15, 0x16, 0x26, 17, 1);

                ovr025.damage_player(damage, player);
                seg037.draw8x8_clear_area(0x0f, 0x26, 1, 0x11);

                ovr025.PartySummary(player);
            }
        }
    }
}
