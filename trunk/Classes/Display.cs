using System;
using System.Collections.Generic;
using System.Text;


namespace Classes
{
    public interface IOSDisplay
    {
        void Init(int height, int width);
        void RawCopy(byte[] videoRam, int videoRamSize);
    }

    public class Display
    {
        static byte[,] OrigEgaColors = { { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 }, { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 }, { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 } };
        static byte[,] egaColors = { { 0, 0, 0 }, { 0, 0, 173 }, { 0, 173, 0 }, { 0, 173, 173 }, { 173, 0, 0 }, { 173, 0, 173 }, { 173, 82, 0 }, { 173, 173, 173 }, { 82, 82, 82 }, { 82, 82, 255 }, { 82, 255, 82 }, { 82, 255, 255 }, { 255, 82, 82 }, { 255, 82, 255 }, { 255, 255, 82 }, { 255, 255, 255 } };
        static int[,] ram;
        static byte[] videoRam;
        static byte[] videoRamBkUp;
        static int videoRamSize;
        static int scanLineWidth;
        static int outputWidth;
        static int outputHeight;

        static IOSDisplay lowLevelDisplay;
        public static IOSDisplay LowLevelDisplay
        {
            set {
                lowLevelDisplay = value;
                lowLevelDisplay.Init(outputHeight, outputWidth);
            }
        }

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

            ram = new int[outputHeight, outputWidth];
            scanLineWidth = outputWidth * 3;
            videoRamSize = scanLineWidth * outputHeight;
            videoRam = new byte[videoRamSize];
        }

        static int[] MonoBitMask = { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

        public static void DisplayMono8x8(int xCol, int yCol, byte[] monoData8x8, int bgColor, int fgColor)
        {
            int pX = xCol * 8;

            for (int yStep = 0; yStep < 8; yStep++)
            {
                int pY = (yCol * 8) + yStep;
                int value = gbl.monoCharData[yStep];

                for (int i = 0; i < 8; i++)
                {
                    ram[pY, pX + i] = (value & MonoBitMask[i]) != 0 ? fgColor : bgColor;
                    SetVidPixel(pX + i, pY, ram[pY, pX + i]);
                }
            }
        }

        public static void SetEgaPalette(int index, int colour)
        {
            egaColors[index, 0] = OrigEgaColors[colour, 0];
            egaColors[index, 1] = OrigEgaColors[colour, 1];
            egaColors[index, 2] = OrigEgaColors[colour, 2];

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
                lowLevelDisplay.RawCopy(videoRam, videoRamSize);

                if (updateCallback != null)
                {
                    updateCallback.Invoke();
                }
            }
        }

        public static void SaveVidRam()
        {
            videoRamBkUp = (byte[])videoRam.Clone();
        }

        public static void RestoreVidRam()
        {
            videoRam = videoRamBkUp;
        }

        public static byte GetPixel(int x, int y)
        {
            return (byte)ram[y, x];
        }

        public static void SetPixel3(int x, int y, int value)
        {
            if (value < 16)
            {
                ram[y, x] = value;

                SetVidPixel(x, y, ram[y, x]);
            }
            if (value > 16)
            {
            }
        }

        public const int MaxRow = 0x18;
        public const int MinRow = 0x00;
        public const int MaxCol = 0x31;
        public const int MinCol = 0x00;
    }
}
