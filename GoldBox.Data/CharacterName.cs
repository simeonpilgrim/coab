using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 16)]
    public struct CharacterName
    {
        public byte Length;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string Value;

        public byte[] ToByteArray() => Extensions.ToByteArray(this);
        public static CharacterName Parse(byte[] array) => Extensions.MarshalAs<CharacterName>(array);
    }
}
