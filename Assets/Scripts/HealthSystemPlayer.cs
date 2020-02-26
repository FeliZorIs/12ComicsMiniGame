using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Gonna be how we handle the player Health system in the game. Right now it's percentage based, we can change this if needed
//Also can modify this if we want enemies to have health (For instance, bosses) and what not.

public class HealthSystemPlayer
{
    public event EventHandler onDamaged;
    public event EventHandler onHealed;

    private int health;
    private int healthMax;

    public HealthSystemPlayer(int health) { //Constructor for the health system.
        healthMax = health; 
        this.health = health;
    }

    public int GetHealth() //Getter function if we need it.
    {
        return health; 
    }

 

    public void Damage(int dmgAmount) //Function to take damage.
    {
        health -= dmgAmount;
        if (health < 0) health = 0; //Makes it so health can't drop below 0 into negatives.
        if (onDamaged != null) onDamaged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if (onHealed != null) onHealed(this, EventArgs.Empty);
    }

    public float GetHealthNormalized() //This returns the health in percentage form.
    {
        return (float)health / healthMax;
    }

}
