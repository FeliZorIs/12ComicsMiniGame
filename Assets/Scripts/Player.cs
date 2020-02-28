using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float       myTime = 0f;

    int                 bultype = 1;
    public Vector3      bullet_rotation1;
    public Vector3      bullet_rotation2;
    public Text         shot_Text;

    //Invincibility after hit
    Renderer rend;
    Color color;

    //For gameOver Button
    public GameObject gameOverPrefab;


    void Start()
    {
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        gameOverPrefab.SetActive(false);
    }
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
     * @brief shooting and switching the ammo types
     */
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bultype = 1;
            shot_Text.text = "Shot Type: basic";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bultype = 2;
            shot_Text.text = "Shot Type: 3 split";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bultype = 3;
            shot_Text.text = "Shot Type: N/A";
        }

        myTime = myTime + Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && myTime > nextFire)
        {   
            switch (bultype)
            {
                case 1:
                    nextFire = myTime + fireRate;
                    Instantiate(shot, shot_spawn.position, shot_spawn.rotation);                    
                    break;

                case 2:
                    nextFire = myTime + fireRate;
                    Instantiate(shot, shot_spawn.position, Quaternion.identity);
                    GameObject ex1 = (GameObject)(Instantiate(shot, shot_spawn.position, Quaternion.EulerAngles(bullet_rotation1)));
                    GameObject ex2 = (GameObject)(Instantiate(shot, shot_spawn.position, Quaternion.EulerAngles(bullet_rotation2)));
                    break;

                case 3:
                    break;
            }
        }
        nextFire = nextFire - myTime;
        myTime = 0f;
    }

    //======================================
    //              collisions
    //======================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when player collides with a BasicEnemy
        if (collision.gameObject.tag == "BasicEnemy")
        {
            PlayerHealth.health -= 1;
            if (PlayerHealth.health > 0)
            {
                StartCoroutine("PlayerInvince");
            }
            if(PlayerHealth.health<= 0)
            {
                gameOver();
            }
            Destroy(collision.gameObject);
        }
    }

    //Invincibility state when getting hit.
    IEnumerator PlayerInvince()
    {
        Physics2D.IgnoreLayerCollision(0, 10, true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(2.4f);
        Physics2D.IgnoreLayerCollision(0,10, false);
        color.a = 1f;
        rend.material.color = color;
    }

    //======================================
    //              PlayerDeath
    //======================================

    private void gameOver() 
    {
        PlayerHealth.health = 0;
        gameOverPrefab.SetActive(true);
        Destroy(gameObject);
    }        
}
