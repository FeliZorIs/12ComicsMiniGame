using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //wave count declarations
    int wave_count;
    public Text wave_text;
     bool wave_bool;

    enum WaveState 
    {
        WAVE,
        ENEMY,
        BOSS,
        RESET
    } WaveState waveState;

    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager");

        spawning = true;
        timer = 0;

        wave_count = 0;
        wave_bool = true;
        wave_text.gameObject.SetActive(false);

        waveState = WaveState.WAVE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (waveState)
        {
            case WaveState.WAVE:
                StartCoroutine("wave_show");
                break;

            case WaveState.ENEMY:
                spawn_wave();
                spawn_boss_check();
                break;

            case WaveState.BOSS:
                if (Boss.GetComponent<Boss>().isAlive_check() == false)
                    waveState = WaveState.RESET;
                break;

            case WaveState.RESET:
                waveKills *= 1.5f;
                Boss.GetComponent<Boss>().reset_boss();
                enemyManager.GetComponent<enemy_manager>().enemiesKilled_current = 0;
                on = true;
                wave_bool = true;
                spawning = true;

                waveState = WaveState.WAVE;
                break;
        }
    }

    //============================
    //          Functions
    //============================

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

    //============================
    //          Coroutines
    //============================

    IEnumerator spawn_boss()
    {
        yield return new WaitForSeconds(1);
        Boss.SetActive(true);
        waveState = WaveState.BOSS;
    }

    IEnumerator wave_show()
    {
        if (wave_bool)
        {
            wave_bool = false;
            wave_count++;
            wave_text.text = "Wave: " + wave_count;
            wave_text.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);

            wave_text.gameObject.SetActive(false);
            waveState = WaveState.ENEMY;
        }
    }
}
