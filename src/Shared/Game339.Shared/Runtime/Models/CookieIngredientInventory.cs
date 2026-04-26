using System.Collections.Generic;

namespace Game339.Shared.Models
{
    public class CookieIngredientInventory
    {
        public Dictionary<CookieIngredient, int> Counts { get; }

        public CookieIngredientInventory(Dictionary<CookieIngredient, int> initialCounts)
        {
            Counts = new Dictionary<CookieIngredient, int>(initialCounts);
        }

        public int GetCount(CookieIngredient ingredient)
        {
            return Counts.TryGetValue(ingredient, out int count) ? count : 0;
        }
    }
}
