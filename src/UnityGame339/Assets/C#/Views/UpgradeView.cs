using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _upgradeAttackButton;
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _upgradeHealButton;
    [SerializeField] private UpgradeController _controller;

    private void Awake()
    {
        _controller.IsUpgradeAvailable.ChangeEvent += SetVisible;
    }

    private void OnDestroy()
    {
        if (_controller != null) _controller.IsUpgradeAvailable.ChangeEvent -= SetVisible;
    }

    private void Start()
    {
        _upgradeAttackButton.onClick.AddListener(_controller.UpgradeAttack);
        _healButton.onClick.AddListener(_controller.HealToFull);
        _upgradeHealButton.onClick.AddListener(_controller.UpgradeHealPotency);
        _panel.SetActive(false);
    }

    private void SetVisible(bool value)
    {
        _panel.SetActive(value);
    }
}