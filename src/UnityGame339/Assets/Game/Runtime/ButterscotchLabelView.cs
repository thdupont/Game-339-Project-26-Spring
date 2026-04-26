using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class ButterscotchLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private static CookieIngredientInventory Inventory =>
            ServiceResolver.Resolve<CookieIngredientInventory>();

        protected override void Subscribe() =>
            Inventory.Butterscotch.ChangeEvent += OnChange;

        protected override void Unsubscribe() =>
            Inventory.Butterscotch.ChangeEvent -= OnChange;

        private void OnChange(int value) => label.text = "Butterscotch: " + value;
    }
}
