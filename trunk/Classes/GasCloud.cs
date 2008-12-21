using System;

namespace Classes
{
    public class GasCloud
    {
        public GasCloud(Player _player, int count, Point pos)
        {
            player = _player;
            field_1C = count;
            targetPos = new Point(pos);
            field_1D = false;

            // zero the rest.
            groundTile = new int[10];
            present = new bool[10];
        }

        public Player player; // 0x00;

        public int[] groundTile; //0x07 base-1 array
        public bool[] present; // 0x10 base-1 array

        public Point targetPos;

        public int field_1C;// 0x1C - field_1C
        public bool field_1D;// 0x1D
    }
}
