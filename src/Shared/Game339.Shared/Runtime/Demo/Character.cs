using Game339.Shared;

public class Character
{
    public Character(string id, string displayName, int hp, int attack, int defense)
    {
        Id = id;
        DisplayName = new ObservableValue<string>(displayName); 
        MaxHP = new ObservableValue<int>(hp);
        HP = new ObservableValue<int>(hp);
        Attack =  new ObservableValue<int>(attack);
        Defense = new ObservableValue<int>(defense);

        _defaultHP = hp;
        _defaultAttack = attack;
        _defaultDefense = defense;
    }
        
    public string Id { get; }
    public ObservableValue<string> DisplayName { get; }
    public ObservableValue<int> MaxHP { get; }
    public ObservableValue<int> HP { get; }
    public ObservableValue<int> Attack { get; }
    public ObservableValue<int> Defense { get; }

    private int _defaultHP;
    private int _defaultAttack;
    private int _defaultDefense;

    public void ResetValues()
    {
        HP.Value = _defaultHP;
        Attack.Value = _defaultAttack;
        Defense.Value = _defaultDefense;
    }
}