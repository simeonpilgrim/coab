using Classes;

namespace engine
{
    class ovr021
    {
        internal static void sub_5801E(byte arg_0, byte arg_2)
        {
            Affect affect01;
            Affect var_17;
            Affect affect;
            Affect var_F;
            byte var_B;
            byte var_A;
            Player player;
            ushort var_5;
            byte var_3;
            byte var_2;
            byte var_1;

            if (gbl.game_state != 2)
            {
                seg051.FillChar(1, 0x48, gbl.unk_1AE24);
            }
            else
            {
                var_B = 0;
                var_2 = 0;

                do
                {
                    if (gbl.unk_1AE24[var_2] != 0)
                    {
                        var_B = 1;
                    }

                    var_2++;
                } while (var_B == 0 && var_2 < gbl.area2_ptr.field_67C);

                if (var_B == 0)
                {
                    return;
                }
            }

            var_5 = arg_2;
            var_1 = arg_0;

            while (var_1 > 1)
            {
                var_5 *= gbl.word_1A13C[var_1 - 1];
                var_1 -= 1;
            }

            while (var_5 > 0)
            {
                if (var_5 > 10)
                {
                    var_3 = 0x0A;
                }
                else
                {
                    var_3 = (byte)var_5;
                }

                var_2 = 0;
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (gbl.unk_1AE24[var_2] != 0)
                    {
                        gbl.unk_1AE24[var_2] = 0;

                        affect01 = player.affect_ptr;
                        var_F = player.affect_ptr;

                        affect = player.affect_ptr;

                        //var_17 = affect.next;

                        if (player.affect_ptr != null)
                        {
                            while (var_F.next != null)
                            {
                                var_F = var_F.next;
                            }
                        }

                        var_A = 0;

                        while (affect01 != null &&
                            var_A == 0)
                        {
                            if (affect01 == var_F)
                            {
                                var_A = 1;
                            }

                            if (affect01.field_1 == 0)
                            {
                                affect01 = affect01.next;
                            }
                            else if (var_3 < affect01.field_1)
                            {
                                affect01.field_1 = var_3;
                                gbl.unk_1AE24[var_2] = 1;

                                affect01 = affect01.next;
                            }
                            else
                            {
                                var_17 = affect01.next;

                                ovr024.remove_affect(affect01, affect01.type, player);

                                if (affect == player.affect_ptr)
                                {
                                    affect01 = player.affect_ptr;
                                }
                                else
                                {
                                    affect01 = var_17;
                                }
                            }

                            affect = player.affect_ptr;

                            while (affect != null &&
                                affect != affect01 &&
                                affect.next != affect01)
                            {
                                affect = affect.next;
                            }
                        }

                        while (affect01 != null)
                        {
                            if (affect01.field_1 > 0)
                            {
                                gbl.unk_1AE24[var_2] = 1;
                            }

                            affect01 = affect01.next;
                        }
                    }

                    var_2++;
                    player = player.next_player;
                }

                if (var_5 > 10)
                {
                    var_5 -= 10;
                }
                else
                {
                    var_5 = 0;
                }
            }
        }


        internal static void sub_58317(RestTime arg_0)
        {
            Player player;
            byte loop_var;

            for (loop_var = 0; loop_var <= 6; loop_var++)
            {
                if (arg_0[loop_var] >= gbl.word_1A13C[loop_var])/* short arrays */
                {
                    if (loop_var != 6)
                    {
                        arg_0[loop_var + 1] += 1;
                        arg_0[loop_var] -= gbl.word_1A13C[loop_var]; ;
                    }
                    else
                    {
                        player = gbl.player_next_ptr;

                        while (player != null)
                        {
                            player.age += 1;

                            player = player.next_player;
                        }
                    }
                }
            }
        }


        internal static void sub_583C8()
        {
            sub_58317(gbl.unk_1D890);

            if (gbl.unk_1D890.field_A > 0)
            {
                gbl.unk_1D890.field_8 += (ushort)(gbl.word_1A13C.field_8 * gbl.unk_1D890.field_A);

                gbl.unk_1D890.field_A = 0;

                if (gbl.unk_1D890.field_8 > 99)
                {
                    gbl.unk_1D890.field_8 = 99;
                }
            }
        }

        /*static byte[] unk_4BC6 = { 0x57, 0x87, 0x57, 0x9B, 0x57, 0xB6, 0x57 };*/
        static ushort[] unk_4BC6 = { 0x00AE, 0x010E, 0x00AE, 0x0136, 0x00AE, 0x016C, 0x00AE };

        internal static void sub_583FA(byte arg_0, byte arg_2)
        {
            byte var_11;
            byte var_10;
            byte var_F;
            RestTime var_E = new RestTime();

            for (var_F = 0; var_F <= 6; var_F++)
            {
                var_E[var_F] = gbl.area_ptr.field_6A00_Get(unk_4BC6[var_F]); // as WORD[]
            }

            var_11 = arg_2;

            for (var_10 = 1; var_10 <= var_11; var_10++)
            {
                var_E[arg_0] += 1;

                sub_58317(var_E);
            }

            for (var_F = 0; var_F <= 6; var_F++)
            {
                gbl.area_ptr.field_6A00_Set(unk_4BC6[var_F], var_E[var_F]);
            }

            sub_5801E(arg_0, arg_2);
        }


        internal static void sub_5849F(byte arg_0, byte arg_2)
        {
            byte var_1;

            if (gbl.unk_1D890.field_8 != 0 ||
                gbl.unk_1D890.field_6 != 0 ||
                gbl.unk_1D890.field_4 != 0 ||
                gbl.unk_1D890.field_2 != 0)
            {
                while (arg_2 > gbl.unk_1D890[arg_0])
                {
                    var_1 = (byte)(arg_0 + 1);

                    while (gbl.unk_1D890[var_1] == 0 &&
                        var_1 < 5)
                    {
                        var_1 += 1;
                    }

                    if (var_1 == 5)
                    {
                        gbl.unk_1D890.Clear();
                        arg_2 = 0;
                    }
                    else
                    {
                        for (int i = var_1; i >= (arg_0 + 1); i--)
                        {
                            gbl.unk_1D890[i] -= 1;
                            gbl.unk_1D890[i - 1] += gbl.word_1A13C[i - 1];
                        }
                    }
                }

                gbl.unk_1D890[arg_0] -= arg_2;

                sub_583C8();
            }
        }


        static string sub_5858A(int arg_0, out string arg_2)
        {
            arg_2 = string.Format("{0:00}", arg_0);

            return arg_2;
        }


        internal static void sub_58615(byte arg_0)
        {
            string var_110;
            byte var_10;
            byte[] var_F = new byte[6];
            byte var_1;

            for (var_1 = 0; var_1 < 6; var_1++)
            {
                var_F[var_1] = 10;
            }

            var_F[arg_0] = 15;

            seg041.displayString("Rest Time:", 0, 10, 17, 1);
            var_10 = 0x0B;

            sub_5858A(gbl.unk_1D890.field_8, out var_110);
            seg041.displayString(var_110, 0, var_F[4], 0x11, var_10 + 1);
            seg041.displayString(":", 0, 10, 17, var_10 + 3);
            var_10 += 3;

            sub_5858A(gbl.unk_1D890.field_6, out var_110);
            seg041.displayString(var_110, 0, var_F[3], 0x11, var_10 + 1);
            seg041.displayString(":", 0, 10, 17, var_10 + 3);
            var_10 += 3;

            sub_5858A((gbl.unk_1D890.field_4 * 10) + gbl.unk_1D890.field_2, out var_110);

            seg041.displayString(var_110, 0, var_F[2], 0x11, var_10 + 1);
        }

        static Set unk_58731 = new Set(0x000B, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x04 });

        internal static byte sub_58751()
        {
            byte var_4;
            char var_3;
            bool var_2;
            byte var_1;

            var_1 = 0;
            var_4 = 2;

            do
            {
                sub_58615(var_4);
                var_3 = ovr027.displayInput(out var_2, 0, 1, 15, 10, 13, "Rest Days Hours Mins Add Subtract Exit", string.Empty);

                if (var_2 == true)
                {
                    switch (var_3)
                    {
                        case 'H':
                            var_3 = 'A';
                            break;

                        case 'P':
                            var_3 = 'S';
                            break;

                        case 'K':
                            var_4 += 1;
                            if (var_4 > 4)
                            {
                                var_4 = 2;
                            }

                            var_3 = 'X';
                            break;

                        case 'M':
                            var_4 -= 1;
                            if (var_4 < 2)
                            {
                                var_4 = 4;
                            }
                            var_3 = 'X';
                            break;

                        default:
                            var_3 = 'X';
                            break;
                    }
                }

                if (var_3 == 0x0D)
                {
                    var_3 = 'R';
                }

                switch (var_3)
                {
                    case 'R':
                        var_1 = 1;
                        break;

                    case 'D':
                        var_4 = 4;
                        break;

                    case 'H':
                        var_4 = 3;
                        break;

                    case 'M':
                        var_4 = 2;
                        break;

                    case 'A':
                        if (var_4 == 2)
                        {
                            gbl.unk_1D890.field_2 += 5;
                        }
                        else if (var_4 == 3)
                        {
                            gbl.unk_1D890.field_6 += 1;
                        }
                        else if (var_4 == 4)
                        {
                            gbl.unk_1D890.field_8 += 1;
                        }
                        else
                        {
                            throw (new System.NotImplementedException("The original code only looked like var_4 could be 2,3 or 4 \r\n  at ovr021:083E"));
                        }

                        sub_583C8();
                        break;

                    case 'S':
                        if (var_4 == 2)
                        {
                            sub_5849F(1, 5);
                        }
                        else
                        {
                            sub_5849F(var_4, 1);
                        }

                        sub_583C8();
                        break;
                }
            } while (unk_58731.MemberOf(var_3) == false);

            return var_1;
        }


        internal static void reset_heal(byte arg_0)
        {
            Player player;
            byte var_1;

            gbl.word_1D8A6++;

            if (gbl.word_1D8A6 >= 0x120)
            {
                var_1 = 0;
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (ovr024.heal_player(0, 1, player) == true)
                    {
                        var_1 = 1;
                    }

                    player = player.next_player;
                }

                if (arg_0 != 0)
                {
                    sub_58615(0);
                }

                seg041.displayString("The Whole Party Is Healed", 0, 10, 19, 1);

                if (var_1 != 0)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                }

                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
                gbl.word_1D8A6 = 0;
            }
        }


        internal static byte rest_memorize(ref bool output, Player player)
        {
            int spell_index;
            byte var_2;

            var_2 = 0;
            spell_index = 0;

            while (spell_index <= 83 && var_2 == 0)
            {
                if (player.spell_list[spell_index] > 0x7F)
                {
                    if (output == true)
                    {
                        var_2 = (byte)gbl.unk_19AEC[player.spell_list[spell_index] & 0x7F].field_1;
                    }
                    else
                    {
                        player.spell_list[spell_index] -= 128;

                        sub_58615(0);

                        ovr023.cast_a_spell(player.spell_list[spell_index], "has memorized", player);
                        output = true;
                    }
                }

                spell_index++;
            }

            return var_2;
        }


        internal static byte reset_scribe(ref bool arg_0, Player player)
        {
            /*byte var_9;*/
            Item item;
            byte var_4;
            byte var_3;
            byte var_2;

            var_4 = 0;
            /*var_9 = 0;*/
            item = player.itemsPtr;

            while (item != null && var_4 == 0)
            {
                var_2 = 1;

                if (ovr023.item_is_scroll(item) == true)
                {
                    while (var_2 <= 3 && var_4 == 0)
                    {
                        if (item.getAffect(var_2) > (Affects)0x80)
                        {
                            /*var_9 = 1;*/

                            if (arg_0 == true)
                            {
                                var_4 = (byte)gbl.unk_19AEC[(int)item.getAffect(var_2) & 0x7F].field_1;
                            }
                            else
                            {
                                var_3 = (byte)((int)item.getAffect(var_2) & 0x7F);
                                player.field_79[var_3] = 1;
                                ovr023.sub_623FF(var_3, item, player);

                                sub_58615(0);

                                ovr023.cast_a_spell(var_3, "has scribed", player);
                                arg_0 = true;
                            }
                        }

                        var_2++;
                    }
                }

                item = item.next;
            }

            return var_4;
        }


        internal static void sub_58B4D()
        {
            bool var_7;
            Player player;
            byte var_2;
            byte var_1;

            var_1 = 1;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (gbl.unk_1D89D[var_1] > 0)
                {
                    gbl.unk_1D89D[var_1] -= 1;
                }

                if (gbl.unk_1D89D[var_1] != 0 &&
                    player.spell_to_learn_count != 0)
                {
                    var_7 = false;
                    var_2 = reset_scribe(ref var_7, player);

                    if (var_2 == 0)
                    {
                        var_2 = rest_memorize(ref var_7, player);
                    }

                    gbl.unk_1D89D[var_1] = (byte)(var_2 * 3);
                }

                player = player.next_player;
                var_1++;
            }
        }


        internal static void sub_58C03(ref byte arg_0)
        {
            bool var_7;
            Player player;
            byte var_2;
            byte var_1;

            arg_0 += 1;

            if (arg_0 >= 0x0C)
            {
                arg_0 = 0;

                var_1 = 1;
                player = gbl.player_next_ptr;
                while (player != null)
                {
                    if (player.spell_to_learn_count > 0 &&
                        --player.spell_to_learn_count == 0)
                    {
                        var_7 = true;
                        var_2 = reset_scribe(ref var_7, player);

                        if (var_2 == 0)
                        {
                            var_2 = rest_memorize(ref var_7, player);
                        }

                        gbl.unk_1D89D[var_1] = (byte)(var_2 * 2);
                    }

                    player = player.next_player;
                    var_1++;
                }
            }
        }


        internal static byte reseting(byte arg_0)
        {
            bool var_E;
            byte var_D;
            byte var_C;
            byte var_B;
            Player var_5;
            byte var_1;

            var_1 = 0;
            var_B = 1;
            var_5 = gbl.player_next_ptr;

            while (var_5 != null)
            {
                gbl.unk_1D89D[var_B] = 0;
                var_B++;
                var_5 = var_5.next_player;
            }

            seg051.FillChar(1, 0x48, gbl.unk_1AE24);
            var_C = 0;
            var_D = 0;

            if (arg_0 != 0)
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
                sub_58615(0);
            }

            gbl.byte_1D8A8 = 1;

            if (arg_0 != 0)
            {
                var_E = (sub_58751() == 0);
            }
            else
            {
                var_E = false;
            }

            while (var_E == false &&
                (gbl.unk_1D890.field_8 > 0 ||
                  gbl.unk_1D890.field_6 > 0 ||
                  gbl.unk_1D890.field_4 > 0 ||
                  gbl.unk_1D890.field_2 > 0))
            {
                if (arg_0 != 0 &&
                    seg049.KEYPRESSED() == true)
                {
                    sub_58615(0);

                    if (ovr027.yes_no(15, 10, 13, "Stop Resting? ") == 'Y')
                    {
                        var_E = true;
                    }
                    else
                    {
                        ovr027.redraw_screen();
                    }
                }

                if (var_E == false)
                {
                    sub_5849F(1, 5);
                    var_D++;

                    if (arg_0 != 0 &&
                        var_D >= 5)
                    {
                        sub_58615(0);
                        var_D = 0;
                    }

                    sub_583FA(1, 5);
                    reset_heal(arg_0);
                    sub_58B4D();
                    sub_58C03(ref var_C);

                    if (gbl.area2_ptr.field_5A4 > 0)
                    {
                        gbl.word_1B2EC++;

                        if (gbl.word_1B2EC >= gbl.area2_ptr.field_5A4)
                        {
                            gbl.word_1B2EC = 0;

                            if (ovr024.roll_dice(100, 1) <= gbl.area2_ptr.field_5A6)
                            {
                                ovr025.ClearPlayerTextArea();
                                sub_58615(0);
                                seg041.displayString("Your repose is suddenly interrupted!", 0, 15, 0x13, 1);
                                var_E = true;
                                var_1 = 1;
                                seg041.GameDelay();
                            }
                        }
                    }
                }
            }

            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
            gbl.byte_1D8A8 = 0;

            return var_1;
        }
    }
}
