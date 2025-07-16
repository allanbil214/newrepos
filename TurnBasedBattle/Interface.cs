using System;

namespace TurnBasedBattle
{
    public interface ICombatant
    {
        void Attack(ICombatant combatant);
        void Guard(ICombatant combatant);

        bool IsAlive { get; }
        int HealthPoint { get; set; }
        int AttackPower { get; set; }
        string Name { get; }
    }
    public interface IPlayer : ICombatant
    {
        void Heal();
    }
}