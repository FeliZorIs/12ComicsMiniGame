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
    public GameObject returnToMenuBtn;
    public GameObject GameOverUI;

    //Customization stats being accounted for here.
    static public int ammolvl;
    
    static public int multiplierlvl;

    //Supermeter vars
    static public int supermeterlvl;
    public float superCast;
    public float superMeterCurrent;
    public Text superMeterText;
    public GameObject enemyManager;
    public SupermeterBar superBar;

    //Shake on City Hit vars
    public City city;
    public int current_cityHealth;
    public CameraShake cameraShake;

    //Shot type icon vars
    public Image single;
    public Image split;
    public Image burst;


    //Figuring out who currentUser is and getting ready to change images based on that.
    public string current;
    void Start()
    {
        current = MenuBtnScript.currentUser;
        userInitialize();
        enemyManager = GameObject.Find("EnemyManager");
        ammolvl = PlayerStats.ammoLevel;
        supermeterlvl = PlayerStats.superMeterLevel;
        superCast = (float)supermeterlvl;
        multiplierlvl = PlayerStats.multiLevel;
        superMeterCurrent = 0;
        superMeterText.text = "Supermeter: " + superMeterCurrent + "%";
        rend = GetComponent<Renderer>();
        color = rend.material.color;

        gameOverPrefab.SetActive(false);
        returnCustomButton.SetActive(false);
        returnToMenuBtn.SetActive(false);
        GameOverUI.SetActive(false);

        bullet_rotation1 = new Vector3(0, 0, 12);
        bullet_rotation2 = new Vector3(0, 0, -12);


        current_cityHealth = city.city_health;

        superBar.SetMaxSuper(100);
    }

    void FixedUpdate()
    {
        Movement();
        shoot();
        superMeterUse();

        superBar.SetSuper(superMeterCurrent);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -7.7f, 4.7f),
            Mathf.Clamp(transform.position.y, -4.2f, 2.9f)
            );

        if (city.city_health < current_cityHealth)
        {
            StartCoroutine(cameraShake.Shake(.15f, .4f));
            current_cityHealth = city.city_health;
        }
    }

    //====================================
    //              functions
    //====================================

    /*
     * @brief this allows the character to move around the screen
     */

    //Who are we playing as. Change UI images, shot graphics, etc.
    void userInitialize()
    {
        //This will be an SQL statement when we get the DB up.
        if (Resources.Load<Sprite>("Hero_UI_Images/" + current) != null)
        {
            playerImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hero_UI_Images/" + current);
        }
        else
        {
            playerImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hero_UI_Images/12Comics_Logo");
        }
    }
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
                //shot_Text.text = "Shot Type: Basic";
               
                single.enabled = true;
                split.enabled = false;
                burst.enabled = false;


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

                single.enabled = true;
                split.enabled = true;
                burst.enabled = false;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    bultype = 1;
                    //shot_Text.text = "Shot Type: Basic";
                    single.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    split.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    bultype = 2;
                    //shot_Text.text = "Shot Type: 3 Split";
                    single.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                    split.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
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

                single.enabled = true;
                split.enabled = true;
                burst.enabled = true;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    bultype = 1;
                    //shot_Text.text = "Shot Type: Basic";
                    single.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    split.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                    burst.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    bultype = 2;
                    //shot_Text.text = "Shot Type: 3 Split";
                    single.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                    split.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                    burst.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    bultype = 3;
                    //shot_Text.text = "Shot Type: Burst";
                    single.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                    split.GetComponent<Image>().color = new Color32(55, 55, 55, 255);
                    burst.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
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
                            nextFire = myTime + (fireRate + 0.3f);
                            StartCoroutine("BurstShot");
                            break;
                    }
                }
                break;

        }

        nextFire = nextFire - myTime;
        myTime = 0f;
    }

    //Burst shot couroutine.
    public IEnumerator BurstShot()
    {
        for (int i = 0; i <= 3; i++ )
        {
            Instantiate(shot, shot_spawn.position, shot_spawn.rotation);
            yield return new WaitForSeconds(0.05f);
        }
          
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
            if(PlayerHealth.health <= 0)
            {
                gameOver();
            }
            Destroy(collision.gameObject);
            
        }

        //player collides with boss
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player collides with enemy bullet
        if (collision.gameObject.tag == "enemy_shot")
        {
            PlayerHealth.health -= 1;
            StartCoroutine("PicUIDamage");
            if (PlayerHealth.health > 0)
            {
                StartCoroutine("PlayerInvince");
            }
            if (PlayerHealth.health <= 0)
            {
                gameOver();
            }
            Destroy(collision.gameObject);
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
        RetryButtonScript.score = ScoreCount.scoreValue;
        gameOverPrefab.SetActive(true);
        returnCustomButton.SetActive(true);
        returnToMenuBtn.SetActive(true);
        GameOverUI.SetActive(true);
        //Destroy all player shots so there are no collisions after player dies.
        GameObject[] theShots = GameObject.FindGameObjectsWithTag("player_shot");
        for (int i = 0; i < theShots.Length; i++)
        {
            GameObject.Destroy(theShots[i]);
        }
        Destroy(gameObject);
        
    }


    //======================================
    //              SuperMeter
    //======================================

    //Charge the supermeter based on the superMeter level.
    //Since we're doing shot collision seperately i'm making this public. We'll change it to private later.
    public void superMeterCharge(float s)
    {
        if (superMeterCurrent < 100f) 
        {
            superMeterCurrent += s*supermeterlvl;
            if (superMeterCurrent >= 100f)
            {
                superMeterCurrent = 100f;
            }
            superMeterText.text = "Supermeter: " + superMeterCurrent + "%";
        }
        else
        {
            superMeterCurrent = 100f;
            superMeterText.text = "Supermeter: " + superMeterCurrent + "%";
        }
    }

    //Use the supermeter and reset it's value.
    private void superMeterUse()
    {

        if (Input.GetKeyDown(KeyCode.Alpha4)){
            if(superMeterCurrent >= 100f) 
            {
                 GameObject[] enemies = GameObject.FindGameObjectsWithTag("BasicEnemy");
                 for (int i = 0; i < enemies.Length; i++)
                 {
                     enemies[i].GetComponent<enemy>().onDeath();
                     ScoreCount.scoreValue += (5 * multiplierlvl);
                     GameObject.Destroy(enemies[i]);
                     enemyManager.GetComponent<enemy_manager>().enemiesKilled_total += 1;
                     enemyManager.GetComponent<enemy_manager>().enemiesKilled_current += 1;
                 }
                 superMeterCurrent = 0f;
                 superMeterText.text = "Supermeter: " + superMeterCurrent + "%";
            }
            else 
            {
                Debug.Log("Supermeter isn't full yet!");
            }
        }
    }



}
