using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
       // moveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Vector3 move = new Vector3(moveHorizontal, moveVertical, 0.0f);
       moveCharacter(new Vector2(moveHorizontal, moveVertical));
    }

    void moveCharacter(Vector2 direction)
    {
        player.Translate(direction * speed * Time.deltaTime);
    }

    
   
}
