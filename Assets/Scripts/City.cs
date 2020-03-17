using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public int city_health = 3;
    int city_health_Max;
    public Text city_text;
    GameObject player;

    void Start()
    {
        city_health_Max = city_health;
        player = GameObject.Find("TestPlayer");
    }

    void Update()
    {
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
    }
}
