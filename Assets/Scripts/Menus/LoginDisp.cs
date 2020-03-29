﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginDisp : MonoBehaviour
{

    public GameObject userText;
    public GameObject heroNameText;
    public Image heroImg;
    public string curr;
    static public string currHero;
    public GameObject currentScore;
    static public int highScore;
    // Start is called before the first frame update
    void Start()
    {
        curr = MenuBtnScript.currentUser;
        showStats();
        userText.GetComponent<Text>().text = "Welcome " + curr + "!";
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Info will be pulled via SQL request.
    void showStats()
    {
        if (Resources.Load<Sprite>("Hero_UI_Images/" + curr) != null)
        {
            heroImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hero_UI_Images/" + curr);
        }
        //Default image if nothing exists. Maybe we can make our own?
        else
        {
            heroImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hero_UI_Images/12Comics_Logo");
        }

        StartCoroutine(display());
    }

    IEnumerator display()
    {

        //Also gonna need high score to be calculated here.
        WWWForm form = new WWWForm();
        form.AddField("username", curr);
        // WWW www = new WWW("https://web.njit.edu/~mrk38/MainMenu.php", form);
        WWW www = new WWW("https://web.njit.edu/~rp553/MainMenu.php", form);
        yield return www;

        string[] heroInfo = www.text.Split(',');

        if (heroInfo[0] == "")
        {
            heroNameText.GetComponent<Text>().text = "No hero name available!";
            currentScore.GetComponent<Text>().text = "no score available!";
            highScore = 0;

        }
        else
        {
            heroNameText.GetComponent<Text>().text = heroInfo[0];
            currentScore.GetComponent<Text>().text = "Current high score: " + heroInfo[1];
            highScore = int.Parse(heroInfo[1]);
            currHero = heroInfo[0];
        }

    }

}
