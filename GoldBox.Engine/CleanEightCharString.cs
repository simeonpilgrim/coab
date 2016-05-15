namespace GoldBox.Engine
{
    public class CleanEightCharString
    {
        static char[] uncleanCharacters = { ' ', '.', '*', ',', '?', '/', '\\', ':', ';', '|' };

        public string clean_string(string s)
        {
            string cleanStr = s.Trim(uncleanCharacters).ToLower();
            if (cleanStr.Length > 8)
            {
                cleanStr = cleanStr.Substring(0, 8);
            }

            return cleanStr;
        }

    }
}
