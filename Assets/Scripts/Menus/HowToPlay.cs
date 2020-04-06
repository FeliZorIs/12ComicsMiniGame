using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour

{

    public GameObject How2Play;
    public Button h2p_button;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;


    // Start is called before the first frame update
    void Start()
    {
        How2Play.SetActive(false);
        P1.SetActive(true);
        P2.SetActive(false);
        P3.SetActive(false);
        P4.SetActive(false);
        P5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (How2Play.activeSelf)
        {
            if (P1.activeSelf) {
                if (Input.GetKeyDown("right"))
                {
                    P1.SetActive(false);
                    P2.SetActive(true);
                    P3.SetActive(false);
                    P4.SetActive(false);
                    P5.SetActive(false);
                }
            }

            else if (P2.activeSelf)
            {
                if (Input.GetKeyDown("right"))
                {
                    P1.SetActive(false);
                    P2.SetActive(false);
                    P3.SetActive(true);
                    P4.SetActive(false);
                    P5.SetActive(false);
                }

                if (Input.GetKeyDown("left"))
                {
                    P1.SetActive(true);
                    P2.SetActive(false);
                    P3.SetActive(false);
                    P4.SetActive(false);
                    P5.SetActive(false);
                }
            }

            else if (P3.activeSelf)
            {
                if (Input.GetKeyDown("right"))
                {
                    P1.SetActive(false);
                    P2.SetActive(false);
                    P3.SetActive(false);
                    P4.SetActive(true);
                    P5.SetActive(false);
                }

                if (Input.GetKeyDown("left"))
                {
                    P1.SetActive(false);
                    P2.SetActive(true);
                    P3.SetActive(false);
                    P4.SetActive(false);
                    P5.SetActive(false);
                }
            }

            else if (P4.activeSelf)
            {
                if (Input.GetKeyDown("right"))
                {
                    P1.SetActive(false);
                    P2.SetActive(false);
                    P3.SetActive(false);
                    P4.SetActive(false);
                    P5.SetActive(true);
                }

                if (Input.GetKeyDown("left"))
                {
                    P1.SetActive(false);
                    P2.SetActive(false);
                    P3.SetActive(true);
                    P4.SetActive(false);
                    P5.SetActive(false);
                }
            }

            else if (P5.activeSelf)
            {
                if (Input.GetKeyDown("left"))
                {
                    P1.SetActive(false);
                    P2.SetActive(false);
                    P3.SetActive(false);
                    P4.SetActive(true);
                    P5.SetActive(false);
                }
            }


            if (Input.GetKey("escape"))
            {
                How2Play.SetActive(false);
            }
        }
    }

    public void H2P()
    {
        How2Play.SetActive(true);
    }
}
