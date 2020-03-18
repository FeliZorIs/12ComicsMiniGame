using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupermeterBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxSuper(int super)
    {
        slider.maxValue = super;
        //slider.value = super;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetSuper(int super)
    {
        slider.value = super;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
