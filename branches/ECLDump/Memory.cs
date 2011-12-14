using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EclDump
{
    partial class Program
    {
        internal static EclOpp[] LoadEclOpps(int numberOfSets)
        {
            int strIndex = 0;

            List<EclOpp> opps = new List<EclOpp>();

            for (int loop_var = 1; loop_var <= numberOfSets; loop_var++)
            {
                byte code = ecl_ptr[0x8000 + ecl_offset + 1];
                byte low = ecl_ptr[0x8000 + ecl_offset + 2];

                EclOpp curOpp = new EclOpp(code, low);

                ecl_offset += 2;

                if (code == 1 || code == 2 || code == 3)
                {
                    ecl_offset++;
                    byte high = ecl_ptr[0x8000 + ecl_offset];

                    curOpp.SetHigh(high);
                }
                else if (code == 0x80) // Load compressed string
                {
                    strIndex++;

                    short strLen = low;

                    if (strLen > 0)
                    {
                        LoadCompressedEclString(strIndex, strLen);
                    }
                    else
                    {
                        stringTable[strIndex] = string.Empty;
                    }
                }
                else if (code == 0x81)
                {
                    strIndex++;
                    ecl_offset++;
                    byte high = ecl_ptr[0x8000 + ecl_offset];

                    curOpp.SetHigh(high);

                    ushort loc = curOpp.Word;

                    stringTable[strIndex] = CopyStringFromMemory(loc);
                }
                else
                {
                }

                opps.Add(curOpp);
            }
            ecl_offset++;

            return opps.ToArray();
        }

        internal static string CopyStringFromMemory(int loc)
        {
            if (loc >= 0x4B00 && loc <= 0x4EFF)
            {
                return string.Format("<string starting at {0}>", Area1Name(0x6A00 + (loc * 2)));
            }
            else if (loc >= 0x7C00 && loc <= 0x7FFF)
            {
                if (loc == 0x7C00)
                {
                    return "<selected player Name>";
                }
                else
                {
                    return string.Format("<string starting at {0}>", Area2Name(0x800 + (loc * 2)));
                }
            }
            else if (loc >= 0x7A00 && loc <= 0x7BFF)
            {
                return string.Format("<string starting at stru_1B2CA.word_{0:X4}>", (((loc * 2) + 0x0C00) & 0xffff));
            }
            else if (loc >= 0x8000 && loc <= 0x9DFF)
            {
                var sb = new System.Text.StringBuilder();
                int offset = 0;
                while (ecl_ptr[offset + loc + 0x8000] != 0)
                {
                    sb.Append((char)ecl_ptr[offset + loc + 0x8000]);
                    offset++;
                } 

                return string.Format("<string starting at ecl byte 0x{0:X4}>{1}", (loc + 0x8000) & 0xffff, sb.ToString());
            }
            else
            {
                return "<empty string>";
            }
        }

        internal static string GetMemoryValue(int loc)
        {
            if (loc >= 0x4B00 && loc <= 0x4EFF)
            {
                return Area1Name(0x6A00 + (loc * 2));
            }
            else if (loc >= 0x7C00 && loc <= 0x7FFF)
            {
                var playerString = get_player_values(loc);
                if (playerString != "")
                {
                    return playerString;
                }
                else
                {
                    return Area2Name(0x800 + (loc * 2));
                }
            }
            else if (loc >= 0x7A00 && loc <= 0x7BFF)
            {
                return string.Format("<stru_1B2CA.word_{0:X4}>", (((loc * 2) + 0x0C00) & 0xffff));
            }
            else if (loc >= 0x8000 && loc <= 0x9DFF)
            {
                return string.Format("<ecl byte {0}>{1}", (loc + 0x8000) & 0xffff, ecl_ptr[loc + 0x8000]);
            }
            else
            {
                if (loc < 0xC04B)
                {
                    switch (loc)
                    {
                        case 0x00B1:
                            return "<word_1D918>";

                        case 0x00FB:
                            return "<word_1D914>";

                        case 0x00FC:
                            return "<word_1D916>";

                        case 0x033D:
                            return "<mapDirection>";

                        case 0x035F:
                            return "0";
                    }
                }
                else
                {
                    loc -= 0xC04B;

                    switch (loc)
                    {
                        case 0:
                            return "<mapPosX>";

                        case 0x01:
                            return "<mapPosY>";

                        case 0x02:
                            return "<gbl.mapDirection / 2>";

                        case 0x03:
                            return "<mapWallType>";

                        case 0x04:
                            return "<mapWallRoof>";

                        case 0x0E:
                            return "0";
                    }
                }
            }

            return "<BAD LOCATION>";
        }


        internal static string get_player_values(int loc)
        {
            loc -= 0x7c00;

            if (loc == 0x15)
            {
                return "<selected player INT>";
            }
            else if (loc == 0x18)
            {
                return "<selected player CON>";
            }
            else if (loc == 0x72)
            {
                return "<selected player Race>";
            }
            else if (loc == 0x73)
            {
                return "<selected player Class>";
            }
            else if (loc == 0x9b)
            {
                return "<selected player SaveVerseType1>";
            }
            else if (loc == 0xa0)
            {
                return "<selected player HitDice>";
            }
            else if (loc >= 0xA5 && loc <= 0xAC)
            {
                return string.Format("<selected palyer Field_EA[{0}]>", (loc - 0xA4) - 1);
            }
            else if (loc == 0xb8)
            {
                return "<selected player Morale>";
            }
            else if (loc == 0xBB)
            {
                return "<selected player Copper Coins>";
            }
            else if (loc == 0xBD)
            {
                return "<selected player Electrum Coins>";
            }
            else if (loc == 0xBF)
            {
                return "<selected player Silver Coins>";
            }
            else if (loc == 0xC1)
            {
                return "<selected player Gold Coins>";
            }
            else if (loc == 0xC3)
            {
                return "<selected player Platinum Coins>";
            }
            else if (loc == 0xC9)
            {
                return "<selected player MagicUser Level>";
            }
            else if (loc == 0xD6)
            {
                return "<selected player Sex>";
            }
            else if (loc == 0xD8)
            {
                return "<selected player Alignment>";
            }
            else if (loc == 0xE4)
            {
                return "<selected player field_192 & 1>";
            }
            else if (loc == 0xF7)
            {
                return "<selected player field_13C>";
            }
            else if (loc == 0xF9)
            {
                return "<selected player field_13E>";
            }
            else if (loc == 0x100)
            {
                return "<selected player in_combat (true 1, false 0x80, player not found 0)>";
            }
            else if (loc == 0x10C)
            {
                return "<selected player (our team && quick fight 0x80, enemy team 0x81, else 0)>";
            }
            else if (loc == 0x10D)
            {
                return "0";
            }
            else if (loc == 0x11B)
            {
                return "<selected player movement>";
            }
            else if (loc == 0x2B1)
            {
                return "<selected player player_index>";
            }
            else if (loc == 0x2B4)
            {
                return "<selected player player_index>";
            }
            else if (loc == 0x2CF)
            {
                return "<selected player charisma (scaled 0-60)>";
            }
            else if (loc == 0x312)
            {
                return "<game area>";
            }
            else if (loc == 0x33E)
            {
                return "<party size>";
            }
            else
            {
                return "";
            }
        }

        static string WriteStringToMemory(int loc)
        {
            if (loc >= 0x4B00 && loc <= 0x4EFF)
            {
                return string.Format("<string starting at {0}>", Area1Name(0x6A00 + (loc * 2)));
            }
            else if (loc >= 0x7C00 && loc <= 0x7FFF)
            {                
                if (loc == 0x7C00)
                {
                    return "<slected player Name>";
                }
                else
                {
                    return string.Format("<string starting at {0}>", Area2Name(0x800 + (loc * 2)));
                }
            }
            else if (loc >= 0x7A00 && loc <= 0x7BFF)
            {
                return string.Format("<string starting at stru_1B2CA.word_{0:X4}>", (((loc * 2) + 0x0C00) & 0xffff));
            }
            else if (loc >= 0x8000 && loc <= 0x9DFF)
            {
                return string.Format("<string starting at ecl byte {0}>", (loc + 0x8000) & 0xffff);
            }
            else
            {
                return "</user/bin>";
            }
        }

        static string SetMemoryValue(int loc, string value)
        {
            if (loc >= 0x4B00 && loc <= 0x4EFF)
            {
                string extra = "";
                if ((loc - 0x4B00) == 0x0FD || (loc - 0x4B00) == 0x0FE)
                {
                    extra = ", <byte_1EE94> = true";
                }

                return string.Format("<{0}> = {1}{2}", Area1Name(0x6A00 + (loc * 2)), value, extra);
            }
            else if (loc >= 0x7C00 && loc <= 0x7FFF)
            {
                string area2txt = string.Format("<{0}> = {1}", Area2Name(0x800 + (loc * 2)), value);
                string altertxt = alter_character(value, loc);
                return area2txt + ", " + altertxt;
            }
            else if (loc >= 0x7A00 && loc <= 0x7BFF)
            {
                return string.Format("<stru_1B2CA.word_{0:X4}> = {1}", (((loc * 2) + 0x0C00) & 0xffff), value);
            }
            else if (loc >= 0x8000 && loc <= 0x9DFF)
            {
                return string.Format("<ecl byte {0}> = {1}", (loc + 0x8000) & 0xffff, value);    
            }
            else
            {
                if (loc < 0xBF68)
                {
                    switch (loc)
                    {
                        case 0xFB:
                            return "nop";

                        case 0xFC:
                            return "nop";

                        case 0xB1:
                            return "nop";

                        case 0x3DE:
                            return string.Format("<word_1EE76> = {0}", value);

                        case 0xB8:
                            return string.Format("<word_1EE78> = {0}", value);

                        case 0xB9:
                            return string.Format("<word_1EE7A> = {0}", value);
                    }
                }
                else
                {
                    loc -= 0xBF68;

                    switch (loc)
                    {
                        case 0xE3:
                            return string.Format("<mapPosX> = {0}", value);

                        case 0xE4:
                            return string.Format("<mapPosY> = {0}", value);

                        case 0xE5:
                            return string.Format("<mapDirection> = mapped({0}, [0,2,4,6,0,2,4,6])", value);

                        case 0xF1:
                        case 0xF7:
                            return string.Format("<byte_1EE91> = true");
                    }
                }
            }

            return "";

        }

        private static string alter_character(string value, int location)
        {
            int loc = location - 0x7C00;

            if (loc >= 0x20 && loc <= 0x70)
			{
				int spellSlot = loc - 0x1f;
                return string.Format("<slected player spell> = {0}", value);
			}
            else if (loc == 0xb8)
			{
                return string.Format("<slected player Morale> =  {0} [if value > 0xB2 then value -= 0x32]", value);
			}
            else if (loc == 0xbb)
			{
                return string.Format("<selected player Copper> = {0}", value);
			}
            else if (loc == 0xbd)
			{
                return string.Format("<selected player Electrum> = {0}", value);
			}
            else if (loc == 0xbf)
			{
                return string.Format("<selected player Silver> = {0}", value);
			}
            else if (loc == 0xc1)
			{
                return string.Format("<selected player Gold> = {0}", value);
			}
            else if (loc == 0xc3)
			{
                return string.Format("<selected player Platinum> = {0}", value);
			}
            else if (loc == 0xf7)
			{
                return string.Format("<selected player field_13C> = {0}", value);
			}
            else if (loc == 0xf9)
			{
                return string.Format("<selected player field_13E> = {0}", value);
			}
            else if (loc == 0x100)
			{
                return string.Format("<selected player in_combat> = false [if value '{0}' >= 0x80, also if value == 0x87 the health_status = stoned]", value);
			}
            else if (loc == 0x10c)
			{
                return string.Format("<selected player CombatTeam & QuickFight> = {0} [0 - Ours & Not Quick, 0x80 - Ours & Quick, 0x81 - Enemy & Quick]", value);
            }
            else if (loc == 0x312)
			{
                return string.Format("<game area> = {0}", value);
			}
			else if (loc == 0x322)
			{
                return string.Format("<if value > 0x80 set WALLDEF 1 value & 0x7F> value: {0}", value);
			}
            else if (loc == 0x324)
			{
                return string.Format("<if value > 0x80 set WALLDEF 2 value & 0x7F> value: {0}", value);
			}
            else if (loc == 0x326)
			{
                return string.Format("<if value > 0x80 set WALLDEF 3 value & 0x7F> value: {0}", value);
			}

            return "";
		}

        private static string Area2Name(int addr)
        {
            int loc = addr & 0xFFFF;
            switch (loc)
            {
                case 0x550: return "area2.training_class_mask";
                case 0x580: return "area2.max_encounter_distance";
                case 0x582: return "area2.encounter_distance";
                case 0x594: return "area2.search_flags";
                case 0x5A4: return "area2.rest_incounter_period";
                case 0x5A6: return "area2.rest_incounter_percentage";
                case 0x5AA: return "area2.tried_to_exit_map";
                case 0x5C2: return "area2.HeadBlockId";
                case 0x5C4: return "area2.EnterTemple";
                case 0x5CC: return "area2.isDuel";
                case 0x624: return "area2.game_area";
                case 0x67C: return "area2.party_size";
                case 0x6D8: return "area2.EnterShop";
            }

            return string.Format("area2.word_{0:X3}", loc);
        }

        private static string Area1Name(int addr)
        {
            int loc = addr & 0xFFFF;

            switch (loc)
            {
                case 0x18E: return "area1.time_minutes_ones";
                case 0x190: return "area1.time_minutes_tens";
                case 0x192: return "area1.time_hour";
                case 0x194: return "area1.time_day";
                case 0x196: return "area1.time_year";
                case 0x1CC: return "area1.inDungeon";
                case 0x1E0: return "area1.lastXPos";
                case 0x1E2: return "area1.lastYPos";
                case 0x1E4: return "area1.LastEclBlockId";
                case 0x1F6: return "area1.block_area_view";
                case 0x1F8: return "area1.game_speed";
                case 0x1FA: return "area1.outdoor_sky_colour";
                case 0x1FC: return "area1.indoor_sky_colour";
                case 0x342: return "area1.current_city";
                case 0x3fe: return "area1.picture_fade";
            }

            return string.Format("area1.word_{0:X3}", addr & 0xffff);
        }



        internal static void LoadCompressedEclString(int strIndex, int inputLength)
        {
            byte[] data = new byte[inputLength];

            for (int i = 0; i < inputLength; i++)
            {
                data[i] = ecl_ptr[ecl_offset + 0x8000 + 1 + i];
            }

            ecl_offset += (ushort)inputLength;

            stringTable[strIndex] = DecompressString(data);
        }

        internal static string DecompressString(byte[] data)
        {
            var sb = new System.Text.StringBuilder();
            int state = 1;
            uint lastByte = 0;

            foreach (uint thisByte in data)
            {
                uint curr = 0;
                switch (state)
                {
                    case 1:
                        curr = (thisByte >> 2) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 2;
                        break;

                    case 2:
                        curr = ((lastByte << 4) | (thisByte >> 4)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 3;
                        break;

                    case 3:
                        curr = ((lastByte << 2) | (thisByte >> 6)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));

                        curr = thisByte & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 1;
                        break;
                }
                lastByte = thisByte;
            }

            return sb.ToString();
        }


        internal static char inflateChar(uint arg_0)
        {
            if (arg_0 <= 0x1f)
            {
                arg_0 += 0x40;
            }

            return (char)arg_0;
        }
    }


    public class EclOpp
    {
        bool wordSet;
        int word;
        int high;
        int low;
        int code;

        public EclOpp(byte _code, byte _low)
        {
            code = _code;
            low = _low;
        }

        public void SetHigh(byte _high)
        {
            high = _high;
            word = (high << 8) | (low);
            wordSet = true;
        }

        public ushort Word
        {
            get
            {
                if (wordSet)
                {
                    return (ushort)word;
                }
                else
                {
                    //throw new Exception();
                    return (ushort)low;
                }
            }
        }

        public byte Code
        {
            get
            {
                return (byte)code;
            }
        }



        public string GetCmdValue()
        {
            switch (code)
            {
                case 0x00:
                    return low.ToString();

                case 0x01:
                case 0x03:
                case 0x80:
                    if (wordSet)
                        return Program.GetMemoryValue(word);
                    return "<opp not word loaded>";

                case 0x02:
                case 0x81:
                    if (wordSet)
                        return word.ToString();
                    return "<opp not word loaded>";

                default:
                    return "<invalid code " + code.ToString() + " >";
            }
        }
    }

    public class EclData
    {
        byte[] data;
        public EclData(byte[] _data)
        {
            data = new byte[_data.Length - 2];
            System.Array.Copy(_data, 2, data, 0, _data.Length - 2);
        }

        public byte this[int index]
        {
            get
            {
                // simulate the 16 bit memory space.
                int loc = index & 0xFFFF;
                if (loc < data.Length)

                    return data[loc];
                else
                    return 0xff;
            }
        }

        internal bool IsValidAddr(int addr)
        {
            int loc = addr & 0xFFFF;

            return loc < data.Length;
        }
    }

}
