using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class NutsLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private static CookieIngredientInventory Inventory =>
            ServiceResolver.Resolve<CookieIngredientInventory>();

        protected override void Subscribe() =>
            Inventory.Nuts.ChangeEvent += OnChange;

        protected override void Unsubscribe() =>
            Inventory.Nuts.ChangeEvent -= OnChange;

        private void OnChange(int value) => label.text = "Nuts: " + value;
    }
}
