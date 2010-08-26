using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DaxDump
{
    partial class Program
    {
        internal static string CMD_Exit()
        {
            //gbl.encounter_flags[0] = false;
            //gbl.encounter_flags[1] = false;

            if (skipNext == false) stopVM = true;
            ecl_offset++;

            return "";
        }


        internal static string CMD_Goto()
        {
            EclOpp[] opps = LoadEclOpps(1);
            int newOffset = opps[0].Word;

            AddAddr(newOffset);

            return string.Format("0x{0:X4}", newOffset);
        }

        internal static string CMD_Gosub()
        {
            EclOpp[] opps = LoadEclOpps(1);
            int newOffset = opps[0].Word;

            AddAddr(newOffset);

            //gbl.vmCallStack.Push(gbl.ecl_offset);
            //gbl.ecl_offset = newOffset;

            return string.Format("0x{0:X4}", newOffset);
        }


        internal static string CMD_Compare()
        {
            EclOpp[] opps = LoadEclOpps(2);

            if (opps[0].Code >= 0x80 ||
                opps[1].Code >= 0x80)
            {
                return string.Format("Strings: '{0}' == '{1}'", stringTable[2], stringTable[1]);
            }
            else
            {
                string value_a = opps[0].GetCmdValue();
                string value_b = opps[1].GetCmdValue();

                return string.Format("Values: '{0}' == '{1}'", value_b, value_a);
            }
        }

        internal static string CMD_Add()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue();
            string val_b = opps[1].GetCmdValue();

            ushort location = opps[2].Word;

            return SetMemoryValue(location, string.Format("{0} + {1}", val_a, val_b));
        }

        internal static string CMD_CompareAnd()
        {
            EclOpp[] opps = LoadEclOpps(4);

            string val_8 = opps[0].GetCmdValue();
            string val_6 = opps[1].GetCmdValue();
            string val_4 = opps[2].GetCmdValue();
            string val_2 = opps[3].GetCmdValue();

            return string.Format("Values: '{0}' == '{1}' && '{2}' == '{3}'", val_8, val_6, val_4, val_2);
        }

        internal static string CMD_Sub()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue();
            string val_b = opps[1].GetCmdValue();

            ushort location = opps[2].Word;

            return SetMemoryValue(location, string.Format("{1} - {0}", val_a, val_b));
        }

        internal static string CMD_Div()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue();
            string val_b = opps[1].GetCmdValue();

            ushort location = opps[2].Word;

            return SetMemoryValue(location, string.Format("{0} / {1}", val_a, val_b)) + string.Format(", area2_ptr.field_67E = {0} % {1}", val_a, val_b);
        }

        internal static string CMD_Multi()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue();
            string val_b = opps[1].GetCmdValue();

            ushort location = opps[2].Word;

            string val = string.Format("{0} * {1}", val_a, val_b);

            return SetMemoryValue(location, val);
        }


        internal static string CMD_Random()
        {
            EclOpp[] opps = LoadEclOpps(2);

            string rand_max = opps[0].GetCmdValue();

            ushort loc = opps[1].Word;
            return SetMemoryValue(loc, string.Format("1 to {0} (if max < 255 then max = max + 1)", rand_max));
        }


        internal static string CMD_Print()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string txt = opps[0].Code < 0x80 ? opps[0].GetCmdValue() : stringTable[1];

            return string.Format("'{0}'", txt);
        }

        internal static string CMD_Return()
        {
            ecl_offset++;

            if (skipNext == false) stopVM = true;
            ecl_offset++;

            return "";
        }

        internal static string CMD_ClearMonsters()
        {
            ecl_offset++;

            return "";
        }

        internal static string CMD_Protection()
        {
            EclOpp[] opps = LoadEclOpps(1);

            return "";
        }

        internal static string CMD_If()
        {
            ecl_offset++;
            skipNext = true;

            return string.Format("<if compare flag is false, skip next opp>");
        }

        internal static string CMD_Dump()
        {
            ecl_offset++;

            return "<drop current selected player>";
        }


        internal static string CMD_InputString()
        {
            EclOpp[] opps = LoadEclOpps(2);

            int loc = opps[1].Word;
            string loc_txt = WriteStringToMemory(loc);

            return string.Format("{0} = <user input string>", loc_txt);
        }

        internal static string CMD_InputNumber()
        {
            EclOpp[] opps = LoadEclOpps(2);

            int loc = opps[1].Word;

            return SetMemoryValue(loc, "<user input number>");
        }

        internal static string CMD_Approach()
        {
            ecl_offset++;

            return "";
        }

        internal static string CMD_Save()
        {
            EclOpp[] opps = LoadEclOpps(2);

            int loc = opps[1].Word;

            if (opps[0].Code < 0x80)
            {
                string val = opps[0].GetCmdValue();
                return SetMemoryValue(loc, val);
            }
            else
            {
                string val = stringTable[1];
                string loc_txt = WriteStringToMemory(loc);

                return string.Format("{0} = '{1}'", loc_txt, val);
            }
        }


        internal static string CMD_SetupMonster()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string sprite_id = opps[0].GetCmdValue();
            string max_distance = opps[1].GetCmdValue();
            string pic_id = opps[2].GetCmdValue();

            return string.Format("sprite_id: {0} max_distance: {1} pic_id: {2}", sprite_id, max_distance, pic_id);
        }


        internal static string CMD_LoadCharacter()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string player_index = opps[0].GetCmdValue();

            return string.Format("select player_index: {0}", player_index);
        }

        internal static string CMD_FindItem()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string item_type = opps[0].GetCmdValue();

            return string.Format("<find if any team member has item type {0}>", item_type);
        }

        internal static string CMD_LoadFiles()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string var_3 = opps[0].GetCmdValue();
            string var_2 = opps[1].GetCmdValue();
            string var_1 = opps[2].GetCmdValue();

            return string.Format("Load GEO {0}", var_3);
        }

        internal static string CMD_LoadPieces()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string var_3 = opps[0].GetCmdValue();
            string var_2 = opps[1].GetCmdValue();
            string var_1 = opps[2].GetCmdValue();

            return string.Format("Load WALLDEF {0} {1} {2}", var_1, var_2, var_3);
        }

        internal static string CMD_Picture() /* sub_26873 */
        {
            EclOpp[] opps = LoadEclOpps(1);
            string blockId = opps[0].GetCmdValue();

            int val;
            if (int.TryParse(blockId, out val))
            {
                if (val != 0xff)
                {
                    return string.Format("blockId: 0x{0:X2} <if area2_ptr.HeadBlockId == 0xFF ? <if blockId >= 0x78 ? <Load BIGPIC> : <Load PIC>> : <Load BODY>", val);
                }
                else
                {
                    return string.Format("<redraw screen>");
                }
            }
            return string.Format("{0} <load BIGPIC, PIC, BODY or redraw based on value>", blockId);
        }

        internal static string CMD_VertMenu()
        {
            EclOpp[] opps = LoadEclOpps(3);
            int mem_loc = opps[0].Word;

            string delay_text = stringTable[1];

            //string menuCount = opps[2].GetCmdValue();
            int menuCount = opps[2].Word;

            ecl_offset--;

            opps = LoadEclOpps(menuCount);

            StringBuilder menutext = new StringBuilder();
            bool addComma = false;
            for (int i = 0; i < menuCount; i++)
            {
                if (addComma) menutext.Append(", ");
                menutext.Append("'");
                menutext.Append(stringTable[i + 1]);
                menutext.Append("'");
                addComma = true;
            }

            return SetMemoryValue(mem_loc, "<selected index from menu>") + " MENU:" + menutext.ToString();

            //ovr008.vm_SetMemoryValue((ushort)index, mem_loc);

        }

        internal static string CMD_HorizontalMenu()
        {
            EclOpp[] opps = LoadEclOpps(2);

            ushort loc = opps[0].Word;
            //byte string_count = opps[1].GetCmdValue();
            int menuCount = opps[1].Word;

            ecl_offset--;

            opps = LoadEclOpps(menuCount);

            StringBuilder menutext = new StringBuilder();
            bool addComma = false;
            for (int i = 0; i < menuCount; i++)
            {
                if (addComma) menutext.Append(", ");
                menutext.Append("'");
                menutext.Append(stringTable[i + 1]);
                menutext.Append("'");
                addComma = true;
            }

            return SetMemoryValue(loc, "<selected index from menu>") + " MENU:" + menutext.ToString();
        }

        internal static string CMD_Program()
        {
            EclOpp[] opps = LoadEclOpps(1);

            //string var_1 = opps[0].GetCmdValue();
            int var_1 = opps[0].Word;

            switch (var_1)
            {
                case 0: return "StartGameMenu";
                case 8: return "GameWon (exit game)";
                case 9:
                    if (skipNext == false) stopVM = true;
                    return "TryEncamp (exit ecl)";
                case 3:
                    if (skipNext == false) stopVM = true;
                    return "PartyKilled (exit ecl)";
            }

            return var_1.ToString();
        }

        internal static string CMD_LoadMonster()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string modId = opps[0].GetCmdValue();
            string numCopies = opps[1].GetCmdValue();
            string iconId = opps[2].GetCmdValue();

            return string.Format("Id: {0} Copies: {1} CPIC id: {2}", modId, numCopies, iconId);
        }

        internal static string CMD_Who()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string prompt = stringTable[1];

            return string.Format("<Select Player> {0}", prompt);
        }

        internal static string CMD_DestroyItems()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string item_type = opps[0].GetCmdValue();

            return string.Format("<remove type {0} items from party>", item_type);
        }

        internal static string CMD_FindSpecial()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string affect_type = opps[0].GetCmdValue();

            return string.Format("<find if select player has affect: {0} >", affect_type);
        }

        internal static string CMD_ClearBox()
        {
            ecl_offset++;
            return "";
        }

        internal static string CMD_Delay()
        {
            ecl_offset++;
            return "";
        }

        internal static string CMD_EclClock()
        {
            EclOpp[] opps = LoadEclOpps(2);
            string timeStep = opps[0].GetCmdValue();
            string timeSlot = opps[1].GetCmdValue();

            return string.Format("slot: {0} step: {1}", timeSlot, timeStep);
        }

        internal static string CMD_SpriteOff()
        {
            ecl_offset++;
            return "";
        }


        internal static string CMD_Damage() /* sub_28958 */
        {
            EclOpp[] opps = LoadEclOpps(5);

            //string var_1 = opps[0].GetCmdValue();
            int var_1 = opps[0].Word;
            string var_2 = opps[1].GetCmdValue();
            string var_3 = opps[2].GetCmdValue();
            string var_7 = opps[3].GetCmdValue();
            //string var_6 = opps[4].GetCmdValue();
            int var_6 = opps[4].Word;

            string damage = var_2 + "D" + var_3 + "+" + var_7;

            if ((var_1 & 0x80) != 0)
            {
                int saveBonus = var_1 & 0x1f;
                int bonusType = var_6 & 7;

                if ((var_1 & 0x40) != 0)
                {
                    if (((var_1 & 0x20) != 0) || ((var_1 & 0x10) != 0))
                    {
                        return string.Format("<all team damaged for {0}>", damage);
                    }
                    else
                    {
                        return string.Format("<all team damaged for {0} if idv. save failed>", damage);
                    }
                }
                else
                {
                    if ((var_6 & 0x80) != 0)
                    {
                        if (bonusType == 0 || (var_1 & 0x10) != 0)
                        {
                            return string.Format("<selected player damaged for {0}>", damage);
                        }
                        else
                        {
                            return string.Format("<selected player damaged for {0} if save failed>", damage);
                        }
                    }
                    else
                    {
                        if ((var_1 & 0x10) != 0)
                        {
                            return string.Format("<random team member damaged for {0}>", damage);
                        }
                        else
                        {
                            return string.Format("<random team member damaged for {0} if save failed>", damage);
                        }
                    }
                }
            }
            else
            {
                return string.Format("<do {0} attacks of {1} on random team member>", var_1, damage);
            }
        }

        internal static string CMD_PrintReturn()
        {
            ecl_offset++;
            return "";
        }

        internal static string CMD_NewECL()
        {
            EclOpp[] opps = LoadEclOpps(1);

            string block_id = opps[0].GetCmdValue();
            if (skipNext == false) stopVM = true;

            return string.Format("<load ECL {0} (exit ecl)>", block_id);
        }

        internal static string CMD_And()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue(); ;
            string val_b = opps[1].GetCmdValue(); ;

            ushort loc = opps[2].Word;

            return SetMemoryValue(loc, string.Format("{0} & {1} <bitwise and>", val_a, val_b)) + ", + result compared to zero";

        }
        internal static string CMD_Or()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string val_a = opps[0].GetCmdValue(); ;
            string val_b = opps[1].GetCmdValue(); ;

            ushort loc = opps[2].Word;

            return SetMemoryValue(loc, string.Format("{0} | {1} <bitwise or>", val_a, val_b)) + ", + result compared to zero";
        }

        internal static string CMD_Call()
        {
            EclOpp[] opps = LoadEclOpps(1);

            int var_2 = opps[0].Word;
            int var_4 = (var_2 - 0x7fff) & 0xFFFF;

            switch (var_4)
            {
                case 0xAE11: return "<reload mapWallRoof>";
                case 1: return "<setup duel vs. ROLF(self)>";
                case 2: return "<setup duel>";
                case 0x3201: return "<play sound 10 or 11 based on word_1EE76>";
                case 0x401F: return "<walk party foward>";
                case 0x4019: return "<reload mapWallType, if not in dungeon>";
                case 0xE804: return "<draw animated sprite, and move to next frame>";
            }

            return string.Format("?? {0:X4} {1:X4}", var_2, var_4);
        }

        internal static string CMD_OnGoto()
        {
            EclOpp[] opps = LoadEclOpps(2);

            string var_1 = opps[0].GetCmdValue();
            //string var_2 = opps[1].GetCmdValue();
            int var_2 = opps[1].Word;

            ecl_offset--;
            opps = LoadEclOpps(var_2);

            StringBuilder sb = new StringBuilder();
            bool addComma = false;
            for (int i = 0; i < var_2; i++)
            {
                if (addComma) sb.Append(", ");
                sb.Append(string.Format("0x{0:X4}", opps[i].Word));
                addComma = true;

                AddAddr(opps[i].Word);
            }

            return string.Format("{0} of [{1}]", var_1, sb.ToString());
        }

        internal static string CMD_Combat()
        {
            ecl_offset++;

            return "<if monstersLoaded == false && combat_type == normal ? [<if area2.EnterShop >? CityShop() : [<if area2.EnterTemple > ? TempleShop() : AfterCombatExpAndTreasure() ]] : DoCombat()>";
        }

        internal static string CMD_Surprise()
        {
            EclOpp[] opps = LoadEclOpps(4);

            return SetMemoryValue(0x2CB, string.Format("{0} {1} {2} {3}", opps[0].GetCmdValue(), opps[1].GetCmdValue(), opps[2].GetCmdValue(), opps[4].GetCmdValue()));

            //byte var_8 = (byte)ovr008.vm_GetCmdValue(1);
            //byte var_7 = (byte)ovr008.vm_GetCmdValue(2);
            //byte var_6 = (byte)ovr008.vm_GetCmdValue(3);
            //byte var_5 = (byte)ovr008.vm_GetCmdValue(4);

            //byte var_9 = (byte)((var_5 + 2) - var_8);
            //byte var_A = (byte)((var_7 + 2) - var_6);

            //byte var_1 = ovr024.roll_dice(6, 1);
            //byte var_2 = ovr024.roll_dice(6, 1);

            //byte val_a = 0;
            //if (var_1 <= var_9)
            //{
            //    if (var_2 <= var_A)
            //    {
            //        val_a = 3;
            //    }
            //    else
            //    {
            //        val_a = 1;
            //    }
            //}

            //if (var_2 <= var_A)
            //{
            //    val_a = 2;
            //}

            // setmemoryvalue(0x2CB, val_a);
        }

        internal static string CMD_Rob()
        {
            EclOpp[] opps = LoadEclOpps(3);


            string allParty = opps[0].GetCmdValue();
            string var_2 = opps[1].GetCmdValue();
            string var_3 = opps[2].GetCmdValue();

            return string.Format("all_party: {0} coin: {1}% item: {2}", allParty, var_2, var_3);
        }

        internal static string CMD_Treasure()
        {
            EclOpp[] opps = LoadEclOpps(8);

            var coins = string.Format("Cp: {0} Sl: {1} El: {2} Gl: {3} Pl: {4} Gems: {5} Jewel: {6}",
                opps[0].GetCmdValue(), opps[1].GetCmdValue(), opps[2].GetCmdValue(), opps[3].GetCmdValue(),
                opps[4].GetCmdValue(), opps[5].GetCmdValue(), opps[6].GetCmdValue());


            string block_id = opps[7].GetCmdValue();

            return string.Format("{1}, <item_val>:{0} [if item_val < 0x80 load ITEM block item_val, if item_val != 0xFF load item_val - 0x80 random items, else no items]", block_id, coins);
        }


        internal static string CMD_GetTable()
        {
            EclOpp[] opps = LoadEclOpps(3);

            int var_2 = opps[0].Word;
            string var_9 = opps[1].GetCmdValue();
            int result_loc = opps[2].Word;

            string loc_text = string.Format("{0}[{1}]", GetMemoryValue(var_2), var_9);

            return SetMemoryValue(result_loc, loc_text);
        }


        internal static string CMD_SaveTable()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string value = opps[0].GetCmdValue();
            int result_loc = opps[1].Word;
            string offset = opps[2].GetCmdValue();

            return string.Format("{0}[{1}] = {2}", GetMemoryValue(result_loc), offset, value);
        }

        internal static string CMD_AddNPC()
        {
            EclOpp[] opps = LoadEclOpps(2);

            string npc_id = opps[0].GetCmdValue();
            string morale = opps[1].GetCmdValue();

            return string.Format("npc_id: {0} morale: {1}", npc_id, morale);
        }

        internal static string CMD_Spell()
        {
            EclOpp[] opps = LoadEclOpps(3);

            string spell_id = opps[0].GetCmdValue();
            int loc_a = opps[1].Word;
            int loc_b = opps[2].Word;

            return string.Format("<find team player with spell_id {0}, and place {1} and {2} into (spell_index 0xFF if not found)>", spell_id, SetMemoryValue(loc_a, "<spell_index>"), SetMemoryValue(loc_b, "<player_index>"));
        }


        internal static string CMD_Parlay()
        {
            EclOpp[] opps = LoadEclOpps(6);

            StringBuilder sb = new StringBuilder();
            bool addComma = false;
            for (int i = 0; i < 5; i++)
            {
                if (addComma) sb.Append(", ");
                sb.Append(opps[i].GetCmdValue());
                addComma = true;
            }

            int location = opps[5].Word;


            return string.Format("<set {0} from [{1}] mapped by [HAUGHTY, SLY, NICE, MEEK, ABUSIVE]", SetMemoryValue(location, "value"), sb.ToString());
        }

        internal static string CMD_PartyStrength()
        {
            EclOpp[] opps = LoadEclOpps(1);

            int loc = opps[0].Word;
            return SetMemoryValue(loc, "<power_value_of_team>");
        }


        internal static string CMD_EncounterMenu()
        {  
            EclOpp[] opps = LoadEclOpps(14);

            string sprite_id = opps[0].GetCmdValue();
            string med = "<area2.max_encounter_distance> = " + opps[1].GetCmdValue();
            string pic_id = opps[2].GetCmdValue();

            int var_43D = opps[3].Word;

            string var_407 = opps[12].GetCmdValue();
            string var_408 = opps[13].GetCmdValue();

            string txt1 = string.Format("sprite_id: {0} max_distance: {1} pic_id: {2} var_43D: {3}", sprite_id, med, pic_id, var_43D);
            string txt2 = string.Format("[{0} {1} {2} {3} {4}]", opps[4].GetCmdValue(), opps[5].GetCmdValue(), opps[6].GetCmdValue(), opps[7].GetCmdValue(), opps[8].GetCmdValue());
            string txt3 = string.Format("'{0}' '{1}' '{2}'", stringTable[1], stringTable[2], stringTable[3]);
            string txt4 = string.Format("{0} {1}", var_407, var_408);

            return string.Format("{0} {1} {2} {3}", txt1, SetMemoryValue(var_43D, txt2), txt3, txt4);
        }

        internal static string CMD_CheckParty()
        {
            EclOpp[] opps = LoadEclOpps(6);
            int var_2 = 0;
            if (opps[0].Code == 1)
            {
                var_2 = opps[0].Word;
            }
            else
            {
                //var_2 = opps[0].GetCmdValue();
            }

            string affect_id = opps[1].GetCmdValue();

            int loc_a = opps[2].Word;
            int loc_b = opps[3].Word;
            int loc_c = opps[4].Word;
            int loc_d = opps[5].Word;

            var_2 = (var_2 - 0x7fff) & 0xFFFF;

            if (var_2 == 8001)
            {
                return SetMemoryValue(loc_a, string.Format("<team meber has affect: {0}>", affect_id)) + ", " + SetMemoryValue(loc_b, "0") + ", " +
                    SetMemoryValue(loc_c, "0") + ", " + SetMemoryValue(loc_d, "0");

                // team member has <affect_id>, 0, 0, 0, loc_a, loc_b, loc_c, loc_d);
            }
            else if (var_2 >= 0x00A5 && var_2 <= 0x00AC)
            {
                string index = string.Format("field_EA[{0}]>", var_2-0xA5);
                // false, avg movement, max field_EA[var_2-0xA5], min field_EA[var_2-0xA5], loc_a, loc_b, loc_c, loc_d
                return SetMemoryValue(loc_a, "0") + ", " + SetMemoryValue(loc_b, "<avg " + index) + ", " +
                   SetMemoryValue(loc_c, "<max " + index) + ", " + SetMemoryValue(loc_d, "<min " + index);
            }
            else if (var_2 == 0x9f)
            {
                return SetMemoryValue(loc_a, "0") + ", " + SetMemoryValue(loc_b, "<avg movement>") + ", " +
                    SetMemoryValue(loc_c, "<max movement>") + ", " + SetMemoryValue(loc_d, "<min movement>");
            }
            else
            {
                return "";
            }
        }
    }
}
