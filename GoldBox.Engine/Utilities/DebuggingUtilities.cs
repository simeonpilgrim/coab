using GoldBox.Classes;
using GoldBox.Logging;

namespace GoldBox.Engine.Utilities
{
    public static class DebuggingUtilities
    {
        public static void ToggleCommandDebugging()
        {
            gbl.printCommands = !gbl.printCommands;

            VmLog.WriteLine(System.DateTime.Now.ToString());
        }

        public static void DumpPlayerAffects()
        {
            foreach (Player player in gbl.TeamList)
            {
                foreach (Affect affect in player.affects)
                {
                    Logger.Debug("who: {0}  sp#: {1} - {2}", player.name, (int)affect.type, affect.type);
                }
            }
        }
    }

    public class GameAreaMonsterWriter
    {
        public void DumpMonsters()
        {
            DumpMonstersFiltered();

            var bkupArea = gbl.game_area;

            string filename = System.IO.Path.Combine(Logger.GetPath(), "Monster.html");
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            var dw = new DebugWriter(filename);

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

        private static void DumpMonstersFiltered()
        {
            var bkupArea = gbl.game_area;

            string filename = System.IO.Path.Combine(Logger.GetPath(), "MonsterFiltered.txt");
            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(filename);
            }
            var dw = new DebugWriter(filename);

            dw.WriteLine("GnomeVsManSizedGiant");
            DumpMonstersFilteredSub(dw, p => (p.field_14B & 2) != 0);
            dw.WriteLine("");

            dw.WriteLine("monsterType == 1");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_1));
            dw.WriteLine("");

            dw.WriteLine("monsterType == giant");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.giant));
            dw.WriteLine("");

            dw.WriteLine("monsterType == dragon");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.dragon));
            dw.WriteLine("");

            dw.WriteLine("monsterType == animated_dead");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.animated_dead));
            dw.WriteLine("");

            dw.WriteLine("monsterType == 9");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_9));
            dw.WriteLine("");

            dw.WriteLine("monsterType == fire");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.fire));
            dw.WriteLine("");

            dw.WriteLine("monsterType == 10");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.troll));
            dw.WriteLine("");

            dw.WriteLine("monsterType == 12");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.type_12));
            dw.WriteLine("");

            dw.WriteLine("monsterType == snake");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.snake));
            dw.WriteLine("");

            dw.WriteLine("monsterType == plant");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.plant));
            dw.WriteLine("");

            dw.WriteLine("monsterType == animal");
            DumpMonstersFilteredSub(dw, p => (p.monsterType == MonsterType.animal));
            dw.WriteLine("");

            dw.WriteLine("monsterType > type_1");
            DumpMonstersFilteredSub(dw, p => (p.monsterType > MonsterType.type_1));
            dw.WriteLine("");


            dw.Close();

            gbl.game_area = bkupArea;
        }

        private static void DumpMonstersFilteredSub(DebugWriter dw, System.Predicate<Player> filter)
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
            string str100 = p.stats2.Str.full == 18 ? string.Format("({0})", p.stats2.Str00.full) : "";
            dw.Write("<td>{0}{1}</td>", p.stats2.Str.full, str100);
            dw.Write("<td>{0}</td>", p.stats2.Dex.full);
            dw.Write("<td>{0}</td>", p.stats2.Con.full);
            dw.Write("<td>{0}</td>", p.stats2.Int.full);
            dw.Write("<td>{0}</td>", p.stats2.Wis.full);
            dw.Write("<td>{0}</td>", p.stats2.Cha.full);
            dw.Write("<td>{0}</td>", p.ClassLevel[0]);
            dw.Write("<td>{0}</td>", p.ClassLevel[1]);
            dw.Write("<td>{0}</td>", p.ClassLevel[2]);
            dw.Write("<td>{0}</td>", p.ClassLevel[3]);
            dw.Write("<td>{0}</td>", p.ClassLevel[4]);
            dw.Write("<td>{0}</td>", p.ClassLevel[5]);
            dw.Write("<td>{0}</td>", p.ClassLevel[6]);
            dw.Write("<td>{0}</td>", p.ClassLevel[7]);

            dw.Write("<td nowrap=\"nowrap\">{0}</td>", p.activeItems.primaryWeapon != null ? p.activeItems.primaryWeapon.GenerateName(0) : "");
            dw.Write("<td nowrap=\"nowrap\">{0}</td>", p.activeItems.armor != null ? p.activeItems.armor.GenerateName(0) : "");
            dw.Write("<td nowrap=\"nowrap\">{0}d{1}{2}{3}</td>", p.attack1_DiceCount, p.attack1_DiceSize,
                p.attack1_DamageBonus > 0 ? "+" : "", p.attack1_DamageBonus != 0 ? p.attack1_DamageBonus.ToString() : "");

            int last = 0;
            int count = 0;
            var sb = new System.Text.StringBuilder();

            foreach (int sp in p.spellList.IdList())
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
    }
}
