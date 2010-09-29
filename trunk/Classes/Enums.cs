using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public enum Status
    {
        okey = 0x0,
        animated = 0x1,
        tempgone = 0x2,
        running = 0x3,
        unconscious = 0x4,
        dying = 0x5,
        dead = 0x6,
        stoned = 0x7,
        gone = 0x8
    }

    public enum MonsterType
    {
        type_1 = 1,
        giant = 2,
        dragon = 3,
        animated_dead = 4,
        fire = 8,
        type_9 = 9,
        troll = 10,
        type_12 = 12,
        snake = 14,
        plant = 18,
        animal = 19,
    }

    public enum Stat
    {
        STR, // 0
        INT, // 1
        WIS, // 2
        DEX, // 3
        CON, // 4
        CHA  // 5
    }

    public enum Race
    {
        monster = 0,
        dwarf = 1,
        elf = 2,
        gnome = 3,
        half_elf = 4,
        halfling = 5,
        half_orc = 6,
        human = 7
    }

    public enum SkillType
    {
        Cleric = 0,
        Druid = 1,
        Fighter = 2,
        Paladin = 3,
        Ranger = 4,
        MagicUser = 5,
        Thief = 6,
        Monk = 7
    }

    public enum ClassId
    {
        cleric = 0,
        druid = 1,
        fighter = 2,
        paladin = 3,
        ranger = 4,
        magic_user = 5,
        thief = 6,
        monk = 7,
        mc_c_f = 8,
        mc_c_f_m = 9,
        mc_c_r = 10,
        mc_c_mu = 11,
        mc_c_t = 12,
        mc_f_mu = 13,
        mc_f_t = 14,
        mc_f_mu_t = 15,
        mc_mu_t = 16,
        unknown = 17
    }

    public enum CombatTeam
    {
        Ours = 0,
        Enemy = 1
    }
}
