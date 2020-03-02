using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject enemyManager;
    public float speed = 2;

    private void Start()
    {
        enemyManager = GameObject.Find("EnemyManager");
        enemyManager.GetComponent<enemy_manager>().active_enemies.Add(this);
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
            enemyManager.GetComponent<enemy_manager>().enemiesKilled += 1;
            onDeath();
        }

        if (collision.tag == "player_shot")
        {
            ScoreCount.scoreValue += 10;
            enemyManager.GetComponent<enemy_manager>().enemiesKilled += 1;
            onDeath();
        }

        if (collision.tag == "Despawner")
        {
            onDeath();
        }
    }
}
