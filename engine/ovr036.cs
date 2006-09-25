using Classes;

namespace engine
{
    class ovr036
    {
        internal static string print_command( out string arg_2 )
        {
			/*
			^:b+throw new System.NotSupportedException();//cmp:b+al,:b{0x:h}\n:b+throw new System.NotSupportedException();//jnz:b+:i\n:b+throw new System.NotSupportedException();//mov:b+di,:boffset:b{:i}\n:b+throw new System.NotSupportedException();//push:b+cs\n:b+throw new System.NotSupportedException();//push:b+di\n:b+throw new System.NotSupportedException();//les:b+di,:b+\[bp\+arg_2\]\n:b+throw new System.NotSupportedException();//push:b+es\n:b+throw new System.NotSupportedException();//push:b+di\n:b+throw new System.NotSupportedException();//mov:b+ax,:b0x0FF\n:b+throw new System.NotSupportedException();//push:b+ax\n:b+throw new System.NotSupportedException();//call:b+operator=\(String:b&,String:b&,Byte\)\n:b+throw new System.NotSupportedException();//jmp.*$
			case \1: arg_2 = \2; break;
			*/
            switch( gbl.command )

            {
            case 0: arg_2 = "EXIT"; break;
            case 1: arg_2 = "GOTO"; break;
            case 2: arg_2 = "GOSUB"; break;
            case 3: arg_2 = "COMPARE"; break;
            case 4: arg_2 = "ADD"; break;
            case 5: arg_2 = "SUBTRAT"; break;
            case 6: arg_2 = "DIVIDE"; break;
            case 7: arg_2 = "MULTIPLY"; break;
            case 8: arg_2 = "RANDOM"; break;
            case 9: arg_2 = "SAVE"; break;
            case 0x0A: arg_2 = "LOAD CHARACTER"; break;
            case 0x0B: arg_2 = "LOAD MONSTER"; break;
            case 0x0C: arg_2 = "SETUP MONSTER"; break;
            case 0x0D: arg_2 = "APPROACH"; break;
            case 0x0E: arg_2 = "PICTURE"; break;
            case 0x0F: arg_2 = "INPUT NUMBER"; break;
            case 0x10: arg_2 = "INPUT STRING"; break;
            case 0x11: arg_2 = "PRINT"; break;
            case 0x12: arg_2 = "PRINTCLEAR"; break;
            case 0x13: arg_2 = "RETURN"; break;
            case 0x14: arg_2 = "COMPARE AND"; break;
            case 0x15: arg_2 = "VERTICAL MENU"; break;
            case 0x16: arg_2 = "IF = "; break;
            case 0x17: arg_2 = "IF <>"; break;
            case 0x18: arg_2 = "IF <"; break;
            case 0x19: arg_2 = "IF >"; break;
            case 0x1A: arg_2 = "IF <="; break;
            case 0x1B: arg_2 = "IF >="; break;
            case 0x1C: arg_2 = "CLEARMONSTERS"; break;
            case 0x1D: arg_2 = "PARTYSTRENGTH"; break;
            case 0x1E: arg_2 = "CHECKPARTY"; break;
            case 0x20: arg_2 = "NEWECL"; break;
            case 0x21: arg_2 = "LOAD FILES"; break;
            case 0x37: arg_2 = "LOAD PIECES"; break;
            case 0x22: arg_2 = "PARTY SURPRISE"; break;
            case 0x23: arg_2 = "SURPRISE"; break;
            case 0x24: arg_2 = "COMBAT"; break;
            case 0x25: arg_2 = "ON GOTO"; break;
            case 0x26: arg_2 = "ON GOSUB"; break;
            case 0x27: arg_2 = "TREASURE"; break;
            case 0x28: arg_2 = "ROB"; break;
            case 0x29: arg_2 = "ENCOUNTER MENU"; break;
            case 0x2A: arg_2 = "GETTABLE"; break;
            case 0x2B: arg_2 = "HORIZONTAL MENU"; break;
            case 0x2C: arg_2 = "PARLAY"; break;
            case 0x2D: arg_2 = "CALL"; break;
            case 0x2E: arg_2 = "DAMAGE"; break;
            case 0x2F: arg_2 = "AND"; break;
            case 0x30: arg_2 = "OR"; break;
            case 0x31: arg_2 = "SPRITE OFF"; break;
            case 0x32: arg_2 = "FIND ITEM"; break;
            case 0x33: arg_2 = "PRINT RETURN"; break;
            case 0x34: arg_2 = "ECL CLOCK"; break;
            case 0x35: arg_2 = "SAVE TABLE"; break;
            case 0x36: arg_2 = "ADD NPC"; break;
            case 0x38: arg_2 = "PROGRAM"; break;
            case 0x39: arg_2 = "WHO"; break;
            case 0x3A: arg_2 = "DELAY"; break;
            case 0x3B: arg_2 = "SPELL"; break;
            case 0x3C: arg_2 = "PROTECTION"; break;
            case 0x3D: arg_2 = "CLEAR BOX"; break;
            case 0x3E: arg_2 = "DUMP"; break;
            case 0x3F: arg_2 = "FIND SPECIAL"; break;
            case 0x40: arg_2 = "DESTROY ITEMS"; break;
            default:
                arg_2 = "Unknown Command"; 
                break;
            }

            arg_2 = string.Format("{1} 0x{0:X}", gbl.command, arg_2);
			return arg_2;
        }
    }
}
