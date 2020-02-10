using CPopeWebsite.Data.TempleOfWishes.V2.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Characters
{
	public class Character
	{
		public string Name { get; protected set; }
		public float MaxHealth { get; protected set; } = 100;
		public float Health { get; protected set; } = 100;
		public int Strength { get; protected set; }
		public bool IsDead { get; protected set; }

		public Character(string name, int strength)
		{
			Name = name;
			Strength = strength;
		}

		public bool Attack(Character character)
		{
			return Attack(character, Strength);
		}

		public bool Attack(Character character, Item weapon)
		{
			return Attack(character, weapon.Value);
		}

		public bool Attack(Character character, float damage)
		{
			if (character.IsDead || Utils.Random.Next(5) == 0)
				return false;

			character.takeDamage(damage);
			return true;
		}

		public void takeDamage(float damage)
		{
			Health -= damage;
			if (Health <= 0)
				IsDead = true;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
