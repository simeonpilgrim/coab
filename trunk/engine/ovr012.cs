using Classes;

namespace engine
{
    class ovr012
    {
        internal static byte keyboardStatus_0417
        {
            /* 0040:0017 keyboard status code. 
             * 0x80 insert on
             * 0x40 caps lock on
             * 0x20 num lock on
             * 0x10 scroll lock on
             * 0x08 alt key down
             * 0x04 control down
             * 0x02 left shift down
             * 0x01 right shift down
             */

            get
            {
                return 0x00;
            }
            set
            {
                //x = value;
            }
        }

        internal static byte keyboardStatus_0418
        {
            /* 0040:0018 keyboard status code. 
             * 0x80 insert key down
             * 0x40 caps lock down
             * 0x20 num lock down
             * 0x10 scroll lock down
             * 0x08 pause on
             * 0x04 sys req down
             * 0x02 left alt down
             * 0x01 left control down
             */

            get
            {
                return 0x00;
            }
            set
            {
                //x = value;
            }
        }

        internal static void sub_39054()
        {
            File unk_1AD74;

            gbl.byte_1EFBA = keyboardStatus_0417;
            keyboardStatus_0417 |= 0x20; // turn num lock on
            keyboardStatus_0418 |= 0x20; // num lock down

            seg051.Randomize();

            gbl.area_ptr = new Area1();
            gbl.area2_ptr = new Area2();
            gbl.stru_1B2CA = new Struct_1B2CA();
            gbl.ecl_ptr = new EclBlock();
            gbl.dax_8x8d1_201 = new byte[0x0588 / 8, 8]; //seg051.GetMem( 0x0588 );
            gbl.stru_1D52C = new byte[3][];
            gbl.stru_1D52C[0] = new byte[0x30C];
            gbl.stru_1D52C[1] = new byte[0x30C];
            gbl.stru_1D52C[2] = new byte[0x30C];
            gbl.stru_1D530 = seg051.GetMem(0x0400);

            for(int i = 0; i < gbl.cmdOppsLimit; i++ )
            {
                gbl.cmd_opps[i] = new Opperation();
                gbl.cmd_opps[i].getMemoryValue = ovr008.vm_GetMemoryValue;
            }

            seg040.init_dax_block(out gbl.cursor_bkup, 0, 1, 1, 8);
            seg040.init_dax_block(out gbl.cursor, 0, 1, 1, 8);

            seg051.FillChar(0xf, (ushort)gbl.cursor.bpp, gbl.cursor.data);

            gbl.symbol_8x8_set = new DaxBlock[5];
            gbl.symbol_8x8_set[0] = null;
            gbl.symbol_8x8_set[1] = null;
            gbl.symbol_8x8_set[2] = null;
            gbl.symbol_8x8_set[3] = null;
            gbl.symbol_8x8_set[4] = null;

            gbl.dax24x24Set = null;
            gbl.dword_1C8FC = null;

            seg040.init_dax_block(out gbl.dax24x24Set, 0, 0x30, 3, 0x18);

            gbl.area_ptr.Clear();

            gbl.area_ptr.field_1CC = 1;
            gbl.area_ptr.field_1E4 = 0;

            gbl.area2_ptr.Clear();

            gbl.stru_1B2CA.Clear();
            gbl.ecl_ptr.Clear();

            gbl.combat_icons = new DaxBlock[26, 2];
            for (int i = 0; i < 26; i++)
            {
                gbl.combat_icons[i, 0] = null;
                gbl.combat_icons[i, 1] = null;
            }

            gbl.byte_1AD44 = 2;
            gbl.current_head_id = 0xff;
            gbl.current_body_id = 0xff;
            gbl.headX_dax = null;
            gbl.bodyX_dax = null;

            gbl.byte_1D556 = new DaxArray();

            gbl.bigpic_dax = null;
            gbl.item_pointer = null;

            gbl.mapPosX = 0;
            gbl.mapPosY = 0;
            gbl.mapDirection = 0;
            gbl.byte_1D53C = 0;
            gbl.byte_1D53D = 0;

            gbl.mapPosX = 7;
            gbl.mapPosY = 0x0D;
            gbl.mapDirection = 0;

            gbl.can_bash_door = true;
            gbl.can_pick_door = true;
            gbl.can_knock_door = true;

            gbl.word_1D53E = 0;
            gbl.word_1D540 = 1;
            gbl.byte_1AD44 = 3;

            gbl.word_1D542 = -1;
            gbl.word_1D544 = -1;

            gbl.word_1D546 = -1;
            gbl.word_1D548 = -1;

            gbl.byte_1B2C0 = 0;
            gbl.AnimationsOn = true;
            gbl.PicsOn = true;
            gbl.DelayBetweenCharacters = true;
            gbl.byte_1B2EB = 0;
            gbl.rest_incounter_count = 0;

            gbl.player_next_ptr = null;
            gbl.player_ptr = null;

            gbl.ecl_offset = 0x8000;
            gbl.game_speed_var = 4;
            gbl.inDemo = false;
            gbl.game_area = 1;
            gbl.game_area_backup = 1;
            gbl.mapAreaDisplay = false;
            gbl.area2_ptr.field_67C = 0;
            gbl.word_1D5BC = 1;
            gbl.combat_type = gbl.combatType.normal;
            gbl.displayPlayerStatusLine18 = false;
            gbl.search_flag_bkup = 0;
            gbl.byte_1EE8A = 0;
            gbl.byte_1EE8B = 0;
            gbl.byte_1EE8C = 0;
            gbl.byte_1B2F0 = 0;
            gbl.byte_1BF12 = 1;
            gbl.displayPlayerSprite = false;
            gbl.byte_1C01A = 0;
            gbl.lastDaxFile = string.Empty;
            gbl.byte_1D5AB = string.Empty;
            gbl.lastDaxBlockId = 0x0FF;
            gbl.byte_1D5B5 = 0x0FF;
            gbl.byte_1C01B = 0;
            gbl.byte_1D913 = 0x0E8;
            gbl.byte_1D912 = 0x0DB;
            gbl.byte_1EE95 = 0;
            gbl.byte_1D910 = true;
            gbl.bigpic_block_id = 0x0FF;
            gbl.byte_1EF9A = 0;
            gbl.byte_1EF9B = 0;
            gbl.byte_1B2F1 = 0;
            gbl.byte_1D5BE = 1;
            gbl.game_state = 4;
            gbl.last_game_state = 0;
            gbl.gameFlag01 = true;
            gbl.byte_1D8AC = false;
            gbl.sky_dax_250 = null;
            gbl.sky_dax_251 = null;
            gbl.sky_dax_252 = null;
            gbl.gameWon = false;
            seg041.load_8x8d1_201();
            ovr027.redraw_screen();
            seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);

            ovr038.Load8x8D(4, 0xca);
            ovr038.Load8x8D(0, 0xcb);

            for (gbl.byte_1AD44 = 0; gbl.byte_1AD44 <= 0x0b; gbl.byte_1AD44++)
            {
                ovr034.chead_cbody_comspr_icon((byte)(gbl.byte_1AD44 + 0x0D), gbl.byte_1AD44, "COMSPR");
            }

            ovr034.chead_cbody_comspr_icon(0x19, 0x19, "COMSPR");

            seg040.load_dax(ref gbl.sky_dax_250, 13, 1, 250, "SKY");
            seg040.load_dax(ref gbl.sky_dax_251, 13, 1, 251, "SKY");
            seg040.load_dax(ref gbl.sky_dax_252, 13, 1, 252, "SKY");

            gbl.byte_1AD48 = seg042.find_and_open_file(out unk_1AD74, 0, "", "ITEMS");
            
            seg051.Reset(unk_1AD74);
            seg051.Seek(2, unk_1AD74);

            byte[] data = new byte[0x810];
            seg051.BlockRead(out gbl.unk_1C8BC, 0x810, data, unk_1AD74);
            gbl.unk_1C020 = new Struct_1C020[0x81];
            for (int i = 0; i < 0x81; i++)
            {
                gbl.unk_1C020[i] = new Struct_1C020(data, i * 0x10);
            }

            seg051.Close(unk_1AD74);
            ovr023.setup_spells();
            ovr013.setup_spells2();
        }


        internal static void sub_396E5()
        {
            gbl.byte_1B2C0 = 1;

            gbl.area_ptr.Clear();
            gbl.area_ptr.field_1CC = 1;
            gbl.area_ptr.field_1E4 = 0;
            gbl.area2_ptr.Clear();
            gbl.stru_1B2CA.Clear();
            gbl.ecl_ptr.Clear();

            gbl.mapPosX = 0;
            gbl.mapPosY = 0;
            gbl.mapDirection = 0;
            gbl.byte_1D53C = 0;
            gbl.byte_1D53D = 0;

            gbl.mapPosX = 7;
            gbl.mapPosY = 0x0D;
            gbl.mapDirection = 2;

            gbl.can_bash_door = true;
            gbl.can_pick_door = true;
            gbl.can_knock_door = true;

            gbl.word_1D53E = 0;
            gbl.word_1D540 = 1;

            gbl.byte_1AD44 = 3;

            gbl.word_1D542 = -1;
            gbl.word_1D544 = -1;
            gbl.word_1D546 = -1;
            gbl.word_1D548 = -1;

            gbl.DelayBetweenCharacters = true;
            gbl.byte_1B2EB = 0;
            gbl.rest_incounter_count = 0;
            gbl.player_next_ptr = null;
            gbl.player_ptr = null;
            gbl.ecl_offset = 0x8000;
            gbl.game_speed_var = 4;
            gbl.game_area = 1;
            gbl.game_area_backup = 1;
            gbl.mapAreaDisplay = false;
            gbl.area2_ptr.field_67C = 0;
            gbl.word_1D5BC = 1;
            gbl.combat_type = gbl.combatType.normal;
            gbl.displayPlayerStatusLine18 = false;
            gbl.search_flag_bkup = 0;
            gbl.byte_1EE8A = 0;
            gbl.byte_1EE8B = 0;
            gbl.byte_1EE8C = 0;
            gbl.byte_1B2F0 = 0;
            gbl.byte_1BF12 = 1;
            gbl.displayPlayerSprite = false;
            gbl.byte_1C01A = 0;
            gbl.lastDaxFile = string.Empty;
            gbl.byte_1D5AB = string.Empty;
            gbl.lastDaxBlockId = 0x0FF;
            gbl.byte_1D5B5 = 0x0FF;
            gbl.byte_1C01B = 0;
            gbl.byte_1D913 = 0x0E8;
            gbl.byte_1D912 = 0x0DB;
            gbl.byte_1EE95 = 0;
            gbl.byte_1D910 = true;
            gbl.bigpic_block_id = 0x0FF;
            gbl.byte_1EF9A = 0;
            gbl.byte_1EF9B = 0;
            gbl.byte_1B2F1 = 0;
            ovr027.redraw_screen();
            gbl.byte_1D5BE = 1;
            gbl.game_state = 4;
            gbl.last_game_state = 0;
            gbl.gameFlag01 = true;
            gbl.byte_1D8AC = false;
            gbl.gameWon = false;
        }
    }
}
