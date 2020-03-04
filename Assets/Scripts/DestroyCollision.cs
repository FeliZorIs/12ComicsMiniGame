using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    public float speed = 2;
    public static int scoreMulti;

    // Start is called before the first frame update
    void Start()
    {
        scoreMulti = PlayerStats.multiLevel;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true) 
        {
            Destroy(gameObject);
        }

        if(other.transform.tag == "playerShot")
        {
            //Calculate score on enemy death determined by base of 10 times whatever the current multiplier is. (WE CAN CHANGE THIS WHEN BALANCING, DON'T WORRY!)
            ScoreCount.scoreValue += (10*scoreMulti);
            Destroy(gameObject);
        }
       
        if (other.transform.tag == "Despawner")
        {
            Destroy(this.gameObject);
        }

    }
}
