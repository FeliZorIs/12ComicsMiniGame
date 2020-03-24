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


    void Start()
    {
        currentP = MenuBtnScript.currentUser;
        //User's current high score. Use this to compare for if we want to store the score or not.
        currentHigh = LoginDisp.highScore;
    }
    public void restartScene()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("TestMap");
        Time.timeScale = 1f;
    }

    public void returnToCustom()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("PlayerCustomization");
        Time.timeScale = 1f;
    }

    public void returnToMenu()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("PlayerMenu");
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
        WWW www = new WWW("https://web.njit.edu/~mrk38/saveScore.php", form);
        yield return www;
    }
}
