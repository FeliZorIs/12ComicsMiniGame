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
    Vector3      bullet_rotation1;
    Vector3      bullet_rotation2;
    public Text         shot_Text;

    //Invincibility after hit & Player UI image damage
    Renderer rend;
    Color color;
   public Image playerImg;

    //For gameOver Buttons
    public GameObject gameOverPrefab;
    public GameObject returnCustomButton;

    //Customization stats being accounted for here.
    static public int ammolvl;
    
    static public int multiplierlvl;

    //Supermeter vars
    static public int supermeterlvl;
    public int superMeterCurrent;
    public Text superMeterText;

  
    void Start()
    {

        ammolvl = PlayerStats.ammoLevel;
        supermeterlvl = PlayerStats.superMeterLevel;
        multiplierlvl = PlayerStats.multiLevel;
        superMeterCurrent = 0;
        superMeterText.text = "Supermeter Charge: " + superMeterCurrent + "%";

        rend = GetComponent<Renderer>();
        color = rend.material.color;
        gameOverPrefab.SetActive(false);
        returnCustomButton.SetActive(false);
        bullet_rotation1 = new Vector3(0, 0, 12);
        bullet_rotation2 = new Vector3(0, 0, -12);
    }
    void FixedUpdate()
    {
        Movement();
        shoot();
        superMeterUse();

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -7.7f, 4.7f),
            Mathf.Clamp(transform.position.y, -4.2f, 4.2f));
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

        //Temp statement just to show that different ammo levels affect the type of shots you can use
        //This can probably be condensed. Let me know if you guys figure out a way to do so.
        switch (ammolvl)
        {

            //Level 1, only access to basic shot.
            case 1:
                bultype = 1;
                shot_Text.text = "Shot Type: basic";

                myTime = myTime + Time.deltaTime;
                if (Input.GetKey(KeyCode.Space) && myTime > nextFire)
                {
                    nextFire = myTime + fireRate;
                    Instantiate(shot, shot_spawn.position, shot_spawn.rotation);
                    break;
                }
                break;

            //Level 2, access to basic shot and split shot.
            case 2:
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
                    }
                }
                break;

            //Level 3, access to third shot type as well.
            case 3:
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
                break;

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
            collision.gameObject.GetComponent<enemy>().onDeath();
            PlayerHealth.health -= 1;
            StartCoroutine("PicUIDamage");
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

        if (collision.gameObject.tag == "Boss")
        {
            PlayerHealth.health -= 1;
            StartCoroutine("PicUIDamage");
            collision.gameObject.GetComponent<Boss>().health -= 1;
            this.transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
            if (PlayerHealth.health > 0)
            {
                StartCoroutine("PlayerInvince");
            }
            if (PlayerHealth.health <= 0)
            {
                gameOver();
            }
        }
    }

    //Player U.I. turns red during damage.
    public IEnumerator PicUIDamage()
    {
        playerImg.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1.0f);
        playerImg.GetComponent<Image>().color = new Color(255,255,255);
    }

    //Invincibility state when getting hit.
    public IEnumerator PlayerInvince()
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

    public void gameOver() 
    {
        PlayerHealth.health = 0;
        gameOverPrefab.SetActive(true);
        returnCustomButton.SetActive(true);
        Destroy(gameObject);
    }


    //======================================
    //              SuperMeter
    //======================================

    //Charge the supermeter based on the superMeter level.
    //Since we're doing shot collision seperately i'm making this public. We'll change it to private later.
    public void superMeterCharge(int s)
    {
        if (superMeterCurrent < 100) 
        {
            superMeterCurrent += s*supermeterlvl;
            if (superMeterCurrent >= 100)
            {
                superMeterCurrent = 100;
            }
            superMeterText.text = "Supermeter Charge: " + superMeterCurrent + "%";
        }
        else
        {
            superMeterCurrent = 100;
            superMeterText.text = "Supermeter Charge: " + superMeterCurrent + "%";
        }
    }

    //Use the supermeter and reset it's value.
    private void superMeterUse()
    {

        if (Input.GetKeyDown(KeyCode.Alpha4)){
            if(superMeterCurrent >= 100) {
                 GameObject[] enemies = GameObject.FindGameObjectsWithTag("BasicEnemy");
                 foreach (GameObject target in enemies)
                 {
                    GameObject.Destroy(target);
                 }
                 superMeterCurrent = 0;
                 superMeterText.text = "Supermeter Charge: " + superMeterCurrent + "%";
            }
            else {
                Debug.Log("Supermeter isn't full yet!");
            }
        }
    }
    
}
