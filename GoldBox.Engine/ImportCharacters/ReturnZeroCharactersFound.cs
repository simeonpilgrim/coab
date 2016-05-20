using System.Collections.Generic;

namespace GoldBox.Engine.ImportCharacters
{
    internal class ReturnZeroCharactersFound : IFindCharactersToAddToParty
    {
        public IEnumerable<CharacterToAddToParty> LookIn(string filePath) => new CharacterToAddToParty[0];
    }
}