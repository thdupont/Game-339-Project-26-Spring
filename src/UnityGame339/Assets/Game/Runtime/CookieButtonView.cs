using Game339.Shared.Diagnostics;
using Game339.Shared.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class CookieButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string cookieName;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            var svc = ServiceResolver.Resolve<ICookieService>();
            var log = ServiceResolver.Resolve<IGameLog>();
            bool ok = svc.TryMakeCookie(cookieName);
            log.Info(ok ? $"Made {cookieName}!" : $"Missing ingredients for {cookieName}.");
        }
    }
}
