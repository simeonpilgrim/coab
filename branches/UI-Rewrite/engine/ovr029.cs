using Classes;

namespace engine
{
    class ovr029
    {
        static int[] sky_colours = new int[]{ /* seg600:0A8A unk_16D9A*/
        0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02, 0x09, 0x0E, 0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02 , 0x09, 0x0E};

        internal static void RedrawView() /* sub_6F0BA */
        {
            if (gbl.lastDaxBlockId == 0x50)
            {
                gbl.can_draw_bigpic = false;
            }

            if (gbl.party_killed == false)
            {
                if (gbl.area_ptr.inDungeon != 0)
                {
                    gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                    if (gbl.mapWallRoof > 0x7F)
                    {
                        // indoor  
                        gbl.sky_colour = sky_colours[gbl.area_ptr.indoor_sky_colour];
                    }
                    else
                    {
                        // outdoors
                        gbl.sky_colour = sky_colours[gbl.area_ptr.outdoor_sky_colour];
                    }

                    if (gbl.area_ptr.block_area_view != 0 &&
                        Cheats.always_show_areamap == false)
                    {
                        gbl.mapAreaDisplay = false;
                    }

                    ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                }
                else if (gbl.can_draw_bigpic == true)
                {
                    ovr030.draw_bigpic();
                }

                gbl.can_draw_bigpic = false;
            }
        }
    }
}
