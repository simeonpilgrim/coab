namespace GoldBox.Classes
{
    public class ActiveItems
    {
        const int ItemSlots = 13;
        Item[] itemArray = new Item[ItemSlots]; // 0x151[]

        public byte PrimaryWeaponHandCount()
        {
            if (primaryWeapon == null) return 0;

            return primaryWeapon.HandsCount();
        }

        public byte SecondaryWeaponHandCount()
        {
            if (secondaryWeapon == null) return 0;

            return secondaryWeapon.HandsCount();
        }

        public Item primaryWeapon // field_151
        {// 0x151
            get { return itemArray[0]; }
            set { itemArray[0] = value; }
        }
        public Item secondaryWeapon // field_155
        { // 0x155 
            get { return itemArray[1]; }
            set { itemArray[1] = value; }
        }

        public Item armor // field_159
        { // 0x159 
            get { return itemArray[2]; }
            set { itemArray[2] = value; }
        }

        public Item field_15D
        { // 0x15D
            get { return itemArray[3]; }
            set { itemArray[3] = value; }
        }
        public Item field_161
        { // 0x161
            get { return itemArray[4]; }
            set { itemArray[4] = value; }
        }
        public Item field_165
        { // 0x165
            get { return itemArray[5]; }
            set { itemArray[5] = value; }
        }
        public Item field_169
        { // 0x169 
            get { return itemArray[6]; }
            set { itemArray[6] = value; }
        }
        public Item field_16D
        { // 0x16D
            get { return itemArray[7]; }
            set { itemArray[7] = value; }
        }
        public Item field_171
        { // 0x171
            get { return itemArray[8]; }
            set { itemArray[8] = value; }
        }
        public Item Item_ptr_01
        { // 0x175
            get { return itemArray[9]; }
            set { itemArray[9] = value; }
        }
        public Item Item_ptr_02
        {// 0x179
            get { return itemArray[10]; }
            set { itemArray[10] = value; }
        }
        public Item arrows
        {// 0x17d
            get { return itemArray[11]; }
            set { itemArray[11] = value; }
        }
        public Item quarrels
        { // 0x181
            get { return itemArray[12]; }
            set { itemArray[12] = value; }
        }

        public Item this[ItemSlot slot]
        {
            get
            {
                return itemArray[(int)slot];
            }
            set
            {
                itemArray[(int)slot] = value;
            }
        }

        public void Reset()
        {
            for (int slot = 0; slot < ItemSlots; slot++)
            {
                itemArray[slot] = null;
            }
        }

        public void UndreadyAll(int classFlags)
        {
            for (int item_slot = 0; item_slot < ItemSlots; item_slot++)
            {
                if (itemArray[item_slot] != null &&
                    (gbl.ItemDataTable[itemArray[item_slot].type].classFlags & classFlags) == 0 &&
                    itemArray[item_slot].cursed == false)
                {
                    itemArray[item_slot].readied = false;
                }
            }
        }
    }
}
