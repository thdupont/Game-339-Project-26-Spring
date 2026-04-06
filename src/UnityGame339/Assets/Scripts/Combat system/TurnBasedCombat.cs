using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TurnBasedCombat : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private TurnBasedFighter playerFighter;
    [SerializeField] private GameObject enemy;
    private TurnBasedFighter enemyFighter;

    [Header("Parry UI")]
    [SerializeField] private GameObject parryUI;
    [SerializeField] private Image parryBarFill;
    [SerializeField] private TextMeshProUGUI parryStatusText;

    [Header("Parry Settings")]
    [SerializeField] private float parryDuration = 1f; // Total time for the bar to fill from 0 to 1.0
    [SerializeField] private float parryWindowMin = 0.4f;
    [SerializeField] private float parryWindowMax = 0.8f;

    private bool isParryWindowActive = false;
    private float parryProgress = 0f;
    private bool hasInputBeenDetected = false;

    
    
    
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
            if (!isParryWindowActive)
            {
                OnEnemyTurn();
            }
            else
            {
                HandleParryLogic();
            }
        }

        
    }

    private void HandleParryLogic()
    {
        // Increment progress based on time relative to total duration
        parryProgress += Time.deltaTime / parryDuration;
        parryBarFill.fillAmount = Mathf.Clamp01(parryProgress);

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !hasInputBeenDetected)
        {
            hasInputBeenDetected = true;
            CheckParryTiming();
        }

        if (parryProgress >= 1f)
        {
            if (!hasInputBeenDetected)
            {
                OnParryFail("Miss!");
            }
            FinishEnemyTurn();
        }
    }

    private void CheckParryTiming()
    {
        if (parryProgress >= parryWindowMin && parryProgress <= parryWindowMax)
        {
            OnParrySuccess();
        }
        else
        {
            OnParryFail("Too Early/Late!");
        }
    }

    private void OnParrySuccess()
    {
        parryStatusText.text = "Parried!";
        Debug.Log("Parry Success!");
        // Reduce damage or take no damage
        // We'll call enemy attack now but with reduced effect or handle it here
        enemyFighter.TakeDamage(0); // For now, maybe reflect damage or just block
    }

    private void OnParryFail(string message)
    {
        parryStatusText.text = message;
        Debug.Log("Parry Fail: " + message);
        playerFighter.TakeDamage(enemyFighter.GetFishDamage());
        Debug.Log("Enemy did: " + enemyFighter.GetFishDamage() + " Damage. Player HP now: " + playerFighter.fishData.Health);
    }

    private void FinishEnemyTurn()
    {
        isParryWindowActive = false;
        // Small delay could be added here to show the status text
        Invoke(nameof(ResetParryUI), 0.1f);
        currentTurn = GameTurn.Player;
    }

    private void ResetParryUI()
    {
        parryUI.SetActive(false);
        parryStatusText.text = "";
    }
    
    


    public void OnPlayerTurn()
    {
        
    }

    public void OnPlayerAttack()
    {
        enemyFighter.TakeDamage(playerFighter.GetFishDamage());
        Debug.Log("player did: " +  playerFighter.GetFishDamage() + " Damage" + " Enemy HP now: " + enemyFighter.fishData.Health);

        currentTurn = GameTurn.Enemy;
    }
    

    public void OnEnemyTurn()
    {
        Debug.Log("enemy turn starting");
        // Start enemy attack sequence which includes the parry bar
        OnEnemyAttack();
    }

    public void OnEnemyAttack()
    {
        enableParryBar();
    }

    public void enableParryBar()
    {
        parryUI.SetActive(true);
        parryBarFill.fillAmount = 0;
        parryProgress = 0;
        hasInputBeenDetected = false;
        parryStatusText.text = "Press SPACE!";
        isParryWindowActive = true;
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
