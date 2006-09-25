using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Set.
	/// </summary>
	public class Set
	{
        byte[] bits;
        const uint arrayLen = 0x20;
        const int shift = 3;
        const int mask = 7;

		public Set()
		{
            bits = new byte[arrayLen];
		}


		public Set( ushort arg_0, byte[] arg_2 )
		{
            bits = new byte[arrayLen];

			int indA = arg_0 >> 8;
			int indB = arg_0 & 0x0f;

			for( int i = 0; i < indA; i++ )
			{
				bits[ i ] = 0;
			}

			for( int i = 0; i< indB; i++ )
			{
				bits[ i + indA ] = arg_2[ i ];
			}

			for( int i = indA + indB; i < 20; i++ )
			{
				bits[ i ] = 0;
			}
		}

        public void Clear( )
        {
            for( int i = 0; i < arrayLen; i++ )
            {
                bits[i] = 0;
            }
        }

        public static Set operator+( Set lhs, byte rhs )
        {
            lhs.bits[rhs>>shift] |= (byte)(1 << ( rhs & mask ));

            return lhs;
        }

        public void SetRange( byte arg_0, byte arg_2 )
        {
            //public static void Set__operator+=( byte arg_0, byte arg_2, int arg_4 )
            //Set::operator+=(Byte,Byte)
            //original left the Set object on the stack.

            for( byte i = arg_2; i <= arg_0; i++ )
            {
                bits[i>>shift] |= (byte)(1 << ( i & mask ));
            }
        }


		public bool MemberOf( char arg_4 )
		{
			byte lhs = bits[arg_4>>shift];
            byte rhs = (byte)(1 << (arg_4 & mask));

			return ( (lhs & rhs) != 0 );
		}

		public bool MemberOf( byte arg_4 )
		{
            byte lhs = bits[arg_4 >> shift];
            byte rhs = (byte)(1 << (arg_4 & mask));

			return ( (lhs & rhs) != 0 );
		}


        //public static void operator+=( int arg_0, int arg_4 )
        //operator+=(Set far &,Set far &)
        //Complex
        //{
        //cld
        //mov	bx, sp
        //mov	dx, ds
        //les	di, ss:[bx+arg_4]
        //lds	si, ss:[bx+arg_0]
        //mov	cx, 0x10
        //loop_top:
        //lodsw
        //or	ax, es:[di]
        //stosw
        //loop	loop_top
        //mov	ds, dx
        //retf	4
        //}


        //public static void operator-=( int arg_0, int arg_4 )
        //operator-=(Set far &,Set far &)
        //Complex
        //{
        //cld
        //mov	bx, sp
        //mov	dx, ds
        //les	di, ss:[bx+arg_4]
        //lds	si, ss:[bx+arg_0]
        //mov	cx, 0x10
        //loop_top:
        //lodsw
        //not	ax
        //and	ax, es:[di]
        //stosw
        //loop	loop_top
        //mov	ds, dx
        //retf	4
        //}


        //public static void operator*=( int arg_0, int arg_4 )
        //operator*=(Set far &,Set far &)
        //Complex
        //{
        //cld
        //mov	bx, sp
        //mov	dx, ds
        //les	di, ss:[bx+arg_4]
        //lds	si, ss:[bx+arg_0]
        //mov	cx, 0x10
        //loc_15482:
        //lodsw
        //and	ax, es:[di]
        //stosw
        //loop	loc_15482
        //mov	ds, dx
        //retf	4
        //}


        //public static void operator==( int arg_0, int arg_4 )
        //operator==(Set far &,Set far &)
        //Complex
        //{
        //cld
        //mov	bx, sp
        //mov	dx, ds
        //les	di, ss:[bx+arg_4]
        //lds	si, ss:[bx+arg_0]
        //mov	cx, 0x10
        //repe cmpsw
        //mov	ds, dx
        //retf	8
        //}


        //public static void operator>=( int arg_0, int arg_4 )
        //operator>=(Set far &,Set far &)
        //Complex
        //{
        //cld
        //mov	bx, sp
        //mov	dx, ds
        //les	di, ss:[bx+arg_4]
        //lds	si, ss:[bx+arg_0]
        //mov	cx, 0x10
        //loop_top:
        //lodsw
        //or	ax, es:[di]
        //scasw
        //jnz	func_end
        //loop	loop_top
        //func_end:
        //mov	ds, dx
        //retf	8
        //}


	}
}
