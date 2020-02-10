using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using CPopeWebsite.Data.TempleOfWishes.V2.Characters;

namespace CPopeWebsite.Data.TempleOfWishes.V2.Commands
{
    public abstract class TOWCommand
    {
        public string Name { get; private set; }
        public string ProperUse { get; private set; }
        public string Desc { get; private set; }
        public List<string> Aliases { get; private set; }

        public TOWCommand(string name, string properUse, string desc, List<string> aliases)
        {
            Name = name;
            ProperUse = properUse;
            Desc = desc;
            Aliases = aliases;

            Command.addCommand(name, this);

        }

        public abstract bool Execute(Hero hero, List<string> args);

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append("Help for: ");
            message.Append(Name);
            message.Append("\nProper Use: ");
            message.Append(ProperUse);
            message.Append("\nDescription: ");
            message.Append(Desc);
            message.Append("\nAlternatives: ");
            message.Append(Aliases.ToString());

            return message.ToString();
        }
    }
}
