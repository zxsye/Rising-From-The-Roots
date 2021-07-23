using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    public Slider slider;

    public float scoreOnHit;

    public float penaltyOnMiss;

    private float currentValue;

    private Image fill;

    public float CurrentValue 
    {
        get 
        {
            return currentValue;
        }
        set 
        {
            currentValue = value;
            slider.value= currentValue;
            slider.value = currentValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>(); 
        fill = GameObject.Find("Fill").GetComponent<Image>();
        CurrentValue = 0f;
        scoreOnHit = 0.01f;
        penaltyOnMiss = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void noteHit()
    {
        if (CurrentValue < 1f) 
        {
            CurrentValue += scoreOnHit;
        }
    }

    public void noteMissed()
    {
        if (CurrentValue > 0f)
        {
            CurrentValue -= penaltyOnMiss;
        }
    }

    public void setSuccess()
    {
        fill.color = Color.green;
    }

    public void setFail()
    {
        fill.color = Color.red;
    }
}
