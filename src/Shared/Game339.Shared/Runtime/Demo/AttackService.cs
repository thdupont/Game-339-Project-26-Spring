using Game339.Shared.Diagnostics;

namespace Game.Runtime
{
    public class AttackService
    {
        /// <summary>
        /// Attacker takes away Target health 
        /// </summary>
        /// <param name="attacker">attacking</param>
        /// <param name="target">getting attack</param>
        /// <returns>TRUE if target is dead</returns>
        public bool Attack(Character attacker, Character target)
        {
            int dmg = attacker.Attack.Value - target.Defense.Value;
            int remainingHealth = target.HP.Value - dmg;
            if (remainingHealth <= 0)
            {
                target.HP.Value = 0;
                return true;
            }

            target.HP.Value = remainingHealth;
            return false;
        }

        public void Heal(Character healer)
        {
            healer.HP.Value += 2;
        }
    }
}