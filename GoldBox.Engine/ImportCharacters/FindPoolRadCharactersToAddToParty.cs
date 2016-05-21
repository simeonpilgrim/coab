using GoldBox.Classes;
using GoldBox.Data;
using System.Collections.Generic;
using System.IO;

namespace GoldBox.Engine.ImportCharacters
{
    //TODO when using the copycurs program from DOSBOX
    internal class FindPoolRadCharactersToAddToParty : IFindCharactersToAddToParty
    {
        public IEnumerable<CharacterToAddToParty> LookIn(string filePath)
        {
            foreach (var fileName in Directory.GetFiles(filePath, "*.cha"))
            {
                var data = File.ReadAllBytes(fileName);
                if (data.Length != PoolRadPlayer.StructSize)
                    continue;

                var character = PoolRadCharacter.Parse(data);

                if (character.NpcByte > 0x7F)
                    continue;

                var isCharacterInTeamList = gbl.TeamList.Find(player => character.NameStructure.Value == player.name.Trim()) != null;
                if (isCharacterInTeamList)
                    continue;

                yield return new CharacterToAddToParty(fileName, character.NameStructure.Value);
            }
        }
    }
}
