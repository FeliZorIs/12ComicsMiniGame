using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButtonScript : MonoBehaviour
{
    public Image GameOver;
    public Button retryButton;
    public Button customButton;
    public Button returnMenu;
    public string currentP;
    public GameObject city;
    static public int score;
    static public int currentHigh;


    //For scene fade.
    public GameObject fader;

    //Get audioManager components!
    GameObject audioManagerMusic;
    GameObject audioManagerSFX;

    void Start()
    {
        if(MenuBtnScript.debugOn == true)
        {
            //Randomness for debug purposes.
            currentP = "Katheryne";
            currentHigh = 99999;
        }
        else
        {
            currentP = MenuBtnScript.currentUser;
            //User's current high score. Use this to compare for if we want to store the score or not.
            currentHigh = LoginDisp.highScore;
            audioManagerMusic = GameObject.FindWithTag("MusicManager");
            audioManagerSFX = GameObject.FindWithTag("SFXManager");
        }
        
    }
    public void restartScene()
    {
        resetStats();
        gameObject.SetActive(false);
        fader.GetComponent<Scene_Fade>().FadeToLevel("TestMap");
        //SceneManager.LoadScene("TestMap");
        Time.timeScale = 1f;
    }

    public void returnToCustom()
    {
        resetStats();
        gameObject.SetActive(false);
        if (MenuBtnScript.debugOn == true)
        {

        }
        else
        {
            audioManagerMusic.GetComponent<AudioManager>().Stop("GameplayMusic_DAY");
            audioManagerMusic.GetComponent<AudioManager>().Stop("GameplayMusic_NIGHT");
            audioManagerMusic.GetComponent<AudioManager>().Play("MenuMusic");
        }
        fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerCustomization");
        //SceneManager.LoadScene("PlayerCustomization");
        Time.timeScale = 1f;
    }

    public void returnToMenu()
    {
        resetStats();
        gameObject.SetActive(false);
        if (MenuBtnScript.debugOn == true)
        {

        }
        else
        {
            audioManagerMusic.GetComponent<AudioManager>().Stop("GameplayMusic_DAY");
            audioManagerMusic.GetComponent<AudioManager>().Stop("GameplayMusic_NIGHT");
            audioManagerMusic.GetComponent<AudioManager>().Play("MenuMusic");
        }

        fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerMenu");
        // SceneManager.LoadScene("PlayerMenu");
        Time.timeScale = 1f;
    }


    void resetStats()
    {
        if (score > currentHigh)
        {
            StartCoroutine(saveScore());
        }
        ScoreCount.scoreValue = 0;
        PlayerHealth.health = PlayerStats.healthLevel;
        city.GetComponent<City>().city_health = 3;
        Time.timeScale = 1f;
    }

    IEnumerator saveScore()
    {

        WWWForm form = new WWWForm();
        form.AddField("username", currentP);
        form.AddField("score", score);
        //WWW www = new WWW("https://web.njit.edu/~mrk38/saveScore.php", form);
        WWW www = new WWW("https://web.njit.edu/~rp553/saveScore.php", form);
        yield return www;

        LoginDisp.highScore = score;
    }
}
