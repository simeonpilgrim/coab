using System.Collections.Generic;
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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1E00)]
        public byte[] EclPointer;

        public sbyte MapPosX;
        public sbyte MapPosY;
        public byte MapDirection;
        public byte MapWallType;
        public byte MapWallRoof;

        public GameState LastGameState;
        public GameState GameState;

        public SetBlock SetBlock1;
        public SetBlock SetBlock2;
        public SetBlock SetBlock3;

        public PlayerLoadFiles Players;

        public byte[] ToByteArray() => Extensions.ToByteArray(this);
        public static CurseSaveGame Parse(byte[] array) => Extensions.MarshalAs<CurseSaveGame>(array);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 4)]
    public struct SetBlock
    {
        public short BlockId;
        public short SetId;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 0x149)]
    public struct PlayerLoadFiles
    {
        public byte NumberOfPlayersInParty;
        public CharacterLoadFile Player1;
        public CharacterLoadFile Player2;
        public CharacterLoadFile Player3;
        public CharacterLoadFile Player4;
        public CharacterLoadFile Player5;
        public CharacterLoadFile Player6;
        public CharacterLoadFile Player7;
        public CharacterLoadFile Player8;

        public IEnumerable<CharacterLoadFile> Players
        {
            get
            {
                var list = new List<CharacterLoadFile> {Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8};
                return list.GetRange(0, NumberOfPlayersInParty);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 0x29)]
    public struct CharacterLoadFile
    {
        private byte FileNameLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x08)]
        public string FileName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x20)]
        private byte[] Houses;
    }
    public enum GameState : byte
    {
        StartGameMenu = 0,
        Shop = 1,
        Camping = 2,
        WildernessMap = 3,
        DungeonMap = 4,
        Combat = 5,
        AfterCombat = 6,
        EndGame = 7
    }
}
