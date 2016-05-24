using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 0x29)]
    public struct CharacterLoadFile
    {
        private byte FileNameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x08)]
        public string FileName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
        private byte[] Houses;
    }

}