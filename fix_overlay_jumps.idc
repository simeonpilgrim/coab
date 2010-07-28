#include <idc.idc>

static main()
{
	auto seg, loc;
	auto off, base;
	auto xref;

	seg = FirstSeg();

	while(seg != BADADDR ) 
	{
		loc = SegStart(seg);
		
		if( Byte(loc) == 0xCD && Byte(loc+1) == 0x3F)
		{
			Message("Fixing segment %s jumps\n", SegName(seg));
			
			loc = loc + 0x20;
			
			while(loc < SegEnd(seg))
			{
				if( Byte(loc) == 0xEA )
				{
					off = Word(loc+1);
					base = Word(loc+3);
						
					//Message("loc %x %x %x %x\n", xref, Byte(loc), Word(loc+1), Word(loc+3));
					
					xref = RfirstB(loc);
					while( xref != BADADDR )
					{
						Message("Loc %x ref from %x\n", loc, xref);
						//Message("xref %x %x %x %x\n", xref, Byte(xref), Word(xref+1), Word(xref+3));
						
						PatchWord(xref+1, off);
						PatchWord(xref+3, base);	

						DelCodeXref(xref, loc, 0 );				
						
						xref = RnextB(loc, xref);
					}
				}
				loc = loc + 5;
				
				//seg = BADADDR;
			}
			
		}

		seg = NextSeg(seg);
	}
}
