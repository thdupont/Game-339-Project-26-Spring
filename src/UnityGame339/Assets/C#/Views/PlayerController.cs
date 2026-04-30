using System;
using Game.Runtime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [SerializeField] private string _playerCharacter;
    [SerializeField] private string _enemyCharacter;
    [SerializeField] private int _healAmount = 2;
    
    private Character _player;
    private Character _enemy;
    private AttackService _attackService;
    private TurnEngine _turnEngine;
    
    private int _defaultHealAmount;
    
    public event Action<int> PlayerTakeDamage;
    public event Action<int> EnemyTakeDamage;
    public event Action PlayerHeal;
    public event Action EnemyHeal;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        _player = ServiceResolver.Resolve<CharacterManager>().Get(_playerCharacter);
        _enemy = ServiceResolver.Resolve<CharacterManager>().Get(_enemyCharacter);
        _attackService = ServiceResolver.Resolve<AttackService>();
        _turnEngine = ServiceResolver.Resolve<TurnEngine>();
        
        _player.ResetValues();
        _enemy.ResetValues();
        
        _defaultHealAmount = _healAmount;
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
        int healed = _player.HP.Value + _healAmount;
        if (healed > _player.MaxHP.Value) healed = _player.MaxHP.Value;
        _player.HP.Value = healed;
        _turnEngine.EndPlayerTurn();
    }
    
    //===== Upgrades =====
    public void UpgradeHealPotency(int amount)
    {
        _healAmount += amount;
    }
    
    public void ResetHealPotency()
    {
        _healAmount = _defaultHealAmount;
    }
}