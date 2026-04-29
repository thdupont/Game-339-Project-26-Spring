using System;
using Game339.Shared;
using UnityEngine;

public class TurnEngine
{
    //===== Turn Events =====
    public event Action EncounterStart;
    public event Action<bool> EncounterEnd;
    public event Action<int> TurnStart;
    public event Action TurnEnd;

    //===== Global Information =====
    public ObservableValue<int> Lives { private set; get; } = new ObservableValue<int>();
    public ObservableValue<int> TotalEncounters { private set; get; } = new ObservableValue<int>();
    public ObservableValue<bool> IsPlayerTurn { private set; get; } = new ObservableValue<bool>();
    public bool isEncounterRunning { private set; get; } = false;

    //===== Encounter Information =====
    private int turnIndex = 0;
    
    public void EnterEncounter()
    {
        turnIndex = 0;
        isEncounterRunning = true;
        EncounterStart?.Invoke();
    }

    public void StartPlayerTurn()
    {
        turnIndex += 1;
        TurnStart?.Invoke(turnIndex);
        
        IsPlayerTurn.Value = true;
    }

    public void EndPlayerTurn()
    {
        TurnEnd?.Invoke();
        
        IsPlayerTurn.Value = false;
    }

    public void ExitEncounter(bool success)
    {
        IsPlayerTurn.Value = false;
        isEncounterRunning = false;
        
        if (success)
        {
            TotalEncounters.Value += 1;
        }
        else
        {
            Lives.Value -= 1;
        }
        EncounterEnd?.Invoke(success);
    }

}
