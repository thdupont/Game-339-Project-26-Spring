using Game.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : ObserverMonoBehaviour
{
    [Header("Initialize")]
    [SerializeField] private string _characterID; //TODO replace with scriptable object reference
    private Character _character;
    
    [Header("Values")]
    [SerializeField] private Image _characterImage;
    
    [Header("View")]
    [SerializeField] private HealthView _healthView;
    
    protected override void Subscribe()
    {
        _character = ServiceResolver.Resolve<CharacterManager>().Get(_characterID);
        _healthView.Subscribe(_character);
    }

    protected override void Unsubscribe()
    {
        _healthView.Unsubscribe();
    }
}
