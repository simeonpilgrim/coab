using Classes;

namespace engine
{
    class ovr021
    {
        internal static void sub_5801E(int arg_0, int arg_2)
        {
            if (gbl.game_state != 2)
            {
                for (int i = 0; i < 0x48; i++)
                {
                    gbl.affects_timed_out[i] = true;
                }
            }
            else
            {
                bool var_B = false;
                byte player_count = 0;

                do
                {
                    if (gbl.affects_timed_out[player_count] == true)
                    {
                        var_B = true;
                    }

                    player_count++;
                } while (var_B == false && player_count < gbl.area2_ptr.field_67C);

                if (var_B == false)
                {
                    return;
                }
            }

            int var_5 = arg_2;
            int var_1 = arg_0;

            while (var_1 > 1)
            {
                var_5 *= gbl.word_1A13C[var_1 - 1];
                var_1 -= 1;
            }

            while (var_5 > 0)
            {
                int var_3 = System.Math.Min(10, var_5);

                int player_count = 0;
                Player player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (gbl.affects_timed_out[player_count] == true)
                    {
                        gbl.affects_timed_out[player_count] = false;

                        Affect affect_a = player.affect_ptr;
                        Affect affect_b = player.affect_ptr;
                        Affect affect_c = player.affect_ptr;

                        if (player.affect_ptr != null)
                        {
                            while (affect_b.next != null)
                            {
                                affect_b = affect_b.next;
                            }
                        }

                        bool last = false;

                        while (affect_a != null &&
                            last == false)
                        {
                            if (affect_a == affect_b)
                            {
                                last = true;
                            }

                            if (affect_a.field_1 == 0)
                            {
                                affect_a = affect_a.next;
                            }
                            else if (var_3 < affect_a.field_1)
                            {
                                affect_a.field_1 -= (ushort)var_3;
                                gbl.affects_timed_out[player_count] = true;

                                affect_a = affect_a.next;
                            }
                            else
                            {
                                Affect next_affect = affect_a.next;

                                ovr024.remove_affect(affect_a, affect_a.type, player);

                                if (affect_c == player.affect_ptr)
                                {
                                    affect_a = player.affect_ptr;
                                }
                                else
                                {
                                    affect_a = next_affect;
                                }
                            }

                            affect_c = player.affect_ptr;

                            while (affect_c != null &&
                                affect_c != affect_a &&
                                affect_c.next != affect_a)
                            {
                                affect_c = affect_c.next;
                            }
                        }

                        while (affect_a != null)
                        {
                            if (affect_a.field_1 > 0)
                            {
                                gbl.affects_timed_out[player_count] = true;
                            }

                            affect_a = affect_a.next;
                        }
                    }

                    player_count++;
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


        internal static void normalize_clock(RestTime arg_0) /* sub_58317 */
        {
            for (int i = 0; i <= 6; i++)
            {
                if (arg_0[i] >= gbl.word_1A13C[i])/* short arrays */
                {
                    if (i != 6)
                    {
                        arg_0[i + 1] += 1;
                        arg_0[i] -= gbl.word_1A13C[i]; ;
                    }
                    else
                    {
                        Player player = gbl.player_next_ptr;

                        while (player != null)
                        {
                            player.age += 1;

                            player = player.next_player;
                        }
                    }
                }
            }
        }


        internal static void clock_583C8() /* sub_583C8 */
        {
            normalize_clock(gbl.unk_1D890);

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
        static ushort[] time_table = { 0x00AE, 0x010E, 0x00AE, 0x0136, 0x00AE, 0x016C, 0x00AE };

        internal static void sub_583FA(int arg_0, int arg_2) /* sub_583FA */
        {
            RestTime rest_time = new RestTime();

            for (int i = 0; i <= 6; i++)
            {
                rest_time[i] = gbl.area_ptr.field_6A00_Get(time_table[i]); // as WORD[]
            }

            for (int i = 1; i <= arg_2; i++)
            {
                rest_time[arg_0] += 1;

                normalize_clock(rest_time);
            }

            for (int i = 0; i <= 6; i++)
            {
                gbl.area_ptr.field_6A00_Set(time_table[i], rest_time[i]);
            }

            sub_5801E(arg_0, arg_2);
        }


        internal static void rest_time_5849F(int time_index, byte arg_2) /* sub_5849F */
        {
            if (gbl.unk_1D890.field_8 != 0 ||
                gbl.unk_1D890.field_6 != 0 ||
                gbl.unk_1D890.field_4 != 0 ||
                gbl.unk_1D890.field_2 != 0)
            {
                while (arg_2 > gbl.unk_1D890[time_index])
                {
                    int var_1 = time_index + 1;

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
                        for (int i = var_1; i >= (time_index + 1); i--)
                        {
                            gbl.unk_1D890[i] -= 1;
                            gbl.unk_1D890[i - 1] += gbl.word_1A13C[i - 1];
                        }
                    }
                }

                gbl.unk_1D890[time_index] -= arg_2;

                clock_583C8();
            }
        }


        static string format_time(int value) /* sub_5858A */
        {
            return string.Format("{0:00}", value);
        }


        internal static void display_resting_time(int highlight_time) /* sub_58615 */
        {
            int[] colors = new int[6];

            for (int index = 0; index < 6; index++)
            {
                colors[index] = 10;
            }

            colors[highlight_time] = 15;

            seg041.displayString("Rest Time:", 0, 10, 17, 1);
            int col_x = 11;

            string text = format_time(gbl.unk_1D890.field_8);
            seg041.displayString(text, 0, colors[4], 0x11, col_x + 1);
            seg041.displayString(":", 0, 10, 17, col_x + 3);
            col_x += 3;

            text = format_time(gbl.unk_1D890.field_6);
            seg041.displayString(text, 0, colors[3], 0x11, col_x + 1);
            seg041.displayString(":", 0, 10, 17, col_x + 3);
            col_x += 3;

            text = format_time((gbl.unk_1D890.field_4 * 10) + gbl.unk_1D890.field_2);

            seg041.displayString(text, 0, colors[2], 0x11, col_x + 1);
        }


        static Set unk_58731 = new Set(0x000B, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x04 });

        internal static bool resting_time_menu() /* sub_58751 */
        {
            char input_key;

            bool resting = false;
            int time_index = 2;

            do
            {
                display_resting_time(time_index);
                bool control_key;

                input_key = ovr027.displayInput(out control_key, false, 1, 15, 10, 13, "Rest Days Hours Mins Add Subtract Exit", string.Empty);

                if (control_key == true)
                {
                    switch (input_key)
                    {
                        case 'H':
                            input_key = 'A';
                            break;

                        case 'P':
                            input_key = 'S';
                            break;

                        case 'K':
                            time_index += 1;
                            if (time_index > 4)
                            {
                                time_index = 2;
                            }

                            input_key = 'X';
                            break;

                        case 'M':
                            time_index -= 1;
                            if (time_index < 2)
                            {
                                time_index = 4;
                            }
                            input_key = 'X';
                            break;

                        default:
                            input_key = 'X';
                            break;
                    }
                }

                if (input_key == 0x0D)
                {
                    input_key = 'R';
                }

                switch (input_key)
                {
                    case 'R':
                        resting = true;
                        break;

                    case 'D':
                        time_index = 4;
                        break;

                    case 'H':
                        time_index = 3;
                        break;

                    case 'M':
                        time_index = 2;
                        break;

                    case 'A':
                        if (time_index == 2)
                        {
                            gbl.unk_1D890.field_2 += 5;
                        }
                        else
                        {
                            gbl.unk_1D890[time_index] += 1;
                        }

                        clock_583C8();
                        break;

                    case 'S':
                        if (time_index == 2)
                        {
                            rest_time_5849F(1, 5);
                        }
                        else
                        {
                            rest_time_5849F(time_index, 1);
                        }

                        clock_583C8();
                        break;
                }
            } while (unk_58731.MemberOf(input_key) == false);

            return resting;
        }


        internal static void rest_heal(bool show_text) /* reset_heal */
        {
            gbl.rest_10_seconds++;

            if (gbl.rest_10_seconds >= (8 * 36))
            {
                bool update_ui = false;
                Player player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (ovr024.heal_player(0, 1, player) == true)
                    {
                        update_ui = true;
                    }

                    player = player.next_player;
                }

                if (show_text == true)
                {
                    display_resting_time(0);
                }

                seg041.displayString("The Whole Party Is Healed", 0, 10, 19, 1);

                if (update_ui)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                }

                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
                gbl.rest_10_seconds = 0;
            }
        }


        internal static byte rest_memorize(ref bool output, Player player)
        {
            byte var_2 = 0;
            int spell_index = 0;

            while (spell_index <= 83 && var_2 == 0)
            {
                if (player.spell_list[spell_index] > 0x7F)
                {
                    if (output == true)
                    {
                        var_2 = (byte)gbl.spell_table[player.spell_list[spell_index] & 0x7F].spellLevel;
                    }
                    else
                    {
                        player.spell_list[spell_index] -= 128;

                        display_resting_time(0);

                        ovr023.cast_spell_text(player.spell_list[spell_index], "has memorized", player);
                        output = true;
                    }
                }

                spell_index++;
            }

            return var_2;
        }


        internal static byte reset_scribe(ref bool arg_0, Player player)
        {
            /*byte var_9 = 0;*/

            byte var_4 = 0;
            Item item = player.itemsPtr;

            while (item != null && var_4 == 0)
            {
                int var_2 = 1;

                if (ovr023.item_is_scroll(item) == true)
                {
                    while (var_2 <= 3 && var_4 == 0)
                    {
                        if (item.getAffect(var_2) > (Affects)0x80)
                        {
                            /*var_9 = 1;*/

                            if (arg_0 == true)
                            {
                                var_4 = (byte)gbl.spell_table[(int)item.getAffect(var_2) & 0x7F].spellLevel;
                            }
                            else
                            {
                                byte affect = (byte)((int)item.getAffect(var_2) & 0x7F);
                                player.field_79[affect - 1] = 1;
                                ovr023.remove_spell_from_scroll(affect, item, player);

                                display_resting_time(0);

                                ovr023.cast_spell_text(affect, "has scribed", player);
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
            int var_1 = 1;
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (gbl.unk_1D89D[var_1] > 0)
                {
                    gbl.unk_1D89D[var_1] -= 1;
                }

                if (gbl.unk_1D89D[var_1] == 0 &&
                    player.spell_to_learn_count == 0)
                {
                    bool var_7 = false;
                    byte var_2 = reset_scribe(ref var_7, player);

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


        internal static void sub_58C03(ref int arg_0)
        {
            arg_0 += 1;

            if (arg_0 >= 12)
            {
                arg_0 = 0;

                int var_1 = 1;
                Player player = gbl.player_next_ptr;
                while (player != null)
                {
                    if (player.spell_to_learn_count > 0 &&
                        --player.spell_to_learn_count == 0)
                    {
                        bool var_7 = true;
                        byte var_2 = reset_scribe(ref var_7, player);

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


        internal static bool resting(bool interactive_resting) /* reseting */
        {
            bool stop_resting;
            bool resting_interupted = false;

            int var_B = 1;
            Player var_5 = gbl.player_next_ptr;

            while (var_5 != null)
            {
                gbl.unk_1D89D[var_B] = 0;
                var_B++;
                var_5 = var_5.next_player;
            }

            for (int i = 0; i < 0x48; i++)
            {
                gbl.affects_timed_out[i] = true;
            }

            int var_C = 0;
            int display_counter = 0;

            if (interactive_resting == true)
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
                display_resting_time(0);
            }

            gbl.displayPlayerStatusLine18 = true;

            if (interactive_resting == true)
            {
                stop_resting = !resting_time_menu();
            }
            else
            {
                stop_resting = false;
            }

            while (stop_resting == false &&
                (gbl.unk_1D890.field_8 > 0 ||
                 gbl.unk_1D890.field_6 > 0 ||
                 gbl.unk_1D890.field_4 > 0 ||
                 gbl.unk_1D890.field_2 > 0))
            {
                if (interactive_resting == true &&
                    seg049.KEYPRESSED() == true)
                {
                    display_resting_time(0);

                    if (ovr027.yes_no(15, 10, 13, "Stop Resting? ") == 'Y')
                    {
                        stop_resting = true;
                    }
                    else
                    {
                        ovr027.redraw_screen();
                    }
                }

                if (stop_resting == false)
                {
                    rest_time_5849F(1, 5);
                    display_counter++;

                    if (interactive_resting == true &&
                        display_counter >= 5)
                    {
                        display_resting_time(0);
                        display_counter = 0;
                    }

                    sub_583FA(1, 5);
                    rest_heal(interactive_resting);
                    sub_58B4D();
                    sub_58C03(ref var_C);

                    if (gbl.area2_ptr.rest_incounter_period > 0)
                    {
                        gbl.rest_incounter_count++;

                        if (gbl.rest_incounter_count >= gbl.area2_ptr.rest_incounter_period)
                        {
                            gbl.rest_incounter_count = 0;

                            if (ovr024.roll_dice(100, 1) <= gbl.area2_ptr.rest_incounter_percentage)
                            {
                                ovr025.ClearPlayerTextArea();
                                display_resting_time(0);
                                seg041.displayString("Your repose is suddenly interrupted!", 0, 15, 0x13, 1);
                                stop_resting = true;
                                resting_interupted = true;
                                seg041.GameDelay();
                            }
                        }
                    }
                }
            }

            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);
            gbl.displayPlayerStatusLine18 = false;

            return resting_interupted;
        }
    }
}
