using System;
using Game.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [SerializeField] private string _playerCharacter;
    [SerializeField] private string _enemyCharacter;
    
    private Character _player;
    private Character _enemy;
    private AttackService _attackService;
    private TurnEngine _turnEngine;
    
    public event Action<int> PlayerTakeDamage;
    public event Action<int> EnemyTakeDamage;
    public event Action PlayerHeal;
    public event Action EnemyHeal;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        _player = ServiceResolver.Resolve<CharacterManager>().Get(_playerCharacter);
        _enemy = ServiceResolver.Resolve<CharacterManager>().Get(_enemyCharacter);
        _attackService = ServiceResolver.Resolve<AttackService>();
        _turnEngine = ServiceResolver.Resolve<TurnEngine>();

        //these will be moved to a new class
        _turnEngine.EncounterEnd += UpgradeEnemy;
        _turnEngine.EncounterStart += HealEnemy;
    }

    private void OnDestroy()
    {
        _turnEngine.EncounterEnd -= UpgradeEnemy;
        _turnEngine.EncounterStart -= HealEnemy;
    }

    private void Start()
    {
        _turnEngine.EnterEncounter(); //change this later
        
        //TODO -- pick who goes first
        _turnEngine.StartPlayerTurn(); //debug player goes first
    }

    //===== Character Abilities =====
    public void PlayerAttack()
    {
        if (Attack(_player, _enemy)) _turnEngine.EndPlayerTurn();
    }

    public void EnemyAttack()
    {
        if (Attack(_enemy, _player)) _turnEngine.StartPlayerTurn();
    }
    
    private bool Attack(Character attacker, Character target)
    {
        bool isTargetDead = _attackService.Attack(attacker, target);
        if (isTargetDead)
        {
            bool isPlayerAttacker = attacker == _player;
            _turnEngine.ExitEncounter(isPlayerAttacker);
        }
        return !isTargetDead;
    }

    public void Heal()
    {
        _attackService.Heal(_player);
        _turnEngine.EndPlayerTurn();
    }
    
    
    //===== PLAYTEST ONLY =====
    //these methods should be in a different controller for our actual game

    public void ResetEnemy()
    {
        _enemy.ResetValues();
    }

    public void UpgradeEnemy(bool isPlayerWin)
    {
        if (!isPlayerWin) return;
        
        int random = Random.Range(0, 2+1);
        switch (random)
        {
            case 0:
                _enemy.Attack.Value += 1;
                Debug.Log("enemy upgraded attack power");
                break;
            case 1:
                _enemy.Defense.Value += 1;
                Debug.Log("enemy upgraded defence");
                break;
            case 2:
                _enemy.MaxHP.Value += 1;
                Debug.Log("enemy upgraded max health");
                break;
        }
    }

    public void HealEnemy()
    {
        _enemy.HP.Value = _enemy.MaxHP.Value;
    }
    
    //called by UI button
    public void PLAYTEST_StartEncounter()
    {
        if (_turnEngine.isEncounterRunning) return;
        _turnEngine.EnterEncounter();
        _turnEngine.StartPlayerTurn(); 
    }
    
}
