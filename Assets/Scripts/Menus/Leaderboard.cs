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
    public GameObject rankings;
    string theText;
    int currentHighest;
   


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dispEvery());
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

    /*
     * --COROUTINES!!!!--
     ******************
     */

    public IEnumerator dispEvery()
    {
        WWWForm form = new WWWForm();
       
        WWW www = new WWW("https://web.njit.edu/~mrk38/LeaderboardAll.php");
        // WWW www = new WWW("https://web.njit.edu/~rp553/LeaderboardAll.php");
        yield return www;
        
         string[] players = www.text.Split(',');
         for (int i = 0; i < players.Length; i++)
         {

             theText = theText + players[i] + "\r\n";
             // 
             //  Debug.Log("The value of a string: " + Sstats[i] + " is now an int that is: " + stats[i]);
         }
         rankings.GetComponent<Text>().text = theText;

    }
}
