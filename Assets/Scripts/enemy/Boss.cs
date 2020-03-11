using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Renderer renderer;
    public int health;
    int maxHealth;
    [SerializeField]int stage_health;
    [SerializeField]bool invince;
    bool isAlive;

    enum BossStage
    {
        STAGE_1,
        STAGE_2,
        STAGE_3
    } BossStage bossStage;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        maxHealth = health;
        stage_health = maxHealth / 3;
        renderer = GetComponent<Renderer>();
        bossStage = BossStage.STAGE_1;

        Debug.Log(stage_health);
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossStage)
        {
            case BossStage.STAGE_1:
                Debug.Log("1");
                if (health <= stage_health*2)
                    bossStage = BossStage.STAGE_2;
                break;

            case BossStage.STAGE_2:
                Debug.Log("2");
                if (health <= stage_health)
                    bossStage = BossStage.STAGE_3;
                break;

            case BossStage.STAGE_3:
                Debug.Log("3");
                break;
        }

        if (health <= 0)
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }
    }

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

    IEnumerator flash()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(.1f);
        renderer.material.color = new Color(255,255,255, 125);
        yield return new WaitForSeconds(.1f);
        renderer.material.color = Color.white;        
    }

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
