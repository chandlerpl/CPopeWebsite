using CPopeWebsite.Data.TempleOfWishes.V2.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Commands
{
    public class Command
    {
        public static Dictionary<string, TOWCommand> Commands { get; private set; } = new Dictionary<string, TOWCommand>();

        public Command()
        {
            new WalkCommand();
            new RestCommand();
        }

        public static bool addCommand(string name, TOWCommand command)
        {
            if(!Commands.ContainsKey(name))
            {
                Commands.Add(name, command);
                return true;
            }

            return false;
        }

        public bool commandInterface(Hero hero, string[] args)
        {
            foreach (TOWCommand command in Commands.Values)
            {
                if(command.Aliases.Contains(args[0].ToLower()))
                {
                    if(!command.Execute(hero, args.ToList()))
                    {
                        hero.Logger.Append(command);
                    }

                    return true;
                }
            }

            hero.Logger.Append("Invalid Command!\n");
            return false;
        }
    }
}
