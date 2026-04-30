using System;
using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerView : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _healButton;
    
    private void Awake()
    {
        ServiceResolver.Resolve<TurnEngine>().IsPlayerTurn.ChangeEvent += SetInteractable;
    }

    private void OnDestroy()
    {
        ServiceResolver.Resolve<TurnEngine>().IsPlayerTurn.ChangeEvent -= SetInteractable;
    }

    private void Start()
    {
        _attackButton.onClick.AddListener(PlayerController.Instance.PlayerAttack);
        _healButton.onClick.AddListener(PlayerController.Instance.Heal);
    }

    private void SetInteractable(bool value)
    {
        _attackButton.interactable = value;
        _healButton.interactable = value;
    }
}
