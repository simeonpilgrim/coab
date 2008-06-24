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
            ptrs = new Struct_1D558[8];
            ptrs[0] = new Struct_1D558(); // 1D558 - seg600:7248
            ptrs[1] = new Struct_1D558(); // 1D560 - seg600:7250
            ptrs[2] = new Struct_1D558(); // 1D568 - seg600:7258
            ptrs[3] = new Struct_1D558(); // 1D570 - seg600:7260
            ptrs[4] = new Struct_1D558(); // 1D578 - seg600:7268
            ptrs[5] = new Struct_1D558(); // 1D580 - seg600:7270
            ptrs[6] = new Struct_1D558(); // 1D588 - seg600:7278
            ptrs[7] = new Struct_1D558(); // 1D590 - seg600:7280
        }
    }
}
