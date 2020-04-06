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

    //health kit drop declarations
    public GameObject medKit;
    public float odds;

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
        player = GameObject.Find("TestPlayer");
        multiBonus = PlayerStats.multiLevel;
        city = GameObject.Find("Despawn_Enemy");
        renderer = GetComponent<Renderer>();
        if (this.tag != "Gold_enemy")
            enemyManager.GetComponent<enemy_manager>().active_enemies.Add(this);


    }
    public void onDeath()
    {
        enemyManager.GetComponent<enemy_manager>().active_enemies.Remove(this);

        if (this.tag == "Gold_enemy")
        {
            //gold enemy shouldn't drop med kits
        }
        else 
        {
            dropMed();
        }

        Destroy(this.gameObject);
    }

    void dropMed()
    {
        float med_num = Random.RandomRange(1, 100);
        float xrange = Random.RandomRange(-3.5f, 4);
        //percent = 1%
        if (med_num <= odds)
        {
            Debug.Log("here you go");
            Instantiate(medKit, new Vector2(xrange, 5), Quaternion.identity);
        }
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
            enemy_health--;
            if (enemy_health <= 0)
            {
                ScoreCount.scoreValue += (10 * multiBonus);
                player.GetComponent<Player>().superMeterCharge(0.5f);
                killed_by_player();
            }

            else
            {
                StartCoroutine("flash");
            }
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
