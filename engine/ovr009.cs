
using Classes;

namespace engine
{
    class ovr009
    {
        internal static void sub_3304B()
        {
            Struct_1D885 var_4;

            while (gbl.stru_1D885 != null)
            {
                var_4 = gbl.stru_1D885.next;

                seg051.FreeMem(0x1E, gbl.stru_1D885);

                gbl.stru_1D885 = var_4;
            }

            while (gbl.stru_1D889 != null)
            {
                var_4 = gbl.stru_1D889.next;

                seg051.FreeMem(0x1E, gbl.stru_1D889);

                gbl.stru_1D889 = var_4;
            }

            seg051.FreeMem(0x4e9, gbl.stru_1D1BC);
            gbl.stru_1D1BC = null;

            seg040.free_dax_block(ref gbl.dword_1D90A);
            ovr033.sub_74130();
            gbl.dword_1D5CA = new Classes.spellDelegate(ovr023.cast_spell_on);
        }


        internal static void sub_33100()
        {
            Player playerbase_ptr;
            bool var_1;

            gbl.game_state = 5;
            gbl.dword_1D5CA = new Classes.spellDelegate(ovr014.target);
            ovr011.battle_begins();
            var_1 = false;

            if (gbl.friends_count == 0 ||
                gbl.foe_count == 0)
            {
                var_1 = true;
            }

            while (var_1 == false)
            {
                ovr025.count_teams();
                playerbase_ptr = gbl.player_next_ptr;

                while (playerbase_ptr != null)
                {
                    ovr014.sub_3E000(playerbase_ptr);

                    playerbase_ptr = playerbase_ptr.next_player;
                }

                gbl.area2_ptr.field_596 = 0;

                sub_331BC(ref playerbase_ptr);

                while (playerbase_ptr != null)
                {
                    sub_33281(playerbase_ptr);
                    sub_331BC(ref playerbase_ptr);
                }

                battle01(ref var_1);
            }

            sub_3304B();
            gbl.byte_1B2F2 = 1;
        }


        internal static void sub_331BC(ref Player arg_0)
        {
            Player player;
            byte var_3;
            byte var_2;
            sbyte var_1;

            var_1 = 0;
            var_2 = 0;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                var_3 = ovr024.roll_dice(100, 1);

                if (player.actions.delay > var_1)
                {
                    var_2 = var_3;
                }

                if (player.actions.delay > var_1 &&
                    var_3 >= var_2)
                {
                    var_2 = var_3;
                    var_1 = player.actions.delay;

                    arg_0 = player;
                }

                player = player.next_player;
            }

            if (var_1 == 0)
            {
                arg_0 = null;
            }
        }


        internal static void sub_33281(Player player)
        {
            player.actions.field_F = 0;
            player.actions.field_12 = 0;
            player.actions.guarding = false;
            ovr024.work_on_00(player, 7);

            if (player.actions.delay > 0)
            {
                if (player.actions.delay == 20)
                {
                    player.actions.delay = 19;
                }

                gbl.player_ptr = player;

                gbl.byte_1D910 = ( (player.combat_team == 0) || (ovr033.sub_74761(0, player) == true) );

                ovr033.sub_75356(1, 2, player);
                ovr025.sub_66C20(player);
                gbl.byte_1D90F = 1;
                ovr025.hitpoint_ac(player);
                ovr024.work_on_00(player, 15);

                if (player.actions.spell_id == 0)
                {
                    ovr024.work_on_00(player, 0x15);
                }

                if (player.actions.delay > 0)
                {
                    if (player.field_198 != 0)
                    {
                        ovr010.sub_3504B(player);
                    }
                    else
                    {
                        camp_menu(player);
                    }
                }

                ovr033.sub_7431C(ovr033.sub_74C5A(player), ovr033.sub_74C32(player));
            }
        }


        internal static void camp_menu(Player player)
        {
            byte spell_id;
            Struct_1D183 var_D = new Struct_1D183();
            Player player_ptr;
            bool var_2 = false; /*HACK was unassigned in ovr023.sub_5D2E1 call */
            char var_1;

            if (player.in_combat == true)
            {
                if (player.actions.spell_id > 0)
                {

                    spell_id = player.actions.spell_id;
                    player.actions.spell_id = 0;

                    ovr023.sub_5D2E1(ref var_2, 1, 0, spell_id);
                    var_2 = ovr025.clear_actions(player);
                }
                else
                {
                    var_2 = false;

                    while (var_2 == false)
                    {
                        combat_menu(out var_1, player);

                        if (gbl.byte_1D5BF == false)
                        {
                            switch (var_1)
                            {
                                case 'Q':
                                    sub_3432F(player);
                                    ovr027.redraw_screen();
                                    seg043.clear_keyboard();
                                    seg049.SysDelay(0x0C8);
                                    var_2 = true;
                                    ovr010.sub_3504B(player);
                                    break;

                                case 'M':
                                    sub_33B26(ref var_2, ' ', player);
                                    break;

                                case 'V':
                                    ovr020.viewPlayer(out var_2);
                                    ovr014.sub_3EDD4(player);
                                    if (var_2 == false)
                                    {
                                        ovr025.sub_68DC0();
                                    }
                                    break;

                                case 'A':
                                    ovr014.sub_41B25( var_D, out var_2, 1, 0, 1, 0xff, player);
                                    break;

                                case 'U':
                                    gbl.byte_1D5BE = 2;
                                    ovr020.use_item(ref var_2);
                                    ovr014.sub_3EDD4(player);
                                    if (var_2 == false)
                                    {
                                        ovr025.sub_68DC0();
                                    }
                                    break;

                                case 'C':
                                    ovr014.spell_menu3(out var_2, 0, 0);
                                    break;

                                case 'T':
                                    ovr014.turns_undead(player);
                                    var_2 = ovr025.clear_actions(player);
                                    break;

                                case 'D':
                                    delay_menu(ref var_2, player);
                                    break;

                                case ' ':
                                    /* Turn off auto-fight. */
                                    player_ptr = gbl.player_next_ptr;

                                    while (player_ptr != null)
                                    {
                                        if (player_ptr.field_F7 < 0x80)
                                        {
                                            player_ptr.field_198 = 0;
                                        }
                                        player_ptr = player_ptr.next_player;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (var_1)
                            {
                                case 'G':
                                    goto case 'I';
                                case 'H':
                                    goto case 'I';
                                case 'K':
                                    goto case 'I';
                                case 'M':
                                    goto case 'I';
                                case 'O':
                                    goto case 'I';
                                case 'P':
                                    goto case 'I';
                                case 'Q':
                                    goto case 'I';
                                case 'I':
                                    sub_33B26(ref var_2, var_1, player);
                                    break;

                                case '2':
                                    gbl.byte_1D904 = !gbl.byte_1D904;

                                    if (gbl.byte_1D904 == true)
                                    {
                                        ovr025.string_print01("Magic On");
                                    }
                                    else
                                    {
                                        ovr025.string_print01("Magic Off");
                                    }
                                    break;

                                case (char)0x10:
                                    player.actions.delay = 20;
                                    player_ptr = gbl.player_next_ptr;
                                    while (player_ptr != null)
                                    {
                                        sub_3432F(player_ptr);
                                        player_ptr = player_ptr.next_player;
                                    }
                                    ovr027.redraw_screen();
                                    seg049.SysDelay(0x0C8);

                                    var_2 = true;
                                    break;

                                case '-':
                                    if (ovr014.god_intervene() == true)
                                    {
                                        ovr033.sub_75356(0, 3, player);
                                        var_2 = true;
                                    }
                                    else
                                    {
                                        ovr025.string_print01("That doesn't work");
                                    }
                                    break;
                            }
                        }

                        if (var_2 == false)
                        {
                            ovr033.sub_75356(1, 2, player);
                            gbl.byte_1D90F = 1;
                            ovr025.hitpoint_ac(player);
                        }
                    }
                }
            }
            else
            {
                var_2 = ovr025.clear_actions(player);
            }
        }

        static Set unk_33748 = new Set(0x0209, new byte[] { 0x09, 0x00, 0x00, 0x20, 0x04, 0x00, 0x80, 0xAB, 0x03 });
        static Set unk_33768 = new Set(0x0209, new byte[] { 0x09, 0x00, 0x01, 0x20, 0x04, 0x00, 0x9A, 0xAB, 0x73 });


        internal static void combat_menu(out char arg_0, Player player)
        {
            bool var_2B;
            byte var_2A;
            string var_29;

            var_29 = string.Empty;

            if (player.actions.move > 0)
            {
                var_29 += "Move ";
            }

            var_29 += "View Aim ";

            if (player.field_14C > 0)
            {
                var_29 += "Use ";
            }

            var_2B = false;

            for (var_2A = 0; var_2A <= 0x53; var_2A++)
            {
                if (player.spell_list[var_2A] > 0)
                {
                    var_2B = true;
                }
            }

            if (var_2B == true &&
                player.actions.can_cast != 0 &&
                gbl.area_ptr.can_cast_spells == false)
            {
                var_29 += "Cast ";
            }

            if (player.cleric_lvl > 0 ||
                (player.turn_undead > 0 && ovr026.sub_6B3D1(player) != 0))
            {
                if (player.actions.field_11 == 0)
                {
                    var_29 += "Turn ";
                }
            }

            var_29 += "Quick Done";

            do
            {
                arg_0 = ovr027.displayInput(out var_2B, 0, 1, 15, 10, 13, var_29, string.Empty);

                if (var_2B == true &&
                    unk_33748.MemberOf(arg_0) == false)
                {
                    arg_0 = '\0';
                }

            } while (unk_33768.MemberOf(arg_0) == false);

            ovr027.redraw_screen();
        }


        internal static void battle01(ref bool arg_0)
        {
            byte var_6;
            Player player;

            ovr021.sub_583FA(1, 1);
            gbl.byte_1D8B7++;
            ovr014.sub_40E00();
            player = gbl.player_next_ptr;
            var_6 = 1;

            while (player != null)
            {
                ovr024.work_on_00(player, 0x13);
                ovr024.in_poison_cloud(0, player);

                if (player.health_status == Status.dying)
                {
                    player.actions.bleeding += 1;

                    if (player.actions.bleeding > 9)
                    {
                        player.health_status = Status.dead;
                    }

                }
                var_6++;
                player = player.next_player;
            }

            if (ovr025.bandage(false))
            {
                ovr025.string_print01("Your Teammate is Dying");
            }

            ovr025.count_teams();

            ovr033.sub_749DD(8, 0xff, (sbyte)(gbl.stru_1D1BC.field_3 + 3), (sbyte)(gbl.stru_1D1BC.field_2 + 3));

            if (gbl.friends_count == 0 ||
                gbl.foe_count == 0 ||
                gbl.byte_1D8B7 >= gbl.byte_1D8B8)
            {
                arg_0 = true;
            }

            if (gbl.friends_count > 1 &&
                gbl.foe_count == 0 &&
                gbl.inDemo == false &&
                ovr027.yes_no(15, 10, 13, "Continue Battle:") == 'Y')
            {
                arg_0 = false;
            }
        }


        internal static void sub_33B26(ref bool arg_0, char arg_4, Player player)
        {
            string var_20D;
            int var_D;
            byte var_C;
            byte var_B;
            char var_A;
            byte var_9;
            byte var_8;
            byte var_7;
            sbyte var_4;
            sbyte var_3;
            byte var_2;
            byte var_1;

            var_1 = player.actions.move;
            var_2 = player.actions.field_9;
            var_3 = ovr033.sub_74C32(player);
            var_4 = ovr033.sub_74C5A(player);
            var_A = arg_4;

            arg_0 = false;
            var_7 = 8;

            while (player.actions.move > 1 &&
                var_A != 0 &&
                var_A != 13)
            {
                seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0x18);

                if (var_A == ' ')
                {
                    var_20D = "Move/Attack, Move Left = ";
                    var_20D += ovr025.sub_670CC(player.actions.move / 2);
                    var_20D += " ";

                    var_A = ovr027.displayInput(out gbl.byte_1D905, 0, 1, 15, 10, 10, string.Empty, var_20D);
                }

                switch (var_A)
                {
                    case '\0':
                        player.actions.move = var_1;

                        ovr033.sub_74572(ovr033.get_player_index(player), 0, 0);

                        if (ovr033.sub_7515A(0, var_4, var_3, player) == 0)
                        {
                            arg_0 = true;
                        }
                        else
                        {
                            arg_0 = false;
                        }

                        ovr033.sub_749DD(8, 0, ovr033.sub_74C5A(player), ovr033.sub_74C32(player));
                        player.actions.field_9 = var_2;
                        var_7 = 8;
                        break;

                    case 'H':
                        var_7 = 0;
                        break;

                    case 'I':
                        var_7 = 1;
                        break;

                    case 'M':

                        var_7 = 2;
                        break;

                    case 'Q':
                        var_7 = 3;
                        break;

                    case 'P':
                        var_7 = 4;
                        break;

                    case 'O':
                        var_7 = 5;
                        break;

                    case 'K':
                        var_7 = 6;
                        break;

                    case 'G':
                        var_7 = 7;
                        break;

                    default:
                        var_7 = 8;
                        break;
                }

                if (var_7 < 8)
                {
                    ovr033.sub_74B3F(0, 0, var_7, player);
                    ovr033.sub_74D04(out var_C, out var_B, out var_9, out var_8, var_7, player);

                    if (var_8 > 0)
                    {
                        throw new System.NotSupportedException();//player_array.seg[var_8<<2] for byte array.
                        sub_33F03(ref arg_0, gbl.player_array[var_8], player);
                    }
                    else if (var_9 == 0)
                    {
                        char b = ovr027.yes_no(15, 10, 13, "Flee:");
                        if (b == 'Y')
                        {
                            arg_0 = ovr014.flee_battle(player);
                        }
                        else if (b == 'N')
                        {
                            arg_0 = false;
                        }
                    }
                    else
                    {
                        if ((var_7 / 2) < 1)
                        {
                            var_D = gbl.unk_189B4[var_9].field_0 * 3;
                        }
                        else
                        {
                            var_D = gbl.unk_189B4[var_9].field_0 * 2;
                        }

                        if (gbl.unk_189B4[var_9].field_0 == 0xFF)
                        {
                            var_D = 0xFFFF;
                        }

                        if (var_D > player.actions.move)
                        {
                            ovr025.string_print01("can't go there");
                        }
                        else
                        {
                            ovr014.sub_3E954(var_7, player);

                            if (player.in_combat == false)
                            {
                                arg_0 = ovr025.clear_actions(player);
                            }
                            else
                            {
                                if (player.actions.move > 0)
                                {

                                    ovr014.sub_3E748(var_7, player);
                                }

                                if (player.in_combat == false)
                                {
                                    arg_0 = ovr025.clear_actions(player);
                                }

                                ovr024.in_poison_cloud(1, player);

                                if (ovr025.is_held(player))
                                {
                                    arg_0 = ovr025.clear_actions(player);
                                }
                            }
                        }
                    }
                }

                if (var_A != 0 &&
                    var_A != 13)
                {
                    var_A = ' ';
                }
            }

            if (player.actions.move < 2)
            {
                player.actions.move = 0;
            }
        }


        internal static void sub_33F03(ref bool arg_0, Player target, Player player)
        {
            if (ovr025.offset_above_1(player) == true &&
                ovr025.offset_equals_20(player) == false)
            {
                ovr025.string_print01("Not with that weapon");
            }
            else if (ovr014.sub_40F1F(target, player) == true)
            {
                player.actions.target = target;

                if (ovr014.sub_3EF3D(target, player) == true)
                {
                    arg_0 = true;
                }
                else
                {
                    ovr014.sub_3F94D(target, player);

                    ovr014.sub_3F9DB(out arg_0, null, 0, target, player);
                }
            }
        }


        internal static void delay_menu(ref bool arg_0, Player player_ptr)
        {
            char var_2A;
            string var_29;

            arg_0 = false;
            var_29 = string.Empty;

            if (ovr025.offset_above_1(player_ptr) == false ||
                ovr025.offset_equals_20(player_ptr) == true)
            {
                var_29 += "Guard ";
            }

            var_29 += "Delay Quit ";

            if (ovr025.bandage(false) == true)
            {
                var_29 += "Bandage ";
            }

            var_29 += "Speed Exit";
            var_2A = ' ';

            while (unk_341B3.MemberOf(var_2A) == false)
            {
                if (arg_0 == false)
                {
                    var_2A = ovr027.displayInput(out gbl.byte_1D905, 0, 0, 15, 10, 13, var_29, string.Empty);

                    switch (var_2A)
                    {
                        case 'G':
                            arg_0 = ovr025.guarding(player_ptr);
                            break;

                        case 'D':
                            player_ptr.actions.delay = 1;
                            arg_0 = true;
                            break;

                        case 'Q':
                            arg_0 = ovr025.clear_actions(player_ptr);
                            break;

                        case 'B':
                            arg_0 = ovr025.bandage(true);
                            arg_0 = ovr025.clear_actions(player_ptr);
                            break;

                        case 'S':
                            set_gamespeed();
                            break;
                    }
                }
            }
        }

        static Set unk_341B3 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void set_gamespeed()
        {
            char var_53;
            string var_52;
            string var_29;

            var_53 = ' ';

            while (unk_341B3.MemberOf(var_53) == false)
            {
                var_52 = "GameSpeed (" + ovr025.sub_670CC(gbl.game_speed_var) + ") :";
                var_29 = " ";

                if (gbl.game_speed_var < 9)
                {
                    var_29 += "Slower ";
                }

                if (gbl.game_speed_var > 0)
                {
                    var_29 += "Faster ";
                }

                var_29 += "Exit";

                var_53 = ovr027.displayInput(out gbl.byte_1D905, 0, 0, 15, 10, 13, var_29, var_52);

                if (var_53 == 0x53)
                {
                    gbl.game_speed_var++;
                }
                else if (var_53 == 0x46)
                {
                    gbl.game_speed_var--;
                }
            }
        }


        internal static void sub_3432F(Player player)
        {
            player.field_198 = 1;
            if (player.actions.target != null)
            {
                if (player.actions.target.combat_team == player.combat_team)
                {
                    player.actions.target = null;
                }
            }
        }
    }
}