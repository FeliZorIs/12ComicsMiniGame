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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "player_shot")
        {
            ScoreCount.scoreValue += 10;
            Destroy(this.gameObject);
        }

        if (collision.tag == "Despawner")
        {
            Destroy(this.gameObject);
        }
    }
}
