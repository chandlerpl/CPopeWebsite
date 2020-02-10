using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{
	public class Character
	{
		public static readonly int MIN_FIREPOWER = 5;
		public static readonly int MAX_FIREPOWER = 10;
		private String name;
		private int firepower;

		public Character(String name, int firepower)
		{
			this.name = name;
			this.firepower = firepower;
		}

		public String getName()
		{
			return name;
		}

		public int getFirepower()
		{
			return firepower;
		}

		public void setFirepower(int firepower)
		{
			if (firepower <= MAX_FIREPOWER && firepower >= MIN_FIREPOWER)
				this.firepower = firepower;
		}

		public bool fight(Character character)
		{
			int rand = GameManager.getRandom().Next(2);
			if (firepower > character.getFirepower() || (firepower == character.getFirepower() && rand == 1))
			{
				win();
				character.lose();
				return true;
			}
			else
			{
				lose();
				character.win();
				return false;
			}
		}

		public virtual void lose() { }

		public virtual void win() { }

		public override string ToString()
		{
			return name;
		}
	}
}
