using GoldBox.Classes;

namespace GoldBox.Engine.ImportCharacters
{
    public class FindCharactersToAddToPartyFactory
    {
        public static IFindCharactersToAddToParty Create(ImportSource importSource)
        {
            switch (importSource)
            {
                case ImportSource.Curse:
                case ImportSource.Hillsfar:
                case ImportSource.Pool:
                default:
                    return new ReturnZeroCharactersFound();

            }
        }
    }
}
