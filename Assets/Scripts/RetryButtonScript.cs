using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButtonScript : MonoBehaviour
{

    public Button retryButton;
    public Button customButton;
    public Button returnMenu;

    public GameObject city;
    public void restartScene()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("TestMap");
    }

    public void returnToCustom()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("PlayerCustomization");
    }

    public void returnToMenu()
    {
        resetStats();
        gameObject.SetActive(false);
        SceneManager.LoadScene("PlayerMenu");
    }


    void resetStats()
    {
        ScoreCount.scoreValue = 0;
        PlayerHealth.health = PlayerStats.healthLevel;
        city.GetComponent<City>().city_health = 3;
    }
}
