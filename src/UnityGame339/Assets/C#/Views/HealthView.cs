using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    private const int VALUE_OFFSET = 1;
    
    [SerializeField] private TextMeshProUGUI _currentHealthText;
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private Slider _healthBar;

    private Character _character;
    
    public void Subscribe(Character character)
    {
        _character = character;
        _character.HP.ChangeEvent += OnHealthChange;
        _character.MaxHP.ChangeEvent += OnMaxHealthChange;
        
        OnMaxHealthChange(_character.MaxHP.Value);
        OnHealthChange(_character.HP.Value);
    }

    public void Unsubscribe()
    {
        _character.HP.ChangeEvent -= OnHealthChange;
        _character.MaxHP.ChangeEvent -= OnMaxHealthChange;
    }

    private void OnHealthChange(int value)
    {
        _healthBar.value = value;
        _currentHealthText.text = $"{value * VALUE_OFFSET}";
    }

    private void OnMaxHealthChange(int value)
    {
        _healthBar.maxValue = value;
        _maxHealthText.text = $"{value * VALUE_OFFSET}";
    }
}