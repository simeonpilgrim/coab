using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
	public class Money
	{
		public const int Copper = 0;
		public const int Silver = 1;
		public const int Electrum = 2;
		public const int Gold = 3;
		public const int Platinum = 4;
		public const int Gems = 5;
		public const int Jewelry = 6;

		public static string[] names = { "Copper", "Silver", "Electrum", "Gold", "Platinum", "Gems", "Jewelry" };

		public static int[] per_copper = { 1, 10, 100, 200, 1000 };
	}


	public class MoneySet : IDataIO
	{
		int[] money = new int[7];

		// overload operator +
		public static MoneySet operator +(MoneySet a, MoneySet b)
		{
			var c = new MoneySet();

			for (int coin = Money.Copper; coin <= Money.Platinum; coin++)
			{
				c.money[coin] = a.money[coin] + b.money[coin];
			}

			return c;
		}


		public void ClearAll()
		{
			for (int coin = Money.Copper; coin <= Money.Jewelry; coin++)
			{
				money[coin] = 0;
			}
		}


		public void ClearCoins()
		{
			for (int coin = Money.Copper; coin <= Money.Platinum; coin++)
			{
				money[coin] = 0;
			}
		}


		public int GetExpWorth()
		{
			int total = GetGoldWorth();

			total += money[Money.Gems] * 250;
			total += money[Money.Jewelry] * 2200;

			return total;
		}


		public int GetGoldWorth()
		{
			int copperValue = 0;
			for (int coin = Money.Copper; coin <= Money.Platinum; coin++)
			{
				copperValue += money[coin] * Money.per_copper[coin];
			}

			return copperValue / Money.per_copper[Money.Gold];
		}

		public void AddCoins(int coinType, int count)
		{
			money[coinType] += count;
		}


		public void SetCoins(int coinType, int count)
		{
			money[coinType] += count;
		}


		public void SubtractGoldWorth(int gold)
		{
			int coppers = gold * Money.per_copper[Money.Gold];

			int coin = Money.Copper;

			while (coppers > 0)
			{
				int sub_coins = (coppers / Money.per_copper[coin]) + 1;

				if (money[coin] < sub_coins)
				{
					sub_coins = money[coin];
				}

				coppers -= Money.per_copper[coin] * sub_coins;
				money[coin] -= sub_coins;

				coin ++;
			}

			if (coppers < 0)
			{
				coppers = System.Math.Abs(coppers);
				coin = Money.Platinum;

				while (coppers > 0)
				{
					int add_coins = coppers / Money.per_copper[coin];
					coppers -= Money.per_copper[coin] * add_coins;

					money[coin] += add_coins;
					coin --;
				}
			}
		}


		public int GetCoins(int coinType)
		{
			return money[coinType];
		}


		public bool AnyMoney()
		{
			for (int coin = Money.Copper; coin <= Money.Platinum; coin++)
			{
				if (money[coin] > 0)
				{
					return true;
				}
			}

			return false;
		}


		public bool ScaleAll(double scale)
		{
			bool didScale = false;
			for (int coin = Money.Copper; coin <= Money.Platinum; coin++)
			{
				didScale = didScale || (money[coin] > 0);
				money[coin] = (int)(money[coin] * scale);
			}

			return didScale;
		}

		public int Copper 
		{
			get { return money[Money.Copper]; }
			//set { money[Money.copper] = value; }
		}
		public int Electrum 
		{
			get { return money[Money.Electrum]; }
			//set { money[Money.electrum] = value; }
		}
		public int Silver 
		{
			get { return money[Money.Silver]; }
			//set { money[Money.silver] = value; }
		}
		public int Gold 
		{
			get { return money[Money.Gold]; }
			//set { money[Money.gold] = value; }
		}
		public int Platinum 
		{
			get { return money[Money.Platinum]; }
			//set { money[Money.platum] = value; }
		}
		public int Gems
		{
			get { return money[Money.Gems]; }
			//set { money[5] = value; }
		}
		public int Jewels
		{
			get { return money[Money.Jewelry]; }
			//set { money[6] = value; }
		}


		void IDataIO.Write(byte[] data, int offset)
		{
			Sys.ShortToArray((short)money[0], data, offset + 0);
			Sys.ShortToArray((short)money[1], data, offset + 2);
			Sys.ShortToArray((short)money[2], data, offset + 4);
			Sys.ShortToArray((short)money[3], data, offset + 6);
			Sys.ShortToArray((short)money[4], data, offset + 8);
			Sys.ShortToArray((short)money[5], data, offset + 10);
			Sys.ShortToArray((short)money[6], data, offset + 12);
		}

		void IDataIO.Read(byte[] data, int offset)
		{
			money[0] = Sys.ArrayToShort(data, offset + 0);
			money[1] = Sys.ArrayToShort(data, offset + 2);
			money[2] = Sys.ArrayToShort(data, offset + 4);
			money[3] = Sys.ArrayToShort(data, offset + 6);
			money[4] = Sys.ArrayToShort(data, offset + 8);
			money[5] = Sys.ArrayToShort(data, offset + 10);
			money[6] = Sys.ArrayToShort(data, offset + 12);
		}
	}
}
