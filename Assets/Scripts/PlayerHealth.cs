using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static int health;
    public static int maxHealth; 
    Text HealthText;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = PlayerStats.healthLevel;
        health = maxHealth;
        HealthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        HealthText.text = "Health: " + health + " / " + maxHealth;
      
    }

    void FixedUpdate()
    {
       
    }

    

    
}
