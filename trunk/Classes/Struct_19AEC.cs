using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_19AEC.
	/// </summary>
	public class Struct_19AEC
	{
		public Struct_19AEC()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public Struct_19AEC(sbyte f0, sbyte f1, sbyte f2, byte f3, byte f4, byte f5,
            byte f6, byte f7, byte f8, byte f9, byte fa, byte fb, byte fc, byte fd, byte fe, byte ff)
        {
            field_0 = f0;
            field_1 = f1;
            field_2 = f2;
            field_3 = f3;
            field_4 = f4;
            field_5 = f5;
            field_6 = f6;
            field_7 = f7;
            field_8 = f8;
            field_9 = f9;
            field_A = (Affects)fa;
            field_B = fb;
            field_C = fc;
            field_D = fd;
            field_E = fe;
            field_F = ff;
        }

        public sbyte field_0; //seg600:37DC asc_19AEC    db 0
        public sbyte field_1; //seg600:37DD unk_19AED    db 0
        public sbyte field_2; //seg600:37DE              db 0
        public byte field_3; //seg600:37DF              db 0
        public byte field_4; //seg600:37E0              db	  0
        public byte field_5; //seg600:37E1              db	  0
        public byte field_6; //seg600:37E2              db	  0
        public byte field_7; //seg600:37E3              db	  0
        public byte field_8; //seg600:37E4 unk_19AF4    db	  0
        public byte field_9; //seg600:37E5 unk_19AF5    db	  0
        public Affects field_A; //seg600:37E6 unk_19AF6    db	  0
        public byte field_B; //seg600:37E7 unk_19AF7    db	  0
        public byte field_C; //seg600:37E8 unk_19AF8    db	  0
        public byte field_D; //seg600:37E9 unk_19AF9    db	  0
        public byte field_E; //seg600:37EA unk_19AFA    db	  0
        public byte field_F; //seg600:37EB unk_19AFB    db	  0
    }
}
