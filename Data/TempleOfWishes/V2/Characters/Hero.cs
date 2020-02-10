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
                        CurrTile = GameManager.Tiles[CurrTile.Y - 1, CurrTile.X];
                        break;
                    case Directions.Northeast:
                        CurrTile = GameManager.Tiles[CurrTile.Y - 1, CurrTile.X - 1];
                        break;
                    case Directions.East:
                        CurrTile = GameManager.Tiles[CurrTile.Y, CurrTile.X - 1];
                        break;
                    case Directions.Southeast:
                        CurrTile = GameManager.Tiles[CurrTile.Y + 1, CurrTile.X - 1];
                        break;
                    case Directions.South:
                        CurrTile = GameManager.Tiles[CurrTile.Y + 1, CurrTile.X];
                        break;
                    case Directions.Southwest:
                        CurrTile = GameManager.Tiles[CurrTile.Y + 1, CurrTile.X + 1];
                        break;
                    case Directions.West:
                        CurrTile = GameManager.Tiles[CurrTile.Y, CurrTile.X + 1];
                        break;
                    case Directions.Northwest:
                        CurrTile = GameManager.Tiles[CurrTile.Y - 1, CurrTile.X + 1];
                        break;
                }

                return true;
            }

            return false;
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
