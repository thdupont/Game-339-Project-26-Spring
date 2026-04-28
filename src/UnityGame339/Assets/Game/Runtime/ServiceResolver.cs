using System;
using System.Collections.Generic;
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

            var stringService = new StringService(logger);
            container.RegisterSingletonInstance<IStringService>(stringService);

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
