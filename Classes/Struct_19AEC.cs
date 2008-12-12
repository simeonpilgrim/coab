using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_19AEC.
	/// </summary>
    public class SpellEntry /* Struct_19AEC */
	{
        public SpellEntry(sbyte f0, sbyte f1, sbyte f2, byte f3, byte f4, byte f5,
            byte f6, byte f7, DamageOnSave f8, SaveVerseType f9, Affects fa, byte fb, byte fc, byte fd, byte fe, byte ff)
        {
            spellClass = f0;
            spellLevel = f1;
            field_2 = f2;
            field_3 = f3;
            field_4 = f4;
            field_5 = f5;
            field_6 = f6;
            field_7 = f7;
            can_save_flag = f8;
            saveVerse = f9;
            affect_id = fa;
            field_B = fb;
            field_C = fc;
            field_D = fd;
            field_E = fe;
            field_F = ff;
        }

        /// <summary>
        /// 0 - Cleric, 1 - Druid, 2 - Magic-User
        /// </summary>
        public sbyte spellClass; //seg600:37DC asc_19AEC    // field_0
        public sbyte spellLevel; //seg600:37DD unk_19AED    // field_1
        public sbyte field_2; //seg600:37DE              
        public byte field_3; //seg600:37DF            
        public byte field_4; //seg600:37E0             
        public byte field_5; //seg600:37E1            
        public byte field_6; //seg600:37E2              
        public byte field_7; //seg600:37E3             
        public DamageOnSave can_save_flag; //seg600:37E4 unk_19AF4  // field_8 
        public SaveVerseType saveVerse; //seg600:37E5 unk_19AF5    
        public Affects affect_id; //seg600:37E6 unk_19AF6   // field_A
        public byte field_B; //seg600:37E7 unk_19AF7    
        public byte field_C; //seg600:37E8 unk_19AF8    
        public byte field_D; //seg600:37E9 unk_19AF9    
        public byte field_E; //seg600:37EA unk_19AFA    
        public byte field_F; //seg600:37EB unk_19AFB   
    }
}
