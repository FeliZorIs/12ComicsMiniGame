using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystemPlayer : MonoBehaviour
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
