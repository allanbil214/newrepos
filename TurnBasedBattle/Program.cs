using System;
using System.Collections.Generic;

namespace TurnBasedBattle
{
    class Game
    {
        Random rand = new Random();
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

        void GameInit()
        {
            Player player = new Player();
            if (ShowStartScreen())
            {
                CreateCharacter(player);
                RunGameLoop(player);
            }
        }
        
        bool ShowStartScreen()
        {
            Console.WriteLine("\n==== Welcome to Battler ====\n== Start Screen ==\n[i] Select the option: \n[1] Start \n[2] Exit");
            string selected = Console.ReadLine();
            
            if (selected == "1") return true;
            else if (selected == "2") Console.WriteLine("[i] Bye-bye!");
            else Console.WriteLine("[!] Womp Womp, unrecognized input :()");
            
            return false;
        }

        void CreateCharacter(Player player)
        {
            Console.WriteLine("\n[=] Name Your Character!");
            playerName = Console.ReadLine();
            player.Name = playerName;
            
            ShowClasses();
            Console.WriteLine("\n[=] Select your Classes");
            string selected = Console.ReadLine();
            
            ApplyClassStats(player, selected);
            Console.WriteLine($"Welcome {player.Name}! Your adventure begins now...\n");
        }

        void ApplyClassStats(Player player, string classChoice)
        {
            switch (classChoice)
            {
                case "1":
                    selectedClass = playerClasses["Warrior"];
                    Console.WriteLine("[i] You Selected Warrior \n");
                    break;
                case "2":
                    selectedClass = playerClasses["Mage"];
                    Console.WriteLine("[i] You Selected Mage \n");
                    break;
                case "3":
                    selectedClass = playerClasses["Assassin"];
                    Console.WriteLine("[i] You Selected Assassin \n");
                    break;
                default:
                    Console.WriteLine("[!] Invalid class selection, defaulting to Warrior");
                    selectedClass = playerClasses["Warrior"];
                    break;
            }
            
            player.HealthPoint = selectedClass[(int)entityStats.HP];
            player.AttackPower = selectedClass[(int)entityStats.AP];
            player.InnateDefense = selectedClass[(int)entityStats.InnateDefense];
        }

        void RunGameLoop(Player player)
        {
            Random rand = new Random();
            int enemiesDefeated = 0;
            bool gameRunning = true;
            
            while (gameRunning && player.IsAlive)
            {
                BaseGoblin enemy = CreateRandomEnemy(rand);
                BattleResult result = RunBattle(player, enemy, enemiesDefeated);
                
                if (result.PlayerWon)
                {
                    enemiesDefeated++;
                    enemy.Loot(player);
                    gameRunning = AskContinue(player, enemiesDefeated);
                }
                else if (result.PlayerRanAway)
                {
                    gameRunning = AskRetry(player, enemiesDefeated);
                }
                else
                {
                    gameRunning = false;
                }
            }
            
            ShowGameOverScreen(player, enemiesDefeated);
        }

        BaseGoblin CreateRandomEnemy(Random rand)
        {
            string[] enemyNames = {"Goblin Rook", "Goblin Shaman", "Goblin Swordsman"};
            string enemyName = enemyNames[rand.Next(enemyNames.Length)];
            int[] enemyStats = enemyTypes[enemyName];
            
            return new BaseGoblin()
            {
                Name = enemyName,
                HealthPoint = enemyStats[(int)entityStats.HP],
                AttackPower = enemyStats[(int)entityStats.AP],
                InnateDefense = enemyStats[(int)entityStats.InnateDefense],
                LootAmount = rand.Next(5, 21)
            };
        }

        BattleResult RunBattle(Player player, BaseGoblin enemy, int battleNumber)
        {
            Console.WriteLine($"\n==== BATTLE {battleNumber + 1} ====");
            Console.WriteLine($"A wild {enemy.Name} appears!");
            Console.WriteLine($"Player ({player.Name}): HP({player.HealthPoint}), AP({player.AttackPower}), Gold({player.Gold})");
            Console.WriteLine($"Enemy ({enemy.Name}): HP({enemy.HealthPoint}), AP({enemy.AttackPower})\n");
            
            while (player.IsAlive && enemy.IsAlive)
            {
                string action = GetPlayerAction();
                
                if (action == "4") 
                {
                    Console.WriteLine($"{player.Name} ran away from the battle!");
                    return new BattleResult { PlayerRanAway = true };
                }
                
                ProcessPlayerAction(player, enemy, action);
                
                if (enemy.HealthPoint <= 0)
                {
                    enemy.IsAlive = false;
                    Console.WriteLine($"{enemy.Name} has been defeated!");
                    return new BattleResult { PlayerWon = true };
                }
                
                if (enemy.IsAlive)
                {
                    EnemyTurn(enemy, player);

                    if (player.HealthPoint <= 0)
                    {
                        player.IsAlive = false;
                        Console.WriteLine($"{player.Name} has been defeated...");
                        return new BattleResult { PlayerWon = false };
                    }
                }
                
                ShowBattleStatus(player, enemy);
            }
            
            return new BattleResult { PlayerWon = player.IsAlive };
        }
        
		void EnemyTurn(BaseGoblin enemy, Player player)
		{
            Console.WriteLine("It is the Enemy's Turn!");
			if (enemy.HealthPoint < 30 && rand.Next(100) < 40)
            {
                enemy.Guard();
                Console.WriteLine($"{enemy.Name} takes a defensive stance!");
            }
            else if (rand.Next(100) < 80)
            {
                enemy.Attack(player);
            }
            else
            {
                Console.WriteLine($"{enemy.Name} hesitates...");
            }
		}

        string GetPlayerAction()
        {
            Console.WriteLine("Choose your action:");
            Console.WriteLine("[1] Attack");
            Console.WriteLine("[2] Guard");
            Console.WriteLine("[3] Heal");
            Console.WriteLine("[4] Run Away");
            return Console.ReadLine();
        }

        void ProcessPlayerAction(Player player, BaseGoblin enemy, string action)
        {
            switch (action)
            {
                case "1":
                    player.Attack(enemy);
                    break;
                case "2":
                    player.Guard();
                    break;
                case "3":
                    player.Heal();
                    break;
                default:
                    Console.WriteLine("[!] Invalid action! You hesitate...");
                    break;
            }
        }

        void ShowBattleStatus(Player player, BaseGoblin enemy)
        {
            if (player.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine($"\nStatus: {player.Name} HP({player.HealthPoint}) vs {enemy.Name} HP({enemy.HealthPoint})\n");
            }
        }

        bool AskContinue(Player player, int enemiesDefeated)
        {
            Console.WriteLine($"\nBattles won: {enemiesDefeated}");
            Console.WriteLine("Do you want to continue your adventure?");
            Console.WriteLine("[1] Continue fighting");
            Console.WriteLine("[2] Rest and quit");
            
            string continueChoice = Console.ReadLine();
            if (continueChoice == "2")
            {
                Console.WriteLine($"\n{player.Name} decides to rest after {enemiesDefeated} battles.");
                Console.WriteLine($"Final stats: HP({player.HealthPoint}), Gold({player.Gold})");
                Console.WriteLine("Thanks for playing Battler!");
                return false;
            }
            else if (continueChoice != "1")
            {
                Console.WriteLine("[!] Invalid choice, continuing adventure...");
            }
            return true;
        }

        bool AskRetry(Player player, int enemiesDefeated)
        {
            Console.WriteLine("Do you want to try fighting again?");
            Console.WriteLine("[1] Fight another enemy");
            Console.WriteLine("[2] Quit game");
            
            string runChoice = Console.ReadLine();
            if (runChoice == "2")
            {
                Console.WriteLine($"\n{player.Name} decides to quit after {enemiesDefeated} battles.");
                Console.WriteLine($"Final stats: HP({player.HealthPoint}), Gold({player.Gold})");
                Console.WriteLine("Thanks for playing Battler!");
                return false;
            }
            return true;
        }

        void ShowGameOverScreen(Player player, int enemiesDefeated)
        {
            if (!player.IsAlive)
            {
                Console.WriteLine($"\n==== GAME OVER ====");
                Console.WriteLine($"{player.Name} fought valiantly but was defeated.");
                Console.WriteLine($"Battles won: {enemiesDefeated}");
                Console.WriteLine($"Gold collected: {player.Gold}");
                Console.WriteLine("Better luck next time!");
            }
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
            Player player = new Player() {HealthPoint = 100, AttackPower = 7, Name = "Slayer", Gold = 0};
            BaseGoblin enemy = new BaseGoblin() {HealthPoint = 100, AttackPower = 7, Name = "Goblin Rook", LootAmount = 10};

            Console.WriteLine($"\n==== Welcome to Battler ====\n" +
                    $"Player ({player.Name}): HP({player.HealthPoint}), AP({player.AttackPower})\n" +
                    $"Enemy ({enemy.Name}: HP({enemy.HealthPoint}), AP({enemy.AttackPower})\n");

            player.Attack(enemy);
            Console.WriteLine($"Looks like Player attacked Enemy which dealt {player.AttackPower} and left the enemy HP to {enemy.HealthPoint}\n");
        }
    }
}