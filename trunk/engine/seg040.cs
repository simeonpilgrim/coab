using Classes;

namespace engine
{
    class seg040
    {
        internal static byte get_mask_bits(uint longint, byte mask_color)
        {
            byte loop_var;
            byte var_4;
            byte mask_high;
            byte var_2;

            var_2 = 0;
            mask_high = (byte)(mask_color << 4);
            var_4 = 0x80;

            for (loop_var = 0; loop_var <= 3; loop_var++)
            {
                if (((longint >> (8 * loop_var)) & 0xf0) == mask_high)
                {
                    var_2 += var_4;
                }

                var_4 >>= 1;

                if (((longint >> (8 * loop_var)) & 0x0f) == mask_color)
                {
                    var_2 += var_4;
                }

                var_4 >>= 1;
            }

            return var_2;
        }


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

        static byte[] ega_color_channel = { 1, 2, 4, 8 };

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


        internal static void OverlayUnbounded(DaxBlock dest, DaxBlock source, int arg_8, int itemIdex, int rowY, int colX)
        {
            bool var_E;

            var_E = ((arg_8 & 1) != 0 && source.data_ptr != null);

            if (var_E == true)
            {
                OverlayMergeUnbounded(dest, source, itemIdex, rowY, colX);
            }
            else
            {
                OverlayCopyUnbounded(dest, source, itemIdex, rowY, colX);
            }
        }


        internal static void sub_E353(DaxBlock dest, DaxBlock source, byte arg_8, int itemIndex, int rowY, int colX)
        {
            int backupOffset;
            bool doBackup;
            int sourcePrefix;
            int sourcePostfix;
            int destPrefix;
            int destPostfix;
            int copySize;
            int sourceOffset;
            int destOffset;
            int lineToCopy;
            int lineNo;
            int var_6;

            if (colX < 0)
            {
                sourcePrefix = System.Math.Abs(colX);
                destPrefix = 0;
            }
            else
            {
                sourcePrefix = 0;
                destPrefix = colX;
            }

            if (dest.width < (colX + source.width))
            {
                sourcePostfix = (colX + source.width) - dest.width;
                destPostfix = 0;
            }
            else
            {
                sourcePostfix = 0;
                destPostfix = dest.width - colX - source.width;
            }

            int posY = System.Math.Abs(rowY) * 8;

            if (rowY < 0)
            {
                lineToCopy = source.height - posY;
                sourceOffset = posY * (source.width * 8);
                destOffset = 0;
                lineNo = 0;
            }
            else
            {
                sourceOffset = 0;
                lineNo = posY;
                destOffset = lineNo * (dest.width * 8);

                if ((lineNo + source.height) > dest.height)
                {
                    lineToCopy = dest.height - lineNo;
                }
                else
                {
                    lineToCopy = source.height;
                }
            }

            if (source.width < sourcePrefix ||
                source.width < sourcePostfix ||
                sourceOffset > source.bpp ||
                destOffset > dest.bpp ||
                lineToCopy > source.height)
            {
                return;
            }

            copySize = (source.width - sourcePrefix - sourcePostfix) * 8;
            //sourcePrefix *= 8;
            //sourcePostfix *= 8;
            //destPostfix *= 8;
            //destPrefix *= 8;
            var_6 = destPrefix;

            backupOffset = sourceOffset;
            sourceOffset += source.bpp * itemIndex;

            if (arg_8 == 1)
            {
                DaxBlockSubMerge(lineNo, lineToCopy, dest, source, null, source.data_ptr, var_6,
                    destOffset, sourceOffset, copySize, destPostfix, destPrefix,
                    sourcePostfix, sourcePrefix, false, backupOffset);

                return;
            }

            doBackup = false;

            if (arg_8 != 4)
            {
                doBackup = true;
                //throw new System.ApplicationException("if this happens, the null below needs to be re-thought");
            }

            DaxBlockSubMerge(lineNo, lineToCopy, dest, source, null, source.data_ptr, var_6,
                destOffset, sourceOffset, copySize, destPostfix, destPrefix,
                sourcePostfix, sourcePrefix, doBackup, backupOffset);
            return;
        }


        internal static byte bits_flip(byte arg_0)
        {
            byte var_2;

            var_2 = (byte)((arg_0 & 0x01) << 7);
            var_2 += (byte)((arg_0 & 0x02) << 5);
            var_2 += (byte)((arg_0 & 0x04) << 3);
            var_2 += (byte)((arg_0 & 0x08) << 1);
            var_2 += (byte)((arg_0 & 0x10) >> 1);
            var_2 += (byte)((arg_0 & 0x20) >> 3);
            var_2 += (byte)((arg_0 & 0x40) >> 5);
            var_2 += (byte)((arg_0 & 0x80) >> 7);

            return var_2;
        }


        internal static void merge_icon(DaxBlock arg_0, DaxBlock arg_4)
        {
            short var_17;
            short var_15;
            DaxBlock var_13;
            byte var_D;
            short var_C;
            short var_A;
            short var_8;
            short var_6;
            short var_4;
            short var_2;

            if (arg_4 != null && arg_0 != null)
            {
                var_13 = arg_4;

                seg051.Move(8, arg_0.field_9, var_13.field_9);

                var_8 = (short)(var_13.width << 2);
                var_4 = (short)(var_8 - 4);
                var_2 = 0;
                var_15 = var_13.height;

                for (var_C = 1; var_C <= var_15; var_C++)
                {
                    var_6 = var_2;
                    var_17 = var_13.width;

                    for (var_A = 1; var_A <= var_17; var_A++)
                    {
                        for (var_D = 0; var_D <= 3; var_D++)
                        {
                            arg_0.data[var_D + var_2] = bits_flip(var_13.data[var_4 - var_2 + var_6 + var_D]);

                            if (var_13.data_ptr != null)
                            {
                                arg_0.data_ptr[var_D + var_2] = bits_flip(var_13.data_ptr[var_4 - var_2 + var_6 + var_D]);
                            }
                        }

                        var_2 += 4;
                    }

                    var_4 += var_8;
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


        internal static void draw_picture(DaxBlock dax_block, int rowY, int colX)
        {
            DaxBlock dax_block_ptr;
            int var_10;
            int maxY;
            int maxX;
            int minY;
            int minX;

            if (dax_block != null)
            {
                dax_block_ptr = dax_block;

                var_10 = 0;

                minY = rowY * 8;
                maxY = minY + dax_block_ptr.height - 1;

                minX = colX;
                maxX = minX + dax_block_ptr.width;

                minX *= 8;
                maxX *= 8;
                maxX -= 1;

                for (int pixY = minY; pixY <= maxY; pixY++)
                {
                    for (int pixX = minX; pixX <= maxX; pixX++)
                    {
                        if (pixX < 320 && pixY < 200)
                        {
                            Display.SetPixel3(pixX, pixY, dax_block.data[var_10]);
                        }

                        var_10++;
                    }
                }
                Display.Update();
            }
        }
        
        static int backcolor = 0;

        internal static void DrawOverlay()
        {
            backcolor = (backcolor+1)%8;

            for (int line = 0; line < 0x0A8; line++)
            {
                if (gbl.overlayLineFlag[line] == true)
                {
                    int xPos = gbl.overlayLineXStart[line];

                    int dataPosStart = gbl.overlayLineDataStart[line];
                    int dataPosEnd = gbl.overlayLineDataEnd[line];

                    for (int dataPos = dataPosStart; dataPos <= dataPosEnd; dataPos++)
                    {
                        if (xPos+8 < 320)
                        {
                            byte c = gbl.overlayLines.data[dataPos];
                            if (c == 0) c = (byte)backcolor;
                            Display.SetPixel3(xPos + 8, line + 8, c);
                        }

                        xPos += 1;
                    }
                }
            }

            Display.Update();

            for (int i = 0; i < 0xA8; i++)
            {
                gbl.overlayLineFlag[i] = false;
                gbl.overlayLineDataStart[i] = int.MaxValue;
                gbl.overlayLineDataEnd[i] = 0;
            }

            System.Array.Clear(gbl.overlayLines.data, 0, gbl.overlayLines.data.Length);
        }

        internal static void SetPaletteColor(byte color, byte index)
        {
            byte newColor = color;

            if (color >= 8)
            {
                newColor += 8;
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


        internal static void sub_F6F7(DaxBlock arg_0, byte[] arg_4, byte mask, ushort arg_A, ushort width, short posY, short posX)
        {
            int lineNo;
            int dataPos;
            int lineCount;
            int lineWidth;
            int dataPostfix;
            int lineXPrefix;
            int widthOverrun;
            int dataXOffset;

            if (arg_0 == null)
            {
                return;
            }

            if (posX < 0)
            {
                dataXOffset = System.Math.Abs(posX);
                lineXPrefix = 0;
            }
            else
            {
                dataXOffset = 0;
                lineXPrefix = posX;
            }

            if (arg_0.width < (posX + width))
            {
                widthOverrun = (posX + width) - arg_0.width;
                dataPostfix = 0;
            }
            else
            {
                widthOverrun = 0;
                dataPostfix = arg_0.width - (posX + width);
            }

            if (posY < 0)
            {
                dataPos = 0;
                lineNo = 0;
                lineCount = arg_A - System.Math.Abs(posY);
            }
            else
            {
                lineNo = posY;
                dataPos = lineNo * arg_0.width;
                lineCount = arg_A - 1;

                if (arg_0.height < (arg_A + posY))
                {
                    lineCount = (arg_0.height - 1) - posY;
                }
            }

            lineWidth = (short)(width - dataXOffset - widthOverrun) * 8;

            lineXPrefix *= 8;
            dataPostfix *= 8;
            dataPos *= 8;

            //for (int y = 0; y <= lineCount; y++)
            //{
            //    gbl.overlayLineFlag[lineNo] = true;
            //    dataPos += lineXPrefix;

            //    if (gbl.overlayLineDataStart[lineNo] > dataPos)
            //    {
            //        gbl.overlayLineDataStart[lineNo] = dataPos;
            //        gbl.overlayLineXStart[lineNo] = lineXPrefix;
            //    }

            //    for (int x = 0; x < lineWidth; x++)
            //    {
            //        arg_0.data[dataPos] = mask;
            //        dataPos += 1;
            //    }

            //    if ((dataPos - 1) > gbl.overlayLineDataEnd[lineNo])
            //    {
            //        gbl.overlayLineDataEnd[lineNo] = dataPos - 1;
            //    }

            //    dataPos += dataPostfix;
            //    lineNo++;
            //}
        }


        static void OverlayCopyUnbounded(DaxBlock dest, DaxBlock source, int itemIndex, int rowY, int colX)
        {
            int linePrefix;
            int linePostfix;
            int copySize;
            int sourceOffset;
            int destOffset;
            int var_6;


            int lineNo = rowY * 8;
            destOffset = lineNo * dest.width * 8;
            linePrefix = colX * 8;

            linePostfix = (dest.width - source.width - colX) * 8;
            var_6 = linePrefix;

            copySize = (short)(source.width * 4);
            int linesToCopy = source.height;

            sourceOffset = source.bpp * itemIndex;

            DaxBlockSubCopy(lineNo, linesToCopy, dest, source, var_6, destOffset, sourceOffset,
                copySize, linePostfix, linePrefix);
        }


        static void OverlayMergeUnbounded(DaxBlock dest, DaxBlock source, int itemIdex, int rowY, int colX)
        {
            int lineNo = rowY * 8;

            int sourceOffset = source.bpp * itemIdex;
            int destOffset = lineNo * dest.width * 8;

            int destPrefix = colX * 8;
            int destPostfix = (dest.width - source.width - colX) * 8;

            int sourcePrefix = 0;
            int sourcePostfix = 0;

            int var_6 = destPrefix;

            int copySize = source.width * 8;
            int linesToCopy = source.height;

            bool doBackup = false;
            int backupOffset = 0; /* not used because var_1A is false */

            DaxBlockSubMerge(lineNo, linesToCopy, dest, source, null, source.data_ptr, var_6,
                destOffset, sourceOffset, copySize,
                destPostfix, destPrefix, sourcePostfix, sourcePrefix,
                doBackup, backupOffset);
            return;
        }

        static void DaxBlockSubCopy(int lineNo, int linesToCopy, DaxBlock dest, DaxBlock source, int bp_var_6,
            int destOffset, int sourceOffset, int copySize, int linePostfix, int linePrefix)
        {
            for (int i = 0; i < linesToCopy; i++)
            {
                gbl.overlayLineFlag[lineNo] = true;

                destOffset += linePrefix;

                if (destOffset <= gbl.overlayLineDataStart[lineNo])
                {
                    gbl.overlayLineDataStart[lineNo] = destOffset;
                    gbl.overlayLineXStart[lineNo] = bp_var_6;
                }

                System.Array.Copy(source.data, sourceOffset, dest.data, destOffset, copySize);

                sourceOffset += copySize;
                destOffset += copySize;

                if ((destOffset - 1) > gbl.overlayLineDataEnd[lineNo])
                {
                    gbl.overlayLineDataEnd[lineNo] = destOffset - 1;
                    int width = gbl.overlayLineDataEnd[lineNo] - gbl.overlayLineDataStart[lineNo];
                }

                destOffset += linePostfix;
                lineNo += 1;
            }

            //draw_picture(source, lineNo / 8, bp_var_6+1);
        }


        static void DaxBlockSubMerge(int lineNo, int linesToCopy, DaxBlock dest, DaxBlock source, DaxBlock backup,
            byte[] source_data_ptr, int bp_var_6, int destOffset, int sourceOffset, int copySize, int destPostfix,
            int destPrefix, int sourcePostfix, int sourcePrefix, bool doBackup, int backupOffset)
        {
            //do
            //{
            //    gbl.overlayLineFlag[lineNo] = true;

            //    sourceOffset += sourcePrefix;
            //    destOffset += destPrefix;

            //    if (destOffset <= gbl.overlayLineDataStart[lineNo])
            //    {
            //        gbl.overlayLineDataStart[lineNo] = destOffset;
            //        gbl.overlayLineXStart[lineNo] = bp_var_6;
            //    }

            //    for (int i = 0; i < copySize; i++)
            //    {
            //        if (doBackup == true)
            //        {
            //            if (backup != null)
            //            {
            //                backup.data[backupOffset] = dest.data[destOffset];
            //            }
            //        }

            //        //if (dest.data[destOffset] == 16 ||
            //        //    source_data_ptr[sourceOffset] == 16 ||
            //        //    source.data[sourceOffset] == 16)
            //        //{
            //        //}

            //        if (source.data[sourceOffset] == 16)
            //        {
            //            //dest.data[destOffset] = leave it alone;
            //        }
            //        else
            //        {
            //            dest.data[destOffset] = source.data[sourceOffset];
            //        }

            //        //dest.data[destOffset] &= source_data_ptr[sourceOffset];
            //        //dest.data[destOffset] |= source.data[sourceOffset];

            //        sourceOffset += 1;
            //        backupOffset += 1;
            //        destOffset += 1;
            //    }

            //    if ((destOffset - 1) > gbl.overlayLineDataEnd[lineNo])
            //    {
            //        gbl.overlayLineDataEnd[lineNo] = destOffset - 1;
            //        int width = gbl.overlayLineDataEnd[lineNo] - gbl.overlayLineDataStart[lineNo];

            //    }

            //    destOffset += destPostfix;
            //    sourceOffset += sourcePostfix;
            //    backupOffset += sourcePostfix;

            //    lineNo += 1;
            //} while ((--linesToCopy) != 0);

            draw_picture(source, lineNo / 8, bp_var_6+1);
        }
    }
}
