using System;

namespace Classes
{
    public enum Affects
    {
        none = 0,
        bless = 0x1,
        cursed = 0x2,
        sticks_to_snakes = 0x3,
        dispel_evil = 0x4,
        detect_magic = 0x5,
        affect_06 = 0x6,
        faerie_fire = 0x7,
        protection_from_evil = 0x8,
        protection_from_good = 0x9,
        resist_cold = 0xa,
        charm_person = 0xb,
        enlarge = 0xc,
        reduce = 0xd,
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
        dwarf_vs_orc = 0x1a,
        fumbling = 0x1b,
        mirror_image = 0x1c,
        ray_of_enfeeblement = 0x1d,
        stinking_cloud = 0x1e,
        helpless = 0x1f,
        animate_dead = 0x20,
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
        dwarf_and_gnome_vs_giants = 0x2f,
        affect_30 = 0x30,
        prayer = 0x31,
        hot_fire_shield = 0x32,
        snake_charm = 0x33,
        paralyze = 0x34,
        sleep = 0x35,
        cold_fire_shield = 0x36,
        poisoned = 0x37,
        item_invisibility = 0x38,
        affect_39 = 0x39,
        clear_movement = 0x3a,
        regenerate = 0x3b,
        resist_normal_weapons = 0x3c,
        fire_resist = 0x3d,
        highConRegen = 0x3e,
        minor_globe_of_invulnerability = 0x3f,
        poison_plus_0 = 0x40,
        poison_plus_4 = 0x41,
        poison_plus_2 = 0x42,
        thri_kreen_paralyze = 0x43,
        feeblemind = 0x44,
        invisible_to_animals = 0x45,
        poison_neg_2 = 0x46,
        invisible = 0x47,
        camouflage = 0x48,
        prot_drag_breath = 0x49,
        affect_4a = 0x4a,
        weap_dragon_slayer = 0x4b,
        weap_frost_brand = 0x4c,
        berserk = 0x4d,
        affect_4e = 0x4e,
        fireAttack_2d10 = 0x4f,
        ankheg_acid_attack = 0x50,
        half_damge = 0x51,
        resist_fire_and_cold = 0x52,
        paralizing_gaze = 0x53,
        shambling_absorb_lightning = 0x54,
        affect_55 = 0x55,
        spit_acid = 0x56,
        affect_57 = 0x57,
        breath_elec = 0x58,
        displace = 0x59,
        breath_acid = 0x5a,
        affect_5b = 0x5b,
        affect_5c = 0x5c,
        affect_5d = 0x5d,
        affect_5e = 0x5e,
        affect_5F = 0x5f,
        owlbear_hug_check = 0x60,
        con_saving_bonus = 0x61,
        regen_3_hp = 0x62,
        affect_63 = 0x63,
        troll_fire_or_acid = 0x64,
        troll_regen = 0x65,
        affect_66 = 0x66,
        affect_67 = 0x67,
        affect_68 = 0x68,
        affect_69 = 0x69,
        affect_6a = 0x6a,
        elf_resist_sleep = 0x6b,
        protect_charm_sleep = 0x6c,
        affect_6d = 0x6d,
        affect_6e = 0x6e,
        affect_6f = 0x6f,
        immune_to_fire = 0x70,
        affect_71 = 0x71,
        affect_72 = 0x72,
        affect_73 = 0x73,
        affect_74 = 0x74,
        affect_75 = 0x75,
        affect_76 = 0x76,
        affect_77 = 0x77,
        affect_78 = 0x78,
        affect_79 = 0x79,
        dracolich_paralysis = 0x7a,
        affect_7b = 0x7b,
        halfelf_resistance = 0x7c,
        affect_7d = 0x7d,
        affect_7e = 0x7e,
        affect_7f = 0x7f,
        affect_80 = 0x80,
        protect_magic = 0x81,
        affect_82 = 0x82,
        cast_breath_fire = 0x83,
        cast_throw_lightening = 0x84,
        affect_85 = 0x85,
        ranger_vs_giant = 0x86,
        protect_elec = 0x87,
        entangle = 0x88,
        affect_89 = 0x89,
        affect_8a = 0x8a,
        affect_8b = 0x8b,
        paladinDailyHealCast = 0x8c,
        paladinDailyCureRefresh = 0x8d,
        fear = 0x8e,
        affect_8f = 0x8f,
        owlbear_hug_round_attack = 0x90,
        sp_dispel_evil = 0x91,
        strenght_spell = 0x92,
        do_items_affect = 0x93
    }

    /// <summary>
	/// Summary description for Affect.
	/// </summary>
	public class Affect
	{
        public const int StructSize = 9;

        public Affect(Affects _type, ushort _minutes, byte _affect_data, bool _call_spell_jump_list)
        {
            type = _type;
            minutes = _minutes;
            affect_data = _affect_data;
            callAffectTable = _call_spell_jump_list;
        }

        public Affect(byte[] data, int offset)
        {
            type = (Affects)data[offset + 0x0];
            minutes = Sys.ArrayToUshort(data, offset + 0x1);
            affect_data = data[offset + 0x3];
            callAffectTable = (data[offset + 0x4] != 0);
        }

		public Affect ShallowClone()
		{
			Affect a = (Affect) this.MemberwiseClone();
			return a;
		}

        [DataOffset(0x00, DataType.IByte)]
		public Affects type;
        [DataOffset(0x01, DataType.Word)]
		public ushort minutes;
        [DataOffset(0x03, DataType.Byte)]
        public byte affect_data;
        [DataOffset(0x04, DataType.Bool)]
        public bool callAffectTable;

        public byte[] ToByteArray()
        {
            byte[] data = new byte[StructSize];

            DataIO.WriteObject(this, data);

            return data;
        }
    }
}
