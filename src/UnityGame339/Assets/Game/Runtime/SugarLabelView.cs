using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class SugarLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        private static CookieIngredientInventory Inventory =>
            ServiceResolver.Resolve<CookieIngredientInventory>();

        protected override void Subscribe() =>
            Inventory.Sugar.ChangeEvent += OnChange;

        protected override void Unsubscribe() =>
            Inventory.Sugar.ChangeEvent -= OnChange;

        private void OnChange(int value) => label.text = "Sugar: " + value;
    }
}
