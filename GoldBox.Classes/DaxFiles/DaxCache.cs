using System.Collections.Generic;

namespace GoldBox.Classes.DaxFiles
{
    public class DaxCache
    {
        static Dictionary<string, DaxFile> fileCache = new Dictionary<string, DaxFile>();

        /// <summary>
        /// Tries loading a block from Dax file. Might return null
        /// </summary>
        /// <param name="file_name">the Dax file to read the block from</param>
        /// <param name="block_id">the id of the block</param>
        /// <returns>the block of data from the dax file or null if it can't be found</returns>
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
