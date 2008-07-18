using Classes;
using System;

namespace engine
{
    class ovr025
    {
        internal static void sub_66023(Player player)
        {
            Item item = player.field_151;

            if (item != null)
            {
                uint item_type = item.type;

                player.hitBonus = player.field_73;

                if ((gbl.unk_1C020[item_type].field_E & 2) != 0)
                {
                    player.hitBonus += (sbyte)dexReactionAdj(player);
                }

                player.damageBonus = (sbyte)gbl.unk_1C020[item_type].field_B;

                if ((gbl.unk_1C020[item_type].field_E & 0x04) != 0)
                {
                    player.hitBonus += strengthHitBonus(player);
                    player.damageBonus += strengthDamBonus(player);
                }

                sbyte bonus = item.plus;

                if (gbl.unk_1C020[item_type].field_E > 0x7F &&
                    player.Item_ptr_04 != null)
                {
                    bonus += player.Item_ptr_04.plus;
                }

                if ((gbl.unk_1C020[item_type].field_E & 0x01) != 0 &&
                    player.Item_ptr_03 != null)
                {
                    bonus += player.Item_ptr_03.plus;
                }

                player.damageBonus += bonus;

                if (player.race == Race.elf &&
                    ((item.type > 0x28 && item.type < 0x2D) ||
                      item.type == 0x25 || item.type == 0x24))
                {
                    bonus++;
                }

                player.hitBonus += bonus;
                player.attack_dice_count = gbl.unk_1C020[item_type].field_9;
                player.attack_dice_size = gbl.unk_1C020[item_type].field_A;
            }
        }

        /// <summary>
        /// Item weight initiative effect check.
        /// </summary>
        internal static void sub_6621E(Item item, Player player)
        {
            if (gbl.unk_1C020[item.type].item_slot == 2)
            {
                if (item.weight >= 0 && item.weight <= 0x96)
                {
                    player.movement = player.field_E4;
                }
                else if (item.weight >= 0x97 && item.weight <= 0x18F)
                {
                    player.movement = 9;
                }
                else
                {
                    player.movement = 6;
                }

                if (player.movement != 0 && player.movement <= 9)
                {
                    player.movement += 3;
                }
            }
        }


        internal static void sub_662A6(ref byte output, ref sbyte[] bonus, Item item, Player player)
        {
            byte var_1 = gbl.unk_1C020[item.type].field_6;
            if (var_1 > 0x7f)
            {
                var_1 &= 0x7F;
                byte var_2 = gbl.unk_1C020[item.type].item_slot;
                if (var_2 == 1)
                {
                    bonus[1] = (sbyte)(item.plus + var_1);
                    return;
                }

                if (var_1 == 0)
                {
                    if (var_2 == 9)
                    {
                        if (item.plus > bonus[3])
                        {
                            bonus[3] = item.plus;
                        }
                    }
                    else
                    {
                        bonus[2] += (sbyte)(item.plus);
                    }

                    player.field_186 = (sbyte)(player.field_186 + item.plus_save);
                    return;
                }


                if ((item.plus + var_1) > bonus[4])
                {
                    bonus[4] = (sbyte)(item.plus + var_1);

                    if (item.plus > 0)
                    {
                        if (var_2 == 2)
                        {
                            output = 1;
                        }
                    }
                }
            }
        }


        internal static void calc_movement(Player player) /* sub_663C4 */
        {
            int overload = player.weight - max_encumberance(player);

            if (overload < 0)
            {
                overload = 0;
            }

            int moves;
            if (overload >= 0 && overload <= 0x200)
            {
                moves = player.movement;
            }
            else if (overload >= 0x201 && overload <= 0x300)
            {
                moves = 9;
            }
            else if (overload >= 0x301 && overload <= 0x400)
            {
                moves = 6;
            }
            else
            {
                moves = 3;
            }

            if (moves < player.movement)
            {
                player.movement = (byte)moves;
            }
        }


        static string[] itemNames = { "",
            "Battle Axe","Hand Axe","Bardiche","Bec De Corbin","Bill-Guisarme",
            "Bo Stick", "Club","Dagger","Dart","Fauchard",
            
            "Fauchard-Fork","Flail","Military Fork","Glaive","Glaive-Guisarme",
            "Guisarme","Guisarme-Voulge","Halberd","Lucern Hammer","Hammer",
            
            "Javelin","Jo Stick","Mace","Morning Star","Partisan",      
            "Military Pick","Awl Pike","Quarrel","Ranseur","Scimitar",
 
            "Spear","Spetum","Quarter Staff","Bastard Sword","Broad Sword",
            
            "Long Sword","Short Sword","Two-Handed Sword","Trident","Voulge",
            "Composite Long Bow","Composite Short Bow","Long Bow","Short Bow","Heavy Crossbow",
            
            "Light Crossbow","Sling","Mail","Armor","Leather",
            "Padded","Studded","Ring","Scale","Chain",					 
            "Splint","Banded","Plate","Shield","Woods",
            
            "Arrow",string.Empty,string.Empty,"Potion","Scroll",
            "Ring","Rod","Stave","Wand","Jug",
            "Amulet","Dragon Breath","Bag","Defoliation","Ice Storm",
            "Book","Boots","Hornets Nest","Bracers","Piercing",

            "Brooch","Elfin Chain","Wizardry","ac10", "Dexterity",
            "Fumbling","Chime","Cloak","Crystal","Cube",
			"Cubic","The Dwarves","Decanter","Gloves","Drums",
            "Dust","Thievery","Hat","Flask","Gauntlets",
            
            "Gem","Girdle","Helm","Horn","Stupidity",
            "Incense","Stone","Ioun Stone", "Javelin","Jewel",
            "Ointment","Pale Blue","Scarlet And","Manual","Incandescent",
            
            "Deep Red","Pink","Mirror","Necklace","And Green",
            "Blue","Pearl","Powerlessness",
								 "Vermin","Pipes","Hole","Dragon Slayer","Robe","Rope",
								 "Frost Brand","Berserker","Scarab","Spade","Sphere",
								 "Blessed","Talisman","Tome","Trident","Grimoire","Well",
								 "Wings","Vial","Lantern",string.Empty,"Flask of Oil",
								 "10 ft. Pole","50 ft. Rope","Iron","Thf Prickly Tools",
								 "Iron Rations","Standard Rations","Holy Symbol",
								 "Holy Water vial","Unholy Water vial","Barding","Dragon",
								 "Lightning","Saddle","Staff","Drow","Wagon","+1",
								 "+2","+3","+4","+5","of","Vulnerability","Cloak",
								 "Displacement","Torches","Oil","Speed","Tapestry",
								 "Spine","Copper","Silver","Electrum","Gold","Platinum",
								 "Ointment","Keoghtum's","Sheet","Strength","Healing",
								 
                                 "Holding","Extra","Gaseous Form","Slipperiness",
								 "Jewelled","Flying","Treasure Finding","Fear",
								 "Disappearance","Statuette","Fungus","Chain","Pendant",
								 "Broach","Of Seeking","-1","-2","-3","Lightning Bolt",
								 "Fire Resistance","Magic Missiles","Save","Clrc Scroll",
								 "MU Scroll","With 1 Spell","With 2 Spells","With 3 Spells",
								 "Prot. Scroll","Jewelry","Fine","Huge","Bone","Brass",
								 "Key","AC 2","AC 6","AC 4","AC 3","Of Prot.","Paralyzation",
								 "Ogre Power","Invisibility","Missiles","Elvenkind",
								 "Rotting","Covered","Efreeti","Bottle","Missile Attractor",
								 "Of Maglubiyet","Secr Door & Trap Det","Gd Dragon Control",
								 "Feather Falling","Giant Strength","Restoring Level(s)",
								 "Flame Tongue","Fireballs","Spiritual","Boulder","Diamond",
								 "Emerald","Opal","Saphire","Of Tyr","Of Tempus","Of Sune",
								 "Wooden","+3 vs Undead","Pass","Cursed" 
							 };



        internal static void DebugItems()
        {
            Item it;
            for (int i = 0; i < 0x81; i++)
            {
                ovr022.create_item(out it, i);
                int a = it.field_2F < itemNames.Length ? (int)(byte)it.field_2F : 0;
                int b = it.field_30 < itemNames.Length ? (int)(byte)it.field_30 : 0;
                int c = it.field_31 < itemNames.Length ? (int)(byte)it.field_31 : 0;
                Player pl = new Player();
                pl.field_151 = it;
                bool o1 = is_weapon_ranged(pl);
                bool o20 = is_weapon_ranged_melee(pl);
                System.Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}",
                    i, itemNames[c], itemNames[b], itemNames[a],
                    it.type, o1, o20);
            }
        }

        internal static void ItemDisplayNameBuild(bool display_new_name, bool displayReadied, byte arg_4, byte arg_6, Item item, Player player) /*id_item*/
        {
            Player player_ptr;

            item.name = string.Empty;

            if (displayReadied == true)
            {
                if (item.readied)
                {
                    item.name = " Yes  ";
                }
                else
                {
                    item.name = " No   ";
                }
            }

            bool detectMagic = false;
            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null && detectMagic == false)
            {
                if (find_affect(Affects.detect_magic, player_ptr) == true)
                {
                    detectMagic = true;
                }

                player_ptr = player_ptr.next_player;
            }

            if (detectMagic == true &&
                (item.plus > 0 || item.plus_save > 0 || item.cursed == true))
            {
                item.name += "* ";
            }

            if (item.count > 0)
            {
                item.name += item.count.ToString() + " ";
            }

            int hidden_names_flag = item.hidden_names_flag;

            if (Cheats.display_full_item_names)
            {
                //item.hidden_names_flag = 0;
                hidden_names_flag = 0;
            }

            int display_flags = 0;
            display_flags |= (item.field_2F != 0 && (hidden_names_flag & 0x4) == 0) ? 0x1 : 0;
            display_flags |= (item.field_30 != 0 && (hidden_names_flag & 0x2) == 0) ? 0x2 : 0;
            display_flags |= (item.field_31 != 0 && (hidden_names_flag & 0x1) == 0) ? 0x4 : 0;

            bool pural_added = false;

            for (int var_1 = 3; var_1 >= 1; var_1--)
            {
                if (((display_flags >> (var_1 - 1)) & 1) > 0)
                {
                    item.name += itemNames[item.field_2EArray(var_1)];

                    if (item.count < 2 ||
                        pural_added == true)
                    {
                        item.name += " ";
                    }
                    else if ((1 << (var_1 - 1) == display_flags) ||
                            (var_1 == 1 && display_flags > 4 && item.type != 0x56) ||
                            (var_1 == 2 && (display_flags & 1) == 0) ||
                            (var_1 == 3 && item.type == 0x56) ||
                            (item.field_31 != 0x87 && (item.type == 0x49 || item.type == 0x1c || item.type == 0x09) && item.field_31 != 0xb1))
                    {
                        item.name += "s ";
                        pural_added = true;
                    }
                    else
                    {
                        item.name += " ";
                    }
                }
            }

            if (display_new_name)
            {
                seg041.displayString(item.name, 0, 10, arg_4, arg_6);
            }
        }


        internal static void Player_Summary(Player player)
        {
            int var_7;
            int y_pos;
            int x_pos;
            Player player_ptr;

            if (gbl.game_state != 3)
            {

                if (gbl.game_state == 0)
                {
                    x_pos = 1;
                }
                else
                {
                    x_pos = 17;
                }

                y_pos = 2;

                seg041.displayString("Name", 0, 15, y_pos, x_pos);
                seg041.displayString("AC  HP", 0, 15, y_pos, 0x21);

                y_pos += 2;

                player_ptr = gbl.player_next_ptr;

                while (player_ptr != null)
                {
                    seg037.draw8x8_clear_area(y_pos, 0x26, y_pos, x_pos);

                    if (player_ptr == player)
                    {
                        seg041.displayString(player_ptr.name, 0, 15, y_pos, x_pos);
                    }
                    else
                    {
                        displayPlayerName(false, y_pos, x_pos, player_ptr);
                    }

                    if (player_ptr.ac >= 0 && player_ptr.ac <= 0x32)
                    {
                        var_7 = 1;
                    }
                    else if (player_ptr.ac >= 0x33 && player_ptr.ac <= 0x3C)
                    {
                        var_7 = 2;
                    }
                    else if (player_ptr.ac >= 0x3D && player_ptr.ac <= 0x45)
                    {
                        var_7 = 1;
                    }
                    else
                    {
                        var_7 = 0;
                    }

                    display_AC(y_pos, var_7 + 0x1F /*+0x20*/, player_ptr);

                    if (player_ptr.hit_point_current >= 0 && player_ptr.hit_point_current <= 9)
                    {
                        var_7 = 2;
                    }
                    else if (player_ptr.hit_point_current >= 10 && player_ptr.hit_point_current <= 99)
                    {
                        var_7 = 1;
                    }
                    else
                    {
                        var_7 = 0;
                    }

                    display_hp(false, y_pos, var_7 + 0x24, player_ptr);
                    y_pos++;
                    player_ptr = player_ptr.next_player;
                }

                seg037.draw8x8_clear_area(y_pos, 0x26, y_pos, x_pos);
            }
        }


        internal static void display_AC(int y_offset, int x_offset, Player player)
        {
            string text = (0x3c - (int)player.ac).ToString();

            seg041.displayString(text, 0, 10, y_offset, x_offset);
        }


        internal static void display_hp(bool arg_0, int y_pos, int x_pos, Player player)
        {
            int colour;

            if (player.hit_point_current < player.hit_point_max)
            {
                colour = 0x0E;
            }
            else
            {
                colour = 0x0A;
            }

            if (arg_0 == true)
            {
                colour = 0x0D;
            }

            seg041.displayString(player.hit_point_current.ToString(), 0, colour, y_pos, x_pos);
        }


        internal static void display_hitpoint_ac(Player player) /* hitpoint_ac */
        {
            if (gbl.display_hitpoints_ac == true)
            {
                gbl.display_hitpoints_ac = false;
                seg037.draw8x8_clear_area(0x15, 0x26, 1, 0x17);

                int line = 1;

                DisplayPlayerStatusString(false, line, " ", player);

                line++;

                seg041.displayString("Hitpoints", 0, 10, line + 1, 0x17);

                display_hp(false, line + 1, 0x21, player);
                line += 2;

                seg041.displayString("AC", 0, 10, line + 1, 0x17);
                display_AC(line + 1, 0x1A, player);

                gbl.textYCol = line + 1;

                if (player.field_151 != null)
                {
                    line += 2;
                    /*var_1 = 0x17;*/
                    ItemDisplayNameBuild(false, false, 0, 0, player.field_151, player);

                    seg041.press_any_key(player.field_151.name, true, 0, 10, line + 3, 0x26, line + 1, 0x17);
                }

                line = gbl.textYCol + 1;

                if (player.in_combat == false)
                {
                    seg041.displayString(ovr020.statusString[(int)player.health_status], 0, 15, line + 1, 0x17);
                }
                else if (is_held(player) == true)
                {
                    seg041.displayString("(Helpless)", 0, 15, line + 1, 0x17);
                }
            }
        }

        static Affects[] affects_01 = { Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless };


        internal static bool is_held(Player player)
        {
            bool held = false;
            for (int loop_var = 0; loop_var < 4; loop_var++)
            {
                if (find_affect(affects_01[loop_var], player) == true)
                {
                    held = true;
                }
            }

            return held;
        }


        internal static void reclac_player_values(Player player) /* sub_66C20 */
        {
            sbyte[] stat_bonus = new sbyte[5];

            player.field_151 = null;
            player.field_155 = null;
            player.field_159 = null;
            player.field_15D = null;
            player.field_161 = null;
            player.field_165 = null;
            player.field_169 = null;
            player.field_16D = null;
            player.field_171 = null;
            player.Item_ptr_01 = null;
            player.Item_ptr_02 = null;
            player.Item_ptr_03 = null;
            player.Item_ptr_04 = null;

            bool var_8 = false ;
            player.field_14C = 0;

            Item item = player.itemsPtr;
            player.field_185 = 0;

            player.weight = 0;
            gbl.word_1AFE0 = 0;

            while (item != null)
            {
                player.field_14C++;
                short item_weight = item.weight;

                if (item.count > 0)
                {
                    item_weight *= (short)item.count;
                }

                player.weight += item_weight;

                if (item.readied)
                {
                    gbl.word_1AFE0 += item_weight;

                    int var_13 = gbl.unk_1C020[item.type].item_slot;

                    if (var_13 >= 0 && var_13 <= 8)
                    {
                        player.itemArray[var_13] = item;
                    }
                    else if (var_13 == 9)
                    {
                        if (player.Item_ptr_01 != null)
                        {
                            if (player.Item_ptr_02 == null)
                            {
                                player.Item_ptr_02 = item;
                            }
                        }
                        else
                        {
                            player.Item_ptr_01 = item;
                        }
                    }

                    if (item.type == 0x49)
                    {
                        player.Item_ptr_03 = item;
                    }

                    if (item.type == 0x1C)
                    {
                        player.Item_ptr_04 = item;
                    }

                    player.field_185 += gbl.unk_1C020[item.type].field_1;
                }

                item = item.next;
            }


            for (int money = 0; money < 7; money++)
            {
                player.weight += player.Money[money];
            }

            player.attack_dice_count = player.field_11E;
            player.field_19F = player.field_11F;

            player.attack_dice_size = player.field_120;
            player.field_1A1 = player.field_121;

            player.damageBonus = player.field_122;
            player.field_1A3 = player.field_123;

            for (int i = 0; i <= 4; i++)
            {
                stat_bonus[i] = 0;
            }

            byte var_7 = 0;

            player.field_186 = 0;
            player.ac = player.field_124;
            player.movement = player.field_E4;
            player.hitBonus = player.field_73;

            stat_bonus[0] = ovr025.dex_ac_bonus(player);

            if (player.field_151 == null)
            {
                player.hitBonus += strengthHitBonus(player);
                player.damageBonus += strengthDamBonus(player);
            }

            sub_66023(player);
            item = player.itemsPtr;

            while (item != null)
            {
                if (item.readied)
                {
                    sub_6621E(item, player);
                    sub_662A6(ref var_7, ref stat_bonus, item, player);
                }

                item = item.next;
            }

            if (var_7 != 0)
            {
                stat_bonus[3] = 0;
            }

            if (var_8 == true)
            {
                if (player.weight < 5000)
                {
                    player.weight = 0;
                }
                else
                {
                    player.weight -= 5000;
                }

                if (player.weight < gbl.word_1AFE0)
                {
                    player.weight = gbl.word_1AFE0;
                }
            }

            calc_movement(player);

            if (stat_bonus[4] < player.ac)
            {
                stat_bonus[4] = (sbyte)player.ac;
            }

            player.ac = 0;

            for (int i = 0; i <= 4; i++)
            {
                player.ac += (byte)stat_bonus[i];
            }

            player.field_19B = (byte)((stat_bonus[4] + stat_bonus[2] + stat_bonus[3]) - 2);

            if ((player.fighter_lvl > 0 ||
                (player.field_113 > 0 && ovr026.sub_6B3D1(player) != 0)) &&
                player.race > Race.monster)
            {
                player.field_DD = (byte)((player.field_113 * ovr026.sub_6B3D1(player)) + player.fighter_lvl);
            }
            else
            {
                player.field_DD = 1;
            }
        }


        internal static sbyte dex_ac_bonus(Player player) /* stat_bonus */
        {
            sbyte bonus;

            byte stat_val = player.dex;

            if (stat_val >= 1 && stat_val <= 3)
            {
                bonus = -4;
            }
            else if (stat_val >= 4 && stat_val <= 6)
            {
                bonus = (sbyte)(stat_val - 7);
            }
            else if (stat_val >= 15 && stat_val <= 18)
            {
                bonus = (sbyte)(stat_val - 14);
            }
            else if (stat_val == 19 || stat_val == 20)
            {
                bonus = 4;
            }
            else if (stat_val >= 21 && stat_val <= 23)
            {
                bonus = 5;
            }
            else if (stat_val == 24 || stat_val == 25)
            {
                bonus = 6;
            }
            else
            {
                bonus = 0;
            }

            return bonus;
        }


        internal static int dexReactionAdj(Player player)
        {
            int bonus;

            int stat_val = player.dex;

            if (stat_val >= 0 && stat_val <= 2)
            {
                bonus = -4;
            }
            else if (stat_val >= 3 && stat_val <= 5)
            {
                bonus = stat_val - 6;
            }
            else if (stat_val >= 16 && stat_val <= 18)
            {
                bonus = stat_val - 15;
            }
            else if (stat_val >= 19 && stat_val <= 20)
            {
                bonus = 3;
            }
            else if (stat_val >= 21 && stat_val <= 23)
            {
                bonus = 4;
            }
            else if (stat_val >= 24 && stat_val <= 25)
            {
                bonus = 5;
            }
            else
            {
                bonus = 0;
            }

            return bonus;
        }


        internal static int player_strenght_group(Player player) /* playerStrengh */
        {
            int ret_val;

            if (player.strength >= 0 && player.strength <= 17)
            {
                ret_val = player.strength;
            }
            else if (player.strength == 18)
            {
                if (player.tmp_str_00 == 0)
                {
                    ret_val = 18;
                }
                else if (player.tmp_str_00 >= 1 && player.tmp_str_00 <= 50)
                {
                    ret_val = 19;
                }
                else if (player.tmp_str_00 >= 51 && player.tmp_str_00 <= 75)
                {
                    ret_val = 20;
                }
                else if (player.tmp_str_00 >= 76 && player.tmp_str_00 <= 90)
                {
                    ret_val = 21;
                }
                else if (player.tmp_str_00 >= 91 && player.tmp_str_00 <= 99)
                {
                    ret_val = 22;
                }
                else if (player.tmp_str_00 >= 100)
                {
                    ret_val = 23;
                }
                else
                {
                    throw new System.NotSupportedException();
                }
            }
            else if (player.strength >= 19 && player.strength <= 25)
            {
                ret_val = player.strength + 5;
            }
            else
            {
                throw new System.NotSupportedException();
            }

            return ret_val;
        }


        internal static sbyte strengthHitBonus(Player player)
        {
            int str_bonus = 0;
            int str_stat = player_strenght_group(player);

            if (player.field_125 != 0)
            {
                if (str_stat >= 1 && str_stat <= 3)
                {
                    str_bonus = -3;
                }
                else if (str_stat == 4 || str_stat == 5)
                {
                    str_bonus = -2;
                }
                else if (str_stat == 6 || str_stat == 7)
                {
                    str_bonus = -1;
                }
                else if (str_stat >= 17 && str_stat <= 19)
                {
                    str_bonus = 1;
                }
                else if (str_stat >= 20 && str_stat <= 22)
                {
                    str_bonus = 2;
                }
                else if (str_stat >= 23 && str_stat <= 25)
                {
                    str_bonus = 3;
                }
                else if (str_stat == 26 || str_stat == 27)
                {
                    str_bonus = 4;
                }
                else if (str_stat >= 28 && str_stat <= 30)
                {
                    str_bonus = str_stat - 23; ;
                }
            }

            return (sbyte)str_bonus;
        }


        internal static sbyte strengthDamBonus(Player player)
        {
            int damage_bonus = 0;

            int var_2 = player_strenght_group(player);

            if (player.field_125 != 0)
            {
                if (var_2 == 1 || var_2 == 2)
                {
                    damage_bonus = -2;
                }
                else if (var_2 >= 3 && var_2 <= 5)
                {
                    damage_bonus = -1;
                }
                else if (var_2 == 16)
                {
                    damage_bonus = 1;
                }
                else if (var_2 >= 17 && var_2 <= 19)
                {
                    damage_bonus = var_2 - 16;
                }
                else if (var_2 >= 20 && var_2 <= 29)
                {
                    damage_bonus = var_2 - 17;
                }
                else if (var_2 == 30)
                {
                    damage_bonus = 14;
                }
            }

            return (sbyte)damage_bonus;
        }


        internal static int max_encumberance(Player player) /* strength_bonus */
        {
            int max_encumberance;

            int str = player_strenght_group(player);

            if (str >= 1 && str <= 3)
            {
                max_encumberance = -350;
            }
            else if (str == 4 || str == 5)
            {
                max_encumberance = -250;
            }
            else if (str == 6 || str == 7)
            {
                max_encumberance = -150;
            }
            else if (str == 12 || str == 13)
            {
                max_encumberance = 100;
            }
            else if (str == 14 || str == 15)
            {
                max_encumberance = 200;
            }
            else if (str == 16)
            {
                max_encumberance = 350;
            }
            else if (str >= 17 && str <= 21)
            {
                max_encumberance = ((str - 17) * 250) + 500;
            }
            else if (str >= 22 && str <= 26)
            {
                max_encumberance = ((str - 22) * 1000) + 2000;
            }
            else if (str == 27)
            {
                max_encumberance = 7500;
            }
            else if (str >= 28 && str <= 30)
            {
                max_encumberance = ((str - 28) * 3000) + 9000;
            }
            else
            {
                max_encumberance = 0;
            }

            return max_encumberance;
        }


        internal static void clear_spell(byte spell_id, Player player)
        {
            for (int i = 0; i < gbl.max_spells; i++)
            {
                if (player.spell_list[i] == spell_id)
                {
                    player.spell_list[i] = 0;
                    break;
                }
            }
        }


        internal static void lose_item(Item item, Player player)
        {
            Item item_ptr;

            if (item == null)
            {
                seg041.displayAndDebug("Nil Item pointer...", 0, 14);
            }
            else
            {
                item_ptr = player.itemsPtr;

                if (item_ptr == item)
                {
                    player.itemsPtr = item.next;
                }
                else
                {
                    while (item_ptr != null &&
                        item_ptr.next != item)
                    {
                        item_ptr = item_ptr.next;
                    }
                }

                if (item_ptr == null)
                {
                    seg041.displayAndDebug("Tried to Lose item & couldn't find it!", 0, 14);
                }
                else
                {
                    item_ptr.next = item.next;
                    item.next = null;

                    seg051.FreeMem(0x3f, item);
                }
            }
        }


        internal static void addItem(Item item, Player player)
        {
            Item new_item = item.ShallowClone();
            new_item.next = null;

            if (player.itemsPtr == null)
            {
                player.itemsPtr = new_item;
            }
            else
            {
                Item tmp_item = player.itemsPtr;

                while (tmp_item.next != null)
                {
                    tmp_item = tmp_item.next;
                }

                tmp_item.next = new_item;
            }
        }


        internal static void string_print01(string arg_0)
        {
            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);

            seg041.displayString(arg_0, 0, 10, 0x18, 0);

            seg041.GameDelay();

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
        }


        internal static void DisplayPlayerStatusString(bool clearDisplay, int lineY, string text, Player player) /* sub_67788 */
        {
            if (gbl.game_state == 5)
            {
                seg037.draw8x8_clear_area(0x15, 0x26, lineY, 0x17);

                displayPlayerName(false, lineY, 0x17, player);
                seg041.press_any_key(text, true, 0, 10, 0x15, 0x26, lineY + 1, 0x17);
            }
            else
            {
                int line_y = gbl.displayPlayerStatusLine18 ? 18 : 17;

                seg037.draw8x8_clear_area(0x16, 0x26, line_y, 1);

                displayPlayerName(false, line_y + 1, 1, player);
                seg041.press_any_key(text, true, 0, 10, 0x16, 0x26, line_y + 2, 1);
            }

            if (clearDisplay == true)
            {
                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
            }
        }


        internal static void ClearPlayerTextArea() /* sub_6786F */
        {
            if (gbl.game_state == 5)
            {
                seg037.draw8x8_clear_area(0x15, 0x26, 0x0a, 0x17);
            }
            else
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 0x12, 1);
            }
        }


        internal static void displayPlayerName(bool pural, int y_offset, int x_offset, Player player) /*sub_678A2*/
        {
            byte color;

            if (player.in_combat == false)
            {
                color = 0x0C;
            }
            else if (player.combat_team == CombatTeam.Enemy)
            {
                color = 0x0E;
            }
            else
            {
                color = 0x0B;
            }

            seg041.displayString(player.name, 0, color, y_offset, x_offset);

            if (pural == true)
            {
                seg041.displayString("s", 0, color, y_offset, x_offset + player.name.Length);
            }
        }


        internal static void load_missile_dax(bool flipIcon, byte iconOffset, int iconAction, int iconIdx) /* sub_67924 */
        {
            int dataSize = gbl.missile_dax.bpp;

            if (flipIcon == true)
            {
                DaxBlock var_4;
                seg040.init_dax_block(out var_4, 1, 1, 3, 0x18);
                seg040.flipIconLeftToRight(var_4, gbl.combat_icons[iconIdx, iconAction]);

                System.Array.Copy(var_4.data, 0, gbl.missile_dax.data, iconOffset * dataSize, dataSize);
                System.Array.Copy(var_4.data_ptr, 0, gbl.missile_dax.data_ptr, iconOffset * dataSize, dataSize);

                seg040.free_dax_block(ref var_4);
            }
            else
            {
                DaxBlock src = gbl.combat_icons[iconIdx, iconAction];
                if (src != null)
                {
                    System.Array.Copy(gbl.combat_icons[iconIdx, iconAction].data, 0, gbl.missile_dax.data, iconOffset * dataSize, dataSize);
                    System.Array.Copy(gbl.combat_icons[iconIdx, iconAction].data_ptr, 0, gbl.missile_dax.data_ptr, iconOffset * dataSize, dataSize);
                }
                else
                {
                    System.Array.Clear(gbl.missile_dax.data, iconOffset * dataSize, dataSize);
                    System.Array.Clear(gbl.missile_dax.data_ptr, iconOffset * dataSize, dataSize);
                }
            }
        }


        internal static void load_missile_icons(int iconIdx) /* sub_67A59 */
        {
            load_missile_dax(false, 0, 0, iconIdx);
            load_missile_dax(true, 1, 0, iconIdx);
            load_missile_dax(true, 2, 1, iconIdx);
            load_missile_dax(false, 3, 1, iconIdx);
        }


        internal static void draw_missile_attack(int delay, int frameCount, int target_y, int target_x, int attacker_y, int attacker_x) /* sub_67AA4 */
        {
            int cur_y;
            int cur_x;
            short var_C8;
            short var_C6;
            short var_C0;
            short var_BE;
            int center_y;
            int center_x;
            bool var_B4;
            bool var_B3;
            SteppingPath var_AC = new SteppingPath();
            byte[] var_94 = new byte[0x94];

            int var_BA = attacker_x;
            int var_BC = attacker_y;

            seg051.FillChar(8, 0x94, var_94);

            int var_AF = 0;
            int frame = 0;

            var_AC.attacker_x = attacker_x * 3;
            var_AC.attacker_y = attacker_y * 3;
            var_AC.target_x = target_x * 3;
            var_AC.target_y = target_y * 3;

            var_AC.CalculateDeltas();

            do
            {
                var_B4 = !var_AC.Step();

                var_94[var_AF] = var_AC.direction;

                var_AF++;
            } while (var_B4 == false);

            int var_B0 = var_AF - 2;

            if (var_B0 < 2 || var_AF < 2)
            {
                return;
            }

            int diff_x = target_x - attacker_x;
            int diff_y = target_y - attacker_y;

            if (ovr033.CoordOnScreen((attacker_y - gbl.mapToBackGroundTile.mapScreenTopY), (attacker_x - gbl.mapToBackGroundTile.mapScreenLeftX)) == false ||
                ovr033.CoordOnScreen((target_y - gbl.mapToBackGroundTile.mapScreenTopY), (target_x - gbl.mapToBackGroundTile.mapScreenLeftX)) == false)
            {
                if (System.Math.Abs(diff_x) <= 6 &&
                    System.Math.Abs(diff_y) <= 6)
                {
                    var_B3 = true;
                    center_x = (diff_x / 2) + attacker_x;
                    center_y = (diff_y / 2) + attacker_y;
                }
                else
                {
                    var_B3 = false;
                    center_x = gbl.mapToBackGroundTile.mapScreenLeftX + 3;
                    center_y = gbl.mapToBackGroundTile.mapScreenTopY + 3;
                }
            }
            else
            {
                var_B3 = true;
                center_x = gbl.mapToBackGroundTile.mapScreenLeftX + 3;
                center_y = gbl.mapToBackGroundTile.mapScreenTopY + 3;
            }

            ovr033.redrawCombatArea(8, 0xFF, center_y, center_x);
            var_AF = 0;
            var_BE = 0;
            var_C0 = 0;

            do
            {
                cur_x = ((attacker_x - gbl.mapToBackGroundTile.mapScreenLeftX) * 3) + var_BE;
                cur_y = ((attacker_y - gbl.mapToBackGroundTile.mapScreenTopY) * 3) + var_C0;
                var_BA = attacker_x;
                var_BC = attacker_y;
                var_B4 = false;

                do
                {
                    var_C6 = gbl.MapDirectionXDelta[var_94[var_AF]];
                    var_C8 = gbl.MapDirectionYDelta[var_94[var_AF]];
                    cur_x += var_C6;
                    cur_y += var_C8;


                    if (delay > 0 ||
                        (cur_x % 3) == 0 ||
                        (cur_y % 3) == 0)
                    {
                        Display.SaveVidRam();
                        seg040.OverlayBounded(gbl.missile_dax, 5, frame, cur_y, cur_x);
                        seg040.DrawOverlay();

                        seg049.SysDelay(delay);

                        Display.RestoreVidRam();
                        frame++;

                        if (frame >= frameCount)
                        {
                            frame = 0;
                        }
                    }


                    var_AF++;
                    if (cur_x < 0 || cur_x > 0x12 || cur_y < 0 || cur_y > 0x12)
                    {
                        var_B4 = true;
                    }

                    if (var_B4 == false &&
                        var_AF < var_B0)
                    {
                        var_BE += var_C6;
                        var_C0 += var_C8;

                        if (Math.Abs(var_BE) == 3)
                        {
                            attacker_x += Math.Sign(var_BE);
                            center_x += Math.Sign(var_BE);
                            var_BE = 0;
                        }

                        if (Math.Abs(var_C0) == 3)
                        {
                            attacker_y += Math.Sign(var_C0);
                            center_y += Math.Sign(var_C0);
                            var_C0 = 0;
                        }
                    }

                } while (var_AF < var_B0 && var_B4 == false);

                if (var_AF < var_B0)
                {
                    int var_CE = 0;
                    int var_D0 = 0;
                    var_C0 = 0;
                    var_BE = 0;
                    attacker_x = target_x;
                    attacker_y = target_y;
                    var_BA = attacker_x;
                    var_BC = attacker_y;

                    if ((target_x + 3) > 0x31)
                    {
                        var_CE = target_x - 0x31;

                    }
                    else if (target_x < 3)
                    {
                        var_CE = 3 - target_x;
                    }

                    if ((target_y + 3) > 0x18)
                    {
                        var_D0 = target_y - 0x18;
                    }
                    else if (target_y < 3)
                    {
                        var_D0 = 3 - target_y;
                    }

                    center_x = target_x + var_CE;
                    center_y = target_y + var_D0;

                    ovr033.redrawCombatArea(8, 0xff, center_y, center_x);
                    cur_x = (target_x - gbl.mapToBackGroundTile.mapScreenLeftX) * 3;
                    cur_y = (target_y - gbl.mapToBackGroundTile.mapScreenTopY) * 3;
                    var_AF = var_B0;
                    var_B4 = false;

                    do
                    {
                        var_C6 = (short)(-gbl.MapDirectionXDelta[var_94[var_AF]]);
                        var_C8 = (short)(-gbl.MapDirectionYDelta[var_94[var_AF]]);

                        cur_x += var_C6;
                        cur_y += var_C8;

                        if (cur_x > 0x12)
                        {
                            attacker_x = gbl.mapToBackGroundTile.mapScreenLeftX + 6;
                        }
                        else if (cur_x < 0)
                        {
                            attacker_x = gbl.mapToBackGroundTile.mapScreenLeftX;
                        }

                        if (cur_y > 0x12)
                        {
                            attacker_y = gbl.mapToBackGroundTile.mapScreenTopY + 6;
                        }
                        else if (cur_y < 0)
                        {
                            attacker_y = gbl.mapToBackGroundTile.mapScreenTopY;
                        }

                        if (cur_x < 0 || cur_x > 0x12 || cur_y < 0 || cur_y > 0x12)
                        {
                            var_B4 = true;
                        }

                        if (var_B4 == false)
                        {
                            var_BE += var_C6;
                            var_C0 += var_C8;
                            if (System.Math.Abs(var_BE) == 3)
                            {
                                attacker_x += Math.Sign(var_BE);
                                center_x += Math.Sign(var_BE);
                                var_BE = 0;
                            }

                            if (System.Math.Abs(var_C0) == 3)
                            {
                                attacker_y += Math.Sign(var_C0);
                                center_y += Math.Sign(var_C0);
                                var_C0 = 0;
                            }

                            var_AF -= 1;
                        }

                    } while (var_B4 == false);
                }
                else
                {
                    var_B3 = true;

                    if (ovr033.CoordOnScreen(target_y - gbl.mapToBackGroundTile.mapScreenTopY, target_x - gbl.mapToBackGroundTile.mapScreenLeftX) == false)
                    {
                        ovr033.redrawCombatArea(8, 3, target_y, target_x);
                    }

                    cur_x = (target_x - gbl.mapToBackGroundTile.mapScreenLeftX) * 3;
                    cur_y = (target_y - gbl.mapToBackGroundTile.mapScreenTopY) * 3;

                    Display.SaveVidRam();
                    seg040.OverlayBounded(gbl.missile_dax, 5, frame, cur_y, cur_x);

                    if (delay > 0)
                    {
                        seg040.DrawOverlay();

                        seg049.SysDelay(delay);

                        Display.RestoreVidRam();
                    }
                }
            } while (var_B3 == false);

            seg040.DrawOverlay();
        }


        internal static void sub_6818A(string text, bool arg_4, Player player)
        {
            if (gbl.game_state == 5)
            {
                int iconId = arg_4 ? 0x16 : 0x17;

                load_missile_icons(iconId);

                if (ovr033.sub_74761(true, player) == false)
                {
                    ovr033.redrawCombatArea(8, 3, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));
                }

                if (arg_4)
                {
                    seg044.sound_sub_120E0(gbl.sound_4_188C6);
                }
                else
                {
                    seg044.sound_sub_120E0(gbl.sound_3_188C4);
                }

                DisplayPlayerStatusString(false, 10, text, player);

                int var_106 = arg_4 ? gbl.game_speed_var : 0;

                int colX = gbl.playerScreenX[ovr033.get_player_index(player)] * 3;
                int rowY = gbl.playerScreenY[ovr033.get_player_index(player)] * 3;

                for (int var_105 = 0; var_105 <= var_106; var_105++)
                {
                    for (int frame = 0; frame <= 3; frame++)
                    {
                        Display.SaveVidRam();

                        seg040.OverlayBounded(gbl.missile_dax, 5, frame, rowY, colX);
                        seg040.DrawOverlay();

                        seg049.SysDelay(70);

                        Display.RestoreVidRam();
                    }
                }

                seg040.DrawOverlay();

                if (var_106 == 0)
                {
                    seg041.GameDelay();
                }
            }
            else
            {
                DisplayPlayerStatusString(true, 10, text, player);
            }
        }


        internal static bool find_affect(out Affect affect, Affects affect_type, Player playerBase)
        {
            bool found_one_yet = false;
            affect = playerBase.affect_ptr;

            while (affect != null &&
                   found_one_yet == false)
            {
                if (affect.type == affect_type)
                {
                    found_one_yet = true;
                }
                else
                {
                    affect = affect.next;
                }
            }

            return found_one_yet;
        }

        internal static bool find_affect(Affects affect_type, Player player)
        {
            bool found_one = false;
            Affect affect = player.affect_ptr;

            while (affect != null &&
                   found_one == false)
            {
                if (affect.type == affect_type)
                {
                    found_one = true;
                }
                else
                {
                    affect = affect.next;
                }
            }

            return found_one;
        }

        static Set unk_683B5 = new Set(0x0001, new byte[] { 0x03 });

        internal static void damage_player(int damage, Player player)
        {
            int neg_hp = 0;
            int new_hp = 0;

            if (player.hit_point_current >= damage)
            {
                new_hp = player.hit_point_current - damage;
            }
            else
            {
                neg_hp = damage - player.hit_point_current;
            }

            if (neg_hp > 9 ||
                (new_hp == 0 && player.health_status == Status.animated))
            {
                player.health_status = Status.dead;
            }
            else
            {
                if (neg_hp > 0)
                {
                    player.health_status = Status.dying;

                    if (gbl.game_state == 5)
                    {
                        player.actions.bleeding = neg_hp;
                    }
                }
                else if (new_hp == 0)
                {
                    player.health_status = Status.unconscious;
                }
            }

            if (unk_683B5.MemberOf((byte)player.health_status) == false)
            {
                player.in_combat = false;
                player.hit_point_current = 0;

                if (gbl.game_state == 5)
                {
                    if (player.combat_team == CombatTeam.Ours)
                    {
                        gbl.friends_count--;
                    }
                    else
                    {
                        gbl.foe_count--;
                    }

                    player.actions.delay = 0;
                }
            }
            else
            {
                player.hit_point_current = (byte)new_hp;
            }
        }


        internal static void describeHealing(Player player) /* sub_684F7 */
        {
            string text;

            if (player.hit_point_current == player.hit_point_max)
            {
                text = "is fully healed";
            }
            else
            {
                text = "is partially healed";
            }

            DisplayPlayerStatusString(true, 10, text, player);

            if (gbl.game_state != 5)
            {
                Player_Summary(gbl.player_ptr);
            }
        }


        internal static CombatTeam opposite_team(Player player) /* on_our_team */
        {
            return (player.combat_team == CombatTeam.Ours) ? CombatTeam.Enemy : CombatTeam.Ours;
        }


        internal static void count_teams()
        {
            gbl.friends_count = 0;
            gbl.foe_count = 0;

            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.in_combat == true)
                {
                    if (player.combat_team == CombatTeam.Ours)
                    {
                        gbl.friends_count++;
                    }
                    else
                    {
                        gbl.foe_count++;
                    }
                }

                player = player.next_player;
            }
        }


        internal static int near_enemy(int max_range, Player player) /*near_enermy*/
        {
            int ret_val;

            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile,
                ovr033.PlayerMapSize(player), 0xff, max_range,
                ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

            if (gbl.sortedCombatantCount > 0)
            {
                int tmpCount = 0;

                for (int i = 1; i <= gbl.sortedCombatantCount; i++)
                {
                    Player tmp = gbl.player_array[gbl.SortedCombatantList[i].player_index];
                    //int steps = gbl.SortedCombatantList[i].steps;
                    //int index = gbl.SortedCombatantList[i].player_index;
					
                    if (tmp.combat_team == opposite_team(player))
                    {
                        tmpCount++;
                        gbl.SortedCombatantList[tmpCount] = new SortedCombatant(gbl.SortedCombatantList[i]);
                    }
                }

                gbl.sortedCombatantCount = tmpCount;
            }

            ret_val = gbl.sortedCombatantCount;
            for (int i = 1; i <= gbl.sortedCombatantCount; i++)
            {
                gbl.near_targets[i] = gbl.SortedCombatantList[i].player_index;
            }

            return ret_val;
        }


        internal static int getTargetRange(Player target, Player attacker) /* sub_68708 */
        {
            // Backup previous sort
            SortedCombatant[] backupList = new SortedCombatant[gbl.MaxSortedCombatantCount];
            System.Array.Copy(gbl.SortedCombatantList, backupList, gbl.MaxSortedCombatantCount);

            gbl.mapToBackGroundTile.field_6 = true;

            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile,
                ovr033.PlayerMapSize(attacker), 0xff, 0xff,
                ovr033.PlayerMapYPos(attacker),
                ovr033.PlayerMapXPos(attacker));

            gbl.mapToBackGroundTile.field_6 = false;

            int i = 0;
            while (gbl.player_array[gbl.SortedCombatantList[i].player_index] != target &&
                i < (gbl.sortedCombatantCount - 1))
            {
                i++;
            }

            int range = gbl.SortedCombatantList[i].steps / 2;

            // Restore previous sorting
            System.Array.Copy(backupList, gbl.SortedCombatantList, gbl.MaxSortedCombatantCount);
            return range;
        }


        internal static bool clear_actions(Player player)
        {
            player.actions.delay = 0;
            player.actions.spell_id = 0;
            player.actions.guarding = false;
            player.actions.move = 0;

            return true;
        }


        internal static bool guarding(Player player)
        {
            bool ret_val = clear_actions(player);
            player.actions.guarding = true;

            string_print01("Guarding");

            return ret_val;
        }


        internal static int spell_target_count(int spell_id) /* sub_6886F */
        {
            int target_count = 0;

            if (spell_id == 0)
            {
                return 0;
            }

            if (gbl.player_ptr.cleric_lvl == 0 &&
                gbl.player_ptr.magic_user_lvl == 0 &&
                gbl.player_ptr.paladin_lvl < 9 &&
                gbl.player_ptr.ranger_lvl < 8)
            {
                target_count = 6;
            }
            else
            {
                sbyte spell_class = gbl.spell_table[spell_id].spellClass;
                if (spell_class == 0)
                {
                    int var_2 = gbl.player_ptr.cleric_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.turn_undead);
                    int var_3 = gbl.player_ptr.paladin_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_114) - 8;

                    if (var_2 > var_3)
                    {
                        target_count = var_2;
                    }
                    else
                    {
                        target_count = var_3;
                    }
                }
                else if (spell_class == 1)
                {
                    int var_2 = gbl.player_ptr.ranger_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_115) - 7;
                    if (var_2 > 0)
                    {
                        target_count = var_2;
                    }
                    else
                    {
                        target_count = 0;
                    }
                }
                else if (spell_class == 2)
                {
                    int var_2 = gbl.player_ptr.magic_user_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_116);
                    int var_3 = gbl.player_ptr.ranger_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_115) - 8;

                    if (var_2 > var_3)
                    {
                        target_count = var_2;
                    }
                    else
                    {
                        target_count = var_3;
                    }
                }
                else if (spell_class == 3)
                {
                    target_count = 12;
                }
            }

            if (gbl.spell_from_item == true &&
                gbl.spell_table[spell_id].spellClass != 3)
            {
                target_count = 6;
            }

            return target_count;
        }


        internal static void load_pic()
        {
            gbl.can_draw_bigpic = true;

            switch (gbl.game_state)
            {
                case 0:
                    seg037.draw8x8_outer_frame();
                    break;

                case 1:
                    if (gbl.byte_1EE7E == true)
                    {
                        seg037.draw8x8_03();
                    }

                    if (gbl.lastDaxBlockId == 0x50)
                    {
                        ovr030.sub_7000A(gbl.byte_1D556.frames[0].picture, true, 3, 3);
                    }
                    else
                    {
                        ovr030.head_body(gbl.body_block_id, gbl.head_block_id);
                        ovr030.draw_head_and_body(true, 3, 3);
                    }

                    Player_Summary(gbl.player_ptr);
                    display_map_position_time();
                    break;

                case 2:
                    seg037.draw8x8_03();
                    ovr030.load_pic_final(ref gbl.byte_1D556, 0, 0x1d, "PIC");
                    Player_Summary(gbl.player_ptr);
                    display_map_position_time();
                    break;

                case 4:
                    seg037.draw8x8_03();
                    ovr029.update_3D_view();
                    Player_Summary(gbl.player_ptr);
                    display_map_position_time();
                    gbl.byte_1EE98 = 0;
                    break;

                case 3:
                    if (gbl.lastDaxBlockId != 0x50)
                    {
                        ovr029.update_3D_view();
                    }
                    break;

                case 6:
                    seg037.draw8x8_03();
                    ovr030.load_pic_final(ref gbl.byte_1D556, 0, 1, "PIC");
                    Player_Summary(gbl.player_ptr);
                    break;
            }
        }

        static string direction(int dir)
        {
            switch (dir)
            {
                case 0: return "N";
                case 1: return "NE";
                case 2: return "E";
                case 3: return "SE";
                case 4: return "S";
                case 5: return "SW";
                case 6: return "W";
                case 7: return "NW";
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }


        internal static void display_map_position_time() // camping_search
        {
            if (gbl.game_state != 3)
            {
                string output = string.Empty;

                string minutes = gbl.area_ptr.time_hour.ToString("00");
                string hours = ((gbl.area_ptr.time_minutes_tens * 10) + gbl.area_ptr.time_minutes_ones).ToString("00");

                if (gbl.area_ptr.block_area_view == 0 ||
                    Cheats.always_show_areamap)
                {
                    output = string.Format("{0},{1} ", gbl.mapPosX, gbl.mapPosY);
                }

                output += direction(gbl.mapDirection) + " " + minutes + ":" + hours;

                if (gbl.printCommands == true)
                {
                    output += "*";
                }

                if (gbl.game_state == 2)
                {
                    output += " camping";
                }
                else if ((gbl.area2_ptr.search_flags & 1) > 0)
                {
                    output += " search";
                }

                seg037.draw8x8_clear_area(15, 0x26, 15, 17);

                seg041.displayString(output, 0, 10, 15, 17);
            }
        }


        internal static void sub_68DC0()
        {
            ovr033.Color_0_8_inverse();
            seg037.draw8x8_06();

            ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopY + 3, gbl.mapToBackGroundTile.mapScreenLeftX + 3);
        }

        static Set unk_68DFA = new Set(0x010A, new byte[] { 0x20, 0, 8, 0, 0, 0, 0, 0x20, 0, 8 });


        internal static void selectAPlayer(ref Player player, bool showExit, string prompt)
        {
            string text = showExit ? " Exit" : string.Empty;

            char input_key = ' ';
            while (unk_68DFA.MemberOf(input_key) == false)
            {
                Player_Summary(player);

                bool useOverlay = (gbl.game_state == 2 || gbl.game_state == 6);
                bool special_key;

                input_key = ovr027.displayInput(out special_key, useOverlay, 1, 15, 10, 13, "Select" + text, prompt + " ");
                Player player_ptr = player;

                if (special_key == true)
                {
                    if (input_key == 'O')
                    {
                        player_ptr = player_ptr.next_player;

                        if (player_ptr == null)
                        {
                            player_ptr = gbl.player_next_ptr;
                        }
                    }
                    else if (input_key == 'G')
                    {
                        player_ptr = gbl.player_next_ptr;

                        if (player != gbl.player_next_ptr)
                        {
                            while (player_ptr.next_player != player)
                            {
                                player_ptr = player_ptr.next_player;
                            }
                        }
                        else
                        {
                            while (player_ptr.next_player != null)
                            {
                                player_ptr = player_ptr.next_player;
                            }
                        }
                    }
                }
                else if (showExit == true)
                {
                    if (input_key == 'E' ||
                        input_key == 0)
                    {
                        player_ptr = null;
                    }
                }

                player = player_ptr;
            }
        }


        internal static bool item_is_ranged(Item item)
        {
            return item != null &&
                gbl.unk_1C020[item.type].field_C > 1;
        }

        internal static bool item_is_ranged_melee(Item item)
        {
            return item_is_ranged(item) &&
                 (gbl.unk_1C020[item.type].field_E & 0x14) == 0x14;
        }

        internal static bool is_weapon_ranged(Player player) /* offset_above_1 */
        {
            return item_is_ranged(player.field_151);
        }


        internal static bool is_weapon_ranged_melee(Player player) /* offset_equals_20 */
        {
            return item_is_ranged_melee(player.field_151);
        }


        internal static bool sub_6906C(out Item found_item, Player player)
        {
            found_item = null;
            byte flags = 0;

            Item item = player.field_151;
            if (item != null)
            {
                flags = gbl.unk_1C020[item.type].field_E;

                if ((flags & 0x10) != 0)
                {
                    found_item = item;
                }

                if ((flags & 0x08) != 0)
                {
                    if ((flags & 0x01) != 0)
                    {
                        found_item = player.Item_ptr_03;
                    }

                    if ((flags & 0x80) != 0)
                    {
                        found_item = player.Item_ptr_04;
                    }
                }
            }

            bool item_found = (found_item != null || flags == 10);

            return item_found;
        }


        internal static bool sub_69138(int skill, Player player)
        {
            bool ret_val = false;

            int skill_lvl = player.class_lvls[skill];

            if (skill_lvl > 0)
            {
                switch (player.race)
                {
                    case Race.dwarf:
                        if (skill == 2)
                        {
                            if ((skill_lvl == 8 && player.strength == 17) ||
                                (skill_lvl == 7 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.elf:
                        if (skill == 2)
                        {
                            if ((skill_lvl == 7) ||
                                (skill_lvl == 6 && player.strength == 17) ||
                                (skill_lvl == 5 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.gnome:
                        if (skill == 2)
                        {
                            if ((skill_lvl == 6) ||
                                (skill_lvl == 5 && player.strength < 18))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.half_elf:
                        if (skill == 0 &&
                            skill_lvl == 5)
                        {
                            ret_val = true;
                        }
                        else if (skill == 2)
                        {
                            if (skill_lvl == 8 ||
                                (skill_lvl == 7 && player.strength == 17) ||
                                (skill_lvl == 6 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.halfling:
                        if (skill == 2)
                        {
                            if ((skill_lvl == 6) ||
                                (skill_lvl == 5 && player.strength == 17) ||
                                (skill_lvl == 4 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;
                }
            }

            return ret_val;
        }


        internal static Item new_Item(Affects arg_0, Affects arg_2, Affects arg_4, short arg_6, byte arg_8,
            short arg_A, bool cursed, byte arg_E, bool readied, byte arg_12, sbyte exp_value, byte arg_16,
            sbyte arg_18, sbyte arg_1A, byte type)
        {
            Item item = new Item();

            item.name = string.Empty;
            item.next = null;
            item.type = type;
            item.field_2F = arg_1A;
            item.field_30 = arg_18;
            item.field_31 = arg_16;
            item.plus = exp_value;
            item.plus_save = arg_12;
            item.readied = readied;
            item.hidden_names_flag = arg_E;
            item.cursed = cursed;
            item.weight = arg_A;
            item.count = arg_8;
            item._value = arg_6;
            item.affect_1 = arg_4;
            item.affect_2 = arg_2;
            item.affect_3 = arg_0;

            return item;
        }


        internal static bool bandage(bool bandage_flag)
        {
            bool someone_bleeding = false;

            Player player_ptr = gbl.player_next_ptr;

            while (player_ptr != null)
            {
                if (player_ptr.actions.field_13 == 0 &&
                    player_ptr.combat_team == CombatTeam.Ours &&
                    player_ptr.health_status == Status.dying)
                {
                    someone_bleeding = true;

                    if (bandage_flag == true)
                    {
                        player_ptr.health_status = Status.unconscious;
                        player_ptr.actions.bleeding = 0;

                        DisplayPlayerStatusString(true, 10, "is bandaged", player_ptr);

                        bandage_flag = false;
                    }
                }
                player_ptr = player_ptr.next_player;
            }

            return someone_bleeding;
        }
    }
}
