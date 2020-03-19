using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2;
    public int enemy_health;
    static public int multiBonus;
    protected GameObject enemyManager;
    protected GameObject player;
    protected GameObject city;
    protected Renderer renderer;
    
    private void Start()
    {
        findComponents();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    //===============================
    //           Functions
    //===============================

    protected void findComponents()
    {
        enemyManager = GameObject.Find("EnemyManager");
        enemyManager.GetComponent<enemy_manager>().active_enemies.Add(this);
        player = GameObject.Find("TestPlayer");
        multiBonus = PlayerStats.multiLevel;
        city = GameObject.Find("Despawn_Enemy");
        renderer = GetComponent<Renderer>();

    }
    public void onDeath()
    {
        enemyManager.GetComponent<enemy_manager>().active_enemies.Remove(this);
        Destroy(this.gameObject);
    }

    protected void killed_by_player()
    {
        enemyManager.GetComponent<enemy_manager>().enemiesKilled_total += 1;
        enemyManager.GetComponent<enemy_manager>().enemiesKilled_current += 1;
        onDeath();
    }

    //================================
    //          Collisions
    //================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            killed_by_player();
        }

        if (collision.tag == "player_shot")
        {
            //Calculate score based on current multiplier. If multiplier will change throughout gameplay, we will need to use another reference than PlayerStats.multiLevel to store the multiplier.
            ScoreCount.scoreValue += (10*multiBonus);
            player.GetComponent<Player>().superMeterCharge(2);
            enemy_health--;
            if (enemy_health <= 0)
                killed_by_player();
            else
                StartCoroutine("flash");
            Destroy(collision.gameObject);

        }

        if (collision.tag == "Despawner")
        {
            city.GetComponent<City>().city_health -= 1;
            onDeath();
        }
    }

    //=================================
    //          Coroutines
    //=================================
    protected IEnumerator flash()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(.1f);
        renderer.material.color = new Color(255, 255, 255, 125);
        yield return new WaitForSeconds(.1f);
        renderer.material.color = Color.white;
    }


}
