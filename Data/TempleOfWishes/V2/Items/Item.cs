using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Items
{
    public class Item
    {
        public string Name { get; protected set; }
        public float Value { get; protected set; }

        public Item(string name, float value)
        {
            Name = name;
            Value = value;
        }

        public ItemType getItemType() { return ItemType.Item; }
    }
}
