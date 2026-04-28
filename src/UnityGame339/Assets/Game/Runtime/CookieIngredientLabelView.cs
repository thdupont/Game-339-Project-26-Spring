using System;
using Game339.Shared.Models;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class CookieIngredientLabelView : ObserverMonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private string ingredientName;

        private CookieIngredient _ingredient;

        private static CookieIngredientInventory Inventory
        {
            get { return ServiceResolver.Resolve<CookieIngredientInventory>(); }
        }

        protected override void Subscribe()
        {
            _ingredient = (CookieIngredient)Enum.Parse(typeof(CookieIngredient), ingredientName);
            Inventory.Counts[_ingredient].ChangeEvent += OnChange;
        }

        protected override void Unsubscribe()
        {
            Inventory.Counts[_ingredient].ChangeEvent -= OnChange;
        }

        private void OnChange(int value)
        {
            label.text = ingredientName + ": " + value;
        }
    }
}
