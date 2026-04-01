using Game339.Shared;

namespace Game.Runtime
{
    public class Character
    {
        public Character(string id, string displayName, int hp, int attack, int defense)
        {
            Id = id;
            DisplayName = new ObservableValue<string>(displayName);
            Hp = new ObservableValue<int>(hp);
            Attack =  new ObservableValue<int>(attack);
            Defense = new ObservableValue<int>(defense);
        }
        
        public string Id { get; }
        public ObservableValue<string> DisplayName { get; }
        public ObservableValue<int> Hp { get; }
        public ObservableValue<int> Attack { get; }
        public ObservableValue<int> Defense { get; }
    }
}