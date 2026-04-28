using System.Collections.Generic;
using Game339.Shared.Infrastructure;

namespace Game339.Shared.Cookie.Models
{
    public class CookieIngredientInventory
    {
        public Dictionary<CookieIngredient, ObservableValue<int>> Counts { get; }

        public CookieIngredientInventory(Dictionary<CookieIngredient, int> initialCounts)
        {
            Counts = new Dictionary<CookieIngredient, ObservableValue<int>>();
            foreach (var kvp in initialCounts)
                Counts[kvp.Key] = new ObservableValue<int>(kvp.Value);
        }

        public int GetCount(CookieIngredient ingredient) =>
            Counts.TryGetValue(ingredient, out var obs) ? obs.Value : 0;
    }
}
