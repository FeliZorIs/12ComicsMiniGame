using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Leaderboard : MonoBehaviour
{

    public Button Personal;
    public Button MySchool;
    public Button Global;
    public GameObject Search;
    public GameObject CurrentRank;
    public GameObject rankings;
    static public string theCurrentUser;
    static public string theCurrentHero;
    List<string> theNames = new List<string>();
    List<int> theScores = new List<int>();

    string theText;
    string theRank;
    string theHero;
    string theScr;

    int currentHighScore;
    int playerRank;



    // Start is called before the first frame update
    void Start()
    {
        theNames = new List<string>();
        theScores = new List<int>();
        currentHighScore = LoginDisp.highScore;
        theCurrentUser = MenuBtnScript.currentUser;
        theCurrentHero = LoginDisp.currHero;
        StartCoroutine(getNames());
        //StartCoroutine(getScores());
        Search.SetActive(false);
        // CurrentRank.SetActive(false);

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
        StartCoroutine(getNames());
        StartCoroutine(getScores());
        Search.SetActive(true);
        CurrentRank.SetActive(true);
    }

    /*
     * --COROUTINES!!!!--
     ******************
     */

    public IEnumerator getNames()
    {
        /*
         * THE ARRAY PASSED DOWN FROM PHP IS MADE UP OF THE USERNAME AND HERO NAME. 
         * IF YOU WANT JUST THE USERNAME/HERO NAME, WE'LL PROBABLY NEED TO EDIT THE PHP FILE
         * AND CREATE ANOTHER COROUTINE TO GRAB THAT INFO SEPERATELY.
         */

        //WWW www = new WWW("https://web.njit.edu/~mrk38/LeaderboardNamesAll.php");
        WWW www = new WWW("https://web.njit.edu/~rp553/LeaderboardNamesAll.php");
        yield return www;

        string[] players = www.text.Split(',');


        for (int i = 0; i < players.Length; i++)
        {
            theNames.Add(players[i]);

        }

        //Wait here before calling getScores because otherwise the name list may not be fully populated yet!
        yield return new WaitForFixedUpdate();
        StartCoroutine(getScores());

    }

    public IEnumerator getScores()
    {

        //WWW www2 = new WWW("https://web.njit.edu/~mrk38/LeaderboardScoresAll.php");
        WWW www2 = new WWW("https://web.njit.edu/~rp553/LeaderboardScoresAll.php");
        yield return www2;

        string[] scores = www2.text.Split(',');

        for (int i = 0; i < scores.Length; i++)
        {
            theScores.Add((int.Parse(scores[i])));
        }
        //Call the function to display the ranks from the database.

        sortRanks();
    }

    //Sort the scores and display them.
    void sortRanks()
    {
        //Take the lists that were formed via the Coroutine, and use them to print the leaderboard data to the screen in descending order.
        //Also rank is simply determined by adding 1 each time the value is printed.

        int numList;
        int currentNum;
        string currentName;
        numList = theScores.Count;

        for (int i = 0; i < numList - 1; i++)
        {
            for (int j = i + 1; j < numList; j++)
            {
                if (theScores[i] < theScores[j])
                {
                    currentNum = theScores[i];
                    currentName = theNames[i];
                    theScores[i] = theScores[j];
                    theScores[j] = currentNum;
                    theNames[i] = theNames[j];
                    theNames[j] = currentName;

                }
            }

            //theText = theText + "\t" + (i + 1) + "\t" + theNames[i] + "\t" + "\t" + theScores[i] + "\n";
            theText = theText + "\t \t" + (i + 1) + "\t \t \t \t" + theNames[i] + "\t" + "\t \t \t \t" + theScores[i] + " \t \t \t \n";


        }

        playerRank = theScores.IndexOf(currentHighScore);
        Debug.Log("My rank is: " + (playerRank + 1));
        rankings.GetComponent<Text>().text = theText;
        CurrentRank.GetComponent<Text>().text = "" + (playerRank + 1);


        /*
         OLD CODE BUT MAY BE USEFUL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        int[] finalScores = new int[numList];
        string[] finalNames = new string[numList];

        for(int r = 0; r<numList; r++)
        {
            finalScores[r] = theScores[r];
            finalNames[r] = theNames[r];
           
        }

        for (int i = 0; i < finalScores.Length - 1; i++)
        {
            for (int j = i + 1; j < finalScores.Length; j++ )
            {
                if(finalScores[i] < finalScores[j])
                {
                    currentNum = finalScores[i];
                    currentName = finalNames[i]; 
                    finalScores[i] = finalScores[j];
                    finalScores[j] = currentNum;
                    finalNames[i] = finalNames[j];
                    finalNames[j] = currentName;
                    
                }
            }
            Debug.Log("The new output is: " + finalNames[i] + " " + finalScores[i]);
            theText = theText + "\t" + (i + 1) + "\t" + finalNames[i] + "\t" + "\t" + finalScores[i] + "\n";
        }
        rankings.GetComponent<Text>().text = theText;
        */

    }


}