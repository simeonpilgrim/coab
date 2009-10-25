using System;

namespace Classes.DaxFiles
{
    class DaxHeaderEntry
    {
        internal int id;
        internal int offset;
        internal int rawSize; // decodeSize
        internal int compSize; // dataLength
    }
}
