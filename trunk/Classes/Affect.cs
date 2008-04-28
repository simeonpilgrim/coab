using System;

namespace Classes
{
    public enum Affects
    {
        bless = 0x1,
        cursed = 0x2,
        affect_03 = 0x3,
        dispel_evil = 0x4,
        detect_magic = 0x5,
        affect_06 = 0x6,
        faerie_fire = 0x7,
        protection_from_evil = 0x8,
        protection_from_good = 0x9,
        resist_cold = 0xa,
        charm_person = 0xb,
        enlarge = 0xc,
        affect_0d = 0xd,
        friends = 0xe,
        affect_0f = 0xf,
        read_magic = 0x10,
        shield = 0x11,
        affect_12 = 0x12,
        find_traps = 0x13,
        resist_fire = 0x14,
        silence_15_radius = 0x15,
        slow_poison = 0x16,
        spiritual_hammer = 0x17,
        detect_invisibility = 0x18,
        invisibility = 0x19,
        affect_1a = 0x1a,
        fumbling = 0x1b,
        mirror_image = 0x1c,
        ray_of_enfeeblement = 0x1d,
        affect_1e = 0x1e,
        helpless = 0x1f,
        funky__32 = 0x20,
        blinded = 0x21,
        cause_disease_1 = 0x22,
        confuse = 0x23,
        bestow_curse = 0x24,
        blink = 0x25,
        strength = 0x26,
        haste = 0x27,
        affect_28 = 0x28,
        prot_from_normal_missiles = 0x29,
        slow = 0x2a,
        affect_2b = 0x2b,
        cause_disease_2 = 0x2c,
        prot_from_evil_10_radius = 0x2d,
        prot_from_good_10_radius = 0x2e,
        affect_2f = 0x2f,
        affect_30 = 0x30,
        prayer = 0x31,
        hot_fire_shield = 0x32,
        snake_charm = 0x33,
        paralyze = 0x34,
        sleep = 0x35,
        cold_fire_shield = 0x36,
        poisoned = 0x37,
        affect_38 = 0x38,
        affect_39 = 0x39,
        affect_3a = 0x3a,
        regenerate = 0x3b,
        affect_3c = 0x3c,
        fire_resist = 0x3d,
        affect_3e = 0x3e,
        minor_globe_of_invulnerability = 0x3f,
        affect_40 = 0x40,
        affect_41 = 0x41,
        affect_42 = 0x42,
        affect_43 = 0x43,
        feeble = 0x44,
        invisible_to_animals = 0x45,
        affect_46 = 0x46,
        invisible = 0x47,
        camouflage = 0x48,
        prot_drag_breath = 0x49,
        affect_4a = 0x4a,
        affect_4b = 0x4b,
        affect_4c = 0x4c,
        berserk = 0x4d,
        affect_4e = 0x4e,
        affect_4f = 0x4f,
        affect_50 = 0x50,
        affect_51 = 0x51,
        affect_52 = 0x52,
        affect_53 = 0x53,
        affect_54 = 0x54,
        affect_55 = 0x55,
        affect_56 = 0x56,
        affect_57 = 0x57,
        affect_58 = 0x58,
        displace = 0x59,
        affect_5a = 0x5a,
        affect_5b = 0x5b,
        affect_5c = 0x5c,
        affect_5d = 0x5d,
        affect_5e = 0x5e,
        affect_5F = 0x5f,
        affect_60 = 0x60,
        affect_61 = 0x61,
        affect_62 = 0x62,
        affect_63 = 0x63,
        affect_64 = 0x64,
        affect_65 = 0x65,
        affect_66 = 0x66,
        affect_67 = 0x67,
        affect_68 = 0x68,
        affect_69 = 0x69,
        affect_6a = 0x6a,
        affect_6b = 0x6b,
        affect_6c = 0x6c,
        affect_6d = 0x6d,
        affect_6e = 0x6e,
        affect_6f = 0x6f,
        affect_70 = 0x70,
        affect_71 = 0x71,
        affect_72 = 0x72,
        affect_73 = 0x73,
        affect_74 = 0x74,
        affect_75 = 0x75,
        affect_76 = 0x76,
        affect_77 = 0x77,
        affect_78 = 0x78,
        affect_79 = 0x79,
        affect_7a = 0x7a,
        affect_7b = 0x7b,
        affect_7c = 0x7c,
        affect_7d = 0x7d,
        affect_7e = 0x7e,
        affect_7f = 0x7f,
        affect_80 = 0x80,
        affect_81 = 0x81,
        affect_82 = 0x82,
        affect_83 = 0x83,
        affect_84 = 0x84,
        affect_85 = 0x85,
        affect_86 = 0x86,
        affect_87 = 0x87,
        affect_88 = 0x88,
        affect_89 = 0x89,
        affect_8a = 0x8a,
        affect_8b = 0x8b,
        affect_8c = 0x8c,
        affect_8D = 0x8d,
        affect_8e = 0x8e,
        affect_8f = 0x8f,
        affect_90 = 0x90,
        affect_91 = 0x91,
        affect_92 = 0x92
    }

    /// <summary>
	/// Summary description for Affect.
	/// </summary>
	public class Affect
	{
		public Affect()
		{
			next = null;
		}

        public const int StructSize = 9;

        public Affect(byte[] data, int offset)
        {
            type = (Affects)data[offset + 0x0];
            field_1 = Sys.ArrayToUshort(data, offset + 0x1);
            field_3 = data[offset + 0x3];
            call_spell_jump_list = (data[offset + 0x4] != 0);
            next = null;
        }

		public Affect ShallowClone()
		{
			Affect a = (Affect) this.MemberwiseClone();
			return a;
		}

        [DataOffset(0x00, DataType.IByte)]
		public Affects type;
        [DataOffset(0x01, DataType.Word)]
		public ushort field_1;
        [DataOffset(0x03, DataType.Byte)]
        public byte field_3;
        [DataOffset(0x04, DataType.Bool)]
        public bool call_spell_jump_list;
		public Affect next; // pointer to next affect.

        public byte[] ToByteArray()
        {
            byte[] data = new byte[StructSize];

            DataIO.WriteObject(this, data);

            return data;
        }
    }
}
