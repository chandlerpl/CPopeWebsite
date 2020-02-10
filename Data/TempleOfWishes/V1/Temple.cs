using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{ public class Temple
	{
		private int xChambers = 6;
		private int yChambers = 6;
		private int numItems = 18;
		private Dragon[] dragons;
		private Item[] items;
		private Chamber[,] chambers;
		
		public Temple(int xChambers = 6, int yChambers = 6, int numItems = 18)
		{
			this.xChambers = xChambers;
			this.yChambers = yChambers;
			this.numItems = numItems;
		}

		public int getXChambers() { return xChambers; }

		public int getYChambers() { return yChambers; }

		public int getNumItems() { return numItems; }

		public Chamber getChamber(int y, int x)
		{
			return chambers[y, x];
		}

		public void generateDragons()
		{
			dragons = new Dragon[numItems];
			for (int i = 0; i < numItems; i++)
			{
				dragons[i] = new Dragon(GameManager.DRAGON_NAMES[i % 3], i / 3 + 5);
			}

			for (int i = 0; i < dragons.Length; i++)
			{
				int j = GameManager.getRandom().Next(i + 1);

				Dragon temp = dragons[j];
				dragons[j] = dragons[i];
				dragons[i] = temp;
			}
		}

		public void generateItems(JsonElement root)
		{
			items = new Item[numItems];
			JsonElement allItems = root.GetProperty("items");

			for (int i = 0; i < numItems; i++)
			{
				String name = allItems[i].GetString();
				items[i] = new Item(name, GameManager.getRandom().Next(20) + 1);
				int y = GameManager.getRandom().Next(yChambers);
				int x = GameManager.getRandom().Next(xChambers);

				while (!chambers[y, x].addItem(items[i]))
				{
					y = GameManager.getRandom().Next(yChambers);
					x = GameManager.getRandom().Next(xChambers);
				}

				chambers[y, x].addDragon(dragons[i]);
			}
		}

		public void generateChambers(JsonElement root)
		{
			chambers = new Chamber[yChambers, xChambers];
			JsonElement allDoors = root.GetProperty("doors");
			int cDoors = 0;

			for (int i = 0; i < chambers.GetLength(0); i++)
			{
				for (int j = 0; j < chambers.GetLength(1); j++)
				{
					int[] doors = { 0, 0, 0, 0 };

					try
					{
						for (var v = 0; v < allDoors[cDoors].GetArrayLength(); v++)
						{
							doors[v] = allDoors[cDoors][v].GetInt32();
						}
						cDoors++;
					} catch (Exception e)
					{
						
					}

					chambers[i, j] = new Chamber(i, j, doors);
				}
			}
		}

		public void endAll()
		{
			foreach (Item item in items)
			{
				item.setPickedUp(true);
			}

			foreach (Dragon dragon in dragons)
			{
				dragon.setDead(true);
			}
		}

		public bool checkItemsTaken()
		{
			foreach (Item item in items)
				if (!item.isPickedUp())
					return false;

			return true;
		}

		public bool checkDragonsDead()
		{
			foreach (Dragon dragon in dragons)
				if (!dragon.isDead())
					return false;

			return true;
		}
	}
}