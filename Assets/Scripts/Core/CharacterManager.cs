using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Responsible for adding and maintaining characters in the scene.
public class CharacterManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static CharacterManager instance;

    // Characters are attached to character panel
    public RectTransform characterPanel;

    // List of all characters currently in our scene
    public List<Character> characters = new List<Character>();

    // Easy lookup for characters
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    public 
    void Awake() {
        instance = this;
    }

    // Try to get character by the name provided from the character list

    public Character GetCharacter(string characterName, bool createCharIfNotExist = true, bool enableCreatedCharacterOnStart = true)
    {
        int index = -1;
        if (characterDictionary.TryGetValue(characterName, out index)) {
            print(index);
            print(characters.Count);
            return characters[index];
        } else {
            return CreateCharacter(characterName, enableCreatedCharacterOnStart);
        }
        // } else if (createCharIfNotExist) {
        // return null;
    }

     public Character CreateCharacter(string characterName, bool enableOnStart = true) {
        print("Character made");
        Character newCharacter = new Character(characterName, enableOnStart);
        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);
        
        return newCharacter;
    }
   
}