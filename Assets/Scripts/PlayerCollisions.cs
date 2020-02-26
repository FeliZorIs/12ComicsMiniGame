using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{

    public Transform HealthBarPlayer;
    HealthSystemPlayer healthSystem;
    private void Awake()
    {
       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystemPlayer(100);
        Transform healthBarTransform = Instantiate(HealthBarPlayer, new Vector3(250,505), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        HealthBarAni healthBar = healthBarTransform.GetComponent<HealthBarAni>();
        healthBar.Setup(healthSystem);

        //Test damage. This works on startup!
        healthSystem.Damage(10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onCollision(Collision2D coll)
    {
        //Taking damage from enemies.
        //Right now this is failing, if I set 'istrigger' to true on the testPlayer, then
        //the enemy dies, but the health doesn't change.
        //If istrigger is false, the player just presses up against the enemy.
        if (coll.gameObject.tag == "Enemy")
        {
            healthSystem.Damage(10);
            Destroy(coll.gameObject);
           
        }
    }
}
