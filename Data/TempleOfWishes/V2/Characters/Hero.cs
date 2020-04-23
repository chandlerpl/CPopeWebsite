using CPopeWebsite.Data.TempleOfWishes.V2.Items;
using CPopeWebsite.Data.TempleOfWishes.V2.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Characters
{
    public class Hero : Character
    {
        public Tile CurrTile { get; private set; }
        private LinkedList<Item> pouch = new LinkedList<Item>();
        public StringBuilder Logger { get; private set; }
        public GameManager GameManager { get; private set; }
        
        public Hero(string name, int strength, Tile tile, GameManager gameManager) : base(name, strength)
        {
            Logger = new StringBuilder();
            CurrTile = tile;
            GameManager = gameManager;
        }

        public bool Move(Directions dir)
        {
            if(CurrTile.hasPath(dir))
            {
                switch (dir)
                {
                    case Directions.North:
                        return Move(CurrTile.Y - 1, CurrTile.X);
                    case Directions.Northeast:
                        return Move(CurrTile.Y - 1, CurrTile.X - 1);
                    case Directions.East:
                        return Move(CurrTile.Y, CurrTile.X - 1);
                    case Directions.Southeast:
                        return Move(CurrTile.Y + 1, CurrTile.X - 1);
                    case Directions.South:
                        return Move(CurrTile.Y + 1, CurrTile.X);
                    case Directions.Southwest:
                        return Move(CurrTile.Y + 1, CurrTile.X + 1);
                    case Directions.West:
                        return Move(CurrTile.Y, CurrTile.X + 1);
                    case Directions.Northwest:
                        return Move(CurrTile.Y - 1, CurrTile.X + 1);
                }

                return true;
            }

            return false;
        }

        private bool Move(int Y, int X)
        {
            if (Y < 0 || X < 0 || Y > GameManager.Tiles.GetLength(0) - 1 || X > GameManager.Tiles.GetLength(1) - 1)
                return false;
            else
            { 
                CurrTile = GameManager.Tiles[Y, X];
                return true;
            }
        }

        // TODO: public bool pickupItem() { }

        public bool Rest()
        {
            if(Health == MaxHealth)
                return false;

            Health = Health <= MaxHealth - 20 ? Health + 20 : MaxHealth;

            return true;
        }

        public override string ToString()
        {
            return Logger.ToString();
        }
    }
}
