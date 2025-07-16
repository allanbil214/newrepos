using System;

namespace Classes
{
    public interface ICombatant
    {
        void Attack(ICombatant combatant);
        void Guard(ICombatant combatant);
        void Heal();
        bool IsAlive { get; }
        int HealthPoint{ get; set; }
        int AttackPower();
        int DealthDamage();
        string Name { get;}
    }

    public class Warrior : ICombatant
    {
        bool isAlive = true;
        int healthPoint = 100;
        readonly int attackPower = 7;
        readonly string name = "Warrior";

        public bool IsAlive => isAlive;
        public int HealthPoint { get => healthPoint; set => healthPoint = value; }
        public int AttackPower() => attackPower;
        public int DealthDamage() => 0;
        public string Name => name;

        public void Attack(ICombatant combatant)
        {
            combatant.HealthPoint -= attackPower;
            
            Console.WriteLine($"You attacked {combatant.Name} and dealth {attackPower} Damage");
        }

        public void Guard(ICombatant combatant)
        {
            healthPoint -= attackPower / 4;
            Console.WriteLine($"You guarded attack by {combatant.Name} and left {healthPoint} HP");
        }

        public void Heal()
        {
            int healPower = attackPower * 2;
            healthPoint += healPower;
            Console.WriteLine($"You healed yourself by {healPower}");
        }

    }
}