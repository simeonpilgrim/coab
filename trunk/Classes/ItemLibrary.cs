using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Classes
{
    public class ItemLibrary
    {

		static string libraryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CotAB");
		static string libraryFile = Path.Combine(libraryPath, "ItemLibrary.dat");

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
				FileStream fs = new FileStream(libraryFile, FileMode.Open);

                if (fs.Length == 0)
                {
                    library = new List<Item>();
                    return;
                }

                // Construct a BinaryFormatter and use it to serialize the data to the stream.
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    library = (List<Item>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    //Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        public static void Write()
        {
			Directory.CreateDirectory(libraryPath);
			FileStream fs = new FileStream(libraryFile, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, library);
            }
            catch (SerializationException e)
            {
                //Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
