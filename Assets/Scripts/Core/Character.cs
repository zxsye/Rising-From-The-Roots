using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    [HideInInspector]public RectTransform root;

    DialogueSystem dialogue;

    public void Say(string speech, bool add = true){
        if (!enabled)
            enabled = !enabled;

        dialogue.Say(speech, characterName, add);
            
    }

    public void TurnOnOff(bool on) {
        if (on) {
            enabled = true;
        } else {
            enabled = false;
        }
    }

    public bool enabled {
        get{
            return root.gameObject.activeInHierarchy;
        } set{
            root.gameObject.SetActive(value);
        }
    }


    // Begin transitioning images
    public Sprite GetSprite(string spriteName) {
        Sprite sprite = Resources.Load<Sprite>("Images/Characters/" + characterName + "_" + spriteName);
        return sprite;
    }

    public void SetBody(string spriteName) {
        renderers.bodyRenderer.sprite = GetSprite(spriteName);
        
    }



    ////////////////////


    // Constructor
    public Character(string _name, bool enableOnStart = true) {
        CharacterManager cm = CharacterManager.instance;
        GameObject prefab = Resources.Load("Characters/Character[" + _name + "]") as GameObject;
        
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;

        // get the renderer(s)
        renderers.renderer = ob.GetComponentInChildren<RawImage>();

        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }

    [System.Serializable]
    class Renderers {
        public RawImage renderer; 
    }

    Renderers renderers = new Renderers();

}
