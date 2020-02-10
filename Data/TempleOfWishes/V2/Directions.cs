using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2
{
    public enum Directions
    {
        North,
        Northeast,
        East,
        Southeast,
        South,
        Southwest,
        West,
        Northwest,  
    }

    public static class DirectionsHelper
    {
        public static Dictionary<string, Directions> DirectionAbbreviations = new Dictionary<string, Directions>() 
        {
            { "N", Directions.North },
            { "NE", Directions.Northeast },
            { "E", Directions.East },
            { "SE", Directions.Southeast },
            { "S", Directions.South },
            { "SW", Directions.Southwest },
            { "W", Directions.West },
            { "NW", Directions.Northwest },
        };
        public static bool TryConvertAbbr(string abbr, out Directions dir)
        {
            abbr = abbr.ToUpper();
            if (DirectionAbbreviations.ContainsKey(abbr))
            {
                dir = DirectionAbbreviations[abbr];
                return true;
            }

            dir = Directions.North;
            return false;
        }
    }
}
