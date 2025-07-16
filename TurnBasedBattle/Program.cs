using System;

namespace TurnBasedBattle
{
    class Game
    {
        public static void Main(string[] args)
        {
            var game = new Game();

            game.Testing();
        }

        void Testing()
        {
            Warrior player = new Warrior() {healthPoint = 100, attackPower = 7, name = "Warrior"};
            Goblin enemy = new Goblin();

            Console.WriteLine($"\n==== Welcome to Battler ====\n" +
                    $"Player ({player.Name}): HP({player.HealthPoint}), AP({player.AttackPower})\n" +
                    $"Enemy ({enemy.Name}: HP({enemy.HealthPoint}), AP({enemy.AttackPower})\n");

            player.Attack(enemy);
            Console.WriteLine($"Looks like Player attacked Enemy which dealt {player.AttackPower} and left the enemy HP to {enemy.HealthPoint}\n");
        }

    }

}