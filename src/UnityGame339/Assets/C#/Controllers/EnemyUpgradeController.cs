using Game.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUpgradeController : MonoBehaviour
{
    [SerializeField] private string _enemyCharacter;
    [SerializeField] private int _attackUpgradeAmount = 1;
    [SerializeField] private int _maxHPUpgradeAmount = 1;
    
    private Character _enemy;
    private TurnEngine _turnEngine;
    private AttackService _attackService;

    private void Awake()
    {
        _enemy = ServiceResolver.Resolve<CharacterManager>().Get(_enemyCharacter);
        _turnEngine = ServiceResolver.Resolve<TurnEngine>();
        _attackService = ServiceResolver.Resolve<AttackService>();
        
        _turnEngine.EncounterEnd += UpgradeEnemy;
        _turnEngine.EncounterStart += HealEnemy;
    }

    private void OnDestroy()
    {
        _turnEngine.EncounterEnd -= UpgradeEnemy;
        _turnEngine.EncounterStart -= HealEnemy;
    }

    public void ResetEnemy()
    {
        _enemy.ResetValues();
    }

    private void UpgradeEnemy(bool isPlayerWin)
    {
        if (!isPlayerWin) return;
        
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                _enemy.Attack.Value += _attackUpgradeAmount;
                Debug.Log($"enemy upgraded attack by {_attackUpgradeAmount}");
                break;
            case 1:
                _enemy.MaxHP.Value += _maxHPUpgradeAmount;
                Debug.Log($"enemy upgraded max health by {_maxHPUpgradeAmount}");
                break;
        }
    }

    private void HealEnemy()
    {
        _attackService.HealToFull(_enemy);
    }
}