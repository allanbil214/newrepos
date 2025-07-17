using System;

namespace TurnBasedBattle
{
    public class BasePlayer : Entity, IPlayer
    {
        Random rand = new Random();
        private int gold = 0;
        private bool isHealed = false;

        public bool IsHealed { get => isHealed; set => isHealed = value; }
        public int Gold { get => gold; set => gold = value; }
        public virtual void Heal()
        {
            if (!IsHealed)
            {
                int healPower = rand.Next(AttackPower, AttackPower * 2);
                HealthPoint += healPower;
                Console.WriteLine($"{Name} healed yourself by {healPower} using Potion");
                isHealed = true;
            }
            else
            {
                Console.WriteLine("Healing is already used up!");
            }
        }
    }

    public class Warrior : BasePlayer
    {
        Random rand = new Random();

        public override void SpecialAttack(ICombatant target)
        {
            if (!IsSpecialed)
            {
                Console.WriteLine($"{Name} is using Special Attack: Berserker Attack at {target.Name}");

                int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));

                int defense = target.InnateDefense;
                if (target.IsGuarding)
                {
                    defense += 1;
                    Console.WriteLine($"{target.Name} blocks some of the special attack! But rather ineffectively.");
                    target.IsGuarding = false;
                }

                int damage = Math.Max(1, actualAP * 2 - defense);
                target.HealthPoint -= damage;
                Console.WriteLine($"{Name} attacked angrily at {target.Name} and dealth {damage} Damage");

                IsSpecialed = true;
            }
            else
            {
                Console.WriteLine("Special Attack is already used up!");
            }
        }
    }
    public class Mage : BasePlayer
    {
        Random rand = new Random();
        public override void Heal()
        {
            if (!IsHealed)
            {
                int healPower = rand.Next(AttackPower, AttackPower * 3);
                HealthPoint += healPower;
                Console.WriteLine($"{Name} healed yourself by {healPower} using Magic effectively");
                IsHealed = true;
            }
            else
            {
                Console.WriteLine("Healing is already used up!");
            }
        }

        public override void SpecialAttack(ICombatant target)
        {
            if (!IsSpecialed)
            {
                Console.WriteLine($"{Name} is using Special Attack: Beam Magic at {target.Name}");

                int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));

                int defense = target.InnateDefense;
                if (target.IsGuarding)
                {
                    defense -= 3;
                    Console.WriteLine($"{target.Name} is guard broken, and can't block effectively.");
                    target.IsGuarding = false;
                }

                int damage = Math.Max(1, actualAP * 2 - defense);
                target.HealthPoint -= damage;
                Console.WriteLine($"{Name} beamed at {target.Name} and dealth {damage} Damage");
                IsSpecialed = true;
            }
            else
            {
                Console.WriteLine("Special Attack is already used up!");
            }
        }
    }

    public class Assassin : BasePlayer
    {
        Random rand = new Random();
        public override void SpecialAttack(ICombatant target)
        {
            if (!IsSpecialed)
            {
                Console.WriteLine($"{Name} is using Special Attack: Assasinate at {target.Name}");

                int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));
                int defense = target.InnateDefense;
                int chance = 1;

                if (target.IsGuarding)
                {
                    defense += 3;
                    Console.WriteLine($"{target.Name} blocks some of the attack.");
                    target.IsGuarding = false;
                }

                if (rand.Next(100) < 50)
                {
                    actualAP += actualAP * 2;
                    chance = actualAP;
                    Console.WriteLine($"Lookslike {Name} will be able to assassinate {target.Name} and deal a lot of damage!");
                }
                else Console.WriteLine("Failed to Assasinate!");

                int firstHit = Math.Max(chance, actualAP);
                target.HealthPoint -= firstHit;
                Console.WriteLine($"{Name} attacked at {target.Name} pierced trough their defense, and dealth {firstHit} Damage");
                IsSpecialed = true;
                }
            else
            {
                Console.WriteLine("Special Attack is already used up!");
            }
            
        }
    }

}