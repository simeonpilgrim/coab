using Classes;

namespace engine
{
	class ovr002
	{
		internal static void delay_or_key( short arg_0 )
		{
			int var_6;
			int var_2;

			seg043.clear_keyboard();

			var_2 = seg041.time01();

            var_6 = var_2 + (arg_0 * 100);

			while ( seg049.KEYPRESSED() == false && 
				var_2 < var_6 )
			{
				var_2 = seg041.time01();
			}

			seg043.clear_keyboard();
		}
 
 
		internal static void credits( )
		{
            Display.UpdateStop();

			seg037.draw8x8_02();

			seg041.displayString( "based on the tsr novel 'azure bonds'", 0, 10, 1, 2 );
            seg041.displayString("by:", 0, 10, 2, 6);
            seg041.displayString("kate novak", 0, 11, 2, 9);
            seg041.displayString("and", 0, 10, 2, 0x14);
            seg041.displayString("jeff grubb", 0, 11, 2, 0x18);
            seg041.displayString("scenario created by:", 0, 10, 4, 0x0a);
            seg041.displayString("tsr, inc.", 0, 0x0e, 5, 0x0b);
            seg041.displayString("and", 0, 0x0a, 5, 0x15);
            seg041.displayString("ssi", 0, 0x0e, 5, 0x19);
            seg041.displayString("jeff grubb", 0, 0x0b, 6, 0x0e);
            seg041.displayString("george mac donald", 0x0, 0x0B, 0x7, 0x0B);
            seg041.displayString("game created by:", 0x0, 0x0A, 0x9, 0x1);
            seg041.displayString("ssi special projects", 0x0, 0x0E, 0x9, 0x12);
            seg041.displayString("project leader:", 0x0, 0x0E, 0x0B, 0x2);
            seg041.displayString("george mac donald", 0x0, 0x0B, 0x0C, 0x2);
            seg041.displayString("programming:", 0x0, 0x0E, 0x0E, 0x2);
            seg041.displayString("scot bayless", 0x0, 0x0B, 0x0F, 0x2);
            seg041.displayString("russ brown", 0x0, 0x0B, 0x10, 0x2);
            seg041.displayString("michael mancuso", 0x0, 0x0B, 0x11, 0x2);
            seg041.displayString("development:", 0x0, 0x0E, 0x13, 0x2);
            seg041.displayString("david shelley", 0x0, 0x0B, 0x14, 0x2);
            seg041.displayString("michael mancuso", 0x0, 0x0B, 0x15, 0x2);
            seg041.displayString("oran kangas", 0x0, 0x0B, 0x16, 0x2);
            seg041.displayString("graphic arts:", 0x0, 0x0E, 0x0B, 0x16);
            seg041.displayString("tom wahl", 0x0, 0x0B, 0x0C, 0x16);
            seg041.displayString("fred butts", 0x0, 0x0B, 0x0D, 0x16);
            seg041.displayString("susan manley", 0x0, 0x0B, 0x0E, 0x16);
            seg041.displayString("mark johnson", 0x0, 0x0B, 0x0F, 0x16);
            seg041.displayString("cyrus lum", 0x0, 0x0B, 0x10, 0x16);
            seg041.displayString("playtesting:", 0x0, 0x0E, 0x12, 0x16);
            seg041.displayString("jim jennings", 0x0, 0x0B, 0x13, 0x16);
            seg041.displayString("james kucera", 0x0, 0x0B, 0x14, 0x16);
            seg041.displayString("rick white", 0x0, 0x0B, 0x15, 0x16);
            seg041.displayString("robert daly", 0x0, 0x0B, 0x16, 0x16);

            Display.UpdateStart();
		}


		internal static void title_screen( )
		{
			DaxBlock dax_ptr;

			dax_ptr = null;
            seg040.load_dax(ref dax_ptr, 0, 0, 1, "Title");
            seg040.draw_picture(dax_ptr, 0, 0, 0);

            delay_or_key(5);

			if ( gbl.DisplayFullTitleScreen == true ) 
			{
                seg040.load_dax(ref dax_ptr, 0, 0, 2, "Title");
                seg040.draw_picture(dax_ptr, 0, 0, 0);
                seg040.free_dax_block(ref dax_ptr);

                seg040.load_dax(ref dax_ptr, 0, 0, 3, "Title");
                seg040.draw_picture(dax_ptr, 0x0b, 6, 0);
                seg040.free_dax_block(ref dax_ptr);
                delay_or_key(10);

                seg040.load_dax( ref dax_ptr, 0, 0, 4, "Title" );

                seg044.sub_120E0(gbl.word_188D8);

                seg040.draw_picture(dax_ptr, 0x0b, 0, 0);
                seg040.free_dax_block(ref dax_ptr);
                delay_or_key(10);

				seg041.clear_screan();
				credits();
				delay_or_key( 10 );

				seg041.clear_screan();
			}
		}	
	}
}
