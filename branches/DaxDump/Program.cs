using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DaxDump
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = System.IO.Directory.GetCurrentDirectory();
            //string path = @"C:\games\DARKNESS";
            string path = @"C:\games\TREASURE";
            //string path = @"C:\games\gateway";
            //string path = @"C:\games\coab";
            //string path = @"C:\games\secret";
            //string path = @"C:\games\deathkrynn";
            //string path = @"C:\games\nwn";
            //string path = @"c:\games\buckmatrix";
            //string path = @"c:\games\buckcount";

            foreach (var filea in Directory.GetFiles(path, "wall*.dax"))
            {
                TryDump(filea);
            }

            List<Bitmap[]> tiles = new List<Bitmap[]>();

            //foreach (var w8x8 in Directory.GetFiles(path, "8x8d1*.dax"))
            //{
            //    foreach (var block in GetAllBlocks(w8x8))
            //    {
            //        tiles.Add(DecodeEGA8x8(block.data));
            //    }
            //}

            //foreach (var wd in Directory.GetFiles(path, "walldef2.dax"))
            //{
            //    foreach (var block in GetAllBlocks(wd))
            //    {
            //        string block_name = string.Format("{0}_{1:000}", Path.Combine(Path.GetDirectoryName(block.file), Path.GetFileNameWithoutExtension(block.file)), block.id);

            //        DecodeEGAWall(block.data, block_name, tiles);
            //    }
            //}

            //TryDump(@"C:\games\DARKNESS\bigpic1.dax");


            //string file = @"C:\games\DARKNESS\cpic1_025.bin";
            //string file = @"C:\games\DARKNESS\SKYGRND_001.bin";
            //string file = @"C:\games\DARKNESS\title_001.bin";

            //Console.ReadKey();
        }

        static int[] MonoBitMask = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
        static uint[] egaColors = { 0XFF000000, 0XFF0000AD, 0XFF00AD00, 0XFF00ADAD, 0XFFAD0000, 0XFFAD00AD, 0XFFAD5200, 0XFFADADAD, 
                                      0XFF525252, 0XFF5252FF, 0XFF52FF52, 0XFF52FFFF, 0XFFFF5252, 0XFFFF52FF, 0XFFFFFF52, 0XFFFFFFFF };


        static void TryDump(string file)
        {
            Console.WriteLine(file);
            foreach (var block in GetAllBlocks(file))
            {
                int b = block.data[0];

                //byte[] data = new byte[block.data.Length - 5];
                //System.Array.Copy(block.data, 5, data, 0, block.data.Length - 5);
                var data = block.data;

                Console.WriteLine("File: {0} Block: {1} Size: {2}", block.file, block.id, data.Length);

                string block_name = string.Format("{0}_{1:000}", Path.Combine(Path.GetDirectoryName(block.file), Path.GetFileNameWithoutExtension(block.file)), block.id);

                DumpBin(data, block_name);         

                //TryMono(data, block_name);

                TryEGA(data, block_name);

                //TryEGASprite(data, block_name);

                //TryVGAPic(data, block_name);


                TryStrataVGA(data, block_name);

                TryStrataBlocksVGA(data, block_name);

                TryVGA(data, block_name);

                TryVGASprite(data, block_name );
            }
        }

 

        private static void DumpBin(byte[] data, string block_name)
        {
            string bin_name = block_name + ".bin";

            using (BinaryWriter binWriter = new BinaryWriter(File.Open(bin_name, FileMode.Create)))
            {
                binWriter.Write(data);
            }
        }

        static int dataOffset = 54;
        static int rows = 8;
        static int cols = 7;

        private static void DecodeEGAWall(byte[] data, string block_name, List<Bitmap[]> tiles)
        {
            for (int tileidx = 0; tileidx < tiles.Count; tileidx++)
            {
                var tile = tiles[tileidx];
                if (tile.Length == 0) continue;

                for (int chunk = 0; chunk < (data.Length / 156); chunk++)
                {
                    var bitmap = new Bitmap(cols * 8, rows * 8, PixelFormat.Format16bppArgb1555);
                    int offset = dataOffset + (chunk * 156);

                    for (int y = 0; y < rows; y++)
                    {
                        for (int x = 0; x < cols; x++)
                        {
                            int idx = data[offset];
                            if (idx != 0)
                            {
                                if (idx - 1 < tile.Length)
                                {
                                    Copy8x8(bitmap, tile[idx - 1], y, x);
                                }
                                else
                                {
                                }
                            }
                            offset += 1;
                        }
                    }

                    string name = string.Format("{0}_{1:00}_{2:00}.png", block_name, tileidx, chunk);
                    bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);

                }
            }
        }

        private static void Copy8x8(Bitmap dst, Bitmap src, int by, int bx)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    dst.SetPixel((bx * 8) + x, (by * 8) + y, src.GetPixel(x, y));
                }
            }
        }


        private static Bitmap[] DecodeEGA8x8(byte[] data)
        {
            // try single frame
            uint height = Sys.ArrayToUshort(data, 0);
            uint width = Sys.ArrayToUshort(data, 2);
            uint x_pos = Sys.ArrayToUshort(data, 4);
            uint y_pos = Sys.ArrayToUshort(data, 6);
            uint item_count = data[8];

            uint width_px = width * 8;
            uint height_px = height;
            uint x_pos_px = x_pos * 8;
            uint y_pos_px = y_pos * 8;

            int ega_data_offset = 17;

            uint egaDataSize = height * width * 4;

            Color[] tileColors = new Color[16];
            for (int i = 0; i < 16; i++)
            {
                tileColors[i] = Color.FromArgb((int)egaColors[i]);
            }
            tileColors[13] = Color.Transparent;

            List<Bitmap> tiles = new List<Bitmap>();

            if (data.Length == (egaDataSize * item_count) + ega_data_offset)
            {
                int offset = 0;
                for (int i = 0; i < item_count; i++, offset += (int)egaDataSize)
                {
                    var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                    for (int y = 0; y < height_px; y++)
                    {
                        for (int x = 0; x < width_px; x += 2)
                        {
                            byte b = data[ega_data_offset + (y * width * 4) + (x / 2) + offset];
                            int pxX = (int)(x + 0 + x_pos_px);
                            int pxY = (int)(y + y_pos_px);
                            bitmap.SetPixel(pxX, pxY, tileColors[b >> 4]);
                            bitmap.SetPixel(pxX + 1, pxY, tileColors[b & 0xF]);
                        }
                    }

                    tiles.Add(bitmap);
                }
            }

            return tiles.ToArray();
        }

        private static void TryEGA(byte[] data, string block_name)
        {
            // try single frame
            uint height = Sys.ArrayToUshort(data, 0);
            uint width = Sys.ArrayToUshort(data, 2);
            uint x_pos = Sys.ArrayToUshort(data, 4);
            uint y_pos = Sys.ArrayToUshort(data, 6);
            uint item_count = data[8];

            uint width_px = width * 8;
            uint height_px = height;
            uint x_pos_px = x_pos * 8;
            uint y_pos_px = y_pos * 8;

            //byte[] field_9 = new byte[8];
            //System.Array.Copy(data, 9, field_9, 0, 8);

            int ega_data_offset = 17;

            if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                return;

            uint egaDataSize = height * width * 4;

            if (data.Length == (egaDataSize * item_count) + ega_data_offset)
            {
                int offset = 0;
                for (int i = 0; i < item_count; i++, offset += (int)egaDataSize)
                {
                    var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                    for (int y = 0; y < height_px; y++)
                    {
                        for (int x = 0; x < width_px; x += 2)
                        {
                            byte b = data[ega_data_offset + (y * width * 4) + (x / 2) + offset];
                            int pxX = (int)(x + 0 + x_pos_px);
                            int pxY = (int)(y + y_pos_px);
                            bitmap.SetPixel(pxX, pxY, Color.FromArgb((int)egaColors[b >> 4]));
                            bitmap.SetPixel(pxX + 1, pxY, Color.FromArgb((int)egaColors[b & 0xF]));
                        }
                    }

                    string name = string.Format("{0}_ega_part_{1:000}.png", block_name, i);
                    bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            else if (data.Length >= egaDataSize + ega_data_offset)
            {
                var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 2)
                    {
                        byte b = data[ega_data_offset + (y * width * 4) + (x / 2)];
                        int pxX = (int)(x + 0 + x_pos_px);
                        int pxY = (int)(y + y_pos_px);
                        bitmap.SetPixel(pxX, pxY, Color.FromArgb((int)egaColors[b >> 4]));
                        bitmap.SetPixel(pxX + 1, pxY, Color.FromArgb((int)egaColors[b & 0xF]));
                    }
                }

                string name = string.Format("{0}_ega.png", block_name);
                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static void TryEGASprite(byte[] data, string block_name)
        {
            // try single frame
            uint frames = data[0];
            int offset = 1;
            if (frames > 8) 
                return;

            Color[] clrs = new Color[16];
            for (int i = 0; i < 16; i++)
            {
                clrs[i] = Color.FromArgb((int)egaColors[i]);
            }

            var filename = Path.GetFileName(block_name);
            bool xorFrames = filename.StartsWith("pic", true, System.Globalization.CultureInfo.CurrentCulture);
            xorFrames |= filename.StartsWith("final", true, System.Globalization.CultureInfo.CurrentCulture);

            if (filename.StartsWith("spri", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                clrs[0] = Color.FromArgb(0, 0, 0, 0);
                clrs[13] = Color.FromArgb((int)egaColors[0]);
            }

            byte[] first_frame_ega_layout = null;

            for (int frame = 0; frame < frames; frame++)
            {
                if (data.Length < 21 + offset) return;

                uint delay = Sys.ArrayToUint(data, offset);
                offset += 4;
                int height = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int width = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int x_pos = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int y_pos = Sys.ArrayToUshort(data, offset);
                offset += 3;
                offset += 8;

                // skip 1 byte
                // skip 8 bytes
                int width_px = width * 8;
                int height_px = height;
                int x_pos_px = x_pos * 8;
                int y_pos_px = y_pos * 8;

                if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                    return;

                int egaDataSize = height * width * 4;

                if (data.Length < egaDataSize + offset) return;
                
                if (xorFrames)
                {
                    if (frame == 0)
                    {
                        first_frame_ega_layout = new byte[egaDataSize + 1];

                        System.Array.Copy(data, offset, first_frame_ega_layout, 0, egaDataSize);
                    }
                    else
                    {
                        for (int i = 0; i < egaDataSize; i++)
                        {
                            byte b = first_frame_ega_layout[i];
                            data[offset + i] ^= b;
                        }
                    }
                }

                var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 2)
                    {
                        byte b = data[(y * width * 4) + (x / 2) + offset];
                        int pxX = (int)(x + 0 + x_pos_px);
                        int pxY = (int)(y + y_pos_px);
                        bitmap.SetPixel(pxX, pxY, clrs[b >> 4]);
                        bitmap.SetPixel(pxX + 1, pxY, clrs[b & 0xF]);
                    }
                }

                string name = string.Format("{0}_ega_pic_{1:000}.png", block_name, frame);
                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);

                offset += egaDataSize;

            }

        }


        public static int sub_5CB7B(byte[] arg_0, int arg_4, byte[] arg_8) // 044B sub_5CB7B
        {
            byte[] var_6A = new byte[0x20];
            byte[] var_4A = new byte[0x20];
            byte[] var_2A = new byte[0x18];
            byte[] var_12 = new byte[0x10];
            byte var_1;

            if ((arg_8[arg_4] & 0xCC) == 0)
            {
                arg_0[0] = 3;

                Array.Copy(arg_8, arg_4, var_12, 0, 8);

                for (var_1 = 0; var_1 <= 0x0f; var_1++)
                {
                    var_4A[var_1] = var_1;

                    if ((var_1 & 1) != 0)
                    {
                        var_6A[var_1] = (byte)(var_12[var_1 / 2] & 0x0F);
                    }
                    else
                    {
                        var_6A[var_1] = (byte)((var_12[var_1 / 2] >> 4) & 0x0F);
                    }
                }
                arg_4 += 8;
            }
            else
            {
                arg_0[0] = arg_8[arg_4];
                arg_4 += 1;

                if ((arg_0[0] & 1) != 0)
                {
                    Array.Copy(arg_8, arg_4, var_12, 0, 8);
                    for (var_1 = 0; var_1 <= 0x1F; var_1++)
                    {
                        int dx = var_1 + var_1 + 6;

                        int dl = (var_12[var_1 / 4] >> dx) & 3;

                        var_6A[var_1] = (byte)dl;
                    }
                    arg_4 += 8;
                }

                if ((arg_0[0] & 2) != 0)
                {
                    Array.Copy(arg_8, arg_4, var_12, 0, 0x10);

                    for (var_1 = 0; var_1 < 0x10; var_1++)
                    {
                        var_4A[(var_1 * 2) + 0] = (byte)(var_12[var_1] & 0x0F);
                        var_4A[(var_1 * 2) + 1] = (byte)((var_12[var_1] >> 4) & 0x0F);
                    }
                    arg_4 += 0x10;
                }

                if ((arg_0[0] & 8) != 0)
                {
                    Array.Copy(arg_8, arg_4, var_2A, 0, 0x18);
                    arg_4 += 0x18;
                }
            }


            Array.Copy(var_2A, 0, arg_0, 0x20, 0x18);

            if ((arg_0[0] & 8) != 0)
            {
                arg_0[0x39] = 0xFF;
            }

            return arg_4;
        }


        private static void TryVGAPic(byte[] data, string block_name)
        {
            // try single frame
            uint frames = data[0];
            int offset = 1;
            if (frames > 8)
                return;

            Color[] clrs = new Color[16];
            for (int i = 0; i < 16; i++)
            {
                clrs[i] = Color.FromArgb((int)egaColors[i]);
            }

            var filename = Path.GetFileName(block_name);
            bool xorFrames = filename.StartsWith("pic", true, System.Globalization.CultureInfo.CurrentCulture);
            xorFrames |= filename.StartsWith("final", true, System.Globalization.CultureInfo.CurrentCulture);

            byte[] first_frame_ega_layout = null;

            for (int frame = 0; frame < frames; frame++)
            {
                if (data.Length < 21 + offset) return;

                uint delay = Sys.ArrayToUint(data, offset);
                offset += 4;
                int height = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int width = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int x_pos = Sys.ArrayToUshort(data, offset);
                offset += 2;
                int y_pos = Sys.ArrayToUshort(data, offset);
                offset += 3;
                offset += 8;

                // skip 1 byte
                // skip 8 bytes
                int width_px = width * 8;
                int height_px = height;
                int x_pos_px = x_pos * 8;
                int y_pos_px = y_pos * 8;

                if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                    return;

                int egaDataSize = height * width * 5;

                if (data.Length < egaDataSize + offset) 
                    return;

                if (xorFrames)
                {
                    if (frame == 0)
                    {
                        first_frame_ega_layout = new byte[egaDataSize + 1];

                        System.Array.Copy(data, offset, first_frame_ega_layout, 0, egaDataSize);
                    }
                    else
                    {
                        for (int i = 0; i < egaDataSize; i++)
                        {
                            byte b = first_frame_ega_layout[i];
                            data[offset + i] ^= b;
                        }
                    }
                }

                var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 2)
                    {
                        byte b = data[(y * width * 4) + (x / 2) + offset];
                        int pxX = (int)(x + 0 + x_pos_px);
                        int pxY = (int)(y + y_pos_px);
                        bitmap.SetPixel(pxX, pxY, clrs[b >> 4]);
                        bitmap.SetPixel(pxX + 1, pxY, clrs[b & 0xF]);
                    }
                }

                string name = string.Format("{0}_ega_pic_{1:000}.png", block_name, frame);
                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);

                offset += egaDataSize;

            }

        }

        private static void TryStrataVGA(byte[] data, string block_name)
        {
            if (data.Length < 20)
                return;

            // try single frame
            int height = Sys.ArrayToUshort(data, 0);
            int width = Sys.ArrayToUshort(data, 2);
            int x_pos = Sys.ArrayToUshort(data, 4);
            int y_pos = Sys.ArrayToUshort(data, 6);
            int pic_count = data[8];

            int width_px = width * 8;
            int height_px = height;
            int x_pos_px = x_pos * 8;
            int y_pos_px = y_pos * 8;

            int clr_count = data[9];
            int clr_start = data[10];

            if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
               return;

            if (data.Length < ((5 * width * height) * pic_count) + 9)
                return;

            var clrs = PaletteBase();
            
            byte[] tempPalette = new byte[0x3B];
            int data_offset = sub_5CB7B(tempPalette, 9, data);

            int[,] pxl_clrs = new int[height_px, width_px];

            int[] dd = new int[48];
            int _base = 0x20;
            for (int i = 0; i < 24; i++)
            {
                int b = tempPalette[_base + i];
                int a1 = (b & 0x0f);
                int a2 = (b & 0xf0);
                dd[0 + i] = (b & 0x0f) * 4;
                dd[24 + i] = (b & 0xf0) / 4;
            }

            for (int i = 0; i < 14; i += 1)
            {
                int r = dd[(i * 3) + 6 + 0];
                int g = dd[(i * 3) + 6 + 1];
                int b = dd[(i * 3) + 6 + 2];
                clrs[i + 18] = Color.FromArgb(r * 4, g * 4, b * 4);
            }

            int blocksize = (5 * width * height);

            for (int pic = 0; pic < pic_count; pic++)
            {
                int in_offset = data_offset;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width_px; x++)
                    {
                        int mask = 0x80 >> (x & 7);
                        int input_byte = x / 8;
                        byte px_clr = 0;

                        if ((data[in_offset + input_byte] & mask) != 0)
                        {
                            px_clr = 1;
                        }

                        if ((data[in_offset + input_byte + width] & mask) != 0)
                        {
                            px_clr += 2;
                        }

                        if ((data[in_offset + input_byte + (width * 2)] & mask) != 0)
                        {
                            px_clr += 4;
                        }

                        if ((data[in_offset + input_byte + (width * 3)] & mask) != 0)
                        {
                            px_clr += 8;
                        }

                        if ((data[in_offset + input_byte + (width * 4)] & mask) != 0)
                        {
                            px_clr += 0x10;
                        }

                        pxl_clrs[y, x] = px_clr;
                    }

                    in_offset += width * 5;
                }

                data_offset = in_offset;


                var bitmap = new Bitmap(width_px + x_pos_px, height_px + y_pos_px, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 1)
                    {
                        int pxX = x + x_pos_px;
                        int pxY = y + y_pos_px;
                        bitmap.SetPixel(pxX, pxY, clrs[pxl_clrs[y, x]]);
                    }
                }

                string name = string.Format("{0}_vga_strata_{1:00}.png", block_name, pic);
                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static void TryStrataBlocksVGA(byte[] data, string block_name)
        {
            if (data.Length < 5)
                return;

            uint frames = data[0];
            int offset = 5;
            if (frames > 8)
                return;

            if (data.Length < 20 + offset)
                return;

            // try single frame
            int height = Sys.ArrayToUshort(data, 0 + offset);
            int width = Sys.ArrayToUshort(data, 2 + offset);
            int x_pos = Sys.ArrayToUshort(data, 4 + offset);
            int y_pos = Sys.ArrayToUshort(data, 6 + offset);
            int item_count = data[8 + offset];

            int width_px = width * 8;
            int height_px = height;
            int x_pos_px = x_pos * 8;
            int y_pos_px = y_pos * 8;

            int clr_count = data[9 + offset];
            int clr_start = data[10 + offset];

            if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                return;

            bool egaBlock = (data[9 + offset] & 0xCC) == 0;
            int blocksize = egaBlock ? (4 * width * height) : (5 * width * height);

            if (data.Length < blocksize + 9 + offset)
                return;

            var clrs = PaletteBase();

            byte[] tempPalette = new byte[0x3B];
            int data_offset = sub_5CB7B(tempPalette, 9 + offset, data);

            int[,] pxl_clrs = new int[height_px, width_px];

            for (int frame = 0; frame < frames; frame++)
            {
                if (egaBlock)
                {
                    int in_offset = data_offset;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width_px; x += 2)
                        {
                            pxl_clrs[y, x] = data[in_offset] >> 4;
                            pxl_clrs[y, x + 1] = data[in_offset] & 0x0F;

                            in_offset += 1;
                        }
                    }

                    data_offset = in_offset;
                }
                else
                {
                    int in_offset = data_offset;
                    int out_offset = 0;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width_px; x++)
                        {
                            int mask = 0x80 >> (x & 7);
                            int input_byte = x / 8;
                            byte px_clr = 0;

                            if ((data[in_offset + input_byte] & mask) != 0)
                            {
                                px_clr = 1;
                            }

                            if ((data[in_offset + input_byte + width] & mask) != 0)
                            {
                                px_clr += 2;
                            }

                            if ((data[in_offset + input_byte + (width * 2)] & mask) != 0)
                            {
                                px_clr += 4;
                            }

                            if ((data[in_offset + input_byte + (width * 3)] & mask) != 0)
                            {
                                px_clr += 8;
                            }

                            if ((data[in_offset + input_byte + (width * 4)] & mask) != 0)
                            {
                                px_clr += 0x10;
                            }

                            pxl_clrs[y, x] ^= px_clr;
                        }

                        in_offset += width * 5;
                        out_offset += width * 8;
                    }

                    data_offset = in_offset + 14;
                }


                int[] dd = new int[48];
                int _base = 0x20;
                for (int i = 0; i < 24; i++)
                {
                    int b = tempPalette[_base + i];
                    int a1 = (b & 0x0f);
                    int a2 = (b & 0xf0);
                    dd[0 + i] = (b & 0x0f) * 4;
                    dd[24 + i] = (b & 0xf0) / 4;
                }

                for (int i = 0; i < 14; i += 1)
                {
                    int r = dd[(i * 3) + 6 + 0];
                    int g = dd[(i * 3) + 6 + 1];
                    int b = dd[(i * 3) + 6 + 2];
                    clrs[i + 18] = Color.FromArgb(r * 4, g * 4, b * 4);
                }

                var bitmap = new Bitmap(width_px + x_pos_px, height_px + y_pos_px, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 1)
                    {
                        int pxX = x + x_pos_px;
                        int pxY = y + y_pos_px;
                        bitmap.SetPixel(pxX, pxY, clrs[pxl_clrs[y, x]]);
                    }
                }

                string name = string.Format("{0}_vga_strata_block_{1:000}.png", block_name, frame);
                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            }
        }



        private static void TryMono(byte[] data, string block_name)
        {
            if ((data.Length % 8) == 0 && Path.GetFileName(block_name).StartsWith("8x8", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                // Try decode it as mono font.
                int count = data.Length / 8;

                for (int ch = 0; ch < count; ch++)
                {
                    var bitmap = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            byte b = data[(ch * 8) + y];
                            Color c = ((b & MonoBitMask[x]) != 0) ? Color.White : Color.Black;
                            bitmap.SetPixel(x, y, c);
                        }
                    }

                    string name = string.Format("{0}_mono_{1:000}.png", block_name, ch);
                    bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        static byte[] UnpackSpriteData(byte[] data, int offset)
        {
            List<byte> nd = new List<byte>();

            int input_index = offset;
            while (input_index < data.Length)
            {
                int run_length = (sbyte)data[input_index];

                if (run_length >= 0)
                {
                    if (input_index + run_length + 1 >= data.Length)
                        return new byte[0];

                    for (int i = 0; i <= run_length; i++)
                    {
                        nd.Add(data[input_index + i + 1]);
                    }

                    input_index += run_length + 2;
                }
                else
                {
                    run_length = -run_length;

                    if (input_index + 1 >= data.Length)
                        return new byte[0];

                    for (int i = 0; i <= run_length; i++)
                    {
                        nd.Add(data[input_index + 1]);
                    }

                    input_index += 2;
                }
            }

            return nd.ToArray();
        }

        static void TryVGASprite(byte[] data, string filename)
        {
            var orig_data = data;

            // Try as 8 vga sprite
            if (data.Length < 20)
                return;

            if (data[1] != 0)
                return;

            int height = data[0];
            int width = data[2];

            int width_px = width * 8;
            int height_px = height;

            if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                return;

            int frames = orig_data[8];
            int clr_base = orig_data[9];
            int clr_count = orig_data[10];

            int data_start = (clr_count * 3) + 11 + 0x79 + (3 * frames); // darkness, buckmatrix
            //int data_start = 306; // treasure sprites
            //int data_start = ww;

            data = UnpackSpriteData(data, data_start);

            int picSize = width_px * height_px;
            if (data.Length % picSize != 0)
                return;

            int frame_count = data.Length / picSize;

            bool sprite_file = false;


            int mframe = orig_data[((clr_count * 3) + 11) + 0x74];

            Color[] clrs = ExtractPalette(orig_data, clr_count, clr_base, 11);
            if (Path.GetFileName(filename).StartsWith("sprit", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                clrs[0] = Color.FromArgb(0);
                clrs[13] = Color.FromArgb(0, 0, 0);
                sprite_file = true;
            }

            if (clrs == null) return;

 
            {
                if (sprite_file)
                {
                    mframe = frame_count - 1;
                }

                for (int frame = 0; frame < frame_count; frame++)
                {
                    var bitmap = new Bitmap(width_px, height_px, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

                    for (int y = 0; y < height_px; y++)
                    {
                        for (int x = 0; x < width_px; x += 1)
                        {
                            byte b1 = data[(y * width_px) + x + (picSize * frame)];
                            byte b3 = data[(y * width_px) + x + (picSize * mframe)];
                            if (frame != mframe)
                            {
                                bitmap.SetPixel(x, y, clrs[b1 ^ b3]);
                            }
                            else
                            {
                                bitmap.SetPixel(x, y, clrs[b1]);
                            }
                        }
                    }

                    string name;
                    if (sprite_file)
                    {
                        name = string.Format("{0}_sprite_{1:000}.png", filename, frame, mframe);
                    }
                    else
                    {
                        name = string.Format("{0}_sprite_{1:000}_m{2:000}.png", filename, frame, mframe);
                    }
                    bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }


        static void TryVGA(byte[] data, string block_name)
        {
            // Try as 8 vga picture
            // try single frame
            if (data.Length < 20)
                return;

            int height = data[0];
            int width = data[1];
            int x_pos = 0;
            int y_pos = 0;

            int width_px = width * 8;
            int height_px = height;
            int x_pos_px = x_pos * 8;
            int y_pos_px = y_pos * 8;

            int clr_base = data[8];
            int clr_count = data[9];

            int palette_end = 10 + (clr_count * 3);
            int chunk_size = (width_px * height_px);

            int data_start = data.Length;
            int chunk_count = 0;

            if (width_px < 1 || height_px < 1 || width_px > 320 || height_px > 200)
                return;

            while (data_start > palette_end)
            {
                if (chunk_size > data_start) break;
                data_start -= chunk_size;
                chunk_count += 1;
            }

            //int frames = data[7];
            //Console.WriteLine("{0} {1}", data_start, palette_end + 0x79 + (3 * frames));
            //Console.WriteLine("{0} {1}", chunk_count, frames);

            for (int chunk = 0; chunk < chunk_count; chunk++, data_start += chunk_size)
            {
                Color[] clrs = ExtractPalette(data, clr_count, clr_base, 10);

                if (clrs == null) return;

                var bitmap = new Bitmap((int)(width_px + x_pos_px), (int)(height_px + y_pos_px), System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
                for (int y = 0; y < height_px; y++)
                {
                    for (int x = 0; x < width_px; x += 1)
                    {
                        byte b = data[data_start + (y * width_px) + x];
                        int pxX = (int)(x + 0 + x_pos_px);
                        int pxY = (int)(y + y_pos_px);
                        bitmap.SetPixel(pxX, pxY, clrs[b]);
                    }
                }


                string name;

                if (chunk_count > 1)
                {
                    name = string.Format("{0}_vga_{1:000}.png", block_name, chunk);
                }
                else
                {
                    name = string.Format("{0}_vga.png", block_name);
                }

                bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        static Color[] PaletteBase()
        {
            Color[] clrs = new Color[256];
            for (int i = 0; i < 16; i++)
            {
                clrs[i + 0x00] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x10] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x20] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x30] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x40] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x50] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x60] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x70] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x80] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0x90] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xA0] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xB0] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xC0] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xD0] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xE0] = Color.FromArgb((int)egaColors[i]);
                clrs[i + 0xF0] = Color.FromArgb((int)egaColors[i]);
            }

            return clrs;
        }

        static Color[] ExtractPalette(byte[] data, int clr_count, int clr_base, int offset)
        {
            var clrs = PaletteBase();

            for (int i = 0; i < clr_count; i++)
            {
                int r = data[offset + (i * 3) + 0] * 4;
                int g = data[offset + (i * 3) + 1] * 4;
                int b = data[offset + (i * 3) + 2] * 4;

                if (r > 255 || g > 255 || b > 255)
                {
                    return null;
                }
                clrs[clr_base + i] = Color.FromArgb(r, g, b);
            }

            return clrs;
        }

        private static IEnumerable<Block> GetAllBlocks(string file)
        {
            // try old CotAB file types
            FileStream fsA = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            var fileA = new BinaryReader(fsA);

            int dataOffset = fileA.ReadInt16() + 2;

            List<HeaderEntry> headers = new List<HeaderEntry>();

            const int headerEntrySize = 9;

            for (int i = 0; i < ((dataOffset - 2) / headerEntrySize); i++)
            {
                HeaderEntry dhe = new HeaderEntry();
                dhe.id = fileA.ReadByte();
                dhe.offset = fileA.ReadInt32();
                dhe.rawSize = fileA.ReadUInt16();
                dhe.compSize = fileA.ReadUInt16();

                //Console.WriteLine(dhe);
                headers.Add(dhe);
            }

            foreach (var dhe in headers)
            {
                byte[] comp = new byte[dhe.compSize];
                if (dhe.rawSize < 0)
                {
                    fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);
                    comp = fileA.ReadBytes(dhe.compSize);

                    yield return new Block(file, dhe.id, comp);
                }
                else
                {
                    byte[] raw = new byte[dhe.rawSize];

                    fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);

                    comp = fileA.ReadBytes(dhe.compSize);

                    Decode(dhe.rawSize, dhe.compSize, raw, comp);

                    yield return new Block(file, dhe.id, raw);
                }
            }

            if (fileA.BaseStream.Position != fileA.BaseStream.Length)
            {
                Console.WriteLine("Wrong Size!");
            }
        }

        static void Decode(int decodeSize, int dataLength, byte[] output_ptr, byte[] input_ptr)
        {
            sbyte run_length;
            int output_index;
            int input_index;

            input_index = 0;
            output_index = 0;

            do
            {
                run_length = (sbyte)input_ptr[input_index];

                if (run_length >= 0)
                {
                    for (int i = 0; i <= run_length; i++)
                    {
                        output_ptr[output_index + i] = input_ptr[input_index + i + 1];
                    }

                    input_index += run_length + 2;
                    output_index += run_length + 1;
                }
                else
                {
                    run_length = (sbyte)(-run_length);

                    for (int i = 0; i < run_length; i++)
                    {
                        output_ptr[output_index + i] = input_ptr[input_index + 1];
                    }

                    input_index += 2;
                    output_index += run_length;
                }
            } while (input_index < dataLength);
        }
    }

    class Block
    {
        public string file;
        public int id;
        public byte[] data;

        public Block(string _file, int _id, byte[] _data)
        {
            file = _file;
            id = _id;
            data = _data;
        }
    }

    class HeaderEntry
    {
        internal int id;
        internal int offset;
        internal int rawSize; // decodeSize
        internal int compSize; // dataLength

        public override string ToString()
        {
            return string.Format("id: {0} offset: {1} raw: {2} comp: {3}", id, offset, rawSize, compSize);
        }
    }
}
