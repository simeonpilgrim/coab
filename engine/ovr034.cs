using Classes;
using Logging;

namespace engine
{
    class ovr034
    {
        internal static void Load24x24Set(int cellCount, int destCellOffset, int block_id, string fileName)
        {
            if (destCellOffset > 0x30)
            {
                Logger.LogAndExit("Start range error in Load24x24Set. {0}", destCellOffset);
            }

            DaxBlock tmp_block = seg040.LoadDax(0, 0, block_id, fileName);

            int dateLength = cellCount * tmp_block.bpp;
            int destByteOffset = destCellOffset * tmp_block.bpp;

            if (gbl.dax24x24Set != null)
            {
                System.Array.Copy(tmp_block.data, 0, gbl.dax24x24Set.data, destByteOffset, dateLength);
            }

            seg043.clear_keyboard();
        }


        internal static void DrawIsoTile(int tileIndex, int rowY, int colX) /* sub_760F7 */
        {
            if (tileIndex > 0x7f)
            {
                seg040.OverlayUnbounded(gbl.dword_1C8FC, tileIndex, tileIndex & 0x7F, rowY, colX);
            }
            else
            {
                seg040.OverlayUnbounded(gbl.dax24x24Set, 0, tileIndex, rowY, colX);
            }
        }


        internal static void ReleaseCombatIcon(int index) // free_icon
        {
            gbl.combat_icons[index, 0] = null;
            gbl.combat_icons[index, 1] = null;
        }


        internal static void chead_cbody_comspr_icon(byte combat_icon_index, int block_id, string fileText)
        {
            string file_text = fileText;

            string sub = seg051.Copy(5, 0, file_text);
            if (sub == "CHEAD" ||
                sub == "CBODY")
            {
                if (char.ToUpper(file_text[file_text.Length - 1]) == 'T')
                {
                    block_id += 0x40;
                }

                file_text = seg051.Copy(file_text.Length - 1, 0, file_text);

                gbl.combat_icons[combat_icon_index, 0] = seg040.LoadDax(0, 1, block_id, file_text);
                gbl.combat_icons[combat_icon_index, 1] = seg040.LoadDax(0, 1, block_id + 0x80, file_text);
            }
            else if (file_text == "COMSPR" || file_text == "ICON")
            {
                gbl.combat_icons[combat_icon_index, 0] = seg040.LoadDax(0, 1, block_id, file_text);
                gbl.combat_icons[combat_icon_index, 1] = seg040.LoadDax(0, 1, block_id + 0x80, file_text);

                if (file_text == "ICON")
                {
                    seg040.DaxBlockRecolor(gbl.combat_icons[combat_icon_index, 0], false, gbl.unk_16E50, gbl.unk_16E30);
                    seg040.DaxBlockRecolor(gbl.combat_icons[combat_icon_index, 1], false, gbl.unk_16E50, gbl.unk_16E30);
                }
            }
            else
            {
                file_text += gbl.game_area.ToString();

                gbl.combat_icons[combat_icon_index, 0] = seg040.LoadDax(0, 1, block_id, file_text);
                seg040.DaxBlockRecolor(gbl.combat_icons[combat_icon_index, 0], false, gbl.unk_16E40, gbl.unk_16E30);

                gbl.combat_icons[combat_icon_index, 1] = seg040.LoadDax(0, 1, block_id + 0x80, file_text);
                seg040.DaxBlockRecolor(gbl.combat_icons[combat_icon_index, 1], false, gbl.unk_16E40, gbl.unk_16E30);
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
                    seg040.FlipIconLeftToRight(icon);
                }

                seg040.draw_combat_picture(icon, (tileY * 3) + 1, (tileX * 3) + 1, 0);
            }
        }
    }
}
