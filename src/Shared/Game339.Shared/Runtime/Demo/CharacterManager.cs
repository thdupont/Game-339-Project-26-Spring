using System.Collections.Generic;
using Game339.Shared.Diagnostics;

namespace Game.Runtime
{
    public class CharacterManager
    {
        private readonly Dictionary<string, Character> _characters;
        private readonly IGameLog _logger;

        public CharacterManager(IGameLog logger)
        {
            _characters = new();
            _logger = logger;
        }

        public void Add(Character character)
        {
            _characters[character.Id] = character;
            _logger.Info($"{character.Id} added");
        }
        
        public Character Get(string id)
        {
            return _characters[id];
        }
    }
}