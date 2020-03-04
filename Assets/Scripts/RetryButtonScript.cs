using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButtonScript : MonoBehaviour
{

    public Button retryButton;
    public Button customButton;
    public void restartScene()
    {
        gameObject.SetActive(false);
        
        ScoreCount.scoreValue = 0;
        PlayerHealth.health = PlayerStats.healthLevel;
        SceneManager.LoadScene("TestMap");
    }

    public void returnToCustom()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Customization Screen");

    }
}
