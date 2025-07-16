using System;

namespace TurnBasedBattle
{
    public class Warrior : Entity, IPlayer
    {   
        public bool IsAlive => isAlive;
        public int HealthPoint { get => healthPoint; set => healthPoint = value; }
        public int AttackPower { get => attackPower; set => attackPower = value; }
        public string Name => name;

        public void Attack(ICombatant combatant)
        {
            combatant.HealthPoint -= attackPower;
            Console.WriteLine($"You attacked {combatant.Name} and dealth {attackPower} Damage");
        }

        public void Guard(ICombatant combatant)
        {
            healthPoint -= combatant.AttackPower / 4;
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