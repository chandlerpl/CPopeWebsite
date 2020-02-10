using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{
	public class Hero : Character
	{
		private Temple temple;
		private Chamber currChamber;
		private LinkedList<Item> pouch = new LinkedList<Item>();

		public Hero(string name, int firepower, Temple temple) : base(name, firepower)
		{
			this.temple = temple;
			currChamber = temple.getChamber(
					GameManager.getRandom().Next(temple.getYChambers()),
					GameManager.getRandom().Next(temple.getXChambers())
					);
		}

		public Chamber getChamber() { return currChamber; }

		public bool move(Directions dir)
		{
			if (currChamber.hasDoor(dir))
			{
				switch (dir)
				{
					case Directions.North:
						currChamber = temple.getChamber(currChamber.getYPos() - 1, currChamber.getXPos());
						break;
					case Directions.East:
						currChamber = temple.getChamber(currChamber.getYPos(), currChamber.getXPos() + 1);
						break;
					case Directions.South:
						currChamber = temple.getChamber(currChamber.getYPos() + 1, currChamber.getXPos());
						break;
					case Directions.West:
						currChamber = temple.getChamber(currChamber.getYPos(), currChamber.getXPos() - 1);
						break;
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool pickupItem()
		{
			if (currChamber.getDragon().isDead() && currChamber.getItem() != null && !currChamber.getItem().isPickedUp())
			{
				if (pouch.Count == 0)
				{
					pouch.AddFirst(currChamber.getItem());
					currChamber.getItem().setPickedUp(true);
					return true;
				}
				else
				{
					for (int i = 0; i < pouch.Count; i++)
					{
						if (pouch.ElementAt(i).getValue() >= currChamber.getItem().getValue())
						{
							pouch.AddAfter(pouch.Find(pouch.ElementAt(i)), currChamber.getItem());
							currChamber.getItem().setPickedUp(true);
							return true;
						}
					}
					pouch.AddLast(currChamber.getItem());
					currChamber.getItem().setPickedUp(true);
					return true;
				}
			}

			return false;
		}

		public string openInv()
		{
			StringBuilder s = new StringBuilder();
			if(pouch.Count == 0)
			{
				s.Append("Your pouch is empty!");
				return s.ToString();
			}
			s.Append("Here are the items in your pouch:\n");

			int tValue = 0;
			for (int i = 0; i < pouch.Count; i++)
			{
				Item item = pouch.ElementAt(i);
				s.Append(i + 1);
				s.Append(": ");
				s.Append(item.getName());
				s.Append(" (value ");
				s.Append(item.getValue());
				s.Append(")\n");
				tValue += item.getValue();
			}

			s.Append("Total value of all items is ");
			s.Append(tValue);

			return s.ToString();
		}

		public override void lose()
		{
			setFirepower(getFirepower() - 1);
		}
		public override void win()
		{
			setFirepower(getFirepower() + 1);
		}

		public void rest()
		{
			setFirepower(getFirepower() + 1);
		}

		public override string ToString()
		{
			return currChamber.toString();
		}
	}
}
