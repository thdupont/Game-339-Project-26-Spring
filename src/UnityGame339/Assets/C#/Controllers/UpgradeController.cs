using Game.Runtime;
using Game339.Shared;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private string _playerCharacter;
    [SerializeField] private int _attackUpgradeAmount = 1;
    [SerializeField] private int _healPotencyUpgradeAmount = 1;

    private Character _player;
    private TurnEngine _turnEngine;
    private AttackService _attackService;

    public ObservableValue<bool> IsUpgradeAvailable { private set; get; } = new ObservableValue<bool>();

    private void Awake()
    {
        _player = ServiceResolver.Resolve<CharacterManager>().Get(_playerCharacter);
        _turnEngine = ServiceResolver.Resolve<TurnEngine>();
        _attackService = ServiceResolver.Resolve<AttackService>();

        _turnEngine.EncounterEnd += OnEncounterEnd;
    }

    private void OnDestroy()
    {
        if (_turnEngine != null) _turnEngine.EncounterEnd -= OnEncounterEnd;
    }

    private void OnEncounterEnd(bool playerWon)
    {
        if (!playerWon) return;
        IsUpgradeAvailable.Value = true;
    }

    // called by UpgradeView button
    public void UpgradeAttack()
    {
        if (!IsUpgradeAvailable.Value) return;
        _player.Attack.Value += _attackUpgradeAmount;
        StartNextEncounter();
    }

    // called by UpgradeView button
    public void HealToFull()
    {
        if (!IsUpgradeAvailable.Value) return;
        _attackService.HealToFull(_player);
        StartNextEncounter();
    }

    // called by UpgradeView button
    public void UpgradeHealPotency()
    {
        if (!IsUpgradeAvailable.Value) return;
        PlayerController.Instance.UpgradeHealPotency(_healPotencyUpgradeAmount);
        StartNextEncounter();
    }

    private void StartNextEncounter()
    {
        IsUpgradeAvailable.Value = false;
        _turnEngine.EnterEncounter();
        _turnEngine.StartPlayerTurn();
    }
}