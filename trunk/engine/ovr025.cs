using Classes;

namespace engine
{
    class ovr025
    {
        internal static void sub_66023(Player arg_0)
        {
            sbyte var_6;
            byte var_5;
            Item var_4;

            var_4 = arg_0.field_151;

            if (var_4 != null)
            {
                var_5 = var_4.type;

                arg_0.field_199 = arg_0.field_73;

                if ((gbl.unk_1C020[var_5].field_E & 2) != 0)
                {
                    arg_0.field_199 = (sbyte)(arg_0.field_199 + stat_bonus02(arg_0));
                }

                throw new System.NotSupportedException();//mov	al, gbl.unk_1C020[ var_5 ].field_B
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	es:[di+1A2h], al
                throw new System.NotSupportedException();//mov	al,gbl.unk_1C020[ var_5 ].field_E
                throw new System.NotSupportedException();//and	al, 4
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jz	loc_66103

                arg_0.field_199 = (sbyte)(arg_0.field_199 + strengthHitBonus(arg_0));
                arg_0.field_1A2 = (sbyte)(arg_0.field_1A2 + strengthDamBonus(arg_0));

                throw new System.NotSupportedException();//loc_66103:
                var_6 = var_4.exp_value;
                throw new System.NotSupportedException();//cmp	gbl.unk_1C020[ var_5 ].field_E, 0x7F
                throw new System.NotSupportedException();//jbe	loc_66146
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	ax, es:[di+181h]
                throw new System.NotSupportedException();//or	ax, es:[di+183h]
                throw new System.NotSupportedException();//jz	loc_66146
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//les	di, es:[di+181h]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_6]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//mov	[bp+var_6], al
                throw new System.NotSupportedException();//loc_66146:
                throw new System.NotSupportedException();//mov	al, gbl.unk_1C020[ var_5 ].field_E
                throw new System.NotSupportedException();//and	al, 1
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jbe	loc_66182
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	ax, es:[di+17Dh]
                throw new System.NotSupportedException();//or	ax, es:[di+17Fh]
                throw new System.NotSupportedException();//jz	loc_66182
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//les	di, es:[di+17Dh]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_6]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//mov	[bp+var_6], al
                throw new System.NotSupportedException();//loc_66182:
                throw new System.NotSupportedException();//mov	al, [bp+var_6]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	al, es:[di+1A2h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	es:[di+1A2h], al
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+74h], 2
                throw new System.NotSupportedException();//jnz	loc_661D0
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+2Eh], 0x28
                throw new System.NotSupportedException();//jbe	loc_661B9
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+2Eh], 0x2D
                throw new System.NotSupportedException();//jb	loc_661CD
                throw new System.NotSupportedException();//loc_661B9:
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+2Eh], 0x25
                throw new System.NotSupportedException();//jz	loc_661CD
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+2Eh], 0x24
                throw new System.NotSupportedException();//jnz	loc_661D0
                throw new System.NotSupportedException();//loc_661CD:
                var_6++;
                throw new System.NotSupportedException();//loc_661D0:
                throw new System.NotSupportedException();//mov	al, [bp+var_6]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	al, es:[di+199h]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	es:[di+199h], al
                throw new System.NotSupportedException();//mov	al, gbl.unk_1C020[ var_5 ].field_9
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	es:[di+19Eh], al
                throw new System.NotSupportedException();//mov	al, gbl.unk_1C020[ var_5 ].field_A
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//mov	es:[di+1A0h], al
            }
            //func_end:
        }

        /// <summary>
        /// Item weight initiative effect check.
        /// </summary>
        internal static void sub_6621E(Item arg_0, Player arg_4)
        {
            if (gbl.unk_1C020[arg_0.type].field_0 == 2)
            {
                if (arg_0.weight >= 0 && arg_0.weight <= 0x96)
                {
                    arg_4.initiative = arg_4.field_E4;
                }
                else if (arg_0.weight >= 0x97 && arg_0.weight <= 0x18F)
                {
                    arg_4.initiative = 9;
                }
                else
                {
                    arg_4.initiative = 6;
                }

                if (arg_4.initiative != 0 && arg_4.initiative <= 9)
                {
                    arg_4.initiative += 3;
                }
            }
        }


        internal static void sub_662A6(ref byte output, ref sbyte[] bonus, Item item, Player player)
        {
            byte var_2;
            byte var_1;

            var_1 = gbl.unk_1C020[item.type].field_6;
            if (var_1 > 0x7f)
            {
                var_1 &= 0x7F;
                var_2 = gbl.unk_1C020[item.type].field_0;
                if (var_2 == 1)
                {
                    bonus[1] = (sbyte)(item.exp_value + var_1);
                    return;
                }

                if (var_1 == 0)
                {
                    if (var_2 == 9)
                    {
                        if (item.exp_value > bonus[3])
                        {
                            bonus[3] = item.exp_value;
                        }
                    }
                    else
                    {
                        bonus[2] += (sbyte)(item.exp_value);
                    }

                    player.field_186 = (sbyte)(player.field_186 + item.field_33);
                    return;
                }


                if ((item.exp_value + var_1) > bonus[4])
                {
                    bonus[4] = (sbyte)(item.exp_value + var_1);

                    if (item.exp_value > 0)
                    {
                        if (var_2 == 2)
                        {
                            output = 1;
                        }
                    }
                }
            }

            //func_end:
        }


        internal static void sub_663C4(Player player)
        {
            byte var_3;
            int var_2;

            var_2 = player.weight - strength_bonus(player);

            if (var_2 < 0)
            {
                var_2 = 0;
            }

            if (var_2 >= 0 && var_2 <= 0x200)
            {
                var_3 = player.initiative;
            }
            else if (var_2 >= 0x201 && var_2 <= 0x300)
            {
                var_3 = 9;
            }
            else if (var_2 >= 0x301 && var_2 <= 0x400)
            {
                var_3 = 6;
            }
            else
            {
                var_3 = 3;
            }

            if (var_3 < player.initiative)
            {
                player.initiative = var_3;
            }
        }


        static string[] itemNames = {
								 "Battle Axe","Hand Axe","Bardiche","Bec De Corbin",
								 "Bill-Guisarme","Bo Stick", "Club","Dagger","Dart",
								 "Fauchard","Fauchard-Fork","Flail","Military Fork",
								 "Glaive","Glaive-Guisarme","Guisarme","Guisarme-Voulge",
								 "Halberd","Lucern Hammer","Hammer","Javelin","Jo Stick",
								 "Mace","Morning Star","Partisan","Military Pick","Awl Pike",
								 "Quarrel","Ranseur","Scimitar","Spear","Spetum","Quarter Staff",
								 "Bastard Sword","Broad Sword","Long Sword","Short Sword",
								 "Two-Handed Sword","Trident",
								 "Voulge","Composite Short Bow","Long Bow","Short Bow",
								 "Heavy Crossbow","Light Crossbow","Sling","Mail","Armor",
								 "Leather","Padded","Studded","Ring","Scale","Chain",
								 "Splint","Banded","Plate","Shield","Woods","Arrow",
								 string.Empty,string.Empty,"Potion","Ring","Rod","Stave",
								 "Wand","Jug","Amulet","Dragon Breath","Bag","Defoliation",
								 "Ice Storm","Book","Boots","Hornets Nest","Bracers",
								 "Piercing","Brooch","Elfin Chain","Wizardry","ac10",
								 "Dexterity","Fumbling","Chime","Cloak","Crystal","Cube",
								 "Cubic","The Dwarves","Decanter","Gloves","Drums","Dust",
								 "Thievery","Hat","Flask","Gauntlets","Gem","Girdle","Helm",
								 "Horn","Stupidity","Incense","Stone","Ioun Stone",
								 "Javelin","Jewel","Ointment","Pale Blue","Scarlet And",
								 "Manual","Incandescent","Deep Red","Pink","Mirror",
								 "Necklace","And Green","Blue","Pearl","Powerlessness",
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



        internal static void id_item(byte arg_0, byte arg_2, byte arg_4, byte arg_6, Item item, Player player)
        {
            Player player_ptr;
            byte var_8;
            byte var_7;
            Affect var_6;
            byte var_2;
            byte var_1;

            item.name = string.Empty;

            if (arg_2 != 0)
            {
                if (item.field_34 != 0)
                {
                    item.name = " Yes  ";
                }
                else
                {
                    item.name = " No   ";
                }
            }

            var_7 = 0;
            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null && var_7 == 0)
            {
                if (find_affect(out var_6, Affects.detect_magic, player_ptr) == true)
                {
                    var_7 = 1;
                }

                player_ptr = player_ptr.next_player;
            }

            if (var_7 != 0)
            {
                if (item.exp_value > 0 || item.field_33 > 0 || item.field_36 != 0)
                {
                    item.name += "* ";
                }
            }

            if (item.count > 0)
            {
                item.name += sub_670CC(item.count) + " ";
            }

            var_2 = 0;

            for (var_1 = 1; var_1 <= 3; var_1++)
            {
                if (item.field_2EArray(var_1) != item.type)
                {
                    if (((item.field_35 >> (3 - var_1)) & 1) == 0)
                    {
                        var_2 += (byte)(1 << (var_1 - 1));
                    }
                }
            }

            var_8 = 0;

            for (var_1 = 3; var_1 >= 1; var_1--)
            {
                if (((var_2 >> (var_1 - 1)) & 1) > 0)
                {
                    item.name += itemNames[item.field_2EArray(var_1)];

                    if (item.count < 2 ||
                        var_8 != 0)
                    {
                        item.name += " ";
                    }
                    else
                    {
                        throw new System.NotSupportedException();//loc_66694:
                        throw new System.NotSupportedException();//mov	al, [bp+var_2]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	bx, ax
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//dec	ax
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//mov	ax, 1
                        throw new System.NotSupportedException();//mov	cx, dx
                        throw new System.NotSupportedException();//shl	ax, cl
                        throw new System.NotSupportedException();//cmp	ax, bx
                        throw new System.NotSupportedException();//jz	loc_66709
                        throw new System.NotSupportedException();//cmp	[bp+var_1], 1
                        throw new System.NotSupportedException();//jnz	loc_666C4
                        throw new System.NotSupportedException();//cmp	[bp+var_2], 4
                        throw new System.NotSupportedException();//jbe	loc_666C4
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.type], 0x56
                        throw new System.NotSupportedException();//jnz	loc_66709
                        throw new System.NotSupportedException();//loc_666C4:
                        throw new System.NotSupportedException();//cmp	[bp+var_1], 2
                        throw new System.NotSupportedException();//jnz	loc_666D1
                        throw new System.NotSupportedException();//mov	al, [bp+var_2]
                        throw new System.NotSupportedException();//shr	al, 1
                        throw new System.NotSupportedException();//jnb	loc_66709
                        throw new System.NotSupportedException();//loc_666D1:
                        throw new System.NotSupportedException();//cmp	[bp+var_1], 3
                        throw new System.NotSupportedException();//jnz	loc_666E1
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.type], 0x56
                        throw new System.NotSupportedException();//jz	loc_66709
                        throw new System.NotSupportedException();//loc_666E1:
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.field_31], 0x87
                        throw new System.NotSupportedException();//jz	loc_66737
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.type], 0x49
                        throw new System.NotSupportedException();//jz	loc_666FF
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.type], 0x1C
                        throw new System.NotSupportedException();//jnz	loc_66737
                        throw new System.NotSupportedException();//loc_666FF:
                        throw new System.NotSupportedException();//les	di, dword ptr [bp+item.offset]
                        throw new System.NotSupportedException();//cmp	es:[di+item.field_31], 0x0B1
                        throw new System.NotSupportedException();//jz	loc_66737
                        throw new System.NotSupportedException();//loc_66709:

                        item.name += "s ";
                        var_8 = 1;
                        throw new System.NotSupportedException();//jmp	short loc_6675F
                        throw new System.NotSupportedException();//loc_66737:
                        item.name += " ";
                    }
                }
                //loc_6675F:
            }

            if (arg_0 != 0)
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
                        sub_678A2(0, y_pos, x_pos, player_ptr);
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

                    display_hp(0, y_pos, var_7 + 0x24, player_ptr);
                    y_pos++;
                    player_ptr = player_ptr.next_player;
                }

                seg037.draw8x8_clear_area(y_pos, 0x26, y_pos, x_pos);
            }
        }


        internal static void display_AC(int y_offset, int x_offset, Player player)
        {
            string var_100 = ((int)player.ac - 0x3c).ToString();

            seg041.displayString(var_100, 0, 10, y_offset, x_offset);
        }


        internal static void display_hp(byte arg_0, int y_pos, int x_pos, Player player)
        {
            byte colour;

            if (player.hit_point_current < player.hit_point_max)
            {
                colour = 0x0E;
            }
            else
            {
                colour = 0x0A;
            }

            if (arg_0 != 0)
            {
                colour = 0x0D;
            }

            string s = sub_670CC(player.hit_point_current);
            seg041.displayString(s, 0, colour, y_pos, x_pos);
        }


        internal static void hitpoint_ac(Player player)
        {
            byte var_2;
            /*byte var_1;*/

            if (gbl.byte_1D90F != 0)
            {
                gbl.byte_1D90F = 0;
                seg037.draw8x8_clear_area(0x15, 0x26, 1, 0x17);

                var_2 = 1;

                sub_67788(0, var_2, " ", player);

                var_2++;

                seg041.displayString("Hitpoints", 0, 10, var_2 + 1, 0x17);

                display_hp(0, (byte)(var_2 + 1), 0x21, player);
                var_2 += 2;

                seg041.displayString("AC", 0, 10, var_2 + 1, 0x17);
                display_AC(var_2 + 1, 0x1A, player);

                gbl.textYCol = (byte)(var_2 + 1);

                if (player.field_151 != null)
                {
                    var_2 += 2;
                    /*var_1 = 0x17;*/
                    id_item(0, 0, 0, 0, player.field_151, player);

                    seg041.press_any_key(player.field_151.name, true, 0, 10, (byte)(var_2 + 3), 0x26, (byte)(var_2 + 1), 0x17);
                }

                var_2 = (byte)(gbl.textYCol + 1);

                if (player.in_combat == false)
                {
                    seg041.displayString(ovr020.statusString[(int)player.health_status], 0, 15, var_2 + 1, 0x17);
                }
                else if (is_held(player) == true)
                {
                    seg041.displayString("(Helpless)", 0, 15, var_2 + 1, 0x17);
                }
            }
        }

        static Affects[] affects_01 = { Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless };


        internal static bool is_held(Player player)
        {
            Affect affect_ptr;
            byte loop_var;
            bool var_1;

            var_1 = false;
            for (loop_var = 0; loop_var < 4; loop_var++)
            {
                if (find_affect(out affect_ptr, affects_01[loop_var], player) == true)
                {
                    var_1 = true;
                }
            }

            return var_1;
        }


        internal static void sub_66C20(Player player)
        {
            byte var_13;
            short var_12;
            byte var_10;
            sbyte[] stat_bonus = new sbyte[5];
            byte var_8;
            byte var_7;
            Item item_ptr;
            byte var_2;

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

            var_8 = 0;
            player.field_14C = 0;

            item_ptr = player.itemsPtr;
            player.field_185 = 0;

            player.weight = 0;
            gbl.word_1AFE0 = 0;

            while (item_ptr != null)
            {
                player.field_14C++;
                var_12 = item_ptr.weight;

                if (item_ptr.count > 0)
                {
                    var_12 = (short)(var_12 * item_ptr.count);
                }

                player.weight += var_12;

                if (item_ptr.field_34 != 0)
                {
                    gbl.word_1AFE0 += var_12;

                    var_13 = gbl.unk_1C020[item_ptr.type].field_0;

                    if (var_13 >= 0 && var_13 <= 8)
                    {
                        player.itemArray[var_13] = item_ptr;
                    }
                    else if (var_13 == 9)
                    {
                        if (player.Item_ptr_01 != null)
                        {
                            if (player.Item_ptr_02 == null)
                            {
                                player.Item_ptr_02 = item_ptr;
                            }
                        }
                        else
                        {
                            player.Item_ptr_01 = item_ptr;
                        }
                    }

                    if (item_ptr.type == 0x49)
                    {
                        player.Item_ptr_03 = item_ptr;
                    }

                    if (item_ptr.type == 0x1C)
                    {
                        player.Item_ptr_04 = item_ptr;
                    }

                    player.field_185 += gbl.unk_1C020[item_ptr.type].field_1;
                }

                item_ptr = item_ptr.next;
            }


            for (var_10 = 0; var_10 <= 6; var_10++)
            {
                player.weight += player.Money[var_10];
            }

            player.field_19E = player.field_11E;
            player.field_19F = player.field_11F;

            player.field_1A0 = player.field_120;
            player.field_1A1 = player.field_121;

            player.field_1A2 = player.field_122;
            player.field_1A3 = player.field_123;

            for (var_2 = 0; var_2 <= 4; var_2++)
            {
                stat_bonus[var_2] = 0;
            }

            var_7 = 0;

            player.field_186 = 0;
            player.ac = player.field_124;
            player.initiative = player.field_E4;
            player.field_199 = player.field_73;

            stat_bonus[0] = ovr025.stat_bonus(player);

            if (player.field_151 == null)
            {
                player.field_199 += strengthHitBonus(player);
                player.field_1A2 += strengthDamBonus(player);
            }

            sub_66023(player);
            item_ptr = player.itemsPtr;

            while (item_ptr != null)
            {
                if (item_ptr.field_34 != 0)
                {
                    sub_6621E(item_ptr, player);
                    sub_662A6(ref var_7, ref stat_bonus, item_ptr, player);
                }

                item_ptr = item_ptr.next;
            }

            if (var_7 != 0)
            {
                stat_bonus[3] = 0;
            }

            if (var_8 != 0)
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

            sub_663C4(player);

            if (stat_bonus[4] < player.ac)
            {
                stat_bonus[4] = (sbyte)player.ac;
            }

            player.ac = 0;

            for (var_2 = 0; var_2 <= 4; var_2++)
            {
                player.ac += (byte)stat_bonus[var_2];
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

        /// <summary>
        /// byte.ToString()
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        internal static string sub_670CC(byte number)
        {
            return number.ToString();
        }
        internal static string sub_670CC(sbyte number)
        {
            return number.ToString();
        }
        internal static string sub_670CC(int number)
        {
            return number.ToString();
        }


        /// <summary>
        /// word.ToString()
        /// </summary>
        /// <param name="number"></param>
        /// <param name="output">not used.</param>
        /// <returns></returns>
        internal static string ConcatWord(int number)
        {
            string output = number.ToString();
            return output;
        }


        internal static string sub_67142(int arg_0)
        {
            string var_100;

            seg051.Str(0xff, out var_100, 0, arg_0);

            return var_100;
        }


        internal static sbyte stat_bonus(Player player)
        {
            byte stat_val;
            sbyte bonus;

            stat_val = player.dex;

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
            else if (stat_val >= 21 || stat_val <= 23)
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


        internal static sbyte stat_bonus02(Player player)
        {
            byte stat_val;
            sbyte var_1;

            stat_val = player.dex;

            if (stat_val >= 0 && stat_val <= 2)
            {
                var_1 = -4;
            }
            else if (stat_val >= 3 && stat_val <= 5)
            {
                var_1 = (sbyte)(stat_val - 6);
            }
            else if (stat_val >= 16 && stat_val <= 18)
            {
                var_1 = (sbyte)(stat_val - 15);
            }
            else if (stat_val >= 19 && stat_val <= 20)
            {
                var_1 = 3;
            }
            else if (stat_val >= 21 && stat_val <= 23)
            {
                var_1 = 4;
            }
            else if (stat_val >= 24 && stat_val <= 25)
            {
                var_1 = 5;
            }
            else
            {
                var_1 = 0;
            }

            return var_1;
        }


        internal static byte playerStrengh(Player player)
        {
            byte ret_val;

            if (player.strength >= 0 && player.strength <= 17)
            {
                ret_val = player.strength;
            }
            else if (player.strength == 18)
            {
                if (player.strength_18_100 == 0)
                {
                    ret_val = 18;
                }
                else if (player.strength_18_100 >= 1 && player.strength_18_100 <= 50)
                {
                    ret_val = 19;
                }
                else if (player.strength_18_100 >= 51 && player.strength_18_100 <= 75)
                {
                    ret_val = 20;
                }
                else if (player.strength_18_100 >= 76 && player.strength_18_100 <= 90)
                {
                    ret_val = 21;
                }
                else if (player.strength_18_100 >= 91 && player.strength_18_100 <= 99)
                {
                    ret_val = 22;
                }
                else if (player.strength_18_100 >= 100)
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
                ret_val = (byte)(player.strength + 5);
            }
            else
            {
                throw new System.NotSupportedException();
            }

            return ret_val;
        }


        internal static sbyte strengthHitBonus(Player player)
        {
            byte str_stat;
            int str_bonus;

            str_bonus = 0;

            str_stat = playerStrengh(player);

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


        internal static sbyte strengthDamBonus(Player arg_0)
        {
            byte var_2;
            int var_1 = 0;

            var_2 = playerStrengh(arg_0);

            if (arg_0.field_125 != 0)
            {
                if (var_2 == 1 || var_2 == 2)
                {
                    var_1 = -2;
                }
                else if (var_2 >= 3 && var_2 <= 5)
                {
                    var_1 = -1;
                }
                else if (var_2 == 16)
                {
                    var_1 = 1;
                }
                else if (var_2 >= 17 && var_2 <= 19)
                {
                    var_1 = var_2 - 16;
                }
                else if (var_2 >= 20 && var_2 <= 29)
                {
                    var_1 = var_2 - 17;
                }
                else if (var_2 == 30)
                {
                    var_1 = 14;
                }
            }

            return (sbyte)var_1;
        }


        internal static short strength_bonus(Player player)
        {
            byte var_3;
            int ret_word;

            var_3 = playerStrengh(player);

            if (var_3 >= 1 && var_3 <= 3)
            {
                ret_word = -350;
            }
            else if (var_3 == 4 || var_3 == 5)
            {
                ret_word = -250;
            }
            else if (var_3 == 6 || var_3 == 7)
            {
                ret_word = -150;
            }
            else if (var_3 == 12 || var_3 == 13)
            {
                ret_word = 100;
            }
            else if (var_3 == 14 || var_3 == 15)
            {
                ret_word = 200;
            }
            else if (var_3 == 16)
            {
                ret_word = 350;
            }
            else if (var_3 >= 17 && var_3 <= 21)
            {
                ret_word = ((var_3 - 17) * 250) + 500;
            }
            else if (var_3 >= 22 && var_3 <= 26)
            {
                ret_word = ((var_3 - 22) * 1000) + 2000;
            }
            else if (var_3 == 27)
            {
                ret_word = 7500;
            }
            else if (var_3 >= 28 && var_3 <= 30)
            {
                ret_word = ((var_3 - 28) * 3000) + 9000;
            }
            else
            {
                ret_word = 0;
            }

            return (short)ret_word;
        }


        internal static void clear_spell(byte spell_id, Player player)
        {
            byte loop_var;

            loop_var = 0;

            while (loop_var <= 0x53)
            {
                if (player.spell_list[loop_var] == spell_id)
                {
                    player.spell_list[loop_var] = 0;
                    loop_var = 0x54;
                }

                loop_var++;
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
            Item item_ptr;
            Item item_ptr02;

            item_ptr = item.ShallowClone();
            item_ptr.next = null;

            item_ptr02 = player.itemsPtr;

            if (item_ptr02 == null)
            {
                player.itemsPtr = item_ptr;
            }
            else
            {
                while (item_ptr02.next != null)
                {
                    item_ptr02 = item_ptr02.next;
                }

                item_ptr02.next = item_ptr;
            }
        }


        internal static void string_print01(string arg_0)
        {
            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);

            seg041.displayString(arg_0, 0, 10, 0x18, 0);

            seg041.GameDelay();

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
        }


        internal static void sub_67788(byte arg_0, byte arg_2, string arg_4, Player player)
        {
            byte var_101;
            string var_100;

            var_100 = arg_4;

            if (gbl.game_state == 5)
            {
                seg037.draw8x8_clear_area(0x15, 0x26, arg_2, 0x17);

                sub_678A2(0, arg_2, 0x17, player);
                seg041.press_any_key(var_100, true, 0, 10, 0x15, 0x26, (byte)(arg_2 + 1), 0x17);
            }
            else
            {
                if (gbl.byte_1D8A8 != 0)
                {
                    var_101 = 0x12;
                }
                else
                {
                    var_101 = 0x11;
                }
                seg037.draw8x8_clear_area(0x16, 0x26, var_101, 1);

                sub_678A2(0, (byte)(var_101 + 1), 1, player);
                seg041.press_any_key(var_100, true, 0, 10, 0x16, 0x26, (byte)(var_101 + 2), 1);
            }

            if (arg_0 != 0)
            {
                seg041.GameDelay();
                ovr025.sub_6786F();
            }
        }


        internal static void sub_6786F()
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


        internal static void sub_678A2(byte arg_0, int y_offset, int x_offset, Player player)
        {
            byte var_1;

            if (player.in_combat == false)
            {
                var_1 = 0x0C;
            }
            else if (player.combat_team == 1)
            {
                var_1 = 0x0E;
            }
            else
            {
                var_1 = 0x0B;
            }

            seg041.displayString(player.name, 0, var_1, y_offset, x_offset);

            if (arg_0 != 0)
            {
                seg041.displayString("s", 0, var_1, y_offset, x_offset + player.name.Length);
            }
        }


        internal static void sub_67924(bool arg_0, byte arg_2, byte arg_4, byte arg_6)
        {
            short var_6;
            DaxBlock var_4;

            var_6 = gbl.dword_1D90A.bpp;

            if (arg_0 == true)
            {
                seg040.init_dax_block(out var_4, 1, 1, 3, 0x18);
                seg040.merge_icon(var_4, gbl.combat_icons[arg_6, arg_4]);
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//add	di, 0x17
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mul	[bp+var_6]
                throw new System.NotSupportedException();//les	di, dword_1D90A
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//add	di, 0x17
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//push	[bp+var_6]
                throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//les	di, es:[di+13h]
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mul	[bp+var_6]
                throw new System.NotSupportedException();//les	di, dword_1D90A
                throw new System.NotSupportedException();//les	di, es:[di+13h]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//push	[bp+var_6]
                throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)

                seg040.free_dax_block(ref var_4);
            }
            else
            {
                throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_6]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//mov	cl, 3
                throw new System.NotSupportedException();//shl	di, cl
                throw new System.NotSupportedException();//add	di, dx
                throw new System.NotSupportedException();//les	di, int ptr combat_icons.offset[di]
                throw new System.NotSupportedException();//add	di, 0x17
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mul	[bp+var_6]
                throw new System.NotSupportedException();//les	di, dword_1D90A
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//add	di, 0x17
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//push	[bp+var_6]
                throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
                throw new System.NotSupportedException();//mov	al, [bp+arg_4]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+arg_6]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//mov	cl, 3
                throw new System.NotSupportedException();//shl	di, cl
                throw new System.NotSupportedException();//add	di, dx
                throw new System.NotSupportedException();//les	di, int ptr combat_icons.offset[di]
                throw new System.NotSupportedException();//les	di, es:[di+13h]
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//mov	al, [bp+arg_2]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mul	[bp+var_6]
                throw new System.NotSupportedException();//les	di, dword_1D90A
                throw new System.NotSupportedException();//les	di, es:[di+13h]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//push	es
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//push	[bp+var_6]
                throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            }
            throw new System.NotSupportedException();//func_end:
        }


        internal static void sub_67A59(byte arg_0)
        {
            sub_67924(false, 0, 0, arg_0);
            sub_67924(true, 1, 0, arg_0);
            sub_67924(true, 2, 1, arg_0);
            sub_67924(false, 3, 1, arg_0);
        }


        internal static void sub_67AA4(byte arg_0, byte arg_2, short arg_4, short arg_6, short arg_8, short arg_A)
        {
            short var_D0;
            short var_CE;
            short var_CC;
            short var_CA;
            short var_C8;
            short var_C6;
            short var_C4;
            short var_C2;
            short var_C0;
            short var_BE;
            short var_BC;
            short var_BA;
            short var_B8;
            short var_B6;
            bool var_B4;
            byte var_B3;
            byte var_B1;
            byte var_B0;
            byte var_AF;
            Struct_XXXX var_AC = new Struct_XXXX();
            byte[] var_94 = new byte[0x94];

            var_BA = arg_A;
            var_BC = arg_8;

            seg051.FillChar(8, 0x94, var_94);

            var_AF = 0;
            var_B1 = 0;

            var_AC.field_00 = (short)(arg_A * 3);
            var_AC.field_02 = (short)(arg_8 * 3);
            var_AC.field_04 = (short)(arg_6 * 3);
            var_AC.field_06 = (short)(arg_4 * 3);

            ovr032.sub_731A5(var_AC);

            do
            {
                var_B4 = !ovr032.sub_7324C(var_AC);

                var_94[var_AF] = var_AC.field_17;

                var_AF++;
            } while (var_B4 == false);

            var_B0 = (byte)(var_AF - 2);

            if (var_B0 < 2 || var_AF < 2)
            {
                return;
            }

            var_C2 = (short)(arg_6 - arg_A);
            var_C4 = (short)(arg_4 - arg_8);

            if (ovr033.sub_74730((arg_8 - gbl.stru_1D1BC.field_3), (arg_A - gbl.stru_1D1BC.field_2)) == false ||
                ovr033.sub_74730((arg_4 - gbl.stru_1D1BC.field_3), (arg_6 - gbl.stru_1D1BC.field_2)) == false)
            {
                if (System.Math.Abs(var_C2) <= 6 &&
                    System.Math.Abs(var_C4) <= 6)
                {
                    var_B3 = 1;
                    var_B6 = (short)((var_C2 / 2) + arg_A);
                    var_B8 = (byte)((var_C4 / 2) + arg_8);
                }
                else
                {
                    var_B3 = 0;
                    var_B6 = (short)(gbl.stru_1D1BC.field_2 + 3);
                    var_B8 = (short)(gbl.stru_1D1BC.field_3 + 3);
                }
            }
            else
            {
                var_B3 = 1;
                var_B6 = (short)(gbl.stru_1D1BC.field_2 + 3);
                var_B8 = (short)(gbl.stru_1D1BC.field_3 + 3);
            }

            ovr033.sub_749DD(8, 0xFF, (sbyte)var_B8, (sbyte)var_B6);
            var_AF = 0;
            var_BE = 0;
            var_C0 = 0;

            do
            {
                var_CA = (short)(((arg_A - gbl.stru_1D1BC.field_2) * 3) + var_BE);
                var_CC = (short)(((arg_8 - gbl.stru_1D1BC.field_3) * 3) + var_C0);
                var_BA = arg_A;
                var_BC = arg_8;
                var_B4 = false;

                do
                {
                    var_C6 = gbl.MapDirectionXDelta[var_94[var_AF]];
                    var_C8 = gbl.MapDirectionYDelta[var_94[var_AF]];
                    var_CA += var_C6;
                    var_CC += var_C8;

                    throw new System.NotSupportedException();//cmp	[bp+arg_0], 0
                    throw new System.NotSupportedException();//ja	loc_67D4A
                    throw new System.NotSupportedException();//mov	ax, [bp+var_CA]
                    throw new System.NotSupportedException();//cwd
                    throw new System.NotSupportedException();//mov	cx, 3
                    throw new System.NotSupportedException();//idiv	cx
                    throw new System.NotSupportedException();//xchg	ax, dx
                    throw new System.NotSupportedException();//or	ax, ax
                    throw new System.NotSupportedException();//jz	loc_67D4A
                    throw new System.NotSupportedException();//mov	ax, [bp+var_CC]
                    throw new System.NotSupportedException();//cwd
                    throw new System.NotSupportedException();//mov	cx, 3
                    throw new System.NotSupportedException();//idiv	cx
                    throw new System.NotSupportedException();//xchg	ax, dx
                    throw new System.NotSupportedException();//or	ax, ax
                    throw new System.NotSupportedException();//jnz	loc_67DB4
                    throw new System.NotSupportedException();//loc_67D4A:

                    seg040.sub_E353(gbl.overlayLines, gbl.dword_1D90A, 5, var_B1, var_CC, var_CA);
                    seg040.DrawOverlay();

                    seg049.SysDelay(arg_0);

                    seg040.sub_E353(gbl.overlayLines, gbl.word_1B316, 0, 0, var_CC, var_CA);
                    var_B1++;

                    if (var_B1 >= arg_2)
                    {
                        var_B1 = 0;
                    }
                    //loc_67DB4:


                    var_AF++;
                    if (var_CA < 0 || var_CA > 0x12 || var_CC < 0 || var_CC > 0x12)
                    {
                        var_B4 = true;
                    }

                    if (var_B4 == false &&
                        var_AF < var_B0)
                    {
                        var_BE += var_C6;
                        var_C0 += var_C8;

                        if (System.Math.Abs(var_BE) == 3)
                        {
                            arg_A += ovr032.sub_73005(var_BE);
                            var_B6 += ovr032.sub_73005(var_BE);
                            var_BE = 0;
                        }

                        if (System.Math.Abs(var_C0) == 3)
                        {
                            arg_8 += ovr032.sub_73005(var_C0);
                            var_B8 += ovr032.sub_73005(var_C0);
                            var_C0 = 0;
                        }
                    }

                } while (var_AF < var_B0 && var_B4 == false);

                throw new System.NotSupportedException();//mov	al, [bp+var_AF]
                throw new System.NotSupportedException();//cmp	al, [bp+var_B0]
                throw new System.NotSupportedException();//jb	loc_67E75
                throw new System.NotSupportedException();//jmp	loc_680A0
                throw new System.NotSupportedException();//loc_67E75:
                var_CE = 0;
                var_D0 = 0;
                var_C0 = 0;
                var_BE = 0;
                arg_A = arg_6;
                arg_8 = arg_4;
                var_BA = arg_A;
                var_BC = arg_8;
                throw new System.NotSupportedException();//mov	ax, [bp+arg_6]
                throw new System.NotSupportedException();//add	ax, 3
                throw new System.NotSupportedException();//cmp	ax, 0x31
                throw new System.NotSupportedException();//jle	loc_67EBE
                var_CE = (short)(arg_6 - 0x31);
                throw new System.NotSupportedException();//jmp	short loc_67ECE
                throw new System.NotSupportedException();//loc_67EBE:
                throw new System.NotSupportedException();//cmp	[bp+arg_6], 3
                throw new System.NotSupportedException();//jge	loc_67ECE
                throw new System.NotSupportedException();//mov	ax, 3
                throw new System.NotSupportedException();//sub	ax, [bp+arg_6]
                throw new System.NotSupportedException();//mov	[bp+var_CE], ax
                throw new System.NotSupportedException();//loc_67ECE:
                throw new System.NotSupportedException();//mov	ax, [bp+arg_4]
                throw new System.NotSupportedException();//add	ax, 3
                throw new System.NotSupportedException();//cmp	ax, 0x18
                throw new System.NotSupportedException();//jle	loc_67EE5
                throw new System.NotSupportedException();//mov	ax, [bp+arg_4]
                throw new System.NotSupportedException();//sub	ax, 0x18
                throw new System.NotSupportedException();//mov	[bp+var_D0], ax
                throw new System.NotSupportedException();//jmp	short loc_67EF5
                throw new System.NotSupportedException();//loc_67EE5:
                throw new System.NotSupportedException();//cmp	[bp+arg_4], 3
                throw new System.NotSupportedException();//jge	loc_67EF5
                throw new System.NotSupportedException();//mov	ax, 3
                throw new System.NotSupportedException();//sub	ax, [bp+arg_4]
                throw new System.NotSupportedException();//mov	[bp+var_D0], ax
                throw new System.NotSupportedException();//loc_67EF5:
                var_B6 = (short)(arg_6 + var_CE);
                var_B8 = (short)(arg_4 + var_D0);

                ovr033.sub_749DD(8, 0xff, (sbyte)var_B8, (sbyte)var_B6);
                var_CA = (short)((arg_6 - gbl.stru_1D1BC.field_2) * 3);
                var_CC = (short)((arg_4 - gbl.stru_1D1BC.field_3) * 3);
                var_AF = var_B0;
                var_B4 = false;

                do
                {
                    var_C6 = (short)(-gbl.MapDirectionXDelta[var_94[var_AF]]);
                    throw new System.NotSupportedException();//mov	al, var_94[ var_AF ]
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//mov	di, ax
                    throw new System.NotSupportedException();//mov	al, byte ptr unk_189AF[di]
                    throw new System.NotSupportedException();//cbw
                    throw new System.NotSupportedException();//neg	ax
                    throw new System.NotSupportedException();//mov	[bp+var_C8], ax
                    throw new System.NotSupportedException();//mov	ax, [bp+var_C6]
                    throw new System.NotSupportedException();//add	[bp+var_CA], ax
                    throw new System.NotSupportedException();//mov	ax, [bp+var_C8]
                    throw new System.NotSupportedException();//add	[bp+var_CC], ax
                    throw new System.NotSupportedException();//cmp	[bp+var_CA], 0x12
                    throw new System.NotSupportedException();//jle	loc_67FBF
                    arg_A = (short)(gbl.stru_1D1BC.field_2 + 6);
                    throw new System.NotSupportedException();//jmp	short loc_67FD2
                    throw new System.NotSupportedException();//loc_67FBF:
                    throw new System.NotSupportedException();//cmp	[bp+var_CA], 0
                    throw new System.NotSupportedException();//jge	loc_67FD2
                    arg_A = gbl.stru_1D1BC.field_2;
                    throw new System.NotSupportedException();//loc_67FD2:
                    throw new System.NotSupportedException();//cmp	[bp+var_CC], 0x12
                    throw new System.NotSupportedException();//jle	loc_67FEA
                    arg_8 = (short)(gbl.stru_1D1BC.field_3 + 6);
                    throw new System.NotSupportedException();//jmp	short loc_67FFD
                    throw new System.NotSupportedException();//loc_67FEA:
                    throw new System.NotSupportedException();//cmp	[bp+var_CC], 0
                    throw new System.NotSupportedException();//jge	loc_67FFD
                    arg_8 = gbl.stru_1D1BC.field_3;
                    throw new System.NotSupportedException();//loc_67FFD:
                    throw new System.NotSupportedException();//cmp	[bp+var_CA], 0
                    throw new System.NotSupportedException();//jl	loc_68019
                    throw new System.NotSupportedException();//cmp	[bp+var_CA], 0x12
                    throw new System.NotSupportedException();//jg	loc_68019
                    throw new System.NotSupportedException();//cmp	[bp+var_CC], 0
                    throw new System.NotSupportedException();//jl	loc_68019
                    throw new System.NotSupportedException();//cmp	[bp+var_CC], 0x12
                    throw new System.NotSupportedException();//jle	loc_6801E
                    throw new System.NotSupportedException();//loc_68019:
                    var_B4 = true;
                    throw new System.NotSupportedException();//loc_6801E:
                    if (var_B4 == false)
                    {
                        var_BE += var_C6;
                        var_C0 += var_C8;
                        if (System.Math.Abs(var_BE) == 3)
                        {
                            arg_A += ovr032.sub_73005(var_BE);
                            var_B6 += ovr032.sub_73005(var_BE);
                            var_BE = 0;
                        }

                        if (System.Math.Abs(var_C0) == 3)
                        {
                            arg_8 += ovr032.sub_73005(var_C0);
                            var_B8 += ovr032.sub_73005(var_C0);
                            var_C0 = 0;
                        }

                        var_AF -= 1;
                    }

                } while (var_B4 == false);

                throw new System.NotSupportedException();//jmp	loc_68175
                throw new System.NotSupportedException();//loc_680A0:
                var_B3 = 1;

                if (ovr033.sub_74730(arg_4 - gbl.stru_1D1BC.field_3, arg_6 - gbl.stru_1D1BC.field_2) == false)
                {
                    ovr033.sub_749DD(8, 3, (sbyte)arg_4, (sbyte)arg_6);
                }

                var_CA = (short)((arg_6 - gbl.stru_1D1BC.field_2) * 3);
                var_CC = (short)((arg_4 - gbl.stru_1D1BC.field_3) * 3);

                seg040.sub_E353(gbl.overlayLines, gbl.dword_1D90A, 5, var_B1, var_CC, var_CA);

                if (arg_0 > 0)
                {
                    seg040.DrawOverlay();

                    seg049.SysDelay(arg_0);

                    seg040.sub_E353(gbl.overlayLines, gbl.word_1B316, 0, 0, var_CC, var_CA);
                }
                throw new System.NotSupportedException();//loc_68175:
            } while (var_B3 == 0);

            seg040.DrawOverlay();
        }


        internal static void sub_6818A(string s, byte arg_4, Player player)
        {
            byte var_107;
            byte var_106;
            byte var_105;
            byte var_104;
            byte var_103;
            byte var_102;
            byte var_101;
            string var_100;

            var_100 = s;

            if (gbl.game_state == 5)
            {
                if (arg_4 != 0)
                {
                    var_103 = 0x16;
                }
                else
                {
                    var_103 = 0x17;
                }

                sub_67A59(var_103);

                if (ovr033.sub_74761(1, player) == false)
                {
                    ovr033.sub_749DD(8, 3, ovr033.sub_74C5A(player), ovr033.sub_74C32(player));
                }

                if (arg_4 != 0)
                {
                    seg044.sub_120E0(gbl.word_188C6);
                }
                else
                {
                    seg044.sub_120E0(gbl.word_188C4);
                }

                sub_67788(0, 10, var_100, player);

                if (arg_4 != 0)
                {
                    var_106 = gbl.game_speed_var;
                }
                else
                {
                    var_106 = 0;
                }

                var_101 = (byte)(gbl.unk_1CAF0[ovr033.get_player_index(player)] * 3);
                var_102 = (byte)(gbl.unk_1CB38[ovr033.get_player_index(player)] * 3);

                var_107 = var_106;

                for (var_105 = 0; var_105 <= var_107; var_105++)
                {
                    for (var_104 = 0; var_104 <= 3; var_104++)
                    {
                        seg040.sub_E353(gbl.overlayLines, gbl.dword_1D90A, 5, var_104, var_102, var_101);
                        seg040.DrawOverlay();

                        seg049.SysDelay(70);

                        seg040.sub_E353(gbl.overlayLines, gbl.word_1B316, 0, 0, var_102, var_101);
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
                sub_67788(1, 10, var_100, player);
            }
        }


        internal static bool find_affect(out Affect affect, Affects affect_type, Player playerBase)
        {
            bool found_one_yet;

            found_one_yet = false;
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

        static Set unk_683B5 = new Set(0x0001, new byte[] { 0x03 });

        internal static void damage_player(byte damage, Player player)
        {
            byte new_hp;
            byte neg_hp;

            neg_hp = 0;
            new_hp = 0;

            if (player.hit_point_current >= damage)
            {
                new_hp = (byte)(player.hit_point_current - damage);
            }
            else
            {
                neg_hp = (byte)(damage - player.hit_point_current);
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
                    if (player.combat_team == 0)
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
                player.hit_point_current = new_hp;
            }
        }


        internal static void sub_684F7(Player player)
        {
            string var_29;

            if (player.hit_point_current == player.hit_point_max)
            {
                var_29 = "is fully healed";
            }
            else
            {
                var_29 = "is partially healed";
            }

            sub_67788(1, 10, var_29, player);

            if (gbl.game_state != 5)
            {
                Player_Summary(gbl.player_ptr);
            }
        }


        internal static sbyte on_our_team(Player player)
        {
            sbyte ret_val;

            if (player.combat_team == 0)
            {
                ret_val = 1;
            }
            else
            {
                ret_val = 0;
            }

            return ret_val;
        }


        internal static void count_teams()
        {
            Player player;

            gbl.friends_count = 0;
            gbl.foe_count = 0;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.in_combat == true)
                {
                    if (player.combat_team == 0)
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


        internal static byte near_enermy(byte arg_0, Player player)
        {
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte ret_val;

            ovr032.sub_738D8(gbl.stru_1D1BC, ovr033.sub_74C82(player), 0xff, arg_0, ovr033.sub_74C5A(player), ovr033.sub_74C32(player));
            var_2 = gbl.byte_1D1C0;
            var_3 = 0;

            if (var_2 > 0)
            {
                var_5 = var_2;
                for (var_4 = 1; var_4 <= var_5; var_4++)
                {
                    if (gbl.player_array[gbl.unk_1D1C1[var_4].field_0].combat_team == on_our_team(player))
                    {
                        var_3++;
                        gbl.unk_1D1C1[var_3].Copy(gbl.unk_1D1C1[var_4]);
                    }
                }

                var_2 = var_3;
                gbl.byte_1D1C0 = var_3;
            }

            ret_val = var_2;
            var_5 = var_2;
            for (var_4 = 1; var_4 <= var_5; var_4++)
            {
                gbl.byte_1D8B9[var_4] = gbl.unk_1D1C1[var_4].field_0;
            }

            return ret_val;
        }


        internal static byte sub_68708(Player playerA, Player playerB)
        {
            byte var_2FF;
            gbl.Struct_1D1C1[] var_2FE = new gbl.Struct_1D1C1[72];
            byte var_1;

            System.Array.Copy(gbl.unk_1D1C1, var_2FE, 72);

            gbl.stru_1D1BC.field_6 = 1;

            ovr032.sub_738D8(gbl.stru_1D1BC, ovr033.sub_74C82(playerB), 0xff, 0xff, ovr033.sub_74C5A(playerB), ovr033.sub_74C32(playerB));

            gbl.stru_1D1BC.field_6 = 0;
            var_2FF = 0;

            while (gbl.player_array[gbl.unk_1D1C1[var_2FF].field_0] != playerA && var_2FF < (gbl.byte_1D1C0 - 1))
            {
                var_2FF++;
            }

            var_1 = (byte)(gbl.unk_1D1C1[var_2FF].field_1 >> 1);

            System.Array.Copy(var_2FE, gbl.unk_1D1C1, 72);
            return var_1;
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
            bool ret_val;

            ret_val = clear_actions(player);
            player.actions.guarding = true;

            string_print01("Guarding");

            return ret_val;
        }


        internal static byte sub_6886F(byte arg_0)
        {
            sbyte var_3;
            sbyte var_2;
            sbyte var_1 = 0;

            if (gbl.player_ptr.cleric_lvl == 0 &&
                gbl.player_ptr.magic_user_lvl == 0 &&
                gbl.player_ptr.paladin_lvl < 9 &&
                gbl.player_ptr.ranger_lvl < 8)
            {
                var_1 = 6;
            }
            else
            {
                switch (gbl.unk_19AEC[arg_0].field_0)
                {
                    case 0:
                        var_2 = (sbyte)(gbl.player_ptr.cleric_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.turn_undead));
                        var_3 = (sbyte)(gbl.player_ptr.paladin_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_114) - 8);


                        if (var_2 > var_3)
                        {
                            var_1 = var_2;
                        }
                        else
                        {
                            var_1 = var_3;
                        }
                        break;

                    case 1:
                        var_2 = (sbyte)(gbl.player_ptr.ranger_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_115) - 7);
                        if (var_2 > 0)
                        {
                            var_1 = var_2;
                        }
                        else
                        {
                            var_1 = 0;
                        }
                        break;

                    case 2:
                        var_2 = (sbyte)(gbl.player_ptr.magic_user_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_116));
                        var_3 = (sbyte)(gbl.player_ptr.ranger_lvl + (ovr026.sub_6B3D1(gbl.player_ptr) * gbl.player_ptr.field_115) - 8);

                        if (var_2 > var_3)
                        {
                            var_1 = var_2;
                        }
                        else
                        {
                            var_1 = var_3;
                        }

                        break;

                    case 3:

                        var_1 = 0x0C;
                        break;
                }
            }

            if (gbl.byte_1D88D != 0 &&
                gbl.unk_19AEC[arg_0].field_0 != 3)
            {
                var_1 = 6;
            }


            return (byte)var_1;
        }


        internal static void load_pic()
        {
            gbl.byte_1D8AA = 1;

            switch (gbl.game_state)
            {
                case 0:
                    seg037.draw8x8_01();
                    break;

                case 1:
                    if (gbl.byte_1EE7E == true)
                    {
                        seg037.draw8x8_03();
                    }

                    if (gbl.byte_1D5B4 == 0x50)
                    {

                        ovr030.sub_7000A(gbl.dword_1D55C, 1, 3, 3);
                    }
                    else
                    {
                        ovr030.head_body(gbl.byte_1B2EF, gbl.byte_1B2EE);
                        ovr030.sub_706DC(true, 3, 3);
                    }

                    Player_Summary(gbl.player_ptr);
                    camping_search();
                    break;

                case 2:
                    seg037.draw8x8_03();
                    ovr030.load_pic_final(ref gbl.byte_1D556, 0, 0x1d, "PIC");
                    Player_Summary(gbl.player_ptr);
                    camping_search();
                    break;

                case 4:
                    seg037.draw8x8_03();
                    ovr029.sub_6F0BA();
                    Player_Summary(gbl.player_ptr);
                    camping_search();
                    gbl.byte_1EE98 = 0;
                    break;

                case 3:
                    if (gbl.byte_1D5B4 != 0x50)
                    {
                        ovr029.sub_6F0BA();
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


        internal static void camping_search()
        {
            string var_31;
            string var_8;
            string var_5;

            if (gbl.game_state != 3)
            {
                var_31 = string.Empty;

                var_5 = ConcatWord(gbl.area_ptr.field_192);

                if (var_5.Length < 2)
                {
                    var_5 = "0" + var_5;
                }

                var_8 = ConcatWord((gbl.area_ptr.field_190 * 10) + gbl.area_ptr.field_18E);

                if (var_8.Length < 2)
                {
                    var_8 = "0" + var_8;
                }

                if (gbl.area_ptr.field_1F6 == 0)
                {
                    var_31 = gbl.mapPosX.ToString() + "," + gbl.mapPosY.ToString() + " ";
                }

                var_31 = var_31 + direction(gbl.mapDirection) + " " + var_5 + ":" + var_8;

                if (gbl.printCommands == true)
                {
                    var_31 = var_31 + "*";
                }

                if (gbl.game_state == 2)
                {
                    var_31 = var_31 + " camping";
                }
                else if ((gbl.area2_ptr.field_594 & 1) > 0)
                {
                    var_31 = var_31 + " search";

                }

                seg037.draw8x8_clear_area(15, 0x26, 15, 17);

                seg041.displayString(var_31, 0, 10, 15, 17);
            }
        }


        internal static void sub_68DC0()
        {
            ovr033.sub_740F2();
            seg037.draw8x8_06();

            ovr033.sub_749DD(8, 0xff, (sbyte)(gbl.stru_1D1BC.field_3 + 3), (sbyte)(gbl.stru_1D1BC.field_2 + 3));
        }

        static Set unk_68DFA = new Set(0x010A, new byte[] { 0x20, 0, 8, 0, 0, 0, 0, 0x20, 0, 8 });


        internal static void selectAPlayer(ref Player player, bool showExit, string arg_6)
        {
            string var_59;
            Player player_ptr;
            byte var_2C;
            bool var_2B;
            char var_2A;
            string var_29;

            var_29 = arg_6;
            var_2A = ' ';
            var_59 = string.Empty;

            if (showExit == true)
            {
                var_59 = " Exit";
            }

            while (unk_68DFA.MemberOf(var_2A) == false)
            {
                Player_Summary(player);

                if (gbl.game_state == 2 ||
                    gbl.game_state == 6)
                {
                    var_2C = 1;
                }
                else
                {
                    var_2C = 0;
                }

                var_2A = ovr027.displayInput(out var_2B, var_2C, 1, 15, 10, 13, "Select" + var_59, var_29 + " ");
                player_ptr = player;

                if (var_2B == true)
                {
                    if (var_2A == 'O')
                    {
                        player_ptr = player_ptr.next_player;

                        if (player_ptr == null)
                        {
                            player_ptr = gbl.player_next_ptr;
                        }
                    }
                    else if (var_2A == 'G')
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
                    if (var_2A == 'E' ||
                        var_2A == 0)
                    {
                        player_ptr = null;
                    }
                }

                player = player_ptr;
            }
        }


        internal static bool offset_above_1(Player player)
        {
            bool ret_val;

            if (player.field_151 != null &&
                gbl.unk_1C020[player.field_151.type].field_C > 1)
            {
                ret_val = true;
            }
            else
            {
                ret_val = false;
            }

            return ret_val;
        }


        internal static bool offset_equals_20(Player player)
        {
            bool ret_val;

            if (offset_above_1(player) == true &&
                (gbl.unk_1C020[player.field_151.type].field_E & 0x14) == 0x14)
            {
                ret_val = true;
            }
            else
            {
                ret_val = false;
            }

            return ret_val;
        }


        internal static bool sub_6906C(out Item output, Player player)
        {
            byte var_6;
            Item filed_151;
            bool var_1;

            var_1 = false;

            filed_151 = player.field_151;
            output = null;
            var_6 = 0;

            if (filed_151 != null)
            {
                var_6 = gbl.unk_1C020[filed_151.type].field_E;

                if ((var_6 & 0x10) != 0)
                {
                    output = filed_151;
                }

                if ((var_6 & 0x08) != 0)
                {
                    if ((var_6 & 0x01) != 0)
                    {
                        output = player.Item_ptr_03;
                    }

                    if ((var_6 & 0x80) != 0)
                    {
                        output = player.Item_ptr_04;
                    }
                }
            }

            if (output != null ||
                var_6 == 10)
            {
                var_1 = true;
            }

            return var_1;
        }


        internal static bool sub_69138(byte arg_0, Player player)
        {
            Player player_ptr;
            byte skill;
            bool ret_val;

            ret_val = false;

            player_ptr = player;

            skill = player_ptr.Skill_A_lvl[arg_0];

            if (skill > 0)
            {
                switch (player.race)
                {
                    case Race.dwarf:
                        if (arg_0 == 2)
                        {
                            if ((skill == 8 && player_ptr.strength == 17) ||
                                (skill == 7 && player_ptr.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.elf:
                        if (arg_0 == 2)
                        {
                            if ((skill == 7) ||
                                (skill == 6 && player_ptr.strength == 17) ||
                                (skill == 5 && player_ptr.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.gnome:
                        if (arg_0 == 2)
                        {
                            if ((skill == 6) ||
                                (skill == 5 && player_ptr.strength < 18))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.half_elf:
                        if (arg_0 == 0 &&
                            skill == 5)
                        {
                            ret_val = true;
                        }
                        else if (arg_0 == 2)
                        {
                            if (skill == 8 ||
                                (skill == 7 && player_ptr.strength == 17) ||
                                (skill == 6 && player_ptr.strength < 17))
                            {
                                ret_val = true;
                            }
                        }
                        break;

                    case Race.halfling:
                        if (arg_0 == 2)
                        {
                            if ((skill == 6) ||
                                (skill == 5 && player_ptr.strength == 17) ||
                                (skill == 4 && player_ptr.strength < 17))
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
            short arg_A, byte arg_C, byte arg_E, byte arg_10, byte arg_12, sbyte arg_14, byte arg_16,
            sbyte arg_18, sbyte arg_1A, byte arg_1C)
        {
            Item var_8;

            var_8 = new Item();

            var_8.name = string.Empty;
            var_8.next = null;
            var_8.type = arg_1C;
            var_8.field_2F = arg_1A;
            var_8.field_30 = arg_18;
            var_8.field_31 = arg_16;
            var_8.exp_value = arg_14;
            var_8.field_33 = arg_12;
            var_8.field_34 = arg_10;
            var_8.field_35 = arg_E;
            var_8.field_36 = arg_C;
            var_8.weight = arg_A;
            var_8.count = arg_8;
            var_8._value = arg_6;
            var_8.affect_1 = arg_4;
            var_8.affect_2 = arg_2;
            var_8.affect_3 = arg_0;

            return var_8;
        }


        internal static bool bandage(bool bandage_flag)
        {
            bool someone_bleeding = false;
            Player player_ptr;

            player_ptr = gbl.player_next_ptr;

            while (player_ptr != null)
            {
                if (player_ptr.actions.field_13 == 0 &&
                    player_ptr.combat_team == 0 &&
                    player_ptr.health_status == Status.dying)
                {
                    someone_bleeding = true;

                    if (bandage_flag == true)
                    {
                        player_ptr.health_status = Status.unconscious;
                        player_ptr.actions.bleeding = 0;

                        sub_67788(1, 10, "is bandaged", player_ptr);

                        bandage_flag = false;
                    }
                }
                player_ptr = player_ptr.next_player;
            }

            return someone_bleeding;
        }
    }
}
