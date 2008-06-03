using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Struct_1D530
    {
        byte[] data;
        public MapInfo[,] maps;

        public void LoadData(byte[] _data)
        {
            data = new byte[0x400];
            System.Array.Copy(_data, 2, data, 0, 0x400);

            maps = new MapInfo[16, 16];

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    maps[y, x] = new MapInfo(data, x, y);
                }
            }
        }
    }

    public class MapInfo
    {
        public byte x0_dir_0;
        public byte x0_dir_2;
        public byte x1_dir_4;
        public byte x1_dir_6;

        public byte x2;

        public byte x3_dir_0;
        public byte x3_dir_2;
        public byte x3_dir_4;
        public byte x3_dir_6;

        internal MapInfo(byte[] data, int map_x, int map_y)
        {
            int map_y_x16 = map_y << 4;

            x0_dir_0 = (byte)((data[map_x + map_y_x16] >> 4) & 0x0f);
            x0_dir_2 = (byte)((data[map_x + map_y_x16]) & 0x0f);
            x1_dir_4 = (byte)((data[0x100 + map_x + map_y_x16] >> 4 ) & 0x0f);
            x1_dir_6 = (byte)((data[0x100 + map_x + map_y_x16]) & 0x0f);

            x2 = data[0x200 + map_y_x16 + map_x];

            byte b = data[0x300 + map_y_x16 + map_x];

            x3_dir_6 = (byte)((b >> 6) & 3);
            x3_dir_4 = (byte)((b >> 4) & 3);
            x3_dir_2 = (byte)((b >> 2) & 3);
            x3_dir_0 = (byte)(b & 3);
        }
    }
}
