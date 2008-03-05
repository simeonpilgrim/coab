using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class DaxArray
    {
        public byte numFrames;
        public byte curFrame;
        public Struct_1D558[] ptrs;

        public DaxArray()
        {
            numFrames = 0;
            curFrame = 0;
            ptrs = new Struct_1D558[5];
            ptrs[0] = new Struct_1D558(); // 1D558
            ptrs[1] = new Struct_1D558(); // 1D55C
            ptrs[2] = new Struct_1D558(); // 1D560
            ptrs[3] = new Struct_1D558(); // 1D564
            ptrs[4] = new Struct_1D558(); // 1D568
        }
    }
}
