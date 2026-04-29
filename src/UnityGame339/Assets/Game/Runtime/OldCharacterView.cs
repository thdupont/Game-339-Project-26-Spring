using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class OldCharacterView : ObserverMonoBehaviour
    {

        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private Button attackButton;

        [SerializeField] public string characterId;
        [SerializeField] public string targetId;

        protected override void Subscribe()
        {
            var characterManager = ServiceResolver.Resolve<CharacterManager>();
            var character = characterManager.Get(characterId);
            character.HP.ChangeEvent += HpOnChangeEvent;
            character.DisplayName.ChangeEvent += DisplayNameOnChangeEvent;

            attackButton.onClick.AddListener(OnClick);
        }

        protected override void Unsubscribe()
        {
            var characterManager = ServiceResolver.Resolve<CharacterManager>();
            var character = characterManager.Get(characterId);
            character.HP.ChangeEvent -= HpOnChangeEvent;
            character.DisplayName.ChangeEvent -= DisplayNameOnChangeEvent;

            attackButton.onClick.RemoveListener(OnClick);
        }

        private void DisplayNameOnChangeEvent(string obj)
        {
            nameText.text = obj;
        }

        private void HpOnChangeEvent(int obj)
        {
            hpText.text = $"HP: {obj}";
        }

        private void OnClick()
        {
            var characterManager = ServiceResolver.Resolve<CharacterManager>();
            var character = characterManager.Get(characterId);
            var target = characterManager.Get(targetId);

            var attackService = ServiceResolver.Resolve<AttackService>();
            // attackService.Attack(character, target);
        }
    }
}
