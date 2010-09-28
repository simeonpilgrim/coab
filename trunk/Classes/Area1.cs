using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Area1.
	/// </summary>
	public class Area1
	{
		const int Area1Size = 0x800;

		public Area1()
		{
			constructorInit();
		}

		public Area1(byte[] data, int offset)
		{
			constructorInit();

			DataIO.ReadObject(this, data, offset);

			System.Array.Copy(data, offset, origData, 0, Area1Size);
		}

		private void constructorInit()
		{
			field_200 = new short[33];
			origData = new byte[Area1Size];
		}

		public void Clear()
		{
			System.Array.Clear(origData, 0, Area1Size);

			DataIO.ReadObject(this, origData, 0);
		}

		protected byte[] origData;

		[DataOffset(0x186, DataType.Byte)]
		public byte field_186;
		[DataOffset(0x188, DataType.Byte)]
		public byte field_188;
		[DataOffset(0x18A, DataType.Byte)]
		public byte current_3DMap_block_id; // field_18A

		[DataOffset(0x18E, DataType.Word)]
		public ushort field_18C;
		[DataOffset(0x18E, DataType.Word)]
		public ushort time_minutes_ones; // field_18E
		[DataOffset(0x190, DataType.Word)]
		public ushort time_minutes_tens; // field_190
		[DataOffset(0x192, DataType.Word)]
		public ushort time_hour; // field_192
		[DataOffset(0x194, DataType.Word)]
		public ushort time_day; // field_194
		[DataOffset(0x196, DataType.Word)]
		public ushort time_year; // field_196
		[DataOffset(0x198, DataType.Word)]
		public ushort field_198;

		[DataOffset(0x1CA, DataType.SWord)]
		public short field_1CA;
		[DataOffset(0x1CC, DataType.SWord)]
		public short inDungeon;
		[DataOffset(0x1CE, DataType.SWord)]
		public short field_1CE;
		[DataOffset(0x1D0, DataType.SWord)]
		public short field_1D0;

		[DataOffset(0x1E0, DataType.SWord)]
		public short lastXPos;
		[DataOffset(0x1E2, DataType.SWord)]
		public short lastYPos;
		[DataOffset(0x1E4, DataType.Word)]
		public ushort LastEclBlockId; // field_1E4
		[DataOffset(0x1F6, DataType.SWord)]
		public short block_area_view; // field_1F6
		[DataOffset(0x1F8, DataType.Byte)]
		public byte game_speed;
		[DataOffset(0x1FA, DataType.Word)]
		public ushort outdoor_sky_colour;
		[DataOffset(0x1FC, DataType.Word)]
		public ushort indoor_sky_colour;
		[DataOffset(0x1FE, DataType.Byte)]
		public byte pics_on; //field_1FE;

		[DataOffset(0x1FF, DataType.Bool)]
		public bool can_cast_spells;

		[DataOffset(0x200, DataType.ShortArray, 33)]
		public short[] field_200; // 1-32


		[DataOffset(0x244, DataType.Word)]
		public ushort field_244;

		[DataOffset(0x24E, DataType.Word)]
		public ushort field_24E;
		[DataOffset(0x250, DataType.Word)]
		public ushort field_250;
		[DataOffset(0x252, DataType.Word)]
		public ushort field_252;
		[DataOffset(0x254, DataType.Word)]
		public ushort field_254;
		[DataOffset(0x256, DataType.Word)]
		public ushort field_256;
		[DataOffset(0x258, DataType.Word)]
		public ushort field_258;
		[DataOffset(0x25A, DataType.Word)]
		public ushort field_25A;
		[DataOffset(0x25C, DataType.Word)]
		public ushort field_25C;
		[DataOffset(0x25E, DataType.Word)]
		public ushort field_25E;
		[DataOffset(0x260, DataType.Word)]
		public ushort field_260;

		[DataOffset(0x26A, DataType.Word)]
		public ushort field_26A;

		[DataOffset(0x296, DataType.Word)]
		public ushort field_296;
		[DataOffset(0x298, DataType.Word)]
		public ushort field_298;
		[DataOffset(0x29A, DataType.Word)]
		public ushort field_29A;
		[DataOffset(0x2B2, DataType.Word)]
		public ushort field_2B2;
		[DataOffset(0x2B4, DataType.Word)]
		public ushort field_2B4;
		[DataOffset(0x2B6, DataType.Word)]
		public ushort field_2B6;

        [DataOffset(0x2C0, DataType.Word)]
        public ushort field_2C0;
        [DataOffset(0x2CA, DataType.Word)]
        public ushort field_2CA;


		[DataOffset(0x336, DataType.Byte)]
		public byte field_336;
		[DataOffset(0x338, DataType.Byte)]
		public byte field_338;
		[DataOffset(0x33A, DataType.Byte)]
		public byte field_33A;
		[DataOffset(0x33C, DataType.Word)]
		public ushort field_33C;

		[DataOffset(0x340, DataType.Byte)]
		public byte field_340;
		[DataOffset(0x342, DataType.Byte)]
		public byte current_city; //field_342
		[DataOffset(0x344, DataType.Byte)]
		public byte field_344;
		[DataOffset(0x346, DataType.Byte)]
		public byte field_346;
		[DataOffset(0x348, DataType.Byte)]
		public byte field_348;

        [DataOffset(0x3C2, DataType.Word)]
        public ushort field_3C2;
        [DataOffset(0x3CA, DataType.Word)]
        public ushort field_3CA;
        [DataOffset(0x3CC, DataType.Word)]
        public ushort field_3CC;

		[DataOffset(0x3D4, DataType.Word)]
		public ushort field_3D4;
		[DataOffset(0x3D6, DataType.Word)]
		public ushort field_3D6;
		[DataOffset(0x3D8, DataType.Word)]
		public ushort field_3D8;
		[DataOffset(0x3DA, DataType.Word)]
		public ushort field_3DA;
		[DataOffset(0x3DC, DataType.Word)]
		public ushort field_3DC;
		[DataOffset(0x3DE, DataType.Word)]
		public ushort field_3DE;

		[DataOffset(0x3E0, DataType.Word)]
		public ushort field_3E0;
		[DataOffset(0x3E2, DataType.Word)]
		public ushort field_3E2;
		[DataOffset(0x3E4, DataType.Word)]
		public ushort field_3E4;
		[DataOffset(0x3E6, DataType.Word)]
		public ushort field_3E6;
		[DataOffset(0x3E8, DataType.Word)]
		public ushort field_3E8;

		[DataOffset(0x3FA, DataType.Byte)]
		public byte field_3FA;
		[DataOffset(0x3FC, DataType.Word)]
		public ushort field_3FC;
		[DataOffset(0x3FE, DataType.SWord)]
		public short picture_fade; //field_3FE

		[DataOffset(0x596, DataType.Word)]
		public ushort field_596;

		public void field_6A00_Set(int index, ushort value)
		{
			int loc = index & 0xFFFF;
			//System.Console.WriteLine("     field_6A00_Set loc: {0,4:X} value: {1,4:X}", loc, value );

			/* ovr021:0482 */
			switch (loc)
			{
				case 0x18C:
					field_18C = value;
					break;
				case 0x18E:
					time_minutes_ones = value;
					break;
				case 0x190:
					time_minutes_tens = value;
					break;
				case 0x192:
					time_hour = value;
					break;
				case 0x194:
					time_day = value;
					break;
				case 0x196:
					time_year = value;
					break;
				case 0x198:
					field_198 = value;
					break;

				case 0x1CC:
					inDungeon = (short)value;
					break;

				case 0x1CE:
					field_1CE = (short)value;
					break;

				case 0x1D0:
					field_1D0 = (short)value;
					break;

				case 0x1E0:
					lastXPos = (short)value;
					break;
				case 0x1E2:
					lastYPos = (short)value;
					break;

				case 0x1E4:
					LastEclBlockId = value;
					break;

				case 0x1F6:
					block_area_view = (short)value;
					break;

				case 0x1F8:
					game_speed = (byte)value;
					break;

				case 0x1FA:
					outdoor_sky_colour = value;
					break;

				case 0x1FC:
					indoor_sky_colour = value;
					break;

				case 0x200:
				case 0x202:
				case 0x204:
				case 0x206:
				case 0x208:
				case 0x20a:
				case 0x20c:
				case 0x20e:
				case 0x210:
				case 0x212:
				case 0x214:
				case 0x216:
				case 0x218:
				case 0x21a:
				case 0x21c:
				case 0x21e:
				case 0x220:
				case 0x222:
				case 0x224:
				case 0x226:
				case 0x228:
				case 0x22a:
				case 0x22c:
				case 0x22e:
				case 0x230:
				case 0x232:
				case 0x234:
				case 0x236:
				case 0x238:
				case 0x23a:
				case 0x23c:
				case 0x23e:
				case 0x240:
					field_200[(loc - 0x200) / 2] = (short)value;
					break;

				case 0x244:
					field_244 = value;
					break;

				case 0x24E:
					field_24E = value;
					break;
				case 0x250:
					field_250 = value;
					break;
				case 0x252:
					field_252 = value;
					break;
				case 0x254:
					field_254 = value;
					break;
				case 0x256:
					field_256 = value;
					break;
				case 0x258:
					field_258 = value;
					break;
				case 0x25A:
					field_25A = value;
					break;
				case 0x25C:
					field_25C = value;
					break;
				case 0x25E:
					field_25E = value;
					break;
				case 0x260:
					field_260 = value;
					break;

				case 0x26A:
					field_26A = value;
					break;

				case 0x296:
					field_296 = value;
					break;
				case 0x298:
					field_298 = value;
					break;
				case 0x29A:
					field_29A = value;
					break;

				case 0x2B2:
					field_2B2 = value;
					break;
				case 0x2B4:
					field_2B4 = value;
					break;
                case 0x2B6:
                    field_2B6 = value;
                    break;

                case 0x2C0:
                    field_2C0 = value;
                    break;
                case 0x2CA:
                    field_2CA = value;
                    break;

                case 0x336:
					field_336 = (byte)value;
					break;
				case 0x338:
					field_338 = (byte)value;
					break;
				case 0x33A:
					field_33A = (byte)value;
					break;
				case 0x33C:
					field_33C = value;
					break;

				case 0x340:
					field_340 = (byte)value;
					break;
				case 0x342:
					current_city = (byte)value;
					break;
				case 0x346:
					field_346 = (byte)value;
					break;
				case 0x348:
					field_348 = (byte)value;
					break;

                case 0x3C2:
                    field_3C2 = value;
                    break;
                case 0x3CA:
                    field_3CA = value;
                    break;
                case 0x3CC:
                    field_3CC = value;
                    break;

                case 0x3D4:
                    field_3D4 = value;
                    break;
				case 0x3D6:
					field_3D6 = value;
					break;
				case 0x3D8:
					field_3D8 = value;
					break;
				case 0x3DA:
					field_3DA = value;
					break;
				case 0x3DC:
					field_3DC = value;
					break;
				case 0x3DE:
					field_3DE = value;
					break;

				case 0x3E0:
					field_3E0 = value;
					break;
				case 0x3E2:
					field_3E2 = value;
					break;
				case 0x3E4:
					field_3E4 = value;
					break;
				case 0x3E6:
					field_3E6 = value;
					break;
				case 0x3E8:
					field_3E8 = value;
					break;


				case 0x3FA:
					field_3FA = (byte)value;
					break;
				case 0x3FC:
					field_3FC = value;
					break;
				case 0x3fe:
					picture_fade = (short)value;
					break;

				case 0x596:
					field_596 = value;
					break;

				default:
					DataIO.SetObjectUShort(this, origData, loc, value);
					break;
			}
		}

		public ushort field_6A00_Get(int index)
		{
			int loc = index & 0xFFFF;
			//System.Console.WriteLine("     field_6A00_Get loc: {0,4:X}", loc);

			/* ovr021:0482 */
			switch (loc)
			{
				case 0x18C:
					return field_18C;
				case 0x18E:
					return time_minutes_ones;
				case 0x190:
					return time_minutes_tens;
				case 0x192:
					return time_hour;
				case 0x194:
					return time_day;
				case 0x196:
					return time_year;
				case 0x198:
					return field_198;

				case 0x1E0:
					return (ushort)lastXPos;

				case 0x1E2:
					return (ushort)lastYPos;

				case 0x1CC:
					return (ushort)inDungeon;

				case 0x1E4:
					return LastEclBlockId;

				case 0x1F8:
					return (ushort)game_speed;

				case 0x1FA:
					return outdoor_sky_colour;

				case 0x1FC:
					return indoor_sky_colour;

				case 0x200:
				case 0x202:
				case 0x204:
				case 0x206:
				case 0x208:
				case 0x20a:
				case 0x20c:
				case 0x20e:
				case 0x210:
				case 0x212:
				case 0x214:
				case 0x216:
				case 0x218:
				case 0x21a:
				case 0x21c:
				case 0x21e:
				case 0x220:
				case 0x222:
				case 0x224:
				case 0x226:
				case 0x228:
				case 0x22a:
				case 0x22c:
				case 0x22e:
				case 0x230:
				case 0x232:
				case 0x234:
				case 0x236:
				case 0x238:
				case 0x23a:
				case 0x23c:
				case 0x23e:
				case 0x240:
					return (ushort)field_200[(loc - 0x200) / 2];

				case 0x244:
					return field_244;

				case 0x24E:
					return field_24E;
				case 0x250:
					return field_250;
				case 0x252:
					return field_252;
				case 0x254:
					return field_254;
				case 0x256:
					return field_256;
				case 0x258:
					return field_258;
				case 0x25A:
					return field_25A;
				case 0x25C:
					return field_25C;
				case 0x25E:
					return field_25E;
				case 0x260:
					return field_260;

				case 0x26A:
					return field_26A;

				case 0x296:
					return field_296;
				case 0x298:
					return field_298;

				case 0x29A:
					return field_29A;

				case 0x2B2:
					return field_2B2;
				case 0x2B4:
					return field_2B4;
                case 0x2B6:
                    return field_2B6;

                case 0x2C0:
                    return field_2C0;
                case 0x2CA:
                    return field_2CA;

                case 0x336:
					return field_336;
				case 0x338:
					return field_338;
				case 0x33A:
					return field_33A;
				case 0x33C:
					return field_33C;

				case 0x344:
					return field_344;
				case 0x346:
					return field_346;

                case 0x3C2:
                    return field_3C2;
                case 0x3CA:
                    return field_3CA;
                case 0x3CC:
                    return field_3CC;
                
                case 0x3D4:
                    return field_3D4;
                case 0x3D6:
					return field_3D6;
				case 0x3D8:
					return field_3D8;
				case 0x3DA:
					return field_3DA;
				case 0x3DC:
					return field_3DC;
				case 0x3DE:
					return field_3DE;


				case 0x3E0:
					return field_3E0;
				case 0x3E2:
					return field_3E2;
				case 0x3E4:
					return field_3E4;
				case 0x3E6:
					return field_3E6;
				case 0x3E8:
					return field_3E8;

				case 0x3FA:
					return field_3FA;
				case 0x3FC:
					return field_3FC;

				case 0x596:
					return field_596;

				default:
					return DataIO.GetObjectUShort(this, origData, loc);
			}
		}

		public byte[] ToByteArray()
		{
			DataIO.WriteObject(this, origData);

			return (byte[])origData.Clone();
		}

		public void RestField200Values()
		{
            for (int loop_var = 0; loop_var <= 32; loop_var++)
            {
                gbl.area_ptr.field_200[loop_var] = 0; // word array.
            }
		}
	}
}
