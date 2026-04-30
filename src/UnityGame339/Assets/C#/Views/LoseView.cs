using UnityEngine;
using UnityEngine.UI;

public class LoseView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private LoseController _controller;

    private void Awake()
    {
        _controller.IsLoseShowing.ChangeEvent += SetVisible;
    }

    private void OnDestroy()
    {
        if (_controller != null) _controller.IsLoseShowing.ChangeEvent -= SetVisible;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(_controller.Restart);
        _quitButton.onClick.AddListener(_controller.Quit);
        _panel.SetActive(false);
    }

    private void SetVisible(bool value)
    {
        _panel.SetActive(value);
    }
}
