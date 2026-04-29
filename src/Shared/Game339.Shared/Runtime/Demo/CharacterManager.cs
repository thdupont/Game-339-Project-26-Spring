using System.Collections.Generic;
using Game339.Shared.Diagnostics;

public class CharacterManager
{
    private readonly Dictionary<string, Character> _characters = new Dictionary<string, Character>();

    public void Add(Character character)
    {
        _characters[character.Id] = character;
    }
        
    public Character Get(string id)
    {
        return _characters[id];
    }
}