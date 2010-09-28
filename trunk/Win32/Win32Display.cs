using System;
using System.Drawing;

namespace Win32
{
    public class Win32Display : Classes.IOSDisplay
    {
        public static Bitmap bm;
        static Rectangle rect = new Rectangle(0, 0, 320, 200);

        public void Init(int height, int width)
        {
            bm = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        public void RawCopy(byte[] videoRam, int videoRamSize)
        {
            System.Drawing.Imaging.BitmapData bmpData =
                bm.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            IntPtr ptr = bmpData.Scan0;

            System.Runtime.InteropServices.Marshal.Copy(videoRam, 0, ptr, videoRamSize);

            bm.UnlockBits(bmpData);
        }
    }
}
