using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2
{
    public static class Utils
    {
        public static Random Random { get; private set; } = new Random();
        public static List<string> ItemNames { get; private set; } = new List<string>();


    }
}
