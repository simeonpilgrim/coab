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
        type4
	}

    public enum SpellWhen
    {
        Camp = 0,
        Combat = 1,
        Both = 2
    }

    public enum SpellTargets
    {
        Combat = 0, // maybe
        Self = 1, // maybe
        PartyMember = 2,
        WholeParty = 4
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
        bless = 0x01,
        curse = 0x02,
        cure_light_wounds = 0x03,
        cause_light_wounds = 0x04,
        detect_magic_CL = 0x05,
        protect_from_evil_CL = 0x06,
        protect_from_good_CL = 0x07,
        resist_cold = 0x08,
        burning_hands = 0x09,
        charm_person = 0x0a,
        detect_magic_MU = 0x0b,
        enlarge = 0x0c,
        reduce = 0x0d,
        friends = 0x0e,
        magic_missile = 0x0f,
        protect_from_evil_MU = 0x10,
        protect_from_good_MU = 0x11,
        read_magic = 0x12,
        shield = 0x13,
        shocking_grasp = 0x14,
        sleep = 0x15,
        find_traps = 0x16,
        hold_person_CL = 0x17,
        resist_fire = 0x18,
        silence_15_radius = 0x19,
        slow_poison = 0x1a,
        snake_charm = 0x1b,
        spiritual_hammer = 0x1c,
        detect_invisibility = 0x1d,
        invisibility = 0x1e,
        knock = 0x1f,
        mirror_image = 0x20,
        ray_of_enfeeblement = 0x21,
        stinking_cloud = 0x22,
        strength = 0x23,
        animate_dead = 0x24,
        cure_blindness = 0x25,
        cause_blindness = 0x26,
        cure_disease = 0x27,
        cause_disease = 0x28,
        dispel_magic_CL = 0x29,
        prayer = 0x2a,
        remove_curse = 0x2b,
        bestow_curse_CL = 0x2c,
        blink = 0x2d,
        dispel_magic_MU = 0x2e,
        fireball = 0x2f,
        haste = 0x30,
        hold_person_MU = 0x31,
        invisibility_10_radius = 0x32,
        lightning_bolt = 0x33,
        protect_from_evil_10_rad = 0x34,
        protect_from_good_10_rad = 0x35,
        protect_from_normal_missiles = 0x36,
        slow = 0x37,
        restoration = 0x38,
        spell_39 = 0x39,
        cure_serious_wounds = 0x3a,
        spell_3b = 0x3b,
        spell_3c = 0x3c,
        spell_3d = 0x3d,
        spell_3e = 0x3e,
        spell_3f = 0x3f,
        spell_40 = 0x40,
        spell_41 = 0x41,
        cause_serious_wounds = 0x42,
        neutralize_poison = 0x43,
        poison = 0x44,
        protect_evil_10_rad = 0x45,
        sticks_to_snakes = 0x46,
        cure_critical_wounds = 0x47,
        cause_critical_wounds = 0x48,
        dispel_evil = 0x49,
        flame_strike = 0x4a,
        raise_dead = 0x4b,
        slay_living = 0x4c,
        detect_magic_DR = 0x4d,
        entangle = 0x4e,
        faerie_fire = 0x4f,
        invisibility_to_animals = 0x50,
        charm_monsters = 0x51,
        confusion = 0x52,
        dimension_door = 0x53,
        fear = 0x54,
        fire_shield = 0x55,
        fumble = 0x56,
        ice_storm = 0x57,
        minor_globe_of_invuln = 0x58,
        spell_59 = 0x59,
        spell_5a = 0x5a,
        cloud_kill = 0x5b,
        cone_of_cold = 0x5c,
        feeblemind = 0x5d,
        hold_monsters = 0x5e,
        spell_5f = 0x5f,
        spell_60 = 0x60,
        spell_61 = 0x61,
        spell_62 = 0x62,
        spell_63 = 0x63,
        bestow_curse_MU = 0x64,
        //spell_88 = 0x88,
    }

    public class SpellEntry /* Struct_19AEC */
    {
        public SpellEntry(int spell_idx, SpellClass _spellClass, sbyte _spellLevel,
            int _fixedRange, int _perLvlRange,
            int _fixedDuration, byte _perLvlDuration,
            byte f6, SpellTargets _targets,
            DamageOnSave _damageOnSave, SaveVerseType _saveVerse,
            Affects _affectId, SpellWhen _whenCast,
            int _castingDelay, int _priority, byte fe, byte ff)
        {
            spellClass = _spellClass;
            spellLevel = _spellLevel;
            fixedRange = _fixedRange;
            perLvlRange = _perLvlRange;
            fixedDuration = _fixedDuration;
            perLvlDuration = _perLvlDuration;
            field_6 = f6;
            targetType = _targets;
            damageOnSave = _damageOnSave;
            saveVerse = _saveVerse;
            affect_id = _affectId;
            whenCast = _whenCast;
            castingDelay = _castingDelay;
            priority = _priority;
            field_E = fe;
            field_F = ff;
            spellIdx = spell_idx;
        }

        /// <summary>
        /// 0 - Cleric, 1 - Druid, 2 - Magic-User
        /// </summary>
        public int spellIdx;

        public SpellClass spellClass; //seg600:37DC asc_19AEC    // field_0
        public int spellLevel; //seg600:37DD unk_19AED    // field_1
        public int fixedRange; //seg600:37DE
        public int perLvlRange; //seg600:37DF
        public int fixedDuration; //seg600:37E0
        public int perLvlDuration; //seg600:37E1
        public byte field_6; //seg600:37E2
        public SpellTargets targetType; //seg600:37E3
        public DamageOnSave damageOnSave; //seg600:37E4 unk_19AF4  // field_8
        public SaveVerseType saveVerse; //seg600:37E5 unk_19AF5
        public Affects affect_id; //seg600:37E6 unk_19AF6   // field_A
        public SpellWhen whenCast; //seg600:37E7 unk_19AF7
        public int castingDelay; //seg600:37E8 unk_19AF8
        public int priority; //seg600:37E9 unk_19AF9
        public byte field_E; //seg600:37EA unk_19AFA
        public byte field_F; //seg600:37EB unk_19AFB
    }
}
