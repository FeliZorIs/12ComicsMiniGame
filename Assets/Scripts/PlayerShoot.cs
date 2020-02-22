using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate; //Amount of time to wait between shots (I believe this is in seconds, have to do more testing.)
    private float nextFire;
    private float myTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Jump") && myTime > nextFire) //Using 'jump' so we can use spacebar to fire. Guess we could also offer the option of mouse and WASD movement, wouldn't be hard.
        {
            nextFire = myTime + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            // create code here that animates the newProjectile

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }
}
