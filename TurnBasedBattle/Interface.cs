using System;

namespace TurnBasedBattle
{
    public interface ICombatant
    {
        void Attack(ICombatant combatant);
        void Guard();
        void SpecialAttack(ICombatant combatant);

        bool IsSpecialed { get; set; }
        bool IsAlive { get; set; }
        bool IsGuarding { get; set; }
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
        void Loot(IPlayer player);
    }
}