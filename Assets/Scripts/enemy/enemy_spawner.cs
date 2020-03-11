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
    public float waveKills;
    bool on = true;

    enum WaveState 
    {
        ENEMY,
        BOSS,
        RESET
    } WaveState waveState;

    void Start()
    {
        spawning = true;
        timer = 0;
        enemyManager = GameObject.Find("EnemyManager");
        waveState = WaveState.ENEMY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (waveState)
        {
            case WaveState.ENEMY:
                Debug.Log("1");
                spawn_wave();
                spawn_boss_check();
                break;

            case WaveState.BOSS:
                Debug.Log("2");
                if (Boss.GetComponent<Boss>().isAlive_check() == false)
                    waveState = WaveState.RESET;
                break;

            case WaveState.RESET:
                Debug.Log("3");
                waveKills *= 1.5f;
                Boss.GetComponent<Boss>().reset_boss();
                enemyManager.GetComponent<enemy_manager>().enemiesKilled_current = 0;
                on = true;
                spawning = true;

                waveState = WaveState.ENEMY;
                break;
        }
    }

    void spawn_wave()
    {
        if (spawning)
            timer += Time.deltaTime;

        if (timer >= timeInBetween)
        {
            //Debug.Log(Enemy.name + "has spawned");
            Instantiate(Enemy, new Vector2(x_position, this.transform.position.y + Random.RandomRange((0 - y_zone), y_zone)), Quaternion.identity);
            timer = 0;
        }
    }


    void spawn_boss_check()
    {
        if (enemyManager.GetComponent<enemy_manager>().enemiesKilled_current >= waveKills)
        {
            spawning = false;
            if (enemyManager.GetComponent<enemy_manager>().active_enemies.Count > 0)
            {
                //Debug.Log("finish the enemies");
            }
            else
            {
                if (on)
                {
                    on = false;
                    StartCoroutine("spawn_boss");
                }
            }
        }
    }

    IEnumerator spawn_boss()
    {
        yield return new WaitForSeconds(1);
        Boss.SetActive(true);
        waveState = WaveState.BOSS;
    }
}
