using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CPopeWebsite.Data.TempleOfWishes.V1
{
    public class GameManager
    {
        public static readonly string[] DRAGON_NAMES = { "red", "blue", "green" };
        private static Random rand = new Random();
        public StringBuilder logs = new StringBuilder();

        private Temple templeOfWishes;
        private Hero hero;

        public static Task<GameManager> CreateGame()
        {
            return Task.Run(() =>
            {
                GameManager gm = new GameManager();

                gm.start();

                return gm;
            });
        }
        public void start()
        {
            try
            {
                var jsonBytes = File.ReadAllBytes("Data/TempleOfWishes/V1/GameData.json");

                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonBytes))
                {
                    JsonElement root = jsonDoc.RootElement;

                    templeOfWishes = new Temple();
                    templeOfWishes.generateChambers(root);
                    templeOfWishes.generateDragons();
                    templeOfWishes.generateItems(root);

                    hero = new Hero("Hero", 5, templeOfWishes);
                }

                logs.Append(hero);
            } catch (Exception e)
            {

            }
        }

        public void play(char command)
        {
            switch(command)
            {
                case 'n':
                    if (hero.move(Directions.North))
                        logs.Append(hero);
                    else
                        logs.Append("You can't go that way.\n");
                    break;
                case 's':
                    if (hero.move(Directions.South))
                        logs.Append(hero);
                    else
                        logs.Append("You can't go that way.\n");
                    break;
                case 'e':
                    if (hero.move(Directions.East))
                        logs.Append(hero);
                    else
                        logs.Append("You can't go that way.\n");
                    break;
                case 'w':
                    if (hero.move(Directions.West))
                        logs.Append(hero);
                    else
                        logs.Append("You can't go that way.\n");
                    break;
                case 'f':
                    if (hero.fight(hero.getChamber().getDragon()))
                        logs.Append("You won the battle! Your firepower is now " + hero.getFirepower() + ".\n");
                    else
                        logs.Append("You lost the battle! Your firepower is now " + hero.getFirepower() + ".\n");
                    break;
                case 'i':
                    logs.Append(hero.openInv());
                    break;
                case 'p':
                    if (hero.pickupItem())
                        logs.Append("Ok, Item taken.\n");
                    else
                        logs.Append("Unable to pickup the item.\n");
                    break;
                case 'r':
                    hero.rest();
                    logs.Append("You rested! Your firepower is now " + hero.getFirepower() + ".\n");
                    break;
                default:
                    logs.Append("Invalid Character.\n");
                    break;
            }
        }

        public static Random getRandom()
        {
            return rand;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(logs.ToString());

            return sb.ToString();
        }
    }
}
