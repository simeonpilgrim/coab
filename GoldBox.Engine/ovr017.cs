using GoldBox.Classes;
using System.Collections.Generic;
using GoldBox.Logging;
using System.IO;

namespace GoldBox.Engine
{
    class ovr017
    {
        private static readonly FileManager _fileManager = new FileManager();
        private static readonly CleanEightCharString _stringCleaner = new CleanEightCharString();

        static void BuildLoadablePlayersLists(ref List<MenuItem> fileNames, ref List<MenuItem> displayNames, short playerFileSize, int npcOffset, int nameOffset, string fileFilter) // sub_4708B
        {
            var file = new Classes.OpenOrCreateFile();

            byte[] data = new byte[16];

            foreach (string filePath in Directory.GetFiles(Config.SavePath, fileFilter))
            {
                FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);

                if (stream.Length == playerFileSize)
                {
                    stream.Seek(nameOffset, SeekOrigin.Begin);
                    stream.Read(data, 0, 16);

                    string playerName = Sys.ArrayToString(data, 0, 15).Trim();

                    byte var_164;

                    if (gbl.import_from == ImportSource.Hillsfar)
                    {
                        var_164 = 0;
                    }
                    else
                    {
                        stream.Seek(npcOffset, SeekOrigin.Begin);
                        stream.Read(data, 0, 1);
                        var_164 = data[0];
                    }


                    string fullNameText =
                        string.Compare(Path.GetExtension(filePath), ".sav", true) == 0 ?
                        string.Format("{0,-15} from save game {1}", playerName, filePath[7]) : playerName;

                    bool found = gbl.TeamList.Find(player => playerName == player.name.Trim()) != null;

                    if (found == false && var_164 <= 0x7F)
                    {
                        fileNames.Add(new MenuItem(filePath));
                        displayNames.Add(new MenuItem(fullNameText));
                    }
                }

                stream.Close();
            }
        }

        static int[] PlayerNameOffset = { 0, 0, 4 };
        static int[] NpcFileOffset = { 0xf7, 0x84, 0x13 };
        static Set unk_4AEA0 = new Set(0, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74);
        static Set unk_4AEEF = new Set(0, 2, 18);
        static Set unk_47635 = new Set(0, 5);
        static Set asc_49280 = new Set(18, 26, 47, 48, 97, 107, 124);
        static Set save_game_keys = new Set(65, 66, 67, 68, 69, 70, 71, 72, 73, 74); // asc_4A761
        static ClassId[] HillsFarClassMap = {
    ClassId.unknown,    ClassId.thief,      ClassId.fighter,    ClassId.mc_f_t, ClassId.magic_user,
    ClassId.mc_mu_t,    ClassId.mc_f_mu,    ClassId.mc_f_mu_t,  ClassId.cleric, ClassId.mc_c_t,
    ClassId.mc_c_f,     ClassId.unknown,    ClassId.mc_c_mu,    ClassId.unknown, ClassId.mc_c_f_m,
    ClassId.unknown};
        
        internal static void BuildLoadablePlayersLists(out List<MenuItem> fileNames, out List<MenuItem> displayNames) // sub_47465
        {
            displayNames = new List<MenuItem>();
            fileNames = new List<MenuItem>();

            if (gbl.import_from == ImportSource.Curse)
            {
                BuildLoadablePlayersLists(ref fileNames, ref displayNames, Player.StructSize, NpcFileOffset[0], PlayerNameOffset[0], "*.guy");
            }
            else if (gbl.import_from == ImportSource.Pool)
            {
                BuildLoadablePlayersLists(ref fileNames, ref displayNames, PoolRadPlayer.StructSize, NpcFileOffset[1], PlayerNameOffset[1], "*.cha");
                BuildLoadablePlayersLists(ref fileNames, ref displayNames, PoolRadPlayer.StructSize, NpcFileOffset[1], PlayerNameOffset[1], "*.sav");
            }
            else if (gbl.import_from == ImportSource.Hillsfar)
            {
                BuildLoadablePlayersLists(ref fileNames, ref displayNames, HillsFarPlayer.StructSize, NpcFileOffset[2], PlayerNameOffset[2], "*.hil");
            }
        }

        internal static void LoadPlayerCombatIcon(bool recolour) /* sub_47A90 */
        {
            seg042.set_game_area(1);

            Player player = gbl.SelectedPlayer;

            var sizeToken = new char[] { '\0', 'S', 'T' };

            ovr034.chead_cbody_comspr_icon(11, player.head_icon, "CHEAD" + sizeToken[player.icon_size]);
            ovr034.chead_cbody_comspr_icon(player.icon_id, player.weapon_icon, "CBODY" + sizeToken[player.icon_size]);

            gbl.combat_icons[player.icon_id].MergeIcon(gbl.combat_icons[11]);

            if (recolour)
            {
                byte[] newColors = new byte[16];
                byte[] oldColors = new byte[16];

                for (byte i = 0; i <= 15; i++)
                {
                    oldColors[i] = i;
                    newColors[i] = i;
                }

                for (int i = 0; i < 6; i++)
                {
                    newColors[gbl.default_icon_colours[i]] = (byte)(player.icon_colours[i] & 0x0F);
                    newColors[gbl.default_icon_colours[i] + 8] = (byte)((player.icon_colours[i] & 0xF0) >> 4);
                }

                gbl.combat_icons[player.icon_id].Recolor(false, newColors, oldColors);
            }

            ovr034.ReleaseCombatIcon(11);
            seg042.restore_game_area();
            seg043.clear_keyboard();
        }
        
        internal static void remove_player_file(Player player)
        {
            string full_path = Path.Combine(Config.SavePath, _stringCleaner.clean_string(player.name));

            _fileManager.DeleteIfExists(full_path + ".guy");
            _fileManager.DeleteIfExists(full_path + ".swg");
            _fileManager.DeleteIfExists(full_path + ".fx");
        }

        internal static void SavePlayer(string arg_0, Player player) // sub_47DFC
        {
            char input_key;

            gbl.import_from = ImportSource.Curse;

            string ext_text;
            string file_text;

            if (arg_0 == "")
            {
                ext_text = ".guy";
                file_text = _stringCleaner.clean_string(player.name);
            }
            else
            {
                ext_text = ".sav";
                file_text = arg_0;
            }

            input_key = 'N';

            while (input_key == 'N' &&
                arg_0.Length == 0 &&
                _fileManager.Exists(Path.Combine(Config.SavePath, file_text) + ext_text))
            {
                input_key = ovr027.yes_no(gbl.alertMenuColors, "Overwrite " + file_text + "? ");

                if (input_key == 'N')
                {
                    file_text = string.Empty;

                    while (file_text == string.Empty)
                    {
                        file_text = seg041.getUserInputString(8, 0, 10, "New file name: ");
                    }
                }
            }

            string filePath = Path.Combine(Config.SavePath, file_text);

            using (var file = new OpenOrCreateFile())
            {
                file.Assign(filePath + ext_text);
                file.Rewrite();
                file.BlockWrite(Player.StructSize, player.ToByteArray());
            }

            _fileManager.DeleteIfExists(filePath + ".swg");
            if (player.items.Count > 0)
            {
                using (var file = new OpenOrCreateFile())
                {
                    file.Assign(filePath + ".swg");
                    file.Rewrite();
                    player.items.ForEach(item => file.BlockWrite(Item.StructSize, item.ToByteArray()));
                }
            }

            _fileManager.DeleteIfExists(filePath + ".fx");
            if (player.affects.Count > 0)
            {
                using (var file = new OpenOrCreateFile())
                {
                    file.Assign(filePath + ".fx");
                    file.Rewrite();
                    player.affects.ForEach(affect => file.BlockWrite(Affect.StructSize, affect.ToByteArray()));
                }
            }
        }

        internal static bool PlayerFileExists(string fileExt, string player_name) // sub_483AE
        {
            byte[] data = new byte[0x10];

            foreach (string filename in Directory.GetFiles(Config.SavePath, "*" + fileExt))
            {
                FileStream stream = System.IO.File.Open(filename, FileMode.Open, FileAccess.Read);

                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, 16);
                stream.Close();

                string in_file_name = Sys.ArrayToString(data, 0, 15).Trim();
                if (in_file_name == player_name)
                {
                    return true;
                }

            }
            return false;
        }
        
        internal static Player ConvertPoolRadPlayer(PoolRadPlayer bp_var_1C0)
        {
            /* nested function, arg_0 is BP */
            var player = new Player();

            player.race = (Race)bp_var_1C0.race;
            player.sex = bp_var_1C0.sex;

            player.name = bp_var_1C0.name;

            var race = (int)player.race;
            int sex = player.sex;

            player.stats2.Str.Load(bp_var_1C0.stat_str);
            player.stats2.Str.EnforceRaceSexLimits(race, sex);

            player.stats2.Int.Load(bp_var_1C0.stat_int);
            player.stats2.Int.EnforceRaceSexLimits(race, sex);

            player.stats2.Wis.Load(bp_var_1C0.stat_wis);
            player.stats2.Wis.EnforceRaceSexLimits(race, sex);

            player.stats2.Dex.Load(bp_var_1C0.stat_dex);
            player.stats2.Dex.EnforceRaceSexLimits(race, sex);

            player.stats2.Con.Load(bp_var_1C0.stat_con);
            player.stats2.Con.EnforceRaceSexLimits(race, sex);

            player.stats2.Cha.Load(bp_var_1C0.stat_cha);
            player.stats2.Cha.EnforceRaceSexLimits(race, sex);

            player.stats2.Str00.Load(bp_var_1C0.stat_str00);
            player.stats2.Str00.EnforceRaceSexLimits(race, sex);

            player.thac0 = bp_var_1C0.thac0;
            player._class = (ClassId)bp_var_1C0._class;
            player.age = bp_var_1C0.age;
            player.hit_point_max = bp_var_1C0.hp_max;

            System.Array.Copy(bp_var_1C0.field_33, player.spellBook, 0x38);
            player.spellBook[(int)Spells.animate_dead - 1] = 0;

            player.attackLevel = bp_var_1C0.field_6B;
            player.field_DE = bp_var_1C0.field_6C;

            System.Array.Copy(bp_var_1C0.field_6D, player.saveVerse, 5);

            player.base_movement = bp_var_1C0.field_72;
            player.HitDice = bp_var_1C0.field_73;
            player.multiclassLevel = player.HitDice;
            player.lost_lvls = bp_var_1C0.field_74;
            player.lost_hp = bp_var_1C0.field_75;
            player.field_E9 = bp_var_1C0.field_76;

            System.Array.Copy(bp_var_1C0.field_77, player.thief_skills, 8);

            player.field_F6 = bp_var_1C0.field_83;
            player.control_morale = bp_var_1C0.field_84;
            player.npcTreasureShareCount = bp_var_1C0.field_85;
            player.field_F9 = bp_var_1C0.field_86;
            player.field_FA = bp_var_1C0.field_87;

            player.Money.SetCoins(Money.Platinum, 300);

            System.Array.Copy(bp_var_1C0.field_96, player.ClassLevel, 8);

            player.monsterType = (MonsterType)bp_var_1C0.field_9F;
            player.alignment = bp_var_1C0.field_A0;

            player.attacksCount = bp_var_1C0.field_A1;
            player.baseHalfMoves = bp_var_1C0.field_A2;
            player.attack1_DiceCountBase = bp_var_1C0.field_A3;
            player.attack2_DiceCountBase = bp_var_1C0.field_A4;
            player.attack1_DiceSizeBase = bp_var_1C0.field_A5;
            player.attack2_DiceSizeBase = bp_var_1C0.field_A6;
            player.attack1_DamageBonusBase = bp_var_1C0.field_A7;
            player.attack2_DamageBonusBase = bp_var_1C0.field_A8;

            player.base_ac = bp_var_1C0.field_A9;
            player.field_125 = bp_var_1C0.field_AA;
            player.mod_id = bp_var_1C0.field_AB;

            player.exp = bp_var_1C0.field_AC;
            player.classFlags = bp_var_1C0.field_B0;
            player.hit_point_rolled = bp_var_1C0.field_B1;

            for (int var_2 = 1; var_2 <= 3; var_2++)
            {
                player.spellCastCount[0, var_2 - 1] = bp_var_1C0.field_B2[var_2 - 1];
                player.spellCastCount[2, var_2 - 1] = bp_var_1C0.field_B5[var_2 - 1];
            }

            player.field_13C = bp_var_1C0.field_B8;

            player.field_13E = bp_var_1C0.field_BA;
            player.field_13F = bp_var_1C0.field_BB;

            player.field_140 = bp_var_1C0.field_BC;
            player.head_icon = bp_var_1C0.field_BD;
            player.weapon_icon = bp_var_1C0.field_BE;
            player.icon_size = bp_var_1C0.field_C0;

            System.Array.Copy(bp_var_1C0.field_C1, player.icon_colours, 6);


            //player.field_14c = bp_var_1C0.field_C7; // Item count

            //mov	di, [bp+arg_0] // copy item pointers...
            //les	di, ss:[di-0x1C0]
            //add	di, 0x0CC
            //push	es
            //push	di
            //les	di, int ptr [bp+player.offset]
            //add	di, 0x151
            //push	es
            //push	di
            //mov	ax, 0x34
            //push	ax
            //call	Move(Any &,Any &,Word)

            player.weaponsHandsUsed = bp_var_1C0.field_100;
            player.field_186 = (sbyte)bp_var_1C0.field_101;
            player.weight = bp_var_1C0.field_102;

            player.health_status = (Status)bp_var_1C0.field_10C;
            player.in_combat = bp_var_1C0.field_10D != 0;
            player.combat_team = (CombatTeam)bp_var_1C0.field_10E;
            player.hitBonus = bp_var_1C0.field_110;

            player.ac = bp_var_1C0.field_111;
            player.ac_behind = bp_var_1C0.field_112;

            player.attack1_AttacksLeft = bp_var_1C0.field_113;
            player.attack2_AttacksLeft = bp_var_1C0.field_114;

            player.attack1_DiceCount = bp_var_1C0.field_115;
            player.attack2_DiceCount = bp_var_1C0.field_116;

            player.attack1_DiceSize = bp_var_1C0.field_117;
            player.attack2_DiceSize = bp_var_1C0.field_118;

            player.attack1_DamageBonus = (sbyte)bp_var_1C0.field_119;
            player.attack2_DamageBonus = bp_var_1C0.field_11A;
            player.hit_point_current = bp_var_1C0.field_11B;
            player.movement = (byte)bp_var_1C0.field_11C;

            return player;
        }
        
        internal static void TransferHillsFarCharacter(HillsFarPlayer hf_player, Player player, Player previousSelectPlayer) // sub_48F35
        {
            if (player.stats2.Str.cur < hf_player.stat_str)
            {
                player.stats2.Str.Load(hf_player.stat_str);
            }

            if (player.stats2.Str00.cur < hf_player.stat_str00)
            {
                player.stats2.Str00.Load(hf_player.stat_str00);
            }

            if (player.stats2.Int.cur < hf_player.stat_int)
            {
                player.stats2.Int.Load(hf_player.stat_int);
            }

            if (player.stats2.Wis.cur < hf_player.stat_wis)
            {
                player.stats2.Wis.Load(hf_player.stat_wis);
            }

            if (player.stats2.Dex.cur < hf_player.stat_dex)
            {
                player.stats2.Dex.Load(hf_player.stat_dex);
            }

            if (player.stats2.Con.cur < hf_player.stat_con)
            {
                player.stats2.Con.Load(hf_player.stat_con);
            }

            if (player.stats2.Cha.cur < hf_player.stat_cha)
            {
                player.stats2.Cha.Load(hf_player.stat_cha);
            }

            if (player.exp < hf_player.field_2E)
            {
                player.exp = hf_player.field_2E;
            }

            // If imported player has more than 500 platinum import that amount.
            if (player.Money.GetGoldWorth() < hf_player.field_28)
            {
                for (int slot = 0; slot < 5; slot++)
                {
                    ovr022.DropCoins(slot, player.Money.GetCoins(slot), player);
                }
                ovr022.addPlayerGold((short)(hf_player.field_28 / 5));
            }

            if (player.age < hf_player.age)
            {
                player.age = hf_player.age;
            }

            player.cleric_lvl = (hf_player.field_B7 > 0) ? (byte)1 : (byte)0;
            player.magic_user_lvl = (hf_player.field_B8 > 0) ? (byte)1 : (byte)0;
            player.fighter_lvl = (hf_player.field_B9 > 0) ? (byte)1 : (byte)0;
            player.thief_lvl = (hf_player.field_BA > 0) ? (byte)1 : (byte)0;

            player.HitDice = 1;

            if (hf_player.field_26 != 0)
            {
                player.field_192 = 1;
            }

            SilentTrainPlayer();
            gbl.SelectedPlayer = previousSelectPlayer;

            player.hit_point_max = hf_player.field_21;
            player.hit_point_rolled = (byte)(player.hit_point_max - ovr018.get_con_hp_adj(player));
            player.hit_point_current = hf_player.field_20;
        }

        internal static void SilentTrainPlayer()
        {
            gbl.area2_ptr.training_class_mask = 0xff;
            gbl.can_train_no_more = false;
            gbl.silent_training = true;

            do
            {
                ovr018.train_player();
            } while (gbl.can_train_no_more == false);

            gbl.silent_training = false;
        }

        internal static void import_char01(ref Player player, string arg_8)
        {
            seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);

            ImportPlayer(ref player, arg_8);

            if (gbl.import_from == ImportSource.Curse)
            {
                arg_8 = Path.GetFileNameWithoutExtension(arg_8);
            }
            else
            {
                arg_8 = _stringCleaner.clean_string(player.name);
            }

            LoadItems(player, arg_8);

            LoadAffects(player, arg_8);

            LoadPoolRadAffects(player, arg_8);

            seg043.clear_keyboard();
            ovr025.reclac_player_values(player);
            ovr026.ReclacClassBonuses(player);
        }

        private static void LoadPoolRadAffects(Player player, string arg_8)
        {
            if (gbl.import_from != ImportSource.Pool) return;

            var filename = Path.Combine(Config.SavePath, arg_8 + ".spc");
            if (!_fileManager.Exists(filename)) return;

            byte[] data = new byte[Affect.StructSize];
            OpenOrCreateFile file;
            _fileManager.find_and_open_file(out file, false, filename);
            using (file)
            {
                while (true)
                {
                    if (file.BlockRead(Affect.StructSize, data) == Affect.StructSize)
                    {
                        if (asc_49280.MemberOf(data[0]))
                        {
                            var tmpAffect = new Affect(data, 0);
                            player.affects.Add(tmpAffect);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }

        private static void LoadAffects(Player player, string arg_8)
        {
            var filename = Path.Combine(Config.SavePath, arg_8 + ".fx");
            if (!_fileManager.Exists(filename)) return;

            byte[] data = new byte[Affect.StructSize];
            OpenOrCreateFile file;
            _fileManager.find_and_open_file(out file, false, filename);
            using (file)
            {
                while (true)
                {
                    if (file.BlockRead(Affect.StructSize, data) == Affect.StructSize)
                    {
                        var tmp_affect = new Affect(data, 0);

                        player.affects.Add(new Affect(data, 0));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void LoadItems(Player player, string arg_8)
        {
            var filename = Path.Combine(Config.SavePath, arg_8 + ".swg");
            if (!_fileManager.Exists(filename)) return;

            byte[] data = new byte[Item.StructSize];
            OpenOrCreateFile file;
            _fileManager.find_and_open_file(out file, false, filename);
            using (file)
            {
                while (true)
                {

                    if (file.BlockRead(Item.StructSize, data) == Item.StructSize)
                    {
                        player.items.Add(new Item(data, 0));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void ImportPlayer(ref Player player, string arg_8)
        {
            OpenOrCreateFile file;
            _fileManager.find_and_open_file(out file, false, Path.Combine(Config.SavePath, arg_8));
            if (gbl.import_from == ImportSource.Curse)
            {
                byte[] data = new byte[Player.StructSize];
                using (file)
                    file.BlockRead(Player.StructSize, data);

                player = new Player(data, 0);

            }
            else if (gbl.import_from == ImportSource.Pool)
            {
                byte[] data = new byte[PoolRadPlayer.StructSize];
                using (file)
                    file.BlockRead(PoolRadPlayer.StructSize, data);

                var poolRadPlayer = new PoolRadPlayer(data);

                player = ConvertPoolRadPlayer(poolRadPlayer);
            }
            else if (gbl.import_from == ImportSource.Hillsfar)
            {
                byte[] data = new byte[HillsFarPlayer.StructSize];
                using (file)
                    file.BlockRead(HillsFarPlayer.StructSize, data);

                var var_1C4 = new HillsFarPlayer(data);

                player = ConvertHillsFarPlayer(var_1C4, arg_8);

                var_1C4 = null;
            }
        }

        private static Player ConvertHillsFarPlayer(HillsFarPlayer hf_player, string arg_8)
        {
            var player = new Player();

            player.items = new List<Item>();
            player.affects = new List<Affect>();
            player.actions = null;

            string fileExt = ".guy";

            if (PlayerFileExists(fileExt, hf_player.name))
            {
                string savename = Path.Combine(Config.SavePath, Path.ChangeExtension(arg_8, fileExt));

                OpenOrCreateFile file;
                _fileManager.find_and_open_file(out file, false, savename);

                byte[] data = new byte[Player.StructSize];
                using (file)
                    file.BlockRead(Player.StructSize, data);

                player = new Player(data, 0);

                Player PreviousSelectedPlayer = gbl.SelectedPlayer;
                gbl.SelectedPlayer = player;

                TransferHillsFarCharacter(hf_player, player, PreviousSelectedPlayer);

                if (hf_player.field_1D > 0)
                {
                    var newItem = new Item(0, Affects.helpless, (Affects)hf_player.field_1D,
                        (short)(hf_player.field_1D * 200), 0, 0,
                        false, 0, false, 0, 0, 0x57, 0xa7, 0xa8, ItemType.Necklace, true);

                    player.items.Add(newItem);
                }

                if (hf_player.field_23 > 0)
                {
                    var newItem = new Item(0, Affects.poison_plus_4, (Affects)hf_player.field_23,
                        (short)(hf_player.field_23 * 0x15E), 0, 1,
                        false, 0, false, 0, 1, 0x45, 0xa7, 0xce, ItemType.WandB, true);

                    player.items.Add(newItem);
                }

                if (hf_player.field_86 > 0)
                {
                    var newItem = new Item(0, Affects.helpless, (Affects)hf_player.field_86,
                        (short)(hf_player.field_86 * 0xc8), 0, 0,
                        false, 0, false, 0, 0, 0x42, 0xa7, 0xa8, ItemType.RingInvis, true);

                    player.items.Add(newItem);
                }

                if (hf_player.field_87 > 0)
                {
                    var newItem = new Item(0, Affects.highConRegen, (Affects)hf_player.field_87,
                        (short)(hf_player.field_87 * 0x190), 0, (short)(hf_player.field_87 * 10),
                        false, 0, false, 0, 0, 0x40, 0xa7, 0xb9, ItemType.Necklace, true);

                    player.items.Add(newItem);
                }
            }
            else
            {
                fileExt = ".cha";

                if (PlayerFileExists(fileExt, hf_player.name))
                {
                    byte[] data = new byte[PoolRadPlayer.StructSize];

                    string savename = System.IO.Path.Combine(Config.SavePath, Path.ChangeExtension(arg_8, fileExt));

                    OpenOrCreateFile file;
                    _fileManager.find_and_open_file(out file, false, savename);

                    using (file)
                        file.BlockRead(PoolRadPlayer.StructSize, data);

                    var poolRadPlayer = new PoolRadPlayer(data);

                    player = ConvertPoolRadPlayer(poolRadPlayer);

                    Player PreviousSelectedPlayer = gbl.SelectedPlayer;
                    gbl.SelectedPlayer = player;

                    TransferHillsFarCharacter(hf_player, player, PreviousSelectedPlayer);
                }
                else
                {
                    Player PreviousSelectedPlayer = gbl.SelectedPlayer;
                    gbl.SelectedPlayer = player;

                    for (int i = 0; i < 6; i++)
                    {
                        player.icon_colours[i] = (byte)(((gbl.default_icon_colours[i] + 8) << 4) + gbl.default_icon_colours[i]);
                    }

                    player.base_ac = 50;
                    player.thac0 = 40;
                    player.health_status = Status.okey;
                    player.in_combat = true;
                    player.field_13F = 1;
                    player.field_140 = 1;
                    player.field_DE = 1;

                    player.mod_id = seg051.Random((byte)0xff);
                    player.icon_id = 0x0A;

                    player.attacksCount = 2;
                    player.attack1_DiceCountBase = 1;
                    player.attack1_DiceSizeBase = 2;
                    player.field_125 = 1;
                    player.base_movement = 12;

                    player.name = hf_player.name;
                    player.stats2.Str.Load(hf_player.stat_str);
                    player.stats2.Str00.Load(hf_player.stat_str00);
                    player.stats2.Int.Load(hf_player.stat_int);
                    player.stats2.Wis.Load(hf_player.stat_wis);
                    player.stats2.Dex.Load(hf_player.stat_dex);
                    player.stats2.Con.Load(hf_player.stat_con);
                    player.stats2.Cha.Load(hf_player.stat_cha);

                    player.race = (Race)(hf_player.field_2D + 1);

                    if (player.race == Race.half_orc)
                    {
                        player.race = Race.human;
                    }

                    switch (player.race)
                    {
                        case Race.halfling:
                            player.icon_size = 1;
                            ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                            break;

                        case Race.dwarf:
                            player.icon_size = 1;
                            ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                            ovr024.add_affect(false, 0xff, 0, Affects.dwarf_vs_orc, player);
                            ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                            break;

                        case Race.gnome:
                            player.icon_size = 1;
                            ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                            ovr024.add_affect(false, 0xff, 0, Affects.gnome_vs_man_sized_giant, player);
                            ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                            ovr024.add_affect(false, 0xff, 0, Affects.affect_30, player);
                            break;

                        case Race.elf:
                            player.icon_size = 2;
                            ovr024.add_affect(false, 0xff, 0, Affects.elf_resist_sleep, player);
                            break;

                        case Race.half_elf:
                            player.icon_size = 2;
                            ovr024.add_affect(false, 0xff, 0, Affects.halfelf_resistance, player);
                            break;

                        default:
                            player.icon_size = 2;
                            break;
                    }

                    player._class = HillsFarClassMap[hf_player.field_35 & 0x0F];
                    player.age = hf_player.age;

                    player.cleric_lvl = (hf_player.field_B7 > 0) ? (byte)1 : (byte)0;
                    player.magic_user_lvl = (hf_player.field_B8 > 0) ? (byte)1 : (byte)0;
                    player.fighter_lvl = (hf_player.field_B9 > 0) ? (byte)1 : (byte)0;
                    player.thief_lvl = (hf_player.field_BA > 0) ? (byte)1 : (byte)0;
                    player.HitDice = 1;
                    player.sex = hf_player.field_2C;
                    player.alignment = hf_player.alignment;
                    player.exp = hf_player.field_2E;

                    if (player.magic_user_lvl > 0)
                    {
                        player.LearnSpell(Spells.detect_magic_MU);
                        player.LearnSpell(Spells.read_magic);
                        player.LearnSpell(Spells.shield);
                        player.LearnSpell(Spells.sleep);
                    }

                    SilentTrainPlayer();

                    ovr022.addPlayerGold(300);
                    gbl.SelectedPlayer = PreviousSelectedPlayer;
                    player.hit_point_max = hf_player.field_21;
                    player.hit_point_rolled = (byte)(player.hit_point_max - ovr018.get_con_hp_adj(player));
                    player.hit_point_current = hf_player.field_20;
                }
            }

            return player;
        }

        internal static Player load_mob(int monster_id)
        {
            return load_mob(monster_id, true);
        }

        internal static Player load_mob(int monster_id, bool exit)
        {
            string area_text = gbl.game_area.ToString();

            byte[] data;
            short decode_size;
            seg042.load_decode_dax(out data, out decode_size, monster_id, "MON" + area_text + "CHA.dax");

            if (decode_size == 0)
            {
                if (exit)
                {
                    seg041.DisplayAndPause("Unable to load monster", 15);
                    seg043.print_and_exit();
                }
                else
                {
                    return null;
                }
            }

            var player = new Player(data, 0);

            seg042.load_decode_dax(out data, out decode_size, monster_id, "MON" + area_text + "SPC.dax");

            if (decode_size != 0)
            {
                int offset = 0;

                do
                {
                    var affect = new Affect(data, offset);
                    player.affects.Add(affect);

                    offset += 9;
                } while (offset < decode_size);
            }

            seg042.load_decode_dax(out data, out decode_size, monster_id, "MON" + area_text + "ITM.dax");

            if (decode_size != 0)
            {
                for (int offset = 0; offset < decode_size; offset += Item.StructSize)
                {
                    player.items.Add(new Item(data, offset));
                }
            }

            seg043.clear_keyboard();

            return player;
        }

        internal static void load_npc(int monster_id) // sub_4A57D
        {
            if (gbl.area2_ptr.party_size <= 7)
            {
                Player player = load_mob(monster_id);

                player.mod_id = (byte)monster_id;

                AssignPlayerIconId(player);

                ovr034.chead_cbody_comspr_icon(player.icon_id, monster_id, "CPIC");
            }
        }

        internal static void AssignPlayerIconId(Player player) // sub_4A60A
        {
            player.icon_id = 0xff;

            gbl.TeamList.Add(player);
            gbl.SelectedPlayer = player;

            bool[] icon_slot = new bool[8];

            foreach (Player tmpPlayer in gbl.TeamList)
            {
                if (tmpPlayer.icon_id >= 0 && tmpPlayer.icon_id < 8)
                {
                    icon_slot[tmpPlayer.icon_id] = true;
                }
            }

            // Now find the lowest free icon slot.
            player.icon_id = 0;

            while (player.icon_id < 8 &&
                icon_slot[player.icon_id])
            {
                player.icon_id += 1;
            }

            gbl.area2_ptr.party_size++;

            if (player.control_morale >= Control.NPC_Base)
            {
                ovr026.ReclacClassBonuses(player);
            }
        }

        internal static void loadGameMenu() // loadGame
        {
            gbl.import_from = ImportSource.Curse;

            var games_list = GetGameList();

            if (games_list.Length == 0)
                return;

            games_list = games_list.TrimEnd();

            bool stop_loop = false;
            char save_letter = '\0';
            do
            {
                bool speical_key;
                char input_key = ovr027.displayInput(out speical_key, false, 0, gbl.defaultMenuColors, games_list, "Load Which Game: ");

                stop_loop = input_key == 0x00; // Escape
                save_letter = '\0';

                if (save_game_keys.MemberOf(input_key))
                {
                    save_letter = input_key;
                    string file_name = Path.Combine(Config.SavePath, "savgam" + save_letter + ".dat");
                    stop_loop = _fileManager.Exists(file_name);
                }
            } while (stop_loop == false);

            if (save_letter != '\0')
            {
                string file_name = Path.Combine(Config.SavePath, "savgam" + save_letter + ".dat");

                loadSaveGame(file_name);
            }
        }

        private static string GetGameList()
        {
            string games_list = string.Empty;

            for (char save_letter = 'A'; save_letter <= 'J'; save_letter++)
            {
                string file_name = Path.Combine(Config.SavePath, "savgam" + save_letter + ".dat");

                if (_fileManager.Exists(file_name))
                {
                    games_list += save_letter + " ";
                }
            }
            return games_list;
        }

        internal static void loadSaveGame(string file_name)
        {
            OpenOrCreateFile file;
            _fileManager.find_and_open_file(out file, true, file_name);

            ovr027.ClearPromptArea();
            seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);
            gbl.reload_ecl_and_pictures = true;

            byte[] data = new byte[0x2000];

            using (file)
            {
                file.BlockRead(1, data);
                gbl.game_area = data[0];

                file.BlockRead(0x800, data);
                gbl.area_ptr = new Area1(data, 0);

                file.BlockRead(0x800, data);
                gbl.area2_ptr = new Area2(data, 0);

                file.BlockRead(0x400, data);
                gbl.stru_1B2CA = new Struct_1B2CA(data, 0);

                file.BlockRead(0x1E00, data);
                gbl.ecl_ptr = new EclBlock(data, 0);

                file.BlockRead(5, data);
                gbl.mapPosX = (sbyte)data[0];
                gbl.mapPosY = (sbyte)data[1];
                gbl.mapDirection = data[2];
                gbl.mapWallType = data[3];
                gbl.mapWallRoof = data[4];

                file.BlockRead(1, data);
                gbl.last_game_state = (GameState)data[0];

                file.BlockRead(1, data);
                gbl.game_state = (GameState)data[0];

                for (int i = 0; i < 3; i++)
                {
                    file.BlockRead(2, data);
                    gbl.setBlocks[i].blockId = Sys.ArrayToShort(data, 0);

                    file.BlockRead(2, data);
                    gbl.setBlocks[i].setId = Sys.ArrayToShort(data, 0);
                }

                file.BlockRead(1, data);
                file.BlockRead(0x148, data);
            }
            int number_of_players = data[0];
            string[] var_148 = Sys.ArrayToStrings(data, 0, System.Math.Min(0x148, 0x29 * number_of_players), 0x29);
            //gbl.PicsOn = ((gbl.area_ptr.pics_on >> 1) != 0);
            //gbl.AnimationsOn = ((gbl.area_ptr.pics_on & 1) != 0);
            gbl.game_speed_var = gbl.area_ptr.game_speed;
            gbl.area2_ptr.party_size = 0;

            for (int index = 0; index < number_of_players; index++)
            {
                string var_1F6 = _stringCleaner.clean_string(var_148[index]);

                if (_fileManager.Exists(Path.Combine(Config.SavePath, var_1F6 + ".sav")))
                {
                    var player = new Player();

                    import_char01(ref player, var_1F6 + ".sav");
                    AssignPlayerIconId(player);
                }
            }

            foreach (Player tmp_player in gbl.TeamList)
            {
                remove_player_file(tmp_player);
            }

            foreach (Player tmp_player in gbl.TeamList)
            {
                gbl.SelectedPlayer = tmp_player;

                if (gbl.SelectedPlayer.control_morale < Control.NPC_Base)
                {
                    LoadPlayerCombatIcon(true);
                }
                else
                {
                    ovr034.chead_cbody_comspr_icon(gbl.SelectedPlayer.icon_id, gbl.SelectedPlayer.mod_id, "CPIC");
                }
            }


            gbl.SelectedPlayer = gbl.TeamList[0];

            gbl.game_area = gbl.area2_ptr.game_area;

            if (gbl.area_ptr.inDungeon != 0)
            {
                if (gbl.game_state != GameState.StartGameMenu)
                {
                    if (gbl.setBlocks[0].blockId > 0)
                    {
                        ovr031.Load3DMap(gbl.area_ptr.current_3DMap_block_id);
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (gbl.setBlocks[i].blockId > 0)
                        {
                            ovr031.LoadWalldef(gbl.setBlocks[i].setId, gbl.setBlocks[i].blockId);
                        }
                    }
                }
            }
            else
            {
                ovr030.load_bigpic(0x79);
            }

            seg043.clear_keyboard();
            ovr027.ClearPromptArea();

            gbl.last_game_state = gbl.game_state;

            gbl.game_state = GameState.StartGameMenu;
        }

        internal static void SaveGame()
        {
            char inputKey;
            using (var save_file = new OpenOrCreateFile())
            {
                string[] var_171 = new string[9];

                do
                {
                    inputKey = ovr027.displayInput((gbl.game_state == GameState.Camping), 0, gbl.defaultMenuColors, "A B C D E F G H I J", "Save Which Game: ");

                } while (unk_4AEA0.MemberOf(inputKey) == false);

                if (inputKey != '\0')
                {
                    gbl.import_from = ImportSource.Curse;

                    short var_1FC;

                    do
                    {
                        save_file.Assign(Path.Combine(Config.SavePath, "savgam" + inputKey + ".dat"));
                        save_file.Rewrite();
                        var_1FC = gbl.FIND_result;

                        if (unk_4AEEF.MemberOf(var_1FC) == false)
                        {
                            seg041.DisplayAndPause("Unexpected error during save: " + var_1FC, 14);
                            return;
                        }
                    } while (unk_4AEEF.MemberOf(var_1FC) == false);

                    ovr027.ClearPromptArea();
                    seg041.displayString("Saving...Please Wait", 0, 10, 0x18, 0);

                    gbl.area_ptr.game_speed = (byte)gbl.game_speed_var;
                    gbl.area_ptr.pics_on = (byte)(((gbl.PicsOn) ? 0x02 : 0) | ((gbl.AnimationsOn) ? 0x01 : 0));
                    gbl.area2_ptr.game_area = gbl.game_area;

                    byte[] data = new byte[0x1E00];

                    data[0] = gbl.game_area;
                    save_file.BlockWrite(1, data);

                    save_file.BlockWrite(0x800, gbl.area_ptr.ToByteArray());
                    save_file.BlockWrite(0x800, gbl.area2_ptr.ToByteArray());
                    save_file.BlockWrite(0x400, gbl.stru_1B2CA.ToByteArray());
                    save_file.BlockWrite(0x1E00, gbl.ecl_ptr.ToByteArray());

                    data[0] = (byte)gbl.mapPosX;
                    data[1] = (byte)gbl.mapPosY;
                    data[2] = gbl.mapDirection;
                    data[3] = gbl.mapWallType;
                    data[4] = gbl.mapWallRoof;
                    save_file.BlockWrite(5, data);

                    data[0] = (byte)gbl.last_game_state;
                    save_file.BlockWrite(1, data);
                    data[0] = (byte)gbl.game_state;
                    save_file.BlockWrite(1, data);

                    for (int i = 0; i < 3; i++)
                    {
                        Sys.ShortToArray((short)gbl.setBlocks[i].blockId, data, (i * 4) + 0);
                        Sys.ShortToArray((short)gbl.setBlocks[i].setId, data, (i * 4) + 2);
                    }
                    save_file.BlockWrite(12, data);

                    for(int i=1; i<=gbl.TeamList.Count; i++)
                    {
                        var_171[i-1] = "CHRDAT" + inputKey + i;
                    }

                    data[0] = (byte)gbl.TeamList.Count;
                    save_file.BlockWrite(1, data);

                    for (int i = 0; i < gbl.TeamList.Count; i++)
                    {
                        Sys.StringToArray(data, 0x29 * i, 0x29, var_171[i]);
                    }
                    save_file.BlockWrite(0x148, data);
                }
                var party_count = 0;
                foreach (Player tmp_player in gbl.TeamList)
                {
                    party_count++;
                    SavePlayer("CHRDAT" + inputKey + party_count, tmp_player);
                    remove_player_file(tmp_player);
                }

                gbl.gameSaved = true;
                ovr027.ClearPromptArea();
            }
        }
    }
}
