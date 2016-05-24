using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack =1, Size = 0x1E00)]
    public struct CommandList
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1E00)]
        public byte[] EclPointer;
    }
}
