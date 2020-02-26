using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement declarations
    public float        speed;
    float               horizontal, 
                        vertical;
    Vector2             direction;

    //shooting declarations
    public GameObject   shot;
    public Transform    shot_spawn;
    public float        fireRate;
    private float       nextFire;
    private float       shot_time = 0f;

    void FixedUpdate()
    {
        Movement();
        shoot();
    }

    //====================================
    //              functions
    //====================================

    /*
     * @brief this allows the character to move around the screen
     */
    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector2(horizontal, vertical);

        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    /*
     * @brief shooting the basic ammo type
     */
    void shoot()
    {
        shot_time = shot_time + Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && shot_time > nextFire)
        {
            nextFire = shot_time + fireRate;
            Instantiate(shot, shot_spawn.position, shot_spawn.rotation);

            nextFire = nextFire - shot_time;
            shot_time = 0f;
        }
    }

    //======================================
    //              collisions
    //======================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when player collides with an BasicEnemy
        if (collision.gameObject.tag == "BasicEnemy")
        {
            PlayerHealth.health -= 1;
            Destroy(collision.gameObject);
        }
    }
}
