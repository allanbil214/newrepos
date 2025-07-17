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

    public class GoblinRook : BaseGoblin
    {
        Random rand = new Random();
        public override void SpecialAttack(ICombatant target)
        {
            Console.WriteLine($"{Name} is using Special Attack: Berserker Attack at {target.Name}");

            int actualAP = rand.Next(AttackPower * 2, (int)(AttackPower * AttackPowerMult));

            int defense = target.InnateDefense;
            if (target.IsGuarding)
            {
                defense += 1;
                Console.WriteLine($"{target.Name} blocks some of the special attack! But rather ineffectively.");
                target.IsGuarding = false;
            }

            int damage = Math.Max(1, actualAP - defense);
            target.HealthPoint -= damage;
            Console.WriteLine($"{Name} attacked angrily at {target.Name} and dealth {damage} Damage");
        }
    }
    public class GoblinShaman : BaseGoblin
    {
        Random rand = new Random();
        public override void SpecialAttack(ICombatant target)
        {
            Console.WriteLine($"{Name} is using Special Attack: Beam Magic at {target.Name}");

            int actualAP = rand.Next(AttackPower * 2, (int)(AttackPower * AttackPowerMult));

            int defense = target.InnateDefense;
            if (target.IsGuarding)
            {
                defense -= 3;
                Console.WriteLine($"{target.Name} is guard broken, and can't block effectively.");
                target.IsGuarding = false;
            }

            int damage = Math.Max(1, actualAP - defense);
            target.HealthPoint -= damage;
            Console.WriteLine($"{Name} beamed at {target.Name} and dealth {damage} Damage");
        }
    }

    public class GoblinSwordsman : BaseGoblin
    {
        Random rand = new Random();
        public override void SpecialAttack(ICombatant target)
        {
            Console.WriteLine($"{Name} is using Special Attack: Double Slash at {target.Name}");

            int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));

            int defense = target.InnateDefense;
            if (target.IsGuarding)
            {
                defense += 3;
                Console.WriteLine($"{target.Name} blocks some of the attack.");
                target.IsGuarding = false;
            }

            int firstHit = Math.Max(1, actualAP - defense);
            target.HealthPoint -= firstHit;
            Console.WriteLine($"{Name} attacked the first slash at {target.Name} and dealth {firstHit} Damage");
            
            if (rand.Next(100) < 25)
            {
                int secondHit = Math.Max(1, actualAP - defense);
                target.HealthPoint -= (int)(secondHit/2);
                Console.WriteLine($"{Name} attacked the first slash at {target.Name} and dealth {secondHit} Damage");
            }
        }
    }
}