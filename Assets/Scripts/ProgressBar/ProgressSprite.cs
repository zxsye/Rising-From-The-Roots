using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSprite : MonoBehaviour
{
    public FillBar slider;

    public RectTransform sliderRect;

    public float prevValue;

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<FillBar>();   
        sliderRect = GameObject.Find("Slider").GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = (sliderRect.rect.width / -2) + (sliderRect.rect.width * slider.CurrentValue);
        Vector3 newPos = transform.localPosition;
        newPos.x = xPos;
        newPos.y = -2;
        transform.localPosition = newPos;
    }
}
