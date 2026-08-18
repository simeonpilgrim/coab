using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Classes
{
    public class ItemLibrary
    {

        static string libraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CotAB");
        // JSON replaces the BinaryFormatter .dat format, which .NET 8+ can no longer read
        static string libraryFile = Path.Combine(libraryPath, "ItemLibrary.json");

        static JsonSerializerOptions serializerOptions = new JsonSerializerOptions { IncludeFields = true };

        static List<Item> library = new List<Item>();
        public static void Add(Item item)
        {
            Item i = item.ShallowClone();
            i.readied = false;
            i.hidden_names_flag = 0;
            i.name = i.GenerateName(0);
            if (library.Contains(i) == false)
            {
                library.Add(i);
                Write();
            }
        }

        public static void Read()
        {
            if (System.IO.File.Exists(libraryFile))
            {
                try
                {
                    library = JsonSerializer.Deserialize<List<Item>>(System.IO.File.ReadAllText(libraryFile), serializerOptions);
                }
                catch (JsonException)
                {
                    library = new List<Item>();
                }

                if (library == null)
                {
                    library = new List<Item>();
                }
            }
        }

        public static void Write()
        {
            Directory.CreateDirectory(libraryPath);

            System.IO.File.WriteAllText(libraryFile, JsonSerializer.Serialize(library, serializerOptions));
        }
    }
}
