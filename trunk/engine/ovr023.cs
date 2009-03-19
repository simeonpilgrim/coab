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
                case SpellClass.Cleric:
                    if (player.wis > 8 &&
                        ((player.cleric_lvl > 0) ||
                         (ovr026.sub_6B3D1(player) != 0 && player.turn_undead > 0) ||
                         (player.paladin_lvl > 8) ||
                         (player.field_114 > 8 && ovr026.sub_6B3D1(player) != 0)))
                    {
                        can_learn = true;
                    }
                    break;

                case SpellClass.Druid:
                    if ((player.wis > 8 && player.ranger_lvl > 6) ||
                        (ovr026.sub_6B3D1(player) != 0 && player.field_115 > 6))
                    {
                        can_learn = true;
                    }
                    break;

                case SpellClass.MagicUser:
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

                case SpellClass.Monster:
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
                    gbl.currentScroll = gbl.scribeScrolls[selected_index];
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


        internal static void scroll_5C912(bool learning) /* sub_5C912 */
        {
            if (gbl.player_ptr.HasAffect(Affects.read_magic) == true ||
                ((gbl.player_ptr.cleric_lvl > 0 || gbl.player_ptr.turn_undead > gbl.player_ptr.field_E6) &&
                  gbl.ItemDataTable[gbl.currentScroll.type].item_slot == 12))
            {
                gbl.currentScroll.hidden_names_flag = 0;
            }

            if (gbl.currentScroll.hidden_names_flag == 0)
            {
                for (byte var_1 = 1; var_1 <= 3; var_1++)
                {
                    if ((learning == true && (int)gbl.currentScroll.getAffect(var_1) > 0x80) ||
                        (learning == false && (int)gbl.currentScroll.getAffect(var_1) > 0))
                    {
                        add_spell_to_list((byte)gbl.currentScroll.getAffect(var_1));
                        gbl.scribeScrolls[gbl.scribeScrollsCount] = gbl.currentScroll;
                        gbl.scribeScrollsCount++;
                    }
                }
            }
        }


        internal static void BuildScrollSpellLists(bool showLearning) // sub_5C9F4
        {
            for (int var_1 = 0; var_1 < 0x30; var_1++)
            {
                gbl.scribeScrolls[var_1] = null;
            }

            gbl.scribeScrollsCount = 0;

            foreach (var item in gbl.player_ptr.items)
            {
                gbl.currentScroll = item;

                if (item.IsScroll())
                {
                    scroll_5C912(showLearning);
                }
            }
        }


        internal static bool sub_5CA74(SpellLoc spl_location)
        {
            bool buildSpellList = true;

            gbl.spell_string_list.Clear();

            for (int var_2 = 0; var_2 < gbl.max_spells; var_2++)
            {
                gbl.memorize_spell_id[var_2] = 0;
            }

            switch (spl_location)
            {
                case SpellLoc.memory:
                    for (int idx = 0; idx < gbl.max_spells; idx++)
                    {
                        if (gbl.player_ptr.spell_list[idx] > 0 &&
                            can_learn_spell(gbl.player_ptr.spell_list[idx], gbl.player_ptr) == true &&
                            gbl.player_ptr.spell_list[idx] < 0x80)
                        {
                            add_spell_to_learning_list(gbl.player_ptr.spell_list[idx]);
                        }
                    }
                    break;

                case SpellLoc.memorize:
                    for (int idx = 0; idx < gbl.max_spells; idx++)
                    {
                        if (gbl.player_ptr.spell_list[idx] > 0x7F &&
                            can_learn_spell(gbl.player_ptr.spell_list[idx], gbl.player_ptr) == true)
                        {
                            add_spell_to_learning_list(gbl.player_ptr.spell_list[idx]);
                        }
                    }
                    break;

                case SpellLoc.grimoire:
                    foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                    {
                        if (gbl.player_ptr.KnowsSpell(spell) && 
                            can_learn_spell((int)spell, gbl.player_ptr))
                        {
                            add_spell_to_learning_list((int)spell);
                        }
                    }
                    break;

                case SpellLoc.scroll:
                    scroll_5C912(false);
                    buildSpellList = false;
                    break;

                case SpellLoc.scrolls:
                    BuildScrollSpellLists(false);
                    buildSpellList = false;
                    break;

                case SpellLoc.scribe:
                    BuildScrollSpellLists(true);
                    buildSpellList = false;
                    break;

                case SpellLoc.choose:
                    foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                    {
                        int sp_lvl = gbl.spell_table[(int)spell].spellLevel;
                        SpellClass sp_class = gbl.spell_table[(int)spell].spellClass;

                        if (sp_lvl >= 5 || sp_class >= SpellClass.Monster)
                        {
                            //skip this spell
                        }
                        else if (gbl.player_ptr.field_12D[(int)sp_class, sp_lvl] > 0 &&
                            can_learn_spell((int)spell, gbl.player_ptr) == true &&
                            gbl.player_ptr.KnowsSpell(spell))
                        {
                            add_spell_to_learning_list((int)spell);
                        }
                    }
                    break;
            }

            if (gbl.spell_string_list.Count > 0)
            {
                if (buildSpellList == true)
                {
                    int idx = 0;
                    int spellLvl = 0; 

                    int insert = 0;
                    var inserts = new Queue<KeyValuePair<int, int>>();

                    foreach (var mi in gbl.spell_string_list)
                    {
                        var lastLvl = spellLvl;

                        if (gbl.memorize_spell_id[idx] != 0)
                        {
                            spellLvl = gbl.spell_table[gbl.memorize_spell_id[idx]].spellLevel;
                        }

                        if (spellLvl > lastLvl)
                        {
                            inserts.Enqueue(new KeyValuePair<int, int>(insert, spellLvl));
                            insert++;
                        }

                        insert++;
                        idx++;
                    }

                    foreach (var vp in inserts)
                    {
                        gbl.spell_string_list.Insert(vp.Key, new MenuItem(LevelStrings[vp.Value], true));
                    }
                }
                return true;
            }

            return false;
        }


        internal static int SpellRange(int spellId) // sub_5CDE5
        {
            int castingLvl = (gbl.spell_from_item == true) ? 6 : ovr025.spellMaxTargetCount(spellId);
            int range = gbl.spell_table[spellId].fixedRange + (gbl.spell_table[spellId].perLvlRange * castingLvl);

            if (range == 0 &&
                gbl.spell_table[spellId].field_6 != 0)
            {
                range = 1;
            }

            if (range == 0xff)
            {
                range = 1;
            }

            return range;
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
                var_4 = gbl.spell_table[spellId].fixedDuration + (gbl.spell_table[spellId].perLvlDuration * ovr025.spellMaxTargetCount(spellId));
            }

            return (ushort)var_4;
        }


        internal static void sub_5CF7F(string arg_0, DamageType damageFlags, int damage, bool arg_8, int TargetCount, byte spell_id)
        {
            gbl.damage_flags = (damage == 0)? 0 : damageFlags;

            if (gbl.spellTargets.Count > 0)
            {
                int target_count = TargetCount > 0 ? TargetCount : ovr025.spellMaxTargetCount(spell_id);

                foreach (var target in gbl.spellTargets)
                {
                    bool saved;

                    if (gbl.spell_table[spell_id].damageOnSave == 0)
                    {
                        saved = false;
                    }
                    else
                    {
                        saved = ovr024.RollSavingThrow(0, gbl.spell_table[spell_id].saveVerse, target);
                    }

                    if (gbl.spell_table[gbl.spell_id].fixedRange == -1)
                    {
                        ovr025.reclac_player_values(target);

                        ovr024.CheckAffectsEffect(target, CheckType.Type_11);

                        if (ovr024.PC_CanHitTarget(target.ac, target, gbl.player_ptr) == false)
                        {
                            damage = 0;
                            saved = true;
                        }
                    }

                    if (damage > 0)
                    {
                        ovr024.damage_person(saved, gbl.spell_table[spell_id].damageOnSave, damage, target);
                    }

                    if (gbl.spell_table[spell_id].affect_id > 0)
                    {
                        ovr024.is_unaffected(arg_0, saved, gbl.spell_table[spell_id].damageOnSave,
                            arg_8, target_count, GetSpellAffectTimeout(spell_id), gbl.spell_table[spell_id].affect_id,
                            target);
                    }
                }
                    
                gbl.damage_flags = 0;
            }
        }


        internal static void cast_spell_on(out bool castSpell, QuickFight quick_fight, byte arg_6)
        {
            if (gbl.lastSelectetSpellTarget == null)
            {
                gbl.lastSelectetSpellTarget = gbl.player_ptr;
            }

            gbl.spellTargets.Clear();
            gbl.spellTargets.Add(gbl.player_ptr);

            castSpell = true;

            switch (gbl.spell_table[arg_6].field_7)
            {
                case 1:
                    break;

                case 2:
                    ovr025.load_pic();

                    ovr025.selectAPlayer(ref gbl.lastSelectetSpellTarget, true, "Cast Spell on whom");

                    gbl.spellTargets.Clear();
                    if (gbl.lastSelectetSpellTarget == null)
                    {
                        castSpell = false;
                    }
                    else
                    {
                        gbl.spellTargets.Add(gbl.lastSelectetSpellTarget);
                    }
                    break;

                case 4:
                    // prepend all players
                    gbl.spellTargets.AddRange(gbl.player_next_ptr);
                    break;

                default:
                    castSpell = false;
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
                        var casterPos = ovr033.PlayerMapPos(caster);

                        byte direction = 0;

                        while (ovr032.CanSeeCombatant(direction, gbl.targetPos, casterPos) == false)
                        {
                            direction++;
                        }

                        gbl.focusCombatAreaOnPlayer = true;
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

                        ovr025.draw_missile_attack(0x1E, 4, gbl.targetPos, casterPos);

                        if (ovr033.PlayerOnScreen(false, caster) == true)
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

                    var func = gbl.spellTable[(Spells)spell_id];
                    func();

                    gbl.spell_id = 0;
                    gbl.byte_1D2C7 = false;
                }
                else
                {
                    if (gbl.game_state != GameState.Combat)
                    {
                        var_1 = false;
                    }
                    else if (quick_fight == QuickFight.True ||
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

            ovr025.ClearPlayerTextArea();

            if (gbl.game_state == GameState.Combat)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static void localSteppingPathInit(Point target, Point caster, SteppingPath path) /* sub_5D676 */
        {
            path.attacker = caster;
            path.target = target;

            path.CalculateDeltas();
        }


        static int find_players_on_path(SteppingPath path, List<int> player_list) /* sub_5D702 */
        {
            int dir = 0;
            while (!path.Step())
            {
                int playerIndex = ovr033.PlayerIndexAtMapXY(path.current.y, path.current.x);

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

        static Point[] unk_16D22 = { new Point(-1, 0), new Point(0, -1), new Point(0, -1), new Point(1, 0), new Point(1, 0), new Point(0, 1), new Point(0, 1), new Point(-1, 0) };
        static Point[] unk_16D32 = { new Point(1, 0), new Point(1, 0), new Point(0, 1), new Point(0, 1), new Point(-1, 0), new Point(-1, 0), new Point(0, -1), new Point(0, -1) };


        static void BuildAreaDamageTargets(int max_range, int playerSize, Point targetPos, Point casterPos) // sub_5D7CF
        {
            List<int> players_on_path = new List<int>();

            bool finished;
            SteppingPath path = new SteppingPath();

            localSteppingPathInit(targetPos, casterPos, path);

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

            var tmpPos = new Point(targetPos);

            while (tmp_range < max_range && finished == false)
            {
                if (tmpPos.x < 0x31 && tmpPos.x > 0 && tmpPos.y < 0x18 && tmpPos.y > 0)
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

                    tmpPos += gbl.MapDirectionDelta[directions[index]];

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

            targetPos.MapBoundaryTrunc();

            int range = 0xff; /* Simeon */
            ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, ref targetPos, casterPos);

            localSteppingPathInit(targetPos, casterPos, path);
            int var_76 = find_players_on_path(path, players_on_path);

            if (playerSize > 1)
            {
                Point map_b = targetPos + unk_16D32[var_76];
                map_b.MapBoundaryTrunc();

                localSteppingPathInit(map_b, casterPos, path);
                find_players_on_path(path, players_on_path);

                if (playerSize > 2)
                {
                    Point map_a = targetPos + unk_16D22[var_76];

                    map_a.MapBoundaryTrunc();

                    localSteppingPathInit(map_a, casterPos, path);
                    find_players_on_path(path, players_on_path);
                }
            }

            gbl.spellTargets.Clear();

            foreach(var idx in players_on_path)
            {
                var player = gbl.player_array[idx];
                if( player != gbl.player_ptr)
                {
                    gbl.spellTargets.Add(player);
                }
            }
        }


        internal static void MultiTargetedSpell(string text, int save_bonus) // sub_5DB24
        {
            bool firstTimeRound = true;
            foreach (var target in gbl.spellTargets)
            {
                if (firstTimeRound == false)
                {
                    seg044.sound_sub_120E0(Sound.sound_2);
                    ovr025.load_missile_icons(0x12);

                    ovr025.draw_missile_attack(0x1E, 4, ovr033.PlayerMapPos(target), ovr033.PlayerMapPos(gbl.player_ptr));
                }

                bool saved;
                DamageOnSave can_save_flag;

                if ((gbl.spell_id == 0x4F || gbl.spell_id == 0x51) &&
                    firstTimeRound == true)
                {
                    saved = true;
                    can_save_flag = DamageOnSave.Zero;
                }
                else
                {
                    saved = ovr024.RollSavingThrow(save_bonus, gbl.spell_table[gbl.spell_id].saveVerse, target);
                    can_save_flag = gbl.spell_table[gbl.spell_id].damageOnSave;
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


        static void CastTeamSpell(string text, CombatTeam team) // sub_5DCA0
        {
            gbl.byte_1D2C7 = true;

            gbl.spellTargets.RemoveAll(target => target.combat_team != team ||
                (gbl.spell_id == (int)Spells.spell_01 && gbl.game_state == GameState.Combat && ovr025.BuildNearTargets(1, target).Count > 0));

            sub_5CF7F(text, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cleric_bless() /* is_Blessed */
        {
            CastTeamSpell("is Blessed", gbl.player_ptr.combat_team);
        }


        internal static void cleric_curse() /* is_Cursed */
        {
            CastTeamSpell("is Cursed", gbl.player_ptr.OppositeTeam());
        }


        internal static void cleric_cure_light() /* sub_5DDBC */
        {
            if (gbl.spellTargets.Count > 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 1), gbl.spellTargets[0]) == true)
            {
                ovr025.DescribeHealing(gbl.spellTargets[0]);
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


        internal static void SpellCharm() // is_charmed
        {
            Player target = gbl.spellTargets[0];

            if (target.field_11A > 1 ||
                target.field_DE > 1)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
            else
            {
                sub_5CF7F("is charmed", 0, 0, true, (byte)(((int)gbl.player_ptr.combat_team << 7) + ovr025.spellMaxTargetCount(gbl.spell_id)), gbl.spell_id);

                Affect affect = target.GetAffect(Affects.charm_person);

                if (affect != null)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, target, Affects.shield);
                }
            }
        }


        internal static void is_stronger()
        {
            Player target = gbl.spellTargets[0];
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
            Player target = gbl.spellTargets[0];

            if (target != null &&
                gbl.spellTargets.Count > 0 &&
                ovr024.RollSavingThrow(0, SaveVerseType.type4, target) == false &&
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

            gbl.spellTargets.RemoveAll(target =>
            {
                gbl.byte_1AFDE = (byte)CalcSleepCost(target);

                if (target.health_status != Status.animated &&
                    target.HasAffect(Affects.sleep) == false &&
                    gbl.byte_1AFDD >= gbl.byte_1AFDE)
                {
                    gbl.byte_1AFDD -= gbl.byte_1AFDE;
                    return false;
                }
                else
                {
                    return true;
                }
            });

            sub_5CF7F("falls asleep", 0, 0, false, 0, gbl.spell_id);
        }

        private static int CalcSleepCost(Player target)
        {
            int cost;
            switch (target.HitDice)
            {
                case 0:
                case 1:
                    cost = 1;
                    break;

                case 2:
                    cost = 2;
                    break;

                case 3:
                    cost = 4;
                    break;

                case 4:
                    cost = 6;
                    break;

                case 5:
                    cost = (target.race == Race.monster)? 10 : 20;
                    break;

                default:
                    cost = 20;
                    break;
            }

            return cost;
        }


        internal static void is_held()
        {
            int save_bonus;

            if (gbl.spellTargets.Count == 1)
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
            else if (gbl.spellTargets.Count == 2)
            {
                save_bonus = -1;
            }
            else if (gbl.spellTargets.Count == 3 || gbl.spellTargets.Count == 4)
            {
                save_bonus = 0;
            }
            else
            {
                throw new System.NotSupportedException();
            }

            MultiTargetedSpell("is held", save_bonus);
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
            Player player = gbl.spellTargets[0];

            if (player.health_status == Status.animated)
            {
                gbl.spellTargets.Clear();
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
            gbl.spellTargets.Clear();

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.field_11A == 0x0e &&
                    gbl.byte_1AFDD >= player.hit_point_current)
                {
                    gbl.byte_1AFDD -= player.hit_point_current;
                    gbl.spellTargets.Add(player);
                }
            }

            sub_5CF7F("is charmed", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_5E681()
        {
            sub_5CF7F(string.Empty, 0, 0, true, 0, gbl.spell_id);

            ovr013.CallAffectTable(Effect.Add, null, gbl.spellTargets[0], Affects.spiritual_hammer);
        }


        internal static void is_invisible()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void SpellKnock()
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
            int groundTile;
            byte var_D;
            int[] var_C = new int[4];

            gbl.byte_1D2C7 = true;

            byte var_10 = (byte)ovr025.spellMaxTargetCount(gbl.spell_id);
            int count = gbl.NoxiousCloud.FindAll(cell => cell.player == gbl.player_ptr).Count;

            GasCloud var_8 = new GasCloud(gbl.player_ptr, count, gbl.targetPos);
            gbl.NoxiousCloud.Add(var_8);

            ovr024.add_affect(true, (byte)(var_10 + (count << 4)), var_10, Affects.affect_28, gbl.player_ptr);

            for (int var_11 = 1; var_11 <= 4; var_11++)
            {
                var_12 = gbl.unk_18AE9[var_11];

                ovr033.AtMapXY(out groundTile, out var_C[var_11 - 1], gbl.targetPos + gbl.MapDirectionDelta[var_12]);

                if (groundTile > 0 && gbl.BackGroundTiles[groundTile].move_cost < 0xFF)
                {
                    var_8.present[var_11] = true;
                }
                else
                {
                    var_8.present[var_11] = false;
                }


                if (groundTile == 0x1E)
                {
                    foreach (var var_4 in gbl.NoxiousCloud)
                    {
                        if (var_4 != var_8)
                        {
                            for (var_D = 1; var_D <= 4; var_D++)
                            {
                                if (var_4.present[var_D] == true &&
                                    gbl.targetPos + gbl.MapDirectionDelta[var_12] == var_4.targetPos + gbl.MapDirectionDelta[gbl.unk_18AE9[var_D]] &&
                                    var_4.groundTile[var_D] != 0x1E)
                                {
                                    groundTile = var_4.groundTile[var_D];
                                }
                            }
                        }
                    }
                }
                else if (groundTile == 0x1F)
                {
                    var c = gbl.downedPlayers.FindLast(cell => cell.map == gbl.targetPos + gbl.MapDirectionDelta[var_12]);
                    if (c != null)
                    {
                        groundTile = c.originalBackgroundTile;
                    }
                }

                var_8.groundTile[var_11] = groundTile;
                if (var_8.present[var_11] == true)
                {
                    var pos = gbl.MapDirectionDelta[var_12] + gbl.targetPos;

                    gbl.mapToBackGroundTile[pos] = 0x1E;
                }
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a noxious cloud", gbl.player_ptr);

            ovr033.redrawCombatArea(8, 0xff, gbl.targetPos);
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
            Player target = gbl.spellTargets[0];

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

            gbl.spellTargets.Clear();

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.health_status == Status.dead &&
                    player.field_11A == 0)
                {
                    if (ovr033.sub_7515A(true, ovr033.PlayerMapPos(player), player) == true)
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
            if (ovr024.cure_affect(Affects.blinded, gbl.spellTargets[0]) == true)
            {
                ovr025.MagicAttackDisplay("can see", true, gbl.spellTargets[0]);
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

            if (ovr024.cure_affect(Affects.cause_disease_1, gbl.spellTargets[0]) == true)
            {
                var_1 = true;
            }

            if (ovr024.cure_affect(Affects.affect_2b, gbl.spellTargets[0]) == true)
            {
                var_1 = true;

                ovr024.remove_affect(null, Affects.cause_disease_2, gbl.spellTargets[0]);
                ovr024.remove_affect(null, Affects.helpless, gbl.spellTargets[0]);
            }

            if (ovr024.cure_affect(Affects.hot_fire_shield, gbl.spellTargets[0]) == true)
            {
                var_1 = true;
                ovr024.remove_affect(null, Affects.affect_39, gbl.spellTargets[0]);
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

            if( gbl.spellTargets.Count > 0 )
            {
                bool is_affected = false;
                Player target = gbl.spellTargets[0];

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
                    ovr025.MagicAttackDisplay("is affected", true, target);
                }
            }

            int yPos = 0;
            int xPos = 0;

            for (int dir = 0; dir <= 8; dir++)
            {
                switch (dir)
                {
                    case 0:
                        xPos = gbl.targetPos.x;
                        yPos = gbl.targetPos.y;
                        break;

                    case 1:
                        yPos = gbl.targetPos.y - 1;
                        break;

                    case 2:
                        xPos = gbl.targetPos.x - 1;
                        break;

                    case 3:
                        yPos = gbl.targetPos.y;
                        break;

                    case 4:
                        yPos = gbl.targetPos.y + 1;
                        break;

                    case 5:
                        xPos = gbl.targetPos.x;
                        break;

                    case 6:
                        xPos = gbl.targetPos.x - 1;
                        break;

                    case 7:
                        yPos = gbl.targetPos.y;
                        break;

                    case 8:
                        yPos = gbl.targetPos.y - 1;
                        break;
                }

                int dummy_byte;
                int ground_tile;
                ovr033.AtMapXY(out ground_tile, out dummy_byte, yPos, xPos);

                if (ground_tile == 0x1C || ground_tile == 0x1E)
                {
                    // Struct_1D885 var_18;
                    int var_14 = (ground_tile == 0x1C) ? 9 : 4;
                    var looplist = (ground_tile == 0x1C) ? gbl.PoisonousCloud : gbl.NoxiousCloud;

                    var mappos = new Point(xPos, yPos);
                    looplist.ForEach(var_18 =>
                    {
                        for (int var_1 = 1; var_1 <= var_14; var_1++)
                        {
                            if (mappos == var_18.targetPos + gbl.MapDirectionDelta[gbl.unk_18AE9[var_1]] &&
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
            if (ovr024.cure_affect(Affects.bestow_curse, gbl.spellTargets[0]) == true)
            {
                ovr025.MagicAttackDisplay("is un-cursed", true, gbl.spellTargets[0]);
            }
            else
            {
                Item item = gbl.spellTargets[0].items.Find(i => i.cursed);

                if (item != null)
                {
                    item.readied = false;

                    if ((int)item.affect_3 > 0x7F)
                    {
                        gbl.applyItemAffect = true;
                        ovr013.CallAffectTable(Effect.Remove, item, gbl.spellTargets[0], item.affect_3);

                        for (int var_6 = 0; var_6 <= 5; var_6++)
                        {
                            ovr024.sub_648D9(var_6, gbl.spellTargets[0]);
                        }

                    }

                    ovr025.MagicAttackDisplay("has an item un-cursed", true, gbl.spellTargets[0]);
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

            if (gbl.area_ptr.inDungeon == 0)
            {
                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, 2, gbl.targetPos);

                gbl.spellTargets.Clear();
                for (int i = 1; i <= gbl.sortedCombatantCount; i++)
                {
                    gbl.spellTargets.Add(gbl.player_array[gbl.SortedCombatantList[i].player_index]);
                }
            }

            ovr033.redrawCombatArea(8, 0, gbl.targetPos);

            sub_5CF7F(string.Empty, DamageType.Magic | DamageType.Fire, ovr024.roll_dice_save(6, dice_count), false, 0, gbl.spell_id);
        }


        internal static void RemoveComplimentSpellFirst(string text, CombatTeam combatTeam, Affects affect) //sub_5F87B
        {
            gbl.byte_1D2C7 = true;

            int maxTargets = ovr025.spellMaxTargetCount(gbl.spell_id);

            gbl.spellTargets.RemoveAll(target =>
                {
                    if (target.combat_team == combatTeam && maxTargets > 0)
                    {
                        maxTargets -= 1;

                        if (ovr024.cure_affect(affect, target) == true)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    return false;
                });

            sub_5CF7F(text, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cast_haste()
        {
            RemoveComplimentSpellFirst("is Hasted", gbl.player_ptr.combat_team, Affects.slow);
        }


        static void DoElecDamage(int player_index, SaveVerseType bonusType, int damage, Point pos)
        {
            int playerIndex = ovr033.PlayerIndexAtMapXY(pos.y, pos.x);

            DoActualElecDamage(player_index, bonusType, damage, playerIndex);
        }

        private static void DoActualElecDamage(int player_index, SaveVerseType bonusType, int damage, int playerIndex)
        {
            if (playerIndex > 0 &&
                playerIndex != player_index)
            {
                Player player = gbl.player_array[playerIndex];
                gbl.damage_flags = DamageType.Magic | DamageType.Electricity;

                ovr024.damage_person(ovr024.RollSavingThrow(0, bonusType, player), DamageOnSave.Half, damage, player);
                ovr025.load_missile_icons(0x13);
                gbl.damage_flags = 0;
            }
        }


        static bool DoElecDamage(bool arg_0, int player_index, SaveVerseType bonusType, int damage, Point pos)// sub_5F986
        {
            int groundTile;
            int playerIndex;

            ovr033.AtMapXY(out groundTile, out playerIndex, pos);

            if (groundTile > 0 &&
                gbl.BackGroundTiles[groundTile].move_cost == 0xff &&
                gbl.area_ptr.inDungeon == 1 &&
                arg_0 == false)
            {
                arg_0 = true;
            }
            else
            {
                arg_0 = false;
            }

            DoActualElecDamage(player_index, bonusType, damage, playerIndex);

            return arg_0;
        }


        internal static void sub_5FA44(byte arg_0, SaveVerseType bonusType, int damage, byte arg_6)
        {
            int var_3A = 0; /* Simeon */
            bool var_36 = false;
            ovr025.load_missile_icons(0x13);

            int var_39;
            int groundTile;

            ovr033.AtMapXY(out groundTile, out var_39, gbl.targetPos);
            int var_3D = 0;
            int var_35 = 1;

            var playerPos = ovr033.PlayerMapPos(gbl.player_ptr);
            byte var_38 = arg_0;

            if (playerPos != gbl.targetPos)
            {
                int var_3C = arg_6 * 2;
                gbl.byte_1D2C7 = true;

                while (var_3C > 0)
                {
                    var path_a = new SteppingPath();

                    path_a.attacker = gbl.targetPos;
                    path_a.target = gbl.targetPos + ((gbl.targetPos - playerPos) * var_35 * var_3C);

                    path_a.CalculateDeltas();

                    do
                    {
                        var tmppos = path_a.current;

                        if (path_a.attacker != path_a.target)
                        {
                            bool stepping;

                            do
                            {
                                stepping = path_a.Step();

                                ovr033.AtMapXY(out groundTile, out var_3A, path_a.current);

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

                        ovr025.draw_missile_attack(0x32, 4, path_a.current, tmppos);

                        var_36 = DoElecDamage(var_36, var_39, bonusType, damage, path_a.current);
                        var_39 = var_3A;

                        if (var_36 == true)
                        {
                            gbl.targetPos = path_a.current;

                            var path_b = new SteppingPath();

                            path_b.attacker = gbl.targetPos;
                            path_b.target = playerPos;

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
            int damage = ovr024.roll_dice(6, ovr025.spellMaxTargetCount(gbl.spell_id));

            DoElecDamage(0, SaveVerseType.type4, damage, gbl.targetPos);
            sub_5FA44(1, SaveVerseType.type4, damage, 7);
        }


        internal static void sub_5FD2E()
        {
            RemoveComplimentSpellFirst("is Slowed", gbl.player_ptr.OppositeTeam(), Affects.haste);
        }


        internal static void cast_restore()
        {
            int var_C = 30; /* simeon */

            Player player = gbl.spellTargets[0];

            if (player.field_E7 > 0)
            {
                byte var_5 = (byte)(player.field_E8 / player.field_E7);

                player.hit_point_max += var_5;
                player.hit_point_current += var_5;
                player.field_12C += var_5;
                player.field_E8 -= var_5;
                player.field_E7 -= 1;

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
            if (ovr024.cure_affect(Affects.slow, gbl.spellTargets[0]) == false)
            {
                sub_5CF7F("is Speedy", 0, 0, false, 0, gbl.spell_id);
            }
        }


        internal static void sub_5FF6D()
        {
            if (gbl.spellTargets.Count > 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 2) + 1, gbl.spellTargets[0]) == true)
            {
                ovr025.DescribeHealing(gbl.spellTargets[0]);
            }
        }


        internal static void cast_strength()
        {
            byte var_1 = 0;
            var target = gbl.spellTargets[0];

            if (ovr024.sub_64728(out var_1, 0, 0x15, target) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is stronger", target);
            }

            ovr024.add_affect(true, var_1, (ushort)((ovr024.roll_dice(4, 1) * 10) + 0x28), Affects.strenght_spell, target);
            ovr024.sub_648D9(0, target);
        }


        internal static void sub_6003C()
        {
            DoElecDamage(0, SaveVerseType.type4, ovr024.roll_dice(6, 1) + 20, gbl.targetPos);
            sub_5FA44(0, SaveVerseType.type4, 20, 3);
        }


        internal static void cast_paralyzed()
        {
            sub_5CF7F("is paralyzed", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void cast_heal()
        {
            if (ovr024.heal_player(0, ovr024.roll_dice(4, 2) + 2, gbl.spellTargets[0]) == true)
            {
                ovr025.MagicAttackDisplay("is Healed", true, gbl.spellTargets[0]);
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


        internal static void SpellNeutralizePoison() // cure_poison
        {
            Player target = gbl.spellTargets[0];

            if (target.health_status == Status.animated)
            {
                gbl.spellTargets.Remove(target);
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


        internal static void SpellPoison() // sub_602D0
        {
            sub_5CF7F(string.Empty, DamageType.Magic, 0, false, 0, gbl.spell_id);

            Player target = gbl.player_ptr.actions.target;

            gbl.current_affect = Affects.poison_plus_0;
            ovr024.CheckAffectsEffect(target, CheckType.Type_9);

            if (gbl.current_affect == Affects.poison_plus_0)
            {
                ovr013.CallAffectTable(Effect.Add, null, gbl.player_ptr, Affects.poison_plus_0);
            }
        }


        internal static void cast_flattern()
        {
            if (gbl.spellTargets[0].HitDice < 6)
            {
                sub_5CF7F(string.Empty, DamageType.Magic, 0, false, ovr025.spellMaxTargetCount(gbl.spell_id), gbl.spell_id);

                Affect affect;
                if (ovr025.FindAffect(out affect, Affects.sticks_to_snakes, gbl.spellTargets[0]) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, gbl.spellTargets[0], Affects.sticks_to_snakes);
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "smashes them flat", gbl.spellTargets[0]);
            }
        }


        internal static void sub_603F0()
        {
            if (gbl.spellTargets.Count > 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 3) + 3, gbl.spellTargets[0]) == true)
            {
                ovr025.DescribeHealing(gbl.spellTargets[0]);
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


        internal static void SpellRaiseDead() // cast_raise
        {
            Player player = gbl.spellTargets[0];

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


        internal static void SpellSlayLiving() //cast_slay
        {
            Player target = gbl.spellTargets[0];
            gbl.damage_flags = DamageType.Unknown40;
            gbl.damage = 67;
            ovr024.CheckAffectsEffect(target, CheckType.Type_9);

            if (gbl.damage != 0)
            {
                if (ovr024.RollSavingThrow(0, SaveVerseType.type4, target) == false)
                {
                    ovr024.KillPlayer("is slain", Status.dead, target);
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


        internal static void SpellEntangle() // cast_entangle
        {
            if (gbl.area_ptr.inDungeon == 0)
            {
                foreach (var target in gbl.spellTargets)
                {
                    bool saved = ovr024.RollSavingThrow(0, SaveVerseType.type4, target);

                    ovr024.is_unaffected("is entangled", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout(0x88), Affects.entangle, target);
                }
            }
        }


        internal static void SpellFaerieFire() /* cast_highlisht */
        {
            MultiTargetedSpell("is highlighted", 0);
        }


        internal static void SpellInvisToAnimals() // cast_invisible2
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.spell_id);
        }


        internal static void SpellCharmPerson() // cast_charmed
        {
            MultiTargetedSpell("is charmed", 0);

            foreach(var target in gbl.spellTargets)
            {
                Affect affect;

                if (ovr025.FindAffect(out affect, Affects.charm_person, target) == true)
                {
                    ovr013.CallAffectTable(Effect.Add, affect, target, Affects.charm_person);
                }
            }
        }


        internal static void cast_confuse()
        {
            int target_count = ovr024.roll_dice(8, 2);

            if (gbl.spellTargets.Count > target_count)
            {
                gbl.spellTargets.RemoveRange(target_count, gbl.spellTargets.Count - target_count);
            }

            foreach (var target in gbl.spellTargets)
            {
                bool saved = ovr024.RollSavingThrow(0, SaveVerseType.type6, target);

                ovr024.is_unaffected("is confused", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout(0x52), Affects.cause_disease_2, target);
            }
        }


        internal static void cast_teleport()
        {
            Affect affect;
            Player player = gbl.player_ptr;

            if (ovr025.FindAffect(out affect, Affects.affect_3a, player) == true)
            {
                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, 1, ovr033.PlayerMapPos(player));

                for (int i = 0; i < gbl.sortedCombatantCount; i++)
                {
                    Player playerB = gbl.player_array[gbl.SortedCombatantList[i].player_index];

                    if (ovr025.FindAffect(out affect, Affects.affect_90, playerB) == true ||
                        ovr025.FindAffect(out affect, Affects.affect_8b, playerB) == true)
                    {
                        if (gbl.player_array[affect.affect_data] == player)
                        {
                            ovr024.remove_affect(null, Affects.affect_90, playerB);
                            ovr024.remove_affect(null, Affects.affect_8b, playerB);
                        }
                    }
                }
            }

            ovr033.RedrawPlayerBackground(ovr033.GetPlayerIndex(player));

            ovr033.sub_7515A(false, gbl.targetPos, player);

            ovr033.redrawCombatArea(8, 0, ovr033.PlayerMapPos(player));

            ovr025.DisplayPlayerStatusString(true, 10, "teleports", player);
        }


        internal static void cast_fear() /* cast_terror */
        {
            Player caster = gbl.player_ptr;

            BuildAreaDamageTargets(6, 3, gbl.targetPos, ovr033.PlayerMapPos(caster));

            foreach(var target in gbl.spellTargets)
            {
                bool saves = ovr024.RollSavingThrow(0, SaveVerseType.type4, target);

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
            Player target = gbl.spellTargets[0];
            gbl.damage_flags = DamageType.Unknown40;

            if (ovr024.RollSavingThrow(0, SaveVerseType.type4, target) == false)
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
            int ground_tile = 0;
            int[] var_11 = new int[10];

            gbl.byte_1D2C7 = true;

            byte var_15 = (byte)ovr025.spellMaxTargetCount(gbl.spell_id);
            int count = gbl.PoisonousCloud.FindAll(cell => cell.player == gbl.player_ptr).Count;

            GasCloud var_8 = new GasCloud(gbl.player_ptr, count, gbl.targetPos);
            gbl.PoisonousCloud.Add(var_8);

            ovr024.add_affect(true, (byte)(var_15 + (count << 4)), var_15, Affects.affect_5b, gbl.player_ptr);

            for (var_16 = 1; var_16 <= 9; var_16++)
            {
                dir = gbl.unk_18AED[var_16];

                ovr033.AtMapXY(out ground_tile, out var_11[var_16], gbl.targetPos + gbl.MapDirectionDelta[dir]);

                if (ground_tile > 0 &&
                    gbl.BackGroundTiles[ground_tile].move_cost < 0xff)
                {
                    var_8.present[var_16] = true;
                }
                else
                {
                    var_8.present[var_16] = false;
                }

                if (ground_tile == 0x1E)
                {
                    bool found = false;
                    foreach (var var_4 in gbl.NoxiousCloud)
                    {
                        for (int var_12 = 1; var_12 <= 4; var_12++)
                        {
                            if (var_4.present[var_12] == true &&
                                (gbl.MapDirectionDelta[gbl.unk_18AE9[var_12]] + var_4.targetPos) == (gbl.MapDirectionDelta[dir] + gbl.targetPos) &&
                                var_4.groundTile[var_12] != 0x1E &&
                                var_4.groundTile[var_12] != 0x1C)
                            {
                                ground_tile = var_4.groundTile[var_12];
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
                                if (var_4.present[var_12] == true &&
                                    (gbl.MapDirectionDelta[gbl.unk_18AED[var_12]] + var_4.targetPos) == (gbl.MapDirectionDelta[dir] + gbl.targetPos) &&
                                    var_4.groundTile[var_12] != 0x1E &&
                                    var_4.groundTile[var_12] != 0x1C)
                                {
                                    ground_tile = var_4.groundTile[var_12];
                                    found = true;
                                }
                            }
                        }

                        if (found) break;
                    }
                }
                else if (ground_tile == 0x1F)
                {
                    var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

                    var c = gbl.downedPlayers.FindLast(cell => cell.map == pos);
                    if (c != null)
                    {
                        ground_tile = c.originalBackgroundTile;
                    }
                }

                var_8.groundTile[var_16] = ground_tile;

                if (var_8.present[var_16] == true)
                {
                    var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

                    gbl.mapToBackGroundTile[pos] = 0x1C;
                }
            }

            var_8.groundTile[var_16] = ground_tile;

            if (var_8.present[var_16] == true)
            {
                var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

                gbl.mapToBackGroundTile[pos] = 0x1C;
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a poisonous cloud", gbl.player_ptr);

            ovr033.redrawCombatArea(8, 0xFF, gbl.targetPos);
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

            BuildAreaDamageTargets(max_range, 2, gbl.targetPos, ovr033.PlayerMapPos(player));

            sub_5CF7F(string.Empty, DamageType.Acid, target_count + ovr024.roll_dice_save(4, target_count), false, 0, gbl.spell_id);
        }


        internal static void SpellFeeblemind() // sub_615F2
        {
            Player target = gbl.spellTargets[0];

            var oldBonus = target.saveVerse[4];

            if (target._class == ClassId.cleric)
            {
                target.saveVerse[4] -= 1;
            }
            else if (target._class == ClassId.magic_user)
            {
                target.saveVerse[4] += 4;
            }
            else
            {
                target.saveVerse[4] += 2;
            }

            gbl.damage_flags = 0;

            if (target.HasAffect(Affects.feeblemind) == true)
            {
                ovr013.CallAffectTable(Effect.Add, null, target, Affects.feeblemind);
            }

            //Todo Feeblemind does not seam to have a lasting effect.
            target.saveVerse[4] = oldBonus;
        }


        internal static void sub_616CC()
        {
            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.spell_id);
        }


        internal static void sub_61727()
        {
            Player attacker = gbl.player_ptr;

            BuildAreaDamageTargets(3, 1, gbl.targetPos, ovr033.PlayerMapPos(attacker));

            foreach (var target in gbl.spellTargets)
            {
                bool change_damage = target.field_11A != 18;

                ovr024.damage_person(change_damage, gbl.spell_table[(int)Spells.spell_62].damageOnSave, ovr024.roll_dice_save(6, 6), target);
            }
        }


        internal static void cast_heal2()
        {
            if (ovr024.heal_player(0, ovr024.roll_dice(4, 2) + 2, gbl.spellTargets[0]) == true)
            {
                ovr025.MagicAttackDisplay("is Healed", true, gbl.spellTargets[0]);
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

                ovr025.draw_missile_attack(0x2d, 4, ovr033.PlayerMapPos(gbl.spell_target), ovr033.PlayerMapPos(player));

                if (player.HasAffect(Affects.affect_7f) == true)
                {
                    Item item = gbl.spell_target.items.Find(i => i.readied && (i.field_2F == 0x76 || i.field_30 == 0x76 || i.field_31 == 0x76));

                    if (item != null)
                    {
                        ovr025.DisplayPlayerStatusString(false, 12, "reflects it!", gbl.spell_target);

                        ovr025.draw_missile_attack(0x2d, 4, ovr033.PlayerMapPos(player), ovr033.PlayerMapPos(gbl.spell_target));
                        gbl.spell_target = player;
                    }
                }

                if (ovr024.RollSavingThrow(0, SaveVerseType.type1, gbl.spell_target) == false)
                {
                    ovr024.KillPlayer("is Stoned", Status.stoned, gbl.spell_target);
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
                var var_2 = ovr033.PlayerMapPos(player);

                ovr025.DisplayPlayerStatusString(true, 10, "Breathes!", player);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x33);

                gbl.targetPos.x = var_2.x + Math.Sign(gbl.targetPos.x - var_2.x);
                gbl.targetPos.y = var_2.y + Math.Sign(gbl.targetPos.y - var_2.y);

                if (gbl.targetPos.x == (var_2.x + 1))
                {
                    gbl.targetPos.x++;
                }

                if (gbl.targetPos.y == (var_2.y + 1))
                {
                    gbl.targetPos.y++;
                }

                ovr024.remove_invisibility(player);
                ovr025.load_missile_icons(0x13);

                ovr025.draw_missile_attack(0x32, 4, gbl.targetPos, var_2);
                var_1 = DoElecDamage(var_1, 0, SaveVerseType.type3, player.hit_point_max, gbl.targetPos);
                sub_5FA44(0, SaveVerseType.type3, player.hit_point_max, 10);

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

                    ovr025.draw_missile_attack(30, 1, ovr033.PlayerMapPos(gbl.spell_target), ovr033.PlayerMapPos(player));

                    ovr024.damage_person(ovr024.RollSavingThrow(0, SaveVerseType.type3, gbl.spell_target), DamageOnSave.Half, player.hit_point_max, gbl.spell_target);
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

                var attackerPos = ovr033.PlayerMapPos(attacker);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x3d);

                if (gbl.byte_1DA70 == true)
                {
                    BuildAreaDamageTargets(6, 1, gbl.targetPos, attackerPos);
                }

                if (gbl.spellTargets.Exists(target => attacker.OppositeTeam() == target.combat_team))
                {
                    gbl.byte_1DA70 = false;
                }           

                if (gbl.byte_1DA70 == true &&
                    gbl.spellTargets.Count > 0)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "breathes acid", attacker);
                    ovr025.load_missile_icons(0x12);

                    ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapPos(gbl.spellTargets[0]), ovr033.PlayerMapPos(attacker));

                    foreach (var target in gbl.spellTargets)
                    {
                        bool save_made = ovr024.RollSavingThrow(0, SaveVerseType.type3, target);
                        ovr024.damage_person(save_made, DamageOnSave.Half, attacker.hit_point_max, target);
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
                var attackPos = ovr033.PlayerMapPos(attacker);

                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x3D);

                if (gbl.byte_1DA70 == true)
                {
                    BuildAreaDamageTargets(9, 3, gbl.targetPos, attackPos);

                    if (gbl.spellTargets.Count > 0)
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "breathes fire", attacker);
                        ovr025.load_missile_icons(0x12);

                        ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapPos(gbl.spellTargets[0]), ovr033.PlayerMapPos(attacker));

                        foreach (var target in gbl.spellTargets)
                        {
                            bool saves = ovr024.RollSavingThrow(0, SaveVerseType.type3, target);

                            ovr024.damage_person(saves, DamageOnSave.Half, attacker.hit_point_max, target);
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

                ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapPos(gbl.spell_target), ovr033.PlayerMapPos(arg_6));

                ovr024.damage_person(ovr024.RollSavingThrow(0, SaveVerseType.type3, gbl.spell_target), DamageOnSave.Half, 7, gbl.spell_target);
            }
        }


        internal static void cast_throw_lightening(Effect arg_0, object param, Player caster) /* cast_throw_lightning */
        {
            bool var_1 = false; /* Simeon */

            if (gbl.combat_round < 4)
            {
                var pos = ovr033.PlayerMapPos(caster);

                ovr025.DisplayPlayerStatusString(true, 10, "throws lightning", caster);
                gbl.dword_1D5CA(out gbl.byte_1DA70, QuickFight.True, 0x33);

                ovr024.remove_invisibility(caster);
                ovr025.load_missile_icons(0x13);
                ovr025.draw_missile_attack(0x32, 4, gbl.targetPos, pos);

                var_1 = DoElecDamage(var_1, 0, SaveVerseType.type4, ovr024.roll_dice_save(6, 16), gbl.targetPos);
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

                ovr025.draw_missile_attack(0x2d, 4, ovr033.PlayerMapPos(gbl.spell_target), ovr033.PlayerMapPos(arg_6));

                if (ovr024.RollSavingThrow(0, SaveVerseType.type1, gbl.spell_target) == false)
                {
                    ovr024.add_affect(false, 0xff, 0x3c, Affects.paralyze, gbl.spell_target);
                    ovr025.DisplayPlayerStatusString(false, 10, "is paralyzed", gbl.spell_target);
                }
            }
        }


        internal static bool item_is_scroll(Item item)
        {
            return (item != null &&
                gbl.ItemDataTable[item.type].item_slot >= 11 &&
                gbl.ItemDataTable[item.type].item_slot <= 13);
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
            gbl.lastSelectetSpellTarget = null;
            gbl.byte_1D2C8 = true;

            gbl.dword_1D5CA = new spellDelegate(ovr023.cast_spell_on);

            gbl.spellTable = new Dictionary<Spells, spellDelegate2>();

            gbl.spellTable.Add(Spells.spell_01, ovr023.cleric_bless);
            gbl.spellTable.Add(Spells.spell_02, ovr023.cleric_curse);
            gbl.spellTable.Add(Spells.spell_03, ovr023.cleric_cure_light);
            gbl.spellTable.Add(Spells.spell_04, ovr023.cleric_cause_light);
            gbl.spellTable.Add(Spells.spell_05, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_06, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_07, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_08, ovr023.cleric_resist_cold);
            gbl.spellTable.Add(Spells.spell_09, ovr023.sub_5DEE1);
            gbl.spellTable.Add(Spells.spell_0a, ovr023.SpellCharm);
            gbl.spellTable.Add(Spells.spell_0b, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_0c, ovr023.is_stronger);
            gbl.spellTable.Add(Spells.spell_0d, ovr023.enlarge_end);
            gbl.spellTable.Add(Spells.spell_0e, ovr023.is_friendly);
            gbl.spellTable.Add(Spells.spell_0f, ovr023.sub_5E221);
            gbl.spellTable.Add(Spells.spell_10, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_11, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_12, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_13, ovr023.is_shielded);
            gbl.spellTable.Add(Spells.spell_14, ovr023.sub_5E2B2);
            gbl.spellTable.Add(Spells.spell_15, ovr023.falls_asleep);
            gbl.spellTable.Add(Spells.spell_16, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_17, ovr023.is_held);
            gbl.spellTable.Add(Spells.spell_18, ovr023.is_fire_resistant);
            gbl.spellTable.Add(Spells.spell_19, ovr023.is_silenced);
            gbl.spellTable.Add(Spells.spell_1a, ovr023.is_affected2);
            gbl.spellTable.Add(Spells.spell_1b, ovr023.is_charmed2);
            gbl.spellTable.Add(Spells.spell_1c, ovr023.sub_5E681);
            gbl.spellTable.Add(Spells.spell_1d, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_1e, ovr023.is_invisible);
            gbl.spellTable.Add(Spells.knock, ovr023.SpellKnock);
            gbl.spellTable.Add(Spells.spell_20, ovr023.is_duplicated);
            gbl.spellTable.Add(Spells.spell_21, ovr023.is_weakened);
            gbl.spellTable.Add(Spells.spell_22, ovr023.create_noxious_cloud);
            gbl.spellTable.Add(Spells.spell_23, ovr023.sub_5EC5B);
            gbl.spellTable.Add(Spells.spell_24, ovr023.is_animated);
            gbl.spellTable.Add(Spells.spell_25, ovr023.can_see);
            gbl.spellTable.Add(Spells.spell_26, ovr023.is_blind);
            gbl.spellTable.Add(Spells.spell_27, ovr023.sub_5F0DC);
            gbl.spellTable.Add(Spells.spell_28, ovr023.is_diseased);
            gbl.spellTable.Add(Spells.spell_29, ovr023.is_affected3);
            gbl.spellTable.Add(Spells.spell_2a, ovr023.is_praying);
            gbl.spellTable.Add(Spells.spell_2b, ovr023.uncurse);
            gbl.spellTable.Add(Spells.spell_2c, ovr023.curse);
            gbl.spellTable.Add(Spells.spell_2d, ovr023.spell_blinking);
            gbl.spellTable.Add(Spells.spell_2e, ovr023.is_affected3);
            gbl.spellTable.Add(Spells.spell_2f, ovr023.sub_5F782);
            gbl.spellTable.Add(Spells.spell_30, ovr023.cast_haste);
            gbl.spellTable.Add(Spells.spell_31, ovr023.is_held);
            gbl.spellTable.Add(Spells.spell_32, ovr023.is_invisible);
            gbl.spellTable.Add(Spells.spell_33, ovr023.sub_5FCD9);
            gbl.spellTable.Add(Spells.spell_34, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_35, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_36, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_37, ovr023.sub_5FD2E);
            gbl.spellTable.Add(Spells.spell_38, ovr023.cast_restore);
            gbl.spellTable.Add(Spells.spell_39, ovr023.cast_speed);
            gbl.spellTable.Add(Spells.spell_3a, ovr023.sub_5FF6D);
            gbl.spellTable.Add(Spells.spell_3b, ovr023.cast_strength);
            gbl.spellTable.Add(Spells.spell_3c, ovr023.sub_6003C);
            gbl.spellTable.Add(Spells.spell_3d, ovr023.cast_paralyzed);
            gbl.spellTable.Add(Spells.spell_3e, ovr023.cast_heal);
            gbl.spellTable.Add(Spells.spell_3f, ovr023.cast_invisible);
            gbl.spellTable.Add(Spells.spell_40, ovr023.sub_5F782);
            gbl.spellTable.Add(Spells.spell_41, ovr023.dam2d4plus2);
            gbl.spellTable.Add(Spells.spell_42, ovr023.dam2d8plus1);
            gbl.spellTable.Add(Spells.spell_43, ovr023.SpellNeutralizePoison);
            gbl.spellTable.Add(Spells.spell_44, ovr023.SpellPoison);
            gbl.spellTable.Add(Spells.spell_45, ovr023.is_protected);
            gbl.spellTable.Add(Spells.spell_46, ovr023.cast_flattern);
            gbl.spellTable.Add(Spells.spell_47, ovr023.sub_603F0);
            gbl.spellTable.Add(Spells.spell_48, ovr023.dam3d8plus3);
            gbl.spellTable.Add(Spells.spell_49, ovr023.is_affected4);
            gbl.spellTable.Add(Spells.spell_4a, ovr023.sub_604DA);
            gbl.spellTable.Add(Spells.spell_4b, ovr023.SpellRaiseDead);
            gbl.spellTable.Add(Spells.spell_4c, ovr023.SpellSlayLiving);
            gbl.spellTable.Add(Spells.spell_4d, ovr023.is_affected);
            gbl.spellTable.Add(Spells.spell_4e, ovr023.SpellEntangle);
            gbl.spellTable.Add(Spells.spell_4f, ovr023.SpellFaerieFire);
            gbl.spellTable.Add(Spells.spell_50, ovr023.SpellInvisToAnimals);
            gbl.spellTable.Add(Spells.spell_51, ovr023.SpellCharmPerson);
            gbl.spellTable.Add(Spells.spell_52, ovr023.cast_confuse);
            gbl.spellTable.Add(Spells.spell_53, ovr023.cast_teleport);
            gbl.spellTable.Add(Spells.spell_54, ovr023.cast_fear);
            gbl.spellTable.Add(Spells.spell_55, ovr023.cast_protection);
            gbl.spellTable.Add(Spells.spell_56, ovr023.spell_slow);
            gbl.spellTable.Add(Spells.spell_57, ovr023.sub_60F0B);
            gbl.spellTable.Add(Spells.spell_58, ovr023.sub_60F4E);
            gbl.spellTable.Add(Spells.spell_59, ovr023.uncurse);
            gbl.spellTable.Add(Spells.spell_5a, ovr023.is_animated);
            gbl.spellTable.Add(Spells.spell_5b, ovr023.spell_poisonous_cloud);
            gbl.spellTable.Add(Spells.spell_5c, ovr023.sub_61550);
            gbl.spellTable.Add(Spells.spell_5d, ovr023.SpellFeeblemind);
            gbl.spellTable.Add(Spells.spell_5e, ovr023.is_held);
            gbl.spellTable.Add(Spells.spell_5f, ovr023.sub_616CC);
            gbl.spellTable.Add(Spells.spell_60, ovr023.sub_616CC);
            gbl.spellTable.Add(Spells.spell_61, ovr023.sub_616CC);
            gbl.spellTable.Add(Spells.spell_62, ovr023.sub_61727);
            gbl.spellTable.Add(Spells.spell_63, ovr023.cast_heal2);
            gbl.spellTable.Add(Spells.spell_64, ovr023.curse);
        }
    }
}
