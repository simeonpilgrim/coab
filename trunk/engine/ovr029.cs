using Classes;

namespace engine
{
    class ovr029
    {
        static byte[] unk_16D9A = new byte[]{ /* seg600:0A8A */
        0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02, 0x09, 0x0E, 0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02 , 0x09, 0x0E};

        internal static void sub_6F0BA()
        {
            if (gbl.byte_1D5B4 == 0x50)
            {
                gbl.byte_1D8AA = 0;
            }

            if (gbl.byte_1B2F0 == 0)
            {
                if (gbl.area_ptr.field_1CC != 0)
                {
                    gbl.byte_1D53D = ovr031.sub_717A5(gbl.mapPosY, gbl.mapPosX);

                    if (gbl.byte_1D53D > 0x7F)
                    {
                        gbl.byte_1D534 = unk_16D9A[gbl.area_ptr.field_1FC];
                    }
                    else
                    {
                        gbl.byte_1D534 = unk_16D9A[gbl.area_ptr.field_1FA];
                    }


                    gbl.byte_1D537 = 8;

                    if (gbl.area_ptr.field_1F6 != 0)
                    {
                        gbl.mapAreaDisplay = false;
                    }

                    ovr031.sub_71820(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                }
                else if (gbl.byte_1D8AA != 0)
                {
                    ovr030.sub_7087A();
                }

                gbl.byte_1D8AA = 0;
            }
        }
    }
}
