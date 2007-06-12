using Classes;

namespace engine
{
    class ovr028
    {
        static short word_1EF9C;
		static short word_1EF9E;

		internal static void sub_6E005( )
        {
            /*
            word_1EF9C = gbl.area_ptr.field_342.field_A4A;
			word_1EF9E = gbl.area_ptr.field_342.field_A6A;
             */
        }


        internal static void sub_6E02E( )
        {
            seg040.ega_01( gbl.dword_1C8F4, word_1EF9E, word_1EF9C );
 			seg040.draw_picture( gbl.dword_1EFA0, word_1EF9E, word_1EF9C, 0 );
        }


        internal static void sub_6E05D( )
        {
 			seg040.draw_picture( gbl.dword_1C8F4, word_1EF9E, word_1EF9C, 0 );
        }
    }
}
