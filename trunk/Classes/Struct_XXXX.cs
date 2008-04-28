namespace Classes
{
    public class Struct_XXXX
    {
        // 0x17 or 23 long
        public int attacker_x; /* was short */
        public int attacker_y; /* was short */
        public int target_x; /* was short */
        public int target_y; /* was short */
        public int field_08; /* was short */
        public int diff_x; /* was short */
        public int diff_y; /* was short */
        public int field_0E; /* was short */
        public int field_10; /* was short */
        public int sign_x; /* was short */
        public int sign_y; /* was short */
        public byte field_16;
        public byte field_17;

        public void Clear()
        {
            attacker_x = 0;
            attacker_y = 0;
            target_x = 0;
            target_y = 0;
            field_08 = 0;
            diff_x = 0;
            diff_y = 0;
            field_0E = 0;
            field_10 = 0;
            sign_x = 0;
            sign_y = 0;
            field_16 = 0;
            field_17 = 0;
        }
    }
}
