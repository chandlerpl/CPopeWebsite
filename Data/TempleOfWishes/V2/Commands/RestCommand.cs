using CPopeWebsite.Data.TempleOfWishes.V2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Commands
{
    public class RestCommand : TOWCommand
    {
        public RestCommand() : base("Rest", "rest",
                    "This command lets the hero rest for a small health gain.",
                    new List<string> { "rest", "r" })
        {}

        public override bool Execute(Hero hero, List<string> args)
        {
            if (hero.Rest())
                hero.Logger.Append("You have rested, your health is now " + hero.Health);
            else
                hero.Logger.Append("You are already at max health.");
            return true;
        }
    }
}
