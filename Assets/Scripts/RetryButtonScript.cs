using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButtonScript : MonoBehaviour
{
    public void restartScene()
    {
        gameObject.SetActive(false);
        
        ScoreCount.scoreValue = 0;
        PlayerHealth.health = 3;
        SceneManager.LoadScene("TestMap");
    }
}
