using GoldBox.Logging;
using System;
using System.IO;

namespace GoldBox.Classes
{
    public class ReadOnlyFileStream : IDisposable
    {
        public Stream BaseStream { get; private set; }
        public bool FileExists { get; }
        public long Length => BaseStream.Length;

        private readonly string _filePath;

        public ReadOnlyFileStream(string fileName)
        {
            var file = Config.BaseFile(fileName);
            if (!file.Exists)
                file = Config.DataFile(fileName);

            _filePath = file.FullName;

            FileExists = file.Exists;
            Open();
        }

        public void Open()
        {
            if(!FileExists)
            {
                Logger.Log("File not found:{0}", Path.GetFullPath(_filePath));
                return;
            }
            BaseStream = File.Open(_filePath, FileMode.Open, FileAccess.Read);
            Logger.Debug("Reading File:{0}", _filePath);
        }

        public void Dispose()
        {
            Logger.Debug("Closing File:{0}", _filePath);
            BaseStream?.Dispose();
        }
        public void Seek(long offset, SeekOrigin seekOrigin)
        {
            BaseStream?.Seek(offset, seekOrigin);
        }

        public void Read(byte[] array, int offset, int count)
        {
            BaseStream?.Read(array, offset, count);
        }
    }
}
