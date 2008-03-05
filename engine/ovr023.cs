using Classes;

namespace engine
{
    class ovr023
    {
        internal static string[] AffectNames = {
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
                                   "Cure Serious Wounds",
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

        internal static byte sub_5C01E(byte arg_0, Player arg_2)
        {
            byte var_1;

            arg_0 &= 0x7f;
            var_1 = 0;

            switch (gbl.unk_19AEC[arg_0].spellClass)
            {
                case 0:
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+15h], 8
                    throw new System.NotSupportedException();//mov	al, 0
                    throw new System.NotSupportedException();//jbe	loc_5C050
                    throw new System.NotSupportedException();//inc	ax
                    throw new System.NotSupportedException();//loc_5C050:
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C09E
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+109h], 0
                    throw new System.NotSupportedException();//jg	loc_5C0A2
                    ovr026.sub_6B3D1(arg_2);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C079
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+111h], 0
                    throw new System.NotSupportedException();//jg	loc_5C0A2
                    throw new System.NotSupportedException();//loc_5C079:
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Ch], 8
                    throw new System.NotSupportedException();//jg	loc_5C0A2
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+114h], 8
                    throw new System.NotSupportedException();//jle	loc_5C09E
                    ovr026.sub_6B3D1(arg_2);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jnz	loc_5C0A2
                    throw new System.NotSupportedException();//loc_5C09E:
                    throw new System.NotSupportedException();//mov	al, 0
                    throw new System.NotSupportedException();//jmp	short loc_5C0A4
                    throw new System.NotSupportedException();//loc_5C0A2:
                    throw new System.NotSupportedException();//mov	al, 1
                    throw new System.NotSupportedException();//loc_5C0A4:
                    throw new System.NotSupportedException();//mov	[bp+var_1], al
                    break;

                case 1:
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+15h], 8
                    throw new System.NotSupportedException();//mov	al, 0
                    throw new System.NotSupportedException();//jbe	loc_5C0BB
                    throw new System.NotSupportedException();//inc	ax
                    throw new System.NotSupportedException();//loc_5C0BB:
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C0E4
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Dh], 6
                    throw new System.NotSupportedException();//jg	loc_5C0E8
                    ovr026.sub_6B3D1(arg_2);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C0E4
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+115h], 6
                    throw new System.NotSupportedException();//jg	loc_5C0E8
                    throw new System.NotSupportedException();//loc_5C0E4:
                    throw new System.NotSupportedException();//mov	al, 0
                    throw new System.NotSupportedException();//jmp	short loc_5C0EA
                    throw new System.NotSupportedException();//loc_5C0E8:
                    throw new System.NotSupportedException();//mov	al, 1
                    throw new System.NotSupportedException();//loc_5C0EA:
                    throw new System.NotSupportedException();//mov	[bp+var_1], al
                    break;

                case 2:
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+13h], 8
                    throw new System.NotSupportedException();//jbe	loc_5C16B
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+74h], 7
                    throw new System.NotSupportedException();//jnz	loc_5C16F
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//mov	ax, es:[di+159h]
                    throw new System.NotSupportedException();//or	ax, es:[di+15Bh]
                    throw new System.NotSupportedException();//jz	loc_5C16F
                    throw new System.NotSupportedException();//cmp	game_state, 5
                    throw new System.NotSupportedException();//jnz	loc_5C16F
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Dh], 8
                    throw new System.NotSupportedException();//jg	loc_5C16F
                    ovr026.sub_6B3D1(arg_2);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C16B
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+115h], 8
                    throw new System.NotSupportedException();//jle	loc_5C16B
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Eh], 0
                    throw new System.NotSupportedException();//jg	loc_5C16F
                    ovr026.sub_6B3D1(arg_2);
                    throw new System.NotSupportedException();//or	al, al
                    throw new System.NotSupportedException();//jz	loc_5C16B
                    throw new System.NotSupportedException();//les	di, [bp+arg_2]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+116h], 0
                    throw new System.NotSupportedException();//jg	loc_5C16F
                    throw new System.NotSupportedException();//loc_5C16B:
                    throw new System.NotSupportedException();//mov	al, 0
                    throw new System.NotSupportedException();//jmp	short loc_5C171
                    throw new System.NotSupportedException();//loc_5C16F:
                    throw new System.NotSupportedException();//mov	al, 1
                    throw new System.NotSupportedException();//loc_5C171:
                    throw new System.NotSupportedException();//mov	[bp+var_1], al
                    break;

                case 3:
                    var_1 = 0;
                    break;

            }

            return var_1;
        }

        static Set unk_5C1A2 = new Set(0x0001, new byte[] { 0x1E });
        static Set asc_5C1D1 = new Set(0x000A, new byte[] { 1, 0, 0, 0, 0, 0, 0, 0x28, 0x30, 8 });
        static Set unk_5C1F1 = new Set(0x0009, new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 0x20 });

        internal static byte spell_menu(ref short arg_0, byte arg_4)
        {
            bool var_61;
            bool var_60;
            char var_5F;
            sbyte var_5E;
            byte var_5D;
            string var_5B;
            string var_32;
            StringList var_9;
            StringList var_5;
            byte var_1;

            switch (arg_4)
            {
                case 1:
                    var_5B = "Cast";
                    break;

                case 2:
                    var_5B = "Memorize";
                    break;

                case 3:
                    var_5B = "Scribe";
                    break;

                case 4:
                    var_5B = "Learn";
                    break;

                default:
                    var_5B = string.Empty;
                    break;
            }

            if (unk_5C1A2.MemberOf(arg_4) == true)
            {
                var_32 = "Choose Spell: ";
            }
            else
            {
                var_32 = string.Empty;
            }

            if (arg_4 == 2)
            {
                var_5E = 0x0F;
            }
            else
            {
                var_5E = 0x16;
            }

            if (arg_4 == 4)
            {
                var_60 = false;
            }
            else
            {
                var_60 = true;
            }

            var_61 = false;

            if (arg_0 < 0)
            {
                var_61 = true;
                arg_0 = 0;
            }

            if (arg_4 == 4 || arg_4 == 1)
            {
                var_61 = true;
            }

            var_5 = null;

            do
            {
                var_5F = ovr027.sl_select_item(out var_5, ref arg_0, ref var_61, var_60, gbl.dword_1AE6C,
                    var_5E, 0x26, 5, 1, 15, 10, 13, var_5B, var_32);

            } while (asc_5C1D1.MemberOf(var_5F) == false);

            var_9 = gbl.dword_1AE6C;
            var_5D = 0;

            while (var_9 != var_5)
            {
                if (var_9.field_29 == 0)
                {
                    var_5D++;
                }

                var_9 = var_9.next;
            }

            if (unk_5C1F1.MemberOf(var_5F) == true)
            {
                var_1 = 0;
            }
            else
            {
                var_1 = gbl.unk_1AEC4[var_5D];
            }

            if (arg_4 == 3)
            {
                gbl.dword_1D5C6 = gbl.unk_1AF18[var_5D];
            }

            ovr027.free_stringList(ref gbl.dword_1AE6C);

            return var_1;
        }


        internal static void sub_5C3ED(byte arg_0)
        {
            sbyte var_6;
            byte var_5;
            StringList var_4;

            var_4 = gbl.dword_1AE6C;

            if (gbl.dword_1AE6C == null)
            {
                var_4 = new StringList();

                gbl.dword_1AE6C = var_4;
                var_6 = 0;
                var_5 = 0;

            }
            else
            {
                var_5 = 1;

                while (var_4.next != null)
                {
                    if (var_4.field_29 == 0)
                    {
                        var_5++;
                    }
                    var_4 = var_4.next;
                }


                var_6 = gbl.unk_19AEC[gbl.unk_1AEC4[var_5 - 1] << 4].spellLevel;

                var_4.next = new StringList();

                var_4 = var_4.next;
            }

            var_4.next = null;

            if (gbl.unk_19AEC[arg_0 & 0x7F].spellLevel != var_6)
            {
                var_4.s = LevelStrings[gbl.unk_19AEC[arg_0 & 0x7F].spellLevel];

                var_4.field_29 = 1;
                var_4.next = new StringList();

                var_4 = var_4.next;
                var_4.next = null;
            }

            if (arg_0 > 0x7f)
            {
                var_4.s = " *";
            }
            else
            {
                var_4.s = "  ";
            }

            var_4.s += AffectNames[arg_0 & 0x7f];
            var_4.field_29 = 0;

            gbl.unk_1AEC4[var_5] = (byte)(arg_0 & 0x7F);
        }


        internal static void sub_5C5B9(byte arg_0)
        {
            sbyte var_12;
            byte var_11;
            byte var_10;
            byte var_F;
            StringList var_E;
            StringList var_A;
            StringList var_6;
            byte var_2;
            byte var_1;

            var_6 = gbl.dword_1AE6C;
            var_11 = 0;

            var_12 = gbl.unk_19AEC[arg_0 & 0x7F].spellLevel;

            if (gbl.dword_1AE6C == null)
            {
                seg051.FillChar(0, 0x54, gbl.unk_1AE70);

                var_6 = new StringList();
                var_6.next = null;

                gbl.dword_1AE6C = var_6;

                var_F = 0;

                gbl.unk_1AE70[var_F] = 1;
            }
            else
            {

                var_F = 0;

                while (var_6 != null && var_11 == 0)
                {
                    throw new System.NotSupportedException();//les	di, [bp+var_6]
                    throw new System.NotSupportedException();//cmp	byte ptr es:[di+29h], 0
                    throw new System.NotSupportedException();//jnz	loc_5C6A2
                    throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[ unk_1AEC4[ var_F ] ] ].field_1
                    throw new System.NotSupportedException();//cmp	al, [bp+var_12]
                    throw new System.NotSupportedException();//jg	loc_5C69C
                    throw new System.NotSupportedException();//mov	al, [bp+arg_0]
                    throw new System.NotSupportedException();//and	al, 0x7F
                    throw new System.NotSupportedException();//mov	dl, al
                    throw new System.NotSupportedException();//mov	al, [bp+var_F]
                    throw new System.NotSupportedException();//cbw
                    throw new System.NotSupportedException();//mov	di, ax
                    throw new System.NotSupportedException();//mov	al, unk_1AEC4[di]
                    throw new System.NotSupportedException();//cmp	al, dl
                    throw new System.NotSupportedException();//jz	loc_5C69C
                    var_F++;
                    var_6 = var_6.next;
                    throw new System.NotSupportedException();//jmp	short loc_5C6A0
                    throw new System.NotSupportedException();//loc_5C69C:
                    var_11 = 1;
                    throw new System.NotSupportedException();//loc_5C6A0:
                    throw new System.NotSupportedException();//jmp	short loc_5C6B6
                    throw new System.NotSupportedException();//loc_5C6A2:
                    var_6 = var_6.next;
                    var_F++;
                    throw new System.NotSupportedException();//loc_5C6B6:
                }
                throw new System.NotSupportedException();//mov	al, [bp+arg_0]
                throw new System.NotSupportedException();//and	al, 0x7F
                throw new System.NotSupportedException();//mov	dl, al
                throw new System.NotSupportedException();//mov	al, [bp+var_F]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//mov	al, unk_1AEC4[di]
                throw new System.NotSupportedException();//cmp	al, dl
                throw new System.NotSupportedException();//jnz	loc_5C6D0
                throw new System.NotSupportedException();//jmp	loc_5C7CF
                throw new System.NotSupportedException();//loc_5C6D0:
                var_A = gbl.dword_1AE6C;
                if (var_A != var_6)
                {
                    while (var_A.next != var_6)
                    {
                        var_A = var_A.next;
                    }

                    var_E = new StringList();
                    var_E.next = var_A.next;
                    var_A.next = var_E;
                    var_6 = var_E;
                }
                else
                {
                    var_E = new StringList();
                    var_E.next = var_6;
                    gbl.dword_1AE6C = var_E;
                    var_6 = var_E;
                }

                var_1 = 1;

                for (var_10 = var_F; var_10 <= 0x53; var_10++)
                {
                    var_2 = gbl.unk_1AE70[var_10];
                    gbl.unk_1AE70[var_10] = var_1;
                    var_1 = var_2;
                }
                throw new System.NotSupportedException();//jmp	short loc_5C7D9
                throw new System.NotSupportedException();//loc_5C7CF:
                gbl.unk_1AE70[var_F] += 1;
            }
            throw new System.NotSupportedException();//loc_5C7D9:
            if (arg_0 > 0x7F)
            {
                var_6.s = " *";
            }
            else
            {
                var_6.s = "  ";
            }

            var_6.s += AffectNames[arg_0 & 0x7F];
            var_6.field_29 = 0;

            if (gbl.unk_1AE70[var_F] > 1)
            {
                var_6.s = string.Format("{0} ({1})", var_6.s, gbl.unk_1AE70[var_F]);
            }
            throw new System.NotSupportedException();//mov	al, [bp+arg_0]
            throw new System.NotSupportedException();//and	al, 0x7F
            throw new System.NotSupportedException();//mov	dl, al
            throw new System.NotSupportedException();//mov	al, [bp+var_F]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	al, unk_1AEC4[di]
            throw new System.NotSupportedException();//cmp	al, dl
            throw new System.NotSupportedException();//jz	func_end
            var_1 = gbl.unk_1AEC4[var_F];
            gbl.unk_1AEC4[var_F] = (byte)(arg_0 & 0x7F);

            for (var_10 = (byte)(var_F + 1); var_10 <= 0x53; var_10++)
            {
                var_2 = gbl.unk_1AEC4[var_10];
                gbl.unk_1AEC4[var_10] = var_1;
                var_1 = var_2;
            }
            throw new System.NotSupportedException();//func_end:
        }


        internal static void sub_5C912(byte arg_0)
        {
            Affect var_5;
            byte var_1;

            ovr025.find_affect(out var_5, Affects.read_magic, gbl.player_ptr);
            throw new System.NotSupportedException();//or	al, al
            throw new System.NotSupportedException();//jnz	loc_5C968
            throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+109h], 0
            throw new System.NotSupportedException();//jg	loc_5C951
            throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
            throw new System.NotSupportedException();//mov	al, es:[di+111h]
            throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
            throw new System.NotSupportedException();//cmp	al, es:[di+0E6h]
            throw new System.NotSupportedException();//jle	loc_5C971
            throw new System.NotSupportedException();//loc_5C951:
            throw new System.NotSupportedException();//cmp	gbl.unk_1C020[ gbl.dword_1D5C6.type ].field_0, 0x0C
            throw new System.NotSupportedException();//jnz	loc_5C971
            throw new System.NotSupportedException();//loc_5C968:
            throw new System.NotSupportedException();//les	di, dword_1D5C6
            throw new System.NotSupportedException();//mov	byte ptr es:[di+35h], 0
            throw new System.NotSupportedException();//loc_5C971:
            throw new System.NotSupportedException();//les	di, dword_1D5C6
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+35h], 0
            throw new System.NotSupportedException();//jnz	func_end
            for (var_1 = 1; var_1 <= 3; var_1++)
            {
                throw new System.NotSupportedException();//cmp	[bp+arg_0], 0
                throw new System.NotSupportedException();//jz	loc_5C99D
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//les	di, dword_1D5C6
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+3Bh], 0x80
                throw new System.NotSupportedException();//ja	loc_5C9B5
                throw new System.NotSupportedException();//loc_5C99D:
                throw new System.NotSupportedException();//cmp	[bp+arg_0], 0
                throw new System.NotSupportedException();//jnz	loc_5C9E8
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//les	di, dword_1D5C6
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+3Bh], 0
                throw new System.NotSupportedException();//jbe	loc_5C9E8
                throw new System.NotSupportedException();//loc_5C9B5:
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//les	di, dword_1D5C6
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	al, es:[di+3Bh]
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//push	cs
                throw new System.NotSupportedException();//call	near ptr sub_5C3ED
                gbl.unk_1AF18[gbl.byte_1AFDC] = gbl.dword_1D5C6;
                gbl.byte_1AFDC++;
                throw new System.NotSupportedException();//loc_5C9E8:
            }
            throw new System.NotSupportedException();//func_end:
        }


        internal static void sub_5C9F4(byte arg_0)
        {
            byte var_1;

            for (var_1 = 0; var_1 < 0x30; var_1++)
            {
                gbl.unk_1AF18[var_1] = null;
            }

            gbl.byte_1AFDC = 0;

            gbl.dword_1D5C6 = gbl.player_ptr.itemsPtr;

            while (gbl.dword_1D5C6 != null)
            {

                if (item_is_scroll(gbl.dword_1D5C6) == true)
                {
                    sub_5C912(arg_0);
                }

                gbl.dword_1D5C6 = gbl.dword_1D5C6.next;
            }
        }


        internal static bool sub_5CA74(SpellLoc spl_location)
        {
            byte var_D;
            sbyte var_C;
            sbyte var_B;
            StringList var_A;
            StringList var_6;
            byte var_2;
            bool var_1;

            var_D = 1;
            var_1 = false;

            gbl.dword_1AE6C = null;

            for (var_2 = 0; var_2 < gbl.max_spells; var_2++)
            {
                gbl.unk_1AEC4[var_2] = 0;
            }

            switch (spl_location)
            {
                case SpellLoc.memory:
                    for (var_2 = 0; var_2 < gbl.max_spells; var_2++)
                    {
                        if (gbl.player_ptr.spell_list[var_2] > 0 &&
                            sub_5C01E((byte)(gbl.player_ptr.spell_list[var_2] & 0x7F), gbl.player_ptr) != 0 &&
                            gbl.player_ptr.spell_list[var_2] < 0x80)
                        {
                            sub_5C5B9(gbl.player_ptr.spell_list[var_2]);
                        }
                    }
                    break;

                case SpellLoc.memorize:
                    for (var_2 = 0; var_2 < gbl.max_spells; var_2++)
                    {
                        if (gbl.player_ptr.spell_list[var_2] > 0x7F &&
                            sub_5C01E((byte)(gbl.player_ptr.spell_list[var_2] & 0x7F), gbl.player_ptr) != 0)
                        {
                            sub_5C5B9(gbl.player_ptr.spell_list[var_2]);
                        }
                    }
                    break;

                case SpellLoc.grimoire:
                    for (var_2 = 1; var_2 <= 100; var_2++)
                    {
                        if (gbl.player_ptr.field_79[var_2-1] != 0 &&
                            sub_5C01E(var_2, gbl.player_ptr) != 0)
                        {
                            sub_5C5B9(var_2);
                        }
                    }
                    break;

                case SpellLoc.scroll:
                    sub_5C912(0);
                    var_D = 0;
                    break;

                case SpellLoc.scrolls:
                    sub_5C9F4(0);
                    var_D = 0;
                    break;

                case SpellLoc.scribe:
                    sub_5C9F4(1);
                    var_D = 0;
                    break;

                case SpellLoc.choose:
                    for (var_2 = 1; var_2 <= 100; var_2++)
                    {
                        throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[ var_2 ].field_1;
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[ var_2 ].field_0
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	si, ax
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//add	ax, si
                        throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//add	di, dx
                        throw new System.NotSupportedException();//cmp	es:[di+charStruct.field_12C], 0
                        throw new System.NotSupportedException();//jbe	loc_5CC64
                        sub_5C01E(var_2, gbl.player_ptr);
                        throw new System.NotSupportedException();//or	al, al
                        throw new System.NotSupportedException();//jz	loc_5CC64
                        throw new System.NotSupportedException();//mov	al, [bp+var_2]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//cmp	byte ptr es:[di+78h], 0
                        throw new System.NotSupportedException();//jnz	loc_5CC64
                        sub_5C5B9(var_2);
                        throw new System.NotSupportedException();//loc_5CC64:
                    }
                    break;
            }

            if (gbl.dword_1AE6C != null)
            {
                if (var_D != 0)
                {
                    var_2 = 0;

                    var_C = gbl.unk_19AEC[gbl.unk_1AEC4[var_2]].spellLevel;

                    var_A = new StringList();

                    var_A.next = gbl.dword_1AE6C;
                    gbl.dword_1AE6C = var_A;

                    gbl.dword_1AE6C.s = LevelStrings[gbl.unk_19AEC[gbl.unk_1AEC4[var_2]].spellLevel];
                    gbl.dword_1AE6C.field_29 = 1;
                    var_6 = gbl.dword_1AE6C;

                    do
                    {
                        var_B = var_C;
                        throw new System.NotSupportedException();//mov	al, [bp+var_2]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//cmp	byte ptr unk_1AEC4[di],	0
                        throw new System.NotSupportedException();//jz	loc_5CD43
                        throw new System.NotSupportedException();//mov	al, [bp+var_2]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	al, byte ptr unk_1AEC4[di]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[ di ].field_1
                        throw new System.NotSupportedException();//mov	[bp+var_C], al
                        throw new System.NotSupportedException();//loc_5CD43:
                        throw new System.NotSupportedException();//mov	al, [bp+var_B]
                        throw new System.NotSupportedException();//cmp	al, [bp+var_C]
                        throw new System.NotSupportedException();//jge	loc_5CDB9
                        var_A = new StringList();
                        var_A.next = var_6.next;
                        var_6.next = var_A;

                        var_6 = var_6.next;

                        var_6.s = LevelStrings[var_C];
                        var_6.field_29 = 1;

                        throw new System.NotSupportedException();//loc_5CDB9:
                        var_2++;
                        var_6 = var_6.next;
                    } while (var_6 != null);
                }
                var_1 = true;
            }

            return var_1;
        }


        internal static byte sub_5CDE5(byte arg_0)
        {
            byte var_2;
            byte var_1;

            if (gbl.byte_1D88D == 0)
            {
                var_2 = (byte)(gbl.unk_19AEC[arg_0].field_2 + (gbl.unk_19AEC[arg_0].field_3 * ovr025.sub_6886F(arg_0)));
            }
            else
            {
                var_2 = (byte)((gbl.unk_19AEC[arg_0].field_3 * 6) + gbl.unk_19AEC[arg_0].field_2);
            }

            if (var_2 == 0 &&
                gbl.unk_19AEC[arg_0].field_6 != 0)
            {
                var_2 = 1;
            }

            if (var_2 == 0xff)
            {
                var_2 = 1;
            }

            var_1 = var_2;

            return var_1;
        }


        internal static ushort sub_5CE92(byte arg_0)
        {
            ushort var_4;

            if (arg_0 == 0x28)
            {
                var_4 = (ushort)(ovr024.roll_dice(6, 1) * 10);
            }
            else if( arg_0 == 0x39 || arg_0 == 0x3D )
            {
                var_4 = ovr024.roll_dice(4, 5);
            }
            else if (arg_0 == 0x3B )
            {
                var_4 = (ushort)((ovr024.roll_dice(4, 1) * 10) + 0x28);
            }
            else if (arg_0 == 0x3F)
            {
                if (gbl.game_state == 5)
                {
                    var_4 = (ushort)(ovr024.roll_dice(10, 2) * 10);
                }
                else
                {
                    var_4 = (ushort)((ovr024.roll_dice(10, 1) + 10) * 10);
                }
            }
            else if (arg_0 == 0x43)
            {
                var_4 = 0x5A0;
            }
            else
            {
                var_4 = (ushort)(gbl.unk_19AEC[arg_0].field_4 + (gbl.unk_19AEC[arg_0].field_5 * ovr025.sub_6886F(arg_0)));
            }

            return var_4;
        }


        internal static void sub_5CF7F(string arg_0, byte arg_4, sbyte arg_6, bool arg_8, byte arg_A, byte arg_C)
        {
            byte var_31;
            bool var_30;
            Player var_2F;
            byte var_2B;
            byte var_2A;
            string var_29;

            var_29 = arg_0;

            if (arg_6 == 0)
            {
                gbl.byte_1D2BF = 0;
            }
            else
            {
                gbl.byte_1D2BF = arg_4;
            }

            if (gbl.byte_1D75E == 0)
            {
                if (arg_A == 0)
                {
                    var_2A = ovr025.sub_6886F(arg_C);
                }
                else
                {
                    var_2A = arg_A;
                }

                var_31 = gbl.byte_1D75E;

                for (var_2B = 1; var_2B <= var_31; var_2B++)
                {
                    if (gbl.sp_target[var_2B] != null)
                    {
                        var_2F = gbl.sp_target[var_2B];

                        if (gbl.unk_19AEC[arg_C].field_8 == 0)
                        {
                            var_30 = false;
                        }
                        else
                        {
                            var_30 = ovr024.do_saving_throw(0, gbl.unk_19AEC[arg_C].field_9, var_2F);
                        }

                        if (gbl.unk_19AEC[gbl.byte_1D2C1].field_2 == -1)
                        {
                            ovr025.sub_66C20(var_2F);

                            ovr024.work_on_00(var_2F, 11);

                            if (ovr024.sub_64245(var_2F.ac, var_2F, gbl.player_ptr) == false)
                            {
                                arg_6 = 0;
                                var_30 = true;
                            }
                        }

                        if (arg_6 > 0)
                        {
                            ovr024.damage_person(var_30, gbl.unk_19AEC[arg_C].field_8, arg_6, var_2F);

                        }

                        if (gbl.unk_19AEC[arg_C].field_A > 0)
                        {
                            ovr024.is_unaffected(var_29, var_30, gbl.unk_19AEC[arg_C].field_8,
                                arg_8, var_2A, sub_5CE92(arg_C), gbl.unk_19AEC[arg_C].field_A,
                                var_2F);
                        }
                    }
                }
                gbl.byte_1D2BF = 0;
            }
        }


        internal static void cast_spell_on(out bool arg_0, byte arg_4, byte arg_6)
        {
            if (gbl.dword_1D87F == null)
            {
                gbl.dword_1D87F = gbl.player_ptr;
            }

            gbl.sp_target[1] = gbl.player_ptr;
            gbl.byte_1D75E = 1;
            arg_0 = true;

            switch (gbl.unk_19AEC[arg_6].field_7)
            {
                case 1:
                    break;

                case 2:
                    ovr025.load_pic();

                    ovr025.selectAPlayer(ref gbl.dword_1D87F, true, "Cast Spell on whom");

                    if (gbl.dword_1D87F == null)
                    {
                        gbl.sp_target[1] = null;
                        gbl.byte_1D75E = 0;
                        arg_0 = false;
                    }
                    else
                    {
                        gbl.sp_target[1] = gbl.dword_1D87F;
                    }
                    break;

                case 4:
                    gbl.sp_target[gbl.byte_1D75E] = gbl.player_next_ptr;

                    while (gbl.sp_target[gbl.byte_1D75E].next_player != null)
                    {
                        gbl.sp_target[gbl.byte_1D75E] = gbl.sp_target[gbl.byte_1D75E].next_player;

                        gbl.byte_1D75E++;
                    }

                    break;

                default:
                    arg_0 = false;
                    break;
            }
        }


        internal static void sub_5D2E1(ref bool arg_0, byte arg_4, byte arg_6, byte arg_8)
        {
            Affect var_E;
            Player var_A;
            byte var_6;
            byte var_1;

            var_A = gbl.player_ptr;
            var_1 = 1;

            if (gbl.game_state != 5 &&
                gbl.unk_19AEC[arg_8].field_7 == 0)
            {
                if (gbl.byte_1D88D == 0)
                {

                    seg041.displayString(AffectNames[arg_8], 0, 10, 0x13, 1);
                    seg041.displayString("can't be cast here...", 0, 10, 0x14, 1);

                    if (ovr027.yes_no(15, 10, 13, "Lose it? ") == 'Y')
                    {
                        ovr025.clear_spell(arg_8, var_A);
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
                var_1 = 0;
            }

            if (ovr025.find_affect(out var_E, Affects.affect_4a, var_A) == true)
            {

                var_6 = ovr024.roll_dice(2, 1);

                if (var_6 == 1)
                {
                    cast_a_spell(arg_8, "miscasts", var_A);
                    arg_4 = 0;
                    var_1 = 0;
                }
            }

            if (arg_4 != 0 ||
                gbl.byte_1D88D == 0)
            {
                cast_a_spell(arg_8, "casts", var_A);
            }

            while (var_1 != 0)
            {
                gbl.dword_1D5CA(out arg_0, arg_6, arg_8);

                if (arg_0 == true)
                {
                    var_1 = 0;

                    if (gbl.game_state == 5)
                    {
                        ovr025.sub_67A59(0x12);
                        int var_3 = ovr033.PlayerMapXPos(var_A);
                        int var_4 = ovr033.PlayerMapYPos(var_A);

                        byte direction = 0;

                        while (ovr032.CanSeeCombatant(direction, gbl.byte_1D884, gbl.byte_1D883, var_4, var_3) == false)
                        {
                            direction++;
                        }

                        gbl.byte_1D910 = true;
                        ovr033.sub_74B3F(0, 1, direction, var_A);

                        if (arg_8 == 0x2F)
                        {
                            seg044.sound_sub_120E0(gbl.word_188D4);
                        }
                        else if (arg_8 == 0x33)
                        {
                            seg044.sound_sub_120E0(gbl.word_188CE);
                        }
                        else
                        {
                            seg044.sound_sub_120E0(gbl.word_188C2);
                        }

                        ovr025.sub_67AA4(0x1E, 4, gbl.byte_1D884, gbl.byte_1D883, var_4, var_3);

                        if (ovr033.sub_74761(0, var_A) == true)
                        {
                            ovr033.sub_74B3F(1, 1, var_A.actions.field_9, var_A);
                            ovr033.sub_74B3F(0, 0, var_A.actions.field_9, var_A);
                        }
                    }

                    ovr024.remove_affect_19(var_A);

                    if (gbl.byte_1D88D == 0)
                    {
                        ovr025.clear_spell(arg_8, var_A);
                    }

                    gbl.byte_1D2C1 = arg_8;

                    gbl.word_1D5CE[gbl.byte_1D2C1 - 1]();

                    gbl.byte_1D2C1 = 0;
                    gbl.byte_1D2C7 = 0;
                }
                else
                {
                    if (gbl.game_state != 5)
                    {
                        var_1 = 0;
                    }
                    else
                    {
                        if (arg_6 != 0 ||
                            ovr027.yes_no(15, 10, 14, "Abort Spell? ") == 'Y')
                        {
                            ovr025.string_print01("Spell Aborted");
                            if (gbl.byte_1D88D == 0)
                            {
                                ovr025.clear_spell(arg_8, var_A);
                            }

                            var_1 = 0;
                        }
                    }
                }
            }

            ovr025.ClearPlayerTextArea();

            if (gbl.game_state == 5)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static void sub_5D676(int arg_2, int arg_4, int arg_6, int arg_8,
            Struct_XXXX bp_var_1C)
        {
            bp_var_1C.field_00 = arg_8;
            bp_var_1C.field_02 = arg_6;
            bp_var_1C.field_04 = arg_4;
            bp_var_1C.field_06 = arg_2;
            ovr032.sub_731A5(bp_var_1C);
        }


        static void BoundCoords(ref int rowY, ref int colX) /* sub_5D6B7 */
        {
            colX = System.Math.Min(colX, Display.MaxCol);
            colX = System.Math.Max(colX, Display.MinCol);

            rowY = System.Math.Min(rowY, Display.MaxRow);
            rowY = System.Math.Max(rowY, Display.MinRow);
        }


        internal static void sub_5D702(Struct_XXXX bp_var_1C, bool bp_var_1D, ref byte bp_var_73, ref byte bp_var_72, byte[] bp_var_71)
        {
            bool var_2;
            byte var_1;

            do
            {
                bp_var_1D = !ovr032.sub_7324C(bp_var_1C);

                if (bp_var_1D == false)
                {
                    ovr033.AtMapXY(out bp_var_73, out bp_var_72, bp_var_1C.field_10, bp_var_1C.field_0E);
                    var_1 = 1;
                    var_2 = false;

                    if (bp_var_72 > 0)
                    {
                        do
                        {
                            if (bp_var_71[var_1] == bp_var_72)
                            {
                                var_2 = true;
                            }
                            else if (bp_var_71[var_1] == 0)
                            {
                                bp_var_71[var_1] = bp_var_72;
                                var_2 = true;
                            }
                            else
                            {
                                var_1++;
                            }

                        } while (var_2 == false);
                    }
                }
            } while (bp_var_1D == false);
        }


        static sbyte[] unk_16D22 = { -1, 0, 0, 1, 1, 0, 0, -1 };
        static sbyte[] unk_16D2A = { 0, -1, -1, 0, 0, 1, 1, 0 };
        static sbyte[] unk_16D32 = { 1, 1, 0, 0, -1, -1, 0, 0 };
        static sbyte[] unk_16D3A = { 0, 0, 1, 1, 0, 0, -1, -1 };

        internal static void sub_5D7CF(byte arg_0, byte arg_2, int arg_4, int arg_6, int arg_8, int arg_A)
        {
            byte var_7A;
            byte var_79 = 0; /* Simeon */
            short var_78 = 0; /* Simeon */
            byte var_76 = 0; /* Simeon */
            byte var_75;
            byte var_74;
            byte var_73 = 0; /* Simeon */
            byte var_72 = 0; /* Simeon */
            byte[] var_71 = new byte[0x1E];
            byte[] var_53 = new byte[0x32];
            byte var_21;
            byte var_20;
            byte var_1F;
            bool var_1E;
            bool var_1D;
            Struct_XXXX var_1C = new Struct_XXXX();

            var_20 = 0;
            sub_5D676(arg_4, arg_6, arg_8, arg_A, var_1C);

            do
            {
                var_1D = !ovr032.sub_7324C(var_1C);

                if (var_1D == true)
                {
                    var_53[var_20] = var_1C.field_17;
                    var_20++;
                }
            } while (var_1D == false);

            var_21 = (byte)(var_20 - 1);
            var_20 = 0;
            arg_0 = (byte)(arg_0 * 2);
            var_1F = var_1C.field_16;
            var_1D = false;

            while (var_1F < arg_0 && var_1D == false)
            {
                if (arg_6 < 0x31 && arg_6 > 0 && arg_4 < 0x18 && arg_4 > 0)
                {
                    switch (var_53[var_20])
                    {
                        case 0: goto case 6;
                        case 2: goto case 6;
                        case 4: goto case 6;
                        case 6:
                            var_1F += 2;
                            break;

                        case 1: goto case 7;
                        case 3: goto case 7;
                        case 5: goto case 7;
                        case 7:
                            var_1F += 3;
                            break;
                    }

                    arg_6 += gbl.MapDirectionXDelta[var_53[var_20]];
                    arg_4 += gbl.MapDirectionYDelta[var_53[var_20]];


                    if (var_20 == var_21)
                    {
                        var_20 = 0;
                    }
                    else
                    {
                        var_20++;
                    }
                }
                else
                {
                    var_1D = true;
                }

            }

            BoundCoords(ref arg_4, ref arg_6);

            var_1E = ovr032.sub_733F1(gbl.mapToBackGroundTile, ref var_78, ref arg_4, ref arg_6, arg_8, arg_A);

            sub_5D676(arg_4, arg_6, arg_8, arg_A, var_1C);

            seg051.FillChar(0, 0x1E, var_71);
            var_74 = 1;

            do
            {
                var_1D = !ovr032.sub_7324C(var_1C);

                if (var_1D == false)
                {
                    ovr033.AtMapXY(out var_73, out var_72, var_1C.field_10, var_1C.field_0E);

                    if (var_72 > 0)
                    {
                        if (var_74 > 1)
                        {
                            var_7A = (byte)(var_74 - 1);

                            for (gbl.byte_1DA71 = 1; gbl.byte_1DA71 < var_7A; gbl.byte_1DA71++)
                            {
                                if (var_71[gbl.byte_1DA71 - 1] == var_72)
                                {
                                    var_79 = 1;
                                }
                            }
                        }
                        else
                        {
                            var_79 = 0;
                        }

                        if (var_79 == 0)
                        {
                            var_71[var_74] = var_72;
                            var_74++;
                        }

                        var_76 = var_1C.field_17;
                    }
                }
            } while (var_1D == false);

            if (arg_2 > 1)
            {
                int var_1 = arg_6 + unk_16D22[var_76];
                int var_2 = arg_4 + unk_16D2A[var_76];

                BoundCoords(ref var_2, ref var_1);

                int var_3 = arg_6 + unk_16D32[var_76];
                int var_4 = arg_4 + unk_16D3A[var_76];

                BoundCoords(ref var_4, ref var_3);

                sub_5D676(var_4, var_3, arg_8, arg_A, var_1C);
                sub_5D702(var_1C, var_1D, ref var_73, ref var_72, var_71);

                if (arg_2 > 2)
                {
                    sub_5D676(var_2, var_1, arg_8, arg_A, var_1C);
                    sub_5D702(var_1C, var_1D, ref var_73, ref var_72, var_71);
                }
            }

            var_74 = 1;
            var_75 = 1;
            gbl.byte_1D75E = 0;

            if (var_71[1] != 0)
            {
                do
                {
                    if (gbl.player_array[var_71[var_74]] != gbl.player_ptr)
                    {
                        gbl.byte_1D75E++;
                        gbl.sp_target[var_75] = gbl.player_array[var_74];
                        var_75++;
                    }
                    var_74++;
                } while (var_71[var_74] != 0);
            }
        }


        internal static void sub_5DB24(string arg_0, sbyte arg_4)
        {
            byte var_30;
            byte var_2F;
            bool var_2E;
            Player var_2D;
            string var_29;

            var_29 = arg_0;

            for (var_2F = gbl.byte_1D75E; var_2F >= 1; var_2F--)
            {
                if (gbl.sp_target[var_2F] != null)
                {
                    var_2D = gbl.sp_target[var_2F];

                    if (var_2F < gbl.byte_1D75E)
                    {
                        seg044.sound_sub_120E0(gbl.word_188C2);
                        ovr025.sub_67A59(0x12);

                        ovr025.sub_67AA4(0x1E, 4, ovr033.PlayerMapYPos(var_2D), ovr033.PlayerMapXPos(var_2D),
                            ovr033.PlayerMapYPos(gbl.player_ptr), ovr033.PlayerMapXPos(gbl.player_ptr));
                    }

                    if ((gbl.byte_1D2C1 == 0x4F || gbl.byte_1D2C1 == 0x51) &&
                        var_2F == gbl.byte_1D75E)
                    {
                        var_2E = true;
                        var_30 = 1;
                    }
                    else
                    {
                        var_2E = ovr024.do_saving_throw(arg_4, gbl.unk_19AEC[gbl.byte_1D2C1].field_9, var_2D);
                        var_30 = gbl.unk_19AEC[gbl.byte_1D2C1].field_8;
                    }

                    if ((var_2D.field_11A > 1 || var_2D.field_DE > 1) &&
                        gbl.byte_1D2C1 != 0x53)
                    {
                        var_2E = true;
                    }

                    ovr024.is_unaffected(var_29, var_2E, var_30, false, ovr025.sub_6886F(gbl.byte_1D2C1), sub_5CE92(gbl.byte_1D2C1),
                        gbl.unk_19AEC[gbl.byte_1D2C1].field_A, var_2D);
                }
            }
        }


        internal static void sub_5DCA0(string arg_0, sbyte arg_4)
        {
            byte var_2B;
            byte var_2A;
            string var_29;

            var_29 = arg_0;
            gbl.byte_1D2C7 = 1;
            var_2B = gbl.byte_1D75E;

            for (var_2A = 1; var_2A <= var_2B; var_2A++)
            {
                if (gbl.sp_target[var_2A].combat_team != arg_4 ||
                    (gbl.byte_1D2C1 == 1 && gbl.game_state == 5 &&
                     ovr025.near_enemy(1, gbl.sp_target[var_2A]) > 0))
                    ovr025.near_enemy(1, gbl.sp_target[var_2A]);
                {
                    gbl.sp_target[var_2A] = null;
                }
            }

            sub_5CF7F(var_29, 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_Blessed()
        {
            sub_5DCA0("is Blessed", gbl.player_ptr.combat_team);
        }


        internal static void is_Cursed()
        {
            sub_5DCA0("is Cursed", ovr025.on_our_team(gbl.player_ptr));
        }


        internal static void sub_5DDBC()
        {
            if (gbl.byte_1D75E != 0 &&
                ovr024.heal_player(0, ovr024.roll_dice(8, 1), gbl.sp_target[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_target[1]);
            }
        }


        internal static void sub_5DDF8()
        {
            sub_5CF7F(string.Empty, 8, ovr024.roll_dice_save(8, 1), false, 0, gbl.byte_1D2C1);
        }


        internal static void is_affected()
        {
            sub_5CF7F("is affected", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_protected()
        {
            sub_5CF7F("is protected", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_cold_resistant()
        {
            sub_5CF7F("is cold-resistant", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_5DEE1()
        {
            sub_5CF7F(string.Empty, 9, (sbyte)ovr025.sub_6886F(gbl.byte_1D2C1), false, 0, gbl.byte_1D2C1);
        }


        internal static void is_charmed()
        {
            Player var_8;
            Affect var_4;

            var_8 = gbl.sp_target[1];

            if (var_8.field_11A > 1 ||
                var_8.field_DE > 1)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", var_8);
            }
            else
            {
                sub_5CF7F("is charmed", 0, 0, true, (byte)((gbl.player_ptr.combat_team << 7) + ovr025.sub_6886F(gbl.byte_1D2C1)), gbl.byte_1D2C1);

                if (ovr025.find_affect(out var_4, Affects.charm_person, var_8) == true)
                {
                    ovr024.sub_630C7(0, var_4, gbl.sp_target[1], Affects.shield);
                }
            }
        }


        internal static void is_stronger()
        {
            Player var_3;
            byte var_1;

            var_3 = gbl.sp_target[1];
            gbl.byte_1AFDD = 0x12;
            gbl.byte_1AFDE = 0;

            switch (ovr025.sub_6886F(gbl.byte_1D2C1))
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

            if (ovr024.sub_64728(out var_1, gbl.byte_1AFDE, gbl.byte_1AFDD, var_3) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is stronger", var_3);

                ovr024.add_affect(true, var_1, sub_5CE92(gbl.byte_1D2C1), Affects.affect_12, var_3);

                ovr024.sub_648D9(0, var_3);
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", var_3);
            }
        }


        internal static void has_been_reduced()
        {
            Player var_6;
            Affect var_4;

            var_6 = gbl.sp_target[1];

            if (var_6 != null &&
                gbl.byte_1D75E > 0 &&
                ovr024.do_saving_throw(0, 4, var_6) == false &&
                ovr025.find_affect(out var_4, Affects.enlarge, var_6) == true)
            {
                ovr024.remove_affect(null, Affects.enlarge, var_6);
                ovr024.sub_648D9(0, var_6);
                ovr025.DisplayPlayerStatusString(true, 10, "has been reduced", var_6);
            }
        }


        internal static void is_friendly()
        {
            sub_5CF7F("is friendly", 0, 0, true, ovr024.roll_dice(4, 2), gbl.byte_1D2C1);
            ovr024.sub_648D9(5, gbl.player_ptr);
        }


        internal static void sub_5E221()
        {
            sbyte var_1;

            var_1 = (sbyte)(ovr025.sub_6886F(gbl.byte_1D2C1) + 1);

            sub_5CF7F(string.Empty, 8, (sbyte)((var_1 >> 1) + ovr024.roll_dice_save(4, (sbyte)(var_1 >> 1))), false, 0, gbl.byte_1D2C1);
        }


        internal static void is_shielded()
        {
            sub_5CF7F("is shielded", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_5E2B2()
        {
            sub_5CF7F(string.Empty, 12, (sbyte)(ovr024.roll_dice_save(8, 1) + ovr025.sub_6886F(gbl.byte_1D2C1)),
                false, 0, gbl.byte_1D2C1);
        }


        internal static void falls_asleep()
        {
            byte var_6;
            Affect var_5;
            byte var_1;

            gbl.byte_1D2C7 = 1;

            gbl.byte_1AFDD = ovr024.roll_dice(4, 4);
            var_6 = gbl.byte_1D75E;

            for (var_1 = 1; var_1 <= var_6; var_1++)
            {
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//les	di, sp_target[di]
                throw new System.NotSupportedException();//mov	al, es:[di+0E5h]
                throw new System.NotSupportedException();//cmp	al, 0
                throw new System.NotSupportedException();//jl	loc_5E35A
                throw new System.NotSupportedException();//cmp	al, 1
                throw new System.NotSupportedException();//jg	loc_5E35A
                gbl.byte_1AFDE = 1;
                throw new System.NotSupportedException();//jmp	short loc_5E3A8
                throw new System.NotSupportedException();//loc_5E35A:
                throw new System.NotSupportedException();//cmp	al, 2
                throw new System.NotSupportedException();//jnz	loc_5E365
                gbl.byte_1AFDE = 2;
                throw new System.NotSupportedException();//jmp	short loc_5E3A8
                throw new System.NotSupportedException();//loc_5E365:
                throw new System.NotSupportedException();//cmp	al, 3
                throw new System.NotSupportedException();//jnz	loc_5E370
                gbl.byte_1AFDE = 4;
                throw new System.NotSupportedException();//jmp	short loc_5E3A8
                throw new System.NotSupportedException();//loc_5E370:
                throw new System.NotSupportedException();//cmp	al, 4
                throw new System.NotSupportedException();//jnz	loc_5E37B
                gbl.byte_1AFDE = 6;
                throw new System.NotSupportedException();//jmp	short loc_5E3A8
                throw new System.NotSupportedException();//loc_5E37B:
                throw new System.NotSupportedException();//cmp	al, 5
                throw new System.NotSupportedException();//jnz	loc_5E3A3
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//les	di, sp_target[di]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+74h], 0
                throw new System.NotSupportedException();//jnz	loc_5E39C
                gbl.byte_1AFDE = 0x0A;
                throw new System.NotSupportedException();//jmp	short loc_5E3A1
                throw new System.NotSupportedException();//loc_5E39C:
                gbl.byte_1AFDE = 0x14;
                throw new System.NotSupportedException();//loc_5E3A1:
                throw new System.NotSupportedException();//jmp	short loc_5E3A8
                throw new System.NotSupportedException();//loc_5E3A3:
                gbl.byte_1AFDE = 0x14;
                throw new System.NotSupportedException();//loc_5E3A8:
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//les	di, sp_target[di]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+195h], 1
                throw new System.NotSupportedException();//jz	loc_5E3F5

                ovr025.find_affect(out var_5, Affects.sleep, gbl.sp_target[var_1]);
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jnz	loc_5E3F5
                throw new System.NotSupportedException();//mov	al, byte_1AFDD
                throw new System.NotSupportedException();//cmp	al, byte_1AFDE
                throw new System.NotSupportedException();//jb	loc_5E3F5
                throw new System.NotSupportedException();//mov	al, byte_1AFDE
                throw new System.NotSupportedException();//sub	byte_1AFDD, al
                throw new System.NotSupportedException();//jmp	short loc_5E40A
                throw new System.NotSupportedException();//loc_5E3F5:
                gbl.sp_target[var_1] = null;
                throw new System.NotSupportedException();//loc_5E40A:
            }

            sub_5CF7F("falls asleep", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_held()
        {
            sbyte var_1;

            if (gbl.byte_1D75E == 1)
            {
                if (gbl.byte_1D2C1 == 0x17)
                {
                    var_1 = -2;
                }
                else
                {
                    var_1 = -3;
                }
            }
            else if (gbl.byte_1D75E == 2)
            {
                var_1 = -1;
            }
            else if (gbl.byte_1D75E == 3 || gbl.byte_1D75E == 4)
            {
                var_1 = 0;
            }
            else
            {
                throw new System.NotSupportedException();
            }


            sub_5DB24("is held", var_1);
        }


        internal static void is_fire_resistant()
        {
            sub_5CF7F("is fire resistant", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_silenced()
        {
            sub_5CF7F("is silenced", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_affected2()
        {
            Player player;
            Affect var_4;

            player = gbl.sp_target[1];

            if (player.health_status == Status.animated)
            {
                gbl.sp_target[1] = null;
            }
            else if (ovr025.find_affect(out var_4, Affects.poisoned, player) == true)
            {
                if (player.hit_point_current == 0)
                {
                    player.hit_point_current = 1;
                }

                sub_5CF7F("is affected", 0, 0, true, 0xff, gbl.byte_1D2C1);
                ovr024.sub_630C7(1, null, player, Affects.affect_4e);
                ovr024.add_affect(true, 0xff, 10, Affects.affect_0f, player);
            }
        }


        internal static void is_charmed2()
        {
            Player player;

            gbl.byte_1AFDD = gbl.player_ptr.hit_point_current;
            gbl.byte_1D75E = 0;
            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.field_11A == 0x0e &&
                    gbl.byte_1AFDD >= player.hit_point_current)
                {
                    gbl.byte_1AFDD -= player.hit_point_current;
                    gbl.byte_1D75E++;
                    gbl.sp_target[gbl.byte_1D75E] = player;
                }

                player = player.next_player;
            }

            sub_5CF7F("is charmed", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_5E681()
        {
            sub_5CF7F(string.Empty, 0, 0, true, 0, gbl.byte_1D2C1);

            ovr024.sub_630C7(0, null, gbl.sp_target[1], Affects.spiritual_hammer);
        }


        internal static void is_invisible()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void knock_Knock()
        {
            sub_5CF7F("Knock-Knock", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void is_duplicated()
        {
            byte var_1;

            var_1 = (byte)(ovr024.roll_dice(4, 1) << 4);

            var_1 |= ovr025.sub_6886F(gbl.byte_1D2C1);

            sub_5CF7F("is duplicated", 0, 0, false, var_1, gbl.byte_1D2C1);
        }


        internal static void is_weakened()
        {
            sub_5CF7F("is weakened", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void create_noxious_cloud()
        {
            byte var_12;
            byte var_11;
            byte var_10;
            byte var_F;
            byte var_E;
            byte var_D;
            byte[] var_C = new byte[4];
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            gbl.byte_1D2C7 = 1;

            var_10 = ovr025.sub_6886F(gbl.byte_1D2C1);
            var_E = 0;
            var_8 = gbl.stru_1D885;

            if (gbl.stru_1D885 == null)
            {
                gbl.stru_1D885 = new Struct_1D885();
                var_8 = gbl.stru_1D885;
            }
            else
            {
                while (var_8.next != null)
                {
                    if (var_8.player == gbl.player_ptr)
                    {
                        var_E++;
                    }

                    var_8 = var_8.next;
                }

                var_8.next = new Struct_1D885();
                var_8 = var_8.next;
            }

            ovr024.add_affect(true, (byte)(var_10 + (var_E << 4)), var_10, Affects.affect_28, gbl.player_ptr);

            var_8.player = gbl.player_ptr;
            var_8.field_1C = var_E;
            var_8.field_1A = (sbyte)gbl.byte_1D883;
            var_8.field_1B = (sbyte)gbl.byte_1D884;

            for (var_11 = 1; var_11 <= 4; var_11++)
            {
                var_12 = gbl.unk_18AE9[var_11];

                ovr033.AtMapXY(out var_F, out var_C[var_11 - 1],
                    gbl.byte_1D884 + gbl.MapDirectionYDelta[var_12],
                    gbl.byte_1D883 + gbl.MapDirectionXDelta[var_12]);


                if (var_F > 0 && gbl.BackGroundTiles[var_F].move_cost < 0xFF)
                {
                    var_8.field_10[var_11] = 1;
                }
                else
                {
                    var_8.field_10[var_11] = 0;
                }


                if (var_F == 0x1E)
                {
                    var_4 = gbl.stru_1D885;

                    while (var_4 != null)
                    {
                        if (var_4 != var_8)
                        {
                            for (var_D = 1; var_D <= 4; var_D++)
                            {
                                if (var_4.field_10[var_D] != 0)
                                {
                                    if (gbl.byte_1D883 + gbl.MapDirectionXDelta[var_12] == var_4.field_1A + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_D]] &&
                                        gbl.byte_1D884 + gbl.MapDirectionYDelta[var_12] == gbl.MapDirectionYDelta[gbl.unk_18AE9[var_D]] + var_4.field_1B)
                                    {
                                        if (var_4.field_7[var_D] != 0x1E)
                                        {
                                            var_F = var_4.field_7[var_D];
                                        }
                                    }
                                }
                            }
                        }
                        var_4 = var_4.next;
                    }
                }
                else if (var_F == 0x1F)
                {
                    for (var_D = 1; var_D <= gbl.byte_1D1BB; var_D++)
                    {
                        if (gbl.unk_1D183[var_D].mapX == gbl.byte_1D883 + gbl.MapDirectionXDelta[var_12] &&
                            gbl.unk_1D183[var_D].mapY == gbl.byte_1D884 + gbl.MapDirectionYDelta[var_12])
                        {
                            var_F = gbl.unk_1D183[var_D].field_6;
                        }
                    }
                }

                var_8.field_7[var_11] = var_F;
                if (var_8.field_10[var_11] != 0)
                {
                    int cx = gbl.MapDirectionXDelta[var_12] + gbl.byte_1D883;
                    int ax = gbl.MapDirectionYDelta[var_12] + gbl.byte_1D884;

                    gbl.mapToBackGroundTile[cx, ax] = 0x1E;
                }
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a noxious cloud", gbl.player_ptr);

            ovr033.sub_749DD(8, 0xff, gbl.byte_1D884, gbl.byte_1D883);
            seg041.GameDelay();
            ovr025.ClearPlayerTextArea();
            for (var_11 = 0; var_11 < 4; var_11++)
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

            for (var_11 = 0; var_11 < 4; var_11++)
            {
                if (var_C[var_11] > 0)
                {
                    ovr024.in_poison_cloud(1, gbl.player_array[var_C[var_11]]);
                }
            }
        }


        internal static void sub_5EC5B()
        {
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            Player player;

            var_6 = 0; /* simeon added */
            var_5 = 0;
            player = gbl.sp_target[1];

            if (player.magic_user_lvl > 0 ||
                player.field_116 > player.field_E6)
            {
                var_6 = ovr024.roll_dice(4, 1);
            }

            if (player.cleric_lvl > 0 ||
                player.turn_undead > player.field_E6 ||
                player.thief_lvl > 0 ||
                player.field_117 > player.field_E6)
            {
                var_6 = ovr024.roll_dice(6, 1);
            }

            if (player.fighter_lvl > 0 ||
                player.field_113 > player.field_E6)
            {

                var_6 = ovr024.roll_dice(8, 1);
            }

            var_7 = (byte)(player.strength + var_6);

            if (var_7 > 18)
            {
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Bh], 0
                throw new System.NotSupportedException();//jg	loc_5ED85
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//mov	al, es:[di+113h]
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	al, es:[di+0E6h]
                throw new System.NotSupportedException();//jg	loc_5ED85
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Ch], 0
                throw new System.NotSupportedException();//jg	loc_5ED85
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//mov	al, es:[di+114h]
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	al, es:[di+0E6h]
                throw new System.NotSupportedException();//jg	loc_5ED85
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+10Dh], 0
                throw new System.NotSupportedException();//jg	loc_5ED85
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//mov	al, es:[di+115h]
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	al, es:[di+0E6h]
                throw new System.NotSupportedException();//jle	loc_5EDB2
                throw new System.NotSupportedException();//loc_5ED85:

                var_5 = (byte)(player.strength_18_100 + ((var_7 - 18) * 10));

                if (var_5 > 100)
                {
                    var_5 = 100;
                }

                var_7 = 18;
                throw new System.NotSupportedException();//jmp	short loc_5EDB6
                throw new System.NotSupportedException();//loc_5EDB2:
                var_7 = 18;
            }
            throw new System.NotSupportedException();//loc_5EDB6:
            if (ovr024.sub_64728(out var_8, var_5, var_7, player) == true)
            {
                var_8 = (byte)(var_6 + 100);

                ovr024.add_affect(true, var_8, sub_5CE92(gbl.byte_1D2C1), Affects.strength, player);
                ovr024.sub_648D9(0, player);
            }

        }


        internal static void is_animated()
        {
            Player var_7;
            byte var_3;
            byte var_2;
            byte var_1;

            gbl.byte_1D2C7 = 1;

            var_3 = ovr025.sub_6886F(gbl.byte_1D2C1);

            gbl.byte_1D75E = 0;
            var_7 = gbl.player_next_ptr;

            while (var_7 != null && var_3 > 0)
            {
                if (var_7.health_status == Status.dead &&
                    var_7.field_11A == 0)
                {

                    if (ovr033.sub_7515A(1, ovr033.PlayerMapYPos(var_7), ovr033.PlayerMapXPos(var_7), var_7) != 0)
                    {
                        var_2 = (byte)((var_7.combat_team << 4) + ovr025.sub_6886F(gbl.byte_1D2C1));

                        var_7.combat_team = gbl.player_ptr.combat_team;
                        var_7.field_198 = 1;
                        var_7.field_E9 = 1;
                        var_7.field_DD = 0;
                        var_7.field_E4 = 6;

                        for (var_1 = 0; var_1 < gbl.max_spells; var_1++)
                        {
                            var_7.spell_list[var_1] = 0;
                        }

                        if (var_7.field_F7 > 0xf7)
                        {
                            var_7.field_F7 = 0xB2;
                        }
                        else
                        {
                            var_7.field_F7 = 0xB3;
                        }

                        var_7.field_11A = 4;

                        if (gbl.game_state == 5)
                        {
                            var_7.actions.target = null;
                        }

                        var_3--;

                        if (ovr024.combat_heal(var_7.hit_point_max, var_7) == true)
                        {
                            ovr024.is_unaffected("is animated", false, 0, true, var_2, 0, Affects.funky__32, var_7);
                            var_7.health_status = Status.animated;
                        }
                    }
                }

                var_7 = var_7.next_player;
            }
        }


        internal static void can_see()
        {
            if (ovr024.is_cured(Affects.blinded, gbl.sp_target[1]) == true)
            {
                ovr025.sub_6818A("can see", 1, gbl.sp_target[1]);
            }
        }


        internal static void is_blind()
        {
            sub_5CF7F("is blind", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static bool sub_5F037()
        {
            bool var_1;

            var_1 = false;
            gbl.byte_1D2C6 = 1;

            if (ovr024.is_cured(Affects.cause_disease_1, gbl.sp_target[1]) == true)
            {
                var_1 = true;
            }

            if (ovr024.is_cured(Affects.affect_2b, gbl.sp_target[1]) == true)
            {
                var_1 = true;

                ovr024.remove_affect(null, Affects.cause_disease_2, gbl.sp_target[1]);
                ovr024.remove_affect(null, Affects.helpless, gbl.sp_target[1]);
            }

            if (ovr024.is_cured(Affects.hot_fire_shield, gbl.sp_target[1]) == true)
            {
                var_1 = true;
                ovr024.remove_affect(null, Affects.affect_39, gbl.sp_target[1]);
            }

            gbl.byte_1D2C6 = 0;

            return var_1;
        }


        internal static void sub_5F0DC()
        {
            sub_5F037();
        }


        internal static void is_diseased()
        {
            sub_5CF7F("is diseased", 0, 0, true, 0, gbl.byte_1D2C1);
        }


        internal static bool sub_5F126(Player arg_2, byte bp_var_2)
        {
            bool var_1;

            gbl.byte_1AFDE = (byte)(arg_2.magic_user_lvl + (arg_2.field_116 * ovr026.sub_6B3D1(gbl.player_ptr)));

            if (bp_var_2 > gbl.byte_1AFDE)
            {
                gbl.byte_1AFDD = (byte)(((bp_var_2 - gbl.byte_1AFDE) * 5) + 50);
            }
            else if (bp_var_2 < gbl.byte_1AFDE)
            {
                gbl.byte_1AFDD = (byte)(50 - ((gbl.byte_1AFDE - bp_var_2) * 2));
            }
            else
            {
                gbl.byte_1AFDD = 50;
            }

            var_1 = (ovr024.roll_dice(100, 1) <= gbl.byte_1AFDD);

            return var_1;
        }


        internal static void is_affected3()
        {
            byte var_19;
            Struct_1D885 var_18;
            byte var_14;
            byte var_13;
            int var_12 = 0; /* Simeon */
            int var_11 = 0; /* Simeon */
            Affect var_10;
            Affect var_C;
            byte var_8;
            byte var_7;
            Player var_6;
            byte var_2;
            byte var_1;

            gbl.byte_1D2C7 = 1;
            var_2 = ovr025.sub_6886F(gbl.byte_1D2C1);
            var_19 = gbl.byte_1D75E;

            for (var_1 = 1; var_1 <= var_19; var_1++)
            {
                var_7 = 0;
                var_6 = gbl.sp_target[1];
                var_C = var_6.affect_ptr;

                while (var_C != null)
                {
                    var_10 = var_C.next;

                    if (var_C.field_3 < 0xff)
                    {
                        gbl.byte_1AFDE = (byte)(var_C.field_3 & 0x0f);

                        if (var_2 > gbl.byte_1AFDE)
                        {
                            gbl.byte_1AFDD = (byte)(50 + ((var_2 - gbl.byte_1AFDE) * 5));
                        }
                        else if (var_2 < gbl.byte_1AFDE)
                        {
                            gbl.byte_1AFDD = (byte)(50 - ((gbl.byte_1AFDE - var_2) * 2));
                        }
                        else
                        {
                            gbl.byte_1AFDD = 50;
                        }

                        if (ovr024.roll_dice(100, 1) <= gbl.byte_1AFDD)
                        {
                            ovr024.remove_affect(var_C, var_C.type, var_6);
                            var_7 = 1;
                        }
                    }

                    var_C = var_10;
                }

                if (var_7 != 0)
                {
                    ovr025.sub_6818A("is affected", 1, var_6);
                }
            }

            for (gbl.byte_1DA71 = 0; gbl.byte_1DA71 <= 8; gbl.byte_1DA71++)
            {
                switch (gbl.byte_1DA71)
                {
                    case 0:
                        var_11 = gbl.byte_1D883;
                        var_12 = gbl.byte_1D884;
                        break;

                    case 1:
                        var_12 = (sbyte)(gbl.byte_1D884 - 1);
                        break;

                    case 2:
                        var_11 = (sbyte)(gbl.byte_1D883 - 1);
                        break;

                    case 3:
                        var_12 = gbl.byte_1D884;
                        break;

                    case 4:
                        var_12 = (sbyte)(gbl.byte_1D884 + 1);
                        break;

                    case 5:
                        var_11 = gbl.byte_1D883;
                        break;

                    case 6:
                        var_11 = (sbyte)(gbl.byte_1D883 - 1);
                        break;

                    case 7:
                        var_12 = gbl.byte_1D884;
                        break;

                    case 8:
                        var_12 = (sbyte)(gbl.byte_1D884 - 1);
                        break;
                }

                ovr033.AtMapXY(out var_13, out var_14, var_12, var_11);

                if (var_13 == 0x1C || var_13 == 0x1E)
                {

                    if (var_13 == 0x1C)
                    {
                        var_18 = gbl.stru_1D889;
                        var_14 = 9;
                    }
                    else
                    {
                        var_18 = gbl.stru_1D885;
                        var_14 = 4;
                    }

                    while (var_18 != null)
                    {
                        var_19 = var_14;

                        for (var_1 = 1; var_1 <= var_19; var_1++)
                        {
                            if (var_11 == var_18.field_1A + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_1]])
                            {
                                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.unk_18AE9[di]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//les	di, [bp+var_18]
                                throw new System.NotSupportedException();//mov	al, es:[di+1Bh]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//cmp	ax, dx
                                throw new System.NotSupportedException();//jz	loc_5F47C
                                throw new System.NotSupportedException();//jmp	loc_5F562
                                throw new System.NotSupportedException();//loc_5F47C:
                                throw new System.NotSupportedException();//les	di, [bp+var_18]
                                throw new System.NotSupportedException();//cmp	byte ptr es:[di+1Dh], 0
                                throw new System.NotSupportedException();//jz	loc_5F489
                                throw new System.NotSupportedException();//jmp	loc_5F562
                                throw new System.NotSupportedException();//loc_5F489:
                                if (sub_5F126(var_18.player, var_2) == true)
                                {

                                    var_C = var_18.player.affect_ptr;
                                    var_8 = 0;

                                    while (var_C != null && var_8 == 0)
                                    {
                                        throw new System.NotSupportedException();//les	di, [bp+var_C]
                                        throw new System.NotSupportedException();//cmp	byte ptr es:[di], 0x5B
                                        throw new System.NotSupportedException();//jnz	loc_5F4D6
                                        throw new System.NotSupportedException();//cmp	[bp+var_13], 0x1C
                                        throw new System.NotSupportedException();//jz	loc_5F4E5
                                        throw new System.NotSupportedException();//loc_5F4D6:
                                        throw new System.NotSupportedException();//les	di, [bp+var_C]
                                        throw new System.NotSupportedException();//cmp	byte ptr es:[di], 0x28
                                        throw new System.NotSupportedException();//jnz	loc_5F507
                                        throw new System.NotSupportedException();//cmp	[bp+var_13], 0x1E
                                        throw new System.NotSupportedException();//jnz	loc_5F507
                                        throw new System.NotSupportedException();//loc_5F4E5:
                                        throw new System.NotSupportedException();//les	di, [bp+var_18]
                                        throw new System.NotSupportedException();//mov	al, es:[di+1Ch]
                                        throw new System.NotSupportedException();//xor	ah, ah
                                        throw new System.NotSupportedException();//mov	dx, ax
                                        throw new System.NotSupportedException();//les	di, [bp+var_C]
                                        throw new System.NotSupportedException();//mov	al, es:[di+3]
                                        throw new System.NotSupportedException();//xor	ah, ah
                                        throw new System.NotSupportedException();//mov	cl, 4
                                        throw new System.NotSupportedException();//shr	ax, cl
                                        throw new System.NotSupportedException();//cmp	ax, dx
                                        throw new System.NotSupportedException();//jnz	loc_5F507
                                        var_8 = 1;
                                        throw new System.NotSupportedException();//jmp	short loc_5F518
                                        throw new System.NotSupportedException();//loc_5F507:
                                        var_C = var_C.next;
                                        throw new System.NotSupportedException();//loc_5F518:
                                    }

                                    if (var_8 != 0)
                                    {
                                        if (var_13 == 0x1C)
                                        {
                                            ovr024.remove_affect(var_C, Affects.affect_5b, var_18.player);
                                        }
                                        else
                                        {
                                            ovr024.remove_affect(var_C, Affects.affect_28, var_18.player);
                                        }
                                    }
                                }
                                else
                                {
                                    var_18.field_1D = 1;
                                }
                            }
                            throw new System.NotSupportedException();//loc_5F562:
                        }

                        var_18 = var_18.next;
                    }
                }
            }

        }


        internal static void is_praying()
        {
            byte tmpByte = (byte)((gbl.player_ptr.combat_team << 4) + ovr025.sub_6886F(gbl.byte_1D2C1));

            sub_5CF7F("is praying", 0, 0, false, tmpByte, gbl.byte_1D2C1);
        }


        internal static void uncurse()
        {
            byte var_6;
            byte var_5;
            Item var_4;

            if (ovr024.is_cured(Affects.bestow_curse, gbl.sp_target[1]) == true)
            {
                ovr025.sub_6818A("is un-cursed", 1, gbl.sp_target[1]);
            }
            else
            {
                var_5 = 0;
                var_4 = gbl.sp_target[1].itemsPtr;

                while (var_4 != null && var_5 == 0)
                {
                    if (var_4.field_36 != 0)
                    {
                        var_5 = 1;
                        var_4.readied = false;

                        if ((int)var_4.affect_3 > 0x7F)
                        {
                            gbl.byte_1D8AC = 1;
                            ovr024.sub_630C7(1, var_4, gbl.sp_target[1], var_4.affect_3);

                            for (var_6 = 0; var_6 <= 5; var_6++)
                            {
                                ovr024.sub_648D9(var_6, gbl.sp_target[1]);
                            }

                        }
                    }

                    var_4 = var_4.next;
                }

                if (var_5 != 0)
                {
                    ovr025.sub_6818A("has an item un-cursed", 1, gbl.sp_target[1]);
                }
            }
        }


        internal static void curse()
        {
            sub_5CF7F("has been cursed!", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void spell_blinking()
        {
            sub_5CF7F("is blinking", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_5F782()
        {
            byte var_3;
            byte var_2;
            sbyte var_1;

            gbl.byte_1D2C7 = 1;

            if (gbl.byte_1D2C1 == 0x40)
            {
                var_1 = (sbyte)((ovr024.roll_dice(3, 1) * 2) + 1);
            }
            else
            {
                var_1 = (sbyte)ovr025.sub_6886F(gbl.byte_1D2C1);
            }

            if (gbl.area_ptr.field_1CC == 0)
            {

                ovr032.sub_738D8(gbl.mapToBackGroundTile, 1, 0xff, 2, gbl.byte_1D884, gbl.byte_1D883);
                var_3 = gbl.byte_1D1C0;

                for (var_2 = 0; var_2 < var_3; var_2++)
                {
                    gbl.sp_target[var_2 + 1] = gbl.player_array[gbl.unk_1D1C1[var_2].field_0];
                }

                gbl.byte_1D75E = gbl.byte_1D1C0;
            }

            ovr033.sub_749DD(8, 0, gbl.byte_1D884, gbl.byte_1D883);

            sub_5CF7F(string.Empty, 9, ovr024.roll_dice_save(6, var_1), false, 0, gbl.byte_1D2C1);

        }


        internal static void sub_5F87B(string arg_0, sbyte arg_4, Affects arg_6)
        {
            byte var_2D;
            byte var_2B;
            byte var_2A;
            string var_29;

            var_29 = arg_0;
            gbl.byte_1D2C7 = 1;

            var_2A = ovr025.sub_6886F(gbl.byte_1D2C1);
            var_2D = gbl.byte_1D75E;

            for (var_2B = 1; var_2B < var_2D; var_2B++)
            {
                if (gbl.sp_target[var_2B].combat_team == arg_4 &&
                    var_2A > 0)
                {
                    var_2A -= 1;

                    if (ovr024.is_cured(arg_6, gbl.sp_target[var_2B]) == true)
                    {
                        gbl.sp_target[var_2B] = null;
                    }
                }
                else
                {
                    gbl.sp_target[var_2B] = null;
                }
            }

            sub_5CF7F(var_29, 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void cast_haste()
        {
            sub_5F87B("is Hasted", gbl.player_ptr.combat_team, Affects.slow);
        }


        internal static void sub_5F986(ref bool arg_0, byte arg_4, byte arg_6, byte arg_8, int arg_A, int arg_C)
        {
            byte var_6;
            byte var_5;
            Player var_4;

            ovr033.AtMapXY(out var_6, out var_5, arg_A, arg_C);

            if (var_6 > 0 &&
                gbl.BackGroundTiles[var_6].move_cost == 0xff &&
                gbl.area_ptr.field_1CC == 1 &&
                arg_0 == false)
            {
                arg_0 = true;
            }
            else
            {
                arg_0 = false;
            }

            if (var_5 > 0 &&
                var_5 != arg_4)
            {

                var_4 = gbl.player_array[var_5];
                gbl.byte_1D2BF = 0x0C;

                ovr024.damage_person(ovr024.do_saving_throw(0, arg_6, var_4), 2, (sbyte)arg_8, var_4);
                ovr025.sub_67A59(0x13);
                gbl.byte_1D2BF = 0;
            }
        }


        internal static void sub_5FA44(byte arg_0, byte arg_2, byte arg_4, byte arg_6)
        {
            byte var_3D;
            byte var_3C;
            byte var_3B;
            byte var_3A = 0; /* Simeon */
            byte var_39;
            byte var_38;
            bool var_37;
            bool var_36;
            sbyte var_35;
            sbyte var_34;
            sbyte var_33;
            Struct_XXXX var_30;
            Struct_XXXX var_18;

            var_36 = false;
            ovr025.sub_67A59(0x13);

            ovr033.AtMapXY(out var_3B, out var_39, gbl.byte_1D884, gbl.byte_1D883);
            var_3D = 0;
            var_35 = 1;

            int var_31 = ovr033.PlayerMapXPos(gbl.player_ptr);
            int var_32 = ovr033.PlayerMapYPos(gbl.player_ptr);
            var_38 = arg_0;

            if (var_31 != gbl.byte_1D883 ||
                var_32 != gbl.byte_1D884)
            {
                var_3C = (byte)(arg_6 * 2);
                gbl.byte_1D2C7 = 1;

                while (var_3C > 0)
                {

                    var_18 = new Struct_XXXX();

                    var_18.field_00 = gbl.byte_1D883;
                    var_18.field_02 = gbl.byte_1D884;
                    var_18.field_04 = (short)(gbl.byte_1D883 + ((gbl.byte_1D883 - var_31) * var_35 * var_3C));
                    var_18.field_06 = (short)(gbl.byte_1D884 + ((gbl.byte_1D884 - var_32) * var_35 * var_3C));

                    ovr032.sub_731A5(var_18);

                    do
                    {
                        var_33 = (sbyte)var_18.field_0E;
                        var_34 = (sbyte)var_18.field_10;

                        if (var_18.field_00 != var_18.field_04 ||
                            var_18.field_02 != var_18.field_06)
                        {
                            do
                            {

                                var_37 = ovr032.sub_7324C(var_18);

                                ovr033.AtMapXY(out var_3B, out var_3A, var_18.field_10, var_18.field_0E);

                                if (gbl.BackGroundTiles[var_3B].move_cost == 1)
                                {
                                    var_36 = false;
                                }

                            } while (var_37 == true && (var_3A <= 0 || var_3A == var_39) && var_3B != 0 &&
                            gbl.BackGroundTiles[var_3B].move_cost <= 1 && var_18.field_16 < var_3C);
                        }

                        if (var_3B == 0)
                        {
                            var_3C = 0;
                        }

                        ovr025.sub_67AA4(0x32, 4, var_18.field_10, var_18.field_0E, var_34, var_33);

                        sub_5F986(ref var_36, var_39, arg_2, arg_4, (sbyte)var_18.field_10, (sbyte)var_18.field_0E);
                        var_39 = var_3A;

                        if (var_36 == true)
                        {
                            gbl.byte_1D883 = (sbyte)var_18.field_0E;
                            gbl.byte_1D884 = (sbyte)var_18.field_10;

                            var_30 = new Struct_XXXX();

                            var_30.field_00 = gbl.byte_1D883;
                            var_30.field_02 = gbl.byte_1D884;
                            var_30.field_06 = var_31;
                            var_30.field_04 = var_32;

                            ovr032.sub_731A5(var_30);

                            while (ovr032.sub_7324C(var_30) == true)
                            {
                                /* empty */
                            }

                            throw new System.NotSupportedException();//cmp	[bp+var_38], 0
                            throw new System.NotSupportedException();//jz	loc_5FC7C
                            throw new System.NotSupportedException();//cmp	var_30.field_16, 8
                            throw new System.NotSupportedException();//ja	loc_5FC7C
                            var_18.field_16 += 8;
                            throw new System.NotSupportedException();//loc_5FC7C:
                            throw new System.NotSupportedException();//mov	al, [bp+var_35]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//xor	ax, ax
                            throw new System.NotSupportedException();//sub	ax, dx
                            throw new System.NotSupportedException();//mov	[bp+var_35], al
                            var_38 = 0;
                            var_39 = 0;
                        }

                        var_3D = (byte)(var_18.field_16 - var_3D);

                        if (var_3D < var_3C)
                        {
                            var_3C -= var_3D;
                        }
                        else
                        {
                            var_3C = 0;
                        }

                        var_3D = var_18.field_16;
                    } while (var_36 == false && var_3C != 0);
                }

                gbl.byte_1D2C7 = 0;
            }
        }


        internal static void sub_5FCD9()
        {
            bool var_2 = false; /* Simeon */
            byte var_1;

            var_1 = ovr024.roll_dice(6, ovr025.sub_6886F(gbl.byte_1D2C1));

            sub_5F986(ref var_2, 0, 4, var_1, gbl.byte_1D884, gbl.byte_1D883);
            sub_5FA44(1, 4, var_1, 7);

        }


        internal static void sub_5FD2E()
        {
            sub_5F87B("is Slowed", ovr025.on_our_team(gbl.player_ptr), Affects.haste);
        }


        internal static void cast_restore()
        {
            byte var_D;
            byte var_C = 30; /* simeon */
            int var_B;
            byte var_7;
            byte var_6;
            byte var_5;
            Player player;

            player = gbl.sp_target[1];

            if (player.field_E7 > 0)
            {
                var_5 = (byte)(player.field_E8 / player.field_E7);

                player.hit_point_max += var_5;
                player.hit_point_current += var_5;
                player.field_12C += var_5;
                player.field_E8 -= var_5;
                player.field_E7--;

                var_6 = 13;
                var_B = 0x00989680;

                for (var_D = 0; var_D <= 7; var_D++)
                {
                    var_7 = player.Skill_A_lvl[var_D];

                    if (var_7 > 0 &&
                        var_7 <= var_6)
                    {
                        throw new System.NotSupportedException();//mov	al, [bp+var_7]
                        throw new System.NotSupportedException();//inc	al
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//mov	cx, ax
                        throw new System.NotSupportedException();//mov	al, [bp+var_D]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, 0x63
                        throw new System.NotSupportedException();//mul	dx
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//add	di, cx
                        throw new System.NotSupportedException();//cmp	short ptr unk_1A5A5[di],	0
                        throw new System.NotSupportedException();//jg	loc_5FE3D
                        throw new System.NotSupportedException();//jl	loc_5FEB0
                        throw new System.NotSupportedException();//cmp	short ptr unk_1A5A3[di],	0
                        throw new System.NotSupportedException();//jbe	loc_5FEB0
                        throw new System.NotSupportedException();//loc_5FE3D:
                        throw new System.NotSupportedException();//mov	al, [bp+var_7]
                        throw new System.NotSupportedException();//inc	al
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//mov	cx, ax
                        throw new System.NotSupportedException();//mov	al, [bp+var_D]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, 0x63
                        throw new System.NotSupportedException();//mul	dx
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//add	di, cx
                        throw new System.NotSupportedException();//mov	ax, short ptr unk_1A5A3[di]
                        throw new System.NotSupportedException();//mov	dx, short ptr unk_1A5A5[di]
                        throw new System.NotSupportedException();//cmp	dx, [bp+var_9]
                        throw new System.NotSupportedException();//jl	loc_5FE6A
                        throw new System.NotSupportedException();//jg	loc_5FEB0
                        throw new System.NotSupportedException();//cmp	ax, [bp+var_B]
                        throw new System.NotSupportedException();//jnb	loc_5FEB0
                        throw new System.NotSupportedException();//loc_5FE6A:
                        throw new System.NotSupportedException();//push	[bp+player.seg]
                        throw new System.NotSupportedException();//push	[bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, [bp+var_D]
                        throw new System.NotSupportedException();//push	ax
                        throw new System.NotSupportedException();//call	sub_69138
                        throw new System.NotSupportedException();//or	al, al
                        throw new System.NotSupportedException();//jnz	loc_5FEB0
                        var_6 = var_7;
                        var_C = var_D;
                        throw new System.NotSupportedException();//mov	al, [bp+var_7]
                        throw new System.NotSupportedException();//inc	al
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//mov	cx, ax
                        throw new System.NotSupportedException();//mov	al, [bp+var_D]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, 0x63
                        throw new System.NotSupportedException();//mul	dx
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//add	di, cx
                        throw new System.NotSupportedException();//mov	ax, short ptr unk_1A5A3[di]
                        throw new System.NotSupportedException();//mov	dx, short ptr unk_1A5A5[di]
                        throw new System.NotSupportedException();//mov	[bp+var_B], ax
                        throw new System.NotSupportedException();//mov	[bp+var_9], dx
                    }
                    throw new System.NotSupportedException();//loc_5FEB0:
                }

                player.Skill_A_lvl[var_C]++;

                if (player.exp < var_B)
                {
                    player.exp = var_B;
                }

                ovr026.sub_6A3C6(player);
                ovr025.DisplayPlayerStatusString(true, 10, "is restored", player);
            }
        }


        internal static void cast_speed()
        {
            if (ovr024.is_cured(Affects.slow, gbl.sp_target[1]) == false)
            {
                sub_5CF7F("is Speedy", 0, 0, false, 0, gbl.byte_1D2C1);
            }
        }


        internal static void sub_5FF6D()
        {
            if (gbl.byte_1D75E != 0 &&
                ovr024.heal_player(0, (byte)(ovr024.roll_dice(8, 2) + 1), gbl.sp_target[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_target[1]);
            }
        }


        internal static void cast_strength()
        {
            byte var_1 = 0;

            if (ovr024.sub_64728(out var_1, 0, 0x15, gbl.sp_target[1]) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is stronger", gbl.sp_target[1]);
            }

            ovr024.add_affect(true, var_1, (ushort)((ovr024.roll_dice(4, 1) * 10) + 0x28), Affects.affect_92, gbl.sp_target[1]);
            ovr024.sub_648D9(0, gbl.sp_target[1]);
        }


        internal static void sub_6003C()
        {
            bool var_1 = false;

            sub_5F986(ref var_1, 0, 4, (byte)(ovr024.roll_dice(6, 1) + 20), gbl.byte_1D884, gbl.byte_1D883);
            sub_5FA44(0, 4, 20, 3);
        }


        internal static void cast_paralyzed()
        {
            sub_5CF7F("is paralyzed", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void cast_heal()
        {
            if (ovr024.heal_player(0, (byte)(ovr024.roll_dice(4, 2) + 2), gbl.sp_target[1]) == true)
            {
                ovr025.sub_6818A("is Healed", 1, gbl.sp_target[1]);
            }
        }


        internal static void cast_invisible()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_6014A()
        {
            sub_5CF7F(string.Empty, 8, (sbyte)(ovr024.roll_dice_save(4, 2) + 2), false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_60185()
        {
            sub_5CF7F(string.Empty, 8, (sbyte)(ovr024.roll_dice_save(8, 2) + 1), false, 0, gbl.byte_1D2C1);
        }


        internal static void cure_poison()
        {
            Affect var_8;
            Player var_4;

            var_4 = gbl.sp_target[1];

            if (var_4.health_status == Status.animated)
            {
                gbl.sp_target[1] = null;
            }
            else if (ovr025.find_affect(out var_8, Affects.poisoned, var_4) == true)
            {
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+1A4h], 0
                throw new System.NotSupportedException();//jnz	loc_60231
                throw new System.NotSupportedException();//les	di, [bp+var_4]
                throw new System.NotSupportedException();//mov	byte ptr es:[di+1A4h], 1
                throw new System.NotSupportedException();//loc_60231:
                gbl.byte_1D2C6 = 1;

                ovr024.remove_affect(null, Affects.poisoned, var_4);
                ovr024.remove_affect(null, Affects.slow_poison, var_4);
                ovr024.remove_affect(null, Affects.affect_0f, var_4);

                gbl.byte_1D2C6 = 0;
                ovr025.DisplayPlayerStatusString(true, 10, "is unpoisoned", var_4);

                var_4.in_combat = true;
                var_4.health_status = 0;
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", var_4);
            }
        }


        internal static void sub_602D0()
        {
            Player var_6;
            Affect var_2 = null; /* Simeon */

            sub_5CF7F(string.Empty, 8, 0, false, 0, gbl.byte_1D2C1);
            var_6 = gbl.player_ptr.actions.target;
            gbl.byte_1D2BD = 0x40;
            ovr024.work_on_00(var_6, 9);

            if (gbl.byte_1D2BD == 0x40)
            {
                ovr024.sub_630C7(0, var_2, gbl.player_ptr, Affects.affect_40);
            }
        }


        internal static void cast_flattern()
        {
            Affect var_4;

            if (gbl.sp_target[1].field_E5 < 6)
            {
                sub_5CF7F(string.Empty, 8, 0, false, ovr025.sub_6886F(gbl.byte_1D2C1), gbl.byte_1D2C1);

                if (ovr025.find_affect(out var_4, Affects.affect_03, gbl.sp_target[1]) == true)
                {
                    ovr024.sub_630C7(0, var_4, gbl.sp_target[1], Affects.affect_03);
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "smashes them flat", gbl.sp_target[1]);
            }
        }


        internal static void sub_603F0()
        {
            if (gbl.byte_1D75E != 0 &&
                ovr024.heal_player(0, (byte)(ovr024.roll_dice(8, 3) + 3), gbl.sp_target[1]) == true)
            {
                ovr025.describeHealing(gbl.sp_target[1]);
            }
        }


        internal static void sub_60431()
        {
            sub_5CF7F(string.Empty, 8, (sbyte)(ovr024.roll_dice_save(8, 3) + 3), false, 0, gbl.byte_1D2C1);
        }


        internal static void is_affected4()
        {
            ovr024.is_unaffected(string.Empty, false, 0, false, 0, sub_5CE92(0x49), Affects.dispel_evil, gbl.player_ptr);
            sub_5CF7F("is affected", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_604DA()
        {
            sub_5CF7F(string.Empty, 9, ovr024.roll_dice_save(8, 6), false, 0, gbl.byte_1D2C1);
        }


        internal static void cast_raise()
        {
            Player var_4;

            var_4 = gbl.sp_target[1];
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+195h], 6
            throw new System.NotSupportedException();//jz	loc_60546
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+195h], 1
            throw new System.NotSupportedException();//jz	loc_60546
            throw new System.NotSupportedException();//jmp	loc_605E2
            throw new System.NotSupportedException();//loc_60546:
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+18h], 0
            throw new System.NotSupportedException();//ja	loc_60553
            throw new System.NotSupportedException();//jmp	loc_605E2
            throw new System.NotSupportedException();//loc_60553:
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+74h], 2
            throw new System.NotSupportedException();//jnz	loc_60560
            throw new System.NotSupportedException();//jmp	loc_605E2
            throw new System.NotSupportedException();//loc_60560:
            gbl.byte_1D2C6 = 1;

            ovr024.remove_affect(null, Affects.funky__32, var_4);
            ovr024.remove_affect(null, Affects.poisoned, var_4);
            gbl.byte_1D2C6 = 0;

            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//mov	byte ptr es:[di+195h], 0
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//mov	byte ptr es:[di+196h], 1
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//dec	byte ptr es:[di+18h]

            ovr024.sub_648D9(4, var_4);
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//mov	byte ptr es:[di+1A4h], 1

            ovr025.DisplayPlayerStatusString(true, 10, "is raised", var_4);
            throw new System.NotSupportedException();//loc_605E2:
        }


        internal static void cast_slay()
        {
            Player var_2;

            var_2 = gbl.sp_target[1];
            gbl.byte_1D2BF = 0x40;
            gbl.byte_1D2BE = 0x43;
            ovr024.work_on_00(var_2, 9);

            if (gbl.byte_1D2BE != 0)
            {

                if (ovr024.do_saving_throw(0, 4, var_2) == false)
                {
                    ovr024.sub_63014("is slain", Status.dead, var_2);
                }
                else
                {
                    gbl.byte_1D2BF = 8;

                    ovr024.damage_person(false, 0, (sbyte)(ovr024.roll_dice_save(8, 2) + 1), var_2);
                }
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", var_2);
            }
        }


        internal static void cast_entangle()
        {
            bool var_6;
            byte var_5;
            Player var_4;

            if (gbl.area_ptr.field_1CC == 0)
            {
                for (var_5 = 1; var_5 <= gbl.byte_1D75E; var_5++)
                {
                    if (gbl.sp_target[var_5] != null)
                    {
                        var_4 = gbl.sp_target[var_5];

                        var_6 = ovr024.do_saving_throw(0, 4, var_4);

                        ovr024.is_unaffected("is entangled", var_6, 1, false, 0, sub_5CE92(0x88), Affects.affect_88, var_4);
                    }
                }
            }
        }


        internal static void cast_highlisht()
        {
            sub_5DB24("is highlighted", 0);
        }


        internal static void cast_invisible2()
        {
            sub_5CF7F("is invisible", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void cast_charmed()
        {
            Affect var_7;
            byte var_1;

            sub_5DB24("is charmed", 0);

            for (var_1 = 1; var_1 <= gbl.byte_1D75E; var_1++)
            {
                if (ovr025.find_affect(out var_7, Affects.charm_person, gbl.sp_target[var_1]) == true)
                {
                    ovr024.sub_630C7(0, var_7, gbl.sp_target[var_1], Affects.charm_person);
                }
            }
        }


        internal static void cast_confuse()
        {
            byte var_B;
            Player var_6;
            bool var_2;
            byte var_1;

            var_B = ovr024.roll_dice(8, 2);

            if (gbl.byte_1D75E > var_B)
            {
                gbl.byte_1D75E = var_B;
            }

            for (var_1 = 1; var_1 <= gbl.byte_1D75E; var_1++)
            {
                if (gbl.sp_target[var_1] != null)
                {

                    var_6 = gbl.sp_target[var_1];

                    var_2 = ovr024.do_saving_throw(0, 6, var_6);

                    ovr024.is_unaffected("is confused", var_2, 1, false, 0, sub_5CE92(0x52), Affects.cause_disease_2, var_6);
                }
            }
        }


        internal static void cast_teleport()
        {
            byte var_F;
            byte var_E;
            Affect var_D;
            Player var_8;
            Player var_4;

            var_4 = gbl.player_ptr;

            if (ovr025.find_affect(out var_D, Affects.affect_3a, var_4) == true)
            {
                ovr032.sub_738D8(gbl.mapToBackGroundTile, 1, 0xff, 1, ovr033.PlayerMapYPos(var_4), ovr033.PlayerMapXPos(var_4));
                var_F = gbl.byte_1D1C0;

                for (var_E = 0; var_E < var_F; var_E++)
                {
                    var_8 = gbl.player_array[gbl.unk_1D1C1[var_E].field_0];

                    if (ovr025.find_affect(out var_D, Affects.affect_90, var_8) == true ||
                        ovr025.find_affect(out var_D, Affects.affect_8b, var_8) == true)
                    {
                        if (gbl.player_array[var_D.field_3] == var_4)
                        {
                            ovr024.remove_affect(null, Affects.affect_90, var_8);
                            ovr024.remove_affect(null, Affects.affect_8b, var_8);
                        }
                    }
                }
            }

            ovr033.sub_74572(ovr033.get_player_index(var_4), 0, 0);

            ovr033.sub_7515A(0, gbl.byte_1D884, gbl.byte_1D883, var_4);

            ovr033.sub_749DD(8, 0, ovr033.PlayerMapYPos(var_4), ovr033.PlayerMapXPos(var_4));

            ovr025.DisplayPlayerStatusString(true, 10, "teleports", var_4);
        }


        internal static void cast_terror()
        {
            byte var_B;
            bool var_A;
            byte var_9;
            Player var_8;
            Player var_4;

            var_4 = gbl.player_ptr;

            sub_5D7CF(6, 3, gbl.byte_1D884, gbl.byte_1D883, ovr033.PlayerMapYPos(var_4), ovr033.PlayerMapXPos(var_4));
            var_B = gbl.byte_1D75E;

            for (var_9 = 1; var_9 < var_B; var_9++)
            {
                var_8 = gbl.sp_target[var_9];

                var_A = ovr024.do_saving_throw(0, 4, var_8);

                if (var_A == false)
                {
                    ovr024.is_unaffected("runs in terror", var_A, 1, true, 0, sub_5CE92(0x54), Affects.affect_8e, var_8);
                    var_8.actions.field_10 = 1;
                    var_8.field_198 = 1;

                    if (var_8.field_F7 <= 0x7F)
                    {
                        var_8.field_F7 = 0xB3;
                    }

                    var_8.actions.target = null;
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", var_8);
                }
            }
        }


        internal static void cast_protection()
        {
            bool var_3;
            bool var_2;
            char var_1;

            var_3 = false;

            do
            {
                if (gbl.player_ptr.field_198 != 0)
                {
                    if (ovr024.roll_dice(10, 1) > 5)
                    {
                        var_1 = 'H';
                    }
                    else
                    {
                        var_1 = 'C';
                    }
                }
                else
                {
                    var_1 = ovr027.displayInput(out var_2, false, 0, 15, 10, 13, "Hot Cold", "flame type: ");
                }

                if (var_1 == 'H')
                {
                    ovr024.is_unaffected("is protected", false, 0, false, 0, sub_5CE92(0x55), Affects.hot_fire_shield, gbl.player_ptr);
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, sub_5CE92(0x55), Affects.affect_87, gbl.player_ptr);
                    var_3 = true;
                }
                else if (var_1 == 'C')
                {
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, sub_5CE92(0x55), Affects.cold_fire_shield, gbl.player_ptr);
                    ovr024.is_unaffected(string.Empty, false, 0, false, 0, sub_5CE92(0x55), Affects.affect_87, gbl.player_ptr);
                    var_3 = true;
                }
                else
                {
                    var_1 = ovr027.displayInput(out var_2, false, 0, 15, 10, 13, "Yes No", "Abort spell? ");

                    if (var_1 == 'Y')
                    {
                        var_3 = true;
                    }
                }

            } while (var_3 == false);
        }


        internal static void spell_slow()
        {
            Affect var_8;
            Player var_4;

            var_4 = gbl.sp_target[1];
            gbl.byte_1D2BF = 0x40;

            if (ovr024.do_saving_throw(0, 4, var_4) == false)
            {
                ovr024.is_unaffected("is clumsy", false, 0, false, 0, sub_5CE92(0x56), Affects.fumbling, var_4);

                if (ovr025.find_affect(out var_8, Affects.fumbling, var_4) == true)
                {
                    ovr024.sub_630C7(0, null, var_4, Affects.fumbling);
                }
            }
            else
            {
                ovr024.is_unaffected("is slowed", false, 0, false, 0, sub_5CE92(0x56), Affects.slow, var_4);

                if (ovr025.find_affect(out var_8, Affects.slow, var_4) == true)
                {
                    ovr024.sub_630C7(0, null, var_4, Affects.slow);
                }
            }
            sub_5CF7F("is clumsy", 0, 0, true, 0, gbl.byte_1D2C1);
        }


        internal static void sub_60F0B()
        {
            sub_5CF7F(string.Empty, 10, ovr024.roll_dice_save(10, 3), false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_60F4E()
        {
            sub_5CF7F("is protected", 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void spell_poisonous_cloud()
        {
            byte var_18;
            byte var_17 = 0; /* Simeon */
            byte var_16;
            byte var_15;
            byte var_14 = 0; /* Simeon */
            byte var_13;
            byte var_12;
            byte[] var_11 = new byte[10];
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            gbl.byte_1D2C7 = 1;

            var_15 = ovr025.sub_6886F(gbl.byte_1D2C1);
            var_13 = 0;
            var_8 = gbl.stru_1D889;

            if (var_8 == null)
            {
                gbl.stru_1D889 = new Struct_1D885();
                var_8 = gbl.stru_1D889;
            }
            else
            {
                while (var_8.next != null)
                {
                    if (var_8.player == gbl.player_ptr)
                    {
                        var_13++;
                    }
                    var_8 = var_8.next;
                }

                var_8.next = new Struct_1D885();
                var_8 = var_8.next;
            }

            ovr024.add_affect(true, (byte)(var_15 + (var_13 << 4)), var_15, Affects.affect_5b, gbl.player_ptr);

            var_8.player = gbl.player_ptr;
            var_8.field_1C = var_13;
            var_8.field_1A = (sbyte)gbl.byte_1D883;
            var_8.field_1B = (sbyte)gbl.byte_1D884;

            for (var_16 = 1; var_16 <= 9; var_16++)
            {
                var_17 = gbl.unk_18AED[var_16];

                ovr033.AtMapXY(out var_14, out var_11[var_16],
                    gbl.byte_1D884 + gbl.MapDirectionYDelta[var_17],
                    gbl.byte_1D883 + gbl.MapDirectionXDelta[var_17]);

                if (var_14 > 0 &&
                    gbl.BackGroundTiles[var_14].move_cost < 0xff)
                {
                    var_8.field_10[var_16] = 1;
                }
                else
                {
                    var_8.field_10[var_16] = 0;
                }

                if (var_14 == 0x1E)
                {
                    var_4 = gbl.stru_1D885;
                    var_18 = 0;

                    while (var_4 != null && var_18 == 0)
                    {
                        for (var_12 = 1; var_12 <= 4; var_12++)
                        {
                            throw new System.NotSupportedException();//cmp	var_4.field_10[var_12], 0
                            throw new System.NotSupportedException();//jnz	loc_6118A
                            throw new System.NotSupportedException();//jmp	loc_61235
                            throw new System.NotSupportedException();//loc_6118A:
                            throw new System.NotSupportedException();//mov	al, [bp+var_17]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.MapDirectionXDelta[di]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//mov	al, byte_1D883
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//add	ax, dx
                            throw new System.NotSupportedException();//mov	cx, ax
                            throw new System.NotSupportedException();//mov	al, [bp+var_12]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.unk_18AE9[di]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.MapDirectionXDelta[di]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//les	di, [bp+var_4]
                            throw new System.NotSupportedException();//mov	al, es:[di+1Ah]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//add	ax, dx
                            throw new System.NotSupportedException();//cmp	ax, cx
                            throw new System.NotSupportedException();//jnz	loc_61235
                            throw new System.NotSupportedException();//mov	al, [bp+var_17]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//mov	al, byte_1D884
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//add	ax, dx
                            throw new System.NotSupportedException();//mov	cx, ax
                            throw new System.NotSupportedException();//mov	al, [bp+var_12]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.unk_18AE9[di]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//mov	dx, ax
                            throw new System.NotSupportedException();//les	di, [bp+var_4]
                            throw new System.NotSupportedException();//mov	al, es:[di+1Bh]
                            throw new System.NotSupportedException();//cbw
                            throw new System.NotSupportedException();//add	ax, dx
                            throw new System.NotSupportedException();//cmp	ax, cx
                            throw new System.NotSupportedException();//jnz	loc_61235
                            throw new System.NotSupportedException();//cmp	var_4.field_7[var_12], 0x1E
                            throw new System.NotSupportedException();//jz	loc_61235
                            throw new System.NotSupportedException();//cmp	var_4.field_7[var_12], 0x1C
                            throw new System.NotSupportedException();//jz	loc_61235

                            var_14 = var_4.field_7[var_12];
                            var_18 = 1;
                            throw new System.NotSupportedException();//loc_61235:
                        }

                        var_4 = var_4.next;
                    }
                }
                else if (var_14 == 0x1C)
                {
                    var_4 = gbl.stru_1D889;
                    var_18 = 0;

                    while (var_4 != null && var_18 == 0)
                    {
                        if (var_4 != var_8)
                        {
                            for (var_12 = 1; var_12 <= 9; var_12++)
                            {
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//les	di, [bp+var_4]
                                throw new System.NotSupportedException();//add	di, ax
                                throw new System.NotSupportedException();//cmp	byte ptr es:[di+10h], 0
                                throw new System.NotSupportedException();//jnz	loc_612B1
                                throw new System.NotSupportedException();//jmp	loc_6135C
                                throw new System.NotSupportedException();//loc_612B1:
                                throw new System.NotSupportedException();//mov	al, [bp+var_17]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.MapDirectionXDelta[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//mov	al, byte_1D883
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//mov	cx, ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.unk_18AED[di]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.MapDirectionXDelta[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//les	di, [bp+var_4]
                                throw new System.NotSupportedException();//mov	al, es:[di+1Ah]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//cmp	ax, cx
                                throw new System.NotSupportedException();//jnz	loc_6135C
                                throw new System.NotSupportedException();//mov	al, [bp+var_17]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//mov	al, byte_1D884
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//mov	cx, ax
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.unk_18AED[di]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//mov	dx, ax
                                throw new System.NotSupportedException();//les	di, [bp+var_4]
                                throw new System.NotSupportedException();//mov	al, es:[di+1Bh]
                                throw new System.NotSupportedException();//cbw
                                throw new System.NotSupportedException();//add	ax, dx
                                throw new System.NotSupportedException();//cmp	ax, cx
                                throw new System.NotSupportedException();//jnz	loc_6135C
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//les	di, [bp+var_4]
                                throw new System.NotSupportedException();//add	di, ax
                                throw new System.NotSupportedException();//cmp	byte ptr es:[di+7], 0x1E
                                throw new System.NotSupportedException();//jz	loc_6135C
                                throw new System.NotSupportedException();//mov	al, [bp+var_12]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//les	di, [bp+var_4]
                                throw new System.NotSupportedException();//add	di, ax
                                throw new System.NotSupportedException();//cmp	byte ptr es:[di+7], 0x1C
                                throw new System.NotSupportedException();//jz	loc_6135C

                                var_14 = var_4.field_7[var_12];
                                var_18 = 1;
                                throw new System.NotSupportedException();//loc_6135C:
                            }
                        }

                        var_4 = var_4.next;
                    }
                }
                else if (var_14 == 0x1F)
                {
                    for (var_12 = 1; var_12 <= gbl.byte_1D1BB; var_12++)
                    {
                        throw new System.NotSupportedException();//mov	al, [bp+var_17]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	al, gbl.MapDirectionXDelta[di]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//mov	al, byte_1D883
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//add	ax, dx
                        throw new System.NotSupportedException();//mov	cx, ax
                        throw new System.NotSupportedException();//mov	al, gbl.unk_1D183[ var_12 ].field_4
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//cmp	ax, cx
                        throw new System.NotSupportedException();//jnz	loc_61401
                        throw new System.NotSupportedException();//mov	al, [bp+var_17]
                        throw new System.NotSupportedException();//xor	ah, ah
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	al, gbl.MapDirectionYDelta[di]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//mov	al, byte_1D884
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//add	ax, dx
                        throw new System.NotSupportedException();//mov	cx, ax
                        throw new System.NotSupportedException();//mov	al, gbl.unk_1D183[ var_12 ].field_5
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//cmp	ax, cx
                        throw new System.NotSupportedException();//jnz	loc_61401
                        var_14 = gbl.unk_1D183[var_12].field_6;
                        throw new System.NotSupportedException();//loc_61401:
                    }
                }

                var_8.field_7[var_16] = var_14;

                if (var_8.field_10[var_16] != 0)
                {
                    int cx = gbl.MapDirectionXDelta[var_17] + gbl.byte_1D883;
                    int ax = gbl.MapDirectionYDelta[var_17] + gbl.byte_1D884;

                    gbl.mapToBackGroundTile[cx, ax] = 0x1C;
                }
            }

            var_8.field_7[var_16] = var_14;

            if (var_8.field_10[var_16] != 0)
            {
                int cx = gbl.MapDirectionXDelta[var_17] + gbl.byte_1D883;
                int ax = gbl.MapDirectionYDelta[var_17] + gbl.byte_1D884;

                gbl.mapToBackGroundTile[cx, ax] = 0x1C;
            }

            ovr025.DisplayPlayerStatusString(false, 10, "Creates a poisonous cloud", gbl.player_ptr);

            ovr033.sub_749DD(8, 0xFF, gbl.byte_1D884, gbl.byte_1D883);
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
            sbyte var_6;
            byte var_5;
            Player var_4;

            var_4 = gbl.player_ptr;

            var_6 = (sbyte)ovr025.sub_6886F(gbl.byte_1D2C1);

            var_5 = (byte)((ovr025.sub_6886F(gbl.byte_1D2C1) + 1) / 2);

            if (var_5 < 1)
            {
                var_5 = 1;
            }

            sub_5D7CF(var_5, 2, gbl.byte_1D884, gbl.byte_1D883, ovr033.PlayerMapYPos(var_4), ovr033.PlayerMapXPos(var_4));

            sub_5CF7F(string.Empty, 10, (sbyte)(var_6 + ovr024.roll_dice_save(4, var_6)), false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_615F2()
        {
            Affect var_9;
            Player var_5;
            byte var_1;

            var_5 = gbl.sp_target[1];

            var_1 = var_5.field_E3;

            if (var_5._class == ClassId.cleric)
            {
                var_5.field_E3 -= 1;
            }
            else if (var_5._class == ClassId.magic_user)
            {
                var_5.field_E3 += 4;
            }
            else
            {
                var_5.field_E3 += 2;
            }

            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.byte_1D2C1);

            if (ovr025.find_affect(out var_9, Affects.feeble, var_5) == true)
            {
                ovr024.sub_630C7(0, null, var_5, Affects.feeble);
            }

            var_5.field_E3 = var_1;
        }


        internal static void sub_6169E()
        {
            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_616CC()
        {
            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_616FA()
        {
            sub_5CF7F(string.Empty, 0, 0, false, 0, gbl.byte_1D2C1);
        }


        internal static void sub_61727()
        {
            byte var_B;
            bool var_A;
            Player var_9;
            Player player;
            byte var_1;

            player = gbl.player_ptr;

            sub_5D7CF(3, 1, gbl.byte_1D884, gbl.byte_1D883, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));
            var_B = gbl.byte_1D75E;

            for (var_1 = 1; var_1 <= var_B; var_1++)
            {
                if (gbl.sp_target[var_1] != null)
                {

                    var_9 = gbl.sp_target[var_1];

                    if (var_9.field_11A == 18)
                    {
                        var_A = false;
                    }
                    else
                    {
                        var_A = true;
                    }

                    ovr024.damage_person(var_A, gbl.byte_1A114, ovr024.roll_dice_save(6, 6), var_9);
                }
            }
        }


        internal static void cast_heal2()
        {
            if (ovr024.heal_player(0, (byte)(ovr024.roll_dice(4, 2) + 2), gbl.sp_target[1]) == true)
            {
                ovr025.sub_6818A("is Healed", 1, gbl.sp_target[1]);
            }
        }


        internal static void spell_stone(byte arg_0, object param, Player player)
        {
            Affect var_9;
            byte var_5;
            Item item;

            player.actions.target = null;

            gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x41);

            if (player.actions.target != null)
            {
                gbl.spell_target = player.actions.target;

                ovr025.DisplayPlayerStatusString(false, 10, "gazes...", player);
                ovr025.sub_67A59(0x12);

                ovr025.sub_67AA4(0x2d, 4, ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                    ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                if (ovr025.find_affect(out var_9, Affects.affect_7f, player) == true)
                {
                    var_5 = 0;
                    item = gbl.spell_target.itemsPtr;

                    while (item != null && var_5 == 0)
                    {
                        if (item.readied)
                        {
                            if (item.field_2F == 0x76 ||
                                item.field_30 == 0x76 ||
                                item.field_31 == 0x76)
                            {
                                ovr025.DisplayPlayerStatusString(false, 12, "reflects it!", gbl.spell_target);

                                ovr025.sub_67AA4(0x2d, 4, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player),
                                    ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target));
                                var_5 = 1;
                                gbl.spell_target = player;
                            }
                        }

                        item = item.next;
                    }
                }

                if (ovr024.do_saving_throw(0, 1, gbl.spell_target) == false)
                {
                    ovr024.sub_63014("is Stoned", Status.stoned, gbl.spell_target);
                }
            }
        }


        internal static void cast_breath(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;
            bool var_1 = false; /* Simeon */

            if (gbl.byte_1D8B7 == 0 ||
                ovr024.roll_dice(100, 1) > 50)
            {
                gbl.byte_1D2BF = 0x24;
                int var_2 = ovr033.PlayerMapXPos(player);
                int var_3 = ovr033.PlayerMapYPos(player);

                ovr025.DisplayPlayerStatusString(true, 10, "Breathes!", player);

                gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x33);

                gbl.byte_1D883 = (sbyte)(var_2 + ovr032.sub_73005(gbl.byte_1D883 - var_2));
                gbl.byte_1D884 = var_3 + ovr032.sub_73005(gbl.byte_1D884 - var_3);

                if (gbl.byte_1D883 == (var_2 + 1))
                {
                    gbl.byte_1D883++;
                }

                if (gbl.byte_1D884 == (var_3 + 1))
                {
                    gbl.byte_1D884++;
                }

                ovr024.remove_affect_19(player);
                ovr025.sub_67A59(0x13);

                ovr025.sub_67AA4(0x32, 4, gbl.byte_1D884, gbl.byte_1D883, var_3, var_2);
                sub_5F986(ref var_1, 0, 3, player.hit_point_max, gbl.byte_1D884, gbl.byte_1D883);
                sub_5FA44(0, 3, player.hit_point_max, 10);

                if (affect.field_3 > 0xFD)
                {
                    affect.field_3--;
                }
                else
                {
                    ovr024.remove_affect(affect, Affects.affect_58, player);
                }

                var_1 = ovr025.clear_actions(player);
            }
        }


        internal static void spell_spit_acid(byte arg_0, object param, Player player)
        {
            byte var_1;

            gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x41);

            gbl.spell_target = player.actions.target;

            var_1 = ovr024.roll_dice(100, 1);

            if (ovr025.sub_68708(gbl.spell_target, player) < 7 &&
                gbl.spell_target != null)
            {
                if (var_1 <= 30)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);
                    ovr025.sub_67A59(0x17);

                    ovr025.sub_67AA4(30, 1,
                        ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    ovr024.damage_person(ovr024.do_saving_throw(0, 3, gbl.spell_target), 2, (sbyte)player.hit_point_max, gbl.spell_target);
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid and Misses", player);
                }
            }
        }


        internal static void spell_breathes_acid(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;
            byte var_A;
            bool var_9;
            Player var_8;
            byte var_4;
            bool var_1;

            gbl.byte_1DA70 = false;

            if (gbl.byte_1D8B7 == 0)
            {
                affect.field_3 = 3;
            }

            if (affect.field_3 > 0)
            {
                gbl.byte_1D2BF = 0x30;

                int var_2 = ovr033.PlayerMapXPos(player);
                int var_3 = ovr033.PlayerMapYPos(player);

                gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x3d);

                if (gbl.byte_1DA70 == true)
                {
                    sub_5D7CF(6, 1, gbl.byte_1D884, gbl.byte_1D883, var_3, var_2);
                }

                var_A = gbl.byte_1D75E;

                for (var_4 = 1; var_4 <= var_A; var_4++)
                {
                    if (ovr025.on_our_team(player) == gbl.sp_target[var_4].combat_team)
                    {
                        gbl.byte_1DA70 = false;
                    }
                }
                throw new System.NotSupportedException();//cmp	byte_1DA70, 0
                throw new System.NotSupportedException();//jnz	loc_61DED
                throw new System.NotSupportedException();//jmp	loc_61EF0
                throw new System.NotSupportedException();//loc_61DED:
                throw new System.NotSupportedException();//cmp	byte_1D75E, 0
                throw new System.NotSupportedException();//ja	loc_61DF7
                throw new System.NotSupportedException();//jmp	loc_61EF0
                throw new System.NotSupportedException();//loc_61DF7:

                ovr025.DisplayPlayerStatusString(true, 10, "breathes acid", player);
                ovr025.sub_67A59(0x12);
                throw new System.NotSupportedException();//push	[bp+player.seg]
                throw new System.NotSupportedException();//push	[bp+player.offset]
                throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//push	[bp+player.seg]
                throw new System.NotSupportedException();//push	[bp+player.offset]
                throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//push	sp_target[1].seg
                throw new System.NotSupportedException();//push	sp_target[1].offset
                throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//push	sp_target[1].seg
                throw new System.NotSupportedException();//push	sp_target[1].offset
                throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//mov	al, 1
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//mov	al, 0x1E
                throw new System.NotSupportedException();//push	ax
                throw new System.NotSupportedException();//call	sub_67AA4
                var_A = gbl.byte_1D75E;

                for (var_4 = 1; var_4 <= var_A; var_4++)
                {
                    if (gbl.sp_target[var_4] != null)
                    {
                        var_8 = gbl.sp_target[var_4];

                        var_9 = ovr024.do_saving_throw(0, 3, var_8);
                        ovr024.damage_person(var_9, 2, (sbyte)player.hit_point_max, var_8);
                    }
                }

                affect.field_3--;

                var_1 = ovr025.clear_actions(player);
            }
            //loc_61EF0:
        }


        internal static void spell_breathes_fire(byte arg_0, object param, Player arg_6)
        {
            Affect arg_2 = (Affect)param;
            byte var_A;
            bool var_9;
            Player var_8;
            byte var_4;
            int var_3;
            int var_2;
            bool var_1;

            throw new System.NotSupportedException();//cmp	byte_1D8B7, 0
            throw new System.NotSupportedException();//jnz	loc_61F19
            throw new System.NotSupportedException();//les	di, [bp+arg_2]
            throw new System.NotSupportedException();//mov	es:[di+affect.field_3],	3
            throw new System.NotSupportedException();//loc_61F19:
            throw new System.NotSupportedException();//les	di, [bp+arg_2]
            throw new System.NotSupportedException();//cmp	es:[di+affect.field_3],	0
            throw new System.NotSupportedException();//ja	loc_61F26
            throw new System.NotSupportedException();//jmp	loc_6207D
            throw new System.NotSupportedException();//loc_61F26:
            gbl.byte_1D2BF = 0x21;
            var_2 = ovr033.PlayerMapXPos(arg_6);
            var_3 = ovr033.PlayerMapYPos(arg_6);

            gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x3D);
            throw new System.NotSupportedException();//cmp	byte_1DA70, 0
            throw new System.NotSupportedException();//jnz	loc_61F60
            throw new System.NotSupportedException();//jmp	loc_6207D
            throw new System.NotSupportedException();//loc_61F60:
            sub_5D7CF(9, 3, gbl.byte_1D884, gbl.byte_1D883, var_3, var_2);
            throw new System.NotSupportedException();//cmp	byte_1D75E, 0
            throw new System.NotSupportedException();//ja	loc_61F84
            throw new System.NotSupportedException();//jmp	loc_6207D
            throw new System.NotSupportedException();//loc_61F84:
            ovr025.DisplayPlayerStatusString(true, 10, "breathes fire", arg_6);
            ovr025.sub_67A59(0x12);
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6+2]
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6]
            throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6+2]
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6]
            throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	sp_target[1].seg
            throw new System.NotSupportedException();//push	sp_target[1].offset
            throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	sp_target[1].seg
            throw new System.NotSupportedException();//push	sp_target[1].offset
            throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, 1
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, 0x1E
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	sub_67AA4
            var_A = gbl.byte_1D75E;

            for (var_4 = 1; var_4 <= var_A; var_4++)
            {
                if (gbl.sp_target[var_4] != null)
                {
                    var_8 = gbl.sp_target[var_4];
                    var_9 = ovr024.do_saving_throw(0, 3, var_8);

                    ovr024.damage_person(var_9, 2, (sbyte)arg_6.hit_point_max, var_8);
                }
            }
            arg_2.field_3 -= 1;
            var_1 = ovr025.clear_actions(arg_6);
            throw new System.NotSupportedException();//loc_6207D:

        }


        internal static void cast_breath_fire(byte arg_0, object param, Player arg_6)
        {
            gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x41);
            throw new System.NotSupportedException();//les	di, [bp+arg_6]
            throw new System.NotSupportedException();//les	di, int ptr es:[di+charStruct.actions.offset]
            throw new System.NotSupportedException();//mov	ax, es:[di+0Ah]
            throw new System.NotSupportedException();//mov	dx, es:[di+0Ch]
            throw new System.NotSupportedException();//mov	spell_target.offset, ax
            throw new System.NotSupportedException();//mov	spell_target.seg, dx
            throw new System.NotSupportedException();//mov	al, 1
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, 0x64
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	roll_dice(byte,byte)
            throw new System.NotSupportedException();//cmp	al, 0x32
            throw new System.NotSupportedException();//jbe	loc_620CF
            throw new System.NotSupportedException();//jmp	loc_6219A
            throw new System.NotSupportedException();//loc_620CF:
            throw new System.NotSupportedException();//call	ovr025.sub_68708( spell_target, arg_6)
            throw new System.NotSupportedException();//cmp	al, 2
            throw new System.NotSupportedException();//jb	loc_620E9
            throw new System.NotSupportedException();//jmp	loc_6219A
            throw new System.NotSupportedException();//loc_620E9:
            throw new System.NotSupportedException();//mov	ax, spell_target.offset
            throw new System.NotSupportedException();//or	ax, spell_target.seg
            throw new System.NotSupportedException();//jnz	loc_620F5
            throw new System.NotSupportedException();//jmp	loc_6219A
            throw new System.NotSupportedException();//loc_620F5:
            gbl.byte_1D2BF = 1;
            gbl.byte_1DA70 = ovr025.clear_actions(arg_6);

            ovr025.DisplayPlayerStatusString(true, 10, "Breathes Fire", arg_6);
            ovr025.sub_67A59(0x17);
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6+2]
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6]
            throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6+2]
            throw new System.NotSupportedException();//push	short ptr [bp+arg_6]
            throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	spell_target.seg
            throw new System.NotSupportedException();//push	spell_target.offset
            throw new System.NotSupportedException();//call	ovr033.sub_74C32(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	spell_target.seg
            throw new System.NotSupportedException();//push	spell_target.offset
            throw new System.NotSupportedException();//call	ovr033.sub_74C5A(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, 1
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, 0x1E
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	sub_67AA4

            ovr024.damage_person(ovr024.do_saving_throw(0, 3, gbl.spell_target), 2, 7, gbl.spell_target);
            throw new System.NotSupportedException();//loc_6219A:
        }


        internal static void cast_throw_lightning(byte arg_0, object param, Player arg_6)
        {
            bool var_1 = false; /* Simeon */

            if (gbl.byte_1D8B7 < 4)
            {
                int var_2 = ovr033.PlayerMapXPos(arg_6);
                int var_3 = ovr033.PlayerMapYPos(arg_6);

                ovr025.DisplayPlayerStatusString(true, 10, "throws lightning", arg_6);
                gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x33);

                ovr024.remove_affect_19(arg_6);
                ovr025.sub_67A59(0x13);
                ovr025.sub_67AA4(0x32, 4, gbl.byte_1D884, gbl.byte_1D883, var_3, var_2);

                sub_5F986(ref var_1, 0, 4, (byte)ovr024.roll_dice_save(6, 16), gbl.byte_1D884, gbl.byte_1D883);
                sub_5FA44(0, 0, (byte)ovr024.roll_dice_save(6, 16), 10);
                var_1 = ovr025.clear_actions(arg_6);
            }
        }


        internal static void cast_gaze_paralyze(byte arg_0, object param, Player arg_6)
        {
            arg_6.actions.target = null;

            gbl.dword_1D5CA(out gbl.byte_1DA70, 1, 0x24);

            gbl.spell_target = arg_6.actions.target;

            if (gbl.spell_target != null)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "gazes...", arg_6);

                ovr025.sub_67A59(0x12);

                ovr025.sub_67AA4(0x2d, 4, ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
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
                gbl.unk_1C020[item.type].field_0 > 10 &&
                gbl.unk_1C020[item.type].field_0 < 14)
            {
                var_1 = true;
            }
            else
            {
                var_1 = false;
            }

            return var_1;
        }


        internal static void sub_623FF(byte arg_0, Item item, Player player)
        {
            byte var_3;
            byte var_2;

            var_3 = 0;

            for (var_2 = 1; var_2 <= 3; var_2++)
            {
                if (((int)item.getAffect(var_2) & 0x7F) == arg_0)
                {
                    var_3 = var_2;
                }
            }

            if (var_3 != 0)
            {
                item.setAffect(var_3, 0);
                item.field_30 -= 1;
                if (item.field_30 < -46)
                {
                    ovr025.lose_item(item, player);
                }
            }
        }


        internal static void cast_a_spell(byte arg_0, string arg_2, Player arg_6)
        {
            if (gbl.game_state == 5)
            {

                ovr025.DisplayPlayerStatusString(true, 10, "Casts a Spell", arg_6);
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);

                seg041.displayString("Spell:" + AffectNames[arg_0], 0, 10, 0x17, 0);
            }
            else
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 0x12, 1);

                ovr025.displayPlayerName(false, 0x13, 1, arg_6);

                seg041.displayString(arg_2, 0, 10, 0x13, arg_6.name.Length + 2);
                seg041.displayString(AffectNames[arg_0], 0, 10, 0x14, 1);
                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();
            }
        }


        internal static void setup_spells()
        {
            gbl.byte_1D2C6 = 0;
            gbl.byte_1D88D = 0;
            gbl.dword_1D87F = null;
            gbl.byte_1D2C8 = true;

            gbl.dword_1D5CA = new spellDelegate(ovr023.cast_spell_on);

            gbl.word_1D5CE[0] = new spellDelegate2(ovr023.is_Blessed);
            gbl.word_1D5CE[1] = new spellDelegate2(ovr023.is_Cursed);
            gbl.word_1D5CE[2] = new spellDelegate2(ovr023.sub_5DDBC);
            gbl.word_1D5CE[3] = new spellDelegate2(ovr023.sub_5DDF8);
            gbl.word_1D5CE[4] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[5] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[6] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[7] = new spellDelegate2(ovr023.is_cold_resistant);
            gbl.word_1D5CE[8] = new spellDelegate2(ovr023.sub_5DEE1);
            gbl.word_1D5CE[9] = new spellDelegate2(ovr023.is_charmed);
            gbl.word_1D5CE[10] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[11] = new spellDelegate2(ovr023.is_stronger);
            gbl.word_1D5CE[12] = new spellDelegate2(ovr023.has_been_reduced);
            gbl.word_1D5CE[13] = new spellDelegate2(ovr023.is_friendly);
            gbl.word_1D5CE[14] = new spellDelegate2(ovr023.sub_5E221);
            gbl.word_1D5CE[15] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[16] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[17] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[18] = new spellDelegate2(ovr023.is_shielded);
            gbl.word_1D5CE[19] = new spellDelegate2(ovr023.sub_5E2B2);
            gbl.word_1D5CE[20] = new spellDelegate2(ovr023.falls_asleep);
            gbl.word_1D5CE[21] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[22] = new spellDelegate2(ovr023.is_held);
            gbl.word_1D5CE[23] = new spellDelegate2(ovr023.is_fire_resistant);
            gbl.word_1D5CE[24] = new spellDelegate2(ovr023.is_silenced);
            gbl.word_1D5CE[25] = new spellDelegate2(ovr023.is_affected2);
            gbl.word_1D5CE[26] = new spellDelegate2(ovr023.is_charmed2);
            gbl.word_1D5CE[27] = new spellDelegate2(ovr023.sub_5E681);
            gbl.word_1D5CE[28] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[29] = new spellDelegate2(ovr023.is_invisible);
            gbl.word_1D5CE[30] = new spellDelegate2(ovr023.knock_Knock);
            gbl.word_1D5CE[31] = new spellDelegate2(ovr023.is_duplicated);
            gbl.word_1D5CE[32] = new spellDelegate2(ovr023.is_weakened);
            gbl.word_1D5CE[33] = new spellDelegate2(ovr023.create_noxious_cloud);
            gbl.word_1D5CE[34] = new spellDelegate2(ovr023.sub_5EC5B);
            gbl.word_1D5CE[35] = new spellDelegate2(ovr023.is_animated);
            gbl.word_1D5CE[36] = new spellDelegate2(ovr023.can_see);
            gbl.word_1D5CE[37] = new spellDelegate2(ovr023.is_blind);
            gbl.word_1D5CE[38] = new spellDelegate2(ovr023.sub_5F0DC);
            gbl.word_1D5CE[39] = new spellDelegate2(ovr023.is_diseased);
            gbl.word_1D5CE[40] = new spellDelegate2(ovr023.is_affected3);
            gbl.word_1D5CE[41] = new spellDelegate2(ovr023.is_praying);
            gbl.word_1D5CE[42] = new spellDelegate2(ovr023.uncurse);
            gbl.word_1D5CE[43] = new spellDelegate2(ovr023.curse);
            gbl.word_1D5CE[44] = new spellDelegate2(ovr023.spell_blinking);
            gbl.word_1D5CE[45] = new spellDelegate2(ovr023.is_affected3);
            gbl.word_1D5CE[46] = new spellDelegate2(ovr023.sub_5F782);
            gbl.word_1D5CE[47] = new spellDelegate2(ovr023.cast_haste);
            gbl.word_1D5CE[48] = new spellDelegate2(ovr023.is_held);
            gbl.word_1D5CE[49] = new spellDelegate2(ovr023.is_invisible);
            gbl.word_1D5CE[50] = new spellDelegate2(ovr023.sub_5FCD9);
            gbl.word_1D5CE[51] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[52] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[53] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[54] = new spellDelegate2(ovr023.sub_5FD2E);
            gbl.word_1D5CE[55] = new spellDelegate2(ovr023.cast_restore);
            gbl.word_1D5CE[56] = new spellDelegate2(ovr023.cast_speed);
            gbl.word_1D5CE[57] = new spellDelegate2(ovr023.sub_5FF6D);
            gbl.word_1D5CE[58] = new spellDelegate2(ovr023.cast_strength);
            gbl.word_1D5CE[59] = new spellDelegate2(ovr023.sub_6003C);
            gbl.word_1D5CE[60] = new spellDelegate2(ovr023.cast_paralyzed);
            gbl.word_1D5CE[61] = new spellDelegate2(ovr023.cast_heal);
            gbl.word_1D5CE[62] = new spellDelegate2(ovr023.cast_invisible);
            gbl.word_1D5CE[63] = new spellDelegate2(ovr023.sub_5F782);
            gbl.word_1D5CE[64] = new spellDelegate2(ovr023.sub_6014A);
            gbl.word_1D5CE[65] = new spellDelegate2(ovr023.sub_60185);
            gbl.word_1D5CE[66] = new spellDelegate2(ovr023.cure_poison);
            gbl.word_1D5CE[67] = new spellDelegate2(ovr023.sub_602D0);
            gbl.word_1D5CE[68] = new spellDelegate2(ovr023.is_protected);
            gbl.word_1D5CE[69] = new spellDelegate2(ovr023.cast_flattern);
            gbl.word_1D5CE[70] = new spellDelegate2(ovr023.sub_603F0);
            gbl.word_1D5CE[71] = new spellDelegate2(ovr023.sub_60431);
            gbl.word_1D5CE[72] = new spellDelegate2(ovr023.is_affected4);
            gbl.word_1D5CE[73] = new spellDelegate2(ovr023.sub_604DA);
            gbl.word_1D5CE[74] = new spellDelegate2(ovr023.cast_raise);
            gbl.word_1D5CE[75] = new spellDelegate2(ovr023.cast_slay);
            gbl.word_1D5CE[76] = new spellDelegate2(ovr023.is_affected);
            gbl.word_1D5CE[77] = new spellDelegate2(ovr023.cast_entangle);
            gbl.word_1D5CE[78] = new spellDelegate2(ovr023.cast_highlisht);
            gbl.word_1D5CE[79] = new spellDelegate2(ovr023.cast_invisible2);
            gbl.word_1D5CE[80] = new spellDelegate2(ovr023.cast_charmed);
            gbl.word_1D5CE[81] = new spellDelegate2(ovr023.cast_confuse);
            gbl.word_1D5CE[82] = new spellDelegate2(ovr023.cast_teleport);
            gbl.word_1D5CE[83] = new spellDelegate2(ovr023.cast_terror);
            gbl.word_1D5CE[84] = new spellDelegate2(ovr023.cast_protection);
            gbl.word_1D5CE[85] = new spellDelegate2(ovr023.spell_slow);
            gbl.word_1D5CE[86] = new spellDelegate2(ovr023.sub_60F0B);
            gbl.word_1D5CE[87] = new spellDelegate2(ovr023.sub_60F4E);
            gbl.word_1D5CE[88] = new spellDelegate2(ovr023.uncurse);
            gbl.word_1D5CE[89] = new spellDelegate2(ovr023.is_animated);
            gbl.word_1D5CE[90] = new spellDelegate2(ovr023.spell_poisonous_cloud);
            gbl.word_1D5CE[91] = new spellDelegate2(ovr023.sub_61550);
            gbl.word_1D5CE[92] = new spellDelegate2(ovr023.sub_615F2);
            gbl.word_1D5CE[93] = new spellDelegate2(ovr023.is_held);
            gbl.word_1D5CE[94] = new spellDelegate2(ovr023.sub_6169E);
            gbl.word_1D5CE[95] = new spellDelegate2(ovr023.sub_616CC);
            gbl.word_1D5CE[96] = new spellDelegate2(ovr023.sub_616FA);
            gbl.word_1D5CE[97] = new spellDelegate2(ovr023.sub_61727);
            gbl.word_1D5CE[98] = new spellDelegate2(ovr023.cast_heal2);
            gbl.word_1D5CE[99] = new spellDelegate2(ovr023.curse);
        }
    }
}
