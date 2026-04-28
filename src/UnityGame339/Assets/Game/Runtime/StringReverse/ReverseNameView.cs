using Game339.Shared.StringReverse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class ReverseNameView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text nameLabel;

        private void Start() => button.onClick.AddListener(OnClick);
        private void OnDestroy() => button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            var svc = ServiceResolver.Resolve<IStringService>();
            nameLabel.text = svc.Reverse(nameLabel.text);
        }
    }
}
