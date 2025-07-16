using System;
using Classes;
namespace TurnBasedBattle
{
    class Game
    {
        public static void Main(string[] args)
        {
            var game = new Game();

            Warrior player = new Warrior();
            Warrior enemy = new Warrior();

            Console.WriteLine($@"\n==== Welcome to Battler ====
Player ({player.Name}): HP({player.HealthPoint}), AP({player.AttackPower()})
Enemy ({enemy.Name}: HP({enemy.HealthPoint}), AP({enemy.AttackPower()})");

            player.Attack(enemy);
            Console.WriteLine($"Looks like Player attacked Enemy which dealt {player.AttackPower()} and left the enemy HP to {enemy.HealthPoint}");
        }


    }

}