using System;
namespace Classes
{
    public class SteppingPath
    {
        // 0x17 or 23 long
        public Point attacker;      // 00
        public Point target;        // 04
        int delta_count;     // 08
        public int diff_x;          // 0a 
        public int diff_y;          // 0a 
        public Point current;       // 0e
        int sign_x;          // 12
        int sign_y;          // 14
        public byte steps;          // 16
        public byte direction;      // 17 

        public void Clear()
        {
            attacker.x = 0;
            attacker.y = 0;
            target.x = 0;
            target.y = 0;
            delta_count = 0;
            diff_x = 0;
            diff_y = 0;
            current.x = 0;
            current.y = 0;
            sign_x = 0;
            sign_y = 0;
            steps = 0;
            direction = 0;
        }

        /// <summary>
        /// Return if step was taken
        /// </summary>
        public bool Step() /* sub_7324C */
        {
            bool stepMade = false;
            int index_x = 1;
            int index_y = 1;

            if (diff_x >= diff_y)
            {
                if (current.x != target.x)
                {
                    current.x += sign_x;
                    delta_count += diff_y * 2;
                    steps += 2;

                    index_x = sign_x + 1;

                    if (delta_count >= diff_x)
                    {
                        current.y += sign_y;
                        delta_count -= diff_x * 2;
                        steps += 1;

                        index_y = sign_y + 1;
                    }

                    stepMade = true;
                }
            }
            else if (current.y != target.y)
            {
                current.y += sign_y;
                delta_count += diff_x * 2;
                steps += 2;

                index_y = sign_y + 1;

                if (delta_count >= diff_y)
                {
                    current.x += sign_x;
                    delta_count -= diff_y * 2;
                    steps += 1;

                    index_x = sign_x + 1;
                }

                stepMade = true;
            }

            direction = directions[(index_y * 3) + index_x];

            return stepMade;
        }

        static byte[] directions = { 7, 0, 1, 6, 8, 2, 5, 4, 3, 8 };

        public void CalculateDeltas() /* sub_731A5 */
        {
            current = attacker;

            var tmp = target - attacker;
            diff_x = Math.Abs(tmp.x);
            diff_y = Math.Abs(tmp.y);

            sign_x = Math.Sign(tmp.x);
            sign_y = Math.Sign(tmp.y);

            delta_count = 0;
            steps = 0;
        }
    }
}
