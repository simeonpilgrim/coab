using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr025
    {
        internal static void CalculateAttackValues(Player player) // sub_66023
        {
            Item item = player.field_151;

            if (item != null)
            {
                uint item_type = item.type;

                player.hitBonus = player.thac0;

                if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.flag_02) != 0)
                {
                    player.hitBonus += DexReactionAdj(player);
                }

                player.attack1_DamageBonus = gbl.ItemDataTable[item_type].bonusNormal;

                if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.melee) != 0)
                {
                    player.hitBonus += strengthHitBonus(player);
                    player.attack1_DamageBonus += strengthDamBonus(player);
                }

                int bonus = item.plus;

                if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.quarrels) != 0 &&
                    player.quarrels != null)
                {
                    bonus += player.quarrels.plus;
                }

                if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.arrows) != 0 &&
                    player.arrows != null)
                {
                    bonus += player.arrows.plus;
                }

                player.attack1_DamageBonus += (sbyte)bonus;

                if (player.race == Race.elf &&
                    (item.type == 41 || // CompositeLongBow
                     item.type == 42 || // CompositeShortBow
                     item.type == 43 || // LongBow
                     item.type == 44 || // ShortBow
                     item.type == 37 || // ShortSword
                     item.type == 36)) // LongSword
                {
                    bonus++;
                }

                player.hitBonus += bonus;
                player.attack1_DiceCount = gbl.ItemDataTable[item_type].diceCountNormal;
                player.attack1_DiceSize = gbl.ItemDataTable[item_type].diceSizeNormal;
            }
        }


        internal static void CalcArmorWeightEffect(Item item, Player player) // sub_6621E
        {
            if (gbl.ItemDataTable[item.type].item_slot == 2) // Armor
            {
                if (item.weight >= 0 && item.weight <= 150)
                {
                    player.movement = player.base_movement;
                }
                else if (item.weight >= 151 && item.weight <= 399)
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
            byte var_1 = gbl.ItemDataTable[item.type].field_6;
            if (var_1 > 0x7f)
            {
                var_1 &= 0x7F;
                byte var_2 = gbl.ItemDataTable[item.type].item_slot;
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
                            bonus[3] = (sbyte)item.plus;
                        }
                    }
                    else
                    {
                        bonus[2] += (sbyte)(item.plus);
                    }

                    player.field_186 += (sbyte)item.plus_save;
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


        public static string[] itemNames = { "",
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


        internal static void ItemDisplayNameBuild(bool display_new_name, bool displayReadied, 
            int yCol, int xCol, Item item) /*id_item*/
        {
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

            bool detectMagic = gbl.player_next_ptr.Exists(pla => pla.HasAffect(Affects.detect_magic));
      
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
                hidden_names_flag = 0;
            }

            item.name += ItemName(item, hidden_names_flag);

            if (display_new_name)
            {
                seg041.displayString(item.name, 0, 10, yCol, xCol);
            }
        }


        internal static string ItemName(Item item, int hidden_names_flag)
        {
            int display_flags = 0;
            display_flags |= (item.namenum1 != 0 && (hidden_names_flag & 0x4) == 0) ? 0x1 : 0;
            display_flags |= (item.namenum2 != 0 && (hidden_names_flag & 0x2) == 0) ? 0x2 : 0;
            display_flags |= (item.namenum3 != 0 && (hidden_names_flag & 0x1) == 0) ? 0x4 : 0;

            bool pural_added = false;
            string name = "";

            for (int var_1 = 3; var_1 >= 1; var_1--)
            {
                if (((display_flags >> (var_1 - 1)) & 1) > 0)
                {
                    name += itemNames[item.field_2EArray(var_1)];

                    if (item.count < 2 ||
                        pural_added == true)
                    {
                        name += " ";
                    }
                    else if ((1 << (var_1 - 1) == display_flags) ||
                            (var_1 == 1 && display_flags > 4 && item.type != 86) ||
                            (var_1 == 2 && (display_flags & 1) == 0) ||
                            (var_1 == 3 && item.type == 86) ||
                            (item.namenum3 != 0x87 && (item.type == 73 /*Arrow*/ || item.type == 28 /*Quarrel*/ || item.type == 9 /*Dart*/) && item.namenum3 != 0xb1))
                    {
                        name += "s ";
                        pural_added = true;
                    }
                    else
                    {
                        name += " ";
                    }
                }
            }

            return name;
        }

        internal static void PartySummary(Player player)
        {
            if (gbl.game_state == GameState.WildernessMap)
            {
                return;
            }

            int x_pos = (gbl.game_state == GameState.State0) ? 1 : 17;
            int y_pos = 2;

            seg041.displayString("Name", 0, 15, y_pos, x_pos);
            seg041.displayString("AC  HP", 0, 15, y_pos, 0x21);

            y_pos += 2;

            foreach (Player tmp_player in gbl.player_next_ptr)
            {
                seg037.draw8x8_clear_area(y_pos, 0x26, y_pos, x_pos);

                if (tmp_player == player)
                {
                    seg041.displayString(tmp_player.name, 0, 15, y_pos, x_pos);
                }
                else
                {
                    displayPlayerName(false, y_pos, x_pos, tmp_player);
                }

                int ac_x_pos;
                if (tmp_player.ac >= 0 && tmp_player.ac <= 0x32)
                {
                    ac_x_pos = 1;
                }
                else if (tmp_player.ac >= 0x33 && tmp_player.ac <= 0x3C)
                {
                    ac_x_pos = 2;
                }
                else if (tmp_player.ac >= 0x3D && tmp_player.ac <= 0x45)
                {
                    ac_x_pos = 1;
                }
                else
                {
                    ac_x_pos = 0;
                }

                display_AC(y_pos, ac_x_pos + 0x1F /*+0x20*/, tmp_player);

                if (tmp_player.hit_point_current >= 0 && tmp_player.hit_point_current <= 9)
                {
                    ac_x_pos = 2;
                }
                else if (tmp_player.hit_point_current >= 10 && tmp_player.hit_point_current <= 99)
                {
                    ac_x_pos = 1;
                }
                else
                {
                    ac_x_pos = 0;
                }

                display_hp(false, y_pos, ac_x_pos + 0x24, tmp_player);
                y_pos++;
            }

            //seg037.draw8x8_clear_area(y_pos, 0x26, y_pos, x_pos);
        }


        internal static void display_AC(int y_offset, int x_offset, Player player)
        {
            string text = (0x3c - (int)player.ac).ToString();

            seg041.displayString(text, 0, 10, y_offset, x_offset);
        }


        internal static void display_hp(bool hightlighted, int y_pos, int x_pos, Player player)
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

            if (hightlighted == true)
            {
                colour = 0x0D;
            }

            seg041.displayString(player.hit_point_current.ToString(), 0, colour, y_pos, x_pos);
        }


        internal static void CombatDisplayPlayerSummary(Player player) /* hitpoint_ac */
        {
            if (gbl.display_hitpoints_ac == true)
            {
                gbl.display_hitpoints_ac = false;
                seg037.draw8x8_clear_area(TextRegion.CombatSummary);

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
                    ItemDisplayNameBuild(false, false, 0, 0, player.field_151);

                    seg041.press_any_key(player.field_151.name, true, 0, 10, line + 3, 0x26, line + 1, 0x17);
                }

                line = gbl.textYCol + 1;

                if (player.in_combat == false)
                {
                    seg041.displayString(ovr020.statusString[(int)player.health_status], 0, 15, line + 1, 0x17);
                }
                else if (player.IsHeld() == true)
                {
                    seg041.displayString("(Helpless)", 0, 15, line + 1, 0x17);
                }
            }
        }



        internal static void reclac_player_values(Player player) /* sub_66C20 */
        {
            sbyte[] stat_bonus = new sbyte[5];

            player.field_151 = null;
            player.field_155 = null;
            player.armor = null;
            player.field_15D = null;
            player.field_161 = null;
            player.field_165 = null;
            player.field_169 = null;
            player.field_16D = null;
            player.field_171 = null;
            player.Item_ptr_01 = null;
            player.Item_ptr_02 = null;
            player.arrows = null;
            player.quarrels = null;

            bool var_8 = false;

            player.field_185 = 0;

            player.weight = 0;
            int totalItemWeight = 0;

            foreach (var item in player.items)
            {
                short item_weight = item.weight;

                if (item.count > 0)
                {
                    item_weight *= (short)item.count;
                }

                player.weight += item_weight;

                if (item.readied)
                {
                    totalItemWeight += item_weight;

                    int var_13 = gbl.ItemDataTable[item.type].item_slot;

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

                    if (item.type == 73) // Arrow
                    {
                        player.arrows = item;
                    }

                    if (item.type == 28) // Quarrel
                    {
                        player.quarrels = item;
                    }

                    player.field_185 += gbl.ItemDataTable[item.type].handsCount;
                }
            }


            for (int money = 0; money < 7; money++)
            {
                player.weight += player.Money[money];
            }

            player.attack1_DiceCount = player.attack1_DiceCountBase;
            player.attack2_DiceCount = player.attack2_DiceCountBase;

            player.attack1_DiceSize = player.attack1_DiceSizeBase;
            player.attack2_DiceSize = player.attack2_DiceSizeBase;

            player.attack1_DamageBonus = player.attack1_DamageBonusBase;
            player.attack2_DamageBonus = player.attack2_DamageBonusBase;


            for (int i = 0; i <= 4; i++)
            {
                stat_bonus[i] = 0;
            }

            byte var_7 = 0;

            player.field_186 = 0;
            player.ac = player.base_ac;
            player.movement = player.base_movement;
            player.hitBonus = player.thac0;

            stat_bonus[0] = ovr025.DexAcBonus(player);

            if (player.field_151 == null)
            {
                player.hitBonus += strengthHitBonus(player);
                player.attack1_DamageBonus += strengthDamBonus(player);
            }

            CalculateAttackValues(player);

            foreach (var item in player.items)
            {
                if (item.readied)
                {
                    CalcArmorWeightEffect(item, player);
                    sub_662A6(ref var_7, ref stat_bonus, item, player);
                }
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

                if (player.weight < totalItemWeight)
                {
                    player.weight = (short)totalItemWeight;
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
                (player.fighter_old_lvl > 0 && ovr026.MulticlassExceedLastLevel(player) != 0)) &&
                player.race > Race.monster)
            {
                player.field_DD = (byte)((player.fighter_old_lvl * ovr026.MulticlassExceedLastLevel(player)) + player.fighter_lvl);
            }
            else
            {
                player.field_DD = 1;
            }

            System.Console.WriteLine("{0} {1} {2}, {3} {4}, {5} {6}", player.name, player.attack1_DiceCount, player.attack2_DiceCount,
       player.attack1_DiceSize, player.attack2_DiceSize, player.attack1_DamageBonus, player.attack2_DamageBonus);

        }


        internal static sbyte DexAcBonus(Player player) /* stat_bonus */
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


        internal static int DexReactionAdj(Player player)
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


        internal static int strengthHitBonus(Player player)
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

            return str_bonus;
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





        internal static void lose_item(Item item, Player player)
        {
            if (!player.items.Remove(item))
            {
                seg041.displayAndDebug("Tried to Lose item & couldn't find it!", 0, 14);
            }
        }


        internal static void string_print01(string arg_0)
        {
            ovr027.ClearPromptAreaNoUpdate();

            seg041.displayString(arg_0, 0, 10, 0x18, 0);

            seg041.GameDelay();

            ovr027.ClearPromptAreaNoUpdate();
        }


        internal static void DisplayPlayerStatusString(bool clearDisplay, int lineY, string text, Player player) /* sub_67788 */
        {
            if (gbl.game_state == GameState.Combat)
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
            if (gbl.game_state == GameState.Combat)
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
            int color;

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

            string name = player.name + ((pural)? "'s" : "");

            seg041.displayString(name, 0, color, y_offset, x_offset);
        }


        internal static void load_missile_dax(bool flipIcon, byte iconOffset, int iconAction, int iconIdx) /* sub_67924 */
        {
            int dataSize = gbl.missile_dax.bpp;

            if (flipIcon == true)
            {
                seg040.FlipIconLeftToRight(gbl.combat_icons[iconIdx, iconAction]);
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


        internal static void draw_missile_attack(int delay, int frameCount, Point target, Point attacker) /* sub_67AA4 */
        {
            Point center;
            bool var_B4;
            bool var_B3;
            SteppingPath path = new SteppingPath();
            byte[] pathDir = new byte[0x94];


            seg051.FillChar(8, 0x94, pathDir);

            int var_AF = 0;
            int frame = 0;

            path.attacker = attacker * 3;
            path.target = target * 3;

            path.CalculateDeltas();

            do
            {
                var_B4 = !path.Step();

                pathDir[var_AF] = path.direction;

                var_AF++;
            } while (var_B4 == false);

            int var_B0 = var_AF - 2;

            if (var_B0 < 2 || var_AF < 2)
            {
                return;
            }

            var diff = target - attacker;

            if (ovr033.CoordOnScreen(attacker - gbl.mapToBackGroundTile.mapScreenTopLeft) == false ||
                ovr033.CoordOnScreen(target - gbl.mapToBackGroundTile.mapScreenTopLeft) == false)
            {
                if (System.Math.Abs(diff.x) <= 6 &&
                    System.Math.Abs(diff.y) <= 6)
                {
                    var_B3 = true;
                    center = (diff / 2) + attacker;
                }
                else
                {
                    var_B3 = false;
                    center = gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter;
                }
            }
            else
            {
                var_B3 = true;
                center = gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter;
            }

            ovr033.redrawCombatArea(8, 0xFF, center);
            var_AF = 0;
            Point delta = new Point(0,0);

            do
            {
                Point cur = ((attacker - gbl.mapToBackGroundTile.mapScreenTopLeft) * 3) + delta;

                var_B4 = false;

                do
                {
                    Point var_C6 = gbl.MapDirectionDelta[pathDir[var_AF]];
                    cur += var_C6;

                    if (delay > 0 ||
                        (cur.x % 3) == 0 ||
                        (cur.y % 3) == 0)
                    {
                        Display.SaveVidRam();
                        seg040.OverlayBounded(gbl.missile_dax, 5, frame, cur.y, cur.x);
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
                    if (cur.x < 0 || cur.x > 0x12 || cur.y < 0 || cur.y > 0x12)
                    {
                        var_B4 = true;
                    }

                    if (var_B4 == false &&
                        var_AF < var_B0)
                    {
                        delta += var_C6;

                        if (Math.Abs(delta.x) == 3)
                        {
                            attacker.x += Math.Sign(delta.x);
                            center.x += Math.Sign(delta.x);
                            delta.x = 0;
                        }

                        if (Math.Abs(delta.y) == 3)
                        {
                            attacker.y += Math.Sign(delta.y);
                            center.y += Math.Sign(delta.y);
                            delta.y = 0;
                        }
                    }

                } while (var_AF < var_B0 && var_B4 == false);

                if (var_AF < var_B0)
                {
                    int var_CE = 0;
                    int var_D0 = 0;
                    delta = new Point(0,0);
                    attacker = target;

                    if ((target.x + Point.ScreenHalfX) > Point.MapMaxX)
                    {
                        var_CE = target.x - Point.MapMaxX;

                    }
                    else if (target.x < Point.ScreenHalfX)
                    {
                        var_CE = Point.ScreenHalfX - target.x;
                    }

                    if ((target.y + Point.ScreenHalfY) > Point.MapMaxY)
                    {
                        var_D0 = target.y - Point.MapMaxY;
                    }
                    else if (target.y < Point.ScreenHalfY)
                    {
                        var_D0 = Point.ScreenHalfY - target.y;
                    }

                    center.x = target.x + var_CE;
                    center.y = target.y + var_D0;

                    ovr033.redrawCombatArea(8, 0xff, center);
                    cur = (target - gbl.mapToBackGroundTile.mapScreenTopLeft) * 3;
                    var_AF = var_B0;
                    var_B4 = false;

                    do
                    {
                        Point var_C6 = new Point(0, 0) - gbl.MapDirectionDelta[pathDir[var_AF]];

                        cur += var_C6;

                        if (cur.x > 18)
                        {
                            attacker.x = gbl.mapToBackGroundTile.mapScreenTopLeft.x + Point.ScreenMaxX;
                        }
                        else if (cur.x < 0)
                        {
                            attacker.x = gbl.mapToBackGroundTile.mapScreenTopLeft.x;
                        }

                        if (cur.y > 18)
                        {
                            attacker.y = gbl.mapToBackGroundTile.mapScreenTopLeft.y + Point.ScreenMaxY;
                        }
                        else if (cur.y < 0)
                        {
                            attacker.y = gbl.mapToBackGroundTile.mapScreenTopLeft.y;
                        }

                        if (cur.x < 0 || cur.x > 18 || cur.y < 0 || cur.y > 18)
                        {
                            var_B4 = true;
                        }

                        if (var_B4 == false)
                        {
                            delta += var_C6;

                            if (System.Math.Abs(delta.x) == Point.ScreenHalfX)
                            {
                                attacker.x += Math.Sign(delta.x);
                                center.x += Math.Sign(delta.x);
                                delta.x = 0;
                            }

                            if (System.Math.Abs(delta.y) == Point.ScreenHalfY)
                            {
                                attacker.y += Math.Sign(delta.y);
                                center.y += Math.Sign(delta.y);
                                delta.y = 0;
                            }

                            var_AF -= 1;
                        }

                    } while (var_B4 == false);
                }
                else
                {
                    var_B3 = true;

                    if (ovr033.CoordOnScreen(target - gbl.mapToBackGroundTile.mapScreenTopLeft) == false)
                    {
                        ovr033.redrawCombatArea(8, 3, target);
                    }

                    cur = (target - gbl.mapToBackGroundTile.mapScreenTopLeft) * 3;

                    Display.SaveVidRam();
                    seg040.OverlayBounded(gbl.missile_dax, 5, frame, cur.y, cur.x);

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


        internal static void MagicAttackDisplay(string text, bool showMagicStars, Player player) // sub_6818A
        {
            if (gbl.game_state == GameState.Combat)
            {
                int iconId = showMagicStars ? 0x16 : 0x17;

                load_missile_icons(iconId);

                if (ovr033.PlayerOnScreen(true, player) == false)
                {
                    ovr033.redrawCombatArea(8, 3, ovr033.PlayerMapPos(player));
                }

                if (showMagicStars)
                {
                    seg044.sound_sub_120E0(Sound.sound_4);
                }
                else
                {
                    seg044.sound_sub_120E0(Sound.sound_3);
                }

                DisplayPlayerStatusString(false, 10, text, player);

                int idx = ovr033.GetPlayerIndex(player);
                Point pos = gbl.CombatMap[idx].screenPos * 3; 

                int loops = showMagicStars ? gbl.game_speed_var : 0;
                for (int loop = 0; loop <= loops; loop++)
                {
                    for (int frame = 0; frame <= 3; frame++)
                    {
                        Display.SaveVidRam();

                        seg040.OverlayBounded(gbl.missile_dax, 5, frame, pos.y, pos.x);
                        seg040.DrawOverlay();

                        seg049.SysDelay(70);

                        Display.RestoreVidRam();
                    }
                }

                seg040.DrawOverlay();

                if (loops == 0)
                {
                    seg041.GameDelay();
                }
            }
            else
            {
                DisplayPlayerStatusString(true, 10, text, player);
            }
        }


        internal static bool FindAffect(out Affect affectFound, Affects affect_type, Player player)
        {
            affectFound = player.affects.Find(aff => aff.type == affect_type);

            return affectFound != null;
        }


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

                    if (gbl.game_state == GameState.Combat)
                    {
                        player.actions.bleeding = neg_hp;
                    }
                }
                else if (new_hp == 0)
                {
                    player.health_status = Status.unconscious;
                }
            }

            if (player.health_status == Status.okey || 
                player.health_status == Status.animated)
            {
                player.hit_point_current = (byte)new_hp;
            }
            else
            {
                player.in_combat = false;
                player.hit_point_current = 0;

                if (gbl.game_state == GameState.Combat)
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
        }


        internal static void DescribeHealing(Player player) /* sub_684F7 */
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

            if (gbl.game_state != GameState.Combat)
            {
                PartySummary(gbl.player_ptr);
            }
        }


        internal static void CountCombatTeamMembers() // count_teams
        {
            gbl.friends_count = 0;
            gbl.foe_count = 0;

            foreach(Player player in gbl.player_next_ptr)
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
            }
        }


        internal static List<CombatPlayerIndex> BuildNearTargets(int max_range, Player player) /*near_enermy*/
        {
            var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, max_range, ovr033.PlayerMapPos(player));
            
            scl.RemoveAll(sc => sc.player.combat_team == player.combat_team);

            List<CombatPlayerIndex> nearTargets = new List<CombatPlayerIndex>();

            foreach (var sc in scl)
            {
                nearTargets.Add(new CombatPlayerIndex(sc.player, sc.pos));
            }

            return nearTargets;
        }


        internal static int getTargetRange(Player target, Player attacker) /* sub_68708 */
        {
            gbl.mapToBackGroundTile.field_6 = true;

            var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile,
                ovr033.PlayerMapSize(attacker), 0xff, 0xff, ovr033.PlayerMapPos(attacker));

            gbl.mapToBackGroundTile.field_6 = false;

            var combatant = scl.Find(sc => sc.player == target);

            if (combatant != null)
            {
                return combatant.steps / 2;
            }
            else
            {
                //Not sure what to return just yet...
                //return combatant.steps / 2; 
                return 0xFF;
            }
        }


        internal static void clear_actions(Player player)
        {
            player.actions.delay = 0;
            player.actions.spell_id = 0;
            player.actions.guarding = false;
            player.actions.move = 0;
        }


        internal static void guarding(Player player)
        {
            clear_actions(player);
            player.actions.guarding = true;

            string_print01("Guarding");
        }


        internal static int spellMaxTargetCount(int spell_id) /* sub_6886F */
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
                switch (gbl.spell_table[spell_id].spellClass)
                {
                    case SpellClass.Cleric:
                        int cleric_count = gbl.player_ptr.cleric_lvl + (ovr026.MulticlassExceedLastLevel(gbl.player_ptr) * gbl.player_ptr.cleric_old_lvl);
                        int paladin_count = gbl.player_ptr.paladin_lvl + (ovr026.MulticlassExceedLastLevel(gbl.player_ptr) * gbl.player_ptr.paladin_old_lvl) - 8;

                        target_count = Math.Max(cleric_count, paladin_count);
                        break;

                    case SpellClass.Druid:
                        int ranger_count = gbl.player_ptr.ranger_lvl + (ovr026.MulticlassExceedLastLevel(gbl.player_ptr) * gbl.player_ptr.ranger_old_lvl) - 7;
                        
                        target_count = Math.Max(ranger_count, 0);
                        break;

                    case SpellClass.MagicUser:
                        int magicuser_count = gbl.player_ptr.magic_user_lvl + (ovr026.MulticlassExceedLastLevel(gbl.player_ptr) * gbl.player_ptr.magic_user_old_lvl);
                        int ranger_count2 = gbl.player_ptr.ranger_lvl + (ovr026.MulticlassExceedLastLevel(gbl.player_ptr) * gbl.player_ptr.ranger_old_lvl) - 8;

                        target_count = Math.Max(magicuser_count, ranger_count2);
                        break;

                    case SpellClass.Monster:
                        target_count = 12;
                        break;
                }
            }

            if (gbl.spell_from_item == true &&
                gbl.spell_table[spell_id].spellClass != SpellClass.Monster)
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
                case GameState.State0:
                    seg037.DrawFrame_Outer();
                    break;

                case GameState.Shop:
                    if (gbl.redrawBoarder == true)
                    {
                        seg037.draw8x8_03();
                    }

                    if (gbl.lastDaxBlockId == 0x50)
                    {
                        ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
                    }
                    else
                    {
                        ovr030.head_body(gbl.body_block_id, gbl.head_block_id);
                        ovr030.draw_head_and_body(true, 3, 3);
                    }

                    PartySummary(gbl.player_ptr);
                    display_map_position_time();
                    break;

                case GameState.Camping:
                    seg037.draw8x8_03();
                    ovr030.load_pic_final(ref gbl.byte_1D556, 0, 0x1d, "PIC");
                    PartySummary(gbl.player_ptr);
                    display_map_position_time();
                    break;

                case GameState.State4:
                    seg037.draw8x8_03();
                    ovr029.RedrawView();
                    PartySummary(gbl.player_ptr);
                    display_map_position_time();
                    gbl.byte_1EE98 = false;
                    break;

                case GameState.WildernessMap:
                    if (gbl.lastDaxBlockId != 0x50)
                    {
                        ovr029.RedrawView();
                    }
                    break;

                case GameState.State6:
                    seg037.draw8x8_03();
                    ovr030.load_pic_final(ref gbl.byte_1D556, 0, 1, "PIC");
                    PartySummary(gbl.player_ptr);
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
            if (gbl.game_state != GameState.WildernessMap)
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

                if (gbl.game_state == GameState.Camping)
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


        internal static void RedrawCombatScreen() // sub_68DC0
        {
            ovr033.Color_0_8_inverse();
            seg037.DrawFrame_Combat();

            ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);
        }

        static Set unk_68DFA = new Set(0x010A, new byte[] { 0x20, 0, 8, 0, 0, 0, 0, 0x20, 0, 8 });


        internal static void selectAPlayer(ref Player player, bool showExit, string prompt)
        {
            string text = showExit ? " Exit" : "";

            char input_key = ' ';
            while (unk_68DFA.MemberOf(input_key) == false)
            {
                PartySummary(player);

                bool useOverlay = (gbl.game_state == GameState.Camping || gbl.game_state == GameState.State6);
                bool special_key;

                input_key = ovr027.displayInput(out special_key, useOverlay, 1, 15, 10, 13, "Select" + text, prompt + " ");

                int index = gbl.player_next_ptr.IndexOf(player);

                if (special_key == true)
                {
                    if (input_key == 'O')
                    {
                        //next
                        index = (index + 1) % gbl.player_next_ptr.Count;
                        player = gbl.player_next_ptr[index];
                    }
                    else if (input_key == 'G')
                    {
                        // previous
                        index = (index - 1 + gbl.player_next_ptr.Count) % gbl.player_next_ptr.Count;
                        player = gbl.player_next_ptr[index];
                    }
                }
                else if (showExit == true)
                {
                    if (input_key == 'E' ||
                        input_key == 0)
                    {
                        player = null;
                    }
                }
            }
        }


        internal static bool item_is_ranged(Item item)
        {
            return item != null &&
                gbl.ItemDataTable[item.type].range > 1;
        }

        internal static bool item_is_ranged_melee(Item item)
        {
            var rangedMelee = (ItemDataFlags.flag_10 | ItemDataFlags.melee);
            return item_is_ranged(item) &&
                 (gbl.ItemDataTable[item.type].field_E & rangedMelee) == rangedMelee;
        }

        internal static bool is_weapon_ranged(Player player) /* offset_above_1 */
        {
            return item_is_ranged(player.field_151);
        }


        internal static bool is_weapon_ranged_melee(Player player) /* offset_equals_20 */
        {
            return item_is_ranged_melee(player.field_151);
        }


        internal static bool sub_6906C(out Item found_item, Player player) // sub_6906C
        {
            found_item = null;
            var flags = ItemDataFlags.None;

            Item item = player.field_151;
            if (item != null)
            {
                flags = gbl.ItemDataTable[item.type].field_E;

                if ((flags & ItemDataFlags.flag_10) != 0)
                {
                    found_item = item;
                }

                if ((flags & ItemDataFlags.flag_08) != 0)
                {
                    if ((flags & ItemDataFlags.arrows) != 0)
                    {
                        found_item = player.arrows;
                    }

                    if ((flags & ItemDataFlags.quarrels) != 0)
                    {
                        found_item = player.quarrels;
                    }
                }
            }

            bool item_found = (found_item != null || flags == (ItemDataFlags.flag_08 | ItemDataFlags.flag_02));

            return item_found;
        }


        internal static bool RaceStatLevelRestricted(ClassId _class, Player player) // sub_69138
        {
            bool ret_val = false;

            int class_lvl = player.ClassLevel[(int)_class];

            if (class_lvl > 0)
            {
                switch (player.race)
                {
                    case Race.dwarf:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 8 && player.strength == 17) ||
                                (class_lvl == 7 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.elf:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 7) ||
                                (class_lvl == 6 && player.strength == 17) ||
                                (class_lvl == 5 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.gnome:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 6) ||
                                (class_lvl == 5 && player.strength < 18))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.half_elf:
                        if (_class == ClassId.cleric &&
                            class_lvl == 5)
                        {
                            ret_val = true;
                        }
                        else if (_class == ClassId.fighter)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.strength == 17) ||
                                (class_lvl == 6 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.halfling:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 6) ||
                                (class_lvl == 5 && player.strength == 17) ||
                                (class_lvl == 4 && player.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;
                }
            }

            return ret_val;
        }


        internal static bool bandage(bool applyBandage)
        {
            bool someoneBleeding = false;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.actions.nonTeamMember == false &&
                    player.combat_team == CombatTeam.Ours &&
                    player.health_status == Status.dying)
                {
                    someoneBleeding = true;

                    if (applyBandage == true)
                    {
                        player.health_status = Status.unconscious;
                        player.actions.bleeding = 0;

                        DisplayPlayerStatusString(true, 10, "is bandaged", player);

                        applyBandage = false;
                    }
                }
            }

            return someoneBleeding;
        }
    }
}
