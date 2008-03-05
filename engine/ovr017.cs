using Classes;

namespace engine
{
    class ovr017
    {
        internal static void sub_4708B(ref StringList arg_2, ref StringList arg_6, short arg_A, short arg_C, short arg_E, string arg_10)
        {
            string var_475;
            string var_275;
            byte var_164;
            string var_163;
            File var_112 = new File();
            SearchRec var_90;
            Player player_ptr;
            StringList var_61 = null;
            StringList var_5D;
            StringList var_59 = null;
            StringList var_55;
            string var_51;

            var_51 = arg_10;

            var_55 = null;
            var_5D = null;
            byte[] data = new byte[16];

            seg046.FINDFIRST(out var_90, 0, var_51);

            while (gbl.word_1EFBC == 0)
            {
                var_112.Assign(gbl.byte_1BF1A + var_90.fileName);
                seg051.Reset(1, var_112);

                if (seg051.FileSize(var_112) == arg_A)
                {

                    var_55 = new StringList();
                    var_5D = new StringList();

                    seg051.Seek(arg_E, var_112);

                    seg051.BlockRead(16, data, var_112);
                    var_55.s = Sys.ArrayToString(data, 0, 16);

                    seg051.Seek(arg_C, var_112);

                    if (gbl.import_from == 2)
                    {
                        var_164 = 0;
                    }
                    else
                    {
                        seg051.BlockRead(1, data, var_112);
                        var_164 = data[0];
                    }

                    var_5D.s = var_90.fileName;

                    if (seg051.Copy(4, 9, var_90.fileName, out var_275) == ".SAV")
                    {
                        var_163 = "from saved game " + var_90.fileName[7].ToString();
                    }
                    else
                    {
                        var_163 = string.Empty;
                    }

                    var_55.s += seg051.Copy(15 - var_55.s.Length, 1, "         ", out var_275) + var_163;

                    player_ptr = gbl.player_next_ptr;

                    while (player_ptr != null)
                    {
                        var_275 = seg051.Copy(15, 1, var_55.s, out var_275);

                        var_475 = string.Format("{0,-15}", player_ptr.name);

                        if (var_275 == var_475)
                        {
                            break;
                        }

                        player_ptr = player_ptr.next_player;
                    }

                    if (var_164 > 0x7F ||
                        player_ptr != null)
                    {
                        var_55 = null;
                        var_5D = null;
                    }

                    if (var_55 != null)
                    {
                        if (arg_6 == null)
                        {
                            arg_6 = var_55;
                            var_59 = arg_6;
                            arg_2 = var_5D;
                            var_61 = arg_2;
                        }
                        else
                        {
                            var_59.next = var_55;
                            var_59 = var_59.next;
                            var_61.next = var_5D;
                            var_61 = var_61.next;
                        }
                    }
                }

                seg051.Close(var_112);
                seg046.FINDNEXT(var_90);
            }
        }

        static byte[] unk_16818 = { 0, 0, 4 };
        static short[] unk_1681B = { 0xf7, 0x84, 0x13 };
        static short[] unk_16821 = { 0x01a6, 0x11d, 0xbc };

        internal static void sub_47465(out StringList arg_0, out StringList arg_4)
        {
            arg_4 = null;
            arg_0 = null;

            if (save() == true)
            {
                if (gbl.import_from == 0)
                {
                    sub_4708B(ref arg_0, ref arg_4, unk_16821[0], unk_1681B[0], unk_16818[0], gbl.byte_1BF1A + "*.guy");
                }
                else if (gbl.import_from == 1)
                {
                    sub_4708B(ref arg_0, ref arg_4, unk_16821[1], unk_1681B[1], unk_16818[1], gbl.byte_1BF1A + "*.cha");
                    sub_4708B(ref arg_0, ref arg_4, unk_16821[1], unk_1681B[1], unk_16818[1], gbl.byte_1BF1A + "*.sav");
                }
                else if (gbl.import_from == 2)
                {
                    sub_4708B(ref arg_0, ref  arg_4, unk_16821[2], unk_1681B[2], unk_16818[2], gbl.byte_1BF1A + "*.hil");
                }
            }
        }

        static Set unk_47635 = new Set(0x0001, new byte[] { 0x21 });

        internal static bool save()
        {
            string var_12F;
            SearchRec var_2E;
            short var_3;

            if (gbl.import_from == 0)
            {
                try
                {
                    if (System.IO.Directory.Exists(gbl.byte_1BF1A) == false)
                    {
                        System.IO.Directory.CreateDirectory(gbl.byte_1BF1a);
                    }
                }
                catch (System.Exception ex)
                {
                    string s = "Unexpected error during save: " + ex.ToString();
                    seg041.displayAndDebug(s, 0, 14);

                    return false;
                }
            }
            else if (gbl.import_from == 1)
            {
                do
                {
                    seg046.FINDFIRST(out var_2E, 16, gbl.byte_1BF1A);
                    var_3 = gbl.word_1EFBC;

                    if (var_3 != 0)
                    {
                        string s = "Unexpected error during save: " + var_3.ToString();
                        seg041.displayAndDebug(s, 0, 14);

                        return false;
                    }

                } while (var_3 != 0);
            }
            else if (gbl.import_from == 2)
            {
                do
                {
                    seg046.FINDFIRST(out var_2E, 16, seg051.Copy(gbl.byte_1BF1A.Length - 1, 1, gbl.byte_1BF1A, out var_12F));
                    var_3 = gbl.word_1EFBC;

                    if (var_3 != 0)
                    {
                        string s = "Unexpected error during save: " + var_3.ToString();
                        seg041.displayAndDebug(s, 0, 14);

                        return false;
                    }
                } while (var_3 != 0);
            }

            return true;
        }


        static void MergeIcons(DaxBlock destIcon, DaxBlock srcIcon) /* icon_xx */
        {
            for (int i = 0; i < srcIcon.bpp; i++)
            {
                byte a = destIcon.data[i];
                byte b = srcIcon.data[i];

                if (a == 16 && b == 16)
                {
                    destIcon.data[i] = 16;
                }
                else if (a == 16)
                {
                    destIcon.data[i] = b;
                }
                else if (b == 16)
                {
                    destIcon.data[i] = a;
                }
                else 
                {
                    destIcon.data[i] = (byte)(a | b);
                }

                //icon.data[i] = (byte)(((a == 16) ? (byte)0 : a) | ((b == 16) ? (byte)0 : b));

                a = destIcon.data_ptr[i];
                b = srcIcon.data_ptr[i];

                //TODO not sure about this...
                if (a == 16 && b == 16)
                {
                    destIcon.data_ptr[i] = 16;
                }
                else if (a == 16)
                {
                    destIcon.data_ptr[i] = b;
                }
                else if (b == 16)
                {
                    destIcon.data_ptr[i] = a;
                }
                else
                {
                    destIcon.data_ptr[i] = (byte)(a & b);
                }
                //icon.data_ptr[i] = (byte)(((a == 16) ? (byte)0xF : a) & ((b == 16) ? (byte)0xF : b));

                 // this worked when the data was packed, but now it's not...
                //icon.data[i] |= arg_4.data[i];
                //icon.data_ptr[i] &= arg_4.data_ptr[i];
            }
        }


        internal static void LoadPlayerCombatIcon(bool recolour) /* sub_47A90 */
        {
            seg042.set_game_area(1);

            Player player_ptr = gbl.player_ptr;

            char[] unk_16827 = new char[] { '\0', 'S', 'T' };

            ovr034.chead_cbody_comspr_icon(11, player_ptr.field_141, "CHEAD" + unk_16827[player_ptr.icon_size].ToString());
            ovr034.chead_cbody_comspr_icon(player_ptr.icon_id, player_ptr.field_142, "CBODY" + unk_16827[player_ptr.icon_size].ToString());

            MergeIcons(gbl.combat_icons[player_ptr.icon_id, 0], gbl.combat_icons[11, 0]);
            MergeIcons(gbl.combat_icons[player_ptr.icon_id, 1], gbl.combat_icons[11, 1]);

            if (recolour)
            {
                byte[] var_23 = new byte[16];
                byte[] var_13 = new byte[16];

                for (byte i = 0; i <= 15; i++)
                {
                    var_13[i] = i;
                    var_23[i] = i;
                }

                for (byte i = 0; i < 6; i++)
                {
                    var_23[gbl.unk_1A1D3[i]] = (byte)(player_ptr.field_145[i] & 0x0F);
                    var_23[gbl.unk_1A1D3[i] + 8] = (byte)((player_ptr.field_145[i] & 0xF0) >> 4);
                }

                seg040.DaxBlockRecolor(gbl.combat_icons[player_ptr.icon_id, 0], 0, 0, var_23, var_13);
                seg040.DaxBlockRecolor(gbl.combat_icons[player_ptr.icon_id, 1], 0, 0, var_23, var_13);
            }

            ovr034.free_icon(11);
            seg042.restore_game_area();
            seg043.clear_keyboard();
        }


        internal static void remove_player_file(Player arg_0)
        {
            string var_51 = gbl.byte_1BF1A + seg042.clean_string(arg_0.name);

            seg042.delete_file(var_51 + ".guy");
            seg042.delete_file(var_51 + ".swg");
            seg042.delete_file(var_51 + ".fx");
        }

        static Set unk_47D82 = new Set(0x0001, new byte[] { 0x05 });

        internal static void sub_47DFC(string arg_0, Player arg_4)
        {
            string var_2DE;
            string var_1DE;
            bool var_DE;
            short var_DD;
            short var_DB;
            Affect var_C1;
            Item var_BD;
            char var_B8;
            string var_B7;
            string var_B2;
            File var_89 = new File();
            string var_9;

            var_9 = arg_0;

            gbl.import_from = 0;

            do
            {
                if (save() == false)
                {
                    return;
                }

                var_DD = 0;

                var_DD += 0x1A6;

                var_BD = arg_4.itemsPtr;

                while (var_BD != null)
                {
                    var_DD += 0x3F;
                    var_BD = var_BD.next;
                }

                var_C1 = arg_4.affect_ptr;

                while (var_C1 != null)
                {
                    var_DD += 9;
                    var_C1 = var_C1.next;
                }

                var_B8 = 'O';

                if (var_DD > seg046.getDiskSpace((byte)(char.ToUpper(gbl.byte_1BF1A[0]) - 0x40)))
                {
                    seg041.displayAndDebug("Can't save.  No room on this disk.", 0, 14);

                    var_B8 = ovr027.displayInput(out var_DE, false, 0, 15, 10, 13, "Ok  Try another disk", "Lose character? ");
                }
            } while (var_B8 != 0x4F);

            if (var_9 == string.Empty)
            {
                var_B7 = ".guy";

                var_B2 = seg042.clean_string(arg_4.name);
            }
            else
            {
                var_B7 = ".sav";
                var_B2 = var_9;
            }


            do
            {
                var_1DE = gbl.byte_1BF1A + var_B2 + var_B7;
                var_89.Assign(var_1DE);
                seg051.Reset(1, var_89);

                var_DB = gbl.word_1EFBC;

                if (unk_47D82.MemberOf((char)var_DB) == false)
                {
                    var_2DE = "Unexpected error during save: " + var_DB.ToString();

                    seg041.displayAndDebug(var_2DE, 0, 14);

                    seg051.Close(var_89);
                    return;
                }

            } while (var_DB != 0 && var_DB != 2);


            seg051.Close(var_89);
            var_B8 = 'N';

            while (var_B8 == 'N' &&
                var_9.Length == 0 &&
                seg042.file_find(gbl.byte_1BF1A + var_B2 + var_B7) == true)
            {
                var_B8 = ovr027.yes_no(15, 10, 14, "Overwrite " + var_B2 + "? ");

                if (var_B8 == 'N')
                {
                    var_B2 = string.Empty;

                    while (var_B2 == string.Empty)
                    {
                        var_B2 = seg041.getUserInputString(8, 0, 10, "New file name: ");
                    }
                }
            }

            var_89.Assign(gbl.byte_1BF1A + var_B2 + var_B7);

            seg051.Rewrite(1, var_89);

            seg051.BlockWrite(0x1A6, arg_4.ToByteArray(), var_89);
            seg051.Close(var_89);

            seg042.delete_file(gbl.byte_1BF1A + var_B2 + ".swg");

            if (arg_4.itemsPtr != null)
            {

                var_89.Assign(gbl.byte_1BF1A + var_B2 + ".swg");
                seg051.Rewrite(1, var_89);
                var_BD = arg_4.itemsPtr;

                while (var_BD != null)
                {
                    seg051.BlockWrite(0x3F, var_BD.ToByteArray(), var_89);
                    var_BD = var_BD.next;
                }

                seg051.Close(var_89);
            }

            seg042.delete_file(gbl.byte_1BF1A + var_B2 + ".fx");

            if (arg_4.affect_ptr != null)
            {
                var_89.Assign(gbl.byte_1BF1A + var_B2 + ".fx");
                seg051.Rewrite(1, var_89);

                var_C1 = arg_4.affect_ptr;

                while (var_C1 != null)
                {
                    seg051.BlockWrite(9, var_C1.ToByteArray(), var_89);

                    var_C1 = var_C1.next;
                }

                seg051.Close(var_89);
            }
        }


        internal static bool sub_483AE(ref short bp_var_182, ref bool bp_var_1BC, string bp_var_1BB, string bp_var_2DF, string bp_var_2DA)
        {
            File var_BC;
            string var_3C;
            SearchRec var_2C;
            bool var_1;

            byte[] data = new byte[0x10];
            var_3C = string.Empty;

            seg046.FINDFIRST(out var_2C, 0, gbl.byte_1BF1A + "*" + bp_var_2DF);

            while (gbl.word_1EFBC == 0 &&
                var_3C != bp_var_2DA)
            {
                bp_var_1BC = seg042.find_and_open_file(out var_BC, 0, bp_var_1BB, gbl.byte_1BF1A + var_2C.fileName);

                seg051.Seek(0, var_BC);

                seg051.BlockRead(out bp_var_182, 0x10, data, var_BC);
                var_3C = Sys.ArrayToString(data, 0, 0x10);

                seg051.Close(var_BC);

                seg046.FINDNEXT(var_2C);
            }

            var_1 = (var_3C == bp_var_2DA);

            return var_1;
        }


        internal static void load_pool_rad_player(Player bp_arg_A, PoolRadPlayer bp_var_1C0)
        {
            /* nested function, arg_0 is BP */

            Player player;
            byte var_2;
            byte var_1;

            player = bp_arg_A;

            player.race = (Race)bp_var_1C0.race;
            player.sex = bp_var_1C0.sex;
            /*bp_var_1E8 = player.name;*/

            for (var_1 = 0; var_1 < 5; var_1++)
            {
                player.stats[var_1].tmp = bp_var_1C0.strength[var_1];

                switch ((Stat)var_1)
                {
                    case Stat.STR:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.sex]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//add	di, dx
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A298[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.tmp_str]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jnb	str_ok
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.sex]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//add	di, dx
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A298[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+charStruct.tmp_str], dl
                        throw new System.NotSupportedException();//str_ok:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.sex]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//add	di, dx
                        throw new System.NotSupportedException();//mov	dl, byte ptr max_str_val[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.tmp_str]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jbe	end_str
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.sex]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	dx, ax
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//add	di, dx
                        throw new System.NotSupportedException();//mov	dl, byte ptr max_str_val[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+charStruct.tmp_str], dl
                        throw new System.NotSupportedException();//end_str:
                        break;

                    case Stat.INT:
                        if (player.stats[var_1].tmp < ovr018.stru_1A298[(int)player.race].int_min)
                        {
                            player.stats[var_1].tmp = ovr018.stru_1A298[(int)player.race].int_min;
                        }

                        throw new System.NotSupportedException();//int_ok:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A29F[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.tmp_str]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jbe	end_int
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A29F[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+charStruct.tmp_str], dl
                        throw new System.NotSupportedException();//end_int:
                        break;

                    case Stat.WIS:
                        if (player.stats[var_1].tmp < ovr018.stru_1A298[(int)player.race].field_8)
                        {
                            player.stats[var_1].tmp = ovr018.stru_1A298[(int)player.race].field_8;
                        }

                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A2A1[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+10h]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jbe	end_wis
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+charStruct.race]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, byte ptr unk_1A2A1[di]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+10h], dl
                        throw new System.NotSupportedException();//end_wis:
                        break;

                    case Stat.DEX:
                        if (player.stats[var_1].tmp < ovr018.stru_1A298[(int)player.race].field_A)
                        {
                            player.stats[var_1].tmp = ovr018.stru_1A298[(int)player.race].field_A;
                        }

                        if (player.stats[var_1].tmp > ovr018.stru_1A298[(int)player.race].field_B)
                        {
                            player.stats[var_1].tmp = ovr018.stru_1A298[(int)player.race].field_B;
                        }
                        break;

                    case Stat.CON:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F94h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+10h]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jnb	loc_4880C
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F94h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+10h], dl
                        throw new System.NotSupportedException();//loc_4880C:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F95h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+10h]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jbe	loc_48852
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F95h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+10h], dl
                        throw new System.NotSupportedException();//loc_48852:
                        break;

                    case Stat.CHA:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F96h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+10h]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jnb	loc_488A2
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F96h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+10h], dl
                        throw new System.NotSupportedException();//loc_488A2:
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F97h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	al, es:[di+10h]
                        throw new System.NotSupportedException();//cmp	al, dl
                        throw new System.NotSupportedException();//jbe	cha_end
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//mov	al, es:[di+74h]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//mov	di, ax
                        throw new System.NotSupportedException();//mov	cl, 4
                        throw new System.NotSupportedException();//shl	di, cl
                        throw new System.NotSupportedException();//mov	dl, [di+3F97h]
                        throw new System.NotSupportedException();//mov	al, [bp+var_1]
                        throw new System.NotSupportedException();//cbw
                        throw new System.NotSupportedException();//shl	ax, 1
                        throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                        throw new System.NotSupportedException();//add	di, ax
                        throw new System.NotSupportedException();//mov	es:[di+10h], dl
                        throw new System.NotSupportedException();//cha_end:
                        break;
                }

                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	dl, es:[di+10h]
                throw new System.NotSupportedException();//mov	al, [bp+var_1]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//shl	ax, 1
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	es:[di+11h], dl
            }
            player.strength_18_100 = bp_var_1C0.strength_100;
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	al, es:[di+119h]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	al, es:[di+74h]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	cl, 4
            throw new System.NotSupportedException();//shl	di, cl
            throw new System.NotSupportedException();//add	di, dx
            throw new System.NotSupportedException();//mov	al, [di+3F8Ch]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//cmp	al, es:[di+10h]
            throw new System.NotSupportedException();//jbe	loc_48970
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	al, es:[di+119h]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	al, es:[di+74h]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//mov	cl, 4
            throw new System.NotSupportedException();//shl	di, cl
            throw new System.NotSupportedException();//add	di, dx
            throw new System.NotSupportedException();//mov	al, [di+3F8Ch]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+10h], al
            throw new System.NotSupportedException();//loc_48970:
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+16h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+1Dh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+2Dh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+73h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+2Fh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+75h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	ax, es:[di+30h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+76h], ax
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+32h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+78h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x33
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x79
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 0x38
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	byte ptr es:[di+9Ch], 0
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+6Bh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0DDh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+6Ch]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0DEh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x6D
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x0DF
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 5
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+72h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E4h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+73h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E5h], al
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	al, es:[di+0E5h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E6h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+74h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E7h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+75h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E8h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+76h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0E9h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x77
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x0EA
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 8
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+83h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0F6h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+84h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0F7h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+85h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0F8h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+86h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0F9h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+87h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+0FAh], al
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	short ptr es:[di+103h], 0x12C
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x96
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x109
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 8
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+9Fh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+11Ah], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0A0h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+11Bh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x0A1
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x11C
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x0A3
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x11E
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x0A5
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x120
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x0A7
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x122
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0A9h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+124h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0AAh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+125h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0ABh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+126h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	ax, es:[di+0ACh]
            throw new System.NotSupportedException();//mov	dx, es:[di+0AEh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+127h], ax
            throw new System.NotSupportedException();//mov	es:[di+129h], dx
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0B0h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+12Bh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0B1h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+12Ch], al
            for (var_2 = 1; var_2 <= 3; var_2++)
            {
                throw new System.NotSupportedException();//mov	al, [bp+var_2]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	di, [bp+arg_0]
                throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	dl, es:[di+0B1h]
                throw new System.NotSupportedException();//mov	al, [bp+var_2]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	es:[di+12Ch], dl
                throw new System.NotSupportedException();//mov	al, [bp+var_2]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	di, [bp+arg_0]
                throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	dl, es:[di+0B4h]
                throw new System.NotSupportedException();//mov	al, [bp+var_2]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	es:[di+136h], dl
            }
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	ax, es:[di+0B8h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+13Ch], ax
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0BAh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+13Eh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0BBh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+13Fh], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0BCh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+140h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0BDh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+141h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0BEh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+142h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0C0h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+144h], al

            System.Array.Copy(bp_var_1C0.field_C1, player.field_145, 6);

            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+0C7h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+14Ch], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x0CC
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x151
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 0x34
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+100h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+185h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+101h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+186h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	ax, es:[di+102h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+187h], ax
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+10Ch]
            throw new System.NotSupportedException();//mov	player.health_status, al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+10Dh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+196h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+10Eh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+197h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+110h]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+199h], al
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x111
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x19A
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x113
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x19C
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x115
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x19E
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x117
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x1A0
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//add	di, 0x119
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//add	di, 0x1A2
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ax, 2
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//call	Move(Any &,Any &,Word)
            throw new System.NotSupportedException();//mov	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, ss:[di-0x1C0]
            throw new System.NotSupportedException();//mov	al, es:[di+11Bh]
            throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
            throw new System.NotSupportedException();//mov	es:[di+1A4h], al

            player.initiative = (byte)bp_var_1C0.field_11C;
        }


        internal static void sub_48F35(HillsFarPlayer bp_var_1C4, Player bp_player_ptr, Player bp_var_1CA)
        {
            Player var_7;
            byte var_3;
            byte var_1;

            var_7 = bp_player_ptr;

            if (var_7.tmp_str < bp_var_1C4.field_14)
            {
                var_7.tmp_str = bp_var_1C4.field_14;
                var_7.strength = bp_var_1C4.field_14;
            }

            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	al, es:[di+1Ch]
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//cmp	al, es:[di+15h]
            throw new System.NotSupportedException();//jnb	loc_48FBE
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//mov	al, es:[di+15h]
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+1Ch], al
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//mov	al, es:[di+15h]
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+1Dh], al
            throw new System.NotSupportedException();//loc_48FBE:
            var_1 = 1;
            throw new System.NotSupportedException();//jmp	short loc_48FC7
            throw new System.NotSupportedException();//loc_48FC4:
            var_1++;
            throw new System.NotSupportedException();//loc_48FC7:
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//add	ax, 0x15
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	dl, es:[di]
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	al, es:[di+10h]
            throw new System.NotSupportedException();//cmp	al, dl
            throw new System.NotSupportedException();//jnb	loc_4902F
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//add	ax, 0x15
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	dl, es:[di]
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	es:[di+10h], dl
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	dl, es:[di+10h]
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	es:[di+11h], dl
            throw new System.NotSupportedException();//loc_4902F:
            throw new System.NotSupportedException();//cmp	[bp+var_1], 5
            throw new System.NotSupportedException();//jnz	loc_48FC4

            if (var_7.exp < bp_var_1C4.field_2E)
            {
                var_7.exp = bp_var_1C4.field_2E;
            }

            if (ovr020.getPlayerGold(bp_player_ptr) < bp_var_1C4.field_28)
            {
                for (var_3 = 0; var_3 <= 4; var_3++)
                {
                    ovr022.sub_59A19(var_3, bp_player_ptr.Money[var_3], bp_player_ptr);
                }
                ovr022.addPlayerGold((short)(bp_var_1C4.field_28 / 5));
            }

            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+0Ah]
            throw new System.NotSupportedException();//mov	ax, es:[di+76h]
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//cmp	ax, es:[di+1Eh]
            throw new System.NotSupportedException();//jnb	loc_49122
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//mov	ax, es:[di+1Eh]
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+0Ah]
            throw new System.NotSupportedException();//mov	es:[di+76h], ax
            throw new System.NotSupportedException();//loc_49122:
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+0B7h], 0
            throw new System.NotSupportedException();//mov	al, 0
            throw new System.NotSupportedException();//jbe	loc_49135
            throw new System.NotSupportedException();//inc	ax
            throw new System.NotSupportedException();//loc_49135:
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+109h], al
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+0B8h], 0
            throw new System.NotSupportedException();//mov	al, 0
            throw new System.NotSupportedException();//jbe	loc_49150
            throw new System.NotSupportedException();//inc	ax
            throw new System.NotSupportedException();//loc_49150:
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+10Eh], al
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+0B9h], 0
            throw new System.NotSupportedException();//mov	al, 0
            throw new System.NotSupportedException();//jbe	loc_4916B
            throw new System.NotSupportedException();//inc	ax
            throw new System.NotSupportedException();//loc_4916B:
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+10Bh], al

            var_7.thief_lvl = (bp_var_1C4.field_BA > 0) ? (byte)1 : (byte)0;

            var_7.field_E5 = 1;

            if (bp_var_1C4.field_26 != 0)
            {
                var_7.field_192 = 1;
            }

            gbl.area2_ptr.field_550 = 0xff;
            gbl.byte_1D8B0 = 0;
            gbl.byte_1B2F1 = 1;
            throw new System.NotSupportedException();//loc_491C4:
            ovr018.train_player();
            throw new System.NotSupportedException();//cmp	byte_1D8B0, 0
            throw new System.NotSupportedException();//jz	loc_491C4
            gbl.byte_1B2F1 = 0;
            gbl.player_ptr = bp_var_1CA;
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//les	di, ss:[di+var_1C4]
            throw new System.NotSupportedException();//mov	al, es:[di+21h]
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+78h], al
            throw new System.NotSupportedException();//mov	di, [bp+previous_bp]
            throw new System.NotSupportedException();//push	short ptr ss:[di+0Ch]
            throw new System.NotSupportedException();//push	short ptr ss:[di+0Ah]
            throw new System.NotSupportedException();//call	seg018.get_con_hp_adj(Player &)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	al, es:[di+78h]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//sub	ax, dx
            throw new System.NotSupportedException();//les	di, [bp+var_7]
            throw new System.NotSupportedException();//mov	es:[di+12Ch], al

            var_7.hit_point_current = bp_var_1C4.field_20;
        }


        static Set asc_49280 = new Set(0x020E, new byte[] { 
            0x04, 0x04, 0x00, 0x80, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x08, 0x00, 0x10	});

        static ClassId[] unk_1682A = {
            ClassId.unknown,    ClassId.thief,      ClassId.fighter,    ClassId.mc_f_t, ClassId.magic_user,
            ClassId.mc_mu_t,    ClassId.mc_f_mu,    ClassId.mc_f_mu_t,  ClassId.cleric, ClassId.mc_c_t,
            ClassId.mc_c_f,     ClassId.unknown,    ClassId.mc_c_mu,    ClassId.unknown, ClassId.mc_c_f_m, 
            ClassId.unknown};

        internal static void import_char01(byte arg_0, byte arg_2, ref Player player_ptr, string arg_8)
        {
            Player player01_ptr;
            string var_2DF;
            string var_2DA;
            string var_2CA;
            Player player02_ptr;
            HillsFarPlayer var_1C4;
            PoolRadPlayer var_1C0;
            bool var_1BC;
            string var_1BB = string.Empty;
            byte[] var_192;
            Affect affect_ptr = null;
            Item item_ptr;
            short var_182;
            File file;
            string var_100;

            var_100 = arg_8;

            var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_100);

            seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);


            if (gbl.import_from == 0)
            {
                var_192 = new byte[Player.StructSize];

                seg051.BlockRead(out var_182, 0x1a6, var_192, file);
                player_ptr = new Player(var_192, 0);

                var_192 = null;
                seg051.Close(file);
            }
            else if (gbl.import_from == 1)
            {
                byte[] data = new byte[0x11D];

                seg051.BlockRead(out var_182, 0x11D, data, file);
                seg051.Close(file);

                var_1C0 = new PoolRadPlayer(data);

                load_pool_rad_player(player_ptr, var_1C0);

                var_1C0 = null;
            }
            else if (gbl.import_from == 2)
            {
                byte[] data = new byte[0xBC];

                seg051.BlockRead(out var_182, 0xBC, data, file);
                seg051.Close(file);

                var_1C4 = new HillsFarPlayer(data);

                player_ptr.itemsPtr = null;
                player_ptr.affect_ptr = null;
                player_ptr.actions = null;
                player_ptr.next_player = null;

                var_2DA = var_1C4.field_4;

                var_2DF = ".guy";
                var_1BC = sub_483AE(ref var_182, ref var_1BC, var_1BB, var_2DF, var_2DA);

                if (var_1BC == true)
                {
                    var_2CA = var_100;
                    seg051.Delete(4, seg051.Pos(var_100, "."), ref var_2CA);

                    var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_2CA + var_2DF);

                    data = new byte[0x1A6];

                    seg051.BlockRead(out var_182, 0x1A6, data, file);
                    seg051.Close(file);

                    player_ptr = new Player(data, 0);

                    player_ptr.itemsPtr = null;
                    player_ptr.affect_ptr = null;
                    player_ptr.actions = null;
                    player_ptr.next_player = null;

                    player02_ptr = gbl.player_ptr;
                    gbl.player_ptr = player_ptr;

                    sub_48F35(var_1C4, player_ptr, player02_ptr);

                    if (var_1C4.field_1D > 0)
                    {
                        item_ptr = ovr025.new_Item(0, Affects.helpless, (Affects)var_1C4.field_1D, (short)(var_1C4.field_1D * 200),
                            0, 0, 0, 0, false, 0, 0, 0x57, -89, -88, 0x46);
                        ovr025.addItem(item_ptr, player_ptr);
                    }

                    if (var_1C4.field_23 > 0)
                    {
                        item_ptr = ovr025.new_Item(0, Affects.affect_41, (Affects)var_1C4.field_23, (short)(var_1C4.field_23 * 0x15E),
                            0, 1, 0, 0, false, 0, 1, 0x45, -89, -50, 0x4F);

                        ovr025.addItem(item_ptr, player_ptr);
                    }

                    if (var_1C4.field_86 > 0)
                    {
                        item_ptr = ovr025.new_Item(0, Affects.helpless, (Affects)var_1C4.field_86, (short)(var_1C4.field_86 * 0xc8),
                            0, 0, 0, 0, false, 0, 0, 0x42, -89, -88, 0x45);

                        ovr025.addItem(item_ptr, player_ptr);
                    }

                    if (var_1C4.field_87 > 0)
                    {
                        item_ptr = ovr025.new_Item(0, Affects.affect_3e, (Affects)var_1C4.field_87, (short)(var_1C4.field_87 * 0x190),
                            0, (short)(var_1C4.field_87 * 10), 0, 0, false, 0, 0, 0x40, -89, -71, 0x46);

                        ovr025.addItem(item_ptr, player_ptr);
                    }
                }
                else
                {
                    var_2DA = var_1C4.field_4;

                    var_2DF = ".cha";

                    var_1BC = sub_483AE(ref var_182, ref var_1BC, var_1BB, var_2DF, var_2DA);

                    if (var_1BC == true)
                    {
                        data = seg051.GetMem(0x11D);

                        var_2CA = var_100;
                        var_2CA = var_100;
                        seg051.Delete(4, seg051.Pos(var_100, "."), ref var_2CA);

                        var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_2CA + var_2DF);

                        seg051.BlockRead(out var_182, 0x011D, data, file);
                        seg051.Close(file);

                        var_1C0 = new PoolRadPlayer(data);

                        load_pool_rad_player(player_ptr, var_1C0);

                        var_1C0 = null;

                        player02_ptr = gbl.player_ptr;
                        gbl.player_ptr = player_ptr;

                        sub_48F35(var_1C4, player_ptr, player02_ptr);
                    }
                    else
                    {
                        player02_ptr = gbl.player_ptr;
                        gbl.player_ptr = player_ptr;

                        player01_ptr = player_ptr;

                        for (gbl.byte_1DA71 = 1; gbl.byte_1DA71 <= 6; gbl.byte_1DA71++)
                        {
                            int i = gbl.byte_1DA71;

                            player01_ptr.field_145[i] = (byte)(((gbl.unk_1A1D3[i] + 8) << 4) + gbl.unk_1A1D3[i]);
                        }

                        player01_ptr.field_124 = 0x32;
                        player01_ptr.field_73 = 0x28;
                        player01_ptr.health_status = Status.okey;
                        player01_ptr.in_combat = true;
                        player01_ptr.field_13F = 1;
                        player01_ptr.field_140 = 1;
                        player01_ptr.field_DE = 1;

                        player01_ptr.field_126 = seg051.Random(0xff);
                        player01_ptr.icon_id = 0x0A;

                        player01_ptr.field_11C = 2;
                        player01_ptr.field_11E = 1;
                        player01_ptr.field_120 = 2;
                        player01_ptr.field_125 = 1;
                        player01_ptr.field_E4 = 0x0C;

                        player01_ptr.name = var_1C4.field_4;
                        player01_ptr.tmp_str = var_1C4.field_14;
                        player01_ptr.strength = var_1C4.field_14;
                        player01_ptr.strength_18_100 = var_1C4.field_15;
                        player01_ptr.field_1D = var_1C4.field_15;

                        player01_ptr.stats[1].tmp = var_1C4.field_16;
                        player01_ptr.stats[1].max = var_1C4.field_16;

                        player01_ptr.stats[2].tmp = var_1C4.field_17;
                        player01_ptr.stats[2].max = var_1C4.field_17;
                        player01_ptr.stats[3].tmp = var_1C4.field_18;
                        player01_ptr.stats[3].max = var_1C4.field_18;
                        player01_ptr.stats[4].tmp = var_1C4.field_19;
                        player01_ptr.stats[4].max = var_1C4.field_19;
                        player01_ptr.stats[5].tmp = var_1C4.field_1A;
                        player01_ptr.stats[5].max = var_1C4.field_1A;

                        player01_ptr.race = (Race)(var_1C4.field_2D + 1);

                        if (player01_ptr.race == Race.half_orc)
                        {
                            player01_ptr.race = Race.human;
                        }


                        switch (player01_ptr.race)
                        {
                            case Race.halfling:
                                player01_ptr.icon_size = 1;
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player_ptr);
                                break;

                            case Race.dwarf:
                                player01_ptr.icon_size = 1;

                                ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player_ptr);
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_1a, player_ptr);
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_2f, player_ptr);
                                break;

                            case Race.gnome:
                                player01_ptr.icon_size = 1;

                                ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player_ptr);
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_12, player_ptr);
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_2f, player_ptr);
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_30, player_ptr);
                                break;

                            case Race.elf:
                                player01_ptr.icon_size = 2;
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_6b, player_ptr);
                                break;

                            case Race.half_elf:
                                player01_ptr.icon_size = 2;
                                ovr024.add_affect(false, 0xff, 0, Affects.affect_7c, player_ptr);
                                break;

                            default:
                                player01_ptr.icon_size = 2;
                                break;
                        }

                        player01_ptr._class = unk_1682A[var_1C4.field_35 & 0x0F];
                        player01_ptr.age = var_1C4.field_1E;

                        player01_ptr.cleric_lvl = (var_1C4.field_B7 > 0) ? (byte)1 : (byte)0;
                        player01_ptr.magic_user_lvl = (var_1C4.field_B8 > 0) ? (byte)1 : (byte)0;
                        player01_ptr.fighter_lvl = (var_1C4.field_B9 > 0) ? (byte)1 : (byte)0;
                        player01_ptr.thief_lvl = (var_1C4.field_BA > 0) ? (byte)1 : (byte)0;
                        player01_ptr.field_E5 = 1;
                        player01_ptr.sex = var_1C4.field_2C;
                        player01_ptr.alignment = var_1C4.field_1C;
                        player01_ptr.exp = var_1C4.field_2E;

                        if (player01_ptr.magic_user_lvl > 0)
                        {
                            player01_ptr.field_83 = 1;
                            player01_ptr.field_8A = 1;
                            player01_ptr.field_8B = 1;
                            player01_ptr.field_8D = 1;
                        }

                        gbl.area2_ptr.field_550 = 0xff;

                        gbl.byte_1D8B0 = 0;
                        gbl.byte_1B2F1 = 1;

                        do
                        {
                            ovr018.train_player();
                        } while (gbl.byte_1D8B0 == 0);

                        gbl.byte_1B2F1 = 0;

                        ovr022.addPlayerGold(300);
                        gbl.player_ptr = player02_ptr;
                        player01_ptr.hit_point_max = var_1C4.field_21;
                        player01_ptr.field_12C = (byte)(player01_ptr.hit_point_max - ovr018.get_con_hp_adj(player_ptr));
                        player01_ptr.hit_point_current = var_1C4.field_20;
                    }
                }

                seg051.FreeMem(0x00bc, var_1C4);
            }

            if (gbl.import_from == 0)
            {
                seg051.Delete(4, seg051.Pos(var_100, "."), ref var_100);
            }
            else
            {
                var_100 = seg042.clean_string(player_ptr.name);
            }

            if (seg042.file_find(gbl.byte_1BF1A + var_100 + ".swg") == true)
            {
                byte[] var_18A = new byte[Item.StructSize];

                var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_100 + ".swg");

                do
                {
                    seg051.BlockRead(out var_182, Item.StructSize, var_18A, file);

                    if (var_182 == Item.StructSize)
                    {
                        item_ptr = new Item(var_18A, 0);

                        if (player_ptr.itemsPtr == null)
                        {
                            player_ptr.itemsPtr = item_ptr;
                        }
                        else
                        {

                            item_ptr.next = player_ptr.itemsPtr;
                            player_ptr.itemsPtr = item_ptr;
                        }
                    }
                } while (var_182 == Item.StructSize);

                seg051.Close(file);
                seg051.FreeMem(Item.StructSize, var_18A);
            }


            if (seg042.file_find(gbl.byte_1BF1A + var_100 + ".fx") == true)
            {
                var_192 = seg051.GetMem(9);
                var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_100 + ".fx");

                do
                {
                    seg051.BlockRead(out var_182, Affect.StructSize, var_192, file);

                    if (var_182 == Affect.StructSize)
                    {
                        affect_ptr = new Affect(var_192, 0);

                        if (player_ptr.affect_ptr == null)
                        {
                            player_ptr.affect_ptr = new Affect();
                            affect_ptr = player_ptr.affect_ptr;
                        }
                        else
                        {
                            affect_ptr.next = new Affect();
                            affect_ptr = affect_ptr.next;
                        }
                    }
                } while (var_182 == Affect.StructSize);

                seg051.Close(file);
                seg051.FreeMem(Affect.StructSize, var_192);
            }

            if (gbl.import_from == 1)
            {
                if (seg042.file_find(gbl.byte_1BF1A + var_100 + ".spc") == true)
                {
                    var_192 = seg051.GetMem(9);

                    var_1BC = seg042.find_and_open_file(out file, 0, var_1BB, gbl.byte_1BF1A + var_100 + ".spc");

                    do
                    {
                        seg051.BlockRead(out var_182, 9, var_192, file);

                        if (var_182 == 9 &&
                            asc_49280.MemberOf(var_192[0]) == true)
                        {
                            Affect tmpAffect = new Affect(var_192, 0);

                            if (player_ptr.affect_ptr == null)
                            {
                                player_ptr.affect_ptr = tmpAffect;
                                affect_ptr = player_ptr.affect_ptr;
                            }
                            else
                            {
                                affect_ptr.next = tmpAffect;
                                affect_ptr = affect_ptr.next;
                            }
                        }
                    } while (var_182 == 9);

                    seg051.Close(file);

                    var_192 = null;
                }
            }
            seg043.clear_keyboard();
            ovr025.sub_66C20(player_ptr);
            ovr026.sub_6A3C6(player_ptr);
        }


        internal static void load_mob(out Player player, byte monster_id)
        {
            string var_12;
            short var_8;
            byte[] var_6;
            short var_2;

            var_12 = gbl.game_area.ToString();
            seg042.load_decode_dax(out var_6, out var_2, monster_id, "MON" + var_12 + "CHA.dax");

            if (var_2 == 0)
            {
                seg041.displayAndDebug("Unable to load monster", 0, 15);
                seg043.print_and_exit();
            }

            player = new Player(var_6, 0);

            seg051.FreeMem(var_6);

            gbl.byte_1C01A = 1;

            seg042.load_decode_dax(out var_6, out var_2, monster_id, "MON" + var_12 + "SPC.dax");

            if (var_2 != 0)
            {
                var_8 = 0;

                Affect lastAffect = null;

                do
                {
                    Affect affect = new Affect(var_6, var_8);

                    if (var_8 == 0)
                    {
                        player.affect_ptr = affect;
                        lastAffect = affect;
                    }
                    else
                    {
                        lastAffect.next = affect;
                        lastAffect = affect;
                    }

                    var_8 += 9;
                } while (var_8 < var_2);

                seg051.FreeMem(var_2, var_6);
            }

            seg042.load_decode_dax(out var_6, out var_2, monster_id, "MON" + var_12 + "ITM.dax");

            if (var_2 != 0)
            {
                var_8 = 0;

                Item lastItem = null;

                do
                {
                    Item item = new Item(var_6, var_8);

                    if (var_8 == 0)
                    {
                        player.itemsPtr = item;
                    }
                    else
                    {
                        lastItem.next = item;
                    }
                    lastItem = item;

                    var_8 += 0x3F;

                } while (var_8 < var_2);
            }

            gbl.byte_1C01A = 0;
            seg043.clear_keyboard();
        }


        internal static void sub_4A57D(byte arg_0)
        {
            Player var_8;

            if (gbl.area2_ptr.field_67C <= 7)
            {
                load_mob(out var_8, arg_0);

                var_8.field_126 = arg_0;

                sub_4A60A(var_8);

                ovr034.chead_cbody_comspr_icon(var_8.icon_id, arg_0, "CPIC");
            }
        }

        static Set unk_4A5EA = new Set(0x0001, new byte[] { 0xFF });

        internal static void sub_4A60A(Player player_ptr)
        {
            Player var_10;
            byte[] var_C;
            Player player;

            player_ptr.icon_id = 0xff;

            if (gbl.player_next_ptr == null)
            {
                gbl.player_next_ptr = player_ptr;
            }
            else
            {
                player = gbl.player_next_ptr;

                while (player.next_player != null)
                {
                    player = player.next_player;
                }

                player.next_player = player_ptr;
            }

            gbl.player_ptr = player_ptr;

            var_C = new byte[8];

            player = gbl.player_next_ptr;

            while (player != null)
            {
                if (unk_4A5EA.MemberOf(player.icon_id) == true)
                {
                    var_C[player.icon_id] = 1;
                }

                player = player.next_player;
            }

            var_10 = gbl.player_ptr;
            var_10.icon_id = 0;

            while (var_10.icon_id < 8 &&
                var_C[var_10.icon_id] != 0)
            {
                var_10.icon_id += 1;
            }

            gbl.area2_ptr.field_67C++;

            if (player_ptr.field_F7 > 0x7f)
            {
                ovr026.sub_6A3C6(player_ptr);
            }
        }

        static Set asc_4A761 = new Set(0x0802, new byte[] { 0xFE, 0x07 });

        internal static void loadGame()
        {
            string var_511;
            string var_311;

            byte var_212;
            string var_211;
            bool var_1FC;
            char var_1FB;
            bool var_1FA;
            byte var_1F9;
            string var_1F6;
            Player var_1CD;
            byte var_1C9;
            File var_1C8;
            string[] var_148;

            gbl.import_from = 0;

            if (save() == true)
            {
                var_211 = string.Empty;

                for (var_1FB = 'A'; var_1FB <= 'J'; var_1FB++)
                {
                    var_311 = gbl.byte_1BF1A + "savgam" + var_1FB.ToString() + ".dat";

                    if (seg042.file_find(var_311) == true)
                    {
                        var_211 += var_1FB.ToString() + " ";
                    }
                }

                if (var_211.Length != 0)
                {
                    seg051.Delete(1, var_211.Length - 1, ref var_211);

                    bool v;
                    do
                    {
                        var_1FB = ovr027.displayInput(out var_1FC, false, 0, 15, 10, 13, var_211, "Load Which Game: ");

                        v = false;
                        if (asc_4A761.MemberOf(var_1FB) == true)
                        {
                            var_311 = gbl.byte_1BF1A + "savgam" + var_1FB.ToString() + ".dat";
                            v = seg042.file_find(var_311);
                        }
                    } while (v == false && var_1FB != 0);

                    if (var_1FB != 0)
                    {
                        var_311 = gbl.byte_1BF1A + "savgam" + var_1FB.ToString() + ".dat";
                        var_511 = "Put save disk in " + gbl.byte_1BF1A;

                        var_1FA = seg042.find_and_open_file(out    var_1C8, 1, var_511, var_311);
                        ovr027.redraw_screen();
                        seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);
                        gbl.byte_1B2EB = 1;

                        byte[] data = new byte[0x2000];

                        seg051.BlockRead(1, data, var_1C8);
                        gbl.game_area = data[0];

                        seg051.BlockRead(0x800, data, var_1C8);
                        gbl.area_ptr = new Area1(data, 0);

                        seg051.BlockRead(0x800, data, var_1C8);
                        gbl.area2_ptr = new Area2(data, 0);

                        seg051.BlockRead(0x400, data, var_1C8);
                        gbl.stru_1B2CA = new Struct_1B2CA(data, 0);

                        seg051.BlockRead(0x1E00, data, var_1C8);
                        gbl.ecl_ptr = new EclBlock(data, 0);

                        seg051.BlockRead(5, data, var_1C8);
                        gbl.mapPosX = (sbyte)data[0];
                        gbl.mapPosY = (sbyte)data[1];
                        gbl.mapDirection = data[2];
                        gbl.byte_1D53C = data[3];
                        gbl.byte_1D53D = data[4];

                        seg051.BlockRead(1, data, var_1C8);
                        gbl.last_game_state = data[0];

                        seg051.BlockRead(1, data, var_1C8);
                        gbl.game_state = data[0];

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D53E = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D540 = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D542 = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D544 = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D546 = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(2, data, var_1C8);
                        gbl.word_1D548 = Sys.ArrayToShort(data, 0);

                        seg051.BlockRead(1, data, var_1C8);
                        var_1F9 = data[0];

                        seg051.BlockRead(0x148, data, var_1C8);
                        var_148 = Sys.ArrayToStrings(data, 0, System.Math.Min(0x148, 0x29*var_1F9), 0x29);

                        seg051.Close(var_1C8);

                        gbl.PicsOn = ((gbl.area_ptr.pics_on >> 1) != 0);
                        gbl.AnimationsOn = ((gbl.area_ptr.pics_on & 1) != 0);
                        gbl.game_speed_var = gbl.area_ptr.game_speed;
                        gbl.area2_ptr.field_67C = 0;
                        var_212 = var_1F9;

                        for (var_1C9 = 0; var_1C9 < var_212; var_1C9++)
                        {
                            var_1F6 = seg042.clean_string(var_148[var_1C9]);

                            if (seg042.file_find(gbl.byte_1BF1A + var_1F6 + ".sav") == true)
                            {
                                var_1CD = new Player();

                                import_char01(1, 1, ref var_1CD, var_1F6 + ".sav");
                                sub_4A60A(var_1CD);

                                if (save() == false)
                                {
                                    return;
                                }
                            }
                        }

                        var_1CD = gbl.player_next_ptr;

                        while (var_1CD != null)
                        {
                            remove_player_file(var_1CD);
                            var_1CD = var_1CD.next_player;
                        }

                        gbl.player_ptr = gbl.player_next_ptr;

                        while (gbl.player_ptr != null)
                        {
                            if (gbl.player_ptr.field_F7 < 0x80)
                            {
                                LoadPlayerCombatIcon(true);
                            }
                            else
                            {
                                ovr034.chead_cbody_comspr_icon(gbl.player_ptr.icon_id, gbl.player_ptr.field_126, "CPIC");
                            }

                            gbl.player_ptr = gbl.player_ptr.next_player;
                        }

                        gbl.player_ptr = gbl.player_next_ptr;

                        gbl.game_area = gbl.area2_ptr.game_area;

                        if (gbl.area_ptr.field_1CC != 0)
                        {
                            if (gbl.game_state != 0)
                            {
                                if (gbl.word_1D53E > 0)
                                {
                                    ovr031.Load3DMap(gbl.area_ptr.field_18A);
                                }

                                if (gbl.word_1D53E > 0)
                                {
                                    ovr031.LoadWalldef((byte)gbl.word_1D540, (byte)gbl.word_1D53E);
                                }
                                if (gbl.word_1D542 > 0)
                                {
                                    ovr031.LoadWalldef((byte)gbl.word_1D544, (byte)gbl.word_1D542);
                                }
                                if (gbl.word_1D546 > 0)
                                {
                                    ovr031.LoadWalldef((byte)gbl.word_1D548, (byte)gbl.word_1D546);
                                }
                            }
                        }
                        else
                        {
                            ovr030.bigpic(0x79);
                        }

                        seg043.clear_keyboard();
                        ovr027.redraw_screen();

                        gbl.last_game_state = gbl.game_state;

                        gbl.game_state = 0;
                    }
                }
            }
        }


        static short save_space_required()
        {
            short var_10;
            Affect var_E;
            Item var_A;
            Player var_6;

            var_10 = 0;
            var_6 = gbl.player_next_ptr;

            while (var_6 != null)
            {
                var_10 += 0x1A6;

                var_A = var_6.itemsPtr;

                while (var_A != null)
                {
                    var_10 += 0x3F;

                    var_A = var_A.next;
                }

                var_E = var_6.affect_ptr;

                while (var_E != null)
                {
                    var_10 += 9;

                    var_E = var_E.next;
                }

                var_6 = var_6.next_player;
            }

            return var_10;
        }

        static Set unk_4AEA0 = new Set (0x000a , new byte[ ] { 0x01, 0x00, 0x00, 0x00 , 0x00 , 0x00 , 0x00 , 0x00, 0xFE, 0x07 });
        static Set unk_4AEEF = new Set(0x0003, new byte[] { 0x05, 0x00, 0x04 });

        internal static void SaveGame()
        {
            short var_1FE;
            short var_1FC;
            char var_1FA;
            bool var_1F9;
            string var_1CF;
            Player player_ptr;
            byte var_1C9;
            File save_file = new File();
            string[] var_171 = new string[9];

            do
            {
                var_1FA = ovr027.displayInput(out var_1F9, (gbl.game_state == 2), 0, 15, 10, 13, "A B C D E F G H I J", "Save Which Game: ");

            } while (unk_4AEA0.MemberOf(var_1FA) == false);

            if (var_1FA != '\0')
            {
                gbl.import_from = 0;

                if (save() == true)
                {
                    if (seg042.file_find(gbl.byte_1BF1A + "savgam" + var_1FA + ".dat") == true)
                    {
                        var_1FE = 0;
                    }
                    else
                    {
                        var_1FE = 0x3208;
                    }

                    var_1FE = save_space_required();

                    if (var_1FE > seg046.getDiskSpace((byte)(char.ToUpper(gbl.byte_1BF1A[0]) - 0x40)))
                    {
                        seg041.displayAndDebug("Can't save.  No room on this disk.", 0, 14);
                        return;
                    }

                    do
                    {
                        save_file.Assign(gbl.byte_1BF1A + "savgam" + var_1FA + ".dat");
                        seg051.Rewrite(1, save_file);
                        var_1FC = gbl.word_1EFBC;

                        if (unk_4AEEF.MemberOf((char)var_1FC) == false)
                        {
                            seg041.displayAndDebug("Unexpected error during save: " + var_1FC.ToString(), 0, 14);
                            seg051.Close(save_file);
                            return;
                        }
                    } while (unk_4AEEF.MemberOf((char)var_1FC) == false);

                    ovr027.redraw_screen();
                    seg041.displayString("Saving...Please Wait", 0, 10, 0x18, 0);

                    gbl.area_ptr.game_speed = gbl.game_speed_var;
                    gbl.area_ptr.pics_on = (byte)(((gbl.PicsOn) ? 0x02 : 0) | ((gbl.AnimationsOn) ? 0x01 : 0));
                    gbl.area2_ptr.game_area = gbl.game_area;

                    byte[] data = new byte[0x1E00];

                    data[0] = gbl.game_area;
                    seg051.BlockWrite(1, data, save_file);

                    seg051.BlockWrite(0x800, gbl.area_ptr.ToByteArray(), save_file);
                    seg051.BlockWrite(0x800, gbl.area2_ptr.ToByteArray(), save_file);
                    seg051.BlockWrite(0x400, gbl.stru_1B2CA.ToByteArray(), save_file);
                    seg051.BlockWrite(0x1E00, gbl.ecl_ptr.ToByteArray(), save_file);

                    data[0] = (byte)gbl.mapPosX;
                    data[1] = (byte)gbl.mapPosY;
                    data[2] = gbl.mapDirection;
                    data[3] = gbl.byte_1D53C;
                    data[4] = gbl.byte_1D53D;
                    seg051.BlockWrite(5, data, save_file);

                    data[0] = gbl.last_game_state;
                    seg051.BlockWrite(1, data, save_file);
                    data[0] = gbl.game_state;
                    seg051.BlockWrite(1, data, save_file);

                    Sys.ShortToArray(gbl.word_1D53E, data, 0);
                    Sys.ShortToArray(gbl.word_1D540, data, 2);

                    Sys.ShortToArray(gbl.word_1D542, data, 4);
                    Sys.ShortToArray(gbl.word_1D544, data, 6);

                    Sys.ShortToArray(gbl.word_1D546, data, 8);
                    Sys.ShortToArray(gbl.word_1D548, data, 10);

                    seg051.BlockWrite(12, data, save_file);

                    var_1C9 = 0;
                    player_ptr = gbl.player_next_ptr;

                    while (player_ptr != null)
                    {
                        var_1C9++;
                        seg051.Str(1, out var_1CF, 0, var_1C9);

                        var_171[var_1C9 - 1] = "CHRDAT" + var_1FA + var_1CF;

                        player_ptr = player_ptr.next_player;
                    }

                    data[0] = var_1C9;
                    seg051.BlockWrite(1, data, save_file);

                    /*todo seg051.BlockWrite(0x148, ref var_171, save_file);*/
                    seg051.Close(save_file);
                    player_ptr = gbl.player_next_ptr;
                    var_1C9 = 0;

                    while (player_ptr != null)
                    {
                        var_1C9++;
                        seg051.Str(1, out var_1CF, 0, var_1C9);

                        sub_47DFC("CHRDAT" + var_1FA + var_1CF, player_ptr);
                        remove_player_file(player_ptr);
                        player_ptr = player_ptr.next_player;
                    }

                    gbl.byte_1C01B = 1;
                    ovr027.redraw_screen();
                }
            }
        }
    }
}
