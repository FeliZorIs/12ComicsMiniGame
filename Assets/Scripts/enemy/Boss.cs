using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Renderer renderer;
    public int health;
    int maxHealth;
    int stage_health;
    bool invince;
    bool isAlive;
    Animator anim;

    //basic shooting declarations
    public GameObject bullet;
    Vector2 bullet_spawn;
    public float fireRate;
    private float myTime = 0f;

    enum BossStage
    {
        ENTRANCE,
        STAGE_1,
        STAGE_2,
        STAGE_3
    } BossStage bossStage;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        isAlive = true;
        maxHealth = health;
        stage_health = maxHealth / 3;

        bossStage = BossStage.ENTRANCE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bullet_spawn = new Vector2(this.transform.position.x - .5f, this.transform.position.y);

        switch (bossStage)
        {
            case BossStage.ENTRANCE:
                //going from off stage to on stage

                break;

            case BossStage.STAGE_1:
                if (health <= stage_health*2)
                    bossStage = BossStage.STAGE_2;

                shoot_basic();
                break;

            case BossStage.STAGE_2:
                if (health <= stage_health)
                    bossStage = BossStage.STAGE_3;

                shoot_basic();
                break;

            case BossStage.STAGE_3:
                shoot_basic();
                break;
        }

        if (health <= 0)
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }
    }

    //=================================
    //          Functions
    //=================================

    //is called in enemy_spawner
    public bool isAlive_check()
    {
        return isAlive;
    }

    //is called in enemy_spawner
    public void reset_boss()
    {
        health = maxHealth;
        isAlive = true;
        bossStage = BossStage.STAGE_1;
    }

    // allows the Boss to shoot at the player with basic shots
    void shoot_basic()
    {
        myTime += Time.deltaTime;

        if (myTime >= fireRate)
        {
            //Debug.Log(Enemy.name + "has spawned");
            Instantiate(bullet, bullet_spawn, Quaternion.identity);
            myTime = 0;
        }
    }

    //=================================
    //          Coroutines
    //=================================

    IEnumerator flash()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(.1f);
        renderer.material.color = new Color(255,255,255, 125);
        yield return new WaitForSeconds(.1f);
        renderer.material.color = Color.white;        
    }
    
    //=================================
    //          Collision
    //=================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player_shot")
        {
            if (invince)
            {
                Destroy(collision.gameObject);
            }
            else 
            {
                health--;
                StartCoroutine("flash");
                Destroy(collision.gameObject);
            }
        }
    }
}
