using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GoldBox.Data
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 0x149)]
    public struct CharacterLoadFileCollection
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
                var list = new List<CharacterLoadFile> { Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8 };
                return list.GetRange(0, NumberOfPlayersInParty);
            }
        }
    }
}