﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public int city_health = 3;
    public int city_health_Max;

    public Text city_text;
    GameObject player;

    public CityBar cityBar;

    void Start()
    {

        city_health_Max = city_health;

        player = GameObject.Find("TestPlayer");

        cityBar.setCityMAX(city_health_Max);

    }

    void Update()
    {
        cityBar.setCityHP(city_health);
        city_text.text = "City: " + city_health + " / "+ city_health_Max;
        if (city_health <= 0 && player != null)
        {
            city_health = 0;
            city_destroyed();
        }
    }

    public void city_destroyed()
    {
        player.GetComponent<Player>().gameOver();
        city_text.text = "City: 0"  + " / " + city_health_Max;
    }
}
