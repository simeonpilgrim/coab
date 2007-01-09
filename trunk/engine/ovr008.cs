using Classes;

namespace engine
{
    class ovr008
    {
        internal static void vm_LoadCmdSets(byte numberOfSets) // parse_command_sub
        {
            byte strIndex;
            byte loop_var;

            strIndex = 0;
            System.Console.WriteLine("  vm_LoadCmdSets: {0}", numberOfSets);

            foreach (Opperation opp in gbl.cmd_opps)
            {
                opp.Clear();
            }

            for (loop_var = 1; loop_var <= numberOfSets; loop_var++)
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

                    System.Console.WriteLine("   code: {0,2:X} low: {1,2:X} high: {2,2:X}",
                        code, low, high);
                }
                else if (code == 0x80) // Load compressed string
                {
                    strIndex++;

                    short strLen = low;

                    if (strLen > 0)
                    {
                        decompressString(strIndex, (byte)strLen);
                    }
                    else
                    {
                        gbl.unk_1D972[strIndex] = string.Empty;
                    }

                    System.Console.WriteLine("   code: {0,2:X} strIndex: {1} strLen: {2} str: '{3}'",
                        code, strIndex, strLen, gbl.unk_1D972[strIndex]);
                }
                else if (code == 0x81)
                {
                    strIndex++;
                    gbl.ecl_offset++;
                    byte high = gbl.ecl_ptr[0x8000 + gbl.ecl_offset];

                    gbl.cmd_opps[loop_var].High = high;

                    ushort loc = gbl.cmd_opps[loop_var].Word;

                    vm_CopyStringFromMemory(loc, strIndex);

                    System.Console.WriteLine("   code: {0,2:X} strIndex: {1} loc: {2} str: '{3}'",
                        code, strIndex, loc, gbl.unk_1D972[strIndex]);
                }
                else
                {
                    System.Console.WriteLine("   code: {0,2:X} low: 0x{1,2:X}",
                        code, low);
                }
            }

            gbl.ecl_offset++;
        }

        internal static ushort vm_GetCmdValue(int arg_0) // sub_30168
        {
            return gbl.cmd_opps[arg_0].GetCmdValue();
            //TODO replace vm_GetCmdValue function with gbl.cmd_opps[arg_0].GetCmdValue();
        }


        internal static void vm_init_ecl() // sub_301E8
        {
            gbl.byte_1EE8C = 0;
            gbl.byte_1EE8E = 0;
            gbl.byte_1EE7C = 0;
            gbl.byte_1EE7D = 0;
            gbl.byte_1D912 = 0x41;
            gbl.byte_1D913 = 9;
            gbl.byte_1EE91 = 1;

            seg051.FillChar(0, 2, gbl.byte_1EE72);
            gbl.byte_1D92D = 8;
            gbl.ecl_offset = 0x8000;
            gbl.byte_1DA70 = false;

            gbl.vmCallStack.Clear();

            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            gbl.area2_ptr.field_5C2 = 0xFF;

            gbl.area2_ptr.field_5A4 = 0;
            gbl.area2_ptr.field_5A6 = 0;
            gbl.area_ptr.can_cast_spells = false;

            vm_LoadCmdSets(1);
            gbl.word_1B2D3 = gbl.cmd_opps[1].Word;

            vm_LoadCmdSets(1);
            gbl.word_1B2D5 = gbl.cmd_opps[1].Word;

            vm_LoadCmdSets(1);
            gbl.word_1B2D7 = gbl.cmd_opps[1].Word;

            vm_LoadCmdSets(1);
            gbl.word_1B2D9 = gbl.cmd_opps[1].Word;

            vm_LoadCmdSets(1);
            gbl.ecl_initial_entryPoint = gbl.cmd_opps[1].Word;

            gbl.area_ptr.field_1CC = 1;

            if (gbl.byte_1B2EB == 0)
            {
                for (int loop_var = 1; loop_var <= 32; loop_var++)
                {
                    gbl.area_ptr.field_200[loop_var] = 0; // word array.
                }

                for (int loop_var = 0; loop_var <= 9; loop_var++)
                {
                    gbl.area2_ptr.field_6F2[loop_var] = 0; // word array.
                }
            }
        }


        internal static void load_ecl_dax(byte block_id)
        {
            byte[] block_mem;
            short block_size = 0;

            gbl.ecl_ptr.Clear();
            gbl.byte_1C01A = 0;

            do
            {
                ovr027.redraw_screen();
                seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);

                seg042.load_decode_dax(out block_mem, out block_size, block_id, string.Format("ECL{0}.dax", gbl.game_area));
            } while (block_size < 2);

            gbl.ecl_ptr.SetData(block_mem, 2, block_size - 2);

            ovr027.redraw_screen();
        }

        internal static void load_mob(out Affect affect, out Item item, out Player player, byte mod_id)
        {
            player = null;

            player = new Player();

            ovr017.load_mob(player, mod_id);

            item = player.itemsPtr;
            affect = player.affect_ptr;
        }


        internal static byte sub_304B4(byte arg_0, sbyte arg_2, sbyte arg_4)
        {
            byte var_3;
            byte var_2;
            byte var_1;

            var_3 = 0;
            var_2 = 0;
            var_1 = 0;

            if (gbl.area_ptr.field_1CC == 0)
            {
                var_1 = 2;
                gbl.area2_ptr.field_582 = 2;
            }
            else
            {
                while (var_3 < 2 && var_2 == 0)
                {

                    if (ovr031.sub_716A2(arg_0, arg_2, arg_4) == 0)
                    {
                        var_3++;
                        var_1 = var_3;

                        switch (arg_0)
                        {
                            case 0:
                                arg_2--;
                                break;

                            case 2:
                                arg_4++;
                                break;

                            case 4:
                                arg_2++;
                                break;

                            case 6:
                                arg_4--;
                                break;
                        }
                    }
                    else
                    {
                        var_2 = 1;
                    }
                }
            }

            return var_1;
        }


        internal static void sub_30543(byte arg_0, byte arg_2)
        {
            gbl.byte_1EE8D = 0;

            gbl.byte_1B2EE = arg_2;
            gbl.byte_1B2EF = arg_0;

            ovr030.head_body(arg_0, arg_2);
            ovr030.sub_706DC(true, 3, 3);
        }


        internal static void sub_30580(byte[] arg_0, ushort arg_4, byte arg_6, byte arg_8)
        {
            byte var_9;

            var_9 = (byte)(arg_4 + 1);

            if (arg_0[1] == 0)
            {

                if (arg_0[0] == 0)
                {
                    if (gbl.byte_1D538 == true)
                    {
                        gbl.byte_1D538 = false;
                        gbl.byte_1D8AA = 1;
                        ovr029.sub_6F0BA();
                    }

                    if (gbl.area_ptr.field_1CC != 0)
                    {
                        ovr030.load_pic_final(ref gbl.byte_1D556, 1, arg_8, "SPRIT");
                        arg_0[0] = 1;
                        gbl.byte_1EE8F = 1;
                    }
                }
                else
                {
                    gbl.byte_1D8AA = 1;
                    ovr029.sub_6F0BA();
                }

                if (gbl.game_state == 4)
                {
                    ovr030.Show3DSprite(gbl.byte_1D556, var_9);
                }
            }

            if (arg_0[1] == 0 ||
                gbl.byte_1EE96 != gbl.area2_ptr.field_5C2)
            {
                if (arg_4 == 0)
                {
                    if (gbl.game_state == 4)
                    {
                        if (gbl.byte_1EE95 == 0 ||
                            gbl.byte_1B2E9 != 0)
                        {
                            gbl.byte_1EE96 = (byte)gbl.area2_ptr.field_5C2;
                            gbl.byte_1EE8C = 1;
                            if (gbl.area2_ptr.field_5C2 == 0xff)
                            {

                                ovr030.load_pic_final(ref gbl.byte_1D556, 0, arg_6, "PIC");
                                arg_0[1] = 1;

                                ovr030.sub_7000A(gbl.dword_1D55C, 1, 3, 3);
                            }
                            else
                            {
                                sub_30543(arg_6, (byte)gbl.area2_ptr.field_5C2);
                                arg_0[1] = 1;
                                gbl.byte_1EE8D = 0;
                            }
                        }
                    }
                }
            }
        }


        internal static ushort bytes_to_word(byte high_byte, byte low_byte)
        {
            ushort word;

            word = (ushort)(low_byte + (high_byte << 8));

            return word;
        }


        internal static char inflateChar(int arg_0)
        {
            if (arg_0 <= 0x1f)
            {
                arg_0 += 0x40;
            }

            return (char)arg_0;
        }


        internal static byte vm_GetMemoryValueType(ushort arg_0) // sub_30723
        {
            byte var_1 = 4;

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


        internal static byte find_gbl_player_index(Player arg_0)
        {
            Player var_6;
            byte var_2;


            var_6 = gbl.player_next_ptr;
            bool plyr_found = false;
            var_2 = 0;

            while (var_6 != null && plyr_found == false)
            {
                if (var_6 == arg_0)
                {
                    plyr_found = true;
                }
                else
                {
                    var_2++;
                    var_6 = var_6.next_player;
                }
            }

            return var_2;
        }


        internal static ushort get_player_values(ref bool arg_0, ushort arg_4)
        {
            ushort return_val;

            arg_0 = true;

            arg_4 -= 0x7c00;

            if (arg_4 == 0x15)
            {
                return_val = gbl.player_ptr._int;
            }
            else if (arg_4 == 0x18)
            {
                return_val = gbl.player_ptr.con;
            }
            else if (arg_4 == 0x72)
            {
                return_val = (ushort)gbl.player_ptr.race;
            }
            else if (arg_4 == 0x73)
            {
                return_val = (ushort)gbl.player_ptr._class;
            }
            else if (arg_4 == 0x9b)
            {
                return_val = gbl.player_ptr.field_E0;
            }
            else if (arg_4 == 0xa0)
            {
                return_val = gbl.player_ptr.field_E5;
            }
            else if (arg_4 >= 0xA5 && arg_4 <= 0xAC)
            {
                int var_3 = arg_4 - 0xA4;

                return_val = gbl.player_ptr.field_EA[var_3 - 1];
            }
            else if (arg_4 == 0xb8)
            {
                return_val = gbl.player_ptr.field_F7;
            }
            else if (arg_4 == 0xBB)
            {
                return_val = (ushort)gbl.player_ptr.copper;
            }
            else if (arg_4 == 0xBD)
            {
                return_val = (ushort)gbl.player_ptr.electrum;
            }
            else if (arg_4 == 0xBF)
            {
                return_val = (ushort)gbl.player_ptr.silver;
            }
            else if (arg_4 == 0xC1)
            {
                return_val = (ushort)gbl.player_ptr.gold;
            }
            else if (arg_4 == 0xC3)
            {
                return_val = (ushort)gbl.player_ptr.platinum;
            }
            else if (arg_4 == 0xC9)
            {
                return_val = (ushort)(gbl.player_ptr.magic_user_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_116));
            }
            else if (arg_4 == 0xD6)
            {
                return_val = gbl.player_ptr.sex;
            }
            else if (arg_4 == 0xD8)
            {
                return_val = gbl.player_ptr.alignment;
            }
            else if (arg_4 == 0xE4)
            {
                return_val = (ushort)(gbl.player_ptr.field_192 & 1);
            }
            else if (arg_4 == 0xF7)
            {
                return_val = (ushort)gbl.player_ptr.field_13C;
            }
            else if (arg_4 == 0xF9)
            {
                return_val = gbl.player_ptr.field_13E;
            }
            else if (arg_4 == 0x100)
            {
                if (gbl.player_ptr.in_combat == true)
                {
                    return_val = 1;
                }
                else
                {
                    return_val = 0x80;
                }

                if (gbl.byte_1EE97 == 1)
                {
                    return_val = 0;
                }

                gbl.byte_1EE97 = 0;
            }
            else if (arg_4 == 0x10C)
            {
                if (gbl.player_ptr.combat_team == 0 &&
                    gbl.player_ptr.field_198 != 0)
                {
                    return_val = 0x80;
                }
                else if (gbl.player_ptr.combat_team == 1)
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
                throw new System.NotSupportedException();//jmp	func_end
            }
            else if (arg_4 == 0x11B)
            {
                return_val = gbl.player_ptr.initiative;
            }
            else if (arg_4 == 0x2B1)
            {
                return_val = find_gbl_player_index(gbl.player_ptr);
            }
            else if (arg_4 == 0x2B4)
            {
                return_val = find_gbl_player_index(gbl.player_ptr);
            }
            else if (arg_4 == 0x2CF)
            {
                throw new System.NotSupportedException();//loc_30A88:
                throw new System.NotSupportedException();//les	di, dword ptr player_ptr.offset
                throw new System.NotSupportedException();//mov	al, es:[di+1Bh]
                throw new System.NotSupportedException();//cmp	al, 3
                throw new System.NotSupportedException();//jnz	loc_30A9C
                return_val = 0;
                throw new System.NotSupportedException();//jmp	loc_30B1B
                throw new System.NotSupportedException();//loc_30A9C:
                throw new System.NotSupportedException();//cmp	al, 4
                throw new System.NotSupportedException();//jnz	loc_30AA7
                return_val = 5;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AA7:
                throw new System.NotSupportedException();//cmp	al, 5
                throw new System.NotSupportedException();//jnz	loc_30AB2
                return_val = 0x0A;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AB2:
                throw new System.NotSupportedException();//cmp	al, 6
                throw new System.NotSupportedException();//jnz	loc_30ABD
                return_val = 0x0F;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30ABD:
                throw new System.NotSupportedException();//cmp	al, 7
                throw new System.NotSupportedException();//jnz	loc_30AC8
                return_val = 0x14;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AC8:
                throw new System.NotSupportedException();//cmp	al, 8
                throw new System.NotSupportedException();//jb	loc_30AD7
                throw new System.NotSupportedException();//cmp	al, 0x0C
                throw new System.NotSupportedException();//ja	loc_30AD7
                return_val = 0x19;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AD7:
                throw new System.NotSupportedException();//cmp	al, 0x0D
                throw new System.NotSupportedException();//jnz	loc_30AE2
                return_val = 0x1E;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AE2:
                throw new System.NotSupportedException();//cmp	al, 0x0E
                throw new System.NotSupportedException();//jnz	loc_30AED
                return_val = 0x23;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AED:
                throw new System.NotSupportedException();//cmp	al, 0x0F
                throw new System.NotSupportedException();//jnz	loc_30AF8
                return_val = 0x28;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30AF8:
                throw new System.NotSupportedException();//cmp	al, 0x10
                throw new System.NotSupportedException();//jnz	loc_30B03
                return_val = 0x32;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30B03:
                throw new System.NotSupportedException();//cmp	al, 0x11
                throw new System.NotSupportedException();//jnz	loc_30B0E
                return_val = 0x37;
                throw new System.NotSupportedException();//jmp	short loc_30B1B
                throw new System.NotSupportedException();//loc_30B0E:
                throw new System.NotSupportedException();//cmp	al, 0x12
                throw new System.NotSupportedException();//jb	loc_30B1B
                throw new System.NotSupportedException();//cmp	al, 0x19
                throw new System.NotSupportedException();//ja	loc_30B1B
                return_val = 0x3C;
                throw new System.NotSupportedException();//loc_30B1B:
            }
            else if (arg_4 == 0x312)
            {
                return_val = gbl.game_area;
            }
            else if (arg_4 == 0x33E)
            {
                return_val = gbl.area2_ptr.field_67C;
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
                    gbl.byte_1EE7D = 1;
                }
            }
            else if (switch_var >= 0x20 && switch_var <= 0x70)
            {
                int var_1 = switch_var - 0x1f;

                gbl.player_ptr.spell_list[var_1] = (byte)(set_value);
            }
            else if (switch_var == 0xb8)
            {
                if (set_value > 0xb2)
                {
                    set_value -= 0x32;
                }

                gbl.player_ptr.field_F7 = (byte)(set_value);
            }
            else if (switch_var == 0xbb)
            {
                gbl.player_ptr.copper = (short)(set_value);
            }
            else if (switch_var == 0xbd)
            {
                gbl.player_ptr.electrum = (short)(set_value);
            }
            else if (switch_var == 0xbf)
            {
                gbl.player_ptr.silver = (short)(set_value);
            }
            else if (switch_var == 0xc1)
            {
                gbl.player_ptr.gold = (short)(set_value);
            }
            else if (switch_var == 0xc3)
            {
                gbl.player_ptr.platinum = (short)(set_value);
            }
            else if (switch_var == 0xf7)
            {
                gbl.player_ptr.field_13C = (short)(set_value);
            }
            else if (switch_var == 0xf9)
            {
                gbl.player_ptr.field_13E = (byte)(set_value);
            }
            else if (switch_var == 0x100)
            {
                if (set_value >= 0x80)
                {
                    gbl.player_ptr.in_combat = false;
                    if (set_value == 0x87)
                    {
                        gbl.player_ptr.health_status = Status.stoned;
                    }
                }

                if (set_value == 0)
                {
                    gbl.byte_1EE7C = 1;
                }
            }
            else if (switch_var == 0x10c)
            {
                switch (set_value)
                {
                    case 0:
                        gbl.player_ptr.combat_team = 0;
                        gbl.player_ptr.field_198 = 0;
                        break;

                    case 0x80:
                        gbl.player_ptr.combat_team = 0;
                        gbl.player_ptr.field_198 = 1;
                        break;

                    case 0x81:
                        gbl.player_ptr.combat_team = 1;
                        gbl.player_ptr.field_198 = 1;
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

                    ovr031.LoadWalldef(1, (byte)(set_value));
                }
            }
            else if (switch_var == 0x324)
            {
                if (set_value > 0x80)
                {
                    set_value &= 0x7f;

                    ovr031.LoadWalldef(2, (byte)(set_value));
                }
            }
            else if (switch_var == 0x326)
            {
                if (set_value > 0x80)
                {
                    set_value &= 0x7f;

                    ovr031.LoadWalldef(3, (byte)(set_value));
                }
            }
        }


        internal static void vm_SetMemoryValue(ushort value, ushort location) // cmd_table01
        {
            byte var_2;
            byte memType;

            memType = vm_GetMemoryValueType(location);

            System.Console.WriteLine("  vm_SetMemoryValue: value: {0:X} loc: {1:X} type: {2:X}",
                value, location, memType);

            if (memType == 0)
            {
                if ((location - 0x4B00) == 0x0FD || (location - 0x4B00) == 0x0FE)
                {
                    System.Console.WriteLine("    gbl.byte_1EE94 = 1");
                    gbl.byte_1EE94 = 1;
                }
                else if ((location - 0x4B00) == 0x0E6 && gbl.area_ptr.field_1CC != value)
                {
                    gbl.last_game_state = gbl.game_state;
                    if (value == 0)
                    {
                        System.Console.WriteLine("    gbl.game_state = 3");
                        gbl.game_state = 3;
                    }
                    else
                    {
                        System.Console.WriteLine("    gbl.game_state = 4");
                        gbl.game_state = 4;
                    }
                }

                gbl.area_ptr.field_6A00_Set((location*2) + 0x6a00, value);
            }
            else if (memType == 1)
            {
                gbl.area2_ptr.field_800_Set((location * 2) + 0x800, value);
                alter_character(value, location);
            }
            else if (memType == 2)
            {
                
                gbl.stru_1B2CA[(location<<1)+0x0C00] = value;
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
                            gbl.byte_1EE92 = 1;
                            gbl.byte_1D539 = (sbyte)(value);
                            break;

                        case 0xE4:
                            gbl.byte_1D53A = (sbyte)(value);
                            gbl.byte_1EE92 = 1;
                            break;

                        case 0xE5:
                            do
                            {
                                var_2 = 1;
                                switch (value)
                                {
                                    case 0:
                                        gbl.byte_1D53B = 0;
                                        break;

                                    case 1:
                                        gbl.byte_1D53B = 2;
                                        break;

                                    case 2:
                                        gbl.byte_1D53B = 4;
                                        break;

                                    case 3:
                                        gbl.byte_1D53B = 6;
                                        break;

                                    default:
                                        var_2 = 0;
                                        value -= 4;
                                        break;
                                }
                            } while (var_2 != 1);

                            gbl.byte_1EE92 = 1;
                            break;

                        case 0xF1:
                            gbl.byte_1D912 = (byte)(value);
                            gbl.byte_1EE91 = 1;
                            break;

                        case 0xF7:
                            gbl.byte_1D913 = (byte)(value);
                            gbl.byte_1EE91 = 1;
                            break;
                    }
                }
            }
        }


        internal static ushort vm_GetMemoryValue(ushort arg_0) // sub_30F16
        {
            bool var_4;
            byte var_3;
            ushort var_2 = 0;

            var_3 = vm_GetMemoryValueType(arg_0);

            var_4 = false;

            switch (var_3)
            {
                case 0:
                    var_2 = gbl.area_ptr.field_6A00_Get((arg_0 * 2)+0x6A00);
                    break;

                case 1:
                    var_2 = get_player_values(ref var_4, arg_0);

                    if (var_4 == false)
                    {
                        var_2 = gbl.area2_ptr.field_800_Get((arg_0 * 2) + 0x800);
                    }
                    break;

                case 2:
                    var_2 = gbl.stru_1B2CA[(arg_0 << 1) + 0x0C00];
                    break;

                case 3:
                    var_2 = gbl.ecl_ptr[arg_0 + 0x8000]; // When does this happen?
                    break;

                case 4:
                    if (arg_0 < 0xC04B)
                    {
                        switch (arg_0)
                        {
                            case 0x00B1:
                                var_2 = (ushort)gbl.word_1D918;
                                break;

                            case 0x00FB:
                                var_2 = (ushort)gbl.word_1D914;
                                break;

                            case 0x00FC:
                                var_2 = (ushort)gbl.word_1D916;
                                break;

                            case 0x033D:
                                var_2 = gbl.byte_1D53B;
                                break;

                            case 0x035F:
                                break;
                        }
                    }
                    else
                    {
                        arg_0 -= 0xC04B;

                        switch (arg_0)
                        {
                            case 0:
                                var_2 = (ushort)gbl.byte_1D539;
                                break;

                            case 0x01:
                                var_2 = (ushort)((short)gbl.byte_1D53A);
                                break;

                            case 0x02:
                                var_2 = gbl.byte_1D53B;
                                break;

                            case 0x03:
                                var_2 = gbl.byte_1D53C;
                                break;

                            case 0x04:
                                var_2 = gbl.byte_1D53D;
                                break;

                            case 0x0E:
                                break;
                        }
                    }
                    break;


            }

            return var_2;
        }


        internal static void vm_WriteStringToMemory(string arg_0, ushort arg_4) // sub_3105D
        {
            byte var_104;
            byte var_103;
            byte var_102;
            byte var_101;
            string var_100;

            var_100 = arg_0;

            var_101 = vm_GetMemoryValueType(arg_4);

            var_102 = (byte)var_100.Length;

            System.Console.WriteLine("  vm_WriteStringToMemory: str: '{0}' loc: {1:X} type: {2:X}",
                arg_0, arg_4, var_101);


            if (var_101 == 0)
            {
                if (var_102 > 0)
                {
                    var_104 = (byte)(var_102 - 1);

                    for (var_103 = 0; var_103 <= var_104; var_103++)
                    {
                        ushort dx = var_100[var_103];

                        ushort ax = (ushort)((var_103 + arg_4) << 1);

                        gbl.area_ptr.field_6A00_Set(0x6A00 + ax, dx);
                    }
                }

                gbl.area_ptr.field_6A00_Set(0x6A00 + ((var_102 + arg_4) << 1), 0);
            }
            else if (var_101 == 1)
            {
                throw new System.NotSupportedException();//cmp	[bp+arg_4], 0x7C00
                throw new System.NotSupportedException();//jnz	loc_31128
                gbl.player_ptr.name = var_100;
                throw new System.NotSupportedException();//jmp	short loc_31195
                throw new System.NotSupportedException();//loc_31128:
                throw new System.NotSupportedException();//cmp	[bp+var_102], 0
                throw new System.NotSupportedException();//jbe	loc_3117D
                var_104 = (byte)(var_102 - 1);
                for (var_103 = 0; var_103 <= var_104; var_103++)
                {
                    throw new System.NotSupportedException();//mov	al, [bp+var_103]
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//inc	ax
                    throw new System.NotSupportedException();//mov	di, ax
                    throw new System.NotSupportedException();//mov	al, [bp+di+var_100]
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//mov	dx, ax
                    throw new System.NotSupportedException();//mov	al, [bp+var_103]
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//add	ax, [bp+arg_4]
                    throw new System.NotSupportedException();//shl	ax, 1
                    throw new System.NotSupportedException();//les	di, dword ptr area2_ptr.offset
                    throw new System.NotSupportedException();//add	di, ax
                    throw new System.NotSupportedException();//mov	es:[di+800h], dx
                }
                throw new System.NotSupportedException();//loc_3117D:
                throw new System.NotSupportedException();//mov	al, [bp+var_102]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, [bp+arg_4]
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//les	di, dword ptr area2_ptr.offset
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//xor	ax, ax
                throw new System.NotSupportedException();//mov	es:[di+800h], ax
                throw new System.NotSupportedException();//loc_31195:
            }
            else if (var_101 == 2)
            {
                if (var_102 > 0)
                {
                    var_104 = (byte)(var_102 - 1);
                    for (var_103 = 0; var_103 <= var_104; var_103++)
                    {
                        gbl.stru_1B2CA[((var_103 + arg_4) << 1) + 0x0C00] = var_100[var_103];
                    }
                }

                gbl.stru_1B2CA[((var_102 + arg_4) << 1) + 0x0C00] = 0;
            }
            else if (var_101 == 3)
            {
                if (var_102 > 0)
                {
                    var_104 = (byte)(var_102 - 1);

                    for (var_103 = 0; var_103 <= var_104; var_103++)
                    {
                        throw new System.NotSupportedException();//mov	al, [bp+var_103]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//inc	ax
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	dl, [bp+di+var_100]
                        throw new System.NotSupportedException();//mov	al, [bp+var_103]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//add	ax, [bp+arg_4]
                        throw new System.NotSupportedException();//les	di, dword ptr ecl_ptr.offset
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+8000h], dl
                    }
                }

                throw new System.NotSupportedException();//mov	al, [bp+var_102]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, [bp+arg_4]
                throw new System.NotSupportedException();//les	di, dword ptr ecl_ptr.offset
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	byte ptr es:[di+8000h],	0
            }
        }


        internal static void decompressString(byte strIndex, byte inputLength)
        {
            int var_5 = 0; /* Simeon */
            byte lastByte = 0; /* Simeon */
            byte thisByte = 0; /* Simeon */
            byte inputConsumed;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            inputConsumed = 0;
            int state = 1;

            while (inputConsumed < inputLength)
            {
                if (state < 4)
                {
                    lastByte = thisByte;
                    gbl.ecl_offset++;
                    thisByte = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];
                    inputConsumed++;
                }

                switch (state)
                {
                    case 1:
                        var_5 = thisByte >> 2;
                        break;

                    case 2:
                        var_5 = (lastByte << 4) | (thisByte >> 4);
                        break;

                    case 3:
                        var_5 = (lastByte << 2) | (thisByte >> 6);
                        break;

                    case 4:
                        var_5 = thisByte;
                        break;
                }

                var_5 &= 0x3F;

                state = (state % 4) + 1;

                if (var_5 != 0)
                {
                    char ch = inflateChar(var_5);
                    sb.Append(ch);
                }
            }

            if (state == 4)
            {
                var_5 = thisByte & 0x3F;

                if (var_5 != 0)
                {
                    char ch = inflateChar(var_5);
                    sb.Append(ch);
                }
            }

            gbl.unk_1D972[strIndex] = sb.ToString();
        }


        internal static void vm_CopyStringFromMemory(ushort location, byte strIndex) // sub_31421
        {
            byte var_2 = 0;

            gbl.unk_1D972[strIndex] = string.Empty;

            switch (vm_GetMemoryValueType(location))
            {
                case 0:
                    while (gbl.area_ptr.field_6A00_Get(((var_2 + location) << 1) + 0x6A00) != 0)
                    {
                        gbl.unk_1D972[strIndex] += (char)((byte)gbl.area_ptr.field_6A00_Get(((var_2 + location) << 1) + 0x6A00));
                        var_2++;
                    }
                    break;

                case 1:
                    if (location == 0x7C00)
                    {
                        gbl.unk_1D972[strIndex] = gbl.player_ptr.name;
                    }
                    else
                    {
                        while (gbl.area2_ptr.field_800_Get((var_2 + location) << 1) != 0)
                        {
                            gbl.unk_1D972[strIndex] += gbl.area2_ptr.field_800_Get((var_2 + location) << 1).ToString();
                            var_2++;
                        }
                    }
                    break;

                case 2:
                    while (gbl.stru_1B2CA.field_C00((var_2 + location) << 1) != 0)
                    {
                        gbl.unk_1D972[strIndex] += gbl.stru_1B2CA.field_C00((var_2 + location) << 1).ToString();
                        var_2++;
                    }
                    break;

                case 3:
                    while (gbl.ecl_ptr[var_2 + location + 0x8000] != 0)
                    {
                        gbl.unk_1D972[strIndex] += gbl.ecl_ptr[var_2 + location + 0x8000].ToString();
                        var_2++;
                    }
                    break;
            }
        }

        static Set unk_31673 = new Set(0x0606, new byte[] { 0xff, 0x03, 0xfe, 0xff, 0xff, 0x07 });

        internal static void buildMenuStrings(out string MenuKeys, ref string MenuString)
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
                    sbA.Append(ch);
                    ch = char.ToUpper(ch);
                }

                sbB.Append(ch);
            }

            MenuKeys = sbA.ToString();
            MenuString = sbB.ToString();
        }

        static Set unk_3178A = new Set(0x0606, new byte[] { 0xff, 0x03, 0xfe, 0xff, 0xff, 0x07 });

        internal static byte sub_317AA(byte arg_0, byte arg_2, byte arg_4, byte arg_6, 
			byte arg_8, string arg_A, string arg_E)
        {
            bool var_156;
            char var_155;
            byte var_154;
            string var_153;
            byte ret_val;

            buildMenuStrings(out var_153, ref arg_A);

            do
            {
                var_155 = ovr027.displayInput(out var_156, arg_0, 1, arg_4, arg_6, arg_8, arg_A, arg_E);

                if (var_156 == true)
                {
                    ovr020.sub_572CF(var_155);
                    ovr025.Player_Summary(gbl.player_ptr);
                    var_155 = '\0';
                }

            } while (unk_3178A.MemberOf(var_155) == false && (var_155 != 0x0d || arg_2 == 0));

            if (var_155 == 0x0d)
            {
                ret_val = 0;
            }
            else
            {
                var_154 = 0;

                while (var_154 < var_153.Length && var_153[var_154] != var_155)
                {
                    var_154++;
                }

                if (var_154 < var_153.Length)
                {
                    ret_val = var_154;
                }
                else
                {
                    ret_val = 0xff;
                }
            }

            return ret_val;
        }


        internal static void sub_318AE(StringList arg_0, ref short arg_4, ref bool arg_8, bool arg_C, 
			StringList arg_E, sbyte arg_12, sbyte arg_14, sbyte arg_16, sbyte arg_18, 
			byte arg_1A, byte arg_1C, byte arg_1E, string arg_20, string arg_24)
        {
            string var_153;
            char var_12A;
            string var_129;
            string var_29;

            var_29 = arg_24;
            var_129 = arg_20;

            buildMenuStrings(out var_153, ref var_129);

            var_12A = ovr027.sl_select_item(out arg_0, ref arg_4, ref arg_8, arg_C, arg_E, arg_12, arg_14,
                arg_16, arg_18, arg_1A, arg_1C, arg_1E, var_129, var_29);
        }


        internal static void sub_3193B(string arg_0, string arg_4)
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            if (arg_4.CompareTo(arg_0) == 0)
            {
                gbl.item_find[0] = true;
            }

            if (arg_4.CompareTo(arg_0) != 0)
            {
                gbl.item_find[1] = true;
            }

            if (arg_4.CompareTo(arg_0) < 0)
            {
                gbl.item_find[2] = true;
            }

            if (arg_4.CompareTo(arg_0) > 0)
            {
                gbl.item_find[3] = true;
            }

            if (arg_4.CompareTo(arg_0) <= 0)
            {
                gbl.item_find[4] = true;
            }

            if (arg_4.CompareTo(arg_0) >= 0)
            {
                gbl.item_find[5] = true;
            }
        }

        /// <summary>
        /// sets global based of arg_0 and arg_2 relation.
        /// sub_31A11
        /// </summary>
        internal static void compare_variables(ushort arg_0, ushort arg_2)
        {
            System.Console.WriteLine("  Compare_variables: {0} {1}", arg_2, arg_0);

            gbl.item_find[0] = arg_2 == arg_0;
            gbl.item_find[1] = arg_2 != arg_0;
            gbl.item_find[2] = arg_2 < arg_0;
            gbl.item_find[3] = arg_2 > arg_0;
            gbl.item_find[4] = arg_2 <= arg_0;
            gbl.item_find[5] = arg_2 >= arg_0;
        }


        internal static void vm_gosub(byte arg_0)
        {
            ushort current = gbl.ecl_offset;

            gbl.vmCallStack.Push(current);
            gbl.ecl_offset = gbl.cmd_opps[arg_0].Word;
                
            System.Console.WriteLine("  vm_gosub: was: {0:X} now: {1:X}", current, gbl.ecl_offset);
        }


        internal static void sub_31B01()
        {
            if (gbl.byte_1D53B == 0)
            {
                if (gbl.byte_1D53A > 0)
                {
                    gbl.byte_1D53A--;
                }
                else
                {
                    gbl.byte_1D53A = 0x0F;
                }
            }
            else if (gbl.byte_1D53B == 2)
            {
                if (gbl.byte_1D539 < 15)
                {
                    gbl.byte_1D539++;
                }
                else
                {
                    gbl.byte_1D539 = 0;
                }
            }
            else if (gbl.byte_1D53B == 4)
            {
                if (gbl.byte_1D53A < 15)
                {
                    gbl.byte_1D53A++;
                }
                else
                {
                    gbl.byte_1D53A = 0;
                }
            }
            else if (gbl.byte_1D53B == 6)
            {
                if (gbl.byte_1D539 > 0)
                {
                    gbl.byte_1D539--;
                }
                else
                {
                    gbl.byte_1D539 = 0x0F;
                }
            }

            gbl.byte_1D53D = ovr031.sub_717A5(gbl.byte_1D53A, gbl.byte_1D539);

            gbl.byte_1D53C = ovr031.sub_716A2(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

            gbl.byte_1EE92 = 1;

        }


        internal static void duel(byte arg_0)
        {
            Item var_14;
            Item item;
            Player playerA;
            Player DuelMaster;
            Player playerC;

            gbl.combat_type = 1;

            gbl.area2_ptr.field_5CC = arg_0;
            playerA = gbl.player_ptr;
            playerC = gbl.player_next_ptr;

            while (playerC != null)
            {
                if (playerC.name != playerA.name)
                {
                    playerC.in_combat = false;
                }
                playerC = playerC.next_player;
            }

            if (arg_0 != 0)
            {
                ovr034.chead_cbody_comspr_icon(gbl.byte_1D92D, 11, "CPIC");
                playerC = gbl.player_next_ptr;

                while (playerC.next_player != null)
                {
                    playerC = playerC.next_player;
                }

                DuelMaster = playerA.ShallowClone();
                DuelMaster.in_combat = true;
                DuelMaster.next_player = null;
                DuelMaster.name = "ROLF";
                DuelMaster.field_198 = 1;

                DuelMaster.combat_team = 1;
                DuelMaster.field_F7 = 0xB2;
                DuelMaster.icon_id = gbl.byte_1D92D;

                DuelMaster.affect_ptr = null;
                DuelMaster.itemsPtr = null;

                playerC.next_player = DuelMaster;
                playerC = playerC.next_player;

                item = playerA.itemsPtr;

                while (item != null)
                {
                    if (playerC.itemsPtr == null)
                    {
                        playerC.itemsPtr = item.ShallowClone();
                        playerC.itemsPtr.next = null;
                    }
                    else
                    {
                        var_14 = playerC.itemsPtr;
                        playerC.itemsPtr = item.ShallowClone();
                        playerC.itemsPtr.next = var_14;
                    }
                    item = item.next;
                }
            }
        }


        internal static void sub_31DEF(Player player_ptr, double arg_4)
        {
            player_ptr.copper = (short)(player_ptr.copper * arg_4);
            player_ptr.electrum = (short)(player_ptr.electrum * arg_4);
            player_ptr.silver = (short)(player_ptr.silver * arg_4);
            player_ptr.gold = (short)(player_ptr.gold * arg_4);
            player_ptr.platinum = (short)(player_ptr.platinum * arg_4);
            player_ptr.field_105 = (short)(player_ptr.field_105 * arg_4);
            player_ptr.field_107 = (short)(player_ptr.field_107 * arg_4);
        }


        internal static void sub_31F1C(Player player, byte arg_4)
        {
            Item next_item_ptr;
            Item item_ptr;
            byte dice_roll;

            item_ptr = player.itemsPtr;

            while (item_ptr != null)
            {
                if (item_ptr.weight > 255)
                {
                    if (arg_4 > 90)
                    {
                        arg_4 -= 90;
                    }
                    else
                    {
                        arg_4 = 0;
                    }
                }
                else if (item_ptr.weight > 24)
                {
                    if (arg_4 > 50)
                    {
                        arg_4 -= 50;
                    }
                    else
                    {
                        arg_4 = 0;
                    }
                }

                dice_roll = ovr024.roll_dice(100, 1);

                next_item_ptr = item_ptr.next;

                if (dice_roll <= arg_4)
                {
                    ovr025.lose_item(item_ptr, player);
                }

                item_ptr = next_item_ptr;
            }
        }


        internal static void vm_skipNextCommand()
        {
            gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

            switch (gbl.command)
            {
                case 0x01:
                case 0x02:
                case 0x0A:
                case 0x0E:
                case 0x11:
                case 0x12:
                case 0x1D:
                case 0x20:
                case 0x2D:
                case 0x32:
                case 0x34:
                case 0x36:
                case 0x38:
                case 0x39:
                case 0x3C:
                case 0x3F:
                case 0x40:
                    vm_LoadCmdSets(1);
                    break;

                case 0x03:
                case 0x08:
                case 0x09:
                case 0x0F:
                case 0x10:
                case 0x1F:
                case 0x22:
                    vm_LoadCmdSets(2);
                    break;

                case 0x04:
                case 0x05:
                case 0x06:
                case 0x07:
                case 0x0B:
                case 0x0C:
                case 0x21:
                case 0x28:
                case 0x2A:
                case 0x2F:
                case 0x30:
                case 0x35:
                case 0x37:
                case 0x3B:
                    vm_LoadCmdSets(3);
                    break;

                case 0x14:
                case 0x23:
                    vm_LoadCmdSets(4);
                    break;

                case 0x2E:
                    vm_LoadCmdSets(5);
                    break;

                case 0x1E:
                case 0x2C:
                    vm_LoadCmdSets(6);
                    break;

                case 0x27:
                    vm_LoadCmdSets(8);
                    break;

                case 0x29:
                    vm_LoadCmdSets(0x0e);
                    break;

                default:
                    gbl.ecl_offset++;
                    break;
            }
        }


        internal static void calc_group_inituative(out byte init_min, out byte init_max)
        {
            byte player_initiative;
            Affect affect_ptr;
            Player player_ptr;

            player_ptr = gbl.player_next_ptr;

            init_max = player_ptr.initiative;
            init_min = player_ptr.initiative;

            while (player_ptr != null)
            {
                player_initiative = player_ptr.initiative;

                if (ovr025.find_affect(out affect_ptr, Affects.haste, player_ptr) == true)
                {
                    player_initiative *= 2;
                }
                else if (ovr025.find_affect(out affect_ptr, Affects.slow, player_ptr) == true)
                {
                    player_initiative /= 2;
                }

                if (player_initiative > init_max)
                {
                    init_max = player_initiative;
                }

                if (player_initiative < init_min)
                {
                    init_min = player_initiative;
                }

                player_ptr = player_ptr.next_player;
            }
        }


        internal static void sub_32200(Player player_ptr, short damage)
        {
            bool var_106;
            string var_101;

            if (player_ptr.health_status != Status.dead)
            {
                var_106 = false;

                if ((player_ptr.hit_point_current + 10) < damage)
                {
                    var_101 = string.Format("  {0} dies. ", player_ptr.name);
                }
                else
                {
                    var_101 = string.Format("  {0} is hit FOR {1} points of Damage.", player_ptr.name, damage);
                }

                if (gbl.textYCol > 0x16)
                {
                    gbl.textYCol = 0x11;
                    var_106 = true;
                    seg041.displayAndDebug("press <enter>/<return> to continue", 0, 15);
                }
                else
                {
                    var_106 = false;
                }

                gbl.textXCol = 0x26;

                seg041.press_any_key(var_101, var_106, 0, 15, 0x16, 0x26, 17, 1);

                ovr025.damage_player((byte)damage, player_ptr);
                seg037.draw8x8_clear_area(0x0f, 0x26, 1, 0x11);

                ovr025.Player_Summary(player_ptr);
            }
        }
    }
}
