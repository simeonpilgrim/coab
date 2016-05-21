using GoldBox.Classes;

namespace GoldBox.Engine.ImportCharacters
{
    public class FindCharactersToAddToPartyFactory
    {
        private static readonly IFindCharactersToAddToParty CurseFinder = new FindCotABCharactersToAddToParty();
        private static readonly IFindCharactersToAddToParty PoolRadFinder = new FindPoolRadCharactersToAddToParty();
        private static readonly IFindCharactersToAddToParty HillsFarFinder = new FindHillsfarCharactersToAddToParty();

        public static IFindCharactersToAddToParty Create(ImportSource importSource)
        {
            switch (importSource)
            {
                case ImportSource.Curse:
                    return CurseFinder;
                case ImportSource.Pool:
                    return PoolRadFinder;
                case ImportSource.Hillsfar:
                    return HillsFarFinder;
                default:
                    return new ReturnZeroCharactersFound();
            }
        }
    }
}
