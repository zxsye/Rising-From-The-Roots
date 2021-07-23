using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdSprite : MonoBehaviour
{
    RectTransform sliderRect;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform sliderRect = GameObject.Find("Slider").GetComponent<RectTransform>();   
        float successThreshold = GameObject.Find("GameMaster").GetComponent<GameMaster>().successThreshold;
        float xPos = (sliderRect.rect.width / -2) + (sliderRect.rect.width * successThreshold);
        transform.localPosition = transform.localPosition + new Vector3(xPos, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
