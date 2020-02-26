using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static int health = 3;
    public static int maxHealth = 3 ; 
    Text HealthText;
    // Start is called before the first frame update
    void Start()
    {
        HealthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        HealthText.text = "Health: " + health + " / " + maxHealth;
      
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("Health is at " + health + " Game over.");
        }
    }

    

    
}
