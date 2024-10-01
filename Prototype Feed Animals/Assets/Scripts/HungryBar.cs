using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class HungryBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHunger(int MaxFeed)
    {
        slider.maxValue = MaxFeed;
        slider.value = 0;

    }

    public void SetHunger(int food)
    {
        slider.value = food;
    }

    public float ReturnBarValue()
    {
        return slider.value;
    }
    
}
