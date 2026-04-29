using System;
using Game339.Shared;
using Game339.Shared.DependencyInjection;
using Game339.Shared.DependencyInjection.Implementation;
using Game339.Shared.Diagnostics;
using Game339.Shared.Services;

namespace Game.Runtime
{
    public static class ServiceResolver
    {
        public static T Resolve<T>() => Container.Value.Resolve<T>();

        private static readonly Lazy<IMiniContainer> Container = new Lazy<IMiniContainer>(() =>
        {
            MiniContainer container = new MiniContainer();

            UnityGameLogger logger = new UnityGameLogger();
            container.RegisterSingletonInstance<IGameLog>(logger);
            
            DamageService damageService = new DamageService();
            container.RegisterSingletonInstance<IDamageService>(damageService);
            
            AttackService attackService = new AttackService();
            container.RegisterSingletonInstance(attackService);

            TurnEngine turnEngine = new TurnEngine();
            container.RegisterSingletonInstance(turnEngine);
            
            var characterManager = new CharacterManager();
            characterManager.Add(new Character("player", "Player", 10, 3, 1));
            characterManager.Add(new Character("enemy", "Enemy", 10, 2, 1));
            container.RegisterSingletonInstance(characterManager);

            return container;
        });
    }
}

/*
 * var gameState = new GameState();
   gameState.GoodGuy.Name.Value = "Good Sandy";
   gameState.GoodGuy.Health.Value = 10;
   gameState.GoodGuy.Damage.Value = 1;
   gameState.BadGuy.Name.Value = "Bad Sandy";
   gameState.BadGuy.Health.Value = 10;
   gameState.BadGuy.Damage.Value = 1;
   container.RegisterSingletonInstance(gameState);

   var damageService = new DamageService(logger);
   container.RegisterSingletonInstance<IDamageService>(damageService);
   
   var stringService = new StringService(logger);
   container.RegisterSingletonInstance<IStringService>(stringService);

   var attackService = new AttackService(logger);
   container.RegisterSingletonInstance(attackService);

   var characterManager = new CharacterManager(logger);
   characterManager.Add(new Character("sandy", "Sandy, the Corgi", 10, 3, 1));
   characterManager.Add(new Character("squirrel", "Evil Squirrel", 5, 2, 1));
   container.RegisterSingletonInstance(characterManager);
 */
