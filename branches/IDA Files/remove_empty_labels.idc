#include <idc.idc>

static main()
{
	auto seg, loc, flags;
	auto count;

	count = 0;

	seg = FirstSeg();

	while(seg != BADADDR ) 
	{
		loc = SegStart(seg);
		while( loc < SegEnd(seg) )
		{
			flags = GetFlags(loc);

			// Has a dummy label and no references, and not start of function, remove name
			if( ((flags & ( FF_LABL | FF_REF)) == FF_LABL) & ((flags & FF_FUNC) == 0))
			{
				MakeNameEx(loc, "", 0);
				count ++;
			}

			loc = loc + ItemSize(loc);
		}

		seg = NextSeg(seg);
	}

	Message("Removed %d empty labels\n", count);
}
