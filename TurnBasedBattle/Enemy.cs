using System;

namespace TurnBasedBattle
{
    public class BaseGoblin : Entity, ICombatant
    {
        private int lootAmount = 0;
        public int LootAmount { get => lootAmount; set => lootAmount = value; }
        public void Loot(IPlayer player)
        {
            player.Gold += LootAmount;
            Console.WriteLine($"{Name} dropped {LootAmount} of Gold. {player.Name} Decided to pick it up.");
        }
    }

    // public class GoblinRook : BaseGoblin
    // {
    //     Random rand = new Random();
    //     public override void Attack(ICombatant target)
    //     {
    //         int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));

    //         int defense = target.InnateDefense;
    //         if (target.IsGuarding)
    //         {
    //             defense += 1;
    //             Console.WriteLine($"{target.Name} blocks some of the attack! But rather ineffectively.");
    //             target.IsGuarding = false;
    //         }

    //         int damage = Math.Max(1, actualAP - defense);
    //         target.HealthPoint -= damage;
    //         Console.WriteLine($"{Name} attacked {target.Name} and dealth {damage} Damage");
    //     }
    // }
}