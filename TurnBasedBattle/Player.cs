using System;

namespace TurnBasedBattle
{
    public class Player : Entity, IPlayer
    {
        private int gold = 0;
        public int Gold { get => gold;  set => gold = value; }
        public void Heal()
        {
            int healPower = (int)(AttackPower / 2);
            HealthPoint += healPower;
            Console.WriteLine($"{Name} healed yourself by {healPower}");
        }
    }
}