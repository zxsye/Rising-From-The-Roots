using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterTest : MonoBehaviour
{
    public Character Narcissus;
    // Start is called before the first frame update
    void Start()
    {
        Narcissus = CharacterManager.instance.GetCharacter("Narcissus", enableCreatedCharacterOnStart: false);
    }

    public string[] speech;
    int i = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (i < speech.Length)
                Narcissus.Say(speech[i]);
            else
                DialogueSystem.instance.Close();
            i++;
        }
    }
}
