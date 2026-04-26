using System;
using System.Collections.Generic;
using Game339.Shared;
using Game339.Shared.DependencyInjection;
using Game339.Shared.DependencyInjection.Implementation;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Game339.Shared.Services;
using Game339.Shared.Services.Implementation;

namespace Game.Runtime
{
    public static class ServiceResolver
    {
        public static T Resolve<T>() => Container.Value.Resolve<T>();

        private static readonly Lazy<IMiniContainer> Container = new (() =>
        {
            var container = new MiniContainer();

            var logger = new UnityGameLogger();
            container.RegisterSingletonInstance<IGameLog>(logger);

            var gameState = new GameState();
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

            var cookieInventory = new CookieIngredientInventory(new Dictionary<CookieIngredient, int>
            {
                { CookieIngredient.Chocolate,    5 },
                { CookieIngredient.Nuts,         5 },
                { CookieIngredient.PeanutButter, 5 },
                { CookieIngredient.Butterscotch, 5 },
                { CookieIngredient.Sugar,        5 },
            });
            container.RegisterSingletonInstance(cookieInventory);

            var cookieService = new CookieService(cookieInventory);
            container.RegisterSingletonInstance<ICookieService>(cookieService);

            return container;
        });
    }
}
