using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
    }

    private void Start()
    {
        setHealth(.8f);
    }

    private void setHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    } 
}
