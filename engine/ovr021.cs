using Classes;
using System.Collections.Generic;

namespace engine
{
    class ovr021
    {
        static int[] timeScales = { 10, 10, 6, 24, 30, 12, 0x100 }; //word_1A13C


        static void CheckAffectsTimingOut(int timeSlot, int timeSteps) // sub_5801E
        {
            if (gbl.game_state != GameState.Camping)
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
                } while (var_B == false && player_count < gbl.area2_ptr.party_size);

                if (var_B == false)
                {
                    return;
                }
            }

            int var_5 = timeSteps;

            while (timeSlot > 1)
            {
                var_5 *= timeScales[timeSlot - 1];
                timeSlot -= 1;
            }

            while (var_5 > 0)
            {
                int var_3 = System.Math.Min(10, var_5);

                int player_count = 0;

                foreach (Player player in gbl.TeamList)
                {
                    if (gbl.affects_timed_out[player_count] == true)
                    {
                        gbl.affects_timed_out[player_count] = false;

                        List<Affect> removeList = new List<Affect>();

                        foreach (Affect affect in player.affects)
                        {
                            if (affect.minutes == 0)
                            {
                                // Do nothing
                            }
                            else if (var_3 < affect.minutes)
                            {
                                affect.minutes -= (ushort)var_3;
                                gbl.affects_timed_out[player_count] = true;
                            }
                            else
                            {
                                removeList.Add(affect);
                            }
                        }

                        foreach (Affect remove in removeList)
                        {
                            ovr024.remove_affect(remove, remove.type, player);
                        }

                        // Not sure why we are doing this again, but this is what the orig code did...
                        foreach (Affect affect in player.affects)
                        {
                            if (affect.minutes > 0)
                            {
                                gbl.affects_timed_out[player_count] = true;
                            }
                        }
                    }

                    player_count++;
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


        static void NormalizeClock(RestTime arg_0) /* sub_58317 */
        {
            for (int i = 0; i <= 6; i++)
            {
                if (arg_0[i] >= timeScales[i])/* short arrays */
                {
                    if (i != 6)
                    {
                        arg_0[i + 1] += 1;
                        arg_0[i] -= timeScales[i]; ;
                    }
                    else
                    {
                        foreach (Player player in gbl.TeamList)
                        {
                            player.age += 1;
                        }
                    }
                }
            }
        }


        static void clock_583C8() /* sub_583C8 */
        {
            NormalizeClock(gbl.timeToRest);

            if (gbl.timeToRest.field_A > 0)
            {
                gbl.timeToRest.field_8 += timeScales[4] * gbl.timeToRest.field_A;

                gbl.timeToRest.field_A = 0;

                if (gbl.timeToRest.field_8 > 99)
                {
                    gbl.timeToRest.field_8 = 99;
                }
            }
        }

        internal static void step_game_time(int time_slot, int amount) /* sub_583FA */
        {
            RestTime rest_time = new RestTime();

            for (int i = 0; i <= 6; i++)
            {
                rest_time[i] = gbl.area_ptr.field_6A00_Get(0x6A00 + ((0x4BC6 + i) * 2)); // as WORD[]
            }

            for (int i = 1; i <= amount; i++)
            {
                rest_time[time_slot] += 1;

                NormalizeClock(rest_time);
            }

            for (int i = 0; i <= 6; i++)
            {
                gbl.area_ptr.field_6A00_Set(0x6A00 + ((0x4BC6 + i) * 2), (ushort)rest_time[i]);
            }

            CheckAffectsTimingOut(time_slot, amount);
        }


        internal static void rest_time_5849F(int time_index, byte arg_2) /* sub_5849F */
        {
            if (gbl.timeToRest.field_8 != 0 ||
                gbl.timeToRest.field_6 != 0 ||
                gbl.timeToRest.field_4 != 0 ||
                gbl.timeToRest.field_2 != 0)
            {
                while (arg_2 > gbl.timeToRest[time_index])
                {
                    int var_1 = time_index + 1;

                    while (gbl.timeToRest[var_1] == 0 &&
                        var_1 < 5)
                    {
                        var_1 += 1;
                    }

                    if (var_1 == 5)
                    {
                        gbl.timeToRest.Clear();
                        arg_2 = 0;
                    }
                    else
                    {
                        for (int i = var_1; i >= (time_index + 1); i--)
                        {
                            gbl.timeToRest[i] -= 1;
                            gbl.timeToRest[i - 1] += timeScales[i - 1];
                        }
                    }
                }

                gbl.timeToRest[time_index] -= arg_2;

                clock_583C8();
            }
        }


        static string format_time(int value) /* sub_5858A */
        {
            return string.Format("{0:00}", value);
        }


        static void display_resting_time(int highlight_time) /* sub_58615 */
        {
            int[] colors = new int[6];

            for (int index = 0; index < 6; index++)
            {
                colors[index] = 10;
            }

            colors[highlight_time] = 15;

            seg041.displayString("Rest Time:", 0, 10, 17, 1);
            int col_x = 11;

            string text = format_time(gbl.timeToRest.field_8);
            seg041.displayString(text, 0, colors[4], 0x11, col_x + 1);
            seg041.displayString(":", 0, 10, 17, col_x + 3);
            col_x += 3;

            text = format_time(gbl.timeToRest.field_6);
            seg041.displayString(text, 0, colors[3], 0x11, col_x + 1);
            seg041.displayString(":", 0, 10, 17, col_x + 3);
            col_x += 3;

            text = format_time((gbl.timeToRest.field_4 * 10) + gbl.timeToRest.field_2);

            seg041.displayString(text, 0, colors[2], 0x11, col_x + 1);
        }


        static Set unk_58731 = new Set(0, 69, 82 );

        static bool resting_time_menu() /* sub_58751 */
        {
            char input_key;

            bool resting = false;
            int time_index = 2;

            do
            {
                display_resting_time(time_index);
                bool control_key;

                input_key = ovr027.displayInput(out control_key, false, 1, gbl.defaultMenuColors, "Rest Days Hours Mins Add Subtract Exit", string.Empty);

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
                            gbl.timeToRest.field_2 += 5;
                        }
                        else
                        {
                            gbl.timeToRest[time_index] += 1;
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


        static void rest_heal(bool show_text) /* reset_heal */
        {
            gbl.rest_10_seconds++;

            if (gbl.rest_10_seconds >= (8 * 36))
            {
                bool update_ui = false;

                foreach (Player player in gbl.TeamList)
                {
                    if (ovr024.heal_player(0, 1, player) == true)
                    {
                        update_ui = true;
                    }
                }

                if (show_text == true)
                {
                    display_resting_time(0);
                }

                seg041.displayString("The Whole Party Is Healed", 0, 10, 19, 1);

                if (update_ui)
                {
                    ovr025.PartySummary(gbl.SelectedPlayer);
                }

                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
                gbl.rest_10_seconds = 0;
            }
        }


        static int rest_memorize(ref bool findNext, Player player)
        {
            foreach (int id in player.spellList.LearningList())
            {
                if (findNext == true)
                {
                    return gbl.spellCastingTable[id].spellLevel;
                }
                else
                {
                    player.spellList.MarkLearnt(id);

                    display_resting_time(0);

                    ovr023.DisplayCaseSpellText(id, "has memorized", player);
                    findNext = true;
                }
            }

            return 0;
        }


        static int rest_scribe(ref bool findNext, Player player)
        {
            int next_scribe_lvl = 0;
            foreach (Item item in player.items.ToArray())
            {
                if (item.IsScroll() == true)
                {
                    for (int spellIdx = 1; spellIdx < 4 && next_scribe_lvl == 0; spellIdx++)
                    {
                        if (item.getAffect(spellIdx) > (Affects)0x80)
                        {
                            if (findNext == true)
                            {
                                next_scribe_lvl = gbl.spellCastingTable[(int)item.getAffect(spellIdx) & 0x7F].spellLevel;
                            }
                            else
                            {
                                byte spellId = (byte)((int)item.getAffect(spellIdx) & 0x7F);
                                player.spellBook.LearnSpell((Spells)spellId);
                                ovr023.remove_spell_from_scroll(spellId, item, player);

                                display_resting_time(0);

                                ovr023.DisplayCaseSpellText(spellId, "has scribed", player);
                                findNext = true;
                            }
                        }
                    }
                }

                if (next_scribe_lvl != 0) break;
            }

            return next_scribe_lvl;
        }

        static int[] spellLaernTimeout = new int[9]; // seg600:758D

        static void CheckForSpellLearning() // sub_58B4D
        {
            int index = 1;
            foreach (Player player in gbl.TeamList)
            {
                if (spellLaernTimeout[index] > 0)
                {
                    spellLaernTimeout[index] -= 1;
                }

                if (spellLaernTimeout[index] == 0 &&
                    player.spell_to_learn_count == 0)
                {
                    bool var_7 = false;
                    int next_lvl = rest_scribe(ref var_7, player);

                    if (next_lvl == 0)
                    {
                        next_lvl = rest_memorize(ref var_7, player);
                    }

                    spellLaernTimeout[index] = next_lvl * 3;
                }

                index++;
            }
        }


        static void sub_58C03(ref int arg_0)
        {
            arg_0 += 1;

            if (arg_0 >= 12)
            {
                arg_0 = 0;

                int index = 1;
                foreach (Player player in gbl.TeamList)
                {
                    if (player.spell_to_learn_count > 0 &&
                        --player.spell_to_learn_count == 0)
                    {
                        bool var_7 = true;
                        int next_lvl = rest_scribe(ref var_7, player);

                        if (next_lvl == 0)
                        {
                            next_lvl = rest_memorize(ref var_7, player);
                        }

                        spellLaernTimeout[index] = next_lvl * 2;
                    }

                    index++;
                }
            }
        }

        /// <summary>
        /// returns if the party is interrupted
        /// </summary>
        internal static bool resting(bool interactive_resting) /* reseting */
        {
            bool stop_resting;
            bool resting_intetrupted = false;

            System.Array.Clear(spellLaernTimeout, 0, gbl.TeamList.Count);

            for (int i = 0; i < 0x48; i++)
            {
                gbl.affects_timed_out[i] = true;
            }

            int var_C = 0;
            int display_counter = 0;

            if (interactive_resting == true)
            {
                seg037.draw8x8_clear_area(TextRegion.NormalBottom);
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
                (gbl.timeToRest.field_8 > 0 ||
                 gbl.timeToRest.field_6 > 0 ||
                 gbl.timeToRest.field_4 > 0 ||
                 gbl.timeToRest.field_2 > 0))
            {
                if (interactive_resting == true &&
                    seg049.KEYPRESSED() == true)
                {
                    display_resting_time(0);

                    if (ovr027.yes_no(gbl.defaultMenuColors, "Stop Resting? ") == 'Y')
                    {
                        stop_resting = true;
                    }
                    else
                    {
                        ovr027.ClearPromptArea();
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

                    step_game_time(1, 5);
                    rest_heal(interactive_resting);
                    CheckForSpellLearning();
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
                                resting_intetrupted = true;
                                seg041.GameDelay();
                            }
                        }
                    }
                }
            }

            seg037.draw8x8_clear_area(TextRegion.NormalBottom);
            gbl.displayPlayerStatusLine18 = false;

            return resting_intetrupted;
        }
    }
}
