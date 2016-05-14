using System.Collections.Generic;

namespace GoldBox.Classes.DaxFiles
{
    public class DaxCache
    {
        static Dictionary<string, DaxFile> fileCache = new Dictionary<string, DaxFile>();

        public static byte[] LoadDax(string file_name, int block_id)
        {
            DaxFile dfc;

            file_name = file_name.ToLower();

            if (!fileCache.TryGetValue(file_name, out dfc))
            {
                dfc = new DaxFile(file_name);
                fileCache.Add(file_name, dfc);
            }

            return dfc.GetData(block_id);
        }
    }
}
