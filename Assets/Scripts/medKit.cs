using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medKit : MonoBehaviour
{
    public int health;
    public float speed;
    public float drop;
    int count;
    Rigidbody2D rb;

    private void Start()
    {
        count = 0;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player_shot")
        {
            count++;
            speed += (drop + (.5f * count));
        }
    }
}
