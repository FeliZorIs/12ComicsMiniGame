using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_offenseBlock : MonoBehaviour
{
    float myTime = 0;
    public float fireRate;
    public GameObject bullet;


    private void Start()
    {

    }

     void Update()
    {
        shoot_basic();
    }
    void shoot_basic()
    {
        myTime += Time.deltaTime;

        if (myTime >= fireRate)
        {
            //Debug.Log(Enemy.name + "has spawned");
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            myTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player_shot")
            Destroy(collision.gameObject);
    }
}
