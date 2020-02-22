using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{

    public float x_zone;
    public float y_zone;
    public float timeInBetween;

    float timer;

    public GameObject Enemy;

    void Start()
    {
        timer = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeInBetween)
        {
            Debug.Log(Enemy.name + "has spawned");
            Instantiate(Enemy, new Vector2(this.transform.position.x, this.transform.position.y+ Random.RandomRange((0-y_zone), y_zone)), Quaternion.identity);
            timer = 0;
        }
    }
}
