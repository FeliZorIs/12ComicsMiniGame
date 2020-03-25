using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{

    public Button Personal;
    public Button MySchool;
    public Button Global;
    public GameObject Search;
    public GameObject CurrentRank;


    // Start is called before the first frame update
    void Start()
    {
        Search.SetActive(false);
        CurrentRank.SetActive(false);
    }

    public void PersonalBoard()
    {
        Search.SetActive(false);
        CurrentRank.SetActive(false);
    }

    public void MySchoolBoard()
    {
        Search.SetActive(true);
        CurrentRank.SetActive(true);
    }

    public void GlobalBoard()
    {
        Search.SetActive(true);
        CurrentRank.SetActive(true);
    }
}
