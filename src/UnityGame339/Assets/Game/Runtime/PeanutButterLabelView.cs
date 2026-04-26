using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class PeanutButterLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private static CookieIngredientInventory Inventory =>
            ServiceResolver.Resolve<CookieIngredientInventory>();

        protected override void Subscribe() =>
            Inventory.PeanutButter.ChangeEvent += OnChange;

        protected override void Unsubscribe() =>
            Inventory.PeanutButter.ChangeEvent -= OnChange;

        private void OnChange(int value) => label.text = "PeanutButter: " + value;
    }
}
