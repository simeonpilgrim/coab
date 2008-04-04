using Classes;

namespace engine
{
    class ovr034
    {
        internal static void Load24x24Set(byte cellCount, byte destCellOffset, byte block_id, string fileName)
        {
            if (destCellOffset > 0x30)
            {
                seg051.Write(0, "Start range error in Load24x24Set", gbl.known01_02);
                seg051.WriteLn(gbl.known01_02);
                seg043.print_and_exit();
            }

            DaxBlock tmp_block = null;
            seg040.load_dax(ref tmp_block, 0, 0, block_id, fileName);

            int dateLength = cellCount * tmp_block.bpp;
            int destByteOffset = destCellOffset * tmp_block.bpp;

            if (gbl.dax24x24Set != null)
            {
                for (int i = 0; i < dateLength; i++)
                {
                    gbl.dax24x24Set.data[destByteOffset + i] = tmp_block.data[i];
                }
            }

            seg040.free_dax_block(ref tmp_block);
            seg043.clear_keyboard();
        }


        internal static void draw_iso_title(int arg_0, int titleIndex, int rowY, int colX) /* sub_760F7 */
        {
            if (titleIndex > 0x7f)
            {
                seg040.OverlayUnbounded(gbl.dword_1C8FC, titleIndex, titleIndex & 0x7F, rowY, colX);
            }
            else
            {
                seg040.OverlayUnbounded(gbl.dax24x24Set, arg_0, titleIndex, rowY, colX);
            }
        }


        internal static void free_icon(byte arg_0)
        {
            if (gbl.combat_icons[arg_0, 0] != null)
            {
                seg040.free_dax_block(ref gbl.combat_icons[arg_0, 0]);
            }

            if (gbl.combat_icons[arg_0, 1] != null)
            {
                seg040.free_dax_block(ref gbl.combat_icons[arg_0, 1]);
            }
        }


        internal static void chead_cbody_comspr_icon(byte arg_0, byte arg_2, string arg_4)
        {
            string var_100;

            var_100 = arg_4;

            string sub = seg051.Copy(5, 0, var_100);
            if (sub == "CHEAD" ||
                sub == "CBODY")
            {
                if (seg051.UpCase(var_100[var_100.Length - 1]) == 'T')
                {
                    arg_2 += 0x40;
                }

                var_100 = seg051.Copy(var_100.Length - 1, 0, var_100);

                seg040.load_dax(ref gbl.combat_icons[arg_0, 0], 0, 1, arg_2, var_100);
                seg040.load_dax(ref gbl.combat_icons[arg_0, 1], 0, 1, (byte)(arg_2 + 0x80), var_100);
            }
            else if (var_100 == "COMSPR" || var_100 == "ICON")
            {
                seg040.load_dax(ref gbl.combat_icons[arg_0, 0], 0, 1, arg_2, var_100);
                seg040.load_dax(ref gbl.combat_icons[arg_0, 1], 0, 1, (byte)(arg_2 + 0x80), var_100);

                if (var_100 == "ICON")
                {
                    seg040.DaxBlockRecolor(gbl.combat_icons[arg_0, 0], 0, 0, gbl.unk_16E50, gbl.unk_16E30);
                    seg040.DaxBlockRecolor(gbl.combat_icons[arg_0, 1], 0, 0, gbl.unk_16E50, gbl.unk_16E30);
                }
            }
            else
            {
                var_100 += gbl.game_area.ToString();

                seg040.load_dax(ref gbl.combat_icons[arg_0, 0], 0, 1, arg_2, var_100);
                seg040.DaxBlockRecolor(gbl.combat_icons[arg_0, 0], 0, 0, gbl.unk_16E40, gbl.unk_16E30);

                seg040.load_dax(ref gbl.combat_icons[arg_0, 1], 0, 1, (byte)(arg_2 + 0x80), var_100);
                seg040.DaxBlockRecolor(gbl.combat_icons[arg_0, 1], 0, 0, gbl.unk_16E40, gbl.unk_16E30);
            }

            seg043.clear_keyboard();
        }


        internal static void draw_combat_icon(int iconIndex, int iconState, int direction, int tileY, int tileX) /* sub_76504 */
        {
            DaxBlock icon = gbl.combat_icons[iconIndex, iconState];

            if (icon != null)
            {
                if (direction > 3)
                {
                    DaxBlock flipped;
                    seg040.init_dax_block(out flipped, 1, 1, icon.width, icon.height); // was 3 and 0x18
                    seg040.flipIconLeftToRight(flipped, icon);
                    icon = flipped;
                }

                seg040.draw_combat_picture(icon, (tileY * 3) + 1, (tileX * 3) + 1, 0);
            }
        }
    }
}
