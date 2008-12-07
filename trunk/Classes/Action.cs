using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Action.
    /// </summary>
    public class Action
    {
        public byte spell_id; // 0x00
        public bool can_cast; // 0x01
        public bool can_use;  // 0x02
        public sbyte delay;   // 0x03
        public byte field_4;  // 0x04
        public byte field_5;  // 0x05
        public byte move;     // 0x06
        public bool guarding; // 0x07
        public byte field_8;  // 0x08
        public byte direction;  // 0x09 field_9
        public Player target; // 0x0A 4 bytes
        public int bleeding; // 0x0E
        public byte field_F;  // 0x0F
        public bool fleeing; // 0x10
        public byte field_11; // 0x11
        public byte field_12; // 0x12
        public byte field_13; // 0x13
        public byte field_14; // 0x14
        public byte field_15; // 0x15

        public Action()
        {
            target = null;
        }
    }
}
