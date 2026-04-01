using Game339.Shared.Diagnostics;

namespace Game.Runtime
{
    public class AttackService
    {
        private readonly IGameLog _logger;

        public AttackService(IGameLog logger)
        {
            _logger = logger;
        }

        public void Attack(Character attacker, Character target)
        {
            var dmg = attacker.Attack.Value - target.Defense.Value;
            target.Hp.Value -= dmg;
            _logger.Info($"{attacker.Id} does {dmg} dmg to {target.Id}");
        }
    }
}