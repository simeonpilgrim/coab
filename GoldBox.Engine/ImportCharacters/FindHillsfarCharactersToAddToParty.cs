using GoldBox.Classes;
using GoldBox.Data;
using System.Collections.Generic;
using System.IO;

namespace GoldBox.Engine.ImportCharacters
{
    //TODO not tested.
    internal class FindHillsfarCharactersToAddToParty : IFindCharactersToAddToParty
    {
        public IEnumerable<CharacterToAddToParty> LookIn(string filePath)
        {
            foreach (var fileName in Directory.GetFiles(filePath, "*.hil"))
            {
                var data = File.ReadAllBytes(fileName);
                if (data.Length != HillsFarPlayer.StructSize)
                    continue;

                var character = HillsfarCharacter.Parse(data);

                var isCharacterInTeamList = gbl.TeamList.Find(player => character.NameStructure.Value == player.name.Trim()) != null;
                if (isCharacterInTeamList)
                    continue;

                yield return new CharacterToAddToParty(fileName, character.NameStructure.Value);
            }
        }
    }
}
