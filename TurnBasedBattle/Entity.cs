using System;

namespace TurnBasedBattle
{
    public class Entity : ICombatant
    {

        public int healthPoint = 50;
        public int attackPower = 3;
        public int attackPowerMult = 1;
        public string name = "Entity";
        public bool isAlive = true;

        public bool IsAlive => isAlive;
        public int HealthPoint { get => healthPoint; set => healthPoint = value; }
        public int AttackPower { get => attackPower; set => attackPower = value; }
        public int AttackPowerMult { get => attackPowerMult; set => attackPowerMult = value; }
        public string Name => name;

        public void Attack(ICombatant combatant)
        {
            Random rand = new Random();
            int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));        

            combatant.HealthPoint -= actualAP;
            Console.WriteLine($"{name} attacked {combatant.Name} and dealth {actualAP} Damage");
        }

        public void Guard(ICombatant combatant)
        {
            HealthPoint -= (int)(combatant.AttackPower / 4);
            Console.WriteLine($"{name} guarded attack by {combatant.Name} and left {HealthPoint} HP");
        }
    }


}