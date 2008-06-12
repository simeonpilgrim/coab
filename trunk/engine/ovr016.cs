using Classes;

namespace engine
{
    class ovr016
    {
        internal static short sub_44032(Player player)
        {
            byte var_D;
            byte var_C;
            byte loop_var;
            Item item;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            short var_2;

            var_3 = 0;
            var_4 = 0;
            var_5 = 0;
            var_6 = 0;

            for (loop_var = 0; loop_var < gbl.max_spells; loop_var++)
            {
                if (player.spell_list[loop_var] > 127)
                {
                    var_C = (byte)gbl.spell_list[player.spell_list[loop_var] & 0x7F].spellLevel;

                    if (var_C > var_3)
                    {
                        var_3 = var_C;
                    }

                    var_5 += var_C;
                }
            }
            item = player.itemsPtr;

            while (item != null)
            {
                if (ovr023.item_is_scroll(item) == true)
                {
                    for (loop_var = 1; loop_var <= 3; loop_var++)
                    {
                        if ((int)item.getAffect(loop_var) > 0x7f)
                        {
                            var_C = (byte)gbl.spell_list[(int)item.getAffect(loop_var) & 0x7f].spellLevel;

                            if (var_C > var_4)
                            {
                                var_4 = var_C;
                            }

                            var_6 += var_C;
                        }
                    }
                }

                item = item.next;
            }

            var_D = 0;
            if (var_5 > 0 || var_6 > 0)
            {
                var_D = 4;
            }

            if (var_3 > 2 || var_4 > 2)
            {
                var_D = 6;
            }

            player.spell_to_learn_count = var_D;
            var_2 = (short)((var_D * 0x3C) + (var_6 * 0x0f) + (var_5 * 0x0f));

            return var_2;
        }


        internal static void cancel_memorize(Player player)
        {
            for (int i = 0; i < gbl.max_spells; i++)
            {
                if (player.spell_list[i] > 0x7F)
                {
                    player.spell_list[i] = 0;
                }
            }

            player.spell_to_learn_count = 0;
        }


        internal static void cancel_scribes(Player player)
        {
            Item item = player.itemsPtr;

            while (item != null)
            {
                if (ovr023.item_is_scroll(item) == true)
                {
                    item.affect_1 &= (Affects)0x7F;
                    item.affect_2 &= (Affects)0x7F;
                    item.affect_3 &= (Affects)0x7F;
                }
                item = item.next;
            }
        }


        internal static void cancel_spells()
        {
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                cancel_memorize(player);
                cancel_scribes(player);
                player = player.next_player;
            }
        }


        internal static int HowManySpellsPlayerCanLearn(int spellClass, int spellLevel) //sub_4428E
        {
            int alreadyLearning = 0;

            for (int i = 0; i < gbl.max_spells; i++)
            {
                int spell_id = gbl.player_ptr.spell_list[i];

                if (spell_id > 0)
                {
                    spell_id &= 0x7F;

                    if (gbl.spell_list[spell_id].spellLevel == spellLevel &&
                        gbl.spell_list[spell_id].spellClass == spellClass)
                    {
                        alreadyLearning++;
                    }
                }
            }

            return gbl.player_ptr.field_12D[spellClass, spellLevel-1] - alreadyLearning;
        }


        internal static bool sub_443A0(byte learn_type)
        {
            string text = string.Empty;

            if (learn_type == 1)
            {
                if (gbl.area_ptr.can_cast_spells == true)
                {
                    text = "cannot cast spells in this area";
                }
            }
            else if (gbl.player_ptr.health_status == Status.animated ||
                gbl.player_ptr.in_combat == false)
            {
                text = "is in no condition to ";

                switch (learn_type)
                {
                    case 1:
                        text += "cast any spells";
                        break;

                    case 2:
                        text += "memorize spells";
                        break;

                    case 3:
                        text += "scribe any scrolls";
                        break;
                }
            }

            if (text.Length != 0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, text, gbl.player_ptr);

                return false;
            }

            return true;
        }


        internal static void cast_spell()
        {
            bool var_5 = false; /* simeon */

            bool var_4 = false;
            short var_2 = -1;

            gbl.dword_1D87F = null;

            gbl.byte_1D5BE = 1;

            if (sub_443A0(1) == true)
            {
                byte spell_id;

                do
                {
                    bool var_3;

                    spell_id = ovr020.spell_menu2(out var_3, ref var_2, 1, SpellLoc.memory);

                    if (spell_id != 0)
                    {
                        var_4 = true;
                        seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

                        ovr023.sub_5D2E1(ref var_5, 1, 0, spell_id);
                    }
                    else if (var_3 == true)
                    {
                        var_4 = true;
                    }
                    else
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "has no spells memorized", gbl.player_ptr);
                    }
                } while (spell_id != 0);
            }

            if (var_4 == true)
            {
                ovr025.load_pic();
            }
        }


        internal static byte sub_445D4()
        {
            const int MaxSpellLevel = 5;
            const int MaxSpellClass = 3;
            string[,] var_60 = new string[MaxSpellClass, MaxSpellLevel];
            byte var_5F;
            byte var_5E;
            byte var_5D;
            sbyte spellLevel;
            sbyte spellClass;
            bool[] canLearnSpellClass = new bool[MaxSpellClass];
            string var_2A = string.Empty;
            byte var_1;

            var_5F = 0;

            for (spellClass = 0; spellClass < MaxSpellClass; spellClass++)
            {
                canLearnSpellClass[spellClass] = false;

                for (spellLevel = 0; spellLevel < MaxSpellLevel; spellLevel++)
                {
                    var_60[spellClass, spellLevel] = HowManySpellsPlayerCanLearn(spellClass, spellLevel+1).ToString();
                    
                    if (gbl.player_ptr.field_12D[spellClass, spellLevel] == 0)
                    {
                        var_60[spellClass,spellLevel] = " ";
                    }
                    else
                    {
                        canLearnSpellClass[spellClass] = true;
                        var_5F = 1;
                    }
                }
            }

            if (var_5F != 0)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "can memorize:", gbl.player_ptr);
                var_5E = 3;
                for (spellClass = 0; spellClass < MaxSpellClass; spellClass++)
                {
                    if (canLearnSpellClass[spellClass])
                    {
                        switch (spellClass)
                        {
                            case 0:
                                var_2A = "    Cleric Spells:";
                                break;

                            case 1:
                                var_2A = "     Druid Spells:";
                                break;

                            case 2:
                                var_2A = "Magic-User Spells:";
                                break;
                        }

                        seg041.displayString(var_2A, 0, 10, var_5E + 17, 1);
                        var_5D = 0x13;
                        for (spellLevel = 0; spellLevel < MaxSpellLevel; spellLevel++)
                        {
                            seg041.displayString(var_60[spellClass, spellLevel], 0, 10, var_5E + 0x11, var_5D + 1);
                            var_5D += 3;
                        }
                        var_5E++;
                    }
                }
            }

            var_1 = var_5F;

            return var_1;
        }


        internal static void rest_menu(out bool arg_0)
        {
            short var_8;
            short var_6;
            Player player;

            var_6 = 0;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                var_8 = sub_44032(player);

                if (var_6 < var_8)
                {
                    var_6 = var_8;
                }

                player = player.next_player;
            }

            gbl.unk_1D890.field_6 = (ushort)(var_6 / 60);

            gbl.unk_1D890.field_4 = (ushort)((var_6 - (gbl.unk_1D890.field_6 * 60)) / 10);

            gbl.unk_1D890.field_2 = (ushort)(var_6 % 10);

            arg_0 = ovr021.resting(true);

            gbl.unk_1D890.Clear();

            ovr025.display_map_position_time();
        }


        internal static void sub_4486F()
        {
            byte var_3;
            byte var_2;
            byte var_1;

            for (var_1 = 0; var_1 <= 0x52; var_1++)
            {
                for (var_2 = var_1; var_2 < gbl.max_spells; var_2++)
                {
                    if ((gbl.player_ptr.spell_list[var_1] & 0xF7) > (gbl.player_ptr.spell_list[var_2] & 0xF7))
                    {
                        var_3 = gbl.player_ptr.spell_list[var_1];
                        gbl.player_ptr.spell_list[var_1] = gbl.player_ptr.spell_list[var_2];
                        gbl.player_ptr.spell_list[var_2] = var_3;
                    }
                }
            }
        }


        internal static void memorize_spell()
        {
            short var_7;
            byte var_5;
            byte var_4;
            byte var_3;
            bool var_2;
            bool var_1;

            if (sub_443A0(2) == true)
            {
                var_1 = false;
                var_7 = -1;
                gbl.byte_1D5BE = 1;

                var_4 = ovr020.spell_menu2(out var_2, ref var_7, 0, SpellLoc.memorize);
                var_3 = 1;

                if (var_2 == true)
                {
                    if (ovr027.yes_no(15, 10, 14, "Memorize These Spells? ") == 'N')
                    {
                        cancel_memorize(gbl.player_ptr);
                    }
                    else
                    {
                        var_1 = true;
                    }
                }
                else
                {
                    var_3 = 0;
                }

                var_7 = -1;

                while (var_1 == false)
                {
                    var_1 = (sub_445D4() == 0);

                    if (var_1 == true)
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "cannot memorize any spells", gbl.player_ptr);
                    }
                    else
                    {
                        var_4 = ovr020.spell_menu2(out var_2, ref var_7, 2, SpellLoc.grimoire);
                        var_3 = 1;

                        if (var_4 == 0)
                        {
                            var_1 = true;
                        }
                        else if (HowManySpellsPlayerCanLearn(gbl.spell_list[var_4].spellClass, gbl.spell_list[var_4].spellLevel) > 0)
                        {
                            var_5 = 0;

                            while (gbl.player_ptr.spell_list[var_5] != 0)
                            {
                                var_5++;
                            }

                            gbl.player_ptr.spell_list[var_5] = (byte)(var_4 + 0x80);

                            sub_4486F();
                        }
                    }
                }

                if (var_7 != -1)
                {
                    var_7 = -1;

                    var_4 = ovr020.spell_menu2(out var_2, ref var_7, 0, SpellLoc.memorize);

                    if (var_2 == true &&
                        ovr027.yes_no(15, 10, 14, "Memorize these spells? ") == 0x4e)
                    {
                        cancel_memorize(gbl.player_ptr);
                    }

                }

                if (var_3 != 0)
                {
                    ovr025.load_pic();
                }
            }
        }


        internal static void scribe_spell()
        {
            bool var_D;
            Item var_C;
            short var_8;
            byte var_4;
            byte var_3;
            bool var_2;
            byte var_1;

            if (sub_443A0(3) == true)
            {
                var_1 = 0;
                var_8 = -1;

                var_4 = ovr020.spell_menu2(out var_2, ref var_8, 0, SpellLoc.scribe);
                var_3 = 1;

                if (var_2 == true)
                {
                    if (ovr027.yes_no(15, 10, 14, "Scribe These Spells? ") == 'N')
                    {
                        cancel_scribes(gbl.player_ptr);
                    }
                    else
                    {
                        var_1 = 1;
                    }
                }
                else
                {
                    var_3 = 0;
                }

                var_8 = -1;

                while (var_1 == 0)
                {
                    var_4 = ovr020.spell_menu2(out var_2, ref var_8, 3, SpellLoc.scrolls);

                    if (var_4 == 0)
                    {
                        var_1 = 1;

                        if (var_2 == false)
                        {
                            ovr025.DisplayPlayerStatusString(true, 10, "has no copyable scrolls", gbl.player_ptr);
                        }
                        else
                        {
                            var_3 = 1;
                        }
                    }
                    else
                    {
                        var_3 = 1;
                        if (gbl.player_ptr.field_79[var_4 - 1] != 0)
                        {
                            ovr025.string_print01("You already know that spell");
                        }
                        else
                        {
                            var_C = gbl.player_ptr.itemsPtr;
                            var_D = false;

                            while (var_C != null && var_D == false)
                            {
                                if (ovr023.item_is_scroll(var_C) == true)
                                {
                                    for (int var_6 = 1; var_6 <= 3; var_6++)
                                    {
                                        if ((int)var_C.getAffect(var_6) > 0x7F &&
                                            ((int)var_C.getAffect(var_6) & 0x7F) == var_4)
                                        {
                                            ovr025.string_print01("You are already scibing that spell");
                                            var_D = true;
                                        }
                                    }
                                }

                                var_C = var_C.next;
                            }

                            if (var_D == false)
                            {
                                int spell_level = gbl.spell_list[var_4].spellLevel;
                                int spell_class = gbl.spell_list[var_4].spellClass;

                                if (gbl.player_ptr.field_12D[spell_class, spell_level - 1] > 0)
                                {
                                    var_C = gbl.player_ptr.itemsPtr;

                                    while (var_C != null && var_D == false)
                                    {
                                        int var_6 = 1;
                                        do
                                        {
                                            if (var_C.getAffect(var_6) == (Affects)var_4)
                                            {
                                                var_C.setAffect(var_6, (Affects)((int)var_C.getAffect(var_6) | 0x80));
                                                var_D = true;
                                            }
 
                                            var_6++;
                                        } while (var_6 <= 3 && var_D == false);

                                        var_C = var_C.next;
                                    }
                                }
                                else
                                {
                                    ovr025.string_print01("You can not scribe that spell.");
                                }
                            }
                        }
                    }
                }

                if (var_8 != -1)
                {
                    var_8 = -1;

                    var_4 = ovr020.spell_menu2(out var_2, ref var_8, 0, SpellLoc.scribe);

                    if (var_2 == true &&
                        ovr027.yes_no(15, 10, 14, "Scribe these spells? ") == 0x4e)
                    {
                        cancel_scribes(gbl.player_ptr);
                    }
                }

                if (var_3 != 0)
                {
                    ovr025.load_pic();
                }
            }
        }

        /// <summary>
        /// addes a new StringList item onto var_C
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arg_6"></param>
        /// <param name="var_C"></param>
        internal static void sub_44E89(string s, byte arg_6, ref StringList var_C)
        {
            var_C.next = new StringList();

            var_C = var_C.next;
            var_C.next = null;
            var_C.s = s;
            var_C.field_29 = arg_6;
        }


        internal static void display_magic_effects()
        {
            short var_42;
            string var_40;
            bool var_17;
            byte var_16;
            byte var_15;
            byte var_13;
            byte var_12;
            Affects var_11;
            Player player;
            StringList var_C;
            StringList var_8;
            Affect affect;

            var_8 = new StringList();
            var_C = var_8;

            var_C.field_29 = 0;
            var_C.s = " ";
            var_C.next = null;

            player = gbl.player_next_ptr;

            while (player != null)
            {
                var_13 = 0;
                sub_44E89(player.name, 1, ref var_C);

                affect = player.affect_ptr;

                while (affect != null)
                {
                    var_16 = 1;

                    var_11 = affect.type;
                    var_40 = string.Empty;

                    switch (var_11)
                    {
                        case Affects.bless:
                            goto case Affects.sleep;
                        case Affects.cursed:
                            goto case Affects.sleep;
                        case Affects.detect_magic:
                            goto case Affects.sleep;
                        case Affects.protection_from_evil:
                            goto case Affects.sleep;
                        case Affects.protection_from_good:
                            goto case Affects.sleep;
                        case Affects.resist_cold:
                            goto case Affects.sleep;
                        case Affects.charm_person:
                            goto case Affects.sleep;
                        case Affects.enlarge:
                            goto case Affects.sleep;

                        case Affects.friends:
                            goto case Affects.sleep;
                        case Affects.read_magic:
                            goto case Affects.sleep;
                        case Affects.shield:
                            goto case Affects.sleep;
                        case Affects.find_traps:
                            goto case Affects.sleep;
                        case Affects.resist_fire:
                            goto case Affects.sleep;
                        case Affects.silence_15_radius:
                            goto case Affects.sleep;
                        case Affects.slow_poison:
                            goto case Affects.sleep;
                        case Affects.spiritual_hammer:
                            goto case Affects.sleep;
                        case Affects.detect_invisibility:
                            goto case Affects.sleep;
                        case Affects.invisibility:
                            goto case Affects.sleep;

                        case Affects.mirror_image:
                            goto case Affects.sleep;
                        case Affects.ray_of_enfeeblement:
                            goto case Affects.sleep;

                        case Affects.funky__32:
                            goto case Affects.sleep;
                        case Affects.blinded:
                            goto case Affects.sleep;
                        case Affects.cause_disease_1:
                            goto case Affects.sleep;

                        case Affects.bestow_curse:
                            goto case Affects.sleep;
                        case Affects.blink:
                            goto case Affects.sleep;
                        case Affects.strength:
                            goto case Affects.sleep;
                        case Affects.haste:
                            goto case Affects.sleep;

                        case Affects.prot_from_normal_missiles:
                            goto case Affects.sleep;
                        case Affects.slow:
                            goto case Affects.sleep;
                        case Affects.prot_from_evil_10_radius:
                            goto case Affects.sleep;
                        case Affects.prot_from_good_10_radius:
                            goto case Affects.sleep;
                        case Affects.prayer:
                            goto case Affects.sleep;

                        case Affects.snake_charm:
                            goto case Affects.sleep;
                        case Affects.paralyze:
                            goto case Affects.sleep;
                        case Affects.sleep:
                            var_12 = 1;
                            var_15 = 0;

                            while (var_12 <= 0x38 &&
                                var_15 == 0)
                            {
                                if (gbl.spell_list[var_12].affect_id == var_11)
                                {
                                    var_40 = ovr023.AffectNames[var_12];
                                    var_15 = 1;
                                }
                                else
                                {
                                    var_12++;
                                }
                            }

                            if (var_15 == 0)
                            {
                                var_40 = "Funky--" + var_11.ToString();
                            }
                            break;

                        case Affects.dispel_evil:
                            var_40 = "Dispel Evil";
                            break;

                        case Affects.faerie_fire:
                            var_40 = "Faerie Fire";
                            break;

                        case Affects.fumbling:
                            var_40 = "Fumbling";
                            break;

                        case Affects.helpless:
                            var_40 = "Helpless";
                            break;

                        case Affects.confuse:
                            var_40 = "Confused";
                            break;

                        case Affects.cause_disease_2:
                            var_40 = "Cause Disease";
                            break;

                        case Affects.hot_fire_shield:
                            var_40 = "Hot Fire Shield";
                            break;

                        case Affects.cold_fire_shield:
                            var_40 = "Cold Fire Shield";
                            break;

                        case Affects.poisoned:
                            var_40 = "Poisoned";
                            break;

                        case Affects.regenerate:
                            var_40 = "Regenerating";
                            break;

                        case Affects.fire_resist:
                            var_40 = "Fire Resistance";
                            break;

                        case Affects.minor_globe_of_invulnerability:
                            var_40 = "Minor Globe of Invulnerability";
                            break;

                        case Affects.feeble:
                            var_40 = "enfeebled";
                            break;

                        case Affects.invisible_to_animals:
                            var_40 = "invisible to animals";
                            break;

                        case Affects.invisible:
                            var_40 = "Invisible";
                            break;

                        case Affects.camouflage:
                            var_40 = "Camouflaged";
                            break;

                        case Affects.prot_drag_breath:
                            var_40 = "protected from dragon breath";
                            break;

                        case Affects.berserk:
                            var_40 = "berserk";
                            break;

                        case Affects.displace:
                            var_40 = "Displaced";
                            break;

                        default:
                            var_16 = 0;
                            break;
                    }

                    if (var_16 != 0)
                    {
                        sub_44E89(" " + var_40, 0, ref var_C);
                    }

                    affect = affect.next;
                }

                if (var_13 == 0)
                {

                    sub_44E89(" <No Spell Effects>", 0, ref var_C);
                }

                sub_44E89(" ", 0, ref var_C);

                player = player.next_player;
            }

            var_17 = true;
            var_42 = 0;
            seg037.draw8x8_outer_frame();

            ovr027.sl_select_item(out var_C, ref var_42, ref var_17, true, var_8,
                0x16, 0x26, 4, 1, 15, 10, 11, string.Empty, string.Empty);

            ovr027.free_stringList(ref var_8);
            ovr025.load_pic();
        }


        internal static void magic_menu(ref bool arg_0)
        {
            bool var_2;
            char var_1;

            var_1 = ' ';

            while (arg_0 == false && unk_45CD7.MemberOf(var_1) == false)
            {
                var_1 = ovr027.displayInput(out var_2, true, 1, 15, 10, 13, "Cast Memorize Scribe Display Rest Exit", string.Empty);

                if (var_2 == true)
                {
                    ovr020.scroll_team_list(var_1);
                    ovr025.Player_Summary(gbl.player_ptr);
                }
                else
                {
                    switch (var_1)
                    {
                        case 'C':
                            cast_spell();
                            break;

                        case 'M':
                            memorize_spell();
                            break;

                        case 'S':
                            scribe_spell();
                            break;

                        case 'D':
                            display_magic_effects();
                            break;

                        case 'R':
                            rest_menu(out arg_0);
                            break;
                    }
                }
            }
        }


        internal static void sub_4558D()
        {
            Player var_C;
            Player var_8;
            Player var_4;

            var_8 = gbl.player_next_ptr;
            var_4 = null;

            if (var_8 == gbl.player_ptr)
            {
                while (var_8.next_player != null)
                {
                    var_8 = var_8.next_player;
                }
            }
            else
            {
                while (var_8.next_player != gbl.player_ptr)
                {
                    var_4 = var_8;
                    var_8 = var_8.next_player;
                }
            }

            var_C = gbl.player_ptr.next_player;

            if (gbl.player_next_ptr == gbl.player_ptr)
            {
                if (var_C != null)
                {
                    gbl.player_next_ptr = var_C;
                }

                var_8.next_player = gbl.player_ptr;

                gbl.player_ptr.next_player = null;
            }
            else
            {
                var_8.next_player = var_C;
                gbl.player_ptr.next_player = var_8;


                if (var_8 == gbl.player_next_ptr ||
                    var_4 == null)
                {
                    gbl.player_next_ptr = gbl.player_ptr;
                }
                else
                {
                    var_4.next_player = gbl.player_ptr;
                }
            }
        }


        internal static void sub_456E5()
        {
            Player player_ptr2;
            Player player_ptr;

            player_ptr = gbl.player_next_ptr;

            if (player_ptr != gbl.player_ptr)
            {
                while (player_ptr.next_player != gbl.player_ptr)
                {
                    player_ptr = player_ptr.next_player;
                }
            }

            player_ptr2 = gbl.player_ptr.next_player;

            if (player_ptr2 == null)
            {

                gbl.player_ptr.next_player = gbl.player_next_ptr;
                gbl.player_next_ptr = gbl.player_ptr;
                gbl.player_ptr.next_player = player_ptr2;
            }
            else
            {
                gbl.player_ptr.next_player = player_ptr2.next_player;

                if (gbl.player_next_ptr == gbl.player_ptr)
                {
                    gbl.player_next_ptr = player_ptr2;
                }

                if (player_ptr != gbl.player_ptr)
                {
                    player_ptr.next_player = player_ptr2;
                }

                player_ptr2.next_player = gbl.player_ptr;
            }
        }

        static string[] seg600_04A6 = { "Select Exit", "Place Exit" };
        static Set unk_45832 = new Set(0x0101, new byte[] { 0x020 });

        internal static void reorder_party()
        {
            bool var_2;

            int var_1 = 0;
            char var_3 = ' ';

            while (unk_45CD7.MemberOf(var_3) == false)
            {
                var_3 = ovr027.displayInput(out var_2, true, 1, 15, 10, 13, seg600_04A6[var_1], "Party Order: ");
                
                if (var_2 == true)
                {
                    if (var_1 == 0)
                    {
                        ovr020.scroll_team_list(var_3);
                        ovr025.Player_Summary(gbl.player_ptr);
                    }
                    else
                    {
                        if (var_3 == 0x47)
                        {
                            sub_4558D();
                        }
                        else if (var_3 == 0x4F)
                        {
                            sub_456E5();
                        }
                        ovr025.Player_Summary(gbl.player_ptr);
                    }
                }
                else if( unk_45832.MemberOf(var_3) == true )
                {
                    var_1 = (var_1 == 0) ? 1 : 0;

                    if (var_1 != 0)
                    {
                        ovr025.DisplayPlayerStatusString(false, 10, "has been selected", gbl.player_ptr);
                    }
                    else
                    {
                        ovr025.ClearPlayerTextArea();
                    }
                }
            }

        }


        internal static void drop_player()
        {
            if (gbl.player_ptr.next_player == null &&
                gbl.player_ptr == gbl.player_next_ptr)
            {
                if (ovr027.yes_no(15, 10, 14, "quit TO DOS: ") == 'Y')
                {
                    ovr018.free_players(true, false);
                    seg043.print_and_exit();
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(false, 10, "will be gone", gbl.player_ptr);

                if (ovr027.yes_no(15, 10, 14, "Drop from party? ") == 'Y')
                {
                    if (gbl.player_ptr.in_combat == true)
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "bids you farewell", gbl.player_ptr);
                    }
                    else
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "is dumped in a ditch", gbl.player_ptr);
                    }

                    ovr018.free_players(true, false);
                    seg037.draw8x8_clear_area(0x0b, 0x26, 1, 0x11);

                    ovr025.Player_Summary(gbl.player_ptr);
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "Breathes A sigh of relief", gbl.player_ptr);
                }
            }
        }


        internal static void game_speed()
        {
            string var_22B;
            string var_2B;
            bool var_2;
            char var_1;

            do
            {
                var_22B = "Game Speed = " + gbl.game_speed_var.ToString() + " (0=fastest 9=slowest)";
                seg041.displayString(var_22B, 0, 10, 18, 1);

                var_2B = string.Empty;

                if (gbl.game_speed_var > 0)
                {
                    var_2B += " Faster";
                }

                if (gbl.game_speed_var < 9)
                {
                    var_2B += " Slower";
                }

                var_2B += " Exit";

                var_1 = ovr027.displayInput(out var_2, true, 1, 15, 10, 13, var_2B, "Game Speed:");

                if (var_2 == true)
                {
                    if (var_1 == 0x50)
                    {
                        if (gbl.game_speed_var > 0)
                        {
                            gbl.game_speed_var--;
                        }
                    }
                    else if (var_1 == 0x48)
                    {
                        if (gbl.game_speed_var < 9)
                        {
                            gbl.game_speed_var++;
                        }
                    }
                }
                else
                {
                    if (var_1 == 0x46)
                    {
                        gbl.game_speed_var--;
                    }
                    else if (var_1 == 0x53)
                    {
                        gbl.game_speed_var++;
                    }
                }

            } while (unk_45CD7.MemberOf(var_1) == false);

            ovr025.ClearPlayerTextArea();
        }

        static Set unk_45CD7 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void alter_menu()
        {
            string var_2C;
            bool var_3;
            char var_2;
            char var_1;

            var_1 = ' ';

            while (unk_45CD7.MemberOf(var_1) == false)
            {
                var_1 = ovr027.displayInput(out var_3, true, 1, 15, 10, 13, "Order Drop Speed Icon Pics Exit", "Alter: ");

                if (var_3 == true)
                {
                    ovr020.scroll_team_list(var_1);
                    ovr025.Player_Summary(gbl.player_ptr);
                }
                else
                {
                    switch (var_1)
                    {
                        case 'O':
                            reorder_party();
                            break;

                        case 'D':
                            drop_player();
                            break;

                        case 'S':
                            game_speed();
                            break;

                        case 'I':
                            ovr018.icon_builder();
                            ovr025.load_pic();
                            break;

                        case 'P':
                            do
                            {
                                if (gbl.PicsOn == true)
                                {
                                    var_2C = "Pics on  ";

                                    if (gbl.AnimationsOn == true)
                                    {
                                        var_2C += "Animation on  ";
                                    }
                                    else
                                    {
                                        var_2C += "Animation off ";
                                    }
                                }
                                else
                                {
                                    var_2C = "Pics off  ";
                                }

                                var_2C += "Exit";

                                var_2 = ovr027.displayInput(out var_3, true, 0, 15, 10, 13, var_2C, string.Empty);

                                if (var_2 == 0x50)
                                {
                                    gbl.PicsOn = !gbl.PicsOn;
                                }
                                else if (var_2 == 0x41)
                                {
                                    gbl.AnimationsOn = !gbl.AnimationsOn;
                                }

                            } while (unk_45CD7.MemberOf(var_2) == false);
                            break;
                    }
                }
            }
        }


        internal static bool player_is_okey(Player player)
        {
            return (player.health_status == Status.okey);
        }


        internal static void sub_45F22(ref int var_2)
        {
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player_is_okey(player) == true)
                {
                    for (int i = 0; i < gbl.max_spells; i++)
                    {
                        switch (player.spell_list[i])
                        {
                            case 3:
                                var_2 += ovr024.roll_dice(8, 1);
                                break;

                            case 0x3A:
                                var_2 += ovr024.roll_dice(8, 2) + 1;
                                break;

                            case 0x47:
                                var_2 += ovr024.roll_dice(8, 3) + 3;
                                break;
                        }
                    }
                }

                player = player.next_player;
            }
        }


        internal static void sub_45FDD(ref int var_2, int bp_var_4, int bp_var_6, int bp_var_8)
        {
            for (int var_3 = 1; var_3 <= bp_var_4; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 1);
            }

            for (int var_3 = 1; var_3 <= bp_var_6; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 2) + 1;
            }

            for (int var_3 = 1; var_3 <= bp_var_8; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 3) + 3;
            }
        }


        internal static int total_hitpoints_lost() /* sub_4608F */
        {
            int lost_points = 0;
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                lost_points += player.hit_point_max - player.hit_point_current;
                player = player.next_player;
            }

            return lost_points;
        }


        internal static void sub_460ED(out int bp_var_8,out int bp_var_6,out int bp_var_4 )
        {
            short var_10;
            short var_E;
            short var_C;
            short var_A;
            Player player_ptr;
            short var_2 = 0; /* Simeon */

            player_ptr = gbl.player_next_ptr;
            var_A = 0;
            var_C = 0;
            var_E = 0;

            bp_var_4 = 0;
            bp_var_6 = 0;
            bp_var_8 = 0;
            int var_8 = 0;

            while (player_ptr != null)
            {
                var_10 = 0;

                if (player_is_okey(player_ptr) == true)
                {
                    bp_var_4 += player_ptr.field_12D[0,0];
                    var_A = (short)(player_ptr.field_12D[0,0] * 15);

                    bp_var_6 += player_ptr.field_12D[0,3];
                    var_C = (short)(player_ptr.field_12D[0,3] * 60);

                    bp_var_8 += player_ptr.field_12D[0,4];
                    var_E = (short)(player_ptr.field_12D[0,4] * 75);
                }

                if (var_A > 0)
                {
                    var_10 = 240;
                    var_2 += 27;
                }

                if ((var_C + var_E) != 0)
                {
                    var_10 = 360;

                    if (var_E > 0)
                    {
                        var_2 += 78;
                    }
                    else
                    {
                        var_2 += 34;
                    }
                }

                var_10 += (short)(var_A + var_C + var_E);

                if (var_8 < var_10)
                {
                    var_8 = var_10;
                }

                player_ptr = player_ptr.next_player;
            }

            if (total_hitpoints_lost() < var_2)
            {
                int var_11 = var_2 / total_hitpoints_lost();

                var_8 /= var_11;
            }

            gbl.unk_1D890.field_6 = (ushort)(var_8 / 60);

            gbl.unk_1D890.field_4 = (ushort)((var_8 - (gbl.unk_1D890.field_6 * 60)) / 10);

            gbl.unk_1D890.field_2 = (ushort)(var_8 % 10);
        }


        internal static void sub_46280(ref int bp_var_2)
        {
            int var_6;

            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.hit_point_max > player.hit_point_current)
                {
                    var_6 = player.hit_point_max - player.hit_point_current;

                    if (var_6 > bp_var_2)
                    {
                        var_6 = bp_var_2;
                    }

                    if (var_6 < 1)
                    {
                        var_6 = 0;
                    }

                    if (var_6 > 0 &&
                        ovr024.heal_player(0, (byte)var_6, player) == true &&
                        var_6 <= bp_var_2)
                    {
                        bp_var_2 -= var_6;
                    }
                }
                player = player.next_player;
            }
        }


        internal static void fix_menu(out bool arg_0)
        {
            RestTime rest_time;
            int var_8;
            int var_6;
            int var_4;

            arg_0 = false;

            if (total_hitpoints_lost() != 0)
            {
                int var_2 = 0;
                sub_45F22(ref var_2);

                if (total_hitpoints_lost() == 0)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                    ovr025.display_map_position_time();
                }
                else
                {
                    rest_time = new RestTime(gbl.unk_1D890);

                    sub_460ED(out var_8, out var_6, out var_4);

                    arg_0 = ovr021.resting(false);

                    if (arg_0 == false)
                    {
                        sub_45FDD(ref var_2, var_4, var_6, var_8);
                        sub_46280(ref var_2);
                        ovr025.Player_Summary(gbl.player_ptr);
                        ovr025.display_map_position_time();

                        gbl.unk_1D890 = new RestTime(rest_time);
                    }
                }
            }
        }

        static Set unk_463F4 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void make_camp(out bool arg_0)
        {
            byte game_state_bkup = gbl.game_state;
            gbl.game_state = 2;
            gbl.rest_10_seconds = 0;

            gbl.unk_1D890.Clear();

            gbl.byte_1D5AB = gbl.lastDaxFile;
            gbl.byte_1D5B5 = gbl.lastDaxBlockId;

            ovr025.load_pic();
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

            seg041.displayString("The party makes camp...", 0, 10, 18, 1);
            cancel_spells();
            arg_0 = false;
            char input_key = ' ';

            while (arg_0 == false &&
                unk_463F4.MemberOf(input_key) == false)
            {
                bool special_key;
                input_key = ovr027.displayInput(out special_key, true, 1, 15, 10, 13, "Save View Magic Rest Alter Fix Exit", "Camp:");

                if (special_key == true)
                {
                    ovr020.scroll_team_list(input_key);
                    ovr025.Player_Summary(gbl.player_ptr);
                }
                else
                {
                    switch (input_key)
                    {
                        case 'S':
                            ovr017.SaveGame();
                            if (ovr027.yes_no(15, 10, 14, "Quit TO DOS ") == 'Y')
                            {
                                seg043.print_and_exit();
                            }
                            break;

                        case 'V':
                            gbl.byte_1D5BE = 1;
                            bool dummyBool;
                            ovr020.viewPlayer(out dummyBool);
                            break;

                        case 'M':
                            gbl.byte_1D5BE = 1;
                            magic_menu(ref arg_0);
                            break;

                        case 'R':
                            gbl.byte_1D5BE = 1;
                            rest_menu(out arg_0);
                            break;

                        case 'F':
                            fix_menu(out arg_0);
                            break;

                        case 'A':
                            gbl.byte_1D5BE = 1;
                            alter_menu();
                            break;
                    }
                }
            }

            if (seg051.Copy(3, 1, gbl.byte_1D5AB) == "PIC")
            {
                ovr030.load_pic_final(ref gbl.byte_1D556, 0, gbl.byte_1D5B5, gbl.byte_1D5AB);
            }

            cancel_spells();
            gbl.dword_1D87F = null;
            gbl.game_state = game_state_bkup;
            ovr025.display_map_position_time();
            ovr025.ClearPlayerTextArea();
            ovr027.redraw_screen();
        }
    }
}
