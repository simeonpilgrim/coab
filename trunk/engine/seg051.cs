using Classes;
using System.Collections.Generic;

namespace engine
{
    class seg051
    {
		static System.Random random_number;
        /*static int random_number;*/

        internal static void o_1_a_87_i_tpdos_idc_tpdos_l_tptv()
        {
            gbl.known01_01 = new Text();
            gbl.known01_01.Assign(string.Empty, Text.AssignType.Write);

            gbl.known01_02 = new Text();
            gbl.known01_02.Assign(string.Empty, Text.AssignType.Write);
        }


        internal static bool fake_mem_check( )
        {
			return false;
        }


        internal static void sub_1474E( byte arg_12 )
        {
            throw new System.NotSupportedException();//sti
            throw new System.NotSupportedException();//add	sp, 6
            throw new System.NotSupportedException();//pop	ax
            throw new System.NotSupportedException();//and	di, 0x1F
            throw new System.NotSupportedException();//add	di, 0x96
            throw new System.NotSupportedException();//cmp	ah, 0x39
            throw new System.NotSupportedException();//jnb	loc_14762
            throw new System.NotSupportedException();//mov	di, 0x0FFFF
            throw new System.NotSupportedException();//loc_14762:
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//mov	ah, 0x54
            throw new System.NotSupportedException();//int	0x21
            throw new System.NotSupportedException();//mov	bp, sp
            throw new System.NotSupportedException();//or	[bp+arg_12], 1
            throw new System.NotSupportedException();//pop	ax
            throw new System.NotSupportedException();//pop	bx
            throw new System.NotSupportedException();//pop	cx
            throw new System.NotSupportedException();//pop	dx
            throw new System.NotSupportedException();//pop	si
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//pop	ds
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//iret
        }


        static void RTE_handler_ff( )
        {
			RTE_handler_00_00( 0xff );
        }


		internal static void RTE_handler_00_00( ushort AX )
		{
			in_RTE_handler( AX, 0, 0 );
		}

		internal static void in_RTE_handler( ushort AX, ushort BX, ushort CX )
		{
			ushort RTE_seg;
			ushort RTE_addr;
			ushort RTE_code;

            RTE_code = AX;
            RTE_addr = CX;
            RTE_seg = BX;

            if (gbl.dword_1AAF0 != null)
            {
                gbl.dword_1AAF0 = null;
                gbl.dos_call_result = 0;
                //gbl.dword_1AAF0(0x123);
                return;
            }

            Close( gbl.known01_01 );
 
            if( ( RTE_addr | RTE_seg ) != 0 )
            {
                System.Console.Out.WriteLine( "Runtime error {0} at {1}:{2}.\r\n", RTE_code, RTE_seg, RTE_addr );
            }

            
            seg001.EngineThread.Abort();
            //throw new System.ApplicationException(string.Format("Exit Code: {0}", RTE_code));
        }


        internal static byte[] GetMem( int arg_0 )
        /*TODO REMOVE THIS*/
        {
            return new byte[arg_0];
        }


        internal static void FreeMem( int arg_0, object arg_2 )
        /*TODO REMOVE THIS*/
        {
            
        }

        internal static void FreeMem( byte[] arg_2)
        /*TODO REMOVE THIS*/
        {

        }

        internal static int MemAvail( )
        {
			return 0x12345678;
        }


        internal static short IOResult( )
        {
            short tmp = gbl.dos_call_result;
            gbl.dos_call_result = 0;

			return tmp;
        }

        internal static void Rewrite(Text arg_2)
        {
            /* Todo: not sure what rewrite is trying todo */
        }


        internal static void Close( Text arg_2 )
        {
            if (arg_2 != null)
            {
                if (arg_2.reader != null)
                {
                    arg_2.reader.Close();
                }
                if (arg_2.writer != null)
                {
                    arg_2.writer.Close();
                }
            }
        }


        static void sub_14C9B( )
        {
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//call	int ptr es:[bx+di]
            throw new System.NotSupportedException();//or	ax, ax
            throw new System.NotSupportedException();//jz	func_end
            throw new System.NotSupportedException();//mov	dos_call_result, ax
            throw new System.NotSupportedException();//func_end:
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//retn
        }

        /// <summary>
        /// Set JNZ flag if anything is wrong.
        /// BX = arg_0.field_8
        /// DX = arg_0.field_A
        /// ES:DI = field_C
        /// </summary>
        /// <param name="arg_0"></param>
        static void __GetEntry( Text arg_0 )
        {
            if (gbl.dos_call_result == 0 &&
                arg_0.field_2 != 0xd7b1)
            {
                gbl.dos_call_result = 0x68;
            }

            throw new System.NotSupportedException();//mov	bx, es:[di+unknown01.field_8]
            throw new System.NotSupportedException();//mov	dx, es:[di+unknown01.field_A]
            throw new System.NotSupportedException();//les	di, int ptr es:[di+unknown01.field_C]
        }


        static void __GetChar( )
        {
            throw new System.NotSupportedException();//cmp	bx, dx
            throw new System.NotSupportedException();//jz	loc_14E43
            throw new System.NotSupportedException();//loc_14E3E:
            throw new System.NotSupportedException();//mov	al, es:[bx+di]
            throw new System.NotSupportedException();//clc
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//loc_14E43:
            throw new System.NotSupportedException();//mov	di, sp
            throw new System.NotSupportedException();//les	di, ss:[di+2]
            throw new System.NotSupportedException();//call	sub_14E89
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//cmp	bx, dx
            throw new System.NotSupportedException();//jnz	loc_14E3E
            throw new System.NotSupportedException();//mov	al, 0x1A
            throw new System.NotSupportedException();//stc
            throw new System.NotSupportedException();//retn
        }


        static void __PutEntry( Text arg_0 )
        {
            if (gbl.dos_call_result == 0 &&
                arg_0.field_2 != 0xD7B2)
            {
                gbl.dos_call_result = 0x69;
            }

            throw new System.NotSupportedException();//bx = arg_0.field_8;
            throw new System.NotSupportedException();//dx = arg_0.field_4;

            throw new System.NotSupportedException();//mov	bx, es:[di+8]
            throw new System.NotSupportedException();//mov	dx, es:[di+4]
            throw new System.NotSupportedException();//les	di, es:[di+0Ch]
        }


        static void __PutChar( )
        {
            throw new System.NotSupportedException();//mov	es:[bx+di], al
            throw new System.NotSupportedException();//inc	bx
            throw new System.NotSupportedException();//cmp	bx, dx
            throw new System.NotSupportedException();//jz	loc_14E83
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//loc_14E83:
            throw new System.NotSupportedException();//mov	di, sp
            throw new System.NotSupportedException();//les	di, ss:[di+2]
        }


        static void sub_14E89( )
        {
            throw new System.NotSupportedException();//push	cx
            throw new System.NotSupportedException();//push	si
            throw new System.NotSupportedException();//push	ds
            throw new System.NotSupportedException();//mov	dx, 0x1631
            throw new System.NotSupportedException();//mov	ds, dx
            throw new System.NotSupportedException();//mov	es:[di+8], bx
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//call	int ptr es:[di+14h]
            throw new System.NotSupportedException();//or	ax, ax
            throw new System.NotSupportedException();//jz	loc_14EA4
            throw new System.NotSupportedException();//mov	dos_call_result, ax
            throw new System.NotSupportedException();//loc_14EA4:
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//mov	ax, es:[di+0Ah]
            throw new System.NotSupportedException();//mov	bx, es:[di+8]
            throw new System.NotSupportedException();//mov	dx, es:[di+4]
            throw new System.NotSupportedException();//les	di, es:[di+0Ch]
            throw new System.NotSupportedException();//pop	ds
            throw new System.NotSupportedException();//pop	si
            throw new System.NotSupportedException();//pop	cx
            throw new System.NotSupportedException();//retn
        }


        internal static void ReadLn( Text arg_0 )
        {
            arg_0.reader.ReadLine();
        }


        internal static void WriteLn( Text arg_0 )
        {
            arg_0.writer.WriteLine();
        }


        internal static void Write( Text arg_0 )
        //Write(Text far &)
        {
            throw new System.NotSupportedException();//les	di, [bp+arg_0]
            sub_14F19();
        }


        static void sub_14F19( )
        {
            throw new System.NotSupportedException();//cmp	short ptr es:[di+1Ah], 0
            throw new System.NotSupportedException();//jnz	loc_14F21
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//loc_14F21:
            throw new System.NotSupportedException();//cmp	dos_call_result, 0
            throw new System.NotSupportedException();//jnz	locret_14F35
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//call	int ptr es:[di+18h]
            throw new System.NotSupportedException();//or	ax, ax
            throw new System.NotSupportedException();//jz	locret_14F35
            throw new System.NotSupportedException();//mov	dos_call_result, ax
            throw new System.NotSupportedException();//locret_14F35:
            throw new System.NotSupportedException();//retn
        }

        internal static char Read(Text arg_0)
        {
            return (char)arg_0.reader.Read();
        }

		/// <summary>
		/// leaves arg_6 on the stack.
		/// </summary>
        internal static void Read( short arg_0, out string arg_2, Text arg_6 )
        {
            char[] buff = new char[arg_0];

            arg_6.reader.Read(buff,0,arg_0);

            arg_2 = Sys.ArrayToString(buff, 0, arg_0);
        }

        internal static void Write(short arg_0, char arg_2, Text arg_4)
        {
            arg_4.writer.Write(string.Empty.PadLeft(arg_0));
            arg_4.writer.Write(arg_2);
        }

        internal static void Write( short arg_0, string arg_2, Text arg_6 )
        {
            arg_6.writer.Write(string.Empty.PadLeft(arg_0));
            arg_6.writer.Write(arg_2);
        }

		/// <summary>
		/// leaves arg_6 on the stack.
		/// </summary>
		internal static void Write( short arg_0, int arg_2, Text arg_6 )
        //Write(Text far &,Longint,Word)
        {
            string var_20 = arg_2.ToString();

            throw new System.NotSupportedException();//les	di, [bp+arg_6]
            throw new System.NotSupportedException();//assume es:nothing
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//call	__PutEntry
            throw new System.NotSupportedException();//jnz	loc_1508A
            throw new System.NotSupportedException();//mov	ax, [bp+arg_0]
            throw new System.NotSupportedException();//sub	ax, cx
            throw new System.NotSupportedException();//jle	loc_1507F
            throw new System.NotSupportedException();//mov	si, cx
            throw new System.NotSupportedException();//mov	cx, ax
            throw new System.NotSupportedException();//loc_15076:
            throw new System.NotSupportedException();//mov	al, 0x20
            throw new System.NotSupportedException();//call	__PutChar
            throw new System.NotSupportedException();//loop	loc_15076
            throw new System.NotSupportedException();//mov	cx, si
            throw new System.NotSupportedException();//loc_1507F:
            throw new System.NotSupportedException();//lea	si, [bp+var_20]
            throw new System.NotSupportedException();//loc_15082:
            throw new System.NotSupportedException();//cld
            throw new System.NotSupportedException();//lods	byte ptr ss:[si+0]
            throw new System.NotSupportedException();//call	__PutChar
            throw new System.NotSupportedException();//loop	loc_15082
            throw new System.NotSupportedException();//loc_1508A:
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//mov	es:[di+8], bx
            throw new System.NotSupportedException();//mov	sp, bp
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//retf	6
        }

        internal static string Copy(int CopyLen, int StartAt, string InString)
        {
            string OutString; 

            if (CopyLen >= InString.Length - StartAt)
            {
                CopyLen = InString.Length - StartAt;
            }

            if (CopyLen > 0)
            {
                OutString = InString.Substring(StartAt, CopyLen);
            }
            else
            {
                OutString = string.Empty;
            }

            return OutString;
        }

		internal static string Copy( int CopyLen, int StartAt, string InString, out string OutString )
        {
            OutString = Copy(CopyLen, StartAt, InString);

            return OutString;
        }


        internal static int Pos( string arg_0, string arg_4 )
        {
            int p = arg_0.IndexOf(arg_4);

            if (p == -1)
            {
                p = arg_0.Length;
            }

            return p;
        }


        internal static void Delete( int deleteLen, int beginAt, ref string arg_4 )
        {
            if( deleteLen > 0 )
            {
                arg_4 = arg_4.Remove( beginAt, deleteLen );
            }
        }


        internal static short Random( short arg_0 )
        {
            if( arg_0 == 0 )
            {
                return 0;
            }

			return (short)(random_number.Next() % arg_0);
        }


        internal static byte Random( byte arg_0 )
        {
            if( arg_0 == 0 )
            {
                return 0;
            }

            return (byte)(random_number.Next() % arg_0);
        }

        internal static double Random__Real( )
        {
			return random_number.NextDouble();
        }


        internal static void Randomize( )
        {
			random_number = new System.Random(unchecked((int)System.DateTime.Now.Ticks)); 
        }


        internal static void Str( short arg_0, out string arg_2, short arg_6, int arg_8 )
        {
            string var_20 = arg_8.ToString();

            int len = var_20.Length;
            //if (len < arg_0)
            //{
            //    var_20 = var_20.PadLeft(arg_0);
            //}
            //else 
            if (len > arg_0)
            {
                var_20 = var_20.Substring(len - arg_0, arg_0);
            }

            arg_2 = var_20;
        }


        internal static void Reset( short arg_2, File arg_4 )
        {
            arg_4.stream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        internal static void Rewrite( short arg_0, File arg_2 )
        {
            arg_2.stream.SetLength(0);

        }


        internal static void Close( File arg_0 )
        {
            arg_0.stream.Close();

            if (checkFILEvalid(arg_0))
			{
				arg_0.status = 0xD7B0;
			}
        }


        static bool checkFILEvalid( File f )
        {
			if( f.status != 0xD7B3 )
			{
				gbl.dos_call_result = 0x67;
				return false;
			}

			return true;
        }


        internal static void Read( int arg_2, File arg_6 )
        //Read(File far &,Any far	&)
        {
            throw new System.NotSupportedException();//mov	ah, 0x3F
            throw new System.NotSupportedException();//mov	dx, 0x64
            throw new System.NotSupportedException();//jmp	short loc_15F9B
            throw new System.NotSupportedException();//Write(File far &,Any far &):
            throw new System.NotSupportedException();//mov	ah, 0x40
            throw new System.NotSupportedException();//mov	dx, 0x65
            throw new System.NotSupportedException();//loc_15F9B:
            throw new System.NotSupportedException();//push	bp
            throw new System.NotSupportedException();//mov	bp, sp
            throw new System.NotSupportedException();//les	di, [bp+arg_6]
            throw new System.NotSupportedException();//call	checkFILEvalid
            throw new System.NotSupportedException();//jnz	func_end
            throw new System.NotSupportedException();//push	ds
            throw new System.NotSupportedException();//push	dx
            throw new System.NotSupportedException();//lds	dx, [bp+arg_2]
            throw new System.NotSupportedException();//mov	cx, es:[di+4]
            throw new System.NotSupportedException();//mov	bx, es:[di]
            throw new System.NotSupportedException();//int	0x21
            throw new System.NotSupportedException();//pop	dx
            throw new System.NotSupportedException();//pop	ds
            throw new System.NotSupportedException();//jb	loc_15FBE
            throw new System.NotSupportedException();//cmp	ax, cx
            throw new System.NotSupportedException();//jz	func_end
            throw new System.NotSupportedException();//mov	ax, dx
            throw new System.NotSupportedException();//loc_15FBE:
            throw new System.NotSupportedException();//mov	dos_call_result, ax
            throw new System.NotSupportedException();//func_end:
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//retf	4
        }

        internal static void BlockRead(int arg_4, byte[] arg_6, File arg_A)
        {
            arg_A.stream.Read(arg_6, 0, arg_4);
        }

        internal static void BlockRead( out short arg_0, int arg_4, byte[] arg_6, File arg_A )
        {
            arg_0 = (short)arg_A.stream.Read(arg_6, 0, arg_4);
        }


        internal static void BlockWrite(int arg_4, byte[] arg_6, File arg_A)
        {
            arg_A.stream.Write(arg_6, 0, arg_4);
        }

        internal static void Seek( int arg_0, File arg_4 )
        {
            arg_4.stream.Seek(arg_0, System.IO.SeekOrigin.Begin);
        }


        internal static int FilePos( File fileA )
        {
            return 0;
        }


        internal static long FileSize( File f )
        {
            System.IO.FileInfo fi = new System.IO.FileInfo( f.name );

			return fi.Length;
        }


        internal static void Erase( File arg_0 )
        {
			System.IO.File.Delete( arg_0.name );
        }


        internal static void Move( short count, byte[] dest, byte[] source )
        {
            System.Array.Copy( source, dest, count );
        }


        internal static void FillChar( byte fill_byte, ushort buffer_size, byte[] buffer )
        {
			for(int i = 0; i < buffer_size; i++)
			{
				buffer[i] = fill_byte;
			}
        }

        static List<string> Param = new List<string>();

        internal static string ParamStr(short arg_0)
        {
            string param;
            if (Param.Count < arg_0)
            {
                param = string.Empty;
            }
            else
            {
                param = Param[arg_0];
            }

            return param;
        }


        internal static void MkDir( string path )
        {
			System.IO.Directory.CreateDirectory( path );
        }


        internal static char UpCase( char arg_0 )
        {
			return char.ToUpper( arg_0 );
        }
    }
}
