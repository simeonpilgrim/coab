using Classes;

namespace engine
{
    class ovr026
    {
        static byte[] /*seg600:42BC*/ unk_1A5CC = { 0xDD,
            0x06, 0x00, 0xFF, 0xFF, 0xFF,
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
            1, 0, 0, 0, 0,
            0, 1, 0, 0, 0,
            1, 1, 0, 0, 0,
            0, 1, 1, 0, 0,
            0, 0, 1, 0, 0, 
            0, 0, 0, 1, 0,    
            0, 0, 1, 1, 0, 
            1, 1, 0, 0, 1,  //seg600:42EE
            0, 0, 0, 1, 1,  //seg600:42F3
            1, 0, 1, 0, 0,  //seg600:42F8
            1, 1, 1, 0, 0,  //seg600:42FD
        };


   


        internal static void sub_6A00F(Player arg_0)
        {
            Struct_19AEC var_12;
            Player var_E;
            Item var_A;
            byte var_6;
            sbyte var_5;
            sbyte var_4;
            sbyte var_2;
            byte var_1;

            var_E = arg_0;

            seg051.FillChar(0, 0x0f, var_E.field_12D);

            for (var_1 = 0; var_1 <= 7; var_1++)
            {
                if (sub_6B3D1(arg_0) != 0 &&
                    var_E.Skill_B_lvl[var_1] != 0)
                {
                    var_2 = (sbyte)var_E.Skill_B_lvl[var_1];
                }
                else
                {
                    var_2 = (sbyte)var_E.Skill_A_lvl[var_1];
                }

                if (var_2 > 0)
                {
                    switch (var_1)
                    {
                        case 0:
                            var_E.field_12D[0] += 1;

                            for (int var_3 = 2; var_3 <= var_2; var_3++)
                            {
                                for (var_5 = 1; var_5 <= 5; var_5++)
                                {
                                    var_E.field_12D[var_5 - 1] = unk_1A5CC[(var_3 * 5) + var_5];
                                }
                            }

                            sub_6A686(0, arg_0);

                            for (var_6 = 1; var_6 <= 100; var_6++)
                            {
                                var_12 = gbl.unk_19AEC[var_6];


                                if (var_12.field_0 == 0 &&
                                    var_E.field_12CArray[var_12.field_1] > 0 &&
                                    var_6 != 0x24)
                                {
                                    var_E.field_79[var_6 - 1] = 1;
                                }
                            }
                            break;

                        case 3:
                            if (var_2 > 8)
                            {
                                for (int var_3 = 9; var_3 <= var_2; var_3++)
                                {
                                    for (var_5 = 1; var_5 <= 5; var_5++)
                                    {
                                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//mov	dx, ax
                                        throw new System.NotSupportedException();//mov	al, [bp+var_3]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//mov	di, ax
                                        throw new System.NotSupportedException();//mov	si, di
                                        throw new System.NotSupportedException();//shl	di, 1
                                        throw new System.NotSupportedException();//shl	di, 1
                                        throw new System.NotSupportedException();//add	di, si
                                        throw new System.NotSupportedException();//add	di, dx
                                        throw new System.NotSupportedException();//mov	dl, [di+43E5h]
                                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//les	di, [bp+var_E]
                                        throw new System.NotSupportedException();//add	di, ax
                                        throw new System.NotSupportedException();//add	es:[di+12Ch], dl
                                    }
                                }

                                for (var_6 = 1; var_6 <= 100; var_6++)
                                {
                                    var_12 = gbl.unk_19AEC[var_6];

                                    if (var_12.field_0 == 0 &&
                                        var_E.field_12CArray[var_12.field_1] > 0)
                                    {
                                        var_E.field_79[var_6] = 1;
                                    }
                                }
                            }
                            break;

                        case 4:
                            if (var_2 > 7)
                            {
                                for (int var_3 = 8; var_3 <= var_2; var_3++)
                                {
                                    for (var_5 = 1; var_5 <= 3; )
                                    {
                                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//mov	dx, ax
                                        throw new System.NotSupportedException();//mov	al, [bp+var_3]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//mov	di, ax
                                        throw new System.NotSupportedException();//mov	si, di
                                        throw new System.NotSupportedException();//shl	di, 1
                                        throw new System.NotSupportedException();//shl	di, 1
                                        throw new System.NotSupportedException();//add	di, si
                                        throw new System.NotSupportedException();//add	di, dx
                                        throw new System.NotSupportedException();//mov	dl, [di+4448h]
                                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                        throw new System.NotSupportedException();//cbw
                                        throw new System.NotSupportedException();//les	di, [bp+var_E]
                                        throw new System.NotSupportedException();//add	di, ax
                                        throw new System.NotSupportedException();//add	es:[di+131h], dl
                                    }
                                    var_5 = 4;
                                    throw new System.NotSupportedException();//jmp	short loc_6A275
                                    throw new System.NotSupportedException();//loc_6A272:
                                    var_5++;
                                    throw new System.NotSupportedException();//loc_6A275:
                                    var_4 = (sbyte)(var_5 - 3);
                                    throw new System.NotSupportedException();//mov	al, [bp+var_5]
                                    throw new System.NotSupportedException();//cbw
                                    throw new System.NotSupportedException();//mov	dx, ax
                                    throw new System.NotSupportedException();//mov	al, [bp+var_3]
                                    throw new System.NotSupportedException();//cbw
                                    throw new System.NotSupportedException();//mov	di, ax
                                    throw new System.NotSupportedException();//mov	si, di
                                    throw new System.NotSupportedException();//shl	di, 1
                                    throw new System.NotSupportedException();//shl	di, 1
                                    throw new System.NotSupportedException();//add	di, si
                                    throw new System.NotSupportedException();//add	di, dx
                                    throw new System.NotSupportedException();//mov	dl, [di+4448h]
                                    throw new System.NotSupportedException();//mov	al, [bp+var_4]
                                    throw new System.NotSupportedException();//cbw
                                    throw new System.NotSupportedException();//les	di, [bp+var_E]
                                    throw new System.NotSupportedException();//add	di, ax
                                    throw new System.NotSupportedException();//add	es:[di+136h], dl
                                    throw new System.NotSupportedException();//cmp	[bp+var_5], 5
                                    throw new System.NotSupportedException();//jnz	loc_6A272
                                }

                                for (var_6 = 1; var_6 <= 100; var_6++)
                                {
                                    if (gbl.unk_19AEC[var_6].field_0 == 1)
                                    {
                                        throw new System.NotSupportedException();//mov	al, [bp+var_6]
                                        throw new System.NotSupportedException();//xor	ah, ah
                                        throw new System.NotSupportedException();//les	di, [bp+var_E]
                                        throw new System.NotSupportedException();//add	di, ax
                                        throw new System.NotSupportedException();//mov	byte ptr es:[di+78h], 1
                                    }
                                }
                            }

                            break;

                        case 5:
                            var_E.field_12D[10] += 1;

                            for (int var_3 = 0; var_3 <= (var_2 - 2); var_3++)
                            {
                                /* unk_1A7C6 = seg600:44B6 */
                                var_E.field_12D[10] += ovr020.unk_1A7C6[(var_3 * 5) + 0];
                                var_E.field_12D[11] += ovr020.unk_1A7C6[(var_3 * 5) + 1];
                                var_E.field_12D[12] += ovr020.unk_1A7C6[(var_3 * 5) + 2];
                                var_E.field_12D[13] += ovr020.unk_1A7C6[(var_3 * 5) + 3];
                                var_E.field_12D[14] += ovr020.unk_1A7C6[(var_3 * 5) + 4];
                            }
                            break;
                    }
                }
            }
            var_A = var_E.itemsPtr;

            while (var_A != null)
            {
                if (var_A.affect_3 == Affects.affect_81 &&
                    var_A.readied)
                {
                    for (var_5 = 1; var_5 <= 3; var_5++)
                    {
                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//les	di, [bp+arg_0]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	dl, es:[di+136h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_5]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//les	di, [bp+arg_0]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//add	es:[di+136h], dl
                    }
                }

                var_A = var_A.next;
            }
        }


        internal static void sub_6A3C6(Player arg_0)
        {
            byte var_6;
            byte var_4;
            byte var_1;

            arg_0.field_73 = 0;

            for (var_1 = 0; var_1 <= 7; var_1++)
            {
                var_4 = arg_0.Skill_A_lvl[var_1];

                if (ovr018.unk_1A14A[(var_1 * 0xD) + var_4] > arg_0.field_73)
                {
                    arg_0.field_73 = ovr018.unk_1A14A[(var_1 * 0xD) + var_4];
                }

                if (arg_0.field_E5 < var_4)
                {
                    arg_0.field_E5 = var_4;
                }

                if (var_1 == 2 || var_1 == 3)
                {
                    if (var_4 > 6)
                    {
                        arg_0.field_11C = 3;
                    }
                }
                else if (var_1 == 4)
                {
                    if (var_4 > 7)
                    {
                        arg_0.field_11C = 3;
                    }
                }
            }

            sub_6A00F(arg_0);
            sub_6A7FB(arg_0);

            if (arg_0.thief_lvl > 0)
            {
                sub_6AAEA(arg_0);
            }

            arg_0.field_12B = 0;

            for (var_1 = 0; var_1 <= 7; var_1++)
            {
                if (arg_0.Skill_A_lvl[var_1] > 0 ||
                    (arg_0.Skill_B_lvl[var_1] > 0 && arg_0.Skill_B_lvl[var_1] < arg_0.field_E5))
                {
                    arg_0.field_12B += ovr018.unk_1A1B2[var_1];
                }
            }

            for (var_6 = 0; var_6 <= 12; var_6++)
            {
                if (arg_0.itemArray[var_6] != null &&
                    (gbl.unk_1C020[arg_0.itemArray[var_6].type].field_D & arg_0.field_12B) == 0 &&
                    arg_0.itemArray[var_6].field_36 == 0)
                {
                    arg_0.itemArray[var_6].readied = false;
                }
            }

            if (sub_6B3D1(arg_0) != 0)
            {
                for (var_1 = 0; var_1 <= 7; var_1++)
                {
                    var_4 = arg_0.Skill_B_lvl[var_1];

                    if (var_1 == 2 || var_1 == 3)
                    {
                        if (var_4 > 6)
                        {
                            arg_0.field_11C = 3;
                        }
                    }
                    else if (var_1 == 4)
                    {
                        if (var_4 > 7)
                        {
                            arg_0.field_11C = 3;
                        }
                    }

                    if (ovr018.unk_1A14A[(var_1 * 0xd) + var_4] > arg_0.field_73)
                    {
                        arg_0.field_73 = ovr018.unk_1A14A[(var_1 * 0xd) + var_4];
                    }
                }

                if (arg_0.field_113 > 6 ||
                    arg_0.field_115 > 7 ||
                    arg_0.field_114 > 6)
                {
                    arg_0.field_11C = 3;
                }

                if (arg_0.field_117 > 0)
                {
                    sub_6AAEA(arg_0);
                }
            }
        }


        internal static void sub_6A686(byte arg_0, Player arg_2)
        {
            Player var_7;
            sbyte var_3;
            sbyte var_2;
            sbyte var_1;

            var_3 = (sbyte)(arg_2.cleric_lvl + arg_2.turn_undead * sub_6B3D1(arg_2));

            if (var_3 > 0)
            {
                var_7 = arg_2;

                if (arg_0 != 0)
                {
                    for (var_2 = 2; var_2 <= 5; var_2++)
                    {
                        var_7.field_12CArray[var_2] = 0;
                    }

                    var_7.field_12D[0] = 1;

                    for (var_1 = 2; var_1 <= var_3; var_1++)
                    {
                        for (var_2 = 1; var_2 <= 5; var_2++)
                        {
                            var_7.field_12CArray[var_2] += unk_1A5CC[var_2 + (var_1 * 5)];
                        }
                    }
                }

                if (var_7.wis > 12 && var_7.field_12CArray[1] > 0)
                {
                    var_7.field_12CArray[1] += 1;
                }

                if (var_7.wis > 13 && var_7.field_12CArray[1] > 0)
                {
                    var_7.field_12CArray[1] += 1;
                }

                if (var_7.wis > 14 && var_7.field_12CArray[2] > 0)
                {
                    var_7.field_12CArray[2] += 1;
                }

                if (var_7.wis > 15 && var_7.field_12CArray[2] > 0)
                {
                    var_7.field_12CArray[2] += 1;
                }

                if (var_7.wis > 16 && var_7.field_12CArray[3] > 0)
                {
                    var_7.field_12CArray[3] += 1;
                }

                if (var_7.wis > 17 && var_7.field_12CArray[4] > 0)
                {
                    var_7.field_12CArray[4] += 1;
                }
            }
        }

        static byte[] byte_1A8CE = { 
        
    4, 2, 6, 5, 4, 10, 13, 14, 16, 15, 10, 13, 14,
    16, 15, 10, 13, 14, 16, 15, 9, 12, 13, 15,
    14, 9, 12, 13, 15, 14, 9, 12, 13, 15, 14, 7,
    10, 11, 13, 12, 7, 10, 11, 13, 12, 7, 10, 11,
    13, 12, 6, 9, 10, 12, 11, 6, 9, 10, 12, 11,
    6, 9, 10, 12, 11, 10, 13, 14, 16, 15, 10, 13,
    14, 16, 15, 10, 13, 14, 16, 15, 9, 12, 13,
    15, 14, 9, 12, 13, 15, 14, 9, 12, 13, 15, 14,
    7, 10, 11, 13, 12, 7, 10, 11, 13, 12, 7, 10,
    11, 13, 12, 6, 9, 10, 12, 11, 6, 9, 10, 12,
    11, 6, 9, 10, 12, 11, 14, 15, 16, 17, 17, 14,
    15, 16, 17, 17, 13, 14, 15, 16, 16, 13, 14,
    15, 16, 16, 11, 12, 13, 13, 14, 11, 12, 13,
    13, 14, 10, 11, 12, 12, 13, 10, 11, 12, 12,
    13, 8, 9, 10, 9, 11, 8, 9, 10, 9, 11, 7, 8, 9,
    8, 10, 7, 8, 9, 8, 10, 12, 13, 14, 15, 15, 12,
    13, 14, 15, 15, 11, 12, 13, 14, 14, 11, 12,
    13, 14, 14, 9, 9, 11, 11, 12, 9, 9, 11, 11,
    12, 8, 9, 10, 10, 11, 9, 9, 10, 10, 11, 6, 7,
    8, 7, 9, 6, 7, 8, 7, 9, 5, 6, 7, 6, 8, 5, 6, 7, 6, 8,
    14, 15, 16, 17, 17, 14, 15, 16, 17, 17, 13,
    14, 15, 16, 16, 13, 14, 15, 16, 16, 11, 12,
    13, 13, 14, 11, 12, 13, 13, 14, 10, 11, 12,
    12, 13, 10, 11, 12, 12, 13, 8, 9, 10, 9, 11,
    8, 9, 10, 9, 11, 7, 8, 9, 8, 10, 7, 8, 9, 8, 10,
    14, 13, 11, 15, 12, 14, 13, 11, 15, 12, 14,
    13, 11, 15, 12, 14, 13, 11, 15, 12, 14, 13,
    11, 15, 12, 13, 11, 9, 13, 10, 13, 11, 9, 13,
    10, 13, 11, 9, 13, 10, 13, 11, 9, 13, 10, 13,
    11, 9, 13, 10, 11, 9, 7, 11, 8, 11, 9, 7, 11,
    8, 13, 12, 14, 16, 15, 13, 12, 14, 16, 15,
    13, 12, 14, 16, 15, 13, 12, 14, 16, 15, 12,
    11, 12, 15, 13, 12, 11, 12, 15, 13, 12, 11,
    12, 15, 13, 12, 11, 12, 15, 13, 11, 10, 10,
    14, 11, 11, 10, 10, 14, 11, 11, 10, 10, 14,
    11, 11, 10, 10, 14, 11, 13, 12, 14, 16, 15,
    13, 12, 14, 16, 15, 13, 12, 14, 16, 15, 13,
    12, 14, 16, 15, 12, 11, 12, 15, 13, 12, 11,
    12, 15, 13, 12, 11, 12, 15, 13, 12, 11, 12,
    15, 13, 11, 10, 10, 14, 11, 13, 11, 9, 13,
    10, 11, 9, 7, 11, 8 };

        internal static void sub_6A7FB(Player arg_0)
        {
            bool var_9;
            Item var_8;
            byte var_4;
            byte var_2;
            byte var_1;

            var_9 = false;

            var_8 = arg_0.itemsPtr;

            while (var_8 != null && var_9 == false)
            {
                if ((int)var_8.affect_3 > 0x80 &&
                    var_8.readied)
                {
                    var_4 = (byte)((int)var_8.affect_3 & 0x7F);

                    var_9 = (var_4 == 6);
                }

                var_8 = var_8.next;
            }

            for (var_1 = 0; var_1 <= 4; var_1++)
            {
                arg_0.field_DFArraySet(var_1, 0x14);
                for (var_2 = 0; var_2 <= 7; var_2++)
                {
                    if (arg_0.Skill_A_lvl[var_2] > 0)
                    {
                        byte dl = byte_1A8CE[(var_2 * 60) + (arg_0.Skill_A_lvl[var_2] * 5) + var_1];

                        if (arg_0.field_DFArrayGet(var_1) > dl)
                        {
                            arg_0.field_DFArraySet(var_1, dl);
                        }
                    }
                }

                var_2 -= 1; /* Orignal code had a post use test and would exit on 7,
                            * but this for loops uses are pre-test increment so fix var_2 */

                if (arg_0.Skill_A_lvl[var_2] > arg_0.Skill_B_lvl[var_2])
                {
                    byte dl = byte_1A8CE[(var_2 * 60) + (arg_0.Skill_B_lvl[var_2] * 5) + var_1];

                    if (arg_0.field_DFArrayGet(var_1) > dl)
                    {
                        arg_0.field_DFArraySet(var_1, dl);
                    }
                }

                if (var_1 == 0)
                {
                    if (arg_0.race == Race.dwarf || arg_0.race == Race.halfling ||
                        var_9 == true)
                    {
                        if (arg_0.con >= 4 && arg_0.con <= 6)
                        {
                            arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 1));
                        }
                        else if (arg_0.con >= 7 && arg_0.con <= 10)
                        {
                            arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 2));
                        }
                        else if (arg_0.con >= 11 && arg_0.con <= 13)
                        {

                            arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 3));
                        }
                        else if (arg_0.con >= 14 && arg_0.con <= 17)
                        {
                            arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 4));
                        }
                        else if (arg_0.con == 18)
                        {
                            arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 5));
                        }
                    }
                }

                if (var_1 == 0)
                {
                    if (arg_0.con == 0x13 || arg_0.con == 0x14)
                    {
                        arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 1));
                    }
                    else if (arg_0.con == 0x15 || arg_0.con == 0x16)
                    {
                        arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 2));
                    }
                    else if (arg_0.con == 0x17 || arg_0.con == 0x18)
                    {
                        arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 3));
                    }
                    else if (arg_0.con == 0x19)
                    {
                        arg_0.field_DFArraySet(var_1, (byte)(arg_0.field_DFArrayGet(var_1) + 4));
                    }
                }
            }
        }


        static sbyte[,] /*seg600:3F20 */ unk_1A230 = { 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 10, 15, 0, 0, 0, -10, -5 },
            { 0, 5, -5, 0, 5, 10, 5, 0, 0 },
            { 0, 0, 5, 10, 5, 5, 10, -15, 0 }, 
            { 0, 10, 0, 0, 0, 5, 0, 0, 0 }, 
            { 0, 5, 5, 5, 10, 15, 5, -15, -5 }, 
            { 0, -5, 5, 5, 0, 0, 5, 5, -10 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, -15, -10, -10, -20, -10, -19, -5, 10 }, 
            { 0, -15, -5, -5, 0, -5, -10, 0, 0 }, 
            { 0, 0, 0, -5, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, -5, 0, 0, 0 } };

        static sbyte[,] /*seg600:3F33 */ unk_1A243 = { 
            { 0, 5, 10, 5, 0, 0 }, 
            { 0, 0, 5, 10, 5, 5 }, 
            { 5, 10, -15, 0, 10, 0 }, 
            { 0, 0, 0, 5, 0, 0 }, 
            { 0, 0, 5, 5, 5, 10 }, 
            { 10, 15, 5, -15, -5, -5 }, 
            { -5, 5, 5, 0, 0, 5 },
            { 5, 5, -10, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, -15, -10, -10, -20, -10 }, 
            { -10, -19, -5, -10, -15, -5 }, 
            { -5, -5, 0, -5, -10, 0 }, 
            { 0, 0, 0, 0, -5, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, -5, 0, 0, 0 }, 
            { 0, 5, 10, 0, 5, 5 }, 
            { 5, 10, 15, 5, 10, 10 }, 
            { 10, 15, 20, 10, 12, 12 }, 
            { 12, 8, 8, 18, 17, 99 }, 
            { 99, 0, 3, 18, 3, 18 } };

        static byte[,] /*seg600:3EC0 */ unk_1A1D0 = {
            { 0, 0, 0, 0 , 0 , 0 ,0 , 0, 0 },
            { 0, 0x1E, 0x19, 0x14 ,0x0F, 0x0A, 0x0A, 0x55, 0x00 },
            { 0, 0x23, 0x1D, 0x19, 0x15, 0x0F, 0x0A, 0x56, 0x00 },
            { 0, 0x28, 0x21, 0x1E, 0x1B, 0x14, 0x0F, 0x57, 0x00 },
            { 0, 0x2D, 0x25, 0x23, 0x21, 0x19, 0x0F, 0x58, 0x14 },
            { 0, 0x32, 0x2A, 0x28, 0x28, 0x1F, 0x14, 0x5A, 0x19 },
            { 0, 0x37, 0x2F, 0x2D, 0x25, 0x14, 0x5C, 0x1E, 0x3C },
            { 0, 0x34, 0x32, 0x37, 0x2B, 0x19, 0x5E, 0x23, 0x41},
            { 0, 0x39, 0x37, 0x3E, 0x31, 0x19, 0x60, 0x28, 0x46},
            { 0, 0x3E, 0x3C, 0x46, 0x38, 0x1E, 0x62, 0x2D, 0x50},
            { 0, 0x43, 0x41, 0x4E, 0x3F, 0x1E, 0x63, 0x32, 0x5A},
            { 0, 0x48, 0x46, 0x56, 0x46, 0x23, 0x63, 0x3C, 0x64},
            { 0, 0x64, 0x4D, 0x4B, 0x5E, 0x4D, 0x23, 0x63, 0x41 } };



        internal static void sub_6AAEA(Player player)
        {
            Player player_ptr;
            bool var_B;
            bool var_A;
            Item item_ptr;
            sbyte var_5;
            sbyte var_4;
            byte var_3;
            byte var_2 = 0; //Simeon
            byte var_1;

            player_ptr = player;
            var_A = false;
            var_B = false;
            item_ptr = player_ptr.itemsPtr;

            while (item_ptr != null && var_A == false && var_B == false)
            {
                if ((int)item_ptr.affect_3 > 0x80 &&
                    item_ptr.readied)
                {
                    var_3 = (byte)((int)item_ptr.affect_3 & 0x7f);

                    var_A = (var_3 == 11);
                    var_B = (var_3 == 2);
                }

                item_ptr = item_ptr.next;
            }


            var_4 = (sbyte)(player.thief_lvl + (sub_6B3D1(player) * player.field_117));

            if (var_4 < 4 && var_B == true)
            {
                var_4 = 4;
                var_B = false;
            }

            var_5 = var_4;

            for (var_1 = 1; var_1 <= 8; var_1++)
            {
                if (var_A == true)
                {
                    switch (var_1)
                    {
                        case 1:
                            if (var_4 < 5)
                            {
                                var_4 = 5;
                                var_2 = 0;
                            }
                            else
                            {
                                var_2 = 5;
                            }

                            break;
                        case 2:
                            if (var_4 < 7)
                            {
                                var_4 = 7;
                                var_2 = 0;
                            }
                            else
                            {
                                var_2 = 5;
                            }
                            break;
                    }
                }

                if (unk_1A230[(int)player.race, var_1] < 0 &&
                    unk_1A1D0[var_4, var_1] < (System.Math.Abs(unk_1A230[(int)player_ptr.race, var_1]) + var_2))
                {
                    player_ptr.field_EA[var_1 - 1] = 0;
                }
                else
                {
                    player_ptr.field_EA[var_1 - 1] = (byte)(var_2 + unk_1A1D0[var_4, var_1] + unk_1A230[(int)player_ptr.race, var_1]);

                    if (var_1 < 6)
                    {
                        player_ptr.field_EA[var_1 - 1] += (byte)unk_1A243[player_ptr.dex, var_1];
                    }

                }

                if (var_B == true)
                {
                    player_ptr.field_EA[var_1 - 1] += 10;
                }

                var_4 = var_5;
            }
        }


        internal static bool sub_6AD3E(byte arg_2, Player bp_arg_0)
        {
            byte var_4;
            sbyte var_3;
            bool var_2;

            var_2 = (gbl.unk_1A30A[(int)bp_arg_0.race, gbl.byte_1DA71] != getFirstSkill(bp_arg_0));
            var_3 = 0;

            while (var_3 <= 5 &&
            (gbl.unk_1A484[getFirstSkill(bp_arg_0)][var_3] < 9 ||
                    bp_arg_0.stats[var_3].tmp > 0x0E))
            {
                var_3++;
            }

            var_2 = (var_2 == true && var_3 > 5);
            var_3 = 0;
            throw new System.NotSupportedException();//loc_6ADE5:
            throw new System.NotSupportedException();//cmp	[bp+var_3], 5
            throw new System.NotSupportedException();//jg	loc_6AE23
            throw new System.NotSupportedException();//mov	al, [bp+var_3]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//mov	al, byte ptr [bp+arg_2]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	si, di
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//add	di, si
            throw new System.NotSupportedException();//add	di, dx
            throw new System.NotSupportedException();//cmp	byte ptr unk_1A484[di].field_0, 9
            throw new System.NotSupportedException();//jb	loc_6AE1E
            throw new System.NotSupportedException();//cmp	bp_arg_0.stats[var_3].tmp, 0x10
            throw new System.NotSupportedException();//jbe	loc_6AE23
            throw new System.NotSupportedException();//loc_6AE1E:
            var_3++;
            throw new System.NotSupportedException();//jmp	short loc_6ADE5
            throw new System.NotSupportedException();//loc_6AE23:
            var_2 = (var_2 == true && var_3 > 5);

            var_4 = 1;
            throw new System.NotSupportedException();//loc_6AE3C:
            throw new System.NotSupportedException();//mov	al, byte ptr [bp+arg_2]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, 0x0A
            throw new System.NotSupportedException();//mul	dx
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	al, unk_1A4EA[di]
            throw new System.NotSupportedException();//cmp	al, [bp+var_4]
            throw new System.NotSupportedException();//jb	loc_6AE7B
            throw new System.NotSupportedException();//mov	al, [bp+var_4]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//mov	cx, ax
            throw new System.NotSupportedException();//mov	al, byte ptr [bp+arg_2]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, 0x0A
            throw new System.NotSupportedException();//mul	dx
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//add	di, cx
            throw new System.NotSupportedException();//mov	al, unk_1A4EA[di]
            throw new System.NotSupportedException();//cmp	al, bp_arg_0.alignment
            throw new System.NotSupportedException();//jz	loc_6AE7B
            var_4++;
            throw new System.NotSupportedException();//jmp	short loc_6AE3C
            throw new System.NotSupportedException();//loc_6AE7B:
            throw new System.NotSupportedException();//cmp	[bp+var_2], 0
            throw new System.NotSupportedException();//jz	loc_6AE95
            throw new System.NotSupportedException();//mov	al, byte ptr [bp+arg_2]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, 0x0A
            throw new System.NotSupportedException();//mul	dx
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	al, unk_1A4EA[di]
            throw new System.NotSupportedException();//cmp	al, [bp+var_4]
            throw new System.NotSupportedException();//jnb	loc_6AE99
            throw new System.NotSupportedException();//loc_6AE95:
            throw new System.NotSupportedException();//mov	al, 0
            throw new System.NotSupportedException();//jmp	short loc_6AE9B
            throw new System.NotSupportedException();//loc_6AE99:
            throw new System.NotSupportedException();//mov	al, 1
            throw new System.NotSupportedException();//loc_6AE9B:
            throw new System.NotSupportedException();//mov	[bp+var_2], al

            return var_2;
        }


        internal static void multiclass(Player arg_0)
        {
            byte var_14;
            Item var_13;
            bool var_F;
            bool var_E;
            short var_D;
            StringList var_B;
            StringList var_7;
            byte var_3;
            byte var_2;
            char var_1;

            var_3 = gbl.unk_1A30A[(int)arg_0.race, 0];

            var_7 = new StringList();

            var_B = var_7;

            var_B.next = null;
            var_B.field_29 = 1;
            var_B.s = "Pick New Class";

            var_D = 1;
            var_14 = var_3;

            for (gbl.byte_1DA71 = 1; gbl.byte_1DA71 < var_14; gbl.byte_1DA71++)
            {
                var_2 = gbl.unk_1A30A[(int)arg_0.race, gbl.byte_1DA71];

                if (sub_6AD3E(var_2, arg_0) == true)
                {
                    var_B.next = new StringList();
                    var_B = var_B.next;

                    var_B.next = null;
                    var_B.field_29 = 0;
                    var_B.s = ovr020.classString[var_2];

                    var_D++;
                }
            }

            if (var_D == 1)
            {
                seg041.DisplayStatusText(15, 4, arg_0.name + " doesn't qualify.");
                ovr027.free_stringList(ref var_7);
                return;
            }

            var_B = var_7;
            var_D = 1;
            var_E = true;
            var_F = true;

            do
            {
                var_1 = ovr027.sl_select_item(out var_B, ref var_D, ref var_F, var_E, var_7,
                    0x16, 0x26, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (var_1 == 0)
                {
                    return;
                }
            } while (var_1 != 0x53);

            arg_0.exp = 0;
            arg_0.field_11C = 2;
            var_2 = 0;

            while (var_2 <= 7 && ovr020.classString[var_2] != var_B.s)
            {
                var_2++;
            }

            ovr027.free_stringList(ref var_7);

            arg_0.Skill_B_lvl[getFirstSkill(arg_0)] = hasAnySkills(arg_0);

            arg_0.field_E6 = arg_0.field_E5;
            arg_0.field_E5 = 1;

            arg_0.Skill_A_lvl[getFirstSkill(arg_0)] = 0;
            arg_0.Skill_A_lvl[var_2] = 1;

            seg051.FillChar(0, 15, arg_0.field_12D);

            if (var_2 == 0)
            {
                arg_0.field_12D[0] = 1;
            }
            else if (var_2 == 5)
            {
                arg_0.field_12D[10] = 1;
                arg_0.field_83 = 1;
                arg_0.field_8A = 1;
                arg_0.field_8D = 1;
            }

            arg_0._class = (ClassId)var_2;

            seg041.DisplayStatusText(0, 10, arg_0.name + " is now a 1st level " + ovr020.classString[var_2] + ".");

            seg051.FillChar(0, 0x54, arg_0.spell_list);

            sub_6A3C6(arg_0);
            sub_6A686(1, arg_0);
            sub_6A7FB(arg_0);
            sub_6AAEA(arg_0);

            var_13 = arg_0.itemsPtr;

            while (var_13 != null)
            {
                if ((gbl.unk_1C020[var_13.type].field_D & arg_0.field_12B) == 0 &&
                    var_13.field_36 == 0)
                {
                    var_13.readied = false;
                }

                var_13 = var_13.next;
            }
        }


        internal static byte getExtraFirstSkill(Player player)
        {
            byte skill_index;
            byte var_1;

            if (player.race != Race.human)
            {
                var_1 = 0x11;
            }
            else
            {
                skill_index = 0;

                while (skill_index < 7 && player.Skill_B_lvl[skill_index] == 0)
                {
                    skill_index++;
                }

                if (player.Skill_B_lvl[skill_index] > 0)
                {
                    var_1 = skill_index;
                }
                else
                {
                    var_1 = 0x11;
                }
            }

            return var_1;
        }


        internal static sbyte getFirstSkill(Player player)
        {
            sbyte skill_index;
            sbyte var_1;

            if (player.race != Race.human)
            {
                var_1 = 0x11;
            }
            else
            {
                skill_index = 0;
                while (skill_index < 7 && player.Skill_A_lvl[skill_index] == 0)
                {
                    skill_index++;
                }

                if (player.Skill_A_lvl[skill_index] > 0)
                {
                    var_1 = skill_index;
                }
                else
                {
                    var_1 = 0x11;
                }
            }

            return var_1;
        }


        internal static byte hasAnySkills(Player player)
        {
            byte loop_var;

            if (player.race != Race.human)
            {
                return 0;
            }

            loop_var = 0;

            while (loop_var < 7 &&
                player.Skill_A_lvl[loop_var] != 0)
            {
                loop_var++;
            }

            return player.Skill_A_lvl[loop_var];
        }


        internal static bool is_human(Player player)
        {
            if (player.race == Race.human)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static sbyte sub_6B3D1(Player player)
        {
            if (hasAnySkills(player) > player.field_E6)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
