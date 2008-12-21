using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public enum SaveVerseType
    {
        Poison = 0,
        type1,
        type2,
        type3,
        type4,
        type5,
        type6,
        type7
    }

    public enum SpellWhen
    {
        Camp = 0,
        Combat = 1,
        Both = 2
    }

    public enum SpellSource
    {
        Cast = 1,
        Memorize = 2,
        Scribe = 3,
        Learn = 4
    }

    public enum SpellClass
    {
        Cleric = 0,
        Druid = 1,
        MagicUser = 2,
        Monster = 3, //??
        Unknown10 = 10
    }

    public enum Spells
    {
        spell_01 = 0x01,
        spell_02 = 0x02,
        spell_03 = 0x03,
        spell_04 = 0x04,
        spell_05 = 0x05,
        spell_06 = 0x06,
        spell_07 = 0x07,
        spell_08 = 0x08,
        spell_09 = 0x09,
        spell_0a = 0x0a,
        spell_0b = 0x0b,
        spell_0c = 0x0c,
        spell_0d = 0x0d,
        spell_0e = 0x0e,
        spell_0f = 0x0f,
        spell_10 = 0x10,
        spell_11 = 0x11,
        spell_12 = 0x12,
        spell_13 = 0x13,
        spell_14 = 0x14,
        spell_15 = 0x15,
        spell_16 = 0x16,
        spell_17 = 0x17,
        spell_18 = 0x18,
        spell_19 = 0x19,
        spell_1a = 0x1a,
        spell_1b = 0x1b,
        spell_1c = 0x1c,
        spell_1d = 0x1d,
        spell_1e = 0x1e,
        knock = 0x1f,
        spell_20 = 0x20,
        spell_21 = 0x21,
        spell_22 = 0x22,
        spell_23 = 0x23,
        spell_24 = 0x24,
        spell_25 = 0x25,
        spell_26 = 0x26,
        spell_27 = 0x27,
        spell_28 = 0x28,
        spell_29 = 0x29,
        spell_2a = 0x2a,
        spell_2b = 0x2b,
        spell_2c = 0x2c,
        spell_2d = 0x2d,
        spell_2e = 0x2e,
        spell_2f = 0x2f,
        spell_30 = 0x30,
        spell_31 = 0x31,
        spell_32 = 0x32,
        spell_33 = 0x33,
        spell_34 = 0x34,
        spell_35 = 0x35,
        spell_36 = 0x36,
        spell_37 = 0x37,
        spell_38 = 0x38,
        spell_39 = 0x39,
        spell_3a = 0x3a,
        spell_3b = 0x3b,
        spell_3c = 0x3c,
        spell_3d = 0x3d,
        spell_3e = 0x3e,
        spell_3f = 0x3f,
        spell_40 = 0x40,
        spell_41 = 0x41,
        spell_42 = 0x42,
        spell_43 = 0x43,
        spell_44 = 0x44,
        spell_45 = 0x45,
        spell_46 = 0x46,
        spell_47 = 0x47,
        spell_48 = 0x48,
        spell_49 = 0x49,
        spell_4a = 0x4a,
        spell_4b = 0x4b,
        spell_4c = 0x4c,
        spell_4d = 0x4d,
        spell_4e = 0x4e,
        spell_4f = 0x4f,
        spell_50 = 0x50,
        spell_51 = 0x51,
        spell_52 = 0x52,
        spell_53 = 0x53,
        spell_54 = 0x54,
        spell_55 = 0x55,
        spell_56 = 0x56,
        spell_57 = 0x57,
        spell_58 = 0x58,
        spell_59 = 0x59,
        spell_5a = 0x5a,
        spell_5b = 0x5b,
        spell_5c = 0x5c,
        spell_5d = 0x5d,
        spell_5e = 0x5e,
        spell_5f = 0x5f,
        spell_60 = 0x60,
        spell_61 = 0x61,
        spell_62 = 0x62,
        spell_63 = 0x63,
        spell_64 = 0x64,
    }

    public class SpellEntry /* Struct_19AEC */
    {
        public SpellEntry(SpellClass _spellClass, sbyte _spellLevel, 
            int _fixedRange, int _perLvlRange, 
            int _fixedDuration, byte _perLvlDuration,
            byte f6, byte f7, 
            DamageOnSave _damageOnSave, SaveVerseType _saveVerse, 
            Affects _affectId, SpellWhen _whenCast, 
            int _castingDelay, byte fd, byte fe, byte ff)
        {
            spellClass = _spellClass;
            spellLevel = _spellLevel;
            fixedRange = _fixedRange;
            perLvlRange = _perLvlRange;
            fixedDuration = _fixedDuration;
            perLvlDuration = _perLvlDuration;
            field_6 = f6;
            field_7 = f7;
            damageOnSave = _damageOnSave;
            saveVerse = _saveVerse;
            affect_id = _affectId;
            whenCast = _whenCast;
            castingDelay = _castingDelay;
            field_D = fd;
            field_E = fe;
            field_F = ff;
        }

        /// <summary>
        /// 0 - Cleric, 1 - Druid, 2 - Magic-User
        /// </summary>
        public SpellClass spellClass; //seg600:37DC asc_19AEC    // field_0
        public int spellLevel; //seg600:37DD unk_19AED    // field_1
        public int fixedRange; //seg600:37DE              
        public int perLvlRange; //seg600:37DF            
        public int fixedDuration; //seg600:37E0             
        public int perLvlDuration; //seg600:37E1            
        public byte field_6; //seg600:37E2              
        public byte field_7; //seg600:37E3             
        public DamageOnSave damageOnSave; //seg600:37E4 unk_19AF4  // field_8 
        public SaveVerseType saveVerse; //seg600:37E5 unk_19AF5    
        public Affects affect_id; //seg600:37E6 unk_19AF6   // field_A
        public SpellWhen whenCast; //seg600:37E7 unk_19AF7    
        public int castingDelay; //seg600:37E8 unk_19AF8    
        public byte field_D; //seg600:37E9 unk_19AF9    
        public byte field_E; //seg600:37EA unk_19AFA    
        public byte field_F; //seg600:37EB unk_19AFB   
    }
}
