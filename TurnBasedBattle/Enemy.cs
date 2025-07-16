using System;

namespace TurnBasedBattle
{
    public class Goblin : Entity, ICombatant
    {
        public int amount = 0;
        public void Loot(IPlayer player)
        {
            player.Gold += amount;
            Console.WriteLine($"{name} dropped {amount} of Gold. {name} Decided to pick it up.");
        }
    }
}