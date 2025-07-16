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
        int AttackPowerMult { get; set; }
        int InnateDefense { get; set; }
        string Name { get; }
    }
    public interface IPlayer : ICombatant
    {
        int Gold {get; set;}
        void Heal();
    }

    public interface IEnemy : ICombatant
    {
        void Loot();
    }
}