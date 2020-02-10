using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{

	public class Dragon : Character
	{
		public bool dead = false;

		public Dragon(string name, int firepower) : base(name, firepower)
		{

		}

		public bool isDead()
		{
			return dead;
		}

		public void setDead(bool b)
		{
			dead = b;
		}

		public override void lose()
		{
			dead = true;
		}
	}
}
