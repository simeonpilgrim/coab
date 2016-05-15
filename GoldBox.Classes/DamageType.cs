namespace GoldBox.Classes
{
    [System.Flags]
    public enum DamageType
    {
        Fire = 0x01,
        Cold = 0x02,
        Electricity = 0x04,
        Magic = 0x08,
        Acid = 0x10,
        DragonBreath = 0x20,
        Unknown40 = 0x40
    }
}
