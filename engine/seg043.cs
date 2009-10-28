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

                seg044.sound_sub_120E0(Sound.sound_FF);

                Logger.Close();

                ItemLibrary.Write();

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
                seg044.sound_sub_120E0(Sound.sound_0);
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
            //for (int i = 0; i < 0x81; i++)
            //{
            //    //var older = new System.Collections.Generic.List<string>();

            //    //for (int j = 0; j < 1000; j++)
            //    //{
            //    Item it = ovr022.create_item(i);
            //    it.name = it.GenerateName(0);
            //    Player pl = new Player();
            //    pl.field_151 = it;
            //    bool ranged = ovr025.is_weapon_ranged(pl);
            //    bool rangedMelee = ovr025.is_weapon_ranged_melee(pl);

            //    //if (older.Contains(name) == false)
            //    //{
            //    Logging.Logger.Debug("Id: {0} {1} Ranged: {2} Ranged-Melee: {3}", i, it.name, ranged, rangedMelee);
            //    //older.Add(name);
            //    //}
            //    //}
            //}
        }

        static void TxtDumpPlayer(Player p, int area, int id)
        {
            string str100 = p.strength == 18 ? string.Format("({0})", p.max_str_00) : "";
            Logger.Debug("Area {0} Id {1} {2} exp: {3} hp: {4} ac: {5} thac0: {6}", area, id, p.name, p.exp, p.hit_point_max, 0x3c - p.ac, 0x3c - p.hitBonus);
            Logger.Debug("   S: {0}{1} D: {2} C: {3} I: {4} W: {5} Ch: {6}", p.strength, str100, p.dex, p.con, p._int, p.wis, p.charisma);
            Logger.Debug("   Lvls: {0} {1} {2} {3} {4} {5} {6} {7}", p.ClassLevel[0], p.ClassLevel[1], p.ClassLevel[2], p.ClassLevel[3], p.ClassLevel[4], p.ClassLevel[5], p.ClassLevel[6], p.ClassLevel[7]);
            if (p.field_151 != null)
                Logger.Debug("   Weapon: {0}", p.field_151.GenerateName(0));
            if (p.armor != null)
                Logger.Debug("   Armor: {0}", p.armor.GenerateName(0));

            Logger.Debug("   Damage: {0}d{1}{2}{3}", p.attack1_DiceCount, p.attack1_DiceSize,
                p.attack1_DamageBonus > 0 ? "+" : "", p.attack1_DamageBonus != 0 ? p.attack1_DamageBonus.ToString() : "");

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


        private static void HtmlTableDumpPlayer(DebugWriter dw, Player p, byte area, int id)
        {
            dw.Write("<tr>");
            dw.Write("<td>{0}</td>", area);
            dw.Write("<td>{0}</td>", id);
            dw.Write("<td nowrap=\"nowrap\">{0}</td>", p.name);
            dw.Write("<td>{0}</td>", p.exp);
            dw.Write("<td>{0}</td>", p.hit_point_max);
            dw.Write("<td>{0}</td>", 0x3c - p.ac);
            dw.Write("<td>{0}</td>", 0x3c - p.hitBonus);
            string str100 = p.strength == 18 ? string.Format("({0})", p.max_str_00) : "";
            dw.Write("<td>{0}{1}</td>", p.strength, str100);
            dw.Write("<td>{0}</td>", p.dex);
            dw.Write("<td>{0}</td>", p.con);
            dw.Write("<td>{0}</td>", p._int);
            dw.Write("<td>{0}</td>", p.wis);
            dw.Write("<td>{0}</td>", p.charisma);
            dw.Write("<td>{0}</td>", p.ClassLevel[0]);
            dw.Write("<td>{0}</td>", p.ClassLevel[1]);
            dw.Write("<td>{0}</td>", p.ClassLevel[2]);
            dw.Write("<td>{0}</td>", p.ClassLevel[3]);
            dw.Write("<td>{0}</td>", p.ClassLevel[4]);
            dw.Write("<td>{0}</td>", p.ClassLevel[5]);
            dw.Write("<td>{0}</td>", p.ClassLevel[6]);
            dw.Write("<td>{0}</td>", p.ClassLevel[7]);

            dw.Write("<td nowrap=\"nowrap\">{0}</td>", p.field_151 != null ? p.field_151.GenerateName(0) : "");
            dw.Write("<td nowrap=\"nowrap\">{0}</td>", p.armor != null ? p.armor.GenerateName(0) : "");
            dw.Write("<td nowrap=\"nowrap\">{0}d{1}{2}{3}</td>", p.attack1_DiceCount, p.attack1_DiceSize,
                p.attack1_DamageBonus > 0 ? "+" : "", p.attack1_DamageBonus != 0 ? p.attack1_DamageBonus.ToString() : "");

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

            dw.Write("<td nowrap=\"nowrap\">{0}</td>", sb.ToString());
            

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

            dw.Write("<td nowrap=\"nowrap\">{0}</td>", sb.ToString());
            dw.WriteLine("</tr>");
        }

        public static void DumpMonstersFiltered()
        {
            var bkupArea = gbl.game_area;

            string filename = "MonsterFiltered.txt";
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            DebugWriter dw = new DebugWriter(filename);

            dw.WriteLine("GnomeVsManSizedGiant");
            DumpMonstersFilteredSub(dw, p => (p.field_14B & 2) != 0);
            dw.WriteLine("");

            dw.WriteLine("field_11A == 1");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_1));
            dw.WriteLine("");

            dw.WriteLine("field_11A == giant");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.giant));
            dw.WriteLine("");

            dw.WriteLine("field_11A == dragon");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.dragon));
            dw.WriteLine("");

            dw.WriteLine("field_11A == animated_dead");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.animated_dead));
            dw.WriteLine("");

            dw.WriteLine("field_11A == 9");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_9));
            dw.WriteLine("");

            dw.WriteLine("field_11A == fire");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.fire));
            dw.WriteLine("");

            dw.WriteLine("field_11A == 10");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_10));
            dw.WriteLine("");

            dw.WriteLine("field_11A == 12");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_12));
            dw.WriteLine("");

            dw.WriteLine("field_11A == snake");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.snake));
            dw.WriteLine("");

            dw.WriteLine("field_11A == plant");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.plant));
            dw.WriteLine("");

            dw.WriteLine("field_11A == animal");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.animal));
            dw.WriteLine("");

            dw.WriteLine("field_11A > type_1");
            DumpMonstersFilteredSub(dw, p => (p.monsterType > MonsterType.type_1));
            dw.WriteLine("");


            dw.Close();

            gbl.game_area = bkupArea;
        }

        static void DumpMonstersFilteredSub(DebugWriter dw, System.Predicate<Player> filter)
        {
            for (byte area = 1; area <= 6; area++)
            {
                gbl.game_area = area;
                for (int id = 0; id < 256; id++)
                {
                    Player p = ovr017.load_mob(id, false);
                    if (p != null)
                    {
                        ovr025.reclac_player_values(p);

                        if (filter(p))
                        {
                            dw.WriteLine(p.ToString());
                        }
                    }
                }
            }
        }

        public static void DumpMonsters()
        {
            DumpMonstersFiltered();

            var bkupArea = gbl.game_area;

            if (System.IO.File.Exists("Monster.html"))
            {
                System.IO.File.Delete("Monster.html");
            }
            DebugWriter dw = new DebugWriter("Monster.html");

            dw.WriteLine("<html><body><table><tbody>");

            for (byte area = 1; area <= 6; area++)
            {
                gbl.game_area = area;
                for (int id = 0; id < 256; id++)
                {
                    Player p = ovr017.load_mob(id, false);
                    if (p != null)
                    {
                        ovr025.reclac_player_values(p);

                        //TxtDumpPlayer(p, area, id);
                        HtmlTableDumpPlayer(dw, p, area, id);
                    }
                }
            }

            dw.WriteLine("</tbody></table></body></html>");
            dw.Close();

            gbl.game_area = bkupArea;
        }
    }
}
