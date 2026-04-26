using System.Collections.Generic;
using Game339.Shared;

namespace Game339.Shared.Models
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

        public ObservableValue<int> Chocolate    => Counts[CookieIngredient.Chocolate];
        public ObservableValue<int> Nuts         => Counts[CookieIngredient.Nuts];
        public ObservableValue<int> PeanutButter => Counts[CookieIngredient.PeanutButter];
        public ObservableValue<int> Butterscotch => Counts[CookieIngredient.Butterscotch];
        public ObservableValue<int> Sugar        => Counts[CookieIngredient.Sugar];
    }
}
