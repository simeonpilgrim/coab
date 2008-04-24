using Classes;

namespace engine
{
    class seg040
    {
        internal static void load_dax(ref DaxBlock mem_ptr, byte mask_colour, byte masked, byte block_id, string fileName)
        {
            DaxBlock dax_ptr;
            byte[] pic_data;
            short pic_data_offset;
            short pic_size;
            byte[] field_9 = new byte[8];
            byte item_count;
            short field_6;
            short field_4;
            short width;
            short height;
            string file_string;

            file_string = fileName;

            if (mem_ptr != null)
            {
                seg040.free_dax_block(ref mem_ptr);
            }

            seg042.load_decode_dax(out pic_data, out pic_size, block_id, file_string + ".dax");

            if (pic_size != 0)
            {
                height = Sys.ArrayToShort(pic_data, 0);

                width = Sys.ArrayToShort(pic_data, 2);
                field_4 = Sys.ArrayToShort(pic_data, 4);
                field_6 = Sys.ArrayToShort(pic_data, 6);
                item_count = pic_data[8];
                System.Array.Copy(pic_data, 9, field_9, 0, 8);

                init_dax_block(out mem_ptr, masked, item_count, width, height);

                dax_ptr = mem_ptr;
                dax_ptr.field_4 = field_4;
                dax_ptr.field_6 = field_6;
                dax_ptr.field_9 = field_9;

                pic_data_offset = 17;

                turn_dax_to_videolayout(mem_ptr, mask_colour, masked, pic_data_offset, pic_data);

                seg051.FreeMem(pic_size, pic_data);
                seg043.clear_keyboard();
            }
        }

        internal static void turn_dax_to_videolayout(DaxBlock dest_dax_block, byte mask_colour, byte masked, short block_offset, byte[] data)
        {
            short width;
            short height;
            byte item_count;
            DaxBlock dax_ptr;
            int dest_offset;

            /* Todo, this function needs to account for masked colours */

            if (dest_dax_block != null)
            {
                dax_ptr = dest_dax_block;
                dest_offset = 0;

                item_count = dax_ptr.item_count;

                for (int loop1_var = 1; loop1_var <= item_count; loop1_var++)
                {
                    height = (short)(dax_ptr.height - 1);

                    for (int loop2_var = 0; loop2_var <= height; loop2_var++)
                    {
                        width = (short)((dax_ptr.width * 4) - 1);

                        for (int loop3_var = 0; loop3_var <= width; loop3_var++)
                        {
                            byte a = (byte)((data[block_offset]) >> 4);
                            byte b = (byte)((data[block_offset]) & 0x0f);

                            if (masked != 0 && a == mask_colour)
                            {
                                dax_ptr.data[dest_offset] = 16;
                            }
                            else
                            {
                                dax_ptr.data[dest_offset] = a;
                            }
                            dest_offset += 1;

                            if (masked != 0 && b == mask_colour)
                            {
                                dax_ptr.data[dest_offset] = 16;
                            }
                            else
                            {
                                dax_ptr.data[dest_offset] = b;
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


        internal static void OverlayBounded(DaxBlock source, byte arg_8, int itemIndex, int rowY, int colX) /* sub_E353 */
        {
            draw_combat_picture(source, rowY + 1, colX + 1, itemIndex);
        }


        internal static void flipIconLeftToRight(DaxBlock dest, DaxBlock source)
        {
            if (source != null && dest != null)
            {
                seg051.Move(8, dest.field_9, source.field_9);
                int width = source.width * 8;
                for( int y = 0; y < source.height; y++ )
                {
                    for (int x = 0; x < width; x++)
                    {
                        int di = (y * width) + x;
                        int si = (y*width) + (width - x) - 1;
                        byte[] dd = dest.data;
                        dest.data[di] = source.data[si];
                        //dest.data[di+1] = source.data[si+0];

                        if (source.data_ptr != null)
                        {
                            dest.data_ptr[di] = source.data_ptr[si];
                            //dest.data_ptr[di+1] = source.data_ptr[si+0];
                        }
                    }
                }
            }
        }


        internal static void ega_01(DaxBlock arg_0, short arg_4, short arg_6)
        {
            DaxBlock var_15;
            byte var_11;
            short var_10;
            short var_C;
            short var_A;
            short var_8;
            short var_4;
            short var_2;

            if (arg_0 != null)
            {
                var_15 = arg_0;

                var_10 = 0;

                var_8 = (short)(arg_4 << 3);
                var_C = (short)(var_8 + var_15.height - 1);
                var_A = (short)(arg_6 + var_15.width - 1);

                throw new System.NotSupportedException();//mov	al, 5
                throw new System.NotSupportedException();//mov	dx, 0x3CE
                throw new System.NotSupportedException();//out	dx, al

                throw new System.NotSupportedException();//mov	al, 0
                throw new System.NotSupportedException();//mov	dx, 0x3CF
                throw new System.NotSupportedException();//out	dx, al

                throw new System.NotSupportedException();//mov	al, 4
                throw new System.NotSupportedException();//mov	dx, 0x3CE
                throw new System.NotSupportedException();//out	dx, al

                for (var_4 = var_8; var_4 <= var_C; var_4++)
                {
                    for (var_2 = arg_6; var_2 <= var_A; var_2++)
                    {
                        for (var_11 = 0; var_11 <= 3; var_11++)
                        {
                            throw new System.NotSupportedException();//mov	al, [bp+var_11]
                            throw new System.NotSupportedException();//mov	dx, 0x3CF
                            throw new System.NotSupportedException();//out	dx, al
                            throw new System.NotSupportedException();//mov	ax, 0x0A000
                            throw new System.NotSupportedException();//push	ax
                            throw new System.NotSupportedException();//mov	ax, [bp+var_2]
                            throw new System.NotSupportedException();//cwd
                            throw new System.NotSupportedException();//mov	cx, ax
                            throw new System.NotSupportedException();//mov	bx, dx
                            throw new System.NotSupportedException();//mov	di, [bp+var_4]
                            throw new System.NotSupportedException();//shl	di, 1
                            throw new System.NotSupportedException();//mov	ax, short ptr unk_1BBF2[di]
                            throw new System.NotSupportedException();//xor	dx, dx
                            throw new System.NotSupportedException();//add	ax, cx
                            throw new System.NotSupportedException();//adc	dx, bx
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//pop	es
                            throw new System.NotSupportedException();//mov	dl, es:[di]
                            throw new System.NotSupportedException();//mov	ax, [bp+var_10]
                            throw new System.NotSupportedException();//les	di, [bp+arg_0]
                            throw new System.NotSupportedException();//add	di, ax
                            throw new System.NotSupportedException();//mov	es:[di+17h], dl
                            var_10++;
                        }
                    }
                }

                throw new System.NotSupportedException();//mov	al, 0
                throw new System.NotSupportedException();//mov	dx, 0x3CF
                throw new System.NotSupportedException();//out	dx, al

                throw new System.NotSupportedException();//mov	al, 5
                throw new System.NotSupportedException();//mov	dx, 0x3CE
                throw new System.NotSupportedException();//out	dx, al

                throw new System.NotSupportedException();//mov	al, 0
                throw new System.NotSupportedException();//mov	dx, 0x3CF
                throw new System.NotSupportedException();//out	dx, al
            }
        }



        internal static void draw_clipped_picture(DaxBlock dax_block, int rowY, int colX, int index, 
            int clipMinX, int clipMaxX, int clipMinY, int clipMaxY)
        {
            if (dax_block != null)
            {
                int var_10 = index * dax_block.bpp;

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
                            Display.SetPixel3(pixX, pixY, dax_block.data[var_10]);
                        }

                        var_10++;
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
        
        //static int backcolor = 0;

        internal static void DrawOverlay()
        {
            //TODO remove this.
        }

        internal static void SetPaletteColor(byte color, byte index)
        {
            byte newColor = color;

            if (color >= 8)
            {
                //newColor += 8;
            }

            Display.SetEgaPalette(index, newColor);
        }

        internal static void DaxBlockRecolor(DaxBlock dax_block, byte arg_4, short arg_6, byte[] newColors, byte[] oldColors)
        {
            byte var_3F;
            DaxBlock var_3A;
            byte var_36;
            byte[] var_2E = new byte[4];
            byte var_2A;
            short posY;
            short posX;
            byte var_25;
            short var_24;
            short var_22;

            if (dax_block != null)
            {
                if (arg_6 < 0)
                {
                    arg_6 = 0;
                    var_25 = dax_block.item_count;
                }
                else
                {
                    var_25 = 1;
                }

                var_22 = (short)(dax_block.bpp * arg_6);
                var_24 = 0;
                init_dax_block(out var_3A, 0, var_25, dax_block.width, dax_block.height);

                System.Array.Copy(dax_block.field_9, var_3A.field_9, dax_block.field_9.Length);

                System.Array.Copy(dax_block.data, var_22, var_3A.data, var_24, var_3A.item_count * var_3A.bpp);

                for (var_36 = 0; var_36 <= 15; var_36++)
                {
                    if (arg_6 < 0)
                    {
                        arg_6 = 0;
                        var_25 = dax_block.item_count;
                    }
                    else
                    {
                        var_25 = 1;
                    }

                    var_22 = (short)(dax_block.bpp * arg_6);
                    var_24 = 0;

                    if (oldColors[var_36] != newColors[var_36])
                    {
                        var_3F = (byte)(var_25 + arg_6 - 1);

                        for (int block = arg_6; block <= var_3F; block++)
                        {
                            for (posY = 1; posY <= var_3A.height; posY++)
                            {
                                for (posX = 1; posX <= var_3A.width * 8; posX++)
                                {
                                    if (arg_4 != 0)
                                    {
                                        //Todo with this random fade stuff
                                        var_2A = (byte)(seg051.Random(0x100) & seg051.Random(0x100));
                                    }
                                    else
                                    {
                                        var_2A = 0x0FF;
                                    }


                                    if (dax_block.data[var_22] == oldColors[var_36])
                                    {
                                        var_3A.data[var_24] = newColors[var_36];
                                    }


                                    var_22 += 1;
                                    var_24 += 1;
                                }
                            }
                        }
                    }
                }

                System.Array.Copy(var_3A.data, 0, dax_block.data, dax_block.bpp * arg_6, var_25 * dax_block.bpp);
                seg040.free_dax_block(ref var_3A);
            }
        }


        internal static void init_dax_block(out DaxBlock out_buff, byte masked, byte item_count, short width, short height)
        {
            ushort ram_size;
            short bpp;

            bpp = (short)(height * width * 8);
            ram_size = (ushort)(item_count * bpp);

            out_buff = new DaxBlock(ram_size);

            seg051.FillChar(0, ram_size, out_buff.data);

            out_buff.height = height;
            out_buff.width = width;
            out_buff.bpp = bpp;
            out_buff.item_count = item_count;

            if ((masked & 1) != 0)
            {
                out_buff.data_ptr = new byte[ram_size];

                seg051.FillChar(0, ram_size, out_buff.data_ptr);
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
