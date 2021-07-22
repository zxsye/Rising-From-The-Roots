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
        print("INPUT LINE: "+ line);
        string[] dialogueAndActions= line.Split('"');
        print("INPUT LINE LENGTH: " + dialogueAndActions.Length);

        // 3 objects meeans dialogue.
        // 1 object means no dialogue. Only actions.

        if (dialogueAndActions.Length == 3) {
            HandleDialogue(dialogueAndActions[0], dialogueAndActions[1]);
            HandleEventsFromLine(dialogueAndActions[2]);
        } else if (dialogueAndActions.Length == 4) {
            HandleDialogue(dialogueAndActions[0], dialogueAndActions[1]);
            HandleEventsFromLine(dialogueAndActions[2]);
            HandleEventsFromLine(dialogueAndActions[3]);
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
        // print("Handle event [" + events + "]"); // @todo
        string[] actions = events.Split(' ');

        foreach(string action in actions) {
            HandleAction(action);
        }

    }

    void HandleAction(string action) {
        print("Handle event [" + action + "]"); // @todo
        // string[] dialogueAndActions = line.Split('"');

        string[] data = action.Split('(',')');

        if (data[0] == "setBackground") {
            Command_SetLayerImage(data[1], BCFC.instance.background);
            return;
        }
        if (data[0] == "setCinematic") {
            Command_SetLayerImage(data[1], BCFC.instance.cinematic);
            return;
        }
        if (data[0] == "setForeground") {
            Command_SetLayerImage(data[1], BCFC.instance.foreground);
            return;
        }
        if (data[0] == "turnOff") {
            TurnOff(data[1]);
        }
        if (data[0] == "EchoSurprised") {
            EchoSurprised();
        }
        if (data[0] == "EchoNeutral") {
            EchoNeutral();
        }
        if (data[0] == "EchoHappy") {
            EchoHappy();
        }
        if (data[0] == "SetTNarcissus") {
            SetTNarcissus(data[1]);
        }
    }

    void EchoHappy() {
        Character c = CharacterManager.instance.GetCharacter("Echo");
        c.SetSprite("happy");
    }

    void EchoNeutral() {
        // print("Getting echo neutral");
        Character c = CharacterManager.instance.GetCharacter("Echo");
        c.SetSprite("neutral");
    }

    void EchoSurprised() {
        // print("Getting echo surprised");
        Character c = CharacterManager.instance.GetCharacter("Echo");
        c.SetSprite("surprised");
    }

    void TurnOff(string characterName) {
        Character character = CharacterManager.instance.GetCharacter(characterName);
        print("Turning off: " + characterName);
        character.TurnOnOff(false);
    }

    void SetTNarcissus(string type) {
        Character c = CharacterManager.instance.GetCharacter("TNarcissus");
        c.TurnOnOff(true);
        c.SetSprite(type);
    }

    void Command_SetLayerImage(string data, BCFC.LAYER layer) {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        Texture2D tex = Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
        float spd = 2f;
        bool smooth = false;

        if (data.Contains(",")) {
            string[] parameters = data.Split(',');
            foreach(string p in parameters) {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                    spd = fVal;
                if (bool.TryParse(p, out bVal)) {
                    smooth = bVal; continue;
                }
            }
        }

        layer.TransitionToTexture(tex, spd, smooth);
    }

}