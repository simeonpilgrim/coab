namespace GoldBox.Engine.ImportCharacters
{
    public class CharacterToAddToParty
    {
        public string FileName { get; }
        public string DisplayName { get; }

        public CharacterToAddToParty(string fileName, string displayName)
        {
            FileName = fileName;
            DisplayName = displayName;
        }
    }
}