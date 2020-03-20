﻿using System.Collections;
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

    //entrance
    bool inEntrance = true;

    //Stage 2
    public GameObject defenseBlock_1;
    public GameObject defenseBlock_2;

    //Stage 3
    public GameObject offenseBlock_1;
    public GameObject offenseBlock_2;

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

        defenseBlock_1.SetActive(false);
        defenseBlock_2.SetActive(false);
        offenseBlock_1.SetActive(false);
        offenseBlock_2.SetActive(false);

        bossStage = BossStage.ENTRANCE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bullet_spawn = new Vector2(this.transform.position.x - .5f, this.transform.position.y);

        switch (bossStage)
        {
            case BossStage.ENTRANCE:
                StartCoroutine("entrance_timer");
                break;

            case BossStage.STAGE_1:
                if (health <= stage_health*2)
                    bossStage = BossStage.STAGE_2;

                shoot_basic();
                break;

            case BossStage.STAGE_2:
                defenseBlock_1.SetActive(true);
                defenseBlock_2.SetActive(true);
                if (health <= stage_health)
                    bossStage = BossStage.STAGE_3;

                shoot_basic();
                break;

            case BossStage.STAGE_3:
                defenseBlock_1.SetActive(false);
                defenseBlock_2.SetActive(false);
                offenseBlock_1.SetActive(true);
                offenseBlock_2.SetActive(true);


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
        bossStage = BossStage.STAGE_1;
        isAlive = true;
        inEntrance = true;
        invince = true;
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

    //real entrance is an animation of the boss flying in from the right
    IEnumerator entrance_timer()
    {
        if (inEntrance)
        {
            invince = true;
            inEntrance = false;
            yield return new WaitForSeconds(2f);
            invince = false;
            bossStage = BossStage.STAGE_1;
        }
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