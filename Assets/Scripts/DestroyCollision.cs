using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
    public float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (true) //For whatever reason, other.gameObject.CompareTag("BasicEnemy") isn't working. Check it out so the projectiles don't destroy boundaries.
        {
            ScoreCount.scoreValue += 10;
            Destroy(gameObject);
        }

       
        if (other.transform.tag == "Despawner")
        {
            Destroy(this.gameObject);
        }
    }
}
