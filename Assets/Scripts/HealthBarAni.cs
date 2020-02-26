using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAni : MonoBehaviour
{
    private const float BAR_WIDTH = 175f;
    private HealthSystemPlayer healthSystem;
    private Transform damagedBarTemplate;
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        damagedBarTemplate = transform.Find("DamagedBarTemplate");
    }

    private void Start()
    {
        healthSystem = new HealthSystemPlayer(100);
        setHealth(healthSystem.GetHealthNormalized());
        healthSystem.onDamaged += HealthSystem_onDamaged;
        healthSystem.onHealed += HealthSystem_onHealed;
        healthSystem.Damage(33);
        
    }

    public void Setup(HealthSystemPlayer healthSystem)
    {
        this.healthSystem = healthSystem;
    }
    

    private void HealthSystem_onHealed(object sender, System.EventArgs e)
    {
        setHealth(healthSystem.GetHealthNormalized());
       
    }

    private void HealthSystem_onDamaged(object sender, System.EventArgs e)
    {

        //This is for the animation of the health falling/being reduced.
        float beforeDMGFill = barImage.fillAmount;
        setHealth(healthSystem.GetHealthNormalized());
        Transform damagedBar = Instantiate(damagedBarTemplate, transform);
        damagedBar.gameObject.SetActive(true);
        damagedBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(barImage.fillAmount * BAR_WIDTH, damagedBar.GetComponent<RectTransform>().anchoredPosition.y);
        damagedBar.GetComponent<Image>().fillAmount = beforeDMGFill - barImage.fillAmount;
        damagedBar.gameObject.AddComponent<HealthFall>();
       
    }

    private void setHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    } 
}
