using CPopeWebsite.Data.TempleOfWishes.V2.Characters;
using CPopeWebsite.Data.TempleOfWishes.V2.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Tiles
{
    public class Tile
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public string Name { get; protected set; }
        public byte[] Paths { get; protected set; } = { 1, 1, 1, 1, 1, 1, 1, 1 };
        public List<Item> Items { get; protected set; } = new List<Item>();
        public List<Character> Characters { get; protected set; } = new List<Character>();

        public Tile(int xPos, int yPos, byte[] paths = null, string name = null)
        {
            X = xPos;
            Y = yPos;
            if(paths != null)
                Paths = paths;
            if(name != null)
                Name = name;
        }

        /*public bool addItem(Item i)
        {
            if (Item != null)
                return false;

            Item = i;
            return true;
        }

        public Item removeItem()
        {
            Item item = Item;
            Item = null;
            return item;
        }*/

        /*public bool addCharacter(Character d)
        {
            if (character != null)
                return false;

            character = d;
            return true;
        }*/

        public bool hasPath(Directions dir)
        {
            return Paths[(int)dir] == 1;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            s.Append("You are in a ");
            s.Append(Name);
            s.Append(" (");
            s.Append(Y);
            s.Append(", ");
            s.Append(X);
            s.Append(")\n");

            if (Paths[0] == 1) s.Append("There is a path to the North\n");
            if (Paths[1] == 1) s.Append("There is a path to the East\n");
            if (Paths[2] == 1) s.Append("There is a path to the South\n");
            if (Paths[3] == 1) s.Append("There is a path to the West\n");

            if(Characters.Count > 0)
            {
                foreach(Character c in Characters)
                {
                    s.Append("There is a ");
                    s.Append(c);
                    s.Append(" here!\n");
                }
            }

            if (Items.Count > 0)
            {
                foreach (Item i in Items)
                {
                    s.Append("There is a ");
                    s.Append(i.Name);
                    s.Append(" here (value ");
                    s.Append(i.Value);
                    s.Append(" here!\n");
                }
            }

            return s.ToString();
        }
    }
}
