using Classes;

namespace engine
{
    class seg040
    {
        internal static DaxBlock LoadDax(byte mask_colour, byte masked, int block_id, string fileName) // load_dax
        {
            short pic_size;
            byte[] pic_data;
            seg042.load_decode_dax(out pic_data, out pic_size, block_id, fileName + ".dax");

            if (pic_size != 0)
            {
                int height = Sys.ArrayToShort(pic_data, 0);
                int width = Sys.ArrayToShort(pic_data, 2);
                int x_pos = Sys.ArrayToShort(pic_data, 4);
                int y_pos = Sys.ArrayToShort(pic_data, 6);
                int item_count = pic_data[8];

                DaxBlock mem_ptr = new DaxBlock(masked, item_count, width, height);
                System.Array.Copy(pic_data, 9, mem_ptr.field_9, 0, 8);

                int pic_data_offset = 17;

                turn_dax_to_videolayout(mem_ptr, mask_colour, masked, pic_data_offset, pic_data);

                seg043.clear_keyboard();

                return mem_ptr;
            }

            return null;
        }

        internal static void turn_dax_to_videolayout(DaxBlock dax_block, byte mask_colour, byte masked, int block_offset, byte[] data)
        {
            /* TODO, this function needs to account for masked colours */

            if (dax_block != null)
            {
                int dest_offset = 0;

                for (int loop1_var = 1; loop1_var <= dax_block.item_count; loop1_var++)
                {
                    int height = dax_block.height - 1;

                    for (int loop2_var = 0; loop2_var <= height; loop2_var++)
                    {
                        int width = (dax_block.width * 4) - 1;

                        for (int loop3_var = 0; loop3_var <= width; loop3_var++)
                        {
                            byte a = (byte)((data[block_offset]) >> 4);
                            byte b = (byte)((data[block_offset]) & 0x0f);

                            if (masked == 1 && a == mask_colour)
                            //if (masked != 0 && a == mask_colour)
                            {
                                dax_block.data[dest_offset] = 16;
                            }
                            else
                            {
                                dax_block.data[dest_offset] = a;
                            }
                            dest_offset += 1;

                            if (masked == 1 && b == mask_colour)
                            //if (masked != 0 && b == mask_colour)
                            {
                                dax_block.data[dest_offset] = 16;
                            }
                            else
                            {
                                dax_block.data[dest_offset] = b;
                            }

                            dest_offset += 1;
                            block_offset += 1;
                        }
                    }
                }
            }
        }


        internal static void OverlayUnbounded(DaxBlock source, int arg_8, int itemIdex, int rowY, int colX)
        {
            draw_combat_picture(source, rowY + 1, colX + 1, itemIdex);
        }


        internal static void OverlayBounded(DaxBlock source, int arg_8, int itemIndex, int rowY, int colX) /* sub_E353 */
        {
            draw_combat_picture(source, rowY + 1, colX + 1, itemIndex);
        }


        internal static void FlipIconLeftToRight(DaxBlock source)
        {
            if (source != null)
            {
                byte[] data = new byte[source.data.Length];
                byte[] dataPtr = new byte[source.data.Length];

                int width = source.width * 8;
                for (int y = 0; y < source.height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int di = (y * width) + x;
                        int si = (y * width) + (width - x) - 1;

                        data[di] = source.data[si];

                        if (source.data_ptr != null)
                        {
                            dataPtr[di] = source.data_ptr[si];
                        }
                    }
                }

                System.Array.Copy(data, source.data, source.data.Length);
                if (source.data_ptr != null)
                {
                    System.Array.Copy(dataPtr, source.data_ptr, source.data.Length);
                }
            }
        }


        internal static void ega_backup(DaxBlock dax_block, int rowY, int colX) /* ega_01 */
        {
            if (dax_block != null)
            {
                int offset = 0;

                int minY = rowY * 8;
                int maxY = minY + dax_block.height;

                int minX = colX * 8;
                int maxX = minX + (dax_block.width * 8);

                for (int pixY = minY; pixY < maxY; pixY++)
                {
                    for (int pixX = minX; pixX < maxX; pixX++)
                    {
                        dax_block.data[offset] = Display.GetPixel(pixX, pixY);
                        offset++;
                    }
                }
            }
        }

        static int color_no_draw = 17;
        static int color_re_color_from = 17;
        static int color_re_color_to = 17;

        internal static void draw_clipped_recolor(int from, int to)
        {
            color_re_color_from = from;
            color_re_color_to = to;
        }

        internal static void draw_clipped_nodraw(int color)
        {
            color_no_draw = color;
        }

        internal static void draw_clipped_picture(DaxBlock dax_block, int rowY, int colX, int index, 
            int clipMinX, int clipMaxX, int clipMinY, int clipMaxY)
        {
            if (dax_block != null)
            {
                int offset = index * dax_block.bpp;

                int minY = rowY * 8;
                int maxY = minY + dax_block.height;

                int minX = colX * 8;
                int maxX = minX + (dax_block.width * 8);

                for (int pixY = minY; pixY < maxY; pixY++)
                {
                    for (int pixX = minX; pixX < maxX; pixX++)
                    {
                        if (pixX >= clipMinX && pixX < clipMaxX && 
                            pixY >= clipMinY && pixY < clipMaxY)
                        {
                            byte color = dax_block.data[offset];

                            if (color == color_no_draw)
                            { }
                            else if (color == color_re_color_from)
                            {
                                Display.SetPixel3(pixX, pixY, color_re_color_to);
                            }
                            else
                            {
                                Display.SetPixel3(pixX, pixY, color);
                            }
                        }

                        offset++;
                    }
                }

                Display.Update();
            }
        }

        internal static void draw_combat_picture(DaxBlock dax_block, int rowY, int colX, int index)
        {
            draw_clipped_picture(dax_block, rowY, colX, index, 8, 176, 8, 176);
        }

        internal static void draw_picture(DaxBlock dax_block, int rowY, int colX, int index)
        {
            draw_clipped_picture(dax_block, rowY, colX, index, 0, 320, 0, 200);
        }
        
        internal static void DrawOverlay()
        {
            //TODO this might be useful when we move to OpenGL.
        }

        internal static void SetPaletteColor(int color, int index)
        {
            int newColor = color;

            //if (color >= 8)
            //{
            //  newColor += 8;
            //}

            Display.SetEgaPalette(index, newColor);
        }

        internal static void DaxBlockRecolor(DaxBlock block, bool useRandom, byte[] newColors, byte[] oldColors)
        {
            if (block != null)
            {
                for (int colorIdx = 0; colorIdx < 16; colorIdx++)
                {
                    if (oldColors[colorIdx] != newColors[colorIdx])
                    {
                        int srcOffset = 0;
                        int destOffset = 0;

                        for (int posY = 0; posY < block.height; posY++)
                        {
                            for (int posX = 0; posX < (block.width * 8); posX++)
                            {
                                if (block.data[srcOffset] == oldColors[colorIdx] &&
                                    (useRandom == false || (seg051.Random(4) == 0)))
                                {
                                    block.data[destOffset] = newColors[colorIdx];
                                }

                                srcOffset += 1;
                                destOffset += 1;
                            }
                        }
                    }
                }
            }
        }

        internal static void free_dax_block(ref DaxBlock block_ptr)
        {
            if (block_ptr != null)
            {
                int var_2 = block_ptr.item_count * block_ptr.bpp;

                if (block_ptr.data_ptr != null)
                {
                    seg051.FreeMem(var_2, block_ptr.data_ptr);
                }

                seg051.FreeMem(var_2 + 0x17, block_ptr);

                block_ptr = null;
            }
        }


        internal static void DrawColorBlock(int color, int lineCount, int colWidth, int lineY, int colX)
        {
            int minY = lineY + 8;
            int maxY = minY + lineCount;

            int minX = (colX * 8) + 8;
            int maxX = minX + (colWidth * 8);

            for (int pixY = minY; pixY < maxY; pixY++)
            {
                for (int pixX = minX; pixX < maxX; pixX++)
                {
                    if (pixX >= 0 && pixX < 320 && pixY >= 0 && pixY < 200)
                    {
                        Display.SetPixel3(pixX, pixY, color);
                    }
                }
            }
        }
    }
}
