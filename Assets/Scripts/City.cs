using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int city_health = 3;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("TestPlayer");
    }

    void Update()
    {
        Debug.Log(city_health);
        if (city_health <= 0 && player != null)
            city_destroyed();
    }

    public void city_destroyed()
    {
        player.GetComponent<Player>().gameOver();
    }
}
