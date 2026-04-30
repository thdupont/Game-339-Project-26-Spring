using Game.Runtime;
using Game339.Shared;
using UnityEngine;

public class LoseController : MonoBehaviour
{
    [SerializeField] private string _playerCharacter;
    [SerializeField] private string _enemyCharacter;
    
    private Character _player;
    private Character _enemy;
    private TurnEngine _turnEngine;
    
    public ObservableValue<bool> IsLoseShowing { private set; get; } = new ObservableValue<bool>();
    
    private void Awake()
    {
        _player = ServiceResolver.Resolve<CharacterManager>().Get(_playerCharacter);
        _enemy = ServiceResolver.Resolve<CharacterManager>().Get(_enemyCharacter);
        _turnEngine = ServiceResolver.Resolve<TurnEngine>();
        
        _turnEngine.EncounterEnd += OnEncounterEnd;
    }
    
    private void OnDestroy()
    {
        if (_turnEngine != null) _turnEngine.EncounterEnd -= OnEncounterEnd;
    }
    
    private void OnEncounterEnd(bool playerWon)
    {
        if (playerWon) return;
        IsLoseShowing.Value = true;
    }
    
    // called by LoseView button
    public void Restart()
    {
        if (!IsLoseShowing.Value) return;
        _player.ResetValues();
        _enemy.ResetValues();
        PlayerController.Instance.ResetHealPotency();
        IsLoseShowing.Value = false;
        _turnEngine.EnterEncounter();
        _turnEngine.StartPlayerTurn();
    }
    
    // called by LoseView button
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}