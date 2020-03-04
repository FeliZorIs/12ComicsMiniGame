using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] [Range (0f, 5f)] float lerpTime;
    [SerializeField] Vector2[] myPositions;
    int posIndex = 0;
    int length;
    float t = 0;

    Renderer renderer;
    public int health;
    public bool invince;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);

        //movement();
    }

    void movement()
    {
        transform.position = Vector2.Lerp(transform.position, myPositions[posIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            posIndex++;
            posIndex = (posIndex >= length) ? 0 : posIndex;
        }
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
