using UnityEngine;

public class TurnBasedCombat : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private TurnBasedFighter playerFighter;
    [SerializeField] private GameObject enemy;
    private TurnBasedFighter enemyFighter;

    public enum GameTurn
    {
        Player,
        Enemy,
    }

    public GameTurn currentTurn = GameTurn.Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerFighter = player.GetComponent<TurnBasedFighter>();
        enemyFighter = enemy.GetComponent<TurnBasedFighter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurn == GameTurn.Player)
        {
            OnPlayerTurn();
        }

        if (currentTurn == GameTurn.Enemy)
        {
            
        }
    }
    
    


    public void OnPlayerTurn()
    {
        
    }

    public void OnPlayerAttack()
    {
        enemyFighter.TakeDamage(playerFighter.GetFishDamage());
    }

    public void OnEnemyTurn()
    {
        
    }

    public void AttackParry()
    {
        
    }


    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }

}
