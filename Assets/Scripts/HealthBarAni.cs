using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAni : MonoBehaviour
{
    private HealthSystemPlayer healthSystem;
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
    }

    private void Start()
    {
        healthSystem = new HealthSystemPlayer(100);
        setHealth(healthSystem.GetHealthNormalized());
        healthSystem.onDamaged += HealthSystem_onDamaged;
        healthSystem.onHealed += HealthSystem_onHealed;

        
    }
    

    private void HealthSystem_onHealed(object sender, System.EventArgs e)
    {
        setHealth(healthSystem.GetHealthNormalized());
       // throw new System.NotImplementedException();
    }

    private void HealthSystem_onDamaged(object sender, System.EventArgs e)
    {
        setHealth(healthSystem.GetHealthNormalized());
        // throw new System.NotImplementedException();
    }

    private void setHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    } 
}
