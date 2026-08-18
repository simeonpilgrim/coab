using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Classes;
using Logging;

namespace MainAvalonia
{
    static class Program
    {
        public static string StartupError { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            Config.Setup();
            string[] avaloniaArgs = ConfigureDataDirectory(args);

            AppDomain.CurrentDomain.UnhandledException
                += delegate(object sender, UnhandledExceptionEventArgs eventArgs)
                {
                    var exception = (Exception)eventArgs.ExceptionObject;

                    string logFile = Path.Combine(Logger.GetPath(), "Crash Log.txt");

                    using (TextWriter tw = new StreamWriter(logFile, true))
                    {
                        tw.WriteLine("");
                        tw.WriteLine("{0}", DateTime.Now);
                        tw.WriteLine("Unhandled exception: " + exception);
                    }

                    Console.Error.WriteLine("Unexpected Error, please send '{0}' to simeon.pilgrim@gmail.com", logFile);
                    Environment.Exit(1);
                };

            Logger.SetExitFunc(engine.seg043.print_and_exit);

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(avaloniaArgs);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }

        static string[] ConfigureDataDirectory(string[] args)
        {
            var avaloniaArgs = new List<string>();
            string requestedPath = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--data-dir")
                {
                    if (i + 1 >= args.Length)
                    {
                        StartupError = "The --data-dir option requires a directory path.";
                        break;
                    }

                    requestedPath = args[++i];
                }
                else if (args[i].StartsWith("--data-dir=", StringComparison.Ordinal))
                {
                    requestedPath = args[i].Substring("--data-dir=".Length);
                }
                else
                {
                    avaloniaArgs.Add(args[i]);
                }
            }

            if (StartupError != null)
            {
                return avaloniaArgs.ToArray();
            }

            string dataDirectory = FindDataDirectory(requestedPath);
            if (dataDirectory != null)
            {
                Directory.SetCurrentDirectory(dataDirectory);
                return avaloniaArgs.ToArray();
            }

            string requestedMessage = requestedPath == null
                ? ""
                : $"The requested data directory was '{requestedPath}'.{Environment.NewLine}{Environment.NewLine}";

            StartupError =
                "Compatible Curse of the Azure Bonds game data could not be found."
                + Environment.NewLine + Environment.NewLine
                + requestedMessage
                + "Place coab.app (macOS) or coab (Linux) in the directory containing "
                + "the ITEMS file and the original *.DAX files, then launch it again. "
                + "The required 8X8D1.DAX file must contain graphics block 202. "
                + "You can also run coab from a terminal with:"
                + Environment.NewLine + Environment.NewLine
                + "coab --data-dir /path/to/game/data"
                + Environment.NewLine + Environment.NewLine
                + $"Logs are stored in '{Logger.GetPath()}'.";

            Console.Error.WriteLine(StartupError);
            return avaloniaArgs.ToArray();
        }

        static string FindDataDirectory(string requestedPath)
        {
            if (requestedPath != null)
            {
                return ValidDataDirectory(requestedPath);
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string applicationDirectory = AppContext.BaseDirectory;
            string bundleParentDirectory = GetBundleParentDirectory();

            string[] candidates = {
                currentDirectory,
                Path.Combine(currentDirectory, "Data"),
                applicationDirectory,
                Path.Combine(applicationDirectory, "Data"),
                bundleParentDirectory,
                bundleParentDirectory == null ? null : Path.Combine(bundleParentDirectory, "Data"),
            };

            foreach (string candidate in candidates)
            {
                string validPath = ValidDataDirectory(candidate);
                if (validPath != null)
                {
                    return validPath;
                }
            }

            return null;
        }

        static string GetBundleParentDirectory()
        {
            var macOSDirectory = new DirectoryInfo(AppContext.BaseDirectory);
            DirectoryInfo contentsDirectory = macOSDirectory.Parent;
            DirectoryInfo bundleDirectory = contentsDirectory?.Parent;

            if (macOSDirectory.Name == "MacOS" &&
                contentsDirectory?.Name == "Contents" &&
                string.Equals(bundleDirectory?.Extension, ".app", StringComparison.OrdinalIgnoreCase))
            {
                return bundleDirectory.Parent?.FullName;
            }

            return null;
        }

        static string ValidDataDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            try
            {
                string fullPath = Path.GetFullPath(path);
                string itemsFile = FilePath.Resolve(Path.Combine(fullPath, "ITEMS"));
                string graphicsFile = FilePath.Resolve(Path.Combine(fullPath, "8X8D1.DAX"));

                if (System.IO.File.Exists(itemsFile) == false ||
                    DaxFileContainsBlock(graphicsFile, 202) == false)
                {
                    return null;
                }

                return fullPath;
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }

        static bool DaxFileContainsBlock(string path, byte blockId)
        {
            if (System.IO.File.Exists(path) == false)
            {
                return false;
            }

            using (var stream = System.IO.File.OpenRead(path))
            using (var reader = new BinaryReader(stream))
            {
                int headerSize = reader.ReadUInt16();
                const int headerEntrySize = 9;

                if (headerSize % headerEntrySize != 0 || headerSize > stream.Length - 2)
                {
                    return false;
                }

                for (int i = 0; i < headerSize / headerEntrySize; i++)
                {
                    byte id = reader.ReadByte();
                    reader.ReadBytes(headerEntrySize - 1);

                    if (id == blockId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
