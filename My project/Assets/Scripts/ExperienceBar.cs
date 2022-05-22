using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;

    public void SetExp(float exp)
    {
        slider.value = exp;
    }

    public void SetExpNeeded(float exp)
    {
        slider.maxValue = exp;
    }
}
