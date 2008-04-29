using Classes;

namespace engine
{
    class ovr013
    {
        internal static void sub_3A019(byte arg_0)
        {
            if (arg_0 == 0 ||
                gbl.byte_1D2BD == arg_0)
            {
                gbl.byte_1D2BE = 0;
                gbl.byte_1D2BD = 0;
            }
        }


        internal static bool addAffect(ushort arg_0, byte arg_2, Affects affect_type, Player player)
        {
            bool var_1;

            if (gbl.byte_1D2C6 == true)
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
                ovr024.add_affect(true, arg_2, arg_0, affect_type, player);
            }
            return var_1;
        }


        internal static void sub_3A071(byte arg_0, object param, Player player)
        {
            ovr025.clear_actions(player);
        }


        internal static void sub_3A087(byte arg_0, object param, Player player)
        {
            gbl.saving_throw_roll++;
            gbl.byte_1D2C9++;
        }


        internal static void sub_3A096(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2CC += 5;
            gbl.byte_1D2C9++;
        }


        internal static void sub_3A0A6(byte arg_0, object param, Player player)
        {
            if (gbl.byte_1D2CC < 5)
            {
                gbl.byte_1D2CC = 0;
            }
            else
            {
                gbl.byte_1D2CC -= 5;
            }
            gbl.byte_1D2C9--;
        }


        internal static void sub_3A0DC(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            byte var_1 = (byte)(player.field_19D + player.field_19C);

            if (affect.field_3 > var_1)
            {
                affect.field_3 -= var_1;
            }
            else
            {
                ovr024.remove_affect(null, Affects.affect_03, player);
            }

            ovr025.sub_6818A("is fighting with snakes", 1, player);
            ovr025.ClearPlayerTextArea();

            ovr025.clear_actions(player);
        }


        internal static void sub_3A15F(byte arg_0, object param, Player player)
        {
            if ((gbl.player_ptr.field_14B & 1) != 0)
            {
                gbl.byte_1D2C9 -= 7;
            }
        }


        internal static void sub_3A17A(byte arg_0, object param, Player player)
        {
            sbyte var_1;

            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.field_11A == 10)
            {
                var_1 = 1;
            }
            else if (gbl.spell_target.field_11A == 9 || gbl.spell_target.field_11A == 12)
            {
                var_1 = 2;
            }
            else if (gbl.spell_target.field_11A == 4)
            {
                var_1 = 3;
            }
            else
            {
                var_1 = 0;
            }

            gbl.byte_1D2C9 += var_1;
            gbl.byte_1D2BE += (byte)var_1;
            gbl.byte_1D2BF = 9;
        }


        internal static void sub_3A1DF(byte arg_0, object param, Player player)
        {
            if (player.ac < 0x3A)
            {
                player.ac += 2;
            }
            else
            {
                player.ac = 0x3C;
            }

            if (player.field_19B < 0x3A)
            {
                player.field_19B += 2;
            }
            else
            {
                player.field_19B = 0x3C;
            }
        }


        internal static void sub_3A224(byte arg_0, object param, Player player)
        {
            if (gbl.player_ptr.alignment == 2 ||
                gbl.player_ptr.alignment == 5 ||
                gbl.player_ptr.alignment == 8)
            {
                gbl.saving_throw_roll += 2;
                gbl.byte_1D2C9 -= 2;
            }
        }


        internal static void sub_3A259(byte arg_0, object param, Player player)
        {
            if (gbl.player_ptr.alignment == 0 ||
                gbl.player_ptr.alignment == 3 ||
                gbl.player_ptr.alignment == 6)
            {
                gbl.saving_throw_roll += 2;
                gbl.byte_1D2C9 -= 2;
            }
        }


        internal static void sub_3A28E(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 2) != 0)
            {
                gbl.byte_1D2BE >>= 1;
                gbl.saving_throw_roll += 3;
            }
        }


        internal static void sub_3A2AD(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (arg_0 != 0)
            {
                player.combat_team = (sbyte)((affect.field_3 & 0x40) >> 6);

                if (player.field_F7 == 0xB3)
                {
                    player.field_F7 = 0;
                }
            }
            else
            {
                if ((affect.field_3 & 0x20) == 0)
                {
                    affect.field_3 = (byte)(20 + (player.combat_team << 6));

                    player.combat_team = (sbyte)(affect.field_3 >> 7);
                    player.field_198 = 1;

                    if (player.field_F7 <= 0x7F)
                    {
                        player.field_F7 = 0x0B3;
                    }

                    player.actions.target = null;
                    ovr025.count_teams();
                }
                gbl.byte_1D2CC = 100;
            }
        }


        internal static void Suffocates(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (affect.field_3 == 0)
            {
                ovr024.sub_63014("Suffocates", Status.dead, player);
            }
            else
            {
                affect.field_3--;
            }
        }


        internal static void sub_3A3BC(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(10, affect.field_3, Affects.affect_0f, player) == true &&
                player.hit_point_current > 1)
            {
                gbl.byte_1D2BF = 0;

                ovr024.damage_person(false, 0, 1, player);

                if (gbl.game_state != 5)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                }
            }
        }


        internal static void sub_3A41F(byte arg_0, object param, Player player)
        {
            if (player.ac < 0x39)
            {
                player.ac = 0x39;
            }

            gbl.saving_throw_roll++;

            if (gbl.spell_id == 15)
            {
                gbl.byte_1D2BE = 0;
            }
        }


        internal static void sub_3A44A(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;
            //HACK - why could gbl.spell_target be null?
            if (gbl.spell_target != null && (gbl.spell_target.field_14B & 2) != 0)
            {
                gbl.byte_1D2C9++;
            }
        }


        internal static void sub_3A480(byte arg_0, object param, Player player)
        {
            if (arg_0 == 0 &&
                (gbl.byte_1D2BF & 1) != 0)
            {
                gbl.byte_1D2BE >>= 1;
                gbl.saving_throw_roll += 3;
            }
        }


        internal static void is_silenced1(byte arg_0, object param, Player player)
        {
            if (player.actions.field_2 != 0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is silenced", player);
            }

            player.actions.field_2 = 0;
            player.actions.can_cast = 0;
        }


        internal static void sub_3A517(byte arg_0, object param, Player player)
        {
            Affect var_4;

            if (ovr025.find_affect(out var_4, Affects.poisoned, player) == true)
            {
                ovr024.sub_63014("dies from poison", Status.dead, player);
            }

            gbl.byte_1D2C6 = true;

            ovr024.remove_affect(null, Affects.affect_0f, player);

            gbl.byte_1D2C6 = false;
        }


        internal static void sub_3A583(byte arg_0, object param, Player player)
        {
            bool item_found;
            Item item;

            item = player.itemsPtr;
            item_found = false;

            while (item != null && item_found == false)
            {
                if (item.type == 0x14 &&
                    item.field_31 == 0xf3)
                {
                    item_found = true;
                }
                else
                {
                    item = item.next;
                }
            }

            if (arg_0 != 0 && item != null)
            {
                ovr025.lose_item(item, player);
            }

            if (arg_0 == 0 &&
                item_found == false &&
                player.field_14C < 0x10)
            {
                item = new Item();
                item.type = 20;
                item.field_30 = 20;
                item.field_31 = 243;
                item.exp_value = 1;
                item.affect_2 = Affects.spiritual_hammer;
                item.affect_3 = (Affects)160;

                ovr025.addItem(item, player);
                ovr020.ready_Item(item);

                seg051.FreeMem(Item.StructSize, item);

                ovr025.DisplayPlayerStatusString(true, 10, "Gains an item", player);
            }

            ovr025.sub_66C20(player);
        }


        internal static void sub_3A6C6(byte arg_0, object param, Player player)
        {
            Affect var_4;

            if (player.name.Length == 0 &&
                ovr025.find_affect(out var_4, Affects.detect_invisibility, gbl.player_ptr) == false)
            {
                gbl.byte_1D2C5 = 1;
                gbl.byte_1D2C9 -= 4;
            }
        }


        internal static void sub_3A6FB(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 4) != 0)
            {
                gbl.byte_1D2C9++;
            }
        }


        internal static void sub_3A73F(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.roll_dice((byte)((affect.field_3 >> 4) + 1), 1) > 1 &&
                gbl.spell_id > 0 &&
                gbl.byte_1D2C7 == 0)
            {
                sub_3A019(0);

                ovr025.DisplayPlayerStatusString(true, 10, "lost an image", player);

                affect.field_3 -= 1;

                if (affect.field_3 == 0)
                {
                    ovr024.remove_affect(null, Affects.mirror_image, player);
                }
            }
        }


        internal static void sub_3A7C0(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2BE -= (byte)(gbl.byte_1D2BE / 4);
        }


        internal static void sub_3A7E8(byte arg_0, object param, Player player)
        {
            if (player.actions.field_2 != 0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is coughing", player);
            }

            player.actions.field_2 = 0;
            player.actions.can_cast = 0;

            ovr025.sub_66C20(player);
            
            if( player.field_19B > 0x34 )
            {
                player.field_19B -= 2;
            }
            else
            {
                player.field_19B = 0x32;
            }

            player.ac = player.field_19B;

            if (player == gbl.player_ptr)
            {
                ovr025.hitpoint_ac(player);
            }
        }


        internal static void sub_3A89E(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            affect.call_spell_jump_list = false;

            if (gbl.byte_1D2C6 == false)
            {
                ovr024.sub_63014("collapses", Status.dead, player);
            }

            player.combat_team = (sbyte)(affect.field_3 >> 4);
            player.field_198 = 1;
            player.field_E9 = 0;

            player.field_DD = (byte)(player.fighter_lvl + (player.field_113 * ovr026.sub_6B3D1(player)));
            player.field_E4 = 0x0C;

            if (player.field_F7 == 0xB3)
            {
                player.field_F7 = 0;
            }

            player.field_11A = 0;
        }


        internal static void sub_3A951(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2C9 -= 4;

            player.ac -= 4;
            player.field_19B -= 4;

            gbl.saving_throw_roll -= 4;
        }


        internal static void sub_3A974(byte arg_0, object param, Player player)
        {
            ovr024.sub_630C7(arg_0, param, player, Affects.affect_2b);
            ovr024.sub_630C7(arg_0, param, player, Affects.cause_disease_2);
        }


        internal static void sub_3A9D9(byte arg_0, object arg_2, Player player)
        {
            byte var_1;


            var_1 = ovr024.roll_dice(100, 1);

            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cmp	al, 1
            throw new System.NotSupportedException();//jnb	loc_3A9F7
            throw new System.NotSupportedException();//jmp	loc_3AA7E
            throw new System.NotSupportedException();//loc_3A9F7:
            throw new System.NotSupportedException();//cmp	al, 0x0A
            throw new System.NotSupportedException();//jbe	loc_3A9FE
            throw new System.NotSupportedException();//jmp	loc_3AA7E
            throw new System.NotSupportedException();//loc_3A9FE:
            ovr024.remove_affect(null, Affects.confuse, player);
            player.actions.field_10 = 1;
            player.field_198 = 1;

            if (player.field_F7 <= 0x7f)
            {
                player.field_F7 = 0xb3;
            }

            player.actions.target = null;

            ovr024.is_unaffected("runs away", false, 1, true, 0, 10, Affects.affect_8e, player);
            throw new System.NotSupportedException();//jmp	loc_3AB3B
            throw new System.NotSupportedException();//loc_3AA7E:
            throw new System.NotSupportedException();//cmp	al, 0x0B
            throw new System.NotSupportedException();//jb	loc_3AABD
            throw new System.NotSupportedException();//cmp	al, 0x3C
            throw new System.NotSupportedException();//ja	loc_3AABD

            ovr025.sub_6818A("is confused", 1, player);
            ovr025.ClearPlayerTextArea();
            sub_3A071(0, arg_2, player);
            throw new System.NotSupportedException();//jmp	short loc_3AB3B
            throw new System.NotSupportedException();//loc_3AABD:
            throw new System.NotSupportedException();//cmp	al, 0x3D
            throw new System.NotSupportedException();//jb	loc_3AB11
            throw new System.NotSupportedException();//cmp	al, 0x50
            throw new System.NotSupportedException();//ja	loc_3AB11
            ovr024.is_unaffected("goes berserk", false, 1, true, (byte)player.combat_team, 1, Affects.affect_89, player);
            ovr024.sub_630C7(0, null, player, Affects.affect_89);
            throw new System.NotSupportedException();//jmp	short loc_3AB3B
            throw new System.NotSupportedException();//loc_3AB11:
            throw new System.NotSupportedException();//cmp	al, 0x51
            throw new System.NotSupportedException();//jb	loc_3AB3B
            throw new System.NotSupportedException();//cmp	al, 0x64
            throw new System.NotSupportedException();//ja	loc_3AB3B

            ovr025.sub_6818A("is enraged", 1, player);
            ovr025.ClearPlayerTextArea();
            throw new System.NotSupportedException();//loc_3AB3B:

            if (ovr024.do_saving_throw(-2, 4, player) == true)
            {
                ovr024.remove_affect(null, Affects.confuse, player);
            }
            throw new System.NotSupportedException();//func_end:
        }


        internal static void sub_3AB6F(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2C9 -= 4;
            gbl.saving_throw_roll -= 4;
        }


        internal static void has_action_timedout(byte arg_0, object param, Player player)
        {
            if (player.actions.delay == 0)
            {
                gbl.byte_1D2C5 = 1;
                gbl.byte_1D2C9 = -1;
            }
        }


        internal static void spl_age(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if ((affect.field_3 & 0x10) != 0)
            {
                affect.field_3 += 0x10;

                ovr025.DisplayPlayerStatusString(true, 10, "ages", player);
                player.age++;
            }

            gbl.byte_1D2C0 *= 2;
        }


        internal static void sub_3AC1D(byte arg_0, object param, Player player)
        {
            sbyte var_D;
            sbyte var_C;
            byte var_B;
            byte var_A;
            bool var_9;
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            Affect affect = (Affect)param;

            var_A = (byte)(affect.field_3 >> 4);

            var_9 = false;
            var_8 = gbl.stru_1D885;

            while (var_8 != null && var_9 == false)
            {
                if (var_8.player == player &&
                    var_8.field_1C == var_A)
                {
                    var_9 = true;
                }
                else
                {
                    var_8 = var_8.next;
                }
            }

            if (var_9 == true)
            {
                ovr025.string_print01("The air clears a little...");

                for (var_B = 1; var_B <= 4; var_B++)
                {
                    if (var_8.field_10[var_B] != 0)
                    {
                        var_C = (sbyte)(var_8.field_1A + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_B]]);
                        var_D = (sbyte)(var_8.field_1B + gbl.MapDirectionYDelta[gbl.unk_18AE9[var_B]]);

                        var_9 = false;

                        for (var_A = 1; var_A <= gbl.byte_1D1BB; var_A++)
                        {
                            if (gbl.unk_1D183[var_A].field_0 != null &&
                                gbl.unk_1D183[var_A].mapX == var_C &&
                                gbl.unk_1D183[var_A].mapY == var_D)
                            {
                                var_9 = true;
                            }
                        }

                        if (var_9 == true)
                        {
                            gbl.mapToBackGroundTile[var_C, var_D] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[var_C, var_D] = var_8.field_7[var_B];
                        }
                    }
                }

                var_4 = gbl.stru_1D885;

                if (var_4 == var_8)
                {
                    gbl.stru_1D885 = var_4.next;
                }
                else
                {
                    while (var_4.next != var_8)
                    {
                        var_4 = var_4.next;
                    }
                }

                var_4.next = var_8.next;

                seg051.FreeMem(0x1E, var_8);
                var_4 = gbl.stru_1D885;

                while (var_4 != null)
                {
                    for (var_B = 1; var_B <= 4; var_B++)
                    {
                        if (var_4.field_10[var_B] != 0)
                        {
                            int cx = gbl.MapDirectionXDelta[gbl.unk_18AE9[var_B]] + var_4.field_1A;
                            int ax = gbl.MapDirectionYDelta[gbl.unk_18AE9[var_B]] + var_4.field_1B;

                            gbl.mapToBackGroundTile[cx, ax] = 0x1E;
                        }
                    }

                    var_4 = var_4.next;
                }
            }
        }


        internal static void sub_3AF06(byte arg_0, Player player)
        {
            if (gbl.player_ptr.field_151 != null &&
                ovr025.getTargetRange(player, gbl.player_ptr) == 0 &&
                ovr024.roll_dice(100, 1) <= arg_0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "Avoids it", player);
                gbl.byte_1D2BE = 0;
                gbl.byte_1D2C9 = -1;
                gbl.byte_1D2CA--;
            }
        }


        internal static Item sub_3AF77(Player player)
        {
            bool var_9;
            Item var_8;

            var_8 = null;

            if (player.field_151 != null)
            {
                var_9 = ovr025.sub_6906C(out var_8, player);

                if (var_9 == false ||
                    var_8 == null)
                {
                    var_8 = player.field_151;
                }
            }

            return var_8;
        }


        internal static void sub_3AFE0(byte arg_0, object param, Player player)
        {
            Item item = sub_3AF77(gbl.player_ptr);

            if (item != null && item.exp_value == 0)
            {
                sub_3AF06(100, player);
            }
        }


        internal static void sub_3B01B(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2C0 /= 2;
        }


        internal static void weaken(byte arg_0, object param, Player player)
        {
            Affect var_4;
            Affect affect = (Affect)param;


            if (addAffect(0x3c, affect.field_3, Affects.affect_2b, player) == true)
            {
                if (player.strength > 3)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is weakened", player);
                    player.strength--;
                }
                else
                {
                    if (ovr025.find_affect(out var_4, Affects.helpless, player) == true)
                    {
                        ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                    }
                }
            }
        }


        internal static void sub_3B0C2(byte arg_0, object param, Player player)
        {
            Affect var_4;
            Affect affect = (Affect)param;

            if (addAffect(10, affect.field_3, Affects.cause_disease_2, player) == true)
            {
                if (player.hit_point_current > 1)
                {
                    gbl.byte_1D2BF = 0;

                    ovr024.damage_person(false, 0, 1, player);

                    if (gbl.game_state != 5)
                    {
                        ovr025.Player_Summary(gbl.player_ptr);
                    }
                }
                else
                {
                    if (ovr025.find_affect(out var_4, Affects.helpless, player) == false)
                    {
                        ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                    }
                }
            }
        }


        internal static void sub_3B153(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.player_ptr.field_11A == 2 ||
                gbl.player_ptr.field_11A == 10)
            {
                if ((gbl.player_ptr.field_DE & 0x7F) == 2)
                {
                    gbl.byte_1D2C9 -= 4;
                }
            }
        }


        internal static void sub_3B1A2(byte arg_0, object param, Player player)
        {
            if (gbl.player_ptr.field_11A == 1 &&
                (gbl.player_ptr.field_DE & 0x7F) == 2)
            {
                gbl.byte_1D2C9 -= 4;
            }
        }


        internal static void sub_3B1C9(byte arg_0, object param, Player player)
        {
            byte var_1;
            Affect affect = (Affect)param;

            var_1 = (byte)((affect.field_3 & 0x10) >> 4);

            if (player.combat_team == var_1)
            {
                sub_3A087(arg_0, affect, player);
            }
            else
            {
                gbl.byte_1D2C9 -= 1;
                gbl.saving_throw_roll -= 1;
            }
        }


        internal static void sub_3B212(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 2) != 0)
            {
                gbl.saving_throw_roll += 2;
            }
            else if ((gbl.byte_1D2BF & 1) != 0 && gbl.save_made == false)
            {
                gbl.byte_1D2BE <<= 1;
            }
        }


        internal static void sub_3B243(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 1) != 0)
            {
                gbl.saving_throw_roll += 2;
            }
            else if ((gbl.byte_1D2BF & 2) != 0 && gbl.byte_1C01B == 0)
            {
                gbl.byte_1D2BE <<= 1;
            }
        }


        internal static void sub_3B27B(byte arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
        }


        internal static void sub_3B29A(byte arg_0, object param, Player player)
        {
            player.actions.move = 0;

            if (gbl.byte_1D2C4 != 0)
            {
                gbl.byte_1D2C0 = 0;
            }
        }


        internal static void sub_3B2BA(byte arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0, Affects.affect_62, player);
        }


        internal static void sub_3B2D8(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 == null ||
                var_4.exp_value == 0)
            {
                gbl.byte_1D2BE = 0;
            }
            else if (var_4 != null &&
                var_4.exp_value < 3)
            {
                gbl.byte_1D2BE >>= 1;
            }
        }


        internal static void sub_3B32B(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 1) != 0)
            {
                for (int i = 1; i <= gbl.dice_count; i++)
                {
                    gbl.byte_1D2BE -= 2;

                    if (gbl.byte_1D2BE < gbl.dice_count)
                    {
                        gbl.byte_1D2BE = (byte)gbl.dice_count;
                    }
                }

                gbl.saving_throw_roll += 4;

                if ((gbl.byte_1D2BF & 8) == 0)
                {
                    sub_3A019(0);
                }
            }
        }


        internal static void sub_3B386(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(0x3C, affect.field_3, Affects.affect_3e, player) == true &&
                ovr024.heal_player(1, 1, player) == true)
            {
                ovr025.describeHealing(player);
            }
        }


        internal static void sub_3B3CA(byte arg_0, object param, Player player)
        {
            if (gbl.spell_id > 0 &&
                gbl.unk_19AEC[gbl.spell_id].spellLevel < 4)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3B407(sbyte arg_0, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(arg_0, 0, gbl.spell_target) == false)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", gbl.spell_target);
                seg041.GameDelay();
                ovr024.add_affect(false, 0xff, 0, Affects.poisoned, gbl.spell_target);

                ovr024.sub_63014("is killed", Status.dead, gbl.spell_target);
            }
        }


        internal static void sub_3B4AE(byte arg_0, ushort affect, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(0, 0, gbl.spell_target) == false)
            {
                ovr025.sub_6818A("is Paralyzed", 1, gbl.spell_target);
                ovr024.add_affect(false, 12, affect, Affects.paralyze, gbl.spell_target);
            }
        }


        internal static void sub_3B520(byte arg_0, object param, Player player)
        {
            sub_3B407(0, player);
        }


        internal static void sub_3B534(byte arg_0, object param, Player player)
        {
            sub_3B407(4, player);
        }


        internal static void sub_3B548(byte arg_0, object param, Player player)
        {
            sub_3B407(2, player);
        }


        internal static void sub_3B55C(byte arg_0, object param, Player player)
        {
            sub_3B4AE(0, ovr024.roll_dice(8, 2), player);
        }


        internal static void spell_stupid(byte arg_0, object param, Player player)
        {
            player._int = 7;
            player.wis = 7;

            ovr025.DisplayPlayerStatusString(true, 10, "is stupid", player);

            if (gbl.game_state == 5)
            {
                player.actions.can_cast = 0;
                if (player.actions.spell_id > 0)
                {

                    ovr025.DisplayPlayerStatusString(true, 12, "lost a spell", player);

                    ovr025.clear_spell(player.actions.spell_id, player);
                    player.actions.spell_id = 0;
                }
            }
        }


        internal static void sub_3B636(byte arg_0, object param, Player player)
        {
            Affect var_4;

            if (gbl.player_ptr.field_11A == 0x13)
            {
                if (ovr025.find_affect(out var_4, Affects.detect_invisibility, gbl.player_ptr) == false)
                {
                    gbl.byte_1D2C5 = 1;
                }

                gbl.byte_1D2C9 -= 4;
            }
        }


        internal static void sub_3B671(byte arg_0, object param, Player player)
        {
            sub_3B407(-2, player);
        }


        internal static void sub_3B685(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2C5 = 1;
            gbl.byte_1D2C9 -= 4;
        }


        internal static void sub_3B696(byte arg_0, object param, Player player)
        {
            if (ovr024.roll_dice(100, 1) <= 95)
            {
                ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
            }
        }


        internal static void sub_3B6D2(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 0x20) > 0)
            {
                sub_3A019(0);
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", player);
            }
        }


        internal static void sub_3B70E(byte arg_0, object param, Player player)
        {

        }


        internal static void sub_3B71A(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.field_11A == 3)
            {
                gbl.byte_1D2BE = (byte)((ovr024.roll_dice(12, 1) * 3) + 4 + ovr025.strengthDamBonus(player));
                gbl.byte_1D2C9 += 2;
            }
        }


        internal static void sub_3B772(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.field_11A == 8)
            {
                gbl.byte_1D2C9 += 3;
                gbl.byte_1D2BE += 3;
            }
        }


        internal static void spl_berzerk(byte arg_0, object param, Player player)
        {
            if (arg_0 == 0)
            {
                player.field_198 = 1;

                if (player.field_F7 <= 0x7F ||
                    player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0x0B3;
                }
                else
                {
                    player.field_F7 = 0x0B2;
                }

                if (gbl.game_state == 5)
                {
                    player.actions.target = null;

                    ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff,
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    player.actions.target = gbl.player_array[gbl.byte_1D1C4];

                    player.actions.can_cast = 0;
                    player.combat_team = (sbyte)(player.actions.target.combat_team ^ 1);

                    ovr025.DisplayPlayerStatusString(true, 10, "goes berzerk", player);
                }
            }
            else
            {
                if (player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0;
                }

                player.combat_team = 0;
            }
        }


        internal static void sub_3B8D9(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_current, player) == false)
            {
                addAffect(1, affect.field_3, Affects.affect_4e, player);
            }
        }


        internal static void sub_3B919(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2BF = 9;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(10, 2), player.actions.target);

        }


        internal static void sub_3B94C(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2BF = 0x10;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(4, 1), player.actions.target);
        }


        internal static void sub_3B97F(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2BE /= 2;
        }


        internal static void sub_3B990(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 0x01) != 0 ||
                (gbl.byte_1D2BF & 0x02) != 0)
            {
                if (ovr024.do_saving_throw(0, 4, player) == true &&
                    gbl.unk_19AEC[gbl.spell_id << 4].field_8 != 0)
                {
                    gbl.byte_1D2BE = 0;
                }
                else
                {
                    gbl.byte_1D2BE /= 2;
                }
            }
        }


        internal static void sub_3B9E1(byte arg_0, object param, Player player)
        {
            byte var_1;

            if ((gbl.byte_1D2BF & 0x04) != 0)
            {
                sub_3A019(0);
                var_1 = ovr024.roll_dice(8, 1);

                player.ac += 8;
            }
        }


        internal static void sub_3BA14(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 != null &&
                gbl.unk_1C020[var_4.type].field_7 == 1)
            {
                gbl.byte_1D2BE = 1;
            }
        }


        internal static void sub_3BA55(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            throw new System.NotSupportedException();//cmp	byte_1D8B7, 0
            throw new System.NotSupportedException();//jnz	loc_3BA78
            throw new System.NotSupportedException();//cmp	byte_1D2C9, 0
            throw new System.NotSupportedException();//jnz	loc_3BA78
            throw new System.NotSupportedException();//les	di, [bp+affect]
            throw new System.NotSupportedException();//mov	al, es:[di+3]
            throw new System.NotSupportedException();//and	al, 0x0F
            throw new System.NotSupportedException();//les	di, [bp+affect]
            throw new System.NotSupportedException();//mov	es:[di+3], al
            throw new System.NotSupportedException();//jmp	short func_end
            throw new System.NotSupportedException();//loc_3BA78:
            throw new System.NotSupportedException();//les	di, [bp+affect]
            throw new System.NotSupportedException();//mov	al, es:[di+3]
            throw new System.NotSupportedException();//and	al, 0x10
            throw new System.NotSupportedException();//or	al, al
            throw new System.NotSupportedException();//jnz	func_end
            gbl.byte_1D2C9 = -1;
            throw new System.NotSupportedException();//les	di, [bp+affect]
            throw new System.NotSupportedException();//mov	al, es:[di+3]
            throw new System.NotSupportedException();//or	al, 0x10
            throw new System.NotSupportedException();//les	di, [bp+affect]
            throw new System.NotSupportedException();//mov	es:[di+3], al
            throw new System.NotSupportedException();//func_end:
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//retf	0x0A
        }


        internal static void sub_3BAB9(byte arg_0, object param, Player player)
        {
            sbyte var_D;
            sbyte var_C;
            byte var_B;
            byte var_A;
            byte var_9;
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            Affect affect = (Affect)param;

            var_A = (byte)(affect.field_3 >> 4);

            var_9 = 0;
            var_8 = gbl.stru_1D889;

            while (var_8 != null && var_9 == 0)
            {
                if (var_8.player == player &&
                    var_8.field_1C == var_A)
                {
                    var_9 = 1;
                }
                else
                {
                    var_8 = var_8.next;
                }
            }

            if (var_9 != 0)
            {
                ovr025.string_print01("The air clears a little...");

                for (var_B = 1; var_B <= 9; var_B++)
                {
                    if (var_8.field_10[var_B] != 0)
                    {
                        var_C = (sbyte)(var_8.field_1A + gbl.MapDirectionXDelta[gbl.unk_18AED[var_B]]);
                        var_D = (sbyte)(var_8.field_1B + gbl.MapDirectionYDelta[gbl.unk_18AED[var_B]]);

                        var_9 = 0;

                        for (var_A = 1; var_A <= gbl.byte_1D1BB; var_A++)
                        {
                            if (gbl.unk_1D183[var_A].field_0 != null &&
                                gbl.unk_1D183[var_A].mapX == var_C &&
                                gbl.unk_1D183[var_A].mapY == var_D)
                            {
                                var_9 = 1;
                            }
                        }

                        if (var_9 != 0)
                        {
                            gbl.mapToBackGroundTile[var_C, var_D] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[var_C, var_D] = var_8.field_7[var_B];
                        }
                    }
                }

                var_4 = gbl.stru_1D889;

                if (var_4 == var_8)
                {
                    gbl.stru_1D889 = var_4.next;
                }
                else
                {
                    while (var_4.next != var_8)
                    {
                        var_4 = var_4.next;
                    }
                }

                var_4.next = var_8.next;
                seg051.FreeMem(0x1E, var_8);
                var_4 = gbl.stru_1D889;

                while (var_4 != null)
                {
                    for (var_B = 1; var_B <= 9; var_B++)
                    {
                        if (var_4.field_10[var_B] != 0)
                        {
                            int cx = gbl.MapDirectionXDelta[gbl.unk_18AED[var_B]] + var_4.field_1A;
                            int ax = gbl.MapDirectionYDelta[gbl.unk_18AED[var_B]] + var_4.field_1B;

                            gbl.mapToBackGroundTile[cx, ax] = 0x1C;
                        }
                    }

                    var_4 = var_4.next;
                }
            }
        }


        internal static void sub_3BD98(byte arg_0, object param, Player arg_6)
        {
            if ((gbl.byte_1D2BF & 0x01) != 0)
            {
                gbl.byte_1D2BE >>= 1;
            }
        }


        internal static void sub_3BDB2(byte arg_0, object param, Player arg_6)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 != null &&
                (gbl.unk_1C020[var_4.type].field_7 & 0x81) != 0)
            {
                gbl.byte_1D2BE >>= 1;
            }
        }


        internal static void sub_3BE06(byte arg_0, object param, Player arg_6)
        {
            Affect affect = (Affect)param;
            affect.call_spell_jump_list = false;

            if (arg_6.in_combat == true)
            {

                ovr024.sub_63014("Falls dead", Status.dead, arg_6);
            }
        }


        internal static void sub_3BE42(byte arg_0, object param, Player arg_6)
        {
            if (gbl.byte_1D2D1 == 4 ||
                gbl.byte_1D2D1 == 2)
            {
                byte var_1 = 0;

                if (arg_6.con >= 4 && arg_6.con <= 6)
                {
                    var_1 = 1;
                }
                else if (arg_6.con >= 7 && arg_6.con <= 10)
                {
                    var_1 = 2;
                }
                else if (arg_6.con >= 11 && arg_6.con <= 13)
                {
                    var_1 = 3;
                }
                else if (arg_6.con >= 14 && arg_6.con <= 17)
                {
                    var_1 = 4;
                }
                else if (arg_6.con >= 18 && arg_6.con <= 20)
                {
                    var_1 = 5;
                }

                gbl.saving_throw_roll += var_1;
            }
        }


        internal static void sub_3BEB8(byte arg_0, object param, Player arg_6)
        {
            arg_6.hit_point_current += 3;

            if (arg_6.hit_point_current > arg_6.hit_point_max)
            {
                arg_6.hit_point_current = arg_6.hit_point_max;
            }
        }


        internal static void sub_3BEE8(byte arg_0, object param, Player player_ptr)
        {
            Affect arg_2 = (Affect)param;

            byte heal_amount;

            heal_amount = 0;

            if (player_ptr.health_status == Status.dying &&
                player_ptr.actions.bleeding < 6)
            {
                heal_amount = (byte)(6 - player_ptr.actions.bleeding);
            }

            if (player_ptr.health_status == Status.unconscious)
            {
                heal_amount = 6;
            }

            if (heal_amount > 0 &&
                ovr024.combat_heal(heal_amount, player_ptr) == true)
            {
                ovr024.add_affect(true, 0xff, (ushort)(ovr024.roll_dice(4, 1) + 1), Affects.affect_5F, player_ptr);
                arg_2.call_spell_jump_list = false;
                ovr024.remove_affect(arg_2, Affects.affect_63, player_ptr);
            }
        }


        internal static void sub_3BF91(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 1) == 0 &&
                (gbl.byte_1D2BF & 0x10) == 0)
            {
                ovr024.add_affect(true, 0xff, ovr024.roll_dice(6, 3), Affects.affect_66, player);
            }
        }


        internal static void sp_regenerate(byte arg_0, object param, Player player)
        {
            Affect var_4;

            if (ovr025.find_affect(out var_4, Affects.affect_62, player) == false &&
                ovr025.find_affect(out var_4, Affects.regenerate, player) == false)
            {
                ovr024.add_affect(true, 0xff, 3, Affects.regenerate, player);
            }
        }


        internal static void sub_3C01E(byte arg_0, object param, Player player)
        {
            Affect arg_2 = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_max, player) == false)
            {
                addAffect(1, arg_2.field_3, Affects.affect_66, player);
            }
        }


        internal static void sub_3C05D(byte arg_0, object param, Player arg_6)
        {
            Affect var_4;

            gbl.spell_target = arg_6.actions.target;

            if (ovr025.find_affect(out var_4, Affects.resist_fire, gbl.spell_target) == false &&
                ovr025.find_affect(out var_4, Affects.cold_fire_shield, gbl.spell_target) == false &&
                ovr025.find_affect(out var_4, Affects.fire_resist, gbl.spell_target) == false)
            {
                gbl.byte_1D2BE += ovr024.roll_dice(6, 1);
            }
        }


        internal static void sub_3C0DA(byte arg_0, object param, Player arg_6)
        {
            sub_3AF06(0x3c, arg_6);
        }


        internal static void sub_3C0EE(byte arg_0)
        {
            byte var_2;
            byte var_1;

            var_1 = ovr025.sub_6886F(gbl.spell_id);
            var_2 = (byte)(arg_0 + ((0x0b - var_1) * 5));

            if (gbl.byte_1D2BD != 0 ||
                (gbl.byte_1D2BF & 8) != 0)
            {
                if (ovr024.roll_dice(100, 1) <= var_2)
                {
                    sub_3A019(0);
                }
            }
        }


        internal static void sub_3C14F(byte arg_0, object param, Player arg_6)
        {
            sub_3C0EE(50);
        }


        internal static void sub_3C15D(byte arg_0, object param, Player arg_6)
        {
            sub_3C0EE(15);
        }


        internal static void sub_3C16B(byte arg_0, object param, Player arg_6)
        {
            if (ovr024.roll_dice(100, 1) <= 0x5a)
            {
                sub_3A019(0x35);
                sub_3A019(11);
            }
        }


        internal static void sub_3C18F(byte arg_0, object param, Player arg_6)
        {
            sub_3A019(11);
            sub_3A019(0x35);
        }


        internal static void sub_3C1A4(byte arg_0, object param, Player arg_6)
        {
            sub_3A019(0x34);
        }


        internal static void sub_3C1B2(byte arg_0, object param, Player arg_6)
        {
            if ((gbl.byte_1D2BF & 2) != 0)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3C1C9(byte arg_0, object param, Player arg_6)
        {
            sub_3A019(0x37);
            sub_3A019(0x34);

            if (gbl.byte_1D2D1 == 0)
            {
                gbl.saving_throw_roll = 100;
            }
        }


        internal static void sub_3C1EA(byte arg_0, object param, Player arg_6)
        {
            if ((gbl.byte_1D2BF & 1) != 0)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3C201(byte arg_0, object param, Player arg_6)
        {
            if ((gbl.byte_1D2BF & 1) != 0)
            {
                for (int i = 1; i <= gbl.dice_count; i++)
                {
                    gbl.byte_1D2BE--;

                    if (gbl.byte_1D2BE < gbl.dice_count)
                    {
                        gbl.byte_1D2BE = (byte)gbl.dice_count;
                    }
                }
            }
        }


        internal static void sub_3C246(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 4) != 0)
            {
                gbl.byte_1D2BF >>= 1;
            }
        }


        internal static void sub_3C260(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 != null)
            {
                if (gbl.unk_1C020[var_4.type].field_7 == 0 ||
                    (gbl.unk_1C020[var_4.type].field_7 & 1) != 0)
                {
                    gbl.byte_1D2BE >>= 1;
                }
            }
        }


        internal static void sub_3C2BF(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 != null &&
                var_4.exp_value > 0)
            {
                gbl.byte_1D2BE >>= 1;
            }
        }


        internal static void sub_3C2F9(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = gbl.player_ptr.field_151;

            if (var_4 != null &&
                var_4.type == 0x55)
            {
                gbl.byte_1D2BE = (byte)(ovr024.roll_dice_save(6, 1) + 1);
            }
        }


        internal static void sub_3C33C(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 2) != 0)
            {
                gbl.byte_1D2BE /= 2;
            }
        }


        internal static void sub_3C356(byte arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = sub_3AF77(gbl.player_ptr);

            if (var_4 == null ||
                var_4.exp_value == 0)
            {
                if (gbl.player_ptr.race > 0 ||
                    gbl.player_ptr.field_E5 < 4)
                {
                    gbl.byte_1D2BE = 0;
                }
            }
        }


        internal static void sub_3C3A2(byte arg_0, object param, Player player)
        {
            Item field_151;

            field_151 = player.field_151;

            if (field_151 != null)
            {
                if (field_151.type == 0x57 ||
                    field_151.type == 0x58)
                {
                    sub_3AF06(0x32, player);
                }
            }
        }


        internal static void sub_3C3F6(byte arg_0, object param, Player player)
        {
            bool var_1;
            Affect affect_ptr = (Affect)param;

            gbl.spell_target = player.actions.target;

            if (ovr024.roll_dice(100, 1) <= 25)
            {
                if (ovr025.getTargetRange(gbl.spell_target, player) < 4)
                {
                    var_1 = ovr025.clear_actions(player);

                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);

                    ovr025.sub_67A59(0x17);

                    ovr025.draw_missile_attack(0x1e, 1,
                        ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    sbyte b2 = ovr024.roll_dice_save(4, 8);
                    bool b1 = ovr024.do_saving_throw(0, 3, gbl.spell_target);

                    ovr024.damage_person(b1, 2, b2, gbl.spell_target);

                    ovr024.remove_affect(affect_ptr, Affects.affect_79, player);
                    ovr024.remove_affect(null, Affects.affect_50, player);
                }
            }
        }


        internal static void spl_paralyze(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(0, 0, gbl.spell_target) == false)
            {
                ovr024.add_affect(false, 0xff, 0, Affects.paralyze, gbl.spell_target);

                ovr025.DisplayPlayerStatusString(true, 10, "is paralyzed", gbl.spell_target);
            }
        }


        internal static void sub_3C59D(byte arg_0, object param, Player player)
        {
            gbl.byte_1D2BF = 0x0A;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2), player.actions.target);
        }


        internal static void sub_3C5D0(byte arg_0, object param, Player player)
        {
            if (ovr024.roll_dice(100, 1) <= 0x1e)
            {
                sub_3A019(0x0b);
                sub_3A019(0x35);
            }
        }


        internal static void sub_3C5F4(byte arg_0, object param, Player player)
        {
            sub_3A019(11);
            sub_3A019(0x35);
            sub_3A019(0x34);
            sub_3A019(0x37);

            if (gbl.byte_1D2D1 != 0)
            {
                gbl.saving_throw_roll = 100;
            }
        }


        internal static void sub_3C623(byte arg_0, object param, Player player)
        {
            if (gbl.byte_1D2BD != 0 ||
                (gbl.byte_1D2BF & 8) != 0)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3C643(byte arg_0, object arg_2, Player player)
        {
            Item var_4 = null;

            if (ovr025.sub_6906C(out var_4, gbl.player_ptr) == true &&
                var_4 != null &&
                var_4.type == 0x1c &&
                var_4.field_31 == 0x87)
            {
                player.health_status = Status.gone;
                player.in_combat = false;
                player.hit_point_current = 0;
                ovr024.sub_645AB(player);
                ovr024.work_on_00(player, 13);

                if (player.in_combat == true)
                {
                    ovr033.sub_74E6F(player);
                }
            }
        }


        internal static void sub_3C6D3(byte arg_0, object param, Player player)
        {
            Item item = (Item)param;

            gbl.byte_1D8AC = 0;

            if (arg_0 != 0)
            {
                ovr024.remove_affect(null, item.affect_2, player);
            }
            else
            {
                ovr024.add_affect(true, 0xff, 0, item.affect_2, player);

                if (gbl.game_state != 5)
                {
                    ovr024.sub_630C7(0, null, player, item.affect_2);
                }
            }
        }


        internal static void sub_3C750(byte arg_0, object param, Player player)
        {
            sub_3A019(0x8E);
            sub_3A019(0x1D);
            sub_3A019(0x44);

            if ((gbl.byte_1D2BF & 0x40) != 0)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3C77C(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 8) != 0)
            {
                gbl.byte_1D2BE += player.ranger_lvl;
            }
        }


        internal static void sub_3C7B5(byte arg_0, object param, Player player)
        {
            if ((gbl.byte_1D2BF & 0x04) != 0)
            {
                sub_3A019(0);
            }
        }


        internal static void sub_3C7CC(byte arg_0, object param, Player player)
        {
            player.actions.move = 0;
        }


        internal static void sub_3C7E0(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (arg_0 == 0)
            {
                player.field_198 = 1;

                if (player.field_F7 <= 0x7f ||
                    player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0x0B3;

                }
                else
                {
                    player.field_F7 = 0x0B2;
                }

                player.actions.target = null;

                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff,
                    ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                player.actions.target = gbl.player_array[gbl.byte_1D1C4];

                player.combat_team = (sbyte)(player.actions.target.combat_team ^ 1);
            }
            else
            {
                if (player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0;
                }

                player.combat_team = (sbyte)affect.field_3;
            }
        }


        internal static void add_affect_19(byte arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0xff, Affects.invisibility, player);
        }


        internal static void sub_3C8EF(byte arg_0, object param, Player player)
        {
            if (arg_0 != 0)
            {
                player.field_191 = (byte)((((player.field_114 * ovr026.sub_6B3D1(player)) + player.paladin_lvl - 1) / 5) + 1);
            }
        }


        internal static void sub_3C932(byte arg_0, object param, Player player)
        {
            if (arg_0 != 0)
            {
                if (player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0;
                    player.field_198 = 0;
                }

                player.actions.field_10 = 0;
            }
        }


        internal static void sub_3C975(byte arg_0, object arg_2, Player arg_6)
        {
            byte var_2;
            byte var_1;

            if (ovr025.getTargetRange(arg_6, gbl.player_ptr) < 2)
            {
                var_1 = gbl.byte_1D2BE;

                gbl.byte_1D2BE <<= 1;

                var_2 = gbl.byte_1D2BF;
                gbl.byte_1D2BF = 8;

                ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.player_ptr);

                ovr024.damage_person(false, 0, (sbyte)gbl.byte_1D2BE, gbl.player_ptr);
                gbl.byte_1D2BE = var_1;
                gbl.byte_1D2BF = var_2;
            }
        }


        internal static void sp_dispel_evil(byte arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 1) != 0 &&
                ovr024.do_saving_throw(0, 4, gbl.spell_target) == false)
            {
                ovr024.sub_63014("is dispelled", Status.gone, gbl.spell_target);

                ovr024.remove_affect(null, Affects.dispel_evil, gbl.player_ptr);
                ovr024.remove_affect(null, Affects.affect_91, gbl.player_ptr);
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.spell_target);
            }
        }

        internal static void empty(byte arg_0, object param, Player player)
        {
        }

        internal static void setup_spells2()
        {
            gbl.spell_jump_list = new spellDelegateX[148];

            /* gbl.spell_jump_list[ 0 ] == stru_1D2D0 */
            gbl.spell_jump_list[1] = ovr013.sub_3A096;
            gbl.spell_jump_list[2] = ovr013.sub_3A0A6;
            gbl.spell_jump_list[3] = ovr013.sub_3A0DC;
            gbl.spell_jump_list[4] = ovr013.sub_3A15F;
            gbl.spell_jump_list[5] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[6] = new spellDelegateX(ovr013.sub_3A17A);
            gbl.spell_jump_list[7] = new spellDelegateX(ovr013.sub_3A1DF);
            gbl.spell_jump_list[8] = new spellDelegateX(ovr013.sub_3A224);
            gbl.spell_jump_list[9] = new spellDelegateX(ovr013.sub_3A259);
            gbl.spell_jump_list[10] = new spellDelegateX(ovr013.sub_3A28E);
            gbl.spell_jump_list[11] = new spellDelegateX(ovr013.sub_3A2AD);
            gbl.spell_jump_list[12] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[13] = new spellDelegateX(ovr013.Suffocates);
            gbl.spell_jump_list[14] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[15] = new spellDelegateX(ovr013.sub_3A3BC);
            gbl.spell_jump_list[16] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[17] = new spellDelegateX(ovr013.sub_3A41F);
            gbl.spell_jump_list[18] = new spellDelegateX(ovr013.sub_3A44A);
            gbl.spell_jump_list[19] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[20] = new spellDelegateX(ovr013.sub_3A480);
            gbl.spell_jump_list[21] = new spellDelegateX(ovr013.is_silenced1);
            gbl.spell_jump_list[22] = new spellDelegateX(ovr013.sub_3A517);
            gbl.spell_jump_list[23] = new spellDelegateX(ovr013.sub_3A583);
            gbl.spell_jump_list[24] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[25] = new spellDelegateX(ovr013.sub_3A6C6);
            gbl.spell_jump_list[26] = new spellDelegateX(ovr013.sub_3A6FB);
            gbl.spell_jump_list[27] = new spellDelegateX(ovr013.sub_3A071);
            gbl.spell_jump_list[28] = new spellDelegateX(ovr013.sub_3A73F);
            gbl.spell_jump_list[29] = new spellDelegateX(ovr013.sub_3A7C0);
            gbl.spell_jump_list[30] = new spellDelegateX(ovr013.sub_3A7E8);
            gbl.spell_jump_list[31] = new spellDelegateX(ovr013.sub_3A071);
            gbl.spell_jump_list[32] = new spellDelegateX(ovr013.sub_3A89E);
            gbl.spell_jump_list[33] = new spellDelegateX(ovr013.sub_3A951);
            gbl.spell_jump_list[34] = new spellDelegateX(ovr013.sub_3A974);
            gbl.spell_jump_list[35] = new spellDelegateX(ovr013.sub_3A9D9);
            gbl.spell_jump_list[36] = new spellDelegateX(ovr013.sub_3AB6F);
            gbl.spell_jump_list[37] = new spellDelegateX(ovr013.has_action_timedout);
            gbl.spell_jump_list[38] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[39] = new spellDelegateX(ovr013.spl_age);
            gbl.spell_jump_list[40] = new spellDelegateX(ovr013.sub_3AC1D);
            gbl.spell_jump_list[41] = new spellDelegateX(ovr013.sub_3AFE0);
            gbl.spell_jump_list[42] = new spellDelegateX(ovr013.sub_3B01B);
            gbl.spell_jump_list[43] = new spellDelegateX(ovr013.weaken);
            gbl.spell_jump_list[44] = new spellDelegateX(ovr013.sub_3B0C2);
            gbl.spell_jump_list[45] = new spellDelegateX(ovr013.sub_3A224);
            gbl.spell_jump_list[46] = new spellDelegateX(ovr013.sub_3A259);
            gbl.spell_jump_list[47] = new spellDelegateX(ovr013.sub_3B153);
            gbl.spell_jump_list[48] = new spellDelegateX(ovr013.sub_3B1A2);
            gbl.spell_jump_list[49] = new spellDelegateX(ovr013.sub_3B1C9);
            gbl.spell_jump_list[50] = new spellDelegateX(ovr013.sub_3B212);
            gbl.spell_jump_list[51] = new spellDelegateX(ovr013.sub_3A071);
            gbl.spell_jump_list[52] = new spellDelegateX(ovr013.sub_3A071);
            gbl.spell_jump_list[53] = new spellDelegateX(ovr013.sub_3A071);
            gbl.spell_jump_list[54] = new spellDelegateX(ovr013.sub_3B243);
            gbl.spell_jump_list[55] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[56] = new spellDelegateX(ovr013.sub_3B27B);
            gbl.spell_jump_list[57] = new spellDelegateX(ovr014.engulfs);
            gbl.spell_jump_list[58] = new spellDelegateX(ovr013.sub_3B29A);
            gbl.spell_jump_list[59] = new spellDelegateX(ovr013.sub_3B2BA);
            gbl.spell_jump_list[60] = new spellDelegateX(ovr013.sub_3B2D8);
            gbl.spell_jump_list[61] = new spellDelegateX(ovr013.sub_3B32B);
            gbl.spell_jump_list[62] = new spellDelegateX(ovr013.sub_3B386);
            gbl.spell_jump_list[63] = new spellDelegateX(ovr013.sub_3B3CA);
            gbl.spell_jump_list[64] = new spellDelegateX(ovr013.sub_3B520);
            gbl.spell_jump_list[65] = new spellDelegateX(ovr013.sub_3B534);
            gbl.spell_jump_list[66] = new spellDelegateX(ovr013.sub_3B548);
            gbl.spell_jump_list[67] = new spellDelegateX(ovr013.sub_3B55C);
            gbl.spell_jump_list[68] = new spellDelegateX(ovr013.spell_stupid);
            gbl.spell_jump_list[69] = new spellDelegateX(ovr013.sub_3B636);
            gbl.spell_jump_list[70] = new spellDelegateX(ovr013.sub_3B671);
            gbl.spell_jump_list[71] = new spellDelegateX(ovr013.sub_3B685);
            gbl.spell_jump_list[72] = new spellDelegateX(ovr013.sub_3B696);
            gbl.spell_jump_list[73] = new spellDelegateX(ovr013.sub_3B6D2);
            gbl.spell_jump_list[74] = new spellDelegateX(ovr013.sub_3B70E);
            gbl.spell_jump_list[75] = new spellDelegateX(ovr013.sub_3B71A);
            gbl.spell_jump_list[76] = new spellDelegateX(ovr013.sub_3B772);
            gbl.spell_jump_list[77] = new spellDelegateX(ovr013.spl_berzerk);
            gbl.spell_jump_list[78] = new spellDelegateX(ovr013.sub_3B8D9);
            gbl.spell_jump_list[79] = new spellDelegateX(ovr013.sub_3B919);
            gbl.spell_jump_list[80] = new spellDelegateX(ovr013.sub_3B94C);
            gbl.spell_jump_list[81] = new spellDelegateX(ovr013.sub_3B97F);
            gbl.spell_jump_list[82] = new spellDelegateX(ovr013.sub_3B990);
            gbl.spell_jump_list[83] = new spellDelegateX(ovr023.spell_stone);
            gbl.spell_jump_list[84] = new spellDelegateX(ovr013.sub_3B9E1);
            gbl.spell_jump_list[85] = new spellDelegateX(ovr013.sub_3BA14);
            gbl.spell_jump_list[86] = new spellDelegateX(ovr023.spell_spit_acid);
            gbl.spell_jump_list[87] = new spellDelegateX(ovr014.attack_or_kill);
            gbl.spell_jump_list[88] = new spellDelegateX(ovr023.cast_breath);
            gbl.spell_jump_list[89] = new spellDelegateX(ovr013.sub_3BA55);
            gbl.spell_jump_list[90] = new spellDelegateX(ovr023.spell_breathes_acid);
            gbl.spell_jump_list[91] = new spellDelegateX(ovr013.sub_3BAB9);
            gbl.spell_jump_list[92] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[93] = new spellDelegateX(ovr013.sub_3BD98);
            gbl.spell_jump_list[94] = new spellDelegateX(ovr013.sub_3BDB2);
            gbl.spell_jump_list[95] = new spellDelegateX(ovr013.sub_3BE06);
            gbl.spell_jump_list[96] = new spellDelegateX(ovr014.hugs);
            gbl.spell_jump_list[97] = new spellDelegateX(ovr013.sub_3BE42);
            gbl.spell_jump_list[98] = new spellDelegateX(ovr013.sub_3BEB8);
            gbl.spell_jump_list[99] = new spellDelegateX(ovr013.sub_3BEE8);
            gbl.spell_jump_list[100] = new spellDelegateX(ovr013.sub_3BF91);
            gbl.spell_jump_list[101] = new spellDelegateX(ovr013.sp_regenerate);
            gbl.spell_jump_list[102] = new spellDelegateX(ovr013.sub_3C01E);
            gbl.spell_jump_list[103] = new spellDelegateX(ovr013.sub_3C05D);
            gbl.spell_jump_list[104] = new spellDelegateX(ovr013.sub_3C0DA);
            gbl.spell_jump_list[105] = new spellDelegateX(ovr013.sub_3C14F);
            gbl.spell_jump_list[106] = new spellDelegateX(ovr013.sub_3C15D);
            gbl.spell_jump_list[107] = new spellDelegateX(ovr013.sub_3C16B);
            gbl.spell_jump_list[108] = new spellDelegateX(ovr013.sub_3C18F);
            gbl.spell_jump_list[109] = new spellDelegateX(ovr013.sub_3C1A4);
            gbl.spell_jump_list[110] = new spellDelegateX(ovr013.sub_3C1B2);
            gbl.spell_jump_list[111] = new spellDelegateX(ovr013.sub_3C1C9);
            gbl.spell_jump_list[112] = new spellDelegateX(ovr013.sub_3C1EA);
            gbl.spell_jump_list[113] = new spellDelegateX(ovr013.sub_3C201);
            gbl.spell_jump_list[114] = new spellDelegateX(ovr013.sub_3C246);
            gbl.spell_jump_list[115] = new spellDelegateX(ovr013.sub_3C260);
            gbl.spell_jump_list[116] = new spellDelegateX(ovr013.sub_3C2BF);
            gbl.spell_jump_list[117] = new spellDelegateX(ovr013.sub_3C2F9);
            gbl.spell_jump_list[118] = new spellDelegateX(ovr013.sub_3C33C);
            gbl.spell_jump_list[119] = new spellDelegateX(ovr013.sub_3C356);
            gbl.spell_jump_list[120] = new spellDelegateX(ovr013.sub_3C3A2);
            gbl.spell_jump_list[121] = new spellDelegateX(ovr013.sub_3C3F6);
            gbl.spell_jump_list[122] = new spellDelegateX(ovr013.spl_paralyze);
            gbl.spell_jump_list[123] = new spellDelegateX(ovr013.sub_3C59D);
            gbl.spell_jump_list[124] = new spellDelegateX(ovr013.sub_3C5D0);
            gbl.spell_jump_list[125] = new spellDelegateX(ovr013.sub_3C5F4);
            gbl.spell_jump_list[126] = new spellDelegateX(ovr023.cast_gaze_paralyze);
            gbl.spell_jump_list[127] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[128] = new spellDelegateX(ovr023.spell_breathes_fire);
            gbl.spell_jump_list[129] = new spellDelegateX(ovr013.sub_3C623);
            gbl.spell_jump_list[130] = new spellDelegateX(ovr013.sub_3C643);
            gbl.spell_jump_list[131] = new spellDelegateX(ovr023.cast_breath_fire);
            gbl.spell_jump_list[132] = new spellDelegateX(ovr023.cast_throw_lightning);
            gbl.spell_jump_list[133] = new spellDelegateX(ovr013.sub_3C750);
            gbl.spell_jump_list[134] = new spellDelegateX(ovr013.sub_3C77C);
            gbl.spell_jump_list[135] = new spellDelegateX(ovr013.sub_3C7B5);
            gbl.spell_jump_list[136] = new spellDelegateX(ovr013.sub_3C7CC);
            gbl.spell_jump_list[137] = new spellDelegateX(ovr013.sub_3C7E0);
            gbl.spell_jump_list[138] = new spellDelegateX(ovr013.add_affect_19);
            gbl.spell_jump_list[139] = new spellDelegateX(ovr014.sub_425C6);
            gbl.spell_jump_list[140] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[141] = new spellDelegateX(ovr013.sub_3C8EF);
            gbl.spell_jump_list[142] = new spellDelegateX(ovr013.sub_3C932);
            gbl.spell_jump_list[143] = new spellDelegateX(ovr013.sub_3C975);
            gbl.spell_jump_list[144] = new spellDelegateX(ovr014.sub_426FC);
            gbl.spell_jump_list[145] = new spellDelegateX(ovr013.sp_dispel_evil);
            gbl.spell_jump_list[146] = new spellDelegateX(ovr013.empty);
            gbl.spell_jump_list[147] = new spellDelegateX(ovr013.sub_3C6D3);

        }
    }
}
