using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{

	public class Chamber
	{
		private int xPos = 1;
		private int yPos = 1;

		private int[] doors = new int[4];
		private Item item;
		private Dragon dragon;

		public Chamber(int yPos, int xPos, int[] doorLocs)
		{
			this.xPos = xPos;
			this.yPos = yPos;

			doors = doorLocs;
		}

		public int getXPos()
		{
			return xPos;
		}

		public int getYPos()
		{
			return yPos;
		}

		public bool addItem(Item i)
		{
			if (item != null)
				return false;

			item = i;
			return true;
		}

		public Item getItem()
		{
			return item;
		}

		public bool addDragon(Dragon d)
		{
			if (dragon != null)
				return false;

			dragon = d;
			return true;
		}

		public Dragon getDragon()
		{
			return dragon;
		}

		public bool hasDoor(Directions dir)
		{
			if (doors[(int) dir] == 1)
				return true;

			return false;
		}

		public string toString()
		{
			StringBuilder s = new StringBuilder();
		
			s.Append("You are in a chamber (");
			s.Append(yPos);
			s.Append(", ");
			s.Append(xPos);
			s.Append(")\n");

			if (doors[0] == 1) s.Append("There is a door to the North\n");
			if (doors[1] == 1) s.Append("There is a door to the East\n");
			if (doors[2] == 1) s.Append("There is a door to the South\n");
			if (doors[3] == 1) s.Append("There is a door to the West\n");
			if (dragon != null && !dragon.isDead())
			{
				s.Append("There is a ");
				s.Append(dragon);
				s.Append(" dragon here!\n");
			}

			if (item != null && !item.isPickedUp())
			{
				s.Append("There is a ");
				s.Append(item.getName());
				s.Append(" here (value ");
				s.Append(item.getValue());
				s.Append(")\n");
			}

			return s.ToString();
		}
	}
}
