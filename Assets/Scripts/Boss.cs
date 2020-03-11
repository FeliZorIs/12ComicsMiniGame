using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Renderer renderer;
    public int health;
    int maxHealth;
    public bool invince;
    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        maxHealth = health;
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
