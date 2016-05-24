using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 4)]
    public struct SetBlock
    {
        public short BlockId;
        public short SetId;
    }
}