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

    public GameObject city;

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
        ScoreCount.scoreValue = 0;
        PlayerHealth.health = PlayerStats.healthLevel;
        city.GetComponent<City>().city_health = 3;
        Time.timeScale = 1f;
    }
}
