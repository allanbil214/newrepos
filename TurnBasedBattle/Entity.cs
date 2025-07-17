using System;

namespace TurnBasedBattle
{

    public class Entity : ICombatant
    {
        Random rand = new Random();
        private int healthPoint = 50;
        private int attackPower = 3;
        private int attackPowerMult = 1;
        private int innateDefense = 1;
        private string name = "Entity";
        private bool isAlive = true;
        private bool isGuarding = false;

        public bool IsGuarding { get => isGuarding; set => isGuarding = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public int HealthPoint { get => healthPoint; set => healthPoint = value; }
        public int AttackPower { get => attackPower; set => attackPower = value; }
        public int AttackPowerMult { get => attackPowerMult; set => attackPowerMult = value; }
        public int InnateDefense { get => innateDefense; set => innateDefense = value; }
        public string Name { get => name; set => name = value; }

        public void Attack(ICombatant target)
        {
            int actualAP = rand.Next(AttackPower, (int)(AttackPower * AttackPowerMult));

            int defense = target.InnateDefense;
            if (target.IsGuarding)
            {
                defense += 3;
                Console.WriteLine($"{target.Name} blocks some of the attack!");
                target.IsGuarding = false;
            }

            int damage = Math.Max(1, actualAP - defense);
            target.HealthPoint -= damage;
            Console.WriteLine($"{Name} attacked {target.Name} and dealth {damage} Damage");
        }

        public void Guard()
        {
            IsGuarding = true;
            Console.WriteLine($"{Name} raises their guard!");
        }

        public virtual void SpecialAttack(ICombatant target)
        {
            Console.WriteLine("Entity Special Attack!!");
        }
    }
}