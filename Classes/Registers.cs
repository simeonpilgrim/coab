using System;
using System.Runtime.InteropServices;

namespace Classes
{
	/// <summary>
	/// Summary description for Registers.
	/// </summary>
	[ StructLayout( LayoutKind.Explicit )]
	public struct reg
	{
		[ FieldOffset( 0 )]
		public byte l;		// 0
		[ FieldOffset( 1 )]
		public byte h;		// 1
		[ FieldOffset( 0 )]
		public ushort x;   // 0
	}

	public class Registers
	{
		public reg a;		// 0
		public reg b;		// 2
		public reg c;		// 4
		public reg d;		// 6

		public ushort si;		// 8
		public ushort di;		// A
		public ushort flags;	// C
		
		public Registers()
		{
			a.x = 0;
			b.x = 0;
			c.x = 0;
			d.x = 0;
			si = 0;
			di = 0;
			flags = 0;
		}
	}
}
