using Classes;
using Logging;

namespace engine
{
    public class seg043
    {
        static bool in_print_and_exit = false;
        public static void print_and_exit()
        {
            if (in_print_and_exit == false)
            {
                in_print_and_exit = true;

                gbl.soundType = gbl.soundTypeBackup;

                seg044.sound_sub_120E0(Sound.sound_FF);

                Logger.Close();

                seg001.EngineStop();
            }
        }


        static void DebugPlayerAffects(Player player)
        {
            foreach (Affect affect in player.affects)
            {
                Logger.Debug("who: {0}  sp#: {1} - {2}", player.name, (int)affect.type, affect.type);
            }
        }

        internal static byte GetInputKey()
        {
            byte key;

            if (gbl.inDemo == true)
            {
                if (seg049.KEYPRESSED() == true)
                {
                    key = seg049.READKEY();
                }
                else
                {
                    key = 0;
                }
            }
            else
            {
                key = seg049.READKEY();
            }

            if (key == 0x13)
            {
                if (gbl.soundType == SoundType.PC)
                {
                    gbl.soundTypeBackup = gbl.soundType;
                    seg044.sound_sub_120E0(Sound.sound_0);
                    gbl.soundType = SoundType.None;
                }
                else if (gbl.soundTypeBackup == SoundType.PC)
                {
                    gbl.soundType = gbl.soundTypeBackup;
                    seg044.sound_sub_120E0(Sound.sound_1);
                }
            }

            if (Cheats.allow_keyboard_exit && key == 3)
            {
                print_and_exit();
            }

            if (key != 0)
            {
                while (seg049.KEYPRESSED() == true)
                {
                    key = seg049.READKEY();
                }
            }

            return key;
        }

        public static void DumpPlayerAffects()
        {
            foreach (Player player in gbl.player_next_ptr)
            {
                DebugPlayerAffects(player);
            }
        }

        public static void ToggleCommandDebugging()
        {
            gbl.printCommands = !gbl.printCommands;

            if (gbl.printCommands == true)
            {
                Logger.Debug(System.DateTime.Now.ToString());
            }
        }

        internal static void clear_keyboard()
        {
            while (seg049.KEYPRESSED() == true)
            {
                GetInputKey();
            }
        }


        internal static void clear_one_keypress()
        {
            if (seg049.KEYPRESSED() == true)
            {
                GetInputKey();
            }
        }


        public static void DumpTreasureItems()
        {
            for (int i = 0; i < 0x81; i++)
            {
                Item it = ovr022.create_item(i);
                string name = ovr025.ItemName(it, 0);
                Player pl = new Player();
                pl.field_151 = it;
                bool ranged = ovr025.is_weapon_ranged(pl);
                bool rangedMelee = ovr025.is_weapon_ranged_melee(pl);

                Logging.Logger.Debug("Id: {0} {1} Ranged: {2} Ranged-Melee: {3}", i, name, ranged, rangedMelee);
            }
        }

        static void TxtDumpPlayer(Player p, int area, int id)
        {
            string str100 = p.strength == 18 ? string.Format("({0})", p.max_str_00) : "";
            Logger.Debug("Area {0} Id {1} {2} exp: {3} hp: {4} ac: {5} thac0: {6}", area, id, p.name, p.exp, p.hit_point_max, 0x3c - p.ac, 0x3c - p.hitBonus);
            Logger.Debug("   S: {0}{1} D: {2} C: {3} I: {4} W: {5} Ch: {6}", p.strength, str100, p.dex, p.con, p._int, p.wis, p.charisma);
            Logger.Debug("   Lvls: {0} {1} {2} {3} {4} {5} {6} {7}", p.class_lvls[0], p.class_lvls[1], p.class_lvls[2], p.class_lvls[3], p.class_lvls[4], p.class_lvls[5], p.class_lvls[6], p.class_lvls[7]);
            if (p.field_151 != null)
                Logger.Debug("   Weapon: {0}", ovr025.ItemName(p.field_151, 0));
            if (p.armor != null)
                Logger.Debug("   Armor: {0}", ovr025.ItemName(p.armor, 0));

            Logger.Debug("   Damage: {0}d{1}{2}{3}", p.attack_dice_count, p.attack_dice_size,
                p.damageBonus > 0 ? "+" : "", p.damageBonus != 0 ? p.damageBonus.ToString() : "");

            foreach (int sp in p.spell_list)
            {
                if (sp != 0)
                {
                    Logger.Debug("   Spell: {0}", ovr023.SpellNames[sp]);
                }
            }

            foreach (var af in p.affects)
            {
                Logger.Debug("   Affect: {0}", af.type);
            }
        }

        private static void HtmlTableDumpPlayer(Player p, byte area, int id)
        {
            Logger.DebugWrite("<tr>");
            Logger.DebugWrite("<td>{0}</td>", area);
            Logger.DebugWrite("<td>{0}</td>", id);
            Logger.DebugWrite("<td nowrap=\"nowrap\">{0}</td>", p.name);
            Logger.DebugWrite("<td>{0}</td>", p.exp);
            Logger.DebugWrite("<td>{0}</td>", p.hit_point_max);
            Logger.DebugWrite("<td>{0}</td>", 0x3c - p.ac);
            Logger.DebugWrite("<td>{0}</td>", 0x3c - p.hitBonus);
            string str100 = p.strength == 18 ? string.Format("({0})", p.max_str_00) : "";
            Logger.DebugWrite("<td>{0}{1}</td>", p.strength, str100);
            Logger.DebugWrite("<td>{0}</td>", p.dex);
            Logger.DebugWrite("<td>{0}</td>", p.con);
            Logger.DebugWrite("<td>{0}</td>", p._int);
            Logger.DebugWrite("<td>{0}</td>", p.wis);
            Logger.DebugWrite("<td>{0}</td>", p.charisma);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[0]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[1]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[2]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[3]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[4]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[5]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[6]);
            Logger.DebugWrite("<td>{0}</td>", p.class_lvls[7]);

            Logger.DebugWrite("<td nowrap=\"nowrap\">{0}</td>", p.field_151 != null ? ovr025.ItemName(p.field_151, 0) : "");
            Logger.DebugWrite("<td nowrap=\"nowrap\">{0}</td>", p.armor != null ? ovr025.ItemName(p.armor, 0) : "");
            Logger.DebugWrite("<td nowrap=\"nowrap\">{0}d{1}{2}{3}</td>", p.attack_dice_count, p.attack_dice_size,
                p.damageBonus > 0 ? "+" : "", p.damageBonus != 0 ? p.damageBonus.ToString() : "");

            int last = 0;
            int count = 0;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (int sp in p.spell_list)
            {
                if (sp != 0)
                {
                    if (sp != last)
                    {
                        if (last != 0)
                        {
                            sb.Append(ovr023.SpellNames[last]);
                            if (count > 1)
                            {
                                sb.Append(string.Format(" ({0})", count));
                            }
                            sb.Append(", ");
                        }
                        last = sp;
                        count = 1;
                    }
                    else
                    {
                        count += 1;
                    }
                }
            }

            if (last != 0)
            {
                sb.Append(ovr023.SpellNames[last]);
                if (count > 1)
                {
                    sb.Append(string.Format(" ({0})", count));
                }
            }

            Logger.DebugWrite("<td>{0}</td>", sb.ToString());
            

            sb = new System.Text.StringBuilder();
            foreach (var af in p.affects)
            {
                sb.Append(af.type.ToString());
                sb.Append(", ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            Logger.DebugWrite("<td>{0}</td>", sb.ToString());
            Logger.Debug("</tr>");
        }

        public static void DumpMonsters()
        {
            var bkupArea = gbl.game_area;

            for (byte area = 1; area <= 6; area++)
            {
                gbl.game_area = area;
                for (int id = 0; id < 256; id++)
                {
                    Player p = ovr017.load_mob(id, false);
                    if (p != null)
                    {
                        ovr025.reclac_player_values(p);

                        TxtDumpPlayer(p, area, id);
                        //HtmlTableDumpPlayer(p, area, id);
                    }
                }
            }

            gbl.game_area = bkupArea;
        }
    }
}
