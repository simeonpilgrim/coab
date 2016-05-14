namespace GoldBox.Classes
{
    interface IDataIO
    {
        void Write(byte[] data, int offset);
        void Read(byte[] data, int offset);
    }
}
