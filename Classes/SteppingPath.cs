using System;
namespace Classes
{
    public class SteppingPath
    {
        // 0x17 or 23 long
        public int attacker_x;
        public int attacker_y;
        public int target_x;
        public int target_y;
        public int delta_count; 
        public int diff_x; 
        public int diff_y; 
        public int current_x;
        public int current_y;
        public int sign_x; 
        public int sign_y; 
        public byte steps;
        public byte direction;

        public void Clear()
        {
            attacker_x = 0;
            attacker_y = 0;
            target_x = 0;
            target_y = 0;
            delta_count = 0;
            diff_x = 0;
            diff_y = 0;
            current_x = 0;
            current_y = 0;
            sign_x = 0;
            sign_y = 0;
            steps = 0;
            direction = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>if a step was done</returns>
        public bool Step() /* sub_7324C */
        {
            bool stepMade = false;
            int index_x = 1;
            int index_y = 1;

            if (diff_x >= diff_y)
            {
                if (current_x != target_x)
                {
                    current_x += sign_x;
                    delta_count += diff_y * 2;
                    steps += 2;

                    index_x = sign_x + 1;

                    if (delta_count >= diff_x)
                    {
                        current_y += sign_y;
                        delta_count -= diff_x * 2;
                        steps += 1;

                        index_y = sign_y + 1;
                    }

                    stepMade = true;
                }
            }
            else if (current_y != target_y)
            {
                current_y += sign_y;
                delta_count += diff_x * 2;
                steps += 2;

                index_y = sign_y + 1;

                if (delta_count >= diff_y)
                {
                    current_x += sign_x;
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
        //, 4, 2, 1, 0, 0,  
		//							  0x55, 0x55, 0xAA, 0xAA, 0xFF, 0xFF, 0, 0, 0,  
		//							  1, 2, 2, 2, 3, 0, 1, 1, 1, 2, 2, 3, 3 
		//						  };


        public void CalculateDeltas() /* sub_731A5 */
        {
            current_x = attacker_x;
            current_y = attacker_y;
            diff_x = Math.Abs(target_x - attacker_x);
            diff_y = Math.Abs(target_y - attacker_y);

            sign_x = Math.Sign(target_x - attacker_x);
            sign_y = Math.Sign(target_y - attacker_y);

            delta_count = 0;
            steps = 0;
        }
 

    }
}
