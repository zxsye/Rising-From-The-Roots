using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelController : MonoBehaviour
{
    List<string> data = new List<string>();

    int progress = 0;

    void Start ()
    {
        LoadChapterFile("introduction");
    }

    void Update() {
        // testing
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            HandleLine(data[progress]);
            progress++;
        }
    }

    void LoadChapterFile(string fileName) {
        data = FileManager.LoadFile(FileManager.savPath + "Resources/Story/" + fileName);
        progress = 0;
    }

    void HandleLine(string line) {
        string[] dialogueAndActions= line.Split('"');

        // 3 objects meeans dialogue.
        // 1 object means no dialogue. Only actions.

        if (dialogueAndActions.Length == 3) {
            HandleDialogue(dialogueAndActions[0], dialogueAndActions[1]);
            HandleEventsFromLine(dialogueAndActions[2]);

        } else {
            HandleEventsFromLine(dialogueAndActions[0]);
        }
    }



    string cachedLastSpeaker = "";
    void HandleDialogue(string dialogueDetails, string dialogue) {
        string speaker = cachedLastSpeaker;
        bool additive = dialogueDetails.Contains("+");

        if (additive)
            dialogueDetails = dialogueDetails.Remove(dialogueDetails.Length-1);
        
        if (dialogueDetails.Length > 0) {
            if (dialogueDetails[dialogueDetails.Length-1] == ' ')
                dialogueDetails = dialogueDetails.Remove(dialogueDetails.Length-1);
            
            speaker = dialogueDetails;
            cachedLastSpeaker = speaker;
        }

        // now speak
        if (speaker != "narrator") {
            Character character = CharacterManager.instance.GetCharacter(speaker);
            character.Say(dialogue, additive);
        } else {
            DialogueSystem.instance.Say(dialogue, speaker, additive);
        }
    }

    void HandleEventsFromLine(string events) {
        print("Handle event [" + events + "]"); // @todo
    }

}