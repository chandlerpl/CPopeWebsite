using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{
	public class Item
	{
		private string name;
		private int value;
		private bool pickedUp = false;

		public Item(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

		public string getName()
		{
			return name;
		}

		public int getValue()
		{
			return value;
		}

		public bool isPickedUp()
		{
			return pickedUp;
		}

		public void setPickedUp(bool b)
		{
			pickedUp = b;
		}
	}
}
