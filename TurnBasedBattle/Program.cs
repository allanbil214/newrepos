using System;

namespace TurnBasedBattle
{
    class Game
    {
        string playerName = "";
        int[] selectedClass = [];
        enum entityStats : byte { HP, AP, InnateDefense }
        Dictionary<string, int[]> playerClasses = new Dictionary<string, int[]>()
        {
            {"Warrior", [100, 7, 4]},
            {"Mage", [80, 10, 3]},
            {"Assassin", [90, 15, 2]}
        };
        Dictionary<string, int[]> enemyTypes = new Dictionary<string, int[]>()
        {
            {"Goblin Rook", [100, 7, 4]},
            {"Goblin Shaman", [80, 10, 3]},
            {"Goblin Swordsman", [90, 15, 2]}
        };

        public static void Main(string[] args)
        {
            var game = new Game();
            game.GameInit();
        }

        void GameInit() // to-do refactor the nested if(s)
        {
            Player player = new Player(); 

            Console.WriteLine("\n==== Welcome to Battler ====\n== Start Screen ==\n[i] Select the option: \n[1] Start \n[2] Exit");
            string selected = Console.ReadLine();
            if (selected == "1")
            {
                Console.WriteLine("\n[=] Name Your Character!");
                playerName = Console.ReadLine();

                ShowClasses();
                Console.WriteLine("\n[=] Select your Classes");
                selected = Console.ReadLine();

                if (selected == "1")
                {
                    selectedClass = playerClasses["Warrior"];
                    Console.WriteLine("[i] You Selected Warrior \n");
                    player.healthPoint = selectedClass[(int)entityStats.HP];
                    player.attackPower = selectedClass[(int)entityStats.AP];
                    player.innateDefense = selectedClass[(int)entityStats.InnateDefense];
                }
            
                
            }
            else if (selected == "2")
            {
                Console.WriteLine("[i] Bye-bye!");
            }
            else Console.WriteLine("[!] Womp Womp, unrecognized input :()");
        }

        void ShowClasses()
        {
            Console.WriteLine("\n[i] Classes Types: ");
            int i = 1;
            foreach (var classes in playerClasses)
            {
                Console.WriteLine($"[{i++}] {classes.Key}: HP ({classes.Value[(int)entityStats.HP]}), AP ({classes.Value[(int)entityStats.AP]})");
            }
        }

        private int TryParsing(string msg)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine(value);
                    return value;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }

        void Testing()
        {
            Player player = new Player() {healthPoint = 100, attackPower = 7, name = "Slayer", gold = 0};
            Goblin enemy = new Goblin() {healthPoint = 100, attackPower = 7, name = "Goblin Rook", amount = 10};

            Console.WriteLine($"\n==== Welcome to Battler ====\n" +
                    $"Player ({player.Name}): HP({player.HealthPoint}), AP({player.AttackPower})\n" +
                    $"Enemy ({enemy.Name}: HP({enemy.HealthPoint}), AP({enemy.AttackPower})\n");

            player.Attack(enemy);
            Console.WriteLine($"Looks like Player attacked Enemy which dealt {player.AttackPower} and left the enemy HP to {enemy.HealthPoint}\n");
        }
    }
}