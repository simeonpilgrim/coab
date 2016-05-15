using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace GoldBox.Classes
{
    sealed class AllowAllAssemblyVersionsDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly().FullName;

            // In this case we are always using the current assembly
            assemblyName = currentAssembly;

            // Get the type using the typeName and assemblyName
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

            return typeToDeserialize;
        }
    }

    public class ItemLibrary
    {
        private readonly static string libraryPath;
        private readonly static string libraryFile;
        private readonly static List<Item> library;
        private readonly static BinaryFormatter _formatter;

        static ItemLibrary()
        {
            _formatter = new BinaryFormatter
            {
                Binder = new AllowAllAssemblyVersionsDeserializationBinder()
            };
            library = new List<Item>();
            libraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CotAB");
            libraryFile = Path.Combine(libraryPath, "ItemLibrary.dat");
        }

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
            library.Clear();

            if (!System.IO.File.Exists(libraryFile))
                return;

            using (var fs = new FileStream(libraryFile, FileMode.Open))
            {
                if (fs.Length == 0)
                    return;

                library .AddRange( (List<Item>)_formatter.Deserialize(fs));
            }
        }

        public static void Write()
        {
            Directory.CreateDirectory(libraryPath);
            using (var fs = new FileStream(libraryFile, FileMode.Create))
            {
                _formatter.Serialize(fs, library);
            }
        }
    }
}
