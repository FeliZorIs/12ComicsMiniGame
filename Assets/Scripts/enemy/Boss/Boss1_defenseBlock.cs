using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_defenseBlock : MonoBehaviour
{
    public float speed;
    public bool up_down; //true = up, false = down

    enum Direction
    { 
        UP,
        DOWN
    } Direction direction;

    void Start()
    {
        if (up_down)
            direction = Direction.UP;
        else
            direction = Direction.DOWN;
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.UP:
                move_UP();
                if (this.transform.localPosition.y >= .6)
                    direction = Direction.DOWN;
                break;

            case Direction.DOWN:
                move_DOWN();
                if (this.transform.localPosition.y <= -.6)
                    direction = Direction.UP;
                break;
        }

        //move up
        if (this.transform.localPosition.y <= -.6)
            move_UP();

        //move down
        else if (this.transform.localPosition.y >= .6)
            move_DOWN();
    }

    void move_UP()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void move_DOWN()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void onTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player_shot")
        {
            Destroy(collision.gameObject);
        }
    }
}
