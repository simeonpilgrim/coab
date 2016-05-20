using System.Collections.Generic;
using System.IO;
using GoldBox.Classes;

namespace GoldBox.Engine.ImportCharacters
{
    internal class FindCotABCharactersToAddToParty : IFindCharactersToAddToParty
    {
        public IEnumerable<CharacterToAddToParty> LookIn(string filePath)
        {
            foreach (var fileName in Directory.GetFiles(filePath, "*.guy"))
            {
                using (var file = new ReadOnlyFileStream(fileName))
                {
                    if (file.Length != Player.StructSize)
                        continue;

                    var data = new byte[16];

                    file.Seek(0xf7, SeekOrigin.Begin);
                    file.Read(data, 0, 1);
                    var isNpc = data[0] > 0x7F;
                    if (isNpc)
                        continue;

                    file.Read(data, 0, 16);
                    var character = Data.CharacterName.Parse(data);
                    var isCharacterInTeamList = gbl.TeamList.Find(player => character.Name == player.name.Trim()) != null;
                    if (isCharacterInTeamList)
                        continue;

                    yield return new CharacterToAddToParty(fileName, character.Name);
                }
            }
        }
    }
}
