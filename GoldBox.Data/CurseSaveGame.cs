using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 0x335D)]
    public struct CurseSaveGame
    {
        public byte GameArea;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x800)]
        public byte[] AreaPointer1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x800)]
        public byte[] AreaPointer2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x200)]
        public ushort[] SomeStructure;

        public CommandList EclPointer;

        public sbyte MapPosX;
        public sbyte MapPosY;
        public byte MapDirection;
        public byte MapWallType;
        public byte MapWallRoof;

        public SavedGameState LastGameState;
        public SavedGameState GameState;

        public SetBlock SetBlock1;
        public SetBlock SetBlock2;
        public SetBlock SetBlock3;

        public CharacterLoadFileCollection Characters;

        public byte[] ToByteArray() => Extensions.ToByteArray(this);
        public static CurseSaveGame Parse(byte[] array) => Extensions.MarshalAs<CurseSaveGame>(array);
    }
}
