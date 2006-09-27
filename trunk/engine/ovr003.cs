using Classes;

namespace engine
{
    class ovr003
    {
        internal static void sub_26050()
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

            seg051.FillChar(0, 2, gbl.byte_1EE72);

            gbl.byte_1EE8C = 0;
            gbl.byte_1EE8E = 0;
            gbl.byte_1AB08 = 1;

            gbl.ecl_offset++;

            while (gbl.dword_1D91A != null)
            {
                gbl.dword_1D91E = gbl.dword_1D91A.field_2;

                gbl.dword_1D91A = gbl.dword_1D91E;
            }

            gbl.byte_1C8CB = 0x11;
            gbl.byte_1C8CA = 1;
            gbl.byte_1EE8A = 0;
        }


        internal static void CMD_Goto()
        {
            ovr008.parse_command_sub(1);

            gbl.ecl_offset = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
        }


        internal static void sub_26108()
        {
            ovr008.parse_command_sub(1);

            ovr008.sub_31A77(1);
        }


        internal static void sub_2611D()
        {
            ovr008.parse_command_sub(2);

            if (gbl.cmd_code[1] >= 0x80 ||
                gbl.cmd_code[2] >= 0x80)
            {
                ovr008.sub_3193B(gbl.unk_1D972[2], gbl.unk_1D972[1]);
            }
            else
            {
                ushort var_2 = ovr008.sub_30168(1);
                ushort var_4 = ovr008.sub_30168(2);

                ovr008.sub_31A11(var_4, var_2);
            }
        }


        internal static void sub_2619A()
        {
            ushort var_9;
            ushort var_6;
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(3);

            var_6 = ovr008.sub_30168(1);
            var_4 = ovr008.sub_30168(2);

            var_2 = ovr008.bytes_to_word(gbl.cmd_high_bytes[3], gbl.cmd_low_bytes[3]);

            switch (gbl.command)
            {
                case 4:
                    var_9 = (ushort)(var_6 + var_4);
                    break;

                case 5:
                    var_9 = (ushort)(var_4 - var_6);
                    break;

                case 6:
                    var_9 = (ushort)(var_6 / var_4);
                    gbl.area2_ptr.field_67E = (short)(var_6 % var_4);
                    break;

                case 7:
                    var_9 = (ushort)(var_6 * var_4);
                    break;

                default:
                    var_9 = 0;
                    throw (new System.Exception("can't get here."));
            }

            ovr008.cmd_table01(var_9, var_2);
        }


        internal static void sub_2623D()
        {
            ushort var_4;
            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(2);

            var_1 = (byte)ovr008.sub_30168(1);

            if (var_1 < 0xff)
            {
                var_1++;
            }

            var_4 = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            var_2 = (byte)((double)(seg051.Random(var_1)));

            ovr008.cmd_table01(var_2, var_4);
        }


        internal static void sub_2629B()
        {
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(2);

            var_4 = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            if (gbl.cmd_code[1] < 0x80)
            {
                var_2 = ovr008.sub_30168(1);

                ovr008.cmd_table01(var_2, var_4);
            }
            else
            {
                ovr008.sub_3105D(gbl.unk_1D972[1], var_4);
            }
        }


        internal static void sub_262E9()
        {
            byte var_8;
            byte var_7;
            byte var_6;
            Player player_ptr;

            gbl.byte_1AB0A = 1;

            ovr008.parse_command_sub(1);
            var_7 = 0;

            var_6 = (byte)ovr008.sub_30168(1);

            player_ptr = gbl.player_next_ptr;
            var_8 = (byte)(var_6 & 0x80);

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
                ovr018.free_players(1, 0);

                ovr025.Player_Summary(gbl.player_ptr);
                gbl.byte_1EE7C = 0;
                gbl.byte_1EE7D = 0;
            }
        }


        internal static void sub_263C9()
        {
            ovr008.parse_command_sub(3);
            gbl.byte_1D92B = (byte)ovr008.sub_30168(1);

            gbl.area2_ptr.field_580 = (byte)ovr008.sub_30168(2);

            gbl.byte_1D92C = (byte)ovr008.sub_30168(3);

            gbl.area2_ptr.field_582 = ovr008.sub_304B4(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

            if (gbl.area2_ptr.field_580 < gbl.area2_ptr.field_582)
            {
                gbl.area2_ptr.field_582 = gbl.area2_ptr.field_580;
            }
            ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
        }


        internal static void sub_26465()
        {
            Affect affect_ptr2;
            Affect affect_ptr;
            Item item_ptr2;
            Item item_ptr;
            Player player_struct;
            Player playerD;
            Player playerC;
            Player playerB;
            Player playerA;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            playerD = gbl.player_ptr;
            var_4 = 1;
            ovr008.parse_command_sub(3);

            if (gbl.byte_1AB0E < 0x3f)
            {
                playerB = null;
                playerA = null;
                var_1 = (byte)ovr008.sub_30168(1);

                ovr008.load_mob(out affect_ptr, out item_ptr, out playerC, var_1);

                player_struct = playerC.ShallowClone();
                item_ptr2 = item_ptr;
                affect_ptr2 = affect_ptr;

                var_2 = (byte)ovr008.sub_30168(2);

                if (var_2 <= 0)
                {
                    var_2 = 1;
                }

                var_3 = (byte)ovr008.sub_30168(3);
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
                gbl.player_ptr = playerD;

            }
        }


        internal static void sub_26835()
        {
            if (gbl.area2_ptr.field_582 > 0)
            {
                gbl.area2_ptr.field_582--;

                ovr008.sub_30580(gbl.byte_1EE72, gbl.area2_ptr.field_582, gbl.byte_1D92C, gbl.byte_1D92B);
            }
            gbl.ecl_offset++;
        }


        internal static void sub_26873()
        {
            byte var_1;

            ovr008.parse_command_sub(1);
            var_1 = (byte)ovr008.sub_30168(1);

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
                        ovr030.sub_7000A(gbl.dword_1D55C, 1, 3, 3);
                    }
                }
                else
                {
                    ovr008.sub_30543(var_1, (byte)gbl.area2_ptr.field_5C2);
                }
            }
            else
            {
                if ((gbl.byte_1B2E4 != 4 ||
                      gbl.game_state == 4) &&
                    (gbl.byte_1EE8C != 0 ||
                      gbl.byte_1EE8F != 0))
                {
                    gbl.byte_1D8AA = 1;
                    ovr029.sub_6F0BA();
                    gbl.byte_1EE8C = 0;
                    gbl.byte_1EE8F = 0;
                    gbl.byte_1EE8D = 1;
                }
                gbl.byte_1EE72[0] = 0;
                gbl.byte_1EE72[1] = 0;
            }
        }


        internal static void sub_2695E()
        {
            ushort var_6;
            ushort var_4;
            string var_2;

            ovr008.parse_command_sub(2);

            var_2 = string.Empty;

            var_6 = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            var_4 = seg041.sub_10CB7(0, 0x0a, var_2);

            ovr008.cmd_table01(var_4, var_6);
        }


        internal static void sub_269A4()
        {
            string var_104;
            ushort var_4;
            string var_2;

            ovr008.parse_command_sub(2);
            var_2 = string.Empty;
            var_4 = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            var_104 = seg041.sub_10B26(0x28, 0, 10, var_2);

            if (var_104.Length == 0)
            {
                var_104 = " ";
            }

            ovr008.sub_3105D(var_104, var_4);
        }


        internal static void CMD_Print()
        {
            ovr008.parse_command_sub(1);
            gbl.byte_1EE90 = 0;
            gbl.byte_1B2F2 = 1;

            if (gbl.cmd_code[1] < 0x80)
            {
                gbl.unk_1D972[1] = ovr025.ConcatWord(ovr008.sub_30168(1));
            }

            if (gbl.command == 0x11)
            {
                seg041.press_any_key(gbl.unk_1D972[1], false, 0, 10, 0x16, 0x26, 0x11, 1);
            }
            else
            {
                gbl.byte_1C8CB = 0x11;
                gbl.byte_1C8CA = 1;

                seg041.press_any_key(gbl.unk_1D972[1], true, 0, 10, 0x16, 0x26, 0x11, 1);
            }

            gbl.byte_1B2F2 = 0;
        }


        internal static void sub_26AB8()
        {
            gbl.ecl_offset++;
            if (gbl.dword_1D91A != null)
            {
                gbl.ecl_offset = gbl.dword_1D91A.field_0;

                gbl.dword_1D91E = gbl.dword_1D91A.field_2;
                gbl.dword_1D91A = gbl.dword_1D91E;
            }
            else
            {
                sub_26050();
            }
        }


        internal static void sub_26B0C()
        {
            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            ovr008.parse_command_sub(4);

            ushort var_8 = ovr008.sub_30168(1);
            ushort var_6 = ovr008.sub_30168(2);
            ushort var_4 = ovr008.sub_30168(3);
            ushort var_2 = ovr008.sub_30168(4);

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


        internal static void sub_26B71()
        {
            gbl.ecl_offset++;

            switch (gbl.command)
            {
                case 0x16:
                    if (gbl.item_find[0] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;

                case 0x17:
                    if (gbl.item_find[1] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;

                case 0x18:
                    if (gbl.item_find[2] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;

                case 0x19:
                    if (gbl.item_find[3] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;

                case 0x1A:
                    if (gbl.item_find[4] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;

                case 0x1B:
                    if (gbl.item_find[5] == false)
                    {
                        ovr008.parse_command();
                    }
                    break;
            }
        }


        internal static void sub_26BE7()
        {
            byte var_1;

            ovr008.parse_command_sub(1);
            gbl.area_ptr.field_1E4 = gbl.byte_1EE88;

            var_1 = (byte)ovr008.sub_30168(1);

            gbl.byte_1EE88 = var_1;

            ovr008.load_ecl_dax(var_1);
            ovr008.sub_301E8();
            gbl.byte_1AB08 = 1;
            gbl.byte_1AB09 = 1;

            seg051.FillChar(0, 2, gbl.byte_1EE72);
        }


        internal static void CMD_LoadFiles()
        {
            byte var_3;
            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(3);
            gbl.byte_1AB0B = 1;

            var_3 = (byte)ovr008.sub_30168(1);
            var_2 = (byte)ovr008.sub_30168(2);
            var_1 = (byte)ovr008.sub_30168(3);

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
                gbl.byte_1B2E4 == 3)
            {
                if (gbl.game_state != 3 &&
                    gbl.byte_1EE98 != 0)
                {
                    seg037.draw8x8_03();
                    ovr025.Player_Summary(gbl.player_ptr);
                    ovr025.camping_search();
                }
                gbl.byte_1EE98 = 0;
            }
        }


        internal static void sub_26DD0()
        {
            byte var_7;
            ushort var_6;
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(3);
            var_6 = ovr008.sub_30168(1);
            var_4 = ovr008.sub_30168(2);

            var_2 = ovr008.bytes_to_word(gbl.cmd_high_bytes[3], gbl.cmd_low_bytes[3]);

            if (gbl.command == 0x27)
            {
                var_7 = (byte)(var_6 & var_4);
            }
            else
            {
                var_7 = (byte)(var_6 | var_4);
            }
            ovr008.sub_31A11(var_7, 0);
            ovr008.cmd_table01(var_7, var_2);
        }


        internal static void sub_26E3F()
        {
            byte var_9;
            ushort var_8;
            ushort var_6;
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(3);

            var_2 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
            var_9 = (byte)ovr008.sub_30168(2);

            var_4 = ovr008.bytes_to_word(gbl.cmd_high_bytes[3], gbl.cmd_low_bytes[3]);

            var_6 = (ushort)(var_9 + var_2);

            var_8 = ovr008.sub_30F16(var_6);
            ovr008.cmd_table01(var_8, var_4);
        }


        internal static void sub_26E9D()
        {
            ushort var_8;
            ushort var_6;
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(3);

            var_6 = ovr008.sub_30168(1);

            var_4 = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            var_2 = ovr008.sub_30168(3);
            var_8 = var_4;
            var_8 += var_2;

            ovr008.cmd_table01(var_6, var_8);
        }


        internal static void sub_26EE9()
        {
            ushort var_111;
            bool var_10F;
            short var_10E;
            string var_10C;
            StringList var_10A;
            StringList var_106;
            string var_102;
            byte var_2;
            byte var_1;

            gbl.byte_1EE90 = 0;
            var_10F = true;
            var_10C = string.Empty;
            var_2 = 1;
            ovr008.parse_command_sub(3);
            var_111 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);

            var_102 = gbl.unk_1D972[1];

            var_1 = (byte)ovr008.sub_30168(3);
            gbl.ecl_offset--;
            ovr008.parse_command_sub(var_1);

            ovr027.alloc_stringList(out var_106, var_1);

            var_10A = var_106;

            gbl.byte_1C8CA = 1;
            gbl.byte_1C8CB = 0x11;

            seg041.press_any_key(var_102, true, 0, 10, 22, 0x26, 11, 1);

            while (var_106 != null)
            {
                var_106.s = gbl.unk_1D972[var_2];
                var_106.field_29 = 0;

                var_106 = var_106.next;

                var_2++;
            }

            var_106 = var_10A;

            var_10E = 0;

            ovr008.sub_318AE(var_106, ref var_10E, ref var_10F, false, var_106, 0x16, 0x26, (sbyte)(gbl.byte_1C8CB + 1),
                1, 15, 10, 13, var_10C, var_10C);
            ovr008.cmd_table01((ushort)var_10E, var_111);

            ovr027.free_stringList(ref var_106);
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
        }


        internal static void CMD_HorizontalMenu()
        {
            byte var_3F;
            byte var_3E;
            byte var_3D;
            byte var_3C;
            byte var_3B;
            ushort var_3A;
            string var_38;
            byte var_3;
            byte var_2;
            byte var_1;


            var_38 = string.Empty;
            ovr008.parse_command_sub(2);

            var_3A = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
            var_1 = (byte)ovr008.sub_30168(2);

            gbl.ecl_offset--;

            ovr008.parse_command_sub(var_1);

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
                var_3C = 0;
            }
            else
            {
                var_3C = 1;
            }

            var_3F = (byte)(var_1 - 1);

            for (var_3 = 1; var_3 <= var_3F; var_3++)
            {
                var_38 = var_38 + "~" + gbl.unk_1D972[var_3] + "~";
            }

            var_38 += "~" + gbl.unk_1D972[var_1];

            var_2 = ovr008.sub_317AA(var_3C, var_3B, var_3D, var_3E, 0x0d, var_38, string.Empty);

            ovr008.cmd_table01(var_2, var_3A);

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
        }

        /// <summary>
        /// Clears the pooled items and pool money.
        /// </summary>
        internal static void sub_27240()
        {
            Item var_4;

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
                var_4 = gbl.item_pointer.next;

                gbl.item_pointer = var_4;
            }
        }


        internal static void sub_272A9()
        {
            Player player_ptr;
            ushort var_8;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            sbyte var_2;
            byte var_1;

            ovr008.parse_command_sub(1);
            var_6 = 0;
            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null)
            {
                var_1 = player_ptr.hit_point_current;
                var_5 = player_ptr.ac;
                var_2 = player_ptr.field_199;

                var_3 = (byte)(player_ptr.magic_user_lvl + (ovr026.sub_6B3D1(player_ptr) * player_ptr.field_116));
                var_4 = (byte)(player_ptr.cleric_lvl + (ovr026.sub_6B3D1(player_ptr) * player_ptr.turn_undead));

                if (var_5 > 0x3c)
                {
                    var_5 -= 0x3c;
                }
                else
                {
                    var_5 = 0;
                }

                if (var_2 > 0x27)
                {
                    var_2 -= 0x27;
                }
                else
                {
                    var_2 = 0;
                }

                var_6 = (byte)(((var_4 * 4) + var_1 + (var_5 * 5) + (var_2 * 5) + (var_3 * 8)) / 10);

                player_ptr = player_ptr.next_player;
            }

            var_8 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
            ovr008.cmd_table01(var_6, var_8);
        }


        internal static void sub_273F6(byte var_A, byte var_F, byte var_10, byte var_11,
            ushort var_13, ushort var_15, ushort var_17, ushort var_19)
        {
            ovr008.cmd_table01(var_11, var_13);
            ovr008.cmd_table01(var_10, var_15);
            ovr008.cmd_table01(var_F, var_17);
            ovr008.cmd_table01(var_A, var_19);
        }


        internal static void sub_27454()
        {
            byte var_1B;
            byte var_1A;
            ushort var_19;
            ushort var_17;
            ushort var_15;
            ushort var_13;
            byte var_11;
            byte var_10;
            byte var_F;
            Affect var_E;
            bool var_A;
            Player player_ptr;
            Affects affect_id;
            short var_4;
            ushort var_2;

            ovr008.parse_command_sub(6);
            player_ptr = gbl.player_next_ptr;

            var_A = false;
            if (gbl.cmd_code[1] == 1)
            {
                var_2 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
            }
            else
            {
                var_2 = ovr008.sub_30168(1);
            }

            affect_id = (Affects)ovr008.sub_30168(2);

            var_13 = ovr008.bytes_to_word(gbl.cmd_high_bytes[3], gbl.cmd_low_bytes[3]);
            var_15 = ovr008.bytes_to_word(gbl.cmd_high_bytes[4], gbl.cmd_low_bytes[4]);
            var_17 = ovr008.bytes_to_word(gbl.cmd_high_bytes[5], gbl.cmd_low_bytes[5]);
            var_19 = ovr008.bytes_to_word(gbl.cmd_high_bytes[6], gbl.cmd_low_bytes[6]);

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

                sub_273F6((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, var_13, var_15, var_17, var_19);
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

                sub_273F6((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, var_13, var_15, var_17, var_19);
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

                sub_273F6((var_A) ? (byte)1 : (byte)0, var_F, var_10, var_11, var_13, var_15, var_17, var_19);
            }
        }


        internal static void sub_2767E()
        {
            ushort var_B;
            ushort var_9;
            byte var_6;
            byte var_5;
            Player player;

            ovr008.parse_command_sub(2);

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

            var_9 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);
            var_B = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);

            ovr008.cmd_table01(var_5, var_9);
            ovr008.cmd_table01(var_6, var_B);
        }


        internal static void sub_2771E()
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

            ovr008.parse_command_sub(4);
            var_4 = 0;

            var_8 = (byte)ovr008.sub_30168(1);
            var_7 = (byte)ovr008.sub_30168(2);
            var_6 = (byte)ovr008.sub_30168(3);
            var_5 = (byte)ovr008.sub_30168(4);

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

            ovr008.cmd_table01(var_4, 0x2cb);
        }


        internal static void sub_277E4()
        {
            ushort var_2;

            gbl.ecl_offset++;

            if (gbl.byte_1EE93 == 0 &&
                gbl.combat_type == 0)
            {
                if (gbl.area2_ptr.field_6D8 == 1)
                {
                    gbl.area2_ptr.field_6D8 = 0;

                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sub_120E0(gbl.word_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sub_12194();
                    }

                    ovr007.sub_2F6E7();

                    if (gbl.area_ptr.field_1CC == 0)
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sub_120E0(gbl.word_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sub_12194();
                        }
                    }
                    else
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sub_120E0(gbl.word_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sub_12194();
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
                            seg044.sub_120E0(gbl.word_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sub_12194();
                        }

                        ovr005.temple_shop();

                        if (gbl.area_ptr.field_1CC == 0)
                        {

                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sub_120E0(gbl.word_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sub_12194();
                            }
                        }
                        else
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sub_120E0(gbl.word_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sub_12194();
                            }
                        }
                    }
                    else
                    {
                        if (gbl.gameFlag01 == true)
                        {
                            seg044.sub_120E0(gbl.word_188BC);
                        }

                        if (gbl.gameFlag01 == false)
                        {
                            seg044.sub_12194();
                        }

                        ovr006.sub_2E7A2();

                        if (gbl.area_ptr.field_1CC == 0)
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sub_120E0(gbl.word_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sub_12194();
                            }
                        }
                        else
                        {
                            if (gbl.gameFlag01 == true)
                            {
                                seg044.sub_120E0(gbl.word_188BC);
                            }

                            if (gbl.gameFlag01 == false)
                            {
                                seg044.sub_12194();
                            }
                        }
                    }
                }
            }
            else
            {
                if (gbl.gameFlag01 == true)
                {
                    seg044.sub_120E0(gbl.word_188BC);
                }

                if (gbl.gameFlag01 == false)
                {
                    seg044.sub_12194();
                }

                var_2 = ovr008.sub_304B4(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

                if (var_2 < gbl.area2_ptr.field_582)
                {
                    gbl.area2_ptr.field_582 = var_2;
                }

                ovr009.sub_33100();

                if (gbl.gameFlag01 == true)
                {
                    seg044.sub_120E0(gbl.word_188BC);
                }

                ovr006.sub_2E7A2();

                if (gbl.gameFlag01 == false)
                {
                    seg044.sub_12194();
                }

                if (gbl.area_ptr.field_1CC == 0)
                {
                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sub_120E0(gbl.word_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sub_12194();
                    }
                    ovr030.bigpic(0x79);
                }
                else
                {
                    if (gbl.gameFlag01 == true)
                    {
                        seg044.sub_120E0(gbl.word_188BC);
                    }

                    if (gbl.gameFlag01 == false)
                    {
                        seg044.sub_12194();
                    }
                }

                if (gbl.combat_type != 0)
                {
                    gbl.combat_type = 0;
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


        internal static void sub_27AE5()
        {
            byte var_5;
            byte var_4;
            byte var_2;
            byte var_1;

            var_5 = 0;
            ovr008.parse_command_sub(2);
            var_1 = (byte)ovr008.sub_30168(1);
            var_2 = (byte)ovr008.sub_30168(2);
            gbl.ecl_offset--;
            ovr008.parse_command_sub(var_2);
            var_4 = 0;

            while (var_4 < var_2 && var_5 == 0)
            {
                if (var_1 == var_4)
                {
                    if (gbl.command == 0x25)
                    {
                        gbl.ecl_offset = ovr008.bytes_to_word(gbl.cmd_high_bytes[var_4 + 1], gbl.cmd_low_bytes[var_4 + 1]);
                    }
                    else
                    {
                        ovr008.sub_31A77((byte)(var_4 + 1));
                    }

                    var_5 = 1;
                }
                else
                {
                    var_4++;
                }
            }
        }


        internal static void load_item()
        {
            byte var_70;
            byte[] var_6F;
            short var_69;
            short var_67;
            byte var_65 = 0;
            byte var_64;
            byte var_63;
            byte var_62;
            Item var_61;
            Item var_5B;
            Item var_59 = null;
            byte var_3;
            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(8);

            for (var_1 = 0; var_1 < 7; var_1++)
            {
                gbl.pooled_money[var_1] = ovr008.sub_30168((byte)(var_1 + 1));
            }

            var_2 = (byte)ovr008.sub_30168(8);

            if (var_2 < 0x80)
            {
                seg042.load_decode_dax(out var_6F, out var_69, var_2, string.Format("ITEM{0}.dax", gbl.game_area));

                if (var_69 == 0)
                {
                    seg041.displayAndDebug("Unable to find item file", 0, 15);

                    seg043.print_and_exit();
                }

                var_67 = 0;

                do
                {
                    var_59 = new Item(var_6F, var_67);

                    var_67 += 0x3F;

                    if (gbl.item_pointer == null)
                    {
                        gbl.item_pointer = new Item();
                        var_59 = gbl.item_pointer;
                    }
                    else
                    {
                        var_5B = gbl.item_pointer;

                        gbl.item_pointer = new Item();
                        var_59 = gbl.item_pointer;

                        gbl.item_pointer.next = var_5B;
                    }

                } while (var_67 < var_69);

                var_6F = null;
            }
            else if (var_2 != 0xff)
            {
                var_1 = (byte)(var_2 - 0x80);
                var_70 = var_1;

                for (var_3 = 1; var_3 <= var_70; var_3++)
                {
                    var_63 = ovr024.roll_dice(100, 1);

                    if (var_63 >= 1 && var_63 <= 0x3c)
                    {
                        var_64 = ovr024.roll_dice(100, 1);

                        if ((var_64 >= 1 && var_64 <= 0x27) ||
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

                    ovr022.sub_5A007(var_59, var_65);

                    if (gbl.item_pointer == null)
                    {
                        gbl.item_pointer = var_59.ShallowClone();
                        gbl.item_pointer.next = null;
                    }
                    else
                    {
                        var_5B = gbl.item_pointer;

                        gbl.item_pointer = var_59.ShallowClone();
                        gbl.item_pointer.next = var_5B;
                    }
                }

                var_61 = gbl.item_pointer;

                while (var_61 != null)
                {
                    ovr025.id_item(0, 0, 0, 0, var_61, gbl.player_ptr);
                    var_61 = var_61.next;
                }
            }
        }


        internal static void sub_27F76()
        {
            double var_11;
            object var_B;
            Player player;
            byte var_3;
            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(3);
            var_1 = (byte)ovr008.sub_30168(1);
            var_2 = (byte)ovr008.sub_30168(2);

            var_11 = (100 - var_2) / 100.0;
            var_3 = (byte)ovr008.sub_30168(3);

            if (var_1 == 0)
            {
                ovr008.sub_31DEF(gbl.player_ptr, var_11);
                ovr008.sub_31F1C(gbl.player_ptr, var_3);
            }
            else
            {
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    var_B = player.itemsPtr;
                    ovr008.sub_31DEF(player, var_11);
                    ovr008.sub_31F1C(player, var_3);
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
            byte var_40E;
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
            gbl.byte_1B2F2 = 1;

            ovr008.calc_group_inituative(out init_min, out var_40A);

            var_439 = string.Empty;
            ovr008.parse_command_sub(0x0e);

            gbl.byte_1D92B = (byte)ovr008.sub_30168(1);
            gbl.area2_ptr.field_580 = ovr008.sub_30168(2);
            gbl.byte_1D92C = (byte)ovr008.sub_30168(3);

            var_43D = ovr008.bytes_to_word(gbl.cmd_high_bytes[4], gbl.cmd_low_bytes[4]);

            for (int i = 0; i < 5; i++)
            {
                var_6[i] = (byte)ovr008.sub_30168(i + 5);
            }

            for (var_409 = 0; var_409 < 3; var_409++)
            {
                var_405[var_409] = gbl.unk_1D972[var_409 + 1];
            }

            var_407 = (byte)ovr008.sub_30168(0x0d);
            var_408 = (byte)ovr008.sub_30168(0x0e);

            gbl.area2_ptr.field_582 = ovr008.sub_304B4(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

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
                    var_40E = 0;
                }
                else
                {
                    var_40E = 1;
                }

                var_40D = (gbl.area_ptr.field_1CC != 0);

                init_max = 0;
                gbl.byte_1C8CA = 1;
                gbl.byte_1C8CB = 0x11;

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

                var_1 = ovr008.sub_317AA(var_40E, 0, 15, 10, 13, var_437, var_439);

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
                            ovr008.cmd_table01(1, var_43D);
                        }
                        else
                        {
                            if (init_min >= var_407)
                            {
                                ovr008.cmd_table01(2, var_43D);
                            }
                            else
                            {
                                ovr008.cmd_table01(1, var_43D);
                            }
                        }
                        break;

                    case 1:
                        if (var_1 == 0)
                        {
                            ovr008.cmd_table01(1, var_43D);
                        }
                        else if (var_1 == 1)
                        {
                            init_max = 1;
                            seg041.press_any_key("Both sides wait.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                        }
                        else if (var_1 == 2)
                        {
                            ovr008.cmd_table01(2, var_43D);
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
                                ovr008.cmd_table01(3, var_43D);
                            }
                        }
                        break;

                    case 2:
                        if (var_1 == 0)
                        {
                            if (var_408 > var_40A)
                            {
                                ovr008.cmd_table01(0, var_43D);

                                gbl.byte_1C8CA = 1;
                                gbl.byte_1C8CB = 0x11;
                                seg041.press_any_key("The monsters flee.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            }
                            else
                            {
                                ovr008.cmd_table01(1, var_43D);
                            }
                        }
                        else if (var_1 >= 1 && var_1 <= 4)
                        {
                            ovr008.cmd_table01(0, var_43D);

                            gbl.byte_1C8CA = 1;
                            gbl.byte_1C8CB = 0x11;
                            seg041.press_any_key("The monsters flee.", true, 0, 10, 0x16, 0x26, 0x11, 1);
                        }
                        break;

                    case 3:
                        if (var_1 == 0)
                        {
                            ovr008.cmd_table01(1, var_43D);
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
                            ovr008.cmd_table01(2, var_43D);
                        }
                        else if (var_1 == 4)
                        {
                            if (gbl.area2_ptr.field_582 <= 0)
                            {
                                ovr008.cmd_table01(3, var_43D);
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
                            ovr008.cmd_table01(1, var_43D);
                        }
                        else if (var_1 == 1 || var_1 == 3 || var_1 == 4)
                        {

                            if (gbl.area2_ptr.field_582 <= 0)
                            {
                                ovr008.cmd_table01(3, var_43D);
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
                            ovr008.cmd_table01(2, var_43D);
                        }

                        break;
                }
            } while (init_max != 0);

            ovr027.redraw_screen();
            gbl.byte_1B2F2 = 0;
            gbl.byte_1EE95 = 0;
        }


        internal static void CMD_Parlay()
        {
            char var_B;
            ushort var_A;

            byte[] var_8 = new byte[5];

            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(6);
            var_B = ' ';

            for (int i = 0; i < 5; i++)
            {
                var_8[i] = (byte)ovr008.sub_30168(i + 1);
            }

            var_1 = ovr008.sub_317AA(0, 0, 15, 10, 13, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", var_B.ToString());
            var_A = ovr008.bytes_to_word(gbl.cmd_high_bytes[6], gbl.cmd_low_bytes[6]);

            var_2 = var_8[var_1];

            ovr008.cmd_table01(var_2, var_A);
        }


        internal static void sub_28856()
        {
            bool found;
            Item item_ptr;
            Player player;
            byte var_1;

            ovr008.parse_command_sub(1);
            found = false;
            var_1 = (byte)ovr008.sub_30168(1);

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


        internal static void sub_28958()
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
            ovr008.parse_command_sub(5);
            var_1 = (byte)ovr008.sub_30168(1);
            var_2 = (byte)ovr008.sub_30168(2);
            var_3 = (byte)ovr008.sub_30168(3);
            var_7 = (byte)ovr008.sub_30168(4);
            var_6 = (byte)ovr008.sub_30168(5);


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
                    throw new System.NotSupportedException();//mov	al, [bp+var_6]
                    throw new System.NotSupportedException();//and	al,0x80
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_28AF1
                    throw new System.NotSupportedException();//cmp	[bp+var_9], 0
                    throw new System.NotSupportedException();//jz	loc_28AC7

                    ovr024.do_saving_throw((sbyte)var_5, (byte)(var_9 - 1), gbl.player_ptr);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jnz	loc_28AD9
                    throw new System.NotSupportedException();//loc_28AC7:
                    ovr008.sub_32200(gbl.player_ptr, var_C);
                    throw new System.NotSupportedException();//jmp	short loc_28AEF
                    throw new System.NotSupportedException();//loc_28AD9:
                    throw new System.NotSupportedException();//cmp	[bp+var_1B], 0
                    throw new System.NotSupportedException();//jz	loc_28AEF
                    ovr008.sub_32200(gbl.player_ptr, var_C);
                    throw new System.NotSupportedException();//loc_28AEF:
                    throw new System.NotSupportedException();//jmp	short loc_28B5C
                    throw new System.NotSupportedException();//loc_28AF1:

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
                throw new System.NotSupportedException();//loc_28B5C:
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
                gbl.byte_1C8CA = 2;
                gbl.byte_1C8CB = 2;

                seg041.press_any_key("The entire party is killed!", true, 0, 10, 0x16, 0x26, 1, 1);
                seg049.SysDelay(0x0BB8);
            }

            gbl.player_ptr = player01;
            seg041.displayAndDebug("press <enter>/<return> to continue", 0, 15);
        }


        internal static void sub_28CB6()
        {
            gbl.ecl_offset++;
            if (gbl.byte_1EE8F != 0)
            {
                gbl.byte_1D8AA = 1;
                ovr029.sub_6F0BA();
                gbl.byte_1EE8F = 0;
                gbl.byte_1EE8C = 0;
            }
        }


        internal static void sub_28CDA()
        {
            byte var_2;
            byte var_1;

            ovr008.parse_command_sub(2);
            var_1 = (byte)ovr008.sub_30168(1);
            var_2 = (byte)ovr008.sub_30168(2);

            ovr021.sub_583FA(var_2, var_1);
        }


        internal static void sub_28D0F()
        {
            gbl.ecl_offset++;

            if (gbl.byte_1EE8B != 0)
            {
                gbl.byte_1C8CA = 1;
                gbl.byte_1C8CB++;
                gbl.byte_1EE8B = 0;
            }
            else
            {
                gbl.byte_1C8CA = 1;
                gbl.byte_1C8CB++;
            }
        }


        internal static void sub_28D38()
        {
            gbl.ecl_offset++;
            seg037.draw8x8_03();
            ovr025.Player_Summary(gbl.player_ptr);
            ovr025.camping_search();

            ovr030.sub_7000A(gbl.dword_1D55C, 1, 3, 3);
            ovr025.camping_search();
            gbl.byte_1EE98 = 0;
            gbl.byte_1EE8A = 1;
        }


        internal static void sub_28D7F()
        {
            string var_100;

            ovr008.parse_command_sub(1);
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

            var_100 = gbl.unk_1D972[1];

            ovr025.selectAPlayer(ref gbl.player_ptr, false, var_100);
        }


        internal static void sub_28DCA()
        {
            byte var_6;
            byte var_5;

            ovr008.parse_command_sub(2);
            var_5 = (byte)ovr008.sub_30168(1);

            ovr017.sub_4A57D(var_5);

            var_6 = (byte)ovr008.sub_30168(2);

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
            ovr008.parse_command_sub(3);
            var_1 = (byte)ovr008.sub_30168(1);
            var_B = ovr008.bytes_to_word(gbl.cmd_high_bytes[2], gbl.cmd_low_bytes[2]);
            var_D = ovr008.bytes_to_word(gbl.cmd_high_bytes[3], gbl.cmd_low_bytes[3]);
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

            ovr008.cmd_table01(var_2, var_B);
            ovr008.cmd_table01(var_3, var_D);
        }


        internal static void CMD_Call()
        {
            ushort var_4;
            ushort var_2;

            ovr008.parse_command_sub(1);

            var_2 = ovr008.bytes_to_word(gbl.cmd_high_bytes[1], gbl.cmd_low_bytes[1]);

            var_4 = (ushort)(var_2 - 0x7fff);

            if (var_4 == 0xAE11)
            {
                gbl.byte_1D53D = ovr031.sub_717A5(gbl.byte_1D53A, gbl.byte_1D539);

                if (gbl.byte_1AB0B != 0)
                {
                    if (gbl.byte_1EE8C != 0 || gbl.byte_1EE8F != 0 || gbl.byte_1EE91 != 0 ||
                        gbl.byte_1EE92 != 0 || gbl.byte_1EE94 != 0)
                    {
                        gbl.byte_1D8AA = 1;
                        ovr029.sub_6F0BA();
                        ovr025.camping_search();
                        gbl.byte_1EE94 = 0;
                        gbl.byte_1EE91 = 0;
                        gbl.byte_1EE92 = 0;
                        gbl.byte_1EE8C = 0;
                        gbl.byte_1EE8F = 0;

                        gbl.byte_1D53C = ovr031.sub_716A2(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

                    }
                }
            }
            else if (var_4 == 1)
            {
                ovr008.duel(1);
            }
            else if (var_4 == 2)
            {
                ovr008.duel(0);
            }
            else if (var_4 == 0x3201)
            {
                if (gbl.word_1EE76 == 8)
                {
                    seg044.sub_120E0(gbl.word_188D2);
                }
                else if (gbl.word_1EE76 == 10)
                {
                    seg044.sub_120E0(gbl.word_188D4);
                }
                else
                {
                    seg044.sub_120E0(gbl.word_188D2);
                }
            }
            else if (var_4 == 0x401F)
            {
                ovr008.sub_31B01();
            }
            else if (var_4 == 0x4019)
            {
                if (gbl.area_ptr.field_1CC == 0)
                {
                    gbl.byte_1D53C = ovr031.sub_716A2(gbl.byte_1D53B, gbl.byte_1D53A, gbl.byte_1D539);

                }
            }
            else if (var_4 == 0xE804)
            {
                ovr030.sub_7000A(gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_4, 1, 3, 3);
                gbl.byte_1D556.curFrame++;

                if (gbl.byte_1D556.curFrame > gbl.byte_1D556.numFrames)
                {
                    gbl.byte_1D556.curFrame = 1;
                }

                seg041.GameDelay();
            }
        }


        internal static void sub_29094()
        {
            byte var_1;

            var_1 = 0;

            sub_29607(gbl.word_1B2D7);
            ovr016.make_camp(ref var_1);

            if (var_1 != 0)
            {
                ovr025.load_pic();
                sub_29607(gbl.word_1B2D9);
            }
            gbl.byte_1D8AA = 1;
            ovr029.sub_6F0BA();
            gbl.byte_1C01B = 0;
        }


        internal static void YourHaveWon()
        {
            Player play_ptr;
            char saveYes;
            Player player;
            ushort var_3;
            byte var_1;

            if (gbl.byte_1AB0A != 0)
            {
                gbl.player_ptr = gbl.player_ptr2;
                gbl.byte_1AB0A = 0;
            }

            ovr008.parse_command_sub(1);
            var_1 = (byte)ovr008.sub_30168(1);

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
                gbl.byte_1EE99 = 1;
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
                sub_26050();
            }
            else if (var_1 == 3)
            {
                gbl.byte_1B2F0 = 1;
                sub_26050();
            }
        }


        internal static void sub_2923F()
        {
            seg051.FillChar(0, 2, gbl.byte_1EE72);
            gbl.byte_1EE8C = 0;
            ovr008.parse_command_sub(1);

            ovr004.copy_protection();
            ovr025.load_pic();
        }


        internal static void sub_29271()
        {
            gbl.ecl_offset++;

            ovr018.free_players(1, 0);

            gbl.player_ptr2 = gbl.player_ptr;

            ovr025.Player_Summary(gbl.player_ptr);
        }


        internal static void sub_292A5()
        {
            Affects var_5;
            Affect var_4;

            for (int i = 0; i < 6; i++)
            {
                gbl.item_find[i] = false;
            }

            ovr008.parse_command_sub(1);
            var_5 = (Affects)ovr008.sub_30168(1);

            if (ovr025.find_affect(out var_4, var_5, gbl.player_ptr) == true)
            {
                gbl.item_find[0] = true;
            }
            else
            {
                gbl.item_find[1] = true;
            }
        }


        internal static void sub_292F9()
        {
            Item item;
            Player player;
            byte var_1;

            ovr008.parse_command_sub(1);
            var_1 = (byte)ovr008.sub_30168(1);


            player = gbl.player_next_ptr;

            while (player != null)
            {
                item = player.itemsPtr;
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
                case 0x00: sub_26050(); break;
                case 0x01: CMD_Goto(); break;
                case 0x02: sub_26108(); break;
                case 0x03: sub_2611D(); break;
                case 0x04: sub_2619A(); break;
                case 0x05: sub_2619A(); break;
                case 0x06: sub_2619A(); break;
                case 0x07: sub_2619A(); break;
                case 0x08: sub_2623D(); break;
                case 0x09: sub_2629B(); break;
                case 0x0A: sub_262E9(); break;
                case 0x0B: sub_26465(); break;
                case 0x0C: sub_263C9(); break;
                case 0x0D: sub_26835(); break;
                case 0x0E: sub_26873(); break;
                case 0x0F: sub_2695E(); break;
                case 0x10: sub_269A4(); break;
                case 0x11: CMD_Print(); break;
                case 0x12: CMD_Print(); break;
                case 0x13: sub_26AB8(); break;
                case 0x14: sub_26B0C(); break;
                case 0x15: sub_26EE9(); break;
                case 0x16: sub_26B71(); break;
                case 0x17: sub_26B71(); break;
                case 0x18: sub_26B71(); break;
                case 0x19: sub_26B71(); break;
                case 0x1A: sub_26B71(); break;
                case 0x1B: sub_26B71(); break;
                case 0x1C: sub_27240(); break;
                case 0x1D: sub_272A9(); break;
                case 0x1E: sub_27454(); break;
                case 0x20: sub_26BE7(); break;
                case 0x21: CMD_LoadFiles(); break;
                case 0x22: sub_2767E(); break;
                case 0x23: sub_2771E(); break;
                case 0x24: sub_277E4(); break;
                case 0x25: sub_27AE5(); break;
                case 0x26: sub_27AE5(); break;
                case 0x27: load_item(); break;
                case 0x28: sub_27F76(); break;
                case 0x29: CMD_EncounterMenu(); break;
                case 0x2A: sub_26E3F(); break;
                case 0x2B: CMD_HorizontalMenu(); break;
                case 0x2C: CMD_Parlay(); break;
                case 0x2D: CMD_Call(); break;
                case 0x2E: sub_28958(); break;
                case 0x2F: sub_26DD0(); break;
                case 0x30: sub_26DD0(); break;
                case 0x31: sub_28CB6(); break;
                case 0x32: sub_28856(); break;
                case 0x33: sub_28D0F(); break;
                case 0x34: sub_28CDA(); break;
                case 0x35: sub_26E9D(); break;
                case 0x36: sub_28DCA(); break;
                case 0x37: CMD_LoadFiles(); break;
                case 0x38: YourHaveWon(); break;
                case 0x39: sub_28D7F(); break;
                case 0x3A: CMD_Delay(); break;
                case 0x3B: CMD_Spell(); break;
                case 0x3C: sub_2923F(); break;
                case 0x3D: sub_28D38(); break;
                case 0x3E: sub_29271(); break;
                case 0x3F: sub_292A5(); break;
                case 0x40: sub_292F9(); break;
                default:
                    break;
            }
        }


        internal static void sub_29607(ushort offset)
        {
            string var_100;

            gbl.ecl_offset = offset;
            gbl.byte_1AB08 = 0;

            while (gbl.byte_1AB08 == 0 ||
                   gbl.byte_1B2F0 == 0)
            {
                gbl.byte_1D928 = gbl.command;

                gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

                ovr036.print_command(out var_100);
                System.Console.Out.WriteLine(var_100);
                
                if (gbl.printCommands == true)
                {
                    ovr036.print_command(out var_100);
                    seg051.Write(0, var_100, gbl.unk_1EE9A);
                    seg051.WriteLn(gbl.unk_1EE9A);
                }

                commandTable();
            }

            gbl.byte_1AB08 = 0;
        }


        internal static void sub_29677()
        {
            do
            {
                ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);
                gbl.byte_1D5AB = string.Empty;
                gbl.byte_1D5B5 = 0x0FF;
                gbl.byte_1AB09 = 0;
                gbl.byte_1D53D = ovr031.sub_717A5(gbl.byte_1D53A, gbl.byte_1D539);

                gbl.area2_ptr.field_5AA = 0;

                gbl.player_ptr2 = gbl.player_ptr;

                sub_29607(gbl.word_1B2DB);

                if (gbl.byte_1AB09 == 0)
                {
                    gbl.area_ptr.field_1E4 = gbl.byte_1EE88;
                }

                if (gbl.byte_1AB09 == 0)
                {
                    if ((gbl.byte_1B2E4 == 4 && gbl.game_state == 4) ||
                        (gbl.byte_1B2E4 != 4 && gbl.byte_1AB0B != 0))
                    {
                        ovr029.sub_6F0BA();
                    }
                    gbl.byte_1AB09 = 0;

                    sub_29607(gbl.word_1B2D3);

                    if (gbl.byte_1AB09 == 0)
                    {
                        sub_29607(gbl.word_1B2D5);

                        if (gbl.byte_1AB09 == 0)
                        {
                            gbl.player_ptr = gbl.player_ptr2;
                            ovr025.Player_Summary(gbl.player_ptr);
                        }
                    }

                }
            } while (gbl.byte_1AB09 != 0);

            gbl.byte_1B2E4 = gbl.game_state;
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

            ovr008.sub_301E8();

            sub_29607(gbl.word_1B2DB);

            if (gbl.inDemo == true)
            {
                do
                {
                    ovr018.free_players(1, 1);
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

                    while ((gbl.area2_ptr.field_594 > 1 || seg051.UpCase(var_1) == 'E') &&
                        gbl.byte_1B2F0 == 0)
                    {
                        if (seg051.UpCase(var_1) == 'E')
                        {
                            sub_29094();
                        }
                        else
                        {
                            gbl.byte_1EE89 = (byte)(gbl.area2_ptr.field_594 & 1);
                            gbl.area2_ptr.field_594 = 1;
                            gbl.byte_1D8AA = 1;
                            ovr029.sub_6F0BA();

                            sub_29607(gbl.word_1B2D5);

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
                        sub_29607(gbl.word_1B2D3);
                    }

                    if (gbl.byte_1AB09 != 0)
                    {
                        sub_29677();
                    }
                    else
                    {
                        if (gbl.byte_1B2F0 == 0)
                        {
                            gbl.area_ptr.field_1E0 = gbl.byte_1D539;
                            gbl.area_ptr.field_1E2 = gbl.byte_1D53A;

                            ovr015.locked_door();
                            ovr029.sub_6F0BA();

                            if (gbl.area_ptr.field_1E0 != gbl.byte_1D539 ||
                                gbl.area_ptr.field_1E2 != gbl.byte_1D53A)
                            {
                                seg044.sub_120E0(gbl.word_188D2);
                            }

                            gbl.byte_1EE8C = 0;
                            gbl.byte_1EE8D = 1;
                            sub_29607(gbl.word_1B2D5);
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
    }
}