using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public static class Limits
    {
        public static int[,] RaceAgeBrackets = { //unk_1A434
            { 9999, 9999, 9999, 9999, 9999}, // monster, made-up
            {   50,  150,  250,  350,  450}, // dwarf
            {  175,  550,  875, 1200, 1600}, //elf
            {   90,  300,  450,  600,  750}, // gnome
            {   40,  100,  175,  250,  325}, // half elf
            { 0x21, 0x44, 0x65, 0x90, 0xC7}, // halfling  
            { 0x0F, 0x1E, 0x2D, 0x3C, 0x50}, // half orc
            { 0x14, 0x28, 0x3C, 0x5A, 0x78} }; // human       

        public static int[] StrAgeEffect = { 0, 1, -1, -2, -1 };
        public static int[] Str00AgeEffect = { 0, 0, 0, 0, 0 };
        public static int[] IntAgeEffect = { 0, 0, 1, 0, 1 };
        public static int[] WisAgeEffect = { -1, 1, 1, 1, 1 };
        public static int[] DexAgeEffect = { 0, 0, 0, -2, -1 };
        public static int[] ConAgeEffect = { 1, 0, -1, -1, -1 };
        public static int[] ChaAgeEffect = { 0, 0, 0, 0, 0 };


        public static int[, ,] StrRaceSexMinMax = {
            { {0, 5}, {10,  0} },
            { {8, 8}, {18, 17} },
            { {3, 3}, {18, 16} },
            { {6, 6}, {18, 15} }, 
            { {3, 3}, {18, 17} },
            { {6, 6}, {17, 14} },
            { {6, 6}, {18, 18} }, 
            { {3, 3}, {18, 18} } };

        public static int[, ,] Str00RaceSexMinMax = {
            { {0, 0}, {  5,  5} }, 
            { {0, 0}, { 99,  0} },
            { {0, 0}, { 75,  0} },
            { {0, 0}, { 50,  0} },
            { {0, 0}, { 90,  0} },
            { {0, 0}, {  0,  0} }, 
            { {0, 0}, { 99, 75} },
            { {0, 0}, {100, 50} } };

        public static int[, ,] IntRaceSexMinMax = {
            { {10, 10}, {15, 15} } ,
            { { 3,  3}, {18, 18} } ,
            { { 8,  8}, {18, 18} } ,
            { { 7,  7}, {18, 18} } ,
            { { 4,  4}, {18, 18} } ,
            { { 6,  6}, {18, 18} } ,
            { { 3,  3}, {17, 17} } ,
            { { 3,  3}, {18, 18} } };

        public static int[, ,] WisRaceSexMinMax = {
            { {5, 5}, {10, 10} },
            { {3, 3}, {18, 18} },
            { {3, 3}, {18, 18} },
            { {3, 3}, {18, 18} },
            { {3, 3}, {18, 18} },
            { {3, 3}, {17, 17} },
            { {3, 3}, {14, 14} },
            { {3, 3}, {18, 18} } };

        public static int[, ,] DexRaceSexMinMax = {
            { {10, 10}, {15, 15} },
            { { 3,  3}, {17, 17} },
            { { 7,  7}, {19, 19} },
            { { 3,  3}, {18, 18} },
            { { 6,  6}, {18, 18} },
            { { 8,  8}, {18, 18} },
            { { 3,  3}, {17, 17} },
            { { 3,  3}, {18, 18} } };

        public static int[, ,] ConRaceSexMinMax = {
            { {20, 20}, {10, 10} },
            { {12, 12}, {19, 19} },
            { { 6,  6}, {18, 18} },
            { { 8,  8}, {18, 18} },
            { { 6,  6}, {18, 18} },
            { {10, 10}, {19, 19} },
            { {13, 13}, {19, 19} },
            { { 3,  3}, {18, 18} } };

        public static int[, ,] ChaRaceSexMinMax = {
             { {12, 12}, {12, 12} },
             { { 3,  3}, {16, 16} },
             { { 8,  8}, {18, 18} },
             { { 3,  3}, {18, 18} },
             { { 3,  3}, {18, 18} },
             { { 3,  3}, {18, 18} },
             { { 3,  3}, {12, 12} },
             { { 3,  3}, {18, 18} } };

        public static int[] StrClassMin = { 6, 0, 9, 12, 13, 0, 6, 15, 9, 9, 0, 0, 0, 9, 9, 9, 0 };
        public static int[] IntClassMin = { 6, 0, 0, 9, 13, 9, 6, 0, 0, 9, 13, 9, 0, 9, 0, 9, 9 };
        public static int[] WisClassMin = { 9, 12, 6, 13, 14, 6, 0, 15, 9, 9, 14, 9, 9, 0, 0, 0, 0 };
        public static int[] DexClassMin = { 0, 0, 6, 0, 0, 6, 9, 15, 0, 0, 0, 0, 9, 0, 9, 9, 9 };
        public static int[] ConClassMin = { 0, 0, 7, 9, 14, 0, 0, 11, 0, 0, 14, 0, 0, 0, 0, 0, 0 };
        public static int[] ChaClassMin = { 0, 15, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[] Str00ClassMin = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static bool RaceClassLimit(int class_lvl, Player player, ClassId _class)
        {
            bool race_limited = false;

            switch (player.race)
            {
                case Race.dwarf:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 9 ||
                            (class_lvl == 8 && player.stats2.Str.full == 17) ||
                            (class_lvl == 7 && player.stats2.Str.full < 17))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.elf:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 7 ||
                            (class_lvl == 6 && player.stats2.Str.full == 17) ||
                            (class_lvl == 5 && player.stats2.Str.full < 17))
                        {
                            race_limited = true;
                        }
                    }

                    if (_class == ClassId.magic_user)
                    {
                        if (class_lvl == 11 ||
                            (class_lvl == 9 && player.stats2.Int.full < 17) ||
                            (class_lvl == 10 && player.stats2.Int.full == 17))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.gnome:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 6 ||
                            (class_lvl == 5 && player.stats2.Str.full < 18))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.half_elf:
                    if (_class == ClassId.cleric && class_lvl == 5)
                    {
                        race_limited = true;
                    }
                    else
                    {
                        if (_class == ClassId.fighter || _class == ClassId.ranger)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.stats2.Str.full == 17) ||
                                (class_lvl == 6 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }

                        if (_class == ClassId.magic_user)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.stats2.Str.full == 17) ||
                                (class_lvl == 6 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }
                    }
                    break;

                case Race.halfling:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 6 ||
                            (class_lvl == 5 && player.stats2.Str.full == 17) ||
                            (class_lvl == 4 && player.stats2.Str.full < 17))
                        {
                            race_limited = true;
                        }
                    }

                    break;

            }

            if (Cheats.no_race_level_limits)
            {
                race_limited = false;
            }

            return race_limited;
        }

        public static bool RaceStatLevelRestricted(ClassId _class, Player player) // sub_69138
        {
            bool race_limited = false;

            int class_lvl = player.ClassLevel[(int)_class];

            if (class_lvl > 0)
            {
                switch (player.race)
                {
                    case Race.dwarf:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 8 && player.stats2.Str.full == 17) ||
                                (class_lvl == 7 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }
                        break;

                    case Race.elf:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 7) ||
                                (class_lvl == 6 && player.stats2.Str.full == 17) ||
                                (class_lvl == 5 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }
                        break;

                    case Race.gnome:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 6) ||
                                (class_lvl == 5 && player.stats2.Str.full < 18))
                            {
                                race_limited = true;
                            }
                        }
                        break;

                    case Race.half_elf:
                        if (_class == ClassId.cleric &&
                            class_lvl == 5)
                        {
                            race_limited = true;
                        }
                        else if (_class == ClassId.fighter)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.stats2.Str.full == 17) ||
                                (class_lvl == 6 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }
                        break;

                    case Race.halfling:
                        if (_class == ClassId.fighter)
                        {
                            if ((class_lvl == 6) ||
                                (class_lvl == 5 && player.stats2.Str.full == 17) ||
                                (class_lvl == 4 && player.stats2.Str.full < 17))
                            {
                                race_limited = true;
                            }
                        }
                        break;
                }
            }

            if (Cheats.no_race_level_limits)
            {
                race_limited = false;
            }

            return race_limited;
        }
    }
}
