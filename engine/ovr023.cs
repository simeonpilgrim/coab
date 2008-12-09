using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr023
    {
        internal static string[] SpellNames = { /* AffectNames */
                                   string.Empty,
                                   "Bless",
                                   "Curse",
                                   "Cure Light Wounds",
                                   "Cause Light Wounds",
                                   "Detect Magic",
                                   "Protection From Evil",
                                   "Protection from Good",
                                   "Resist Cold",
                                   "Burning Hands",
                                   "Charm Person",
                                   "Detect Magic",
                                   "Enlarge",
                                   "Reduce",
                                   "Friends",
                                   "Magic Missile",
                                   "Protection From Evil",
                                   "Protection From Good",
                                   "Read Magic",
                                   "Shield",
                                   "Shocking Grasp",
                                   "Sleep",
                                   "Find Traps",
                                   "Hold Person",
                                   "Resist Fire",
                                   "Silence, 15' Radius",
                                   "Slow Poison",
                                   "Snake Charm",
                                   "Spiritual Hammer",
                                   "Detect Invisibility",
                                   "Invisibility",
                                   "Knock",
                                   "Mirror Image",
                                   "Ray of Enfeeblement",
                                   "Stinking Cloud",
                                   "Strength",
                                   "Animate Dead",
                                   "Cure Blindness",
                                   "Cause Blindness",
                                   "Cure Disease",
                                   "Cause Disease",
                                   "Dispel Magic",
                                   "Prayer",
                                   "Remove Curse",
                                   "Bestow Curse",
                                   "Blink",
                                   "Dispel Magic",
                                   "Fireball",
                                   "Haste",
                                   "Hold Person",
                                   "Invisibility, 10' Radius",
                                   "Lightning Bolt",
                                   "Protection From Evil, 10' Radius",
                                   "Protection From Good, 10' Radius",
                                   "Protection From Normal Missiles",
                                   "Slow",
                                   "Restoration",
                                    string.Empty,
                                   "Cure Serious Wounds",
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,
                                    string.Empty,                                   
                                    "Cause Serious Wounds",
                                   "Neutralize Poison",
                                   "Poison",
                                   "Protection Evil, 10' Radius",
                                   "Sticks to Snakes",
                                   "Cure Critical Wounds",
                                   "Cause Critical Wounds",
                                   "Dispel Evil",
                                   "Flame Strike",
                                   "Raise Dead",
                                   "Slay Living",
                                   "Detect Magic",
                                   "Entangle",
                                   "Faerie Fire",
                                   "Invisibility to Animals",
                                   "Charm Monsters",
                                   "Confusion",
                                   "Dimension Door",
                                   "Fear",
                                   "Fire Shield",
                                   "Fumble",
                                   "Ice Storm",
                                   "Minor Globe Of Invulnerability",
                                   "Remove Curse",
                                   "Animate Dead",
                                   "Cloud Kill",
                                   "Cone of Cold",
                                   "Feeblemind",
                                   "Hold Monsters",
                                   string.Empty,
                                   string.Empty,
                                   string.Empty,
                                   string.Empty,
                                   string.Empty,
                                   "Bestow Curse" };

        static string[] LevelStrings = {
                                    string.Empty,
                                    "1st Level",
                                    "2nd Level",
                                    "3rd Level",
                                    "4th Level",
                                    "5th Level",
                                    "6th Level",
                                    "7th Level",
                                    "8th Level",
                                    "9th Level"
                                };

        internal static bool can_learn_spell(int spell_id, Player player) /* sub_5C01E */
        {
            spell_id &= 0x7f;
            bool can_learn = false;

            switch (gbl.spell_table[spell_id].spellClass)
            {
                case 0: // Cleric
                    if (player.wis > 8 &&
                        ((player.cleric_lvl > 0) ||
                         (ovr026.sub_6B3D1(player) != 0 && player.turn_undead > 0) ||
                         (player.paladin_lvl > 8) ||
                         (player.field_114 > 8 && ovr026.sub_6B3D1(player) != 0)))
                    {
                        can_learn = true;
                    }
                    break;

                case 1: // Druid
                    if ((player.wis > 8 && player.ranger_lvl > 6) ||
                        (ovr026.sub_6B3D1(player) != 0 && player.field_115 > 6))
                    {
                        can_learn = true;
                    }
                    break;

                case 2: // Magic-User
                    if (player._int > 8 &&
                        ((player.race != Race.human) ||
                     (player.armor == null) ||
                     (gbl.game_state != GameState.Combat) ||
                     (player.ranger_lvl > 8) ||
                     (ovr026.sub_6B3D1(player) != 0 && player.field_115 > 8 && player.magic_user_lvl > 0) ||
                     (ovr026.sub_6B3D1(player) != 0 && player.field_116 > 0)))
                    {
                        can_learn = true;
                    }
                    break;

                case 3: // Monster?
                    can_learn = false;
                    break;

            }

            return can_learn;
        }

        static Set unk_5C1A2 = new Set(0x0001, new byte[] { 0x1E });
        static Set asc_5C1D1 = new Set(0x000B, new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0x28, 0x30, 8 });
        static Set unk_5C1F1 = new Set(0x0009, new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0x20 });

        internal static byte spell_menu(ref int index, SpellSource spellSource)
        {
            string text;

            switch (spellSource)
            {
                case SpellSource.Cast:
                    text = "Cast";
                    break;

                case SpellSource.Memorize:
                    text = "Memorize";
                    break;

                case SpellSource.Scribe:
                    text = "Scribe";
                    break;

                case SpellSource.Learn:
                    text = "Learn";
                    break;

                default:
                    text = string.Empty;
                    break;
            }

            string prompt_text = text.Length > 0 ? "Choose Spell: " : "";

            int end_y = (spellSource == SpellSource.Memorize) ? 0x0F : 0x16;

            bool show_exit = spellSource != SpellSource.Learn;
            bool var_61 = false;

            if (index < 0)
            {
                var_61 = true;
                index = 0;
            }

            if (spellSource == SpellSource.Learn || spellSource == SpellSource.Cast)
            {
                var_61 = true;
            }

            MenuItem selected;
            char input_key;

            do
            {
                input_key = ovr027.sl_select_item(out selected, ref index, ref var_61, show_exit, gbl.spell_string_list,
                    end_y, 0x26, 5, 1, 15, 10, 13, text, prompt_text);

            } while (asc_5C1D1.MemberOf(input_key) == false);


            byte spell_id;
            if (unk_5C1F1.MemberOf(input_key) == true)
            {
                spell_id = 0;
            }
            else
            {
                int selected_index = gbl.spell_string_list.GetRange(0, gbl.spell_string_list.IndexOf(selected)).FindAll(mi => mi.Heading == false).Count;
                spell_id = gbl.memorize_spell_id[selected_index];

                if (spellSource == SpellSource.Scribe)
                {
                    gbl.dword_1D5C6 = gbl.unk_1AF18[selected_index];
                }
            }
            gbl.spell_string_list.Clear();

            return spell_id;
        }


        internal static void add_spell_to_list(byte spell_id) /* sub_5C3ED */
        {
            byte masked_id = (byte)(spell_id & 0x7F);

            var string_list = gbl.spell_string_list;

            int last_spell_level = 0;
            int index = 0;
            if (string_list.Count > 0)
            {
                index = string_list.FindAll(mi => mi.Heading == false).Count + 1;
                last_spell_level = gbl.spell_table[gbl.memorize_spell_id[index - 1]].spellLevel;
            }

            if (gbl.spell_table[masked_id].spellLevel != last_spell_level)
            {
                string_list.Add(new MenuItem(LevelStrings[gbl.spell_table[masked_id].spellLevel], true));
            }

            string_list.Add(new MenuItem(string.Format(" {0}{1}", (spell_id > 0x7F) ? '*' : ' ', SpellNames[masked_id])));

            gbl.memorize_spell_id[index] = masked_id;
        }


        internal static void add_spell_to_learning_list(int spell_id) /* sub_5C5B9 */
        {
            int memorize_index;
            byte masked_id = (byte)(spell_id & 0x7F);

            var spell_list = gbl.spell_string_list;

            int sp_lvl = gbl.spell_table[masked_id].spellLevel;

            if (gbl.spell_string_list.Count == 0)
            {
                System.Array.Clear(gbl.memorize_count, 0, gbl.max_spells);

                memorize_index = 0;
                gbl.memorize_count[memorize_index] = 1;
            }
            else
            {
                memorize_index = 0;

                foreach (var mi in spell_list)
                {
                    if (mi.Heading == false)
                    {
                        if (gbl.spell_table[gbl.memorize_spell_id[memorize_index]].spellLevel > sp_lvl ||
                            gbl.memorize_spell_id[memorize_index] == masked_id)
                        {
                            break;
                        }
                    }
                    memorize_index++;
                }

                if (gbl.memorize_spell_id[memorize_index] != masked_id)
                {
                    int insert_count = 1;
                    for (int i = memorize_index; i < gbl.max_spells; i++)
                    {
                        int tmp_count = gbl.memorize_count[i];
                        gbl.memorize_count[i] = insert_count;
                        insert_count = tmp_count;
                    }
                }
                else
                {
                    gbl.spell_string_list.RemoveAt(memorize_index);
                    gbl.memorize_count[memorize_index] += 1;
                }
            }

            string menu_text = string.Format(" {0}{1}{2}",
                spell_id > 0x7F ? '*' : ' ',
                SpellNames[masked_id],
                gbl.memorize_count[memorize_index] > 1 ? string.Format(" ({0})", gbl.memorize_count[memorize_index]) : "");

            // insert before memorize_index
            gbl.spell_string_list.Insert(memorize_index, new MenuItem(menu_text));


            if (gbl.memorize_spell_id[memorize_index] != masked_id)
            {
                byte insert_id = gbl.memorize_spell_id[memorize_index];
                gbl.memorize_spell_id[memorize_index] = masked_id;

                for (int i = memorize_index + 1; i < gbl.max_spells; i++)
                {
                    byte tmp_id = gbl.memorize_spell_id[i];
                    gbl.memorize_spell_id[i] = insert_id;
                    insert_id = tmp_id;
                }
            }
        }


        internal static void scroll_5C912(byte arg_0) /* sub_5C912 */
        {
            if (gbl.player_ptr.HasAffect(Affects.read_magic) == true ||
                ((gbl.player_ptr.cleric_lvl > 0 || gbl.player_ptr.turn_undead > gbl.player_ptr.field_E6) &&
                  gbl.unk_1C020[gbl.dword_1D5C6.type].item_slot == 12))
            {
                gbl.dword_1D5C6.hidden_names_flag = 0;
            }

            if (gbl.dword_1D5C6.hidden_names_flag == 0)
            {
                for (byte var_1 = 1; var_1 <= 3; var_1++)
                {
                    if ((arg_0 != 0 && (int)gbl.dword_1D5C6.getAffect(var_1) > 0x80) ||
                        (arg_0 == 0 && (int)gbl.dword_1D5C6.getAffect(var_1) > 0))
                    {
                        add_spell_to_list((byte)gbl.dword_1D5C6.getAffect(var_1));
                        gbl.unk_1AF18[gbl.byte_1AFDC] = gbl.dword_1D5C6;
                        gbl.byte_1AFDC++;
                    }
                }
            }
        }


        internal static void sub_5C9F4(byte arg_0)
        {
            for (int var_1 = 0; var_1 < 0x30; var_1++)
            {
                gbl.unk_1AF18[var_1] = null;
            }

            gbl.byte_1AFDC = 0;

            foreach (var item in gbl.player_ptr.items)
            {
                gbl.dword_1D5C6 = item;

                if (item.IsScroll())
                {
                    scroll_5C912(arg_0);
                }
            }
        }


        internal static bool sub_5CA74(SpellLoc spl_location)
        {
            bool result = false;
            bool var_D = true;

            gbl.spell_string_list.Clear();

            for (int var_2 = 0; var_2 < gbl.max_spells; var_2++)
            {
                gbl.memorize_spell_id[var_2] = 0;
            }

            switch (spl_location)
            {
                case SpellLoc.memory:
                    for (int var_2 = 0; var_2 < gbl.max_spells; var_2++)
                    {
                        if (gbl.player_ptr.spell_list[var_2] > 0 &&
                            can_learn_spell(gbl.player_ptr.spell_list[var_2] & 0x7F, gbl.player_ptr) == true &&
                            gbl.player_ptr.spell_list[var_2] < 0x80)
                        {
                            add_spell_to_learning_list(gbl.player_ptr.spell_list[var_2]);
                        }
                    }
                    break;

                case SpellLoc.memorize:
                    for (int var_2 = 0; var_2 < gbl.max_spells; var_2++)
                    {
                        if (gbl.player_ptr.spell_list[var_2] > 0x7F &&
                            can_learn_spell(gbl.player_ptr.spell_list[var_2] & 0x7F, gbl.player_ptr) == true)
                        {
                            add_spell_to_learning_list(gbl.player_ptr.spell_list[var_2]);
                        }
                    }
                    break;

                case SpellLoc.grimoire:
                    for (int var_2 = 1; var_2 <= 100; var_2++)
                    {
                        if (gbl.player_ptr.field_79[var_2 - 1] != 0 &&
                            can_learn_spell(var_2, gbl.player_ptr) == true)
                        {
                            add_spell_to_learning_list(var_2);
                        }
                    }
                    break;

                case SpellLoc.scroll:
                    scroll_5C912(0);
                    var_D = false;
                    break;

                case SpellLoc.scrolls:
                    sub_5C9F4(0);
                    var_D = false;
                    break;

                case SpellLoc.scribe:
                    sub_5C9F4(1);
                    var_D = false;
                    break;

                case SpellLoc.choose:
                    for (int var_2 = 1; var_2 <= 100; var_2++)
                    {
                        int sp_lvl = gbl.spell_table[var_2].spellLevel;
                        int sp_class = gbl.spell_table[var_2].spellClass;
                        //int tmp = (sp_class * 5) + sp_lvl - 1;
                        //sp_lvl = tmp % 5;
                        //sp_class = tmp / 5;

                        if (sp_lvl >= 5 || sp_class >= 3)
                        {
                            //skip this spell
                        }
                        else if (gbl.player_ptr.field_12D[sp_class, sp_lvl] > 0 &&
                            can_learn_spell(var_2, gbl.player_ptr) == true &&
                            gbl.player_ptr.field_79[var_2 - 1] == 0)
                        {
                            add_spell_to_learning_list(var_2);
                        }
                    }
                    break;
            }

            if (gbl.spell_string_list.Count > 0)
            {
                if (var_D == true)
                {
                    int var_2 = 0;

                    var spellLvl = gbl.spell_table[gbl.memorize_spell_id[var_2]].spellLevel;

                    gbl.spell_string_list.Insert(0, new MenuItem(LevelStrings[spellLvl], true));

                    int insert = 0;
                    var inserts = new Queue<KeyValuePair<int, int>>();

                    foreach (var mi in gbl.spell_string_list)
                    {
                        var var_B = spellLvl;

                        if (gbl.memorize_spell_id[var_2] != 0)
                        {
                            spellLvl = gbl.spell_table[gbl.memorize_spell_id[var_2]].spellLevel;
                        }

                        if (var_B < spellLvl)
                        {
                            inserts.Enqueue(new KeyValuePair<int, int>(insert, spellLvl));
                            insert++;
                        }

                        insert++;
                        var_2++;
                    }

                    foreach (var vp in inserts)
                    {
                        gbl.spell_string_list.Insert(vp.Key, new MenuItem(LevelStrings[vp.Value], true));
                    }
                }
                result = true;
            }

            return result;
        }


        internal static byte sub_5CDE5(byte arg_0)
        {
            byte var_2;

            if (gbl.spell_from_item == false)
            {
                var_2 = (byte)(gbl.spell_table[arg_0].field_2 + (gbl.spell_table[arg_0].field_3 * ovr025.spellMaxTargetCount(arg_0)));
            }
            else
            {
                var_2 = (byte)((gbl.spell_table[arg_0].field_3 * 6) + gbl.spell_table[arg_0].field_2);
            }

            if (var_2 == 0 &&
                gbl.spell_table[arg_0].field_6 != 0)
            {
                var_2 = 1;
            }

            if (var_2 == 0xff)
            {
                var_2 = 1;
            }

            return var_2;
        }


        internal static ushort GetSpellAffectTimeout(int spellId) // sub_5CE92
        {
            int var_4;

            if (spellId == 0x28)
            {
                var_4 = ovr024.roll_dice(6, 1) * 10;
            }
            else if (spellId == 0x39 || spellId == 0x3D)
            {
                var_4 = ovr024.roll_dice(4, 5);
            }
            else if (spellId == 0x3B)
            {
                var_4 = (ovr024.roll_dice(4, 1) * 10) + 40;
            }
            else if (spellId == 0x3F)
            {
                if (gbl.game_state == GameState.Combat)
                {
                    var_4 = ovr024.roll_dice(10, 2) * 10;
                }
                else
                {
                    var_4 = (ovr024.roll_dice(10, 1) + 10) * 10;
                }
            }
            else if (spellId == 0x43)
            {
                var_4 = 1440;
            }
            else
            {
                var_4 = gbl.spell_table[spellId].field_4 + (gbl.spell_table[spellId].field_5 * ovr025.spellMaxTargetCount(spellId));
            }

            return (ushort)var_4;
        }


        internal static void sub_5CF7F(string arg_0, DamageType arg_4, int damage, bool arg_8, int TargetCount, byte spell_id)
        {
            if (damage == 0)
            {
                gbl.damage_flags = 0;
            }
            else
            {
                gbl.damage_flags = arg_4;
            }

            if (gbl.sp_target_count != 0)
            {
                int target_count = TargetCount > 0 ? TargetCount : ovr025.spellMaxTargetCount(spell_id);

                for (int target_idx = 1; target_idx <= gbl.sp_target_count; target_idx++)
                {
                    if (gbl.sp_targets[target_idx] != null)
                    {
                        Player target = gbl.sp_targets[target_idx];

                        bool var_30;

                        if (gbl.spell_table[spell_id].can_save_flag == 0)
                        {
                            var_30 = false;
                        }
                        else
                        {
                            var_30 = ovr024.do_saving_throw(0, gbl.spell_table[spell_id].field_9, target);
                        }

                        if (gbl.spell_table[gbl.spell_id].field_2 == -1)
                        {
                            ovr025.reclac_player_values(target);

                            ovr024.CheckAffectsEffect(target, CheckType.Type_11);

                            if (ovr024.attacker_can_hit_target(target.ac, target, gbl.player_ptr) == false)
                            {
                                damage = 0;
                                var_30 = true;
                            }
                        }

                        if (damage > 0)
                        {
                            ovr024.damage_person(var_30, gbl.spell_table[spell_id].can_save_flag, damage, target);
                        }

                        if (gbl.spell_table[spell_id].affect_id > 0)
                        {
                            ovr024.is_unaffected(arg_0, var_30, gbl.spell_table[spell_id].can_save_flag,
                                arg_8, target_count, GetSpellAffectTimeout(spell_id), gbl.spell_table[spell_id].affect_id,
                                target);
                        }
                    }
                }
                gbl.damage_flags = 0;
            }
        }


        internal static void cast_spell_on(out bool arg_0, QuickFight quick_fight, byte arg_6)
        {
            if (gbl.dword_1D87F == null)
            {
                gbl.dword_1D87F = gbl.player_ptr;
            }

            gbl.sp_targets[1] = gbl.player_ptr;
            gbl.sp_target_count = 1;
            arg_0 = true;

            switch (gbl.spell_table[arg_6].field_7)
            {
                case 1:
                    break;

                case 2:
                    ovr025.load_pic();

                    ovr025.selectAPlayer(ref gbl.dword_1D87F, true, "Cast Spell on whom");

                    if (gbl.dword_1D87F == null)
                    {
                        gbl.sp_targets[1] = null;
                        gbl.sp_target_count = 0;
                        arg_0 = false;
                    }
                    else
                    {
                        gbl.sp_targets[1] = gbl.dword_1D87F;
                    }
                    break;

                case 4:
                    // prepend all players
                    int count = gbl.player_next_ptr.Count;
                    System.Array.Copy(gbl.player_next_ptr.ToArray(), 0, gbl.sp_targets, gbl.sp_target_count, count);
                    gbl.sp_target_count += count;
                    break;

                default:
                    arg_0 = false;
                    break;
            }
        }


        internal static void sub_5D2E1(byte arg_4, QuickFight quick_fight, byte spell_id)
        {
            bool dummy = false;
            sub_5D2E1(ref dummy, arg_4, quick_fight, spell_id);
        }


        internal static void sub_5D2E1(ref bool arg_0, byte arg_4, QuickFight quick_fight, byte spell_id)
        {
            Player caster = gbl.player_ptr;
            bool var_1 = true;

            if (gbl.game_state != GameState.Combat &&
                gbl.spell_table[spell_id].field_7 == 0)
            {
                if (gbl.spell_from_item == false)
                {
                    seg041.displayString(SpellNames[spell_id], 0, 10, 0x13, 1);
                    seg041.displayString("can't be cast here...", 0, 10, 0x14, 1);

                    if (ovr027.yes_no(15, 10, 13, "Lose it? ") == 'Y')
                    {
                        caster.ClearSpell(spell_id);
                    }
                }
                else
                {
                    seg041.displayString("That Item", 0, 10, 0x13, 1);
                    seg041.displayString("is a combat-only item...", 0, 10, 0x14, 1);

                    if (ovr027.yes_no(15, 10, 13, "Use it? ") == 'Y')
                    {
                        arg_0 = true;
                    }
                }

                arg_4 = 0;
                var_1 = false;
            }

            if (caster.HasAffect(Affects.affect_4a) == true)
            {
                byte dice_roll = ovr024.roll_dice(2, 1);

                if (dice_roll == 1)
                {
                    cast_spell_text(spell_id, "miscasts", caster);
                    arg_4 = 0;
                    var_1 = false;
                }
            }

            if (arg_4 != 0 && gbl.spell_from_item == false)
            {
                cast_spell_text(spell_id, "casts", caster);
            }

            while (var_1 == true)
            {
                gbl.dword_1D5CA(out arg_0, quick_fight, spell_id);

                if (arg_0 == true)
                {
                    var_1 = false;

                    if (gbl.game_state == GameState.Combat)
                    {
                        ovr025.load_missile_icons(0x12);
                        int casterX = ovr033.PlayerMapXPos(caster);
                        int casterY = ovr033.PlayerMapYPos(caster);

                        byte direction = 0;

                        while (ovr032.CanSeeCombatant(direction, gbl.targetY, gbl.targetX, casterY, casterX) == false)
                        {
                            direction++;
                        }

                        gbl.byte_1D910 = true;
                        ovr033.draw_74B3F(0, 1, direction, caster);

                        if (spell_id == 0x2F)
                        {
                            seg044.sound_sub_120E0(Sound.sound_b);
                        }
                        else if (spell_id == 0x33)
                        {
                            seg044.sound_sub_120E0(Sound.sound_8);
                        }
                        else
                        {
                            seg044.sound_sub_120E0(Sound.sound_2);
                        }

                        ovr025.draw_missile_attack(0x1E, 4, gbl.targetY, gbl.targetX, casterY, casterX);

                        if (ovr033.sub_74761(false, caster) == true)
                        {
                            ovr033.draw_74B3F(1, 1, caster.actions.direction, caster);
                            ovr033.draw_74B3F(0, 0, caster.actions.direction, caster);
                        }
                    }

                    ovr024.remove_invisibility(caster);

                    if (gbl.spell_from_item == false)
                    {
                        caster.ClearSpell(spell_id);
                    }

                    gbl.spell_id = spell_id;

                    gbl.spells_func_table[gbl.spell_id]();

                    gbl.spell_id = 0;
                    gbl.byte_1D2C7 = false;
                }
                else
                {
                    if (gbl.game_state != GameState.Combat)
                    {
                        var_1 = false;
                    }
                    else
                    {
                        if (quick_fight == QuickFight.True ||
                            ovr027.yes_no(15, 10, 14, "Abort Spell? ") == 'Y')
                        {
                            ovr025.string_print01("Spell Aborted");
                            if (gbl.spell_from_item == false)
                            {
                                caster.ClearSpell(spell_id);
                            }

                            var_1 = false;
                        }
                    }
                }
            }

            ovr025.ClearPlayerTextArea();

            if (gbl.game_state == GameState.Combat)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static void localSteppingPathInit(int targetY, int targetX, int casterY, int casterX,
            SteppingPath bp_var_1C) /* sub_5D676 */
        {
            bp_var_1C.attacker_x = casterX;
            bp_var_1C.attacker_y = casterY;
            bp_var_1C.target_x = targetX;
            bp_var_1C.target_y = targetY;

            bp_var_1C.CalculateDeltas();
        }


        static void BoundCoords(ref int rowY, ref int colX) /* sub_5D6B7 */
        {
            colX = System.Math.Min(colX, Display.MaxCol);
            colX = System.Math.Max(colX, Display.MinCol);

            rowY = System.Math.Min(rowY, Display.MaxRow);
            rowY = System.Math.Max(rowY, Display.MinRow);
        }


        internal static int find_players_on_path(SteppingPath path, List<int> player_list) /* sub_5D702 */
        {
            int dir = 0;
            while (!path.Step())
            {
                byte playerIndex = ovr033.PlayerIndexAtMapXY(path.current_y, path.current_x);

                if (playerIndex > 0)
                {
                    if (player_list.Contains(playerIndex) == false)
                    {
                        player_list.Add(playerIndex);
                    }
                }
                dir = path.direction;
            }

            return dir;
        }


        static sbyte[] unk_16D22 = { -1, 0, 0, 1, 1, 0, 0, -1 };
        static sbyte[] unk_16D2A = { 0, -1, -1, 0, 0, 1, 1, 0 };
        static sbyte[] unk_16D32 = { 1, 1, 0, 0, -1, -1, 0, 0 };
        static sbyte[] unk_16D3A = { 0, 0, 1, 1, 0, 0, -1, -1 };

        internal static void sub_5D7CF(int max_range, int playerSize, int targetY, int targetX, int casterY, int casterX)
        {
            List<int> players_on_path = new List<int>();

            bool finished;
            SteppingPath path = new SteppingPath();

            localSteppingPathInit(targetY, targetX, casterY, casterX, path);

            byte[] directions = new byte[0x32];
            int index = 0;
            while (!path.Step())
            {
                directions[index] = path.direction;
                index++;
            }

            int count = index - 1;

            index = 0;
            max_range *= 2;
            int tmp_range = path.steps;
            finished = false;

            while (tmp_range < max_range && finished == false)
            {
                if (targetX < 0x31 && targetX > 0 && targetY < 0x18 && targetY > 0)
                {
                    switch (directions[index])
                    {
                        case 0:
                        case 2:
                        case 4:
                        case 6:
                            tmp_range += 2;
                            break;

                        case 1:
                        case 3:
                        case 5:
                        case 7:
                            tmp_range += 3;
                            break;
                    }

                    targetX += gbl.MapDirectionXDelta[directions[index]];
                    targetY += gbl.MapDirectionYDelta[directions[index]];


                    if (index == count)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    finished = true;
                }
            }

            BoundCoords(ref targetY, ref targetX);

            int range = 0xff; /* Simeon */
            ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, ref targetY, ref targetX, casterY, casterX);

            localSteppingPathInit(targetY, targetX, casterY, casterX, path);
            int var_76 = find_players_on_path(path, players_on_path);

            if (playerSize > 1)
            {
                int map_b_x = targetX + unk_16D32[var_76];
                int map_b_y = targetY + unk_16D3A[var_76];

                BoundCoords(ref map_b_y, ref map_b_x);

                localSteppingPathInit(map_b_y, map_b_x, casterY, casterX, path);
                find_players_on_path(path, players_on_path);

                if (playerSize > 2)
                {
                    int map_a_x = targetX + unk_16D22[var_76];
                    int map_a_y = targetY + unk_16D2A[var_76];

                    BoundCoords(ref map_a_y, ref map_a_x);

                    localSteppingPathInit(map_a_y, map_a_x, casterY, casterX, path);
                    find_players_on_path(path, players_on_path);
                }
            }

            int sp_target_index = 1;
            gbl.sp_target_count = 0;

            foreach(var idx in players_on_path)
            {
                var player = gbl.player_array[idx];
                if( player != gbl.player_ptr)
                {
                    gbl.sp_target_count += 1;
                    gbl.sp_targets[sp_target_index] = player;
                    sp_target_index++;
                }
            }
        }


        internal static void sub_5DB24(string text, int save_bonus)
        {
            for (int target_index = gbl.sp_target_count; target_index >= 1; target_index--)
            {
                if (gbl.sp_targets[target_index] != null)
                {
                    Player target = gbl.sp_targets[target_index];

                    if (target_index < gbl.sp_target_count)
                    {
                        seg044.sound_sub_120E0(Sound.sound_2);
                        ovr025.load_missile_icons(0x12);

                        ovr025.draw_missile_attack(0x1E, 4, ovr033.PlayerMapYPos(target), ovr033.PlayerMapXPos(target),
                            ovr033.PlayerMapYPos(gbl.player_ptr), ovr033.PlayerMapXPos(gbl.player_ptr));
                    }

                    bool saved;
                    DamageOnSave can_save_flag;

                    if ((gbl.spell_id == 0x4F || gbl.spell_id == 0x51) &&
                        target_index == gbl.sp_target_count)
                    {
                        saved = true;
                        can_save_flag = DamageOnSave.Zero;
                    }
                    else
                    {
                        saved = ovr024.do_saving_throw(save_bonus, gbl.spell_table[gbl.spell_id].field_9, target);
                        can_save_flag = gbl.spell_table[gbl.spell_id].can_save_flag;
                    }

                    if ((target.field_11A > 1 || target.field_DE > 1) &&
                        gbl.spell_id != 0x53)
                    {
                        saved = true;
                    }

                    ovr024.is_unaffected(text, saved, can_save_flag, false, ovr025.spellMaxTargetCount(gbl.spell_id), GetSpellAffectTimeout(gbl.spell_id),
                        gbl.spell_table[gbl.spell_id].affect_id, target);
                }
            }
        }


        internal static void sub_5DCA0(string text, CombatTeam team)
        {
            gbl.byte_1D2C7 = true;

            for (int i = 1; i <= gbl.sp_target_count; i++)
            {
                if (gbl.sp_targets[i] != null &&
                    (gbl.sp_targets[i].combat_team != team ||
                    (gbl.spell_id == 1 && gbl.game_state == GameState.Combat &&
                     ovr025.near_enemy(1, gbl.sp_targets[i]) > 0)))
                {
                    gbl.sp_targets[i] = null;
                }
            }

            sub_5CF7F(text, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cleric_bless() /* is_Blessed */
        {
            sub_5DCA0("is Blessed", gbl.player_ptr.combat_team);
        }


        internal static void cleric_curse() /* is_Cursed */
        {
            sub_5DCA0("is Cursed", gbl.player_ptr.OppositeTeam());
        }


        internal static void cleric_cure_light() /* sub_5DDBC */
        {
            if (gbl.sp_target_count != 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 1), gbl.sp_targets[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_targets[1]);
            }
        }


        internal static void cleric_cause_light() /* sub_5DDF8 */
        {
            sub_5CF7F(string.Empty, DamageType.Magic, ovr024.roll_dice_save(8, 1), false, 0, gbl.spell_id);
        }


        internal static void is_affected()
        {
            sub_5CF7F("is affected", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void is_protected()
        {
            sub_5CF7F("is protected", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cleric_resist_cold() /* is_cold_resistant */
        {
            sub_5CF7F("is cold-resistant", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_5DEE1()
        {
            sub_5CF7F(string.Empty, DamageType.Magic | DamageType.Fire, ovr025.spellMaxTargetCount(gbl.spell_id), false, 0, gbl.spell_id);
        }


        internal static void is_charmed()
        {
            Player target = gbl.sp_targets[1];

            if (target.field_11A > 1 ||
                target.field_DE > 1)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
            else
            {
                sub_5CF7F("is charmed", 0, 0, true, (byte)(((int)gbl.player_ptr.combat_team << 7) + ovr025.spellMaxTargetCount(gbl.spell_id)), gbl.spell_id);

                Affect affect;

                if (ovr025.find_affect(out affect, Affects.charm_person, target) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, gbl.sp_targets[1], Affects.shield);
                }
            }
        }


        internal static void is_stronger()
        {
            Player target = gbl.sp_targets[1];
            gbl.byte_1AFDD = 0x12;
            gbl.byte_1AFDE = 0;

            switch (ovr025.spellMaxTargetCount(gbl.spell_id))
            {
                case 1:
                    gbl.byte_1AFDE = 0;
                    break;

                case 2:
                    gbl.byte_1AFDE = 1;
                    break;

                case 3:
                    gbl.byte_1AFDE = 0x33;
                    break;

                case 4:
                    gbl.byte_1AFDE = 0x4C;
                    break;

                case 5:
                    gbl.byte_1AFDE = 0x5B;
                    break;

                case 6:
                    gbl.byte_1AFDE = 0x64;
                    break;

                case 7:
                    gbl.byte_1AFDD = 0x13;
                    break;

                case 8:
                    gbl.byte_1AFDD = 0x14;
                    break;

                case 9:
                    gbl.byte_1AFDD = 0x15;
                    break;

                case 10:
                    gbl.byte_1AFDD = 0x16;
                    break;

                case 11:
                    gbl.byte_1AFDD = 0x16;
                    break;
            }

            byte encoded_strength;
            if (ovr024.sub_64728(out encoded_strength, gbl.byte_1AFDE, gbl.byte_1AFDD, target) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is stronger", target);

                ovr024.add_affect(true, encoded_strength, GetSpellAffectTimeout(gbl.spell_id), Affects.affect_12, target);

                ovr024.sub_648D9(0, target);
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
        }


        internal static void enlarge_end() /* has_been_reduced */
        {
            Player target = gbl.sp_targets[1];

            if (target != null &&
                gbl.sp_target_count > 0 &&
                ovr024.do_saving_throw(0, 4, target) == false &&
                target.HasAffect(Affects.enlarge) == true)
            {
                ovr024.remove_affect(null, Affects.enlarge, target);
                ovr024.sub_648D9(0, target);
                ovr025.DisplayPlayerStatusString(true, 10, "has been reduced", target);
            }
        }


        internal static void is_friendly()
        {
            sub_5CF7F("is friendly", 0, 0, true, ovr024.roll_dice(4, 2), gbl.spell_id);
            ovr024.sub_648D9(5, gbl.player_ptr);
        }


        internal static void sub_5E221()
        {
            sbyte var_1 = (sbyte)(ovr025.spellMaxTargetCount(gbl.spell_id) + 1);

            sub_5CF7F(string.Empty, DamageType.Magic, (var_1 / 2) + ovr024.roll_dice_save(4, var_1 / 2), false, 0, gbl.spell_id);
        }


        internal static void is_shielded()
        {
            sub_5CF7F("is shielded", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_5E2B2()
        {
            sub_5CF7F(string.Empty, DamageType.Acid | DamageType.Cold, ovr024.roll_dice_save(8, 1) + ovr025.spellMaxTargetCount(gbl.spell_id),
                false, 0, gbl.spell_id);
        }


        internal static void falls_asleep()
        {
            gbl.byte_1D2C7 = true;
            gbl.byte_1AFDD = ovr024.roll_dice(4, 4);

            for (int target_index = 1; target_index <= gbl.sp_target_count; target_index++)
            {
                switch (gbl.sp_targets[target_index].field_E5)
                {
                    case 0:
                    case 1:
                        gbl.byte_1AFDE = 1;
                        break;

                    case 2:
                        gbl.byte_1AFDE = 2;
                        break;

                    case 3:
                        gbl.byte_1AFDE = 4;
                        break;

                    case 4:
                        gbl.byte_1AFDE = 6;
                        break;

                    case 5:
                        if (gbl.sp_targets[target_index].race == Race.monster)
                        {
                            gbl.byte_1AFDE = 0x0A;
                        }
                        else
                        {
                            gbl.byte_1AFDE = 0x14;
                        }
                        break;

                    default:
                        gbl.byte_1AFDE = 0x14;
                        break;
                }


                if (gbl.sp_targets[target_index].health_status != Status.animated &&
                    gbl.sp_targets[target_index].HasAffect(Affects.sleep) == false &&
                    gbl.byte_1AFDD >= gbl.byte_1AFDE)
                {
                    gbl.byte_1AFDD -= gbl.byte_1AFDE;
                }
                else
                {
                    gbl.sp_targets[target_index] = null;
                }
            }

            sub_5CF7F("falls asleep", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void is_held()
        {
            int save_bonus;

            if (gbl.sp_target_count == 1)
            {
                if (gbl.spell_id == 0x17)
                {
                    save_bonus = -2;
                }
                else
                {
                    save_bonus = -3;
                }
            }
            else if (gbl.sp_target_count == 2)
            {
                save_bonus = -1;
            }
            else if (gbl.sp_target_count == 3 || gbl.sp_target_count == 4)
            {
                save_bonus = 0;
            }
            else
            {
                throw new System.NotSupportedException();
            }

            sub_5DB24("is held", save_bonus);
        }


        internal static void is_fire_resistant()
        {
            sub_5CF7F("is fire resistant", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void is_silenced()
        {
            sub_5CF7F("is silenced", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void is_affected2()
        {
            Player player;

            player = gbl.sp_targets[1];

            if (player.health_status == Status.animated)
            {
                gbl.sp_targets[1] = null;
            }
            else if (player.HasAffect(Affects.poisoned) == true)
            {
                if (player.hit_point_current == 0)
                {
                    player.hit_point_current = 1;
                }

                sub_5CF7F("is affected", 0, 0, true, 0xff, gbl.spell_id);
                ovr013.CallAffectTable(Effect.Remove, null, player, Affects.affect_4e);
                ovr024.add_affect(true, 0xff, 10, Affects.affect_0f, player);
            }
        }


        internal static void is_charmed2()
        {
            gbl.byte_1AFDD = gbl.player_ptr.hit_point_current;
            gbl.sp_target_count = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.field_11A == 0x0e &&
                    gbl.byte_1AFDD >= player.hit_point_current)
                {
                    gbl.byte_1AFDD -= player.hit_point_current;
                    gbl.sp_target_count++;
                    gbl.sp_targets[gbl.sp_target_count] = player;
                }
            }

            sub_5CF7F("is charmed", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_5E681()
        {
            sub_5CF7F(string.Empty, 0, 0, true, 0, gbl.spell_id);

            ovr013.CallAffectTable(Effect.Add, null, gbl.sp_targets[1], Affects.spiritual_hammer);
        }


        internal static void is_invisible()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void knock_Knock()
        {
            sub_5CF7F("Knock-Knock", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void is_duplicated()
        {
            int var_1 = ovr024.roll_dice(4, 1) << 4;

            var_1 += ovr025.spellMaxTargetCount(gbl.spell_id);

            sub_5CF7F("is duplicated", 0, 0, false, var_1, gbl.spell_id);
        }


        internal static void is_weakened()
        {
            sub_5CF7F("is weakened", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void create_noxious_cloud() //TODO similar to spell_poisonous_cloud
        {
            byte var_12;
            byte groundTile;
            byte var_D;
            byte[] var_C = new byte[4];

            gbl.byte_1D2C7 = true;

            byte var_10 = (byte)ovr025.spellMaxTargetCount(gbl.spell_id);
            int count = gbl.NoxiousCloud.FindAll(cell => cell.player == gbl.player_ptr).Count;

            GasCloud var_8 = new GasCloud(gbl.player_ptr, count, gbl.targetX, gbl.targetY);
            gbl.NoxiousCloud.Add(var_8);

            ovr024.add_affect(true, (byte)(var_10 + (count << 4)), var_10, Affects.affect_28, gbl.player_ptr);

            for (int var_11 = 1; var_11 <= 4; var_11++)
            {
                var_12 = gbl.unk_18AE9[var_11];

                ovr033.AtMapXY(out groundTile, out var_C[var_11 - 1],
                    gbl.targetY + gbl.MapDirectionYDelta[var_12],
                    gbl.targetX + gbl.MapDirectionXDelta[var_12]);


                if (groundTile > 0 && gbl.BackGroundTiles[groundTile].move_cost < 0xFF)
                {
                    var_8.field_10[var_11] = 1;
                }
                else
                {
                    var_8.field_10[var_11] = 0;
                }


                if (groundTile == 0x1E)
                {
                    foreach (var var_4 in gbl.NoxiousCloud)
                    {
                        if (var_4 != var_8)
                        {
                            for (var_D = 1; var_D <= 4; var_D++)
                            {
                                if (var_4.field_10[var_D] != 0)
                                {
                                    if (gbl.targetX + gbl.MapDirectionXDelta[var_12] == var_4.target_x + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_D]] &&
                                        gbl.targetY + gbl.MapDirectionYDelta[var_12] == gbl.MapDirectionYDelta[gbl.unk_18AE9[var_D]] + var_4.target_y)
                                    {
                                        if (var_4.field_7[var_D] != 0x1E)
                                        {
                                            groundTile = var_4.field_7[var_D];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (groundTile == 0x1F)
                {
                    for (var_D = 1; var_D <= gbl.byte_1D1BB; var_D++)
                    {
                        if (gbl.unk_1D183[var_D].mapX == gbl.targetX + gbl.MapDirectionXDelta[var_12] &&
                            gbl.unk_1D183[var_D].mapY == gbl.targetY + gbl.MapDirectionYDelta[var_12])
                        {
                            groundTile = gbl.unk_1D183[var_D].field_6;
                        }
                    }
                }

                var_8.field_7[var_11] = groundTile;
                if (var_8.field_10[var_11] != 0)
                {
                    int tmp_x = gbl.MapDirectionXDelta[var_12] + gbl.targetX;
                    int tmp_y = gbl.MapDirectionYDelta[var_12] + gbl.targetY;

                    gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1E;
                }
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a noxious cloud", gbl.player_ptr);

            ovr033.redrawCombatArea(8, 0xff, gbl.targetY, gbl.targetX);
            seg041.GameDelay();
            ovr025.ClearPlayerTextArea();
            for (int var_11 = 0; var_11 < 4; var_11++)
            {
                for (var_D = 0; var_D < 4; var_D++)
                {
                    if (var_C[var_D] == var_C[var_11] &&
                        var_11 != var_D)
                    {
                        var_C[var_11] = 0;
                    }
                }
            }

            for (int var_11 = 0; var_11 < 4; var_11++)
            {
                if (var_C[var_11] > 0)
                {
                    ovr024.in_poison_cloud(1, gbl.player_array[var_C[var_11]]);
                }
            }
        }


        internal static void sub_5EC5B()
        {
            byte var_6 = 0; /* simeon added */
            byte var_5 = 0;
            Player target = gbl.sp_targets[1];

            if (target.magic_user_lvl > 0 ||
                target.field_116 > target.field_E6)
            {
                var_6 = ovr024.roll_dice(4, 1);
            }

            if (target.cleric_lvl > 0 ||
                target.turn_undead > target.field_E6 ||
                target.thief_lvl > 0 ||
                target.field_117 > target.field_E6)
            {
                var_6 = ovr024.roll_dice(6, 1);
            }

            if (target.fighter_lvl > 0 ||
                target.field_113 > target.field_E6)
            {

                var_6 = ovr024.roll_dice(8, 1);
            }

            byte var_7 = (byte)(target.strength + var_6);

            if (var_7 > 18)
            {
                if (target.fighter_lvl > 0 ||
                    target.field_113 > target.field_E6 ||
                    target.paladin_lvl > 0 ||
                    target.field_114 > target.field_E6 ||
                    target.ranger_lvl > 0 ||
                    target.field_115 > target.field_E6)
                {
                    var_5 = (byte)(target.tmp_str_00 + ((var_7 - 18) * 10));

                    if (var_5 > 100)
                    {
                        var_5 = 100;
                    }

                    var_7 = 18;
                }
                else
                {
                    var_7 = 18;
                }
            }

            byte encoded_str;

            if (ovr024.sub_64728(out encoded_str, var_5, var_7, target) == true)
            {
                encoded_str = (byte)(var_6 + 100);

                ovr024.add_affect(true, encoded_str, GetSpellAffectTimeout(gbl.spell_id), Affects.strength, target);
                ovr024.sub_648D9(0, target);
            }
        }


        internal static void is_animated()
        {
            gbl.byte_1D2C7 = true;

            int var_3 = ovr025.spellMaxTargetCount(gbl.spell_id);

            gbl.sp_target_count = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.health_status == Status.dead &&
                    player.field_11A == 0)
                {
                    if (ovr033.sub_7515A(true, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player), player) == true)
                    {
                        byte var_2 = (byte)(((int)player.combat_team << 4) + ovr025.spellMaxTargetCount(gbl.spell_id));

                        player.combat_team = gbl.player_ptr.combat_team;
                        player.quick_fight = QuickFight.True;
                        player.field_E9 = 1;
                        player.field_DD = 0;
                        player.base_movement = 6;

                        for (int var_1 = 0; var_1 < gbl.max_spells; var_1++)
                        {
                            player.spell_list[var_1] = 0;
                        }

                        if (player.field_F7 > 0x7F)
                        {
                            player.field_F7 = 0xB2;
                        }
                        else
                        {
                            player.field_F7 = 0xB3;
                        }

                        player.field_11A = 4;

                        if (gbl.game_state == GameState.Combat)
                        {
                            player.actions.target = null;
                        }

                        var_3--;

                        if (ovr024.combat_heal(player.hit_point_max, player) == true)
                        {
                            ovr024.is_unaffected("is animated", false, 0, true, var_2, 0, Affects.animate_dead, player);
                            player.health_status = Status.animated;
                        }
                    }
                }

                if (var_3 <= 0) break;
            }
        }


        internal static void can_see()
        {
            if (ovr024.cure_affect(Affects.blinded, gbl.sp_targets[1]) == true)
            {
                ovr025.sub_6818A("can see", true, gbl.sp_targets[1]);
            }
        }


        internal static void is_blind()
        {
            sub_5CF7F("is blind", 0, 0, false, 0, gbl.spell_id);
        }


        internal static bool sub_5F037()
        {
            bool var_1 = false;

            gbl.cureSpell = true;

            if (ovr024.cure_affect(Affects.cause_disease_1, gbl.sp_targets[1]) == true)
            {
                var_1 = true;
            }

            if (ovr024.cure_affect(Affects.affect_2b, gbl.sp_targets[1]) == true)
            {
                var_1 = true;

                ovr024.remove_affect(null, Affects.cause_disease_2, gbl.sp_targets[1]);
                ovr024.remove_affect(null, Affects.helpless, gbl.sp_targets[1]);
            }

            if (ovr024.cure_affect(Affects.hot_fire_shield, gbl.sp_targets[1]) == true)
            {
                var_1 = true;
                ovr024.remove_affect(null, Affects.affect_39, gbl.sp_targets[1]);
            }

            gbl.cureSpell = false;

            return var_1;
        }


        internal static void sub_5F0DC()
        {
            sub_5F037();
        }


        internal static void is_diseased()
        {
            sub_5CF7F("is diseased", 0, 0, true, 0, gbl.spell_id);
        }


        internal static bool sub_5F126(Player arg_2, int target_count)
        {
            bool var_1;

            gbl.byte_1AFDE = (byte)(arg_2.magic_user_lvl + (arg_2.field_116 * ovr026.sub_6B3D1(gbl.player_ptr)));

            if (target_count > gbl.byte_1AFDE)
            {
                gbl.byte_1AFDD = (byte)(((target_count - gbl.byte_1AFDE) * 5) + 50);
            }
            else if (target_count < gbl.byte_1AFDE)
            {
                gbl.byte_1AFDD = (byte)(50 - ((gbl.byte_1AFDE - target_count) * 2));
            }
            else
            {
                gbl.byte_1AFDD = 50;
            }

            var_1 = (ovr024.roll_dice(100, 1) <= gbl.byte_1AFDD);

            return var_1;
        }

        public class Tuple<S, T, U>
        {
            public S s;
            public T t;
            public U u;

            public Tuple(S _s, T _t, U _u)
            { s = _s; t = _t; u = _u; }

        }


        internal static void is_affected3()
        {
            gbl.byte_1D2C7 = true;
            int maxTargetCount = ovr025.spellMaxTargetCount(gbl.spell_id);

            for (int i = 1; i <= gbl.sp_target_count; i++)
            {
                bool is_affected = false;
                Player target = gbl.sp_targets[1];

                List<Tuple<Affect, byte, byte>> removeList = new List<Tuple<Affect, byte, byte>>();

                foreach (Affect var_C in target.affects)
                {
                    if (var_C.affect_data < 0xff)
                    {
                        gbl.byte_1AFDE = (byte)(var_C.affect_data & 0x0f);

                        if (maxTargetCount > gbl.byte_1AFDE)
                        {
                            gbl.byte_1AFDD = (byte)(50 + ((maxTargetCount - gbl.byte_1AFDE) * 5));
                        }
                        else if (maxTargetCount < gbl.byte_1AFDE)
                        {
                            gbl.byte_1AFDD = (byte)(50 - ((gbl.byte_1AFDE - maxTargetCount) * 2));
                        }
                        else
                        {
                            gbl.byte_1AFDD = 50;
                        }

                        if (ovr024.roll_dice(100, 1) <= gbl.byte_1AFDD)
                        {
                            removeList.Add(new Tuple<Affect, byte, byte>(var_C, gbl.byte_1AFDD, gbl.byte_1AFDE));
                            is_affected = true;
                        }
                    }
                }

                foreach (Tuple<Affect, byte, byte> remove in removeList)
                {
                    gbl.byte_1AFDD = remove.t;
                    gbl.byte_1AFDE = remove.u;
                    Affect affect = remove.s;
                    ovr024.remove_affect(affect, affect.type, target);
                }
                removeList.Clear();

                if (is_affected == true)
                {
                    ovr025.sub_6818A("is affected", true, target);
                }
            }

            int var_12 = 0;
            int var_11 = 0;

            for (gbl.global_index = 0; gbl.global_index <= 8; gbl.global_index++)
            {
                switch (gbl.global_index)
                {
                    case 0:
                        var_11 = gbl.targetX;
                        var_12 = gbl.targetY;
                        break;

                    case 1:
                        var_12 = gbl.targetY - 1;
                        break;

                    case 2:
                        var_11 = gbl.targetX - 1;
                        break;

                    case 3:
                        var_12 = gbl.targetY;
                        break;

                    case 4:
                        var_12 = gbl.targetY + 1;
                        break;

                    case 5:
                        var_11 = gbl.targetX;
                        break;

                    case 6:
                        var_11 = gbl.targetX - 1;
                        break;

                    case 7:
                        var_12 = gbl.targetY;
                        break;

                    case 8:
                        var_12 = gbl.targetY - 1;
                        break;
                }

                byte dummy_byte;
                byte ground_tile;
                ovr033.AtMapXY(out ground_tile, out dummy_byte, var_12, var_11);

                if (ground_tile == 0x1C || ground_tile == 0x1E)
                {
                    // Struct_1D885 var_18;
                    int var_14 = (ground_tile == 0x1C) ? 9 : 4;
                    var looplist = (ground_tile == 0x1C) ? gbl.PoisonousCloud : gbl.NoxiousCloud;


                    looplist.ForEach(var_18 =>
                    {
                        for (int var_1 = 1; var_1 <= var_14; var_1++)
                        {
                            if (var_11 == var_18.target_x + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_1]])
                            {
                                int tmp_int = (var_18.target_y + gbl.MapDirectionYDelta[gbl.unk_18AE9[var_1]]);

                                if (var_12 == tmp_int &&
                                    var_18.field_1D == false)
                                {
                                    if (sub_5F126(var_18.player, maxTargetCount) == true)
                                    {
                                        Affect affect = null;
                                        bool found = false;

                                        foreach (Affect tmpAffect in var_18.player.affects)
                                        {
                                            if (((affect.type == Affects.affect_5b && ground_tile == 0x1c) ||
                                                 (affect.type == Affects.affect_28 && ground_tile == 0x1E)) &&
                                                (affect.affect_data >> 4) == var_18.field_1C)
                                            {
                                                affect = tmpAffect;
                                                found = true;
                                                break;
                                            }
                                        }

                                        if (found == true)
                                        {
                                            if (ground_tile == 0x1C)
                                            {
                                                ovr024.remove_affect(affect, Affects.affect_5b, var_18.player);
                                            }
                                            else
                                            {
                                                ovr024.remove_affect(affect, Affects.affect_28, var_18.player);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var_18.field_1D = true;
                                    }
                                }
                            }
                        }
                    });


                }
            }
        }


        internal static void is_praying()
        {
            byte tmpByte = (byte)(((int)gbl.player_ptr.combat_team * 16) + ovr025.spellMaxTargetCount(gbl.spell_id));

            sub_5CF7F("is praying", 0, 0, false, tmpByte, gbl.spell_id);
        }


        internal static void uncurse()
        {
            if (ovr024.cure_affect(Affects.bestow_curse, gbl.sp_targets[1]) == true)
            {
                ovr025.sub_6818A("is un-cursed", true, gbl.sp_targets[1]);
            }
            else
            {
                Item item = gbl.sp_targets[1].items.Find(i => i.cursed);

                if (item != null)
                {
                    item.readied = false;

                    if ((int)item.affect_3 > 0x7F)
                    {
                        gbl.applyItemAffect = true;
                        ovr013.CallAffectTable(Effect.Remove, item, gbl.sp_targets[1], item.affect_3);

                        for (int var_6 = 0; var_6 <= 5; var_6++)
                        {
                            ovr024.sub_648D9(var_6, gbl.sp_targets[1]);
                        }

                    }

                    ovr025.sub_6818A("has an item un-cursed", true, gbl.sp_targets[1]);
                }
            }
        }


        internal static void curse()
        {
            sub_5CF7F("has been cursed!", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void spell_blinking()
        {
            sub_5CF7F("is blinking", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_5F782()
        {
            int dice_count;

            gbl.byte_1D2C7 = true;

            if (gbl.spell_id == 0x40)
            {
                dice_count = (ovr024.roll_dice(3, 1) * 2) + 1;
            }
            else
            {
                dice_count = ovr025.spellMaxTargetCount(gbl.spell_id);
            }

            if (gbl.area_ptr.field_1CC == 0)
            {
                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, 2, gbl.targetY, gbl.targetX);

                for (int i = 1; i <= gbl.sortedCombatantCount; i++)
                {
                    gbl.sp_targets[i] = gbl.player_array[gbl.SortedCombatantList[i].player_index];
                }

                gbl.sp_target_count = gbl.sortedCombatantCount;
            }

            ovr033.redrawCombatArea(8, 0, gbl.targetY, gbl.targetX);

            sub_5CF7F(string.Empty, DamageType.Magic | DamageType.Fire, ovr024.roll_dice_save(6, dice_count), false, 0, gbl.spell_id);
        }


        internal static void RemoveComplimentSpellFirst(string text, CombatTeam combatTeam, Affects affect) //sub_5F87B
        {
            gbl.byte_1D2C7 = true;

            int maxTargets = ovr025.spellMaxTargetCount(gbl.spell_id);

            for (int index = 1; index <= gbl.sp_target_count; index++)
            {
                if (gbl.sp_targets[index].combat_team == combatTeam &&
                    maxTargets > 0)
                {
                    maxTargets -= 1;

                    if (ovr024.cure_affect(affect, gbl.sp_targets[index]) == true)
                    {
                        gbl.sp_targets[index] = null;
                    }
                }
                else
                {
                    gbl.sp_targets[index] = null;
                }
            }

            sub_5CF7F(text, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cast_haste()
        {
            RemoveComplimentSpellFirst("is Hasted", gbl.player_ptr.combat_team, Affects.slow);
        }


        internal static void sub_5F986(ref bool arg_0, byte player_index, byte arg_6, int damage, int posY, int posX)
        {
            byte groundTile;
            byte playerIndex;

            ovr033.AtMapXY(out groundTile, out playerIndex, posY, posX);

            if (groundTile > 0 &&
                gbl.BackGroundTiles[groundTile].move_cost == 0xff &&
                gbl.area_ptr.field_1CC == 1 &&
                arg_0 == false)
            {
                arg_0 = true;
            }
            else
            {
                arg_0 = false;
            }

            if (playerIndex > 0 &&
                playerIndex != player_index)
            {
                Player player = gbl.player_array[playerIndex];
                gbl.damage_flags = DamageType.Magic | DamageType.Electricity;

                ovr024.damage_person(ovr024.do_saving_throw(0, arg_6, player), DamageOnSave.Half, damage, player);
                ovr025.load_missile_icons(0x13);
                gbl.damage_flags = 0;
            }
        }


        internal static void sub_5FA44(byte arg_0, byte arg_2, int damage, byte arg_6)
        {
            byte var_3A = 0; /* Simeon */
            bool var_36 = false;
            ovr025.load_missile_icons(0x13);

            byte var_39;
            byte groundTile;

            ovr033.AtMapXY(out groundTile, out var_39, gbl.targetY, gbl.targetX);
            int var_3D = 0;
            int var_35 = 1;

            int var_31 = ovr033.PlayerMapXPos(gbl.player_ptr);
            int var_32 = ovr033.PlayerMapYPos(gbl.player_ptr);
            byte var_38 = arg_0;

            if (var_31 != gbl.targetX ||
                var_32 != gbl.targetY)
            {
                int var_3C = arg_6 * 2;
                gbl.byte_1D2C7 = true;

                while (var_3C > 0)
                {
                    var path_a = new SteppingPath();

                    path_a.attacker_x = gbl.targetX;
                    path_a.attacker_y = gbl.targetY;
                    path_a.target_x = gbl.targetX + ((gbl.targetX - var_31) * var_35 * var_3C);
                    path_a.target_y = gbl.targetY + ((gbl.targetY - var_32) * var_35 * var_3C);

                    path_a.CalculateDeltas();

                    do
                    {
                        int tmp_x = path_a.current_x;
                        int tmp_y = path_a.current_y;

                        if (path_a.attacker_x != path_a.target_x ||
                            path_a.attacker_y != path_a.target_y)
                        {
                            bool stepping;

                            do
                            {
                                stepping = path_a.Step();

                                ovr033.AtMapXY(out groundTile, out var_3A, path_a.current_y, path_a.current_x);

                                if (gbl.BackGroundTiles[groundTile].move_cost == 1)
                                {
                                    var_36 = false;
                                }

                            } while (stepping == true &&
                                (var_3A <= 0 || var_3A == var_39) &&
                                groundTile != 0 &&
                                gbl.BackGroundTiles[groundTile].move_cost <= 1 &&
                                path_a.steps < var_3C);
                        }

                        if (groundTile == 0)
                        {
                            var_3C = 0;
                        }

                        ovr025.draw_missile_attack(0x32, 4, path_a.current_y, path_a.current_x, tmp_y, tmp_x);

                        sub_5F986(ref var_36, var_39, arg_2, damage, path_a.current_y, path_a.current_x);
                        var_39 = var_3A;

                        if (var_36 == true)
                        {
                            gbl.targetX = path_a.current_x;
                            gbl.targetY = path_a.current_y;

                            var path_b = new SteppingPath();

                            path_b.attacker_x = gbl.targetX;
                            path_b.attacker_y = gbl.targetY;
                            path_b.target_y = var_31;
                            path_b.target_x = var_32;

                            path_b.CalculateDeltas();

                            while (path_b.Step() == true)
                            {
                                /* empty */
                            }

                            if (var_38 != 0 && path_b.steps <= 8)
                            {
                                path_a.steps += 8;
                            }

                            var_35 = -var_35;
                            var_38 = 0;
                            var_39 = 0;
                        }

                        var_3D = (byte)(path_a.steps - var_3D);

                        if (var_3D < var_3C)
                        {
                            var_3C -= var_3D;
                        }
                        else
                        {
                            var_3C = 0;
                        }

                        var_3D = path_a.steps;
                    } while (var_36 == false && var_3C != 0);
                }

                gbl.byte_1D2C7 = false;
            }
        }


        internal static void sub_5FCD9()
        {
            bool var_2 = false; /* Simeon */

            int damage = ovr024.roll_dice(6, ovr025.spellMaxTargetCount(gbl.spell_id));

            sub_5F986(ref var_2, 0, 4, damage, gbl.targetY, gbl.targetX);
            sub_5FA44(1, 4, damage, 7);

        }


        internal static void sub_5FD2E()
        {
            RemoveComplimentSpellFirst("is Slowed", gbl.player_ptr.OppositeTeam(), Affects.haste);
        }


        internal static void cast_restore()
        {
            int var_C = 30; /* simeon */

            Player player = gbl.sp_targets[1];

            if (player.field_E7 > 0)
            {
                byte var_5 = (byte)(player.field_E8 / player.field_E7);

                player.hit_point_max += var_5;
                player.hit_point_current += var_5;
                player.field_12C += var_5;
                player.field_E8 -= var_5;
                player.field_E7--;

                int max_lvl = 13;
                int max_exp = 10000000;

                for (int skill = 0; skill <= 7; skill++)
                {
                    int lvl = player.class_lvls[skill];

                    if (lvl > 0 &&
                        lvl <= max_lvl)
                    {
                        if (ovr018.exp_table[skill, lvl] > 0 &&
                            ovr018.exp_table[skill, lvl] < max_exp &&
                            ovr025.sub_69138(skill, player) == false)
                        {
                            max_lvl = lvl;
                            var_C = skill;
                            max_exp = ovr018.exp_table[skill, lvl];
                        }
                    }
                }

                player.class_lvls[var_C]++;

                if (player.exp < max_exp)
                {
                    player.exp = max_exp;
                }

                ovr026.sub_6A3C6(player);
                ovr025.DisplayPlayerStatusString(true, 10, "is restored", player);
            }
        }


        internal static void cast_speed()
        {
            if (ovr024.cure_affect(Affects.slow, gbl.sp_targets[1]) == false)
            {
                sub_5CF7F("is Speedy", 0, 0, false, 0, gbl.spell_id);
            }
        }


        internal static void sub_5FF6D()
        {
            if (gbl.sp_target_count != 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 2) + 1, gbl.sp_targets[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_targets[1]);
            }
        }


        internal static void cast_strength()
        {
            byte var_1 = 0;

            if (ovr024.sub_64728(out var_1, 0, 0x15, gbl.sp_targets[1]) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is stronger", gbl.sp_targets[1]);
            }

            ovr024.add_affect(true, var_1, (ushort)((ovr024.roll_dice(4, 1) * 10) + 0x28), Affects.strenght_spell, gbl.sp_targets[1]);
            ovr024.sub_648D9(0, gbl.sp_targets[1]);
        }


        internal static void sub_6003C()
        {
            bool var_1 = false;

            sub_5F986(ref var_1, 0, 4, ovr024.roll_dice(6, 1) + 20, gbl.targetY, gbl.targetX);
            sub_5FA44(0, 4, 20, 3);
        }


        internal static void cast_paralyzed()
        {
            sub_5CF7F("is paralyzed", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cast_heal()
        {
            if (ovr024.heal_player(0, ovr024.roll_dice(4, 2) + 2, gbl.sp_targets[1]) == true)
            {
                ovr025.sub_6818A("is Healed", true, gbl.sp_targets[1]);
            }
        }


        internal static void cast_invisible()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void dam2d4plus2()
        {
            sub_5CF7F(string.Empty, DamageType.Magic, ovr024.roll_dice_save(4, 2) + 2, false, 0, gbl.spell_id);
        }


        internal static void dam2d8plus1() // sub_60185
        {
            sub_5CF7F(string.Empty, DamageType.Magic, ovr024.roll_dice_save(8, 2) + 1, false, 0, gbl.spell_id);
        }


        internal static void cure_poison()
        {
            Player target = gbl.sp_targets[1];

            if (target.health_status == Status.animated)
            {
                gbl.sp_targets[1] = null;
            }
            else if (target.HasAffect(Affects.poisoned) == true)
            {
                if (target.hit_point_current == 0)
                {
                    target.hit_point_current = 1;
                }

                gbl.cureSpell = true;

                ovr024.remove_affect(null, Affects.poisoned, target);
                ovr024.remove_affect(null, Affects.slow_poison, target);
                ovr024.remove_affect(null, Affects.affect_0f, target);

                gbl.cureSpell = false;

                ovr025.DisplayPlayerStatusString(true, 10, "is unpoisoned", target);

                target.in_combat = true;
                target.health_status = Status.okey;
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
        }


        internal static void sub_602D0()
        {
            Affect var_2 = null; /* Simeon */

            sub_5CF7F(string.Empty, DamageType.Magic, 0, false, 0, gbl.spell_id);
            Player target = gbl.player_ptr.actions.target;
            gbl.current_affect = Affects.affect_40;
            ovr024.CheckAffectsEffect(target, CheckType.Type_9);

            if (gbl.current_affect == Affects.affect_40)
            {
                ovr013.CallAffectTable(Effect.Add, var_2, gbl.player_ptr, Affects.affect_40);
            }
        }


        internal static void cast_flattern()
        {
            if (gbl.sp_targets[1].field_E5 < 6)
            {
                sub_5CF7F(string.Empty, DamageType.Magic, 0, false, ovr025.spellMaxTargetCount(gbl.spell_id), gbl.spell_id);

                Affect affect;
                if (ovr025.find_affect(out affect, Affects.sticks_to_snakes, gbl.sp_targets[1]) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, gbl.sp_targets[1], Affects.sticks_to_snakes);
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "smashes them flat", gbl.sp_targets[1]);
            }
        }


        internal static void sub_603F0()
        {
            if (gbl.sp_target_count != 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 3) + 3, gbl.sp_targets[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_targets[1]);
            }
        }


        internal static void dam3d8plus3() // sub_60431
        {
            sub_5CF7F(string.Empty, DamageType.Magic, ovr024.roll_dice_save(8, 3) + 3, false, 0, gbl.spell_id);
        }


        internal static void is_affected4()
        {
            ovr024.is_unaffected(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(0x49), Affects.dispel_evil, gbl.player_ptr);
            sub_5CF7F("is affected", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_604DA()
        {
            sub_5CF7F(string.Empty, DamageType.Magic | DamageType.Fire, ovr024.roll_dice_save(8, 6), false, 0, gbl.spell_id);
        }


        internal static void cast_raise()
        {
            Player player = gbl.sp_targets[1];

            if ((player.health_status == Status.dead || player.health_status == Status.animated) &&
                player.tmp_con > 0 &&
                player.race != Race.elf)
            {
                gbl.cureSpell = true;

                ovr024.remove_affect(null, Affects.animate_dead, player);
                ovr024.remove_affect(null, Affects.poisoned, player);
                gbl.cureSpell = false;

                player.health_status = Status.okey;
                player.in_combat = true;
                player.tmp_con--;

                ovr024.sub_648D9(4, player);
                player.hit_point_current = 1;

                ovr025.DisplayPlayerStatusString(true, 10, "is raised", player);
            }
        }


        internal static void cast_slay()
        {
            Player target = gbl.sp_targets[1];
            gbl.damage_flags = DamageType.Unknown40;
            gbl.damage = 0x43;
            ovr024.CheckAffectsEffect(target, CheckType.Type_9);

            if (gbl.damage != 0)
            {
                if (ovr024.do_saving_throw(0, 4, target) == false)
                {
                    ovr024.sub_63014("is slain", Status.dead, target);
                }
                else
                {
                    gbl.damage_flags = DamageType.Magic;

                    ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2) + 1, target);
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
        }


        internal static void cast_entangle()
        {
            if (gbl.area_ptr.field_1CC == 0)
            {
                for (int i = 1; i <= gbl.sp_target_count; i++)
                {
                    if (gbl.sp_targets[i] != null)
                    {
                        Player target = gbl.sp_targets[i];

                        bool saved = ovr024.do_saving_throw(0, 4, target);

                        ovr024.is_unaffected("is entangled", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout(0x88), Affects.entangle, target);
                    }
                }
            }
        }


        internal static void cast_faerie_fire() /* cast_highlisht */
        {
            sub_5DB24("is highlighted", 0);
        }


        internal static void cast_invisible2()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cast_charmed()
        {
            sub_5DB24("is charmed", 0);

            for (int i = 1; i <= gbl.sp_target_count; i++)
            {
                Affect affect;

                if (ovr025.find_affect(out affect, Affects.charm_person, gbl.sp_targets[i]) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, gbl.sp_targets[i], Affects.charm_person);
                }
            }
        }


        internal static void cast_confuse()
        {
            int target_count = ovr024.roll_dice(8, 2);

            if (gbl.sp_target_count > target_count)
            {
                gbl.sp_target_count = target_count;
            }

            for (int target_idx = 1; target_idx <= gbl.sp_target_count; target_idx++)
            {
                if (gbl.sp_targets[target_idx] != null)
                {
                    Player target = gbl.sp_targets[target_idx];

                    bool saved = ovr024.do_saving_throw(0, 6, target);

                    ovr024.is_unaffected("is confused", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout(0x52), Affects.cause_disease_2, target);
                }
            }
        }


        internal static void cast_teleport()
        {
            Affect affect;
            Player player = gbl.player_ptr;

            if (ovr025.find_affect(out affect, Affects.affect_3a, player) == true)
            {
                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, 1, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                for (int i = 0; i < gbl.sortedCombatantCount; i++)
                {
                    Player playerB = gbl.player_array[gbl.SortedCombatantList[i].player_index];

                    if (ovr025.find_affect(out affect, Affects.affect_90, playerB) == true ||
                        ovr025.find_affect(out affect, Affects.affect_8b, playerB) == true)
                    {
                        if (gbl.player_array[affect.affect_data] == player)
                        {
                            ovr024.remove_affect(null, Affects.affect_90, playerB);
                            ovr024.remove_affect(null, Affects.affect_8b, playerB);
                        }
                    }
                }
            }

            ovr033.draw_74572(ovr033.get_player_index(player), 0, 0);

            ovr033.sub_7515A(false, gbl.targetY, gbl.targetX, player);

            ovr033.redrawCombatArea(8, 0, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

            ovr025.DisplayPlayerStatusString(true, 10, "teleports", player);
        }


        internal static void cast_fear() /* cast_terror */
        {
            Player caster = gbl.player_ptr;

            sub_5D7CF(6, 3, gbl.targetY, gbl.targetX, ovr033.PlayerMapYPos(caster), ovr033.PlayerMapXPos(caster));

            for (int var_9 = 1; var_9 < gbl.sp_target_count; var_9++)
            {
                Player target = gbl.sp_targets[var_9];

                bool saves = ovr024.do_saving_throw(0, 4, target);

                if (saves == false)
                {
                    ovr024.is_unaffected("runs in terror", saves, DamageOnSave.Zero, true, 0, GetSpellAffectTimeout(0x54), Affects.fear, target);
                    target.actions.fleeing = true;
                    target.quick_fight = QuickFight.True;

                    if (target.field_F7 <= 0x7F)
                    {
                        target.field_F7 = 0xB3;
                    }

                    target.actions.target = null;
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
                }
            }
        }


        internal static void cast_protection()
        {
            char input_key;

            bool var_3 = false;

            do
            {
                if (gbl.player_ptr.quick_fight == QuickFight.True)
                {
                    if (ovr024.roll_dice(10, 1) > 5)
                    {
                        input_key = 'H';
                    }
                    else
                    {
                        input_key = 'C';
                    }
                }
                else
                {
                    bool dummy_bool;
                    input_key = ovr027.displayInput(out dummy_bool, false, 0, 15, 10, 13, "Hot Cold", "flame type: ");
                }

                if (input_key == 'H')
                {
                    ovr024.is_unaffected("is protected", false, 0, false, 0, GetSpellAffectTimeout(0x55), Affects.hot_fire_shield, gbl.player_ptr);
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(0x55), Affects.protect_elec, gbl.player_ptr);
                    var_3 = true;
                }
                else if (input_key == 'C')
                {
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(0x55), Affects.cold_fire_shield, gbl.player_ptr);
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(0x55), Affects.protect_elec, gbl.player_ptr);
                    var_3 = true;
                }
                else
                {
                    bool dummy_bool;
                    input_key = ovr027.displayInput(out dummy_bool, false, 0, 15, 10, 13, "Yes No", "Abort spell? ");

                    if (input_key == 'Y')
                    {
                        var_3 = true;
                    }
                }

            } while (var_3 == false);
        }


        internal static void spell_slow()
        {
            Player target = gbl.sp_targets[1];
            gbl.damage_flags = DamageType.Unknown40;

            if (ovr024.do_saving_throw(0, 4, target) == false)
            {
                ovr024.is_unaffected("is clumsy", false, 0, false, 0, GetSpellAffectTimeout(0x56), Affects.fumbling, target);

                if (target.HasAffect(Affects.fumbling) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, null, target, Affects.fumbling);
                }
            }
            else
            {
                ovr024.is_unaffected("is slowed", false, 0, false, 0, GetSpellAffectTimeout(0x56), Affects.slow, target);

                if (target.HasAffect(Affects.slow) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, null, target, Affects.slow);
                }
            }
            sub_5CF7F("is clumsy", 0, 0, true, 0, gbl.spell_id);
        }


        internal static void sub_60F0B()
        {
            sub_5CF7F(string.Empty, DamageType.Acid, ovr024.roll_dice_save(10, 3), false, 0, gbl.spell_id);
        }


        internal static void sub_60F4E()
        {
            sub_5CF7F("is protected", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void spell_poisonous_cloud() // similar to create_noxious_cloud
        {
            byte dir = 0;
            byte var_16;
            byte ground_tile = 0;
            byte[] var_11 = new byte[10];

            gbl.byte_1D2C7 = true;

            byte var_15 = (byte)ovr025.spellMaxTargetCount(gbl.spell_id);
            int count = gbl.PoisonousCloud.FindAll(cell => cell.player == gbl.player_ptr).Count;

            GasCloud var_8 = new GasCloud(gbl.player_ptr, count, gbl.targetX, gbl.targetY);
            gbl.PoisonousCloud.Add(var_8);

            ovr024.add_affect(true, (byte)(var_15 + (count << 4)), var_15, Affects.affect_5b, gbl.player_ptr);

            for (var_16 = 1; var_16 <= 9; var_16++)
            {
                dir = gbl.unk_18AED[var_16];

                ovr033.AtMapXY(out ground_tile, out var_11[var_16],
                    gbl.targetY + gbl.MapDirectionYDelta[dir],
                    gbl.targetX + gbl.MapDirectionXDelta[dir]);

                if (ground_tile > 0 &&
                    gbl.BackGroundTiles[ground_tile].move_cost < 0xff)
                {
                    var_8.field_10[var_16] = 1;
                }
                else
                {
                    var_8.field_10[var_16] = 0;
                }

                if (ground_tile == 0x1E)
                {
                    bool found = false;
                    foreach (var var_4 in gbl.NoxiousCloud)
                    {
                        for (int var_12 = 1; var_12 <= 4; var_12++)
                        {
                            if (var_4.field_10[var_12] != 0 &&
                                (gbl.MapDirectionXDelta[gbl.unk_18AE9[var_12]] + var_4.target_x) == (gbl.MapDirectionXDelta[dir] + gbl.targetX) &&
                                (gbl.MapDirectionYDelta[gbl.unk_18AE9[var_12]] + var_4.target_y) == (gbl.MapDirectionYDelta[dir] + gbl.targetY) &&
                                var_4.field_7[var_12] != 0x1E &&
                                var_4.field_7[var_12] != 0x1C)
                            {
                                ground_tile = var_4.field_7[var_12];
                                found = true;
                            }
                        }

                        if (found) break;
                    }
                }
                else if (ground_tile == 0x1C)
                {
                    bool found = false;
                    foreach (GasCloud var_4 in gbl.PoisonousCloud)
                    {
                        if (var_4 != var_8)
                        {
                            for (int var_12 = 1; var_12 <= 9; var_12++)
                            {
                                if (var_4.field_10[var_12] != 0 &&
                                    (gbl.MapDirectionXDelta[gbl.unk_18AED[var_12]] + var_4.target_x) == (gbl.MapDirectionXDelta[dir] + gbl.targetX) &&
                                    (gbl.MapDirectionYDelta[gbl.unk_18AED[var_12]] + var_4.target_y) == (gbl.MapDirectionYDelta[dir] + gbl.targetY) &&
                                    var_4.field_7[var_12] != 0x1E &&
                                    var_4.field_7[var_12] != 0x1C)
                                {
                                    ground_tile = var_4.field_7[var_12];
                                    found = true;
                                }
                            }
                        }

                        if (found) break;
                    }
                }
                else if (ground_tile == 0x1F)
                {
                    for (int var_12 = 1; var_12 <= gbl.byte_1D1BB; var_12++)
                    {
                        if (gbl.unk_1D183[var_12].mapX == (gbl.MapDirectionXDelta[dir] + gbl.targetX) &&
                            gbl.unk_1D183[var_12].mapY == (gbl.MapDirectionYDelta[dir] + gbl.targetY))
                        {
                            ground_tile = gbl.unk_1D183[var_12].field_6;
                        }
                    }
                }

                var_8.field_7[var_16] = ground_tile;

                if (var_8.field_10[var_16] != 0)
                {
                    int cx = gbl.MapDirectionXDelta[dir] + gbl.targetX;
                    int ax = gbl.MapDirectionYDelta[dir] + gbl.targetY;

                    gbl.mapToBackGroundTile[cx, ax] = 0x1C;
                }
            }

            var_8.field_7[var_16] = ground_tile;

            if (var_8.field_10[var_16] != 0)
            {
                int tmp_x = gbl.MapDirectionXDelta[dir] + gbl.targetX;
                int tmp_y = gbl.MapDirectionYDelta[dir] + gbl.targetY;

                gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1C;
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a poisonous cloud", gbl.player_ptr);

            ovr033.redrawCombatArea(8, 0xFF, gbl.targetY, gbl.targetX);
            seg041.GameDelay();
            ovr025.ClearPlayerTextArea();

            for (var_16 = 1; var_16 >= 9; var_16++)
            {
                if (var_11[var_16] > 0)
                {
                    ovr024.in_poison_cloud(1, gbl.player_array[var_11[var_16]]);
                }
            }
        }


        internal static void sub_61550()
        {
            Player player = gbl.player_ptr;
            int target_count = ovr025.spellMaxTargetCount(gbl.spell_id);
            int max_range = (ovr025.spellMaxTargetCount(gbl.spell_id) + 1) / 2;

            if (max_range < 1)
            {
                max_range = 1;
            }

            sub_5D7CF(max_range, 2, gbl.targetY, gbl.targetX, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

            sub_5CF7F(string.Empty, DamageType.Acid, target_count + ovr024.roll_dice_save(4, target_count), false, 0, gbl.spell_id);
        }


        internal static void sub_615F2()
        {
            Player target = gbl.sp_targets[1];

            var bkup_val = target.field_DF[4];

            if (target._class == ClassId.cleric)
            {
                target.field_DF[4] -= 1;
            }
            else if (target._class == ClassId.magic_user)
            {
                target.field_DF[4] += 4;
            }
            else
            {
                target.field_DF[4] += 2;
            }

            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.spell_id);

            if (target.HasAffect(Affects.feeblemind) == true)
            {
                ovr013.CallAffectTable(Effect.Add, null, target, Affects.feeblemind);
            }

            target.field_DF[4] = bkup_val;
        }


        internal static void sub_616CC()
        {
            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_61727()
        {
            Player player = gbl.player_ptr;

            sub_5D7CF(3, 1, gbl.targetY, gbl.targetX, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

            for (int i = 1; i <= gbl.sp_target_count; i++)
            {
                Player target = gbl.sp_targets[i];
                if (target != null)
                {
                    bool change_damage = target.field_11A != 18;

                    ovr024.damage_person(change_damage, gbl.byte_1A114, ovr024.roll_dice_save(6, 6), target);
                }
            }
        }


        internal static void cast_heal2()
        {
            if (ovr024.heal_player(0, ovr024.roll_dice(4, 2) + 2, gbl.sp_targets[1]) == true)
            {
                ovr025.sub_6818A("is Healed", true, gbl.sp_targets[1]);
            }
        }


        internal static void AffectParalizingGaze(Effect arg_0, object param, Player player) /* spell_stone */
        {
            player.actions.target = null;

            gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x41);

            if (player.actions.target != null)
            {
                gbl.spell_target = player.actions.target;

                ovr025.DisplayPlayerStatusString(false, 10, "gazes...", player);
                ovr025.load_missile_icons(0x12);

                ovr025.draw_missile_attack(0x2d, 4, ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                    ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                if (player.HasAffect(Affects.affect_7f) == true)
                {
                    Item item = gbl.spell_target.items.Find(i => i.readied && (i.field_2F == 0x76 || i.field_30 == 0x76 || i.field_31 == 0x76));

                    if (item != null)
                    {
                        ovr025.DisplayPlayerStatusString(false, 12, "reflects it!", gbl.spell_target);

                        ovr025.draw_missile_attack(0x2d, 4,
                            ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player),
                            ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target));
                        gbl.spell_target = player;
                    }
                }

                if (ovr024.do_saving_throw(0, 1, gbl.spell_target) == false)
                {
                    ovr024.sub_63014("is Stoned", Status.stoned, gbl.spell_target);
                }
            }
        }


        internal static void DragonBreathElec(Effect arg_0, object param, Player player) // cast_breath
        {
            Affect affect = (Affect)param;
            bool var_1 = false; /* Simeon */

            if (gbl.combat_round == 0 ||
                ovr024.roll_dice(100, 1) > 50)
            {
                gbl.damage_flags = DamageType.DragonBreath | DamageType.Electricity;
                int var_2 = ovr033.PlayerMapXPos(player);
                int var_3 = ovr033.PlayerMapYPos(player);

                ovr025.DisplayPlayerStatusString(true, 10, "Breathes!", player);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x33);

                gbl.targetX = var_2 + Math.Sign(gbl.targetX - var_2);
                gbl.targetY = var_3 + Math.Sign(gbl.targetY - var_3);

                if (gbl.targetX == (var_2 + 1))
                {
                    gbl.targetX++;
                }

                if (gbl.targetY == (var_3 + 1))
                {
                    gbl.targetY++;
                }

                ovr024.remove_invisibility(player);
                ovr025.load_missile_icons(0x13);

                ovr025.draw_missile_attack(0x32, 4, gbl.targetY, gbl.targetX, var_3, var_2);
                sub_5F986(ref var_1, 0, 3, player.hit_point_max, gbl.targetY, gbl.targetX);
                sub_5FA44(0, 3, player.hit_point_max, 10);

                if (affect.affect_data > 0xFD)
                {
                    affect.affect_data -= 1;
                }
                else
                {
                    ovr024.remove_affect(affect, Affects.breath_elec, player);
                }

                var_1 = true;
                ovr025.clear_actions(player);
            }
        }


        internal static void spell_spit_acid(Effect arg_0, object param, Player player)
        {
            gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x41);

            gbl.spell_target = player.actions.target;

            int roll = ovr024.roll_dice(100, 1);

            if (ovr025.getTargetRange(gbl.spell_target, player) < 7 &&
                gbl.spell_target != null)
            {
                if (roll <= 30)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);
                    ovr025.load_missile_icons(0x17);

                    ovr025.draw_missile_attack(30, 1,
                        ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    ovr024.damage_person(ovr024.do_saving_throw(0, 3, gbl.spell_target), DamageOnSave.Half, player.hit_point_max, gbl.spell_target);
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid and Misses", player);
                }
            }
        }


        internal static void DragonBreathAcid(Effect arg_0, object param, Player attacker) // spell_breathes_acid
        {
            Affect affect = (Affect)param;

            gbl.byte_1DA70 = false;

            if (gbl.combat_round == 0)
            {
                affect.affect_data = 3;
            }

            if (affect.affect_data > 0)
            {
                gbl.damage_flags = DamageType.DragonBreath | DamageType.Acid;

                int attacker_x = ovr033.PlayerMapXPos(attacker);
                int attacker_y = ovr033.PlayerMapYPos(attacker);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x3d);

                if (gbl.byte_1DA70 == true)
                {
                    sub_5D7CF(6, 1, gbl.targetY, gbl.targetX, attacker_y, attacker_x);
                }

                for (int i = 1; i <= gbl.sp_target_count; i++)
                {
                    if (attacker.OppositeTeam() == gbl.sp_targets[i].combat_team)
                    {
                        gbl.byte_1DA70 = false;
                    }
                }

                if (gbl.byte_1DA70 == true &&
                    gbl.sp_target_count > 0)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "breathes acid", attacker);
                    ovr025.load_missile_icons(0x12);

                    ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapYPos(gbl.sp_targets[1]), ovr033.PlayerMapXPos(gbl.sp_targets[1]),
                        ovr033.PlayerMapYPos(attacker), ovr033.PlayerMapXPos(attacker));

                    for (int i = 1; i <= gbl.sp_target_count; i++)
                    {
                        if (gbl.sp_targets[i] != null)
                        {
                            Player target = gbl.sp_targets[i];

                            bool save_made = ovr024.do_saving_throw(0, 3, target);
                            ovr024.damage_person(save_made, DamageOnSave.Half, attacker.hit_point_max, target);
                        }
                    }

                    affect.affect_data--;

                    ovr025.clear_actions(attacker);
                }
            }
        }


        internal static void DragonBreathFire(Effect arg_0, object param, Player attacker) // spell_breathes_fire
        {
            Affect affect = (Affect)param;

            if (gbl.combat_round == 0)
            {
                affect.affect_data = 3;
            }

            if (affect.affect_data > 0)
            {
                gbl.damage_flags = DamageType.DragonBreath | DamageType.Fire;
                int attack_x = ovr033.PlayerMapXPos(attacker);
                int attack_y = ovr033.PlayerMapYPos(attacker);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x3D);

                if (gbl.byte_1DA70 == true)
                {
                    sub_5D7CF(9, 3, gbl.targetY, gbl.targetX, attack_y, attack_x);

                    if (gbl.sp_target_count > 0)
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "breathes fire", attacker);
                        ovr025.load_missile_icons(0x12);

                        ovr025.draw_missile_attack(0x1E, 1,
                            ovr033.PlayerMapYPos(gbl.sp_targets[1]), ovr033.PlayerMapXPos(gbl.sp_targets[1]),
                            ovr033.PlayerMapYPos(attacker), ovr033.PlayerMapXPos(attacker));

                        for (int var_4 = 1; var_4 <= gbl.sp_target_count; var_4++)
                        {
                            if (gbl.sp_targets[var_4] != null)
                            {
                                Player target = gbl.sp_targets[var_4];
                                bool saves = ovr024.do_saving_throw(0, 3, target);

                                ovr024.damage_person(saves, DamageOnSave.Half, attacker.hit_point_max, target);
                            }
                        }
                        affect.affect_data -= 1;
                        ovr025.clear_actions(attacker);
                    }
                }
            }
        }


        internal static void cast_breath_fire(Effect arg_0, object param, Player arg_6)
        {
            gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x41);
            gbl.spell_target = arg_6.actions.target;

            if ((gbl.spell_target != null) &&
                (ovr024.roll_dice(100, 1) <= 50) &&
                ovr025.getTargetRange(gbl.spell_target, arg_6) < 2)
            {
                gbl.damage_flags = DamageType.Fire;
                gbl.byte_1DA70 = true;
                ovr025.clear_actions(arg_6);

                ovr025.DisplayPlayerStatusString(true, 10, "Breathes Fire", arg_6);
                ovr025.load_missile_icons(0x17);

                ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                    ovr033.PlayerMapYPos(arg_6), ovr033.PlayerMapXPos(arg_6));

                ovr024.damage_person(ovr024.do_saving_throw(0, 3, gbl.spell_target), DamageOnSave.Half, 7, gbl.spell_target);
            }
        }


        internal static void cast_throw_lightening(Effect arg_0, object param, Player caster) /* cast_throw_lightning */
        {
            bool var_1 = false; /* Simeon */

            if (gbl.combat_round < 4)
            {
                int pos_x = ovr033.PlayerMapXPos(caster);
                int pos_y = ovr033.PlayerMapYPos(caster);

                ovr025.DisplayPlayerStatusString(true, 10, "throws lightning", caster);
                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x33);

                ovr024.remove_invisibility(caster);
                ovr025.load_missile_icons(0x13);
                ovr025.draw_missile_attack(0x32, 4, gbl.targetY, gbl.targetX, pos_y, pos_x);

                sub_5F986(ref var_1, 0, 4, ovr024.roll_dice_save(6, 16), gbl.targetY, gbl.targetX);
                sub_5FA44(0, 0, ovr024.roll_dice_save(6, 16), 10);
                var_1 = true;
                ovr025.clear_actions(caster);
            }
        }


        internal static void cast_gaze_paralyze(Effect arg_0, object param, Player arg_6)
        {
            arg_6.actions.target = null;

            gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x24);

            gbl.spell_target = arg_6.actions.target;

            if (gbl.spell_target != null)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "gazes...", arg_6);

                ovr025.load_missile_icons(0x12);

                ovr025.draw_missile_attack(0x2d, 4, ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                    ovr033.PlayerMapYPos(arg_6), ovr033.PlayerMapXPos(arg_6));

                if (ovr024.do_saving_throw(0, 1, gbl.spell_target) == false)
                {
                    ovr024.add_affect(false, 0xff, 0x3c, Affects.paralyze, gbl.spell_target);
                    ovr025.DisplayPlayerStatusString(false, 10, "is paralyzed", gbl.spell_target);
                }
            }
        }


        internal static bool item_is_scroll(Item item)
        {
            bool var_1;

            if (item != null &&
                gbl.unk_1C020[item.type].item_slot > 10 &&
                gbl.unk_1C020[item.type].item_slot < 14)
            {
                var_1 = true;
            }
            else
            {
                var_1 = false;
            }

            return var_1;
        }


        internal static void remove_spell_from_scroll(byte affect, Item item, Player player) /* sub_623FF */
        {
            int affect_index = 0;

            for (int index = 1; index <= 3; index++)
            {
                if (((int)item.getAffect(index) & 0x7F) == affect)
                {
                    affect_index = index;
                }
            }

            if (affect_index != 0)
            {
                item.setAffect(affect_index, 0);
                item.field_30 -= 1;
                if (item.field_30 < -46)
                {
                    ovr025.lose_item(item, player);
                }
            }
        }


        internal static void cast_spell_text(byte arg_0, string arg_2, Player arg_6) /* cast_a_spell */
        {
            if (gbl.game_state == GameState.Combat)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "Casts a Spell", arg_6);
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);

                seg041.displayString("Spell:" + SpellNames[arg_0], 0, 10, 0x17, 0);
            }
            else
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 0x12, 1);

                ovr025.displayPlayerName(false, 0x13, 1, arg_6);

                seg041.displayString(arg_2, 0, 10, 0x13, arg_6.name.Length + 2);
                seg041.displayString(SpellNames[arg_0], 0, 10, 0x14, 1);
                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
            }
        }


        internal static void setup_spells()
        {
            gbl.cureSpell = false;
            gbl.spell_from_item = false;
            gbl.dword_1D87F = null;
            gbl.byte_1D2C8 = true;

            gbl.dword_1D5CA = new spellDelegate(ovr023.cast_spell_on);

            gbl.spells_func_table[0x00] = null;
            gbl.spells_func_table[0x01] = ovr023.cleric_bless;
            gbl.spells_func_table[0x02] = ovr023.cleric_curse;
            gbl.spells_func_table[0x03] = ovr023.cleric_cure_light;
            gbl.spells_func_table[0x04] = ovr023.cleric_cause_light;
            gbl.spells_func_table[0x05] = ovr023.is_affected;
            gbl.spells_func_table[0x06] = ovr023.is_protected;
            gbl.spells_func_table[0x07] = ovr023.is_protected;
            gbl.spells_func_table[0x08] = ovr023.cleric_resist_cold;
            gbl.spells_func_table[0x09] = ovr023.sub_5DEE1;
            gbl.spells_func_table[0x0a] = ovr023.is_charmed;
            gbl.spells_func_table[0x0b] = ovr023.is_affected;
            gbl.spells_func_table[0x0c] = ovr023.is_stronger;
            gbl.spells_func_table[0x0d] = ovr023.enlarge_end;
            gbl.spells_func_table[0x0e] = ovr023.is_friendly;
            gbl.spells_func_table[0x0f] = ovr023.sub_5E221;
            gbl.spells_func_table[0x10] = ovr023.is_protected;
            gbl.spells_func_table[0x11] = ovr023.is_protected;
            gbl.spells_func_table[0x12] = ovr023.is_affected;
            gbl.spells_func_table[0x13] = ovr023.is_shielded;
            gbl.spells_func_table[0x14] = ovr023.sub_5E2B2;
            gbl.spells_func_table[0x15] = ovr023.falls_asleep;
            gbl.spells_func_table[0x16] = ovr023.is_affected;
            gbl.spells_func_table[0x17] = ovr023.is_held;
            gbl.spells_func_table[0x18] = ovr023.is_fire_resistant;
            gbl.spells_func_table[0x19] = ovr023.is_silenced;
            gbl.spells_func_table[0x1a] = ovr023.is_affected2;
            gbl.spells_func_table[0x1b] = ovr023.is_charmed2;
            gbl.spells_func_table[0x1c] = ovr023.sub_5E681;
            gbl.spells_func_table[0x1d] = ovr023.is_affected;
            gbl.spells_func_table[0x1e] = ovr023.is_invisible;
            gbl.spells_func_table[0x1f] = ovr023.knock_Knock;
            gbl.spells_func_table[0x20] = ovr023.is_duplicated;
            gbl.spells_func_table[0x21] = ovr023.is_weakened;
            gbl.spells_func_table[0x22] = ovr023.create_noxious_cloud;
            gbl.spells_func_table[0x23] = ovr023.sub_5EC5B;
            gbl.spells_func_table[0x24] = ovr023.is_animated;
            gbl.spells_func_table[0x25] = ovr023.can_see;
            gbl.spells_func_table[0x26] = ovr023.is_blind;
            gbl.spells_func_table[0x27] = ovr023.sub_5F0DC;
            gbl.spells_func_table[0x28] = ovr023.is_diseased;
            gbl.spells_func_table[0x29] = ovr023.is_affected3;
            gbl.spells_func_table[0x2a] = ovr023.is_praying;
            gbl.spells_func_table[0x2b] = ovr023.uncurse;
            gbl.spells_func_table[0x2c] = ovr023.curse;
            gbl.spells_func_table[0x2d] = ovr023.spell_blinking;
            gbl.spells_func_table[0x2e] = ovr023.is_affected3;
            gbl.spells_func_table[0x2f] = ovr023.sub_5F782;
            gbl.spells_func_table[0x30] = ovr023.cast_haste;
            gbl.spells_func_table[0x31] = ovr023.is_held;
            gbl.spells_func_table[0x32] = ovr023.is_invisible;
            gbl.spells_func_table[0x33] = ovr023.sub_5FCD9;
            gbl.spells_func_table[0x34] = ovr023.is_protected;
            gbl.spells_func_table[0x35] = ovr023.is_protected;
            gbl.spells_func_table[0x36] = ovr023.is_protected;
            gbl.spells_func_table[0x37] = ovr023.sub_5FD2E;
            gbl.spells_func_table[0x38] = ovr023.cast_restore;
            gbl.spells_func_table[0x39] = ovr023.cast_speed;
            gbl.spells_func_table[0x3a] = ovr023.sub_5FF6D;
            gbl.spells_func_table[0x3b] = ovr023.cast_strength;
            gbl.spells_func_table[0x3c] = ovr023.sub_6003C;
            gbl.spells_func_table[0x3d] = ovr023.cast_paralyzed;
            gbl.spells_func_table[0x3e] = ovr023.cast_heal;
            gbl.spells_func_table[0x3f] = ovr023.cast_invisible;
            gbl.spells_func_table[0x40] = ovr023.sub_5F782;
            gbl.spells_func_table[0x41] = ovr023.dam2d4plus2;
            gbl.spells_func_table[0x42] = ovr023.dam2d8plus1;
            gbl.spells_func_table[0x43] = ovr023.cure_poison;
            gbl.spells_func_table[0x44] = ovr023.sub_602D0;
            gbl.spells_func_table[0x45] = ovr023.is_protected;
            gbl.spells_func_table[0x46] = ovr023.cast_flattern;
            gbl.spells_func_table[0x47] = ovr023.sub_603F0;
            gbl.spells_func_table[0x48] = ovr023.dam3d8plus3;
            gbl.spells_func_table[0x49] = ovr023.is_affected4;
            gbl.spells_func_table[0x4a] = ovr023.sub_604DA;
            gbl.spells_func_table[0x4b] = ovr023.cast_raise;
            gbl.spells_func_table[0x4c] = ovr023.cast_slay;
            gbl.spells_func_table[0x4d] = ovr023.is_affected;
            gbl.spells_func_table[0x4e] = ovr023.cast_entangle;
            gbl.spells_func_table[0x4f] = ovr023.cast_faerie_fire;
            gbl.spells_func_table[0x50] = ovr023.cast_invisible2;
            gbl.spells_func_table[0x51] = ovr023.cast_charmed;
            gbl.spells_func_table[0x52] = ovr023.cast_confuse;
            gbl.spells_func_table[0x53] = ovr023.cast_teleport;
            gbl.spells_func_table[0x54] = ovr023.cast_fear;
            gbl.spells_func_table[0x55] = ovr023.cast_protection;
            gbl.spells_func_table[0x56] = ovr023.spell_slow;
            gbl.spells_func_table[0x57] = ovr023.sub_60F0B;
            gbl.spells_func_table[0x58] = ovr023.sub_60F4E;
            gbl.spells_func_table[0x59] = ovr023.uncurse;
            gbl.spells_func_table[0x5a] = ovr023.is_animated;
            gbl.spells_func_table[0x5b] = ovr023.spell_poisonous_cloud;
            gbl.spells_func_table[0x5c] = ovr023.sub_61550;
            gbl.spells_func_table[0x5d] = ovr023.sub_615F2;
            gbl.spells_func_table[0x5e] = ovr023.is_held;
            gbl.spells_func_table[0x5f] = ovr023.sub_616CC;
            gbl.spells_func_table[0x60] = ovr023.sub_616CC;
            gbl.spells_func_table[0x61] = ovr023.sub_616CC;
            gbl.spells_func_table[0x62] = ovr023.sub_61727;
            gbl.spells_func_table[0x63] = ovr023.cast_heal2;
            gbl.spells_func_table[0x64] = ovr023.curse;
        }
    }
}
