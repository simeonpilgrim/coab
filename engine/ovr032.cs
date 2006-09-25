using Classes;

namespace engine
{
    class ovr032
    {
        internal static short sub_73005( int arg_0 )
        {
            short var_2;
   
			if( arg_0 < 0 )
			{
				var_2 = -1;
			}
			else if( arg_0 > 0 )
			{
				var_2 = 1;
			}
			else
			{
				var_2 = 0;
			}

			return var_2;
        }


        static void sub_73033( )
        {
            if( gbl.byte_1D1C0 > 1 )
            {
                for( int var_1 = 1; var_1 <= (gbl.byte_1D1C0 - 1); var_1++ )
                {
                    for( int var_2 = var_1 + 1; var_2 <= gbl.byte_1D1C0; var_2++ )
                    {
                        byte var_4 = gbl.unk_1D1C1[ var_1 ].field_2;
                        byte var_3 = gbl.unk_1D1C1[ var_2 ].field_2;

						if( gbl.unk_1D1C1[ var_2 ].field_1 < gbl.unk_1D1C1[ var_1 ].field_1 ||
							( gbl.unk_1D1C1[ var_2 ].field_1 == gbl.unk_1D1C1[ var_1 ].field_1 &&
							  var_3 < var_4 &&
							  ( var_3 % 2 ) <= ( var_4 % 2 ) ) )
						{
							gbl.Struct_1D1C1 var_7 = gbl.unk_1D1C1[ var_1 ];
							gbl.unk_1D1C1[ var_1 ] = gbl.unk_1D1C1[ var_2 ];
							gbl.unk_1D1C1[ var_2 ] = var_7;
						}
                    }
                }
            }
        }


        internal static void sub_731A5(Struct_XXXX arg_0)
        {
            arg_0.field_0E = arg_0.field_00;
            arg_0.field_10 = arg_0.field_02;
            arg_0.field_0A = (short)(System.Math.Abs(arg_0.field_04 - arg_0.field_00));
            arg_0.field_0C = (short)(System.Math.Abs(arg_0.field_06 - arg_0.field_02));

            arg_0.field_12 = sub_73005(arg_0.field_04 - arg_0.field_00);
            arg_0.field_14 = sub_73005(arg_0.field_06 - arg_0.field_02);

            arg_0.field_08 = 0;
            arg_0.field_16 = 0;           
        }

        internal static bool sub_7324C(Struct_XXXX arg_0)
        {
            bool var_1 = false;
            sbyte var_2 = 1;
            sbyte var_3 = 1;

            Struct_XXXX var_7 = arg_0;

            if (var_7.field_0A > var_7.field_0C)
            {
                if (var_7.field_0E != var_7.field_04)
                {
                    var_7.field_0E += var_7.field_12;
                    var_2 = (sbyte)(var_7.field_12 + 1);

                    var_7.field_08 += var_7.field_0C;
                    var_7.field_08 += var_7.field_0C;
                    var_7.field_16 += 2;

                    if (var_7.field_08 >= var_7.field_0A)
                    {
                        var_7.field_08 -= var_7.field_0A;
                        var_7.field_08 -= var_7.field_0A;

                        var_7.field_10 += var_7.field_14;
                        var_3 = (sbyte)(var_7.field_14 + 1);
                        var_7.field_16 += 1;
                    }

                    var_1 = true;
                }
            }
            else if( var_7.field_10 != var_7.field_06 )
            {
                var_7.field_10 += var_7.field_14;
                var_3 = (sbyte)(var_7.field_14 + 1);

                var_7.field_08 += var_7.field_0A;
                var_7.field_08 += var_7.field_0A;
                var_7.field_16 += 2;

                if (var_7.field_08 >= var_7.field_0C)
                {
                    var_7.field_08 -= var_7.field_0C;
                    var_7.field_08 -= var_7.field_0C;
                    var_7.field_0E = var_7.field_12;
                    var_2 = (sbyte)(var_7.field_12 + 1);
                    var_7.field_16 += 1;
                }

                var_1 = true;
            }

            var_7.field_17 = unk_1886A[(var_3 * 3) + var_2];


            return var_1;
        }


        static byte[] unk_1886A = {
									  7, 0, 1, 6, 8, 2, 5, 4, 3, 8, 4, 2, 1, 0, 0,  
									  0x55, 0x55, 0xAA, 0xAA, 0xFF, 0xFF, 0, 0, 0,  
									  1, 2, 2, 2, 3, 0, 1, 1, 1, 2, 2, 3, 3 
								  };


        internal static bool sub_733F1( Struct_1D1BC arg_0, ref short arg_4, ref sbyte arg_8, ref sbyte arg_C, sbyte arg_10, sbyte arg_12 )
        {
            short var_35;
            bool var_33;
            bool var_32;

            Struct_XXXX var_31 = new Struct_XXXX();
            Struct_XXXX var_19 = new Struct_XXXX();

            var_35 = (short)(arg_4 * 2);
            var_19.field_00 = arg_12;
            var_19.field_02 = arg_10;
            var_19.field_04 = arg_C;
            var_19.field_06 = arg_8;

            sub_731A5( var_19 );

			var_31.field_00 = 0;

			var_31.field_02 = gbl.unk_189B4[ arg_0[ arg_12, arg_10 ] ].field_1;

			if( var_19.field_0A > var_19.field_0C )
			{
				var_31.field_04 = var_19.field_0A;
			}
			else
			{
				var_31.field_04 = var_19.field_0C;
			}

			var_31.field_06 = gbl.unk_189B4[ arg_0[ arg_12, arg_10 ] ].field_1;
            sub_731A5( var_31 );
            var_32 = false;

			do
			{
				if( ( arg_0.field_6 == 0 && gbl.unk_189B4[ arg_0[ var_19.field_0E, var_19.field_10 ] ].field_2 > var_31.field_0A ) ||
					var_19.field_16 > var_35  )
				{
					arg_C = (sbyte)var_19.field_0E;
					arg_8 = (sbyte)var_19.field_10;
					arg_4 = (sbyte)var_19.field_16;

					return false;
				}

				var_33 = sub_7324C( var_31 );
                var_32 = !sub_7324C(var_19);
			}while( var_32 == false );

			arg_4 = (sbyte)var_19.field_16;

			return true;
        }


        internal static bool sub_7354A( byte arg_0, sbyte arg_2, sbyte arg_4, sbyte arg_6, sbyte arg_8 )
        {
            sbyte var_3;
            sbyte var_2;
            bool var_1;

			if( arg_8 < 0 ||
				arg_8 > 0x31 ||
				arg_6 < 0 ||
				arg_6 > 0x18 ||
				arg_4 < 0 ||
				arg_4 > 0x31 ||
				arg_2 < 0 ||
				arg_2 > 0x18 )
			{
				return false;
			}

			if( arg_0 == 0xff )
			{
				arg_0 = 8;
			}

			var_2 = (sbyte)(arg_8 + gbl.unk_189A6[ arg_0 ]);
			var_3 = (sbyte)(arg_6 + gbl.unk_189AF[ arg_0 ]);

			if( ( arg_8 == arg_4 && arg_6 == arg_2 ) ||
				( var_2 == arg_4 && var_3 ==arg_2 ) )
			{
				 return true;
			}

			switch( arg_0 )
			{
				case 0:
					if( ( arg_4 >= var_2 && arg_2 <= ( ( var_2 - arg_4 ) + var_3 ) ) ||
						( arg_4 <= var_2 && arg_2 <= ( ( arg_4 - var_2 ) + var_3 ) ) )
					{ 
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 1:
					if( ( arg_4 >= var_2 && arg_2 <= ( ( var_2 - arg_4 ) + var_3 ) ) ||
						( arg_4 >= ( ( var_2 - var_3 ) + arg_2 ) && arg_2 <= var_3 ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 2:
					if( ( arg_4 >= ( var_2 + var_3 - arg_2 ) && arg_2 <= var_3 ) ||
						( arg_4 >= ( var_2 + arg_2 - var_3 ) && arg_2 >= var_3 ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}					
					break;

				case 3:
					if( ( arg_4 >= ( ( var_2 + arg_2 ) - var_3 ) && arg_2 >= var_3 ) ||
						( arg_4 >= var_2 && arg_2 >= ( ( arg_4 - var_2 ) + var_3 ) ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 4:
					if( ( arg_4 >= var_2 && arg_2 >= ( ( arg_4 - var_2 ) + var_3 ) ) ||
						( arg_4 <= var_2 && arg_2 >= ( ( var_2 - arg_4 ) + var_3 ) ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 5:
					if( ( arg_4 <= var_2 && arg_2 >= ( ( var_2 - arg_4 ) + var_3 ) ) ||
						( arg_4 <= ( ( var_2 + var_3 ) - arg_2 ) && arg_2 >= var_3 ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 6:
					if( ( arg_4 <= ( ( var_2 + var_3 ) - arg_2 ) && arg_2 >= var_3 ) ||
						( arg_4 <= ( ( var_2 + arg_2 ) - var_3 ) && arg_2 <= var_3 ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 7:
					if( ( arg_4 <= ( ( var_2 + arg_2 ) - var_3 ) && arg_2 <= var_3 ) ||
						( arg_4 <= var_2 && arg_2 <= ( ( arg_4 - var_2 ) + var_3 ) ) )
					{
						var_1 = true;
					}
					else
					{
						var_1 = false;
					}
					break;

				case 8:
					var_1 = true;
					break;

				default:
					throw new System.ApplicationException("Switch value unexpected");
			}

			return var_1;
        }


        internal static void sub_738D8( Struct_1D1BC arg_0, byte arg_4, byte arg_6, short arg_8, sbyte arg_A, sbyte arg_C )
        {
            byte var_20;
            short var_1F;
            short var_1D;
            sbyte[] var_1B = new sbyte[ 4 ];
            sbyte[] var_17 = new sbyte[ 4 ];
			sbyte[] var_13 = new sbyte[ 4 ];
			sbyte[] var_F = new sbyte[ 4 ];
			sbyte var_B;
            sbyte var_A;
            sbyte var_9;
            sbyte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

			var_6 = 255; /* put here because of unused error */
			var_5 = 255; /* put here because of unused error */

			for( var_3 = 0; var_3 <= 3; var_3++ )
			{
				if( ovr033.sub_7400F( out var_B, out var_A, var_3, arg_4 ) == true )
				{
					var_F[ var_3 ] = (sbyte)(var_A + arg_C);
					var_13[ var_3 ] = (sbyte)(arg_A + var_B);
				}
				else
				{
					var_F[ var_3 ] = -1;
				}
			}

            gbl.byte_1D1C0 = 0;
			var_20 = gbl.stru_1C9CD[0].field_3;

			for( var_1 = 1; var_1 <= var_20; var_1++ )
			{
				if( gbl.stru_1C9CD[ var_1 ].field_3 > 0 )
				{
					var_7 = 0;
					var_1F = 0x0FF;

					for( var_4 = 0; var_4 <= 3; var_4++ )
					{
						if( ovr033.sub_7400F( out var_B, out var_A, var_4, gbl.stru_1C9CD[ var_1 ].field_3 ) == true )
						{
							var_17[ var_4 ] = (sbyte)( gbl.stru_1C9CD[ var_1 ].field_0 + var_A );
							var_1B[ var_4 ] = (sbyte)( gbl.stru_1C9CD[ var_1 ].field_1 + var_B );
						}
						else
						{
							var_17[ var_4 ] = -1;
						}
					}

					for( var_4 = 0; var_4 <= 3; var_4++ )
					{
						if( var_17[ var_4 ] >= 0 )
						{
							for( var_3 = 0; var_3 <= 3; var_3++ )
							{
								if( var_F[ var_3 ] >= 0 &&
									sub_7354A( arg_6, var_1B[ var_4 ], var_17[ var_4 ], var_13[ var_3 ], var_F[ var_3 ]) == true )
								{
									var_8 = var_17[ var_4 ];
									var_9 = var_1B[ var_4 ];
									var_1D = arg_8;

									if( sub_733F1( arg_0, ref var_1D, ref var_9, ref var_8, var_13[ var_3 ], var_F[ var_3 ] ) == true )
									{
										var_7 = 1;

										if( var_1D < var_1F )
										{
											var_1F = var_1D;
											var_5 = var_3;
											var_6 = var_4;
										}
									}
								}
							}
						}
					}

					if( var_7 != 0 )
					{
						gbl.byte_1D1C0++;

						gbl.unk_1D1C1[ gbl.byte_1D1C0 ].field_0 = var_1;
						gbl.unk_1D1C1[ gbl.byte_1D1C0 ].field_1 = (byte)var_1F;
						var_2 = 0;

						if( arg_6 < 8 )
						{
							var_2 = arg_6;
						}
						else
						{
							while( sub_7354A( var_2, var_1B[ var_6 ], var_17[ var_6 ], var_13[ var_5 ], var_F[ var_5 ] ) == false )
							{
								var_2++;
							}
						}

						gbl.unk_1D1C1[ gbl.byte_1D1C0 ].field_2 = var_2;
					}
				}
			}

            sub_73033();
        }
    }
}
