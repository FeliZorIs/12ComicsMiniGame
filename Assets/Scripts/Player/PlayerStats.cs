using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    //Variables for different stats. Making sure they can be manipulated in customization screen and their values are kept into gameplay.
    static public int healthLevel = 1;
    static public int ammoLevel = 1;
    static public int superMeterLevel = 1;
    static public int multiLevel = 1;
    static public int maxPoints;
    static public int pointsRemaining = 10;


    public Text healthText;
    public Text ammoText;
    public Text superMeterText;
    public Text multiplierText;
    public Text pointsRemainingText;
   
    // Start is called before the first frame update
    void Start()
    {
        //Check the DB here for the grades and correspond that to the points given to student.
        healthText.text = "Health level: " + healthLevel;
        ammoText.text = "Ammo level: " + ammoLevel;
        superMeterText.text = "SuperMeter level: " + superMeterLevel;
        multiplierText.text = "Multiplier level: " + multiLevel;
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     /////////////////////////////////
     ****BUTTON FUNCTIONS BELOW*****
     /////////////////////////////////
     */


    /* Amount of starting HEALTH during gameplay
    *******************************************
    *
    */
    public void healthIncrease(int health)
    {
        //Make it so Healthlevel cannot exceed a maximum of 10.
        if (pointsRemaining <= 0 || healthLevel >= 10)
        {
            return;
        }
        
        else
        {
            healthLevel += 1;
            pointsRemaining -= 1;
        }

        if (healthLevel == 10)
        {
            healthText.text = "Health level: " + healthLevel + " (MAX)" ;
        }

        else
        {
            healthText.text = "Health level: " + healthLevel;
        }
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;

        //\n (You begin with 10 health **MIGHT USE THIS UNDER EACH SECTION TO GIVE BETTER INDICATION OF WHAT HAPPENS BASED ON LEVEL**
    }

    public void healthDecrease(int health)
    {
        //Health can't go lower than 1.
        if (healthLevel <= 1)
        {
            return;
        }

        else
        {
            healthLevel -= 1;
            pointsRemaining += 1;
        }
        healthText.text = "Health level: " + healthLevel;
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    /* Determines AMMO types available during gameplay 
    ************************************************
    *
    */
    public void ammoIncrease(int ammo)
    {
        //For now only 3 ammo types so yeah.
        if (pointsRemaining <= 0 || ammoLevel >= 3)
        {
            return;
        }

        else
        {
            ammoLevel += 1;
            pointsRemaining -= 1;
        }

        if (ammoLevel == 3)
        {
            ammoText.text = "Ammo level: " + ammoLevel + " (MAX)";
        }

        else
        {
            ammoText.text = "Ammo level: " + ammoLevel;
        }
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }


    public void ammoDecrease(int ammo)
    {
        //Can't go lower than 1!
        if (ammoLevel <= 1)
        {
            return;
        }

        else
        {
            ammoLevel -= 1;
            pointsRemaining += 1;
        }
       
        ammoText.text = "Ammo level: " + ammoLevel;
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }
    /* Rate of SUPERMETER growth during gameplay 
     *******************************************
     *
     */
    public void superMeterIncrease(int supMeter)
    {
        //Temp max for superMeter
        if (pointsRemaining <= 0 || superMeterLevel >= 5)
        {
            return;
        }

        else
        {
            superMeterLevel += 1;
            pointsRemaining -= 1;
        }

        if (superMeterLevel == 5)
        {
            superMeterText.text = "SuperMeter level: " + superMeterLevel + " (MAX)";
        }

        else
        {
            superMeterText.text = "SuperMeter level: " + superMeterLevel;
        }
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    public void superMeterDecrease(int supMeter)
    {
        //Can't go below 1!
        if (superMeterLevel <= 1)
        {
            return;
        }

        else
        {
            superMeterLevel -= 1;
            pointsRemaining += 1;
        }
        superMeterText.text = "SuperMeter level: " + superMeterLevel;   
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    /* Your score Multiplier. Won't be exact to the level. But we'll see how we want to balance this since score is important.
     *************************************************
     *
     */
    public void multiIncrease(int multi)
    {
        //Maybe x5? Not entirely sure yet on this.
        if (pointsRemaining <= 0 || multiLevel >= 5)
        {
            return;
        }

        else
        {
            multiLevel += 1;
            pointsRemaining -= 1;
        }

        if (multiLevel == 5)
        {
            multiplierText.text = "Multiplier level: " + multiLevel + " (MAX)";
        }

        else
        {
            multiplierText.text = "Multiplier level: " + multiLevel;
        }
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    public void multiDecrease(int multi)
    {
        //Can't go below 1!
        if (multiLevel <= 1)
        {
            return;
        }

        else
        {
            multiLevel -= 1;
            pointsRemaining += 1;
        }
        multiplierText.text = "Multiplier level: " + multiLevel;
        pointsRemainingText.text = "Points remaining: " + pointsRemaining;
    }

    //Button click to actually play the game!
    public void Play()
    {
        SceneManager.LoadScene("TestMap");
    }

    public void Back()
    {
        SceneManager.LoadScene("PlayerMenu");
    }
}
