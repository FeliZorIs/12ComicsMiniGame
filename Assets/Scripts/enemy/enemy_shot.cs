﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shot : MonoBehaviour
{
    float timer;
    public float ttk = 2;
    public float speed; //Speed the shot will travel.
    // Start is called before the first frame update

    void Start()
    {
        timer = 0;
        GetComponent<Rigidbody2D>().velocity = transform.right * -speed; //Moving the shot across the screen.
    }

    // Update is called once per frame
    void Update()
    {

    }

    void selfDestruct()
    {
        timer += Time.deltaTime;
        if (timer >= ttk)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Despawner")
        {
            Destroy(this.gameObject);
        }
    }
}
