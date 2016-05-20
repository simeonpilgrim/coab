using System.Collections.Generic;

namespace GoldBox.Engine.ImportCharacters
{

    public interface IFindCharactersToAddToParty
    {
        IEnumerable<CharacterToAddToParty> LookIn(string filePath);
    }
}
