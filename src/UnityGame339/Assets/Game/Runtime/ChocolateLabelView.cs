using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class ChocolateLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private static CookieIngredientInventory Inventory =>
            ServiceResolver.Resolve<CookieIngredientInventory>();

        protected override void Subscribe() =>
            Inventory.Chocolate.ChangeEvent += OnChange;

        protected override void Unsubscribe() =>
            Inventory.Chocolate.ChangeEvent -= OnChange;

        private void OnChange(int value) => label.text = "Chocolate: " + value;
    }
}
