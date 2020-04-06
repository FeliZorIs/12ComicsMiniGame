using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_spawner : MonoBehaviour
{

    public GameObject enemyManager;
    public enemy[] Enemies;
    public GameObject Boss;

    public float x_position;
    public float y_zone;
    public float timeInBetween;
    float timer;

    bool spawning;
    public float waveKills;
    public float maxEnemiesOnscreen;
    bool on = true;
    public bool boss_alive = false;

    //wave count declarations
    int wave_count;
    public Text wave_text;
    bool wave_bool;

    //background change
    public GameObject[] backgrounds;
    int current_background = 0;

    enum WaveState
    {
        WAVE,
        ENEMY,
        BOSS,
        RESET
    }
    WaveState waveState;

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
                if (boss_alive == false)
                    waveState = WaveState.RESET;
                break;

            case WaveState.RESET:
                waveKills_increase();
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
        enemy enemy_toSpawn;
        int chosen = Random.RandomRange(0, 100);
        int goldSpawn = Random.RandomRange(0, 15000);
        int pattern = Random.RandomRange(0, 1000);

        if (goldSpawn >= 14999)
        {
            enemy_toSpawn = Enemies[3];
            Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + Random.RandomRange((0 - y_zone), y_zone)), Quaternion.identity);
        }

        //wave 1: basic enemy only
        if (wave_count == 1)
            enemy_toSpawn = Enemies[0];

        //wave 2: heavy only
        else if (wave_count == 2)
            enemy_toSpawn = Enemies[2];

        //wave 3: shooter only
        else if (wave_count == 3)
            enemy_toSpawn = Enemies[1];

        //wave 4 and over
        else
        {
            if (chosen <= 80)
                enemy_toSpawn = Enemies[0];
            else if (chosen > 80 && chosen <= 90)
                enemy_toSpawn = Enemies[1];
            else
                enemy_toSpawn = Enemies[2];
        }
        //

        if (spawning && enemyManager.GetComponent<enemy_manager>().active_enemies.Count <= maxEnemiesOnscreen)
            timer += Time.deltaTime;


        if (timer >= timeInBetween)
        {
            if (pattern <= 9999)
                Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + Random.RandomRange((0 - y_zone), y_zone)), Quaternion.identity);
            else 
            {
                int pattern_select = Random.RandomRange(1, 4);
                if(pattern_select == 1)
                    StartCoroutine("X_pattern_right", enemy_toSpawn);
                if (pattern_select == 2)
                    StartCoroutine("T_pattern_right", enemy_toSpawn);
                if (pattern_select == 3)
                    StartCoroutine("V_pattern_left", enemy_toSpawn);
                if (pattern_select == 4)
                    StartCoroutine("V_pattern_right", enemy_toSpawn);
            }
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

    void waveKills_increase()
    {
        //going onto wave 2
        if (wave_count == 1)
            waveKills = 15;

        //going onto wave 3
        if (wave_count == 2)
        {
            waveKills = 20;
            timeInBetween = 1.25f;
        }

        //going onto wave 4
        if (wave_count == 3)
        {
            waveKills = 35;
            timeInBetween = .75f;
        }

        //going onto wave 5 and onward
        if (wave_count >= 4)
            waveKills *= 1.15f;

    }

    void changeBackground()
    {
        if (wave_count % 3 == 0)
        {
            current_background++;
            if (current_background > backgrounds.Length - 1)
                current_background = 0;
           
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].gameObject.SetActive(false);
            }

            backgrounds[current_background].gameObject.SetActive(true);
        }
    }

    //============================
    //          Coroutines
    //============================

    IEnumerator spawn_boss()
    {
        yield return new WaitForSeconds(1);
        boss_alive = true;
        Instantiate(Boss, new Vector2(0, 0), Quaternion.identity);
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

            changeBackground();
            wave_text.gameObject.SetActive(false);
            waveState = WaveState.ENEMY;
        }
    }

    IEnumerator X_pattern(enemy enemy_toSpawn)
    {
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + 1), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - 1), Quaternion.identity);

        yield return new WaitForSeconds(.5f);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.5f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + 1), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - 1), Quaternion.identity);
    }

    IEnumerator T_pattern(enemy enemy_toSpawn)
    {
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);

        yield return new WaitForSeconds(.25f);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + .5f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + 1f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - .5f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - 1f), Quaternion.identity);
        yield return new WaitForSeconds(.25f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
    }

    IEnumerator V_pattern_left(enemy enemy_toSpawn)
    {
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.25f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + .5f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - .5f), Quaternion.identity);
        yield return new WaitForSeconds(.25f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + 1f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - 1f), Quaternion.identity);
        yield return new WaitForSeconds(.25f);
    }

    IEnumerator V_pattern_right(enemy enemy_toSpawn)
    {
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + 1f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - 1f), Quaternion.identity);
        yield return new WaitForSeconds(.25f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y + .5f), Quaternion.identity);
        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y - .5f), Quaternion.identity);
        yield return new WaitForSeconds(.25f);

        Instantiate(enemy_toSpawn, new Vector2(x_position, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(.25f);
    }
}
