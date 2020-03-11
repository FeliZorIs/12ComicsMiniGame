using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject enemyManager;
    public float speed = 2;
    static public int multiBonus;
    public GameObject player;
    
    private void Start()
    {
        enemyManager = GameObject.Find("EnemyManager");
        enemyManager.GetComponent<enemy_manager>().active_enemies.Add(this);
        player = GameObject.Find("TestPlayer");
        multiBonus = PlayerStats.multiLevel;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void onDeath()
    {
        enemyManager.GetComponent<enemy_manager>().active_enemies.Remove(this);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemyManager.GetComponent<enemy_manager>().enemiesKilled_total += 1;
            enemyManager.GetComponent<enemy_manager>().enemiesKilled_current += 1;
            onDeath();
        }

        if (collision.tag == "player_shot")
        {
            //Calculate score based on current multiplier. If multiplier will change throughout gameplay, we will need to use another reference than PlayerStats.multiLevel to store the multiplier.
            ScoreCount.scoreValue += (10*multiBonus);
            player.GetComponent<Player>().superMeterCharge(2);
            enemyManager.GetComponent<enemy_manager>().enemiesKilled_total += 1;
            enemyManager.GetComponent<enemy_manager>().enemiesKilled_current += 1;
            onDeath();
        }

        if (collision.tag == "Despawner")
        {
            onDeath();
        }
    }


}
