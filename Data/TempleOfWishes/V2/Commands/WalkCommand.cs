using CPopeWebsite.Data.TempleOfWishes.V2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Commands
{
    public class WalkCommand : TOWCommand
    {
        public WalkCommand() : base("Walk", "walk (north | northeast | east | southeast | south | southwest | west | northwest)",
                                "This command moves the Hero in the specified Direction",
                                new List<string> { "walk", "w", "move", "m" })
        { }

        public override bool Execute(Hero hero, List<string> args)
        {
            if(args != null && args.Count == 2 && args[1].Length > 0)
            {
                Directions dir;
                string name = args[1].Substring(0, 1).ToUpper() + args[1].Substring(1).ToLower();
                name = name.Replace("-", "");

                if (Enum.TryParse(name, out dir) || DirectionsHelper.TryConvertAbbr(name, out dir))
                {
                    if (hero.Move(dir))
                    {
                        hero.Logger.Append(hero.CurrTile);
                    }
                    else
                    {
                        hero.Logger.Append("You can't go that way.\n");
                    }
                } else
                {
                    hero.Logger.Append("The direction ");
                    hero.Logger.Append(name);
                    hero.Logger.Append(" is not valid!\n");
                }

                return true;
            }

            return false;
        }
    }
}
