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
                    var_C = (byte)gbl.unk_19AEC[player.spell_list[loop_var] & 0x7F].field_1;

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
                            var_C = (byte)gbl.unk_19AEC[(int)item.getAffect(loop_var) & 0x7f].field_1;

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
            byte var_1;

            for (var_1 = 0; var_1 < gbl.max_spells; var_1++)
            {
                if (player.spell_list[var_1] > 0x7F)
                {
                    player.spell_list[var_1] = 0;
                }
            }

            player.spell_to_learn_count = 0;
        }


        internal static void cancel_scribes(Player player)
        {
            Item item;

            item = player.itemsPtr;

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
            Player player;

            player = gbl.player_next_ptr;

            while (player != null)
            {
                cancel_memorize(player);
                cancel_scribes(player);
                player = player.next_player;
            }
        }


        internal static byte sub_4428E(sbyte arg_0, sbyte arg_2)
        {
            byte loop_var;
            byte var_2;
            byte var_1;

            var_2 = 0;

            for (loop_var = 0; loop_var < gbl.max_spells; loop_var++)
            {
                byte b = gbl.player_ptr.spell_list[loop_var];

                if (b > 0)
                {
                    b &= 0x7F;

                    if (gbl.unk_19AEC[b].field_1 == arg_2 &&
                        gbl.unk_19AEC[b].field_0 == arg_0)
                    {
                        var_2++;
                    }
                }
            }


            var_1 = (byte)(gbl.player_ptr.field_12CArray[(arg_0 * 5) + arg_2] - var_2);

            return var_1;
        }


        internal static bool sub_443A0(byte learn_type)
        {
            string var_2A;
            bool var_1;

            var_2A = string.Empty;

            if (learn_type == 1)
            {
                if (gbl.area_ptr.can_cast_spells == true)
                {
                    var_2A = "cannot cast spells in this area";
                }
            }
            else if (gbl.player_ptr.health_status == Status.animated ||
                gbl.player_ptr.in_combat == false)
            {
                var_2A = "is in no condition to ";

                switch (learn_type)
                {
                    case 1:
                        var_2A += "cast any spells";
                        break;

                    case 2:
                        var_2A += "memorize spells";
                        break;

                    case 3:
                        var_2A += "scribe any scrolls";
                        break;
                }
            }

            if (var_2A.Length == 0)
            {
                var_1 = true;
            }
            else
            {
                var_1 = false;
                ovr025.DisplayPlayerStatusString(true, 10, var_2A, gbl.player_ptr);
            }

            return var_1;
        }


        internal static void cast_spell()
        {
            byte var_6;
            bool var_5 = false; /* simeon */
            byte var_4;
            bool var_3;
            short var_2;

            var_4 = 0;
            var_2 = -1;

            gbl.dword_1D87F = null;

            gbl.byte_1D5BE = 1;

            if (sub_443A0(1) == true)
            {
                do
                {
                    var_6 = ovr020.spell_menu2(out var_3, ref var_2, 1, SpellLoc.memory);

                    if (var_6 != 0)
                    {
                        var_4 = 1;
                        seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

                        ovr023.sub_5D2E1(ref var_5, 1, 0, var_6);
                    }
                    else if (var_3 == true)
                    {
                        var_4 = 1;
                    }
                    else
                    {
                        ovr025.DisplayPlayerStatusString(true, 10, "has no spells memorized", gbl.player_ptr);
                    }
                } while (var_6 != 0);
            }

            if (var_4 != 0)
            {
                ovr025.load_pic();
            }
        }


        internal static byte sub_445D4()
        {
            string[] var_60 = new string[51];
            byte var_5F;
            byte var_5E;
            byte var_5D;
            sbyte var_5C;
            sbyte var_5B;
            byte[] var_5A = new byte[3];
            string var_2A = string.Empty;
            byte var_1;

            var_5F = 0;

            for (var_5B = 0; var_5B <= 2; var_5B++)
            {
                var_5A[var_5B] = 0;

                for (var_5C = 1; var_5C <= 5; var_5C++)
                {
                    var_60[(var_5C * 9) + (var_5B * 3)] = sub_4428E(var_5B, var_5C).ToString();

                    if (gbl.player_ptr.field_12CArray[var_5C + (var_5B * 5)] == 0)
                    {
                        var_60[(var_5B*3) + (var_5C * 9)] = " ";
                    }
                    else
                    {
                        var_5A[var_5B] = 1;
                        var_5F = 1;
                    }
                }
            }

            if (var_5F != 0)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "can memorize:", gbl.player_ptr);
                var_5E = 3;
                for (var_5B = 0; var_5B <= 2; var_5B++)
                {
                    if (var_5A[var_5B] != 0)
                    {
                        switch (var_5B)
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
                        for (var_5C = 1; var_5C <= 5; var_5C++)
                        {
                            seg041.displayString(var_60[(var_5C * 9) + (var_5B * 3)], 0, 10, var_5E + 0x11, var_5D + 1);
                            var_5D += 3;
                        }
                        var_5E++;
                    }
                }
            }

            var_1 = var_5F;

            return var_1;
        }


        internal static void rest_menu(ref byte arg_0)
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

            arg_0 = ovr021.reseting(1);

            gbl.unk_1D890.Clear();

            ovr025.camping_search();
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
                        else if (sub_4428E(gbl.unk_19AEC[var_4].field_0, gbl.unk_19AEC[var_4].field_1) > 0)
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
            byte var_6;
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
                        throw new System.NotSupportedException();//mov	al, [bp+var_4]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//cmp	es:[di+charStruct.hit_point_max], 0
                        throw new System.NotSupportedException();//jz	loc_44CB1
                        ovr025.string_print01("You already know that spell");
                        throw new System.NotSupportedException();//jmp	loc_44E21
                        throw new System.NotSupportedException();//loc_44CB1:
                        var_C = gbl.player_ptr.itemsPtr;
                        var_D = false;

                        while (var_C != null && var_D == false)
                        {
                            if (ovr023.item_is_scroll(var_C) == true)
                            {
                                for (var_6 = 1; var_6 <= 3; var_6++)
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
                            throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[var_4].field_1
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[var_4].field_0
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	si, ax
                            throw new System.NotSupportedException();//shl	ax, 1
                            throw new System.NotSupportedException();//shl	ax, 1
                            throw new System.NotSupportedException();//add	ax, si
                            throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//add	di, dx
                            throw new System.NotSupportedException();//cmp	es:[di+charStruct.field_12C], 0
                            throw new System.NotSupportedException();//ja	loc_44D8C
                            throw new System.NotSupportedException();//jmp	loc_44E0D
                            throw new System.NotSupportedException();//loc_44D8C:

                            var_C = gbl.player_ptr.itemsPtr;
                            throw new System.NotSupportedException();//loc_44DA0:
                            throw new System.NotSupportedException();//mov	ax, short ptr [bp+var_C]
                            throw new System.NotSupportedException();//or	ax, short ptr [bp+var_C+2]
                            throw new System.NotSupportedException();//jz	loc_44E0B
                            throw new System.NotSupportedException();//cmp	[bp+var_D], 0
                            throw new System.NotSupportedException();//jnz	loc_44E0B
                            var_6 = 1;
                            throw new System.NotSupportedException();//loc_44DB2:
                            throw new System.NotSupportedException();//mov	al, [bp+var_6]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//les	di, [bp+var_C]
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//mov	al, es:[di+3Bh]
                            throw new System.NotSupportedException();//cmp	al, [bp+var_4]
                            throw new System.NotSupportedException();//jnz	loc_44DE9
                            throw new System.NotSupportedException();//mov	al, [bp+var_6]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//les	di, [bp+var_C]
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//mov	al, es:[di+3Bh]
                            throw new System.NotSupportedException();//or	al, 0x80
                            throw new System.NotSupportedException();//mov	dl, al
                            throw new System.NotSupportedException();//mov	al, [bp+var_6]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//les	di, [bp+var_C]
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//mov	es:[di+3Bh], dl
                            var_D = true;
                            throw new System.NotSupportedException();//loc_44DE9:
                            var_6++;
                            throw new System.NotSupportedException();//cmp	[bp+var_6], 3
                            throw new System.NotSupportedException();//ja	loc_44DF8
                            throw new System.NotSupportedException();//cmp	[bp+var_D], 0
                            throw new System.NotSupportedException();//jz	loc_44DB2
                            throw new System.NotSupportedException();//loc_44DF8:
                            throw new System.NotSupportedException();//les	di, [bp+var_C]
                            throw new System.NotSupportedException();//mov	ax, es:[di+2Ah]
                            throw new System.NotSupportedException();//mov	dx, es:[di+2Ch]
                            throw new System.NotSupportedException();//mov	short ptr [bp+var_C], ax
                            throw new System.NotSupportedException();//mov	short ptr [bp+var_C+2], dx
                            throw new System.NotSupportedException();//jmp	short loc_44DA0
                            throw new System.NotSupportedException();//loc_44E0B:
                            throw new System.NotSupportedException();//jmp	short loc_44E21
                            throw new System.NotSupportedException();//loc_44E0D:
                            ovr025.string_print01("You can not scribe that spell.");
                        }
                    }
                    throw new System.NotSupportedException();//loc_44E21:
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
                                if (gbl.unk_19AEC[var_12].field_A == var_11)
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
            seg037.draw8x8_01();

            ovr027.sl_select_item(out var_C, ref var_42, ref var_17, true, var_8,
                0x16, 0x26, 4, 1, 15, 10, 11, string.Empty, string.Empty);

            ovr027.free_stringList(ref var_8);
            ovr025.load_pic();
        }


        internal static void magic_menu(ref byte arg_0)
        {
            bool var_2;
            char var_1;

            var_1 = ' ';

            while (arg_0 == 0 && unk_45CD7.MemberOf(var_1) == false)
            {
                var_1 = ovr027.displayInput(out var_2, 1, 1, 15, 10, 13, "Cast Memorize Scribe Display Rest Exit", string.Empty);

                if (var_2 == true)
                {
                    ovr020.sub_572CF(var_1);
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
                            rest_menu(ref arg_0);
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

        internal static void reorder_party()
        {
            char var_3;
            bool var_2;
            byte var_1;

            var_1 = 0;
            var_3 = ' ';

            while (unk_45CD7.MemberOf(var_3) == false)
            {
                var_3 = ovr027.displayInput(out var_2, 1, 1, 15, 10, 13, seg600_04A6[var_1], "Party Order: ");
                throw new System.NotSupportedException();//cmp	[bp+var_2], 0
                throw new System.NotSupportedException();//jz	loc_45909
                throw new System.NotSupportedException();//cmp	[bp+var_1], 0
                throw new System.NotSupportedException();//jnz	loc_458E5
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//call	ovr020.sub_572CF
                ovr025.Player_Summary(gbl.player_ptr);
                throw new System.NotSupportedException();//jmp	short loc_45907
                throw new System.NotSupportedException();//loc_458E5:
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//cmp	al, 0x47
                throw new System.NotSupportedException();//jnz	loc_458F2
                sub_4558D();
                throw new System.NotSupportedException();//jmp	short loc_458FA
                throw new System.NotSupportedException();//loc_458F2:
                throw new System.NotSupportedException();//cmp	al, 0x4F
                throw new System.NotSupportedException();//jnz	loc_458FA
                sub_456E5();
                throw new System.NotSupportedException();//loc_458FA:
                ovr025.Player_Summary(gbl.player_ptr);
                throw new System.NotSupportedException();//loc_45907:
                throw new System.NotSupportedException();//jmp	short loc_45954
                throw new System.NotSupportedException();//loc_45909:
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//mov	di, offset unk_45832
                throw new System.NotSupportedException();//push	cs
                throw new System.NotSupportedException();//push	di
                throw new System.NotSupportedException();//call	Set__MemberOf(Byte)
                throw new System.NotSupportedException();//jz	loc_45954
                throw new System.NotSupportedException();//cmp	[bp+var_1], 0
                throw new System.NotSupportedException();//mov	al, 0
                throw new System.NotSupportedException();//jnz	loc_45922
                throw new System.NotSupportedException();//inc	ax
                throw new System.NotSupportedException();//loc_45922:
                throw new System.NotSupportedException();//mov	[bp+var_1], al

                if (var_1 != 0)
                {
                    ovr025.DisplayPlayerStatusString(false, 10, "has been selected", gbl.player_ptr);
                }
                else
                {
                    ovr025.ClearPlayerTextArea();
                }
                throw new System.NotSupportedException();//loc_45954:
            }

        }


        internal static void drop_player()
        {
            if (gbl.player_ptr.next_player == null &&
                gbl.player_ptr == gbl.player_next_ptr)
            {
                if (ovr027.yes_no(15, 10, 14, "quit TO DOS: ") == 'Y')
                {
                    ovr018.free_players(1, false);
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

                    ovr018.free_players(1, false);
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

                var_1 = ovr027.displayInput(out var_2, 1, 1, 15, 10, 13, var_2B, "Game Speed:");

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
                var_1 = ovr027.displayInput(out var_3, 1, 1, 15, 10, 13, "Order Drop Speed Icon Pics Exit", "Alter: ");

                if (var_3 == true)
                {
                    ovr020.sub_572CF(var_1);
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

                                var_2 = ovr027.displayInput(out var_3, 1, 0, 15, 10, 13, var_2C, string.Empty);

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
            bool var_1;

            if (player.health_status == Status.okey)
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
            }

            return var_1;
        }


        internal static void sub_45F22(ref short bp_var_2)
        {
            Player player;
            byte var_3;
            short var_2;

            var_2 = 0;

            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player_is_okey(player) == true)
                {
                    for (var_3 = 0; var_3 < gbl.max_spells; var_3++)
                    {
                        switch (player.spell_list[var_3])
                        {
                            case 3:
                                var_2 += ovr024.roll_dice(8, 1);
                                break;

                            case 0x3A:
                                var_2 += (short)(ovr024.roll_dice(8, 2) + 1);
                                break;

                            case 0x47:
                                var_2 += (short)(ovr024.roll_dice(8, 3) + 3);
                                break;
                        }
                    }
                }

                player = player.next_player;
            }

            bp_var_2 = var_2;
        }


        internal static void sub_45FDD(ref short bp_var_2, RestTime bp_var_16)
        {
            int var_3;
            short var_2;

            var_2 = 0;

            for (var_3 = 1; var_3 <= bp_var_16.field_C; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 1);
            }

            for (var_3 = 1; var_3 <= bp_var_16.field_A; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 2);
                var_2 += 1;
            }

            for (var_3 = 1; var_3 <= bp_var_16.field_8; var_3++)
            {
                var_2 += ovr024.roll_dice(8, 3);
                var_2 += 3;
            }

            bp_var_2 += var_2;
        }


        internal static short sub_4608F()
        {
            Player player;
            short var_4;

            var_4 = 0;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                var_4 += (short)(player.hit_point_max - player.hit_point_current);
                player = player.next_player;
            }

            return var_4;
        }


        internal static void sub_460ED(RestTime bp_var_16)
        {
            byte var_11;
            short var_10;
            short var_E;
            short var_C;
            short var_A;
            short var_8;
            Player player_ptr;
            short var_2 = 0; /* Simeon */

            player_ptr = gbl.player_next_ptr;
            var_A = 0;
            var_C = 0;
            var_E = 0;

            bp_var_16.field_C = 0;
            bp_var_16.field_A = 0;
            bp_var_16.field_8 = 0;
            var_8 = 0;

            while (player_ptr != null)
            {
                var_10 = 0;

                if (player_is_okey(player_ptr) == true)
                {
                    bp_var_16.field_C += player_ptr.field_12D[0];
                    var_A = (short)(player_ptr.field_12D[0] * 15);

                    bp_var_16.field_A += player_ptr.field_12D[3];
                    var_C = (short)(player_ptr.field_12D[3] * 0x3C);

                    bp_var_16.field_8 += player_ptr.field_12D[4];
                    var_E = (short)(player_ptr.field_12D[4] * 0x4B);
                }

                if (var_A > 0)
                {
                    var_10 = 0x00F0;
                    var_2 += 0x1B;
                }

                if ((var_C + var_E) > 0)
                {
                    var_10 = 0x168;

                    if (var_E > 0)
                    {
                        var_2 += 0x4E;
                    }
                    else
                    {
                        var_2 += 0x22;
                    }
                }

                var_10 += (short)(var_A + var_C + var_E);

                if (var_8 < var_10)
                {
                    var_8 = var_10;
                }

                player_ptr = player_ptr.next_player;
            }

            if (sub_4608F() < var_2)
            {
                var_11 = (byte)(var_2 / sub_4608F());

                var_8 /= var_11;
            }

            gbl.unk_1D890.field_6 = (ushort)(var_8 / 60);

            gbl.unk_1D890.field_4 = (ushort)((var_8 - (gbl.unk_1D890.field_6 * 60)) / 10);

            gbl.unk_1D890.field_2 = (ushort)(var_8 % 10);
        }


        internal static void sub_46280(RestTime bp_var_16)
        {
            ushort var_6;
            Player player;

            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.hit_point_max > player.hit_point_current)
                {
                    var_6 = (byte)(player.hit_point_max - player.hit_point_current);

                    if (var_6 > bp_var_16.field_C)
                    {
                        var_6 = bp_var_16.field_C;
                    }

                    if (var_6 >= 1)
                    {
                        var_6 = 0;
                    }

                    if (var_6 > 0 &&
                        ovr024.heal_player(0, (byte)var_6, player) == true &&
                        var_6 <= bp_var_16.field_C)
                    {
                        bp_var_16.field_C -= var_6;
                    }
                }
                player = player.next_player;
            }
        }


        internal static void fix_menu(ref byte arg_0)
        {
            RestTime var_16;
            short var_2;

            arg_0 = 0;
            var_2 = 0;

            if (sub_4608F() != 0)
            {
                sub_45F22(ref var_2);

                if (sub_4608F() == 0)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                    ovr025.camping_search();
                }
                else
                {
                    var_16 = gbl.unk_1D890;

                    sub_460ED(var_16);

                    arg_0 = ovr021.reseting(0);

                    if (arg_0 == 0)
                    {
                        sub_45FDD(ref var_2, var_16);
                        sub_46280(var_16);
                        ovr025.Player_Summary(gbl.player_ptr);
                        ovr025.camping_search();

                        gbl.unk_1D890 = var_16;
                    }
                }
            }
        }

        static Set unk_463F4 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void make_camp(ref byte arg_0)
        {
            string var_104;
            bool var_4;
            bool var_3;
            char var_2;
            byte var_1;

            var_1 = gbl.game_state;
            gbl.game_state = 2;
            gbl.word_1D8A6 = 0;

            gbl.unk_1D890.Clear();

            gbl.byte_1D5AB = gbl.byte_1D5A2;
            gbl.byte_1D5B5 = gbl.byte_1D5B4;

            ovr025.load_pic();
            seg037.draw8x8_clear_area(0x16, 0x26, 0x11, 1);

            seg041.displayString("The party makes camp...", 0, 10, 18, 1);
            cancel_spells();
            arg_0 = 0;
            var_2 = ' ';


            while (arg_0 == 0 &&
                unk_463F4.MemberOf(var_2) == false)
            {

                var_2 = ovr027.displayInput(out var_4, 1, 1, 15, 10, 13, "Save View Magic Rest Alter Fix Exit", "Camp:");

                if (var_4 == true)
                {
                    ovr020.sub_572CF(var_2);
                    ovr025.Player_Summary(gbl.player_ptr);
                }
                else
                {
                    switch (var_2)
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
                            ovr020.viewPlayer(out var_3);
                            break;

                        case 'M':
                            gbl.byte_1D5BE = 1;
                            magic_menu(ref arg_0);
                            break;

                        case 'R':
                            gbl.byte_1D5BE = 1;
                            rest_menu(ref arg_0);
                            break;

                        case 'F':
                            fix_menu(ref arg_0);
                            break;

                        case 'A':
                            gbl.byte_1D5BE = 1;
                            alter_menu();
                            break;
                    }
                }
            }

            if (seg051.Copy(3, 1, gbl.byte_1D5AB, out var_104) == "PIC")
            {
                ovr030.load_pic_final(ref gbl.byte_1D556, 0, gbl.byte_1D5B5, gbl.byte_1D5AB);
            }

            cancel_spells();
            gbl.dword_1D87F = null;
            gbl.game_state = var_1;
            ovr025.camping_search();
            ovr025.ClearPlayerTextArea();
            ovr027.redraw_screen();
        }
    }
}
