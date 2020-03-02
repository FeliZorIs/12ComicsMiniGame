using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{

    public GameObject enemyManager;
    public GameObject Enemy;
    public GameObject Boss;

    public float x_position;
    public float y_zone;
    public float timeInBetween;
    float timer;

    bool spawning;
    public int waveKills;

    void Start()
    {
        spawning = true;
        timer = 0;
        enemyManager = GameObject.Find("EnemyManager");
    }

    // Update is called once per frame
    void Update()
    {
        spawn_wave();

        if (enemyManager.GetComponent<enemy_manager>().enemiesKilled >= waveKills)
        {
            spawning = false;
            if (enemyManager.GetComponent<enemy_manager>().active_enemies.Count > 0)
            {
                Debug.Log("finish the enemies");
            }
            else 
            {
                Boss.SetActive(true);
            }
        }
    }

    void spawn_wave()
    {
        if(spawning)
        timer += Time.deltaTime;

        if (timer >= timeInBetween)
        {
            //Debug.Log(Enemy.name + "has spawned");
            Instantiate(Enemy, new Vector2(x_position, this.transform.position.y + Random.RandomRange((0 - y_zone), y_zone)), Quaternion.identity);
            timer = 0;
        }
    }


}
