using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Classes
{
    public class Display
    {
        public static Bitmap bm;
        static byte[,] OrigEgaColors = { { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 }, { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 }, { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 } };
        static byte[,] egaColors = { { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 }, { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 }, { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 } };
        static int[,] ram;
        static byte[] videoRam;
        static int videoRamSize;
        static int scanLineWidth;
        static int outputWidth;
        static int outputHeight;

        public delegate void VoidDeledate();

        static VoidDeledate updateCallback;

        static public VoidDeledate UpdateCallback
        {
            set
            {
                updateCallback = value;
            }
        }

        static Display()
        {
            outputHeight = 200;
            outputWidth = 320;

            bm = new Bitmap(outputWidth, outputHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            ram = new int[outputHeight, outputWidth];
            scanLineWidth = outputWidth * 3;
            videoRamSize = scanLineWidth * outputHeight;
            videoRam = new byte[videoRamSize];
        }

        static public void DisplayMono8x1(int x, int y, int value, int bgColor, int fgColor)
        {
            int px = x * 8;

            for (int i = 0; i < 8; i++)
            {
                ram[y, px + i] = (value & (0x80 >> i)) != 0 ? fgColor : bgColor;
                SetVidPixel(px + i, y, ram[y, px + i]);
            }
        }

        public static void SetEgaPalette(int index, int colour)
        {
            egaColors[index, 0] = OrigEgaColors[index, 0];
            egaColors[index, 1] = OrigEgaColors[index, 1];
            egaColors[index, 2] = OrigEgaColors[index, 2];

            for (int y = 0; y < outputHeight; y++)
            {
                int vy = y * scanLineWidth;
                for (int x = 0; x < outputWidth; x++)
                {
                    int vx = x * 3;
                    int egaColor = ram[y, x];

                    videoRam[vy + vx + 0] = egaColors[egaColor, 2];
                    videoRam[vy + vx + 1] = egaColors[egaColor, 1];
                    videoRam[vy + vx + 2] = egaColors[egaColor, 0];
                }
            }

            Display.Update();
        }

        static void SetVidPixel(int x, int y, int egaColor)
        {
            videoRam[(y * scanLineWidth) + (x * 3) + 0] = egaColors[egaColor, 2];
            videoRam[(y * scanLineWidth) + (x * 3) + 1] = egaColors[egaColor, 1];
            videoRam[(y * scanLineWidth) + (x * 3) + 2] = egaColors[egaColor, 0];
        }

        static public void SetPixel2(int x, int y, int value)
        {
            ram[y, x] = ((value >> 4) & 0x0f);
            ram[y, x + 1] = (value & 0x0f);

            SetVidPixel(x, y, ram[y, x]);
            SetVidPixel(x + 1, y, ram[y, x + 1]);
        }

        static Rectangle rect = new Rectangle(0, 0, 320, 200);

        static int noUpdateCount;

        public static void UpdateStop()
        {
            noUpdateCount++;
        }

        public static void UpdateStart()
        {
            noUpdateCount--;
            Update();
        }

        static public void Update()
        {
            if (noUpdateCount == 0)
            {
                System.Drawing.Imaging.BitmapData bmpData =
                    bm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    bm.PixelFormat);

                IntPtr ptr = bmpData.Scan0;

                System.Runtime.InteropServices.Marshal.Copy(videoRam, 0, ptr, videoRamSize);

                bm.UnlockBits(bmpData);

                if (updateCallback != null)
                {
                    updateCallback.Invoke();
                }
            }
        }

        public static void SetPixel3(int x, int y, byte value)
        {
            if (value != 16)
            {
                ram[y, x] = value;

                SetVidPixel(x, y, ram[y, x]);
            }
        }
    }
}
