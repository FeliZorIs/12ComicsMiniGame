using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtnScript : MonoBehaviour
{

    public Button loginButton;
    public Button PlayButton;
    public Button CustomizeButton;
    public Button LeaderboardsButton;
    public Button logoutButton;
    public Button backButton;


    //Check for login.
    public string username;
    public string password;
    public GameObject userInput;
    public GameObject passInput;
    public GameObject failText;
    bool validLogin = false;


    //Store userinfo
    static public string currentUser;

    //For scene fade.
    public GameObject fader;
    void Start()
    {

    }
    public void LoadMenu()
    {
        username = userInput.GetComponent<InputField>().text;
        password = passInput.GetComponent<InputField>().text;
        //Call this function to check the DB for valid credentials.
        StartCoroutine(Login(username, password));


    }


    IEnumerator Login(string user, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", user);
        form.AddField("password", pass);
        //WWW www = new WWW("https://web.njit.edu/~mrk38/MiniLogin.php", form);
        WWW www = new WWW("https://web.njit.edu/~rp553/MiniLogin.php", form);
        yield return www;

        if (www.text == "1")
        {
            validLogin = true;
            failText.SetActive(false);
            currentUser = username;
            fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerMenu");
            // SceneManager.LoadScene("PlayerMenu"); //Loads PlayerMenu Scene
            Debug.Log("Login successful! PHP: " + www.text);
        }
        else
        {
            Debug.Log("oh no , login failed. php message: " + www.text);
            Debug.Log("User or pass is incorrect!");
            failText.SetActive(true);
        }

    }

    public IEnumerator grabStats()
    {

        WWWForm form2 = new WWWForm();
        form2.AddField("username", currentUser);
        //WWW www = new WWW("https://web.njit.edu/~mrk38/PlayerStats.php", form2);
        WWW www2 = new WWW("https://web.njit.edu/~rp553/PlayerStats.php", form2);
        yield return www2;

        //Grab the array from PHP , using commas to split each value. Convert them to Integers so we can use 'em in Unity for stat manip.
        string[] Sstats = www2.text.Split(',');
        int[] stats = new int[Sstats.Length];
        for (int i = 0; i < Sstats.Length; i++)
        {
            stats[i] = int.Parse(Sstats[i]);
            //  Debug.Log("The value of a string: " + Sstats[i] + " is now an int that is: " + stats[i]);
        }

        // Debug.Log(Sstats.Length);


        PlayerStats.healthLevel = stats[0];
        PlayerStats.ammoLevel = stats[1];
        PlayerStats.superMeterLevel = stats[2];
        PlayerStats.multiLevel = stats[3];
        PlayerStats.pointsRemaining = stats[4];
        PlayerStats.maxPoints = stats[5];
        fader.GetComponent<Scene_Fade>().FadeToLevel("TestMap");
        // SceneManager.LoadScene("TestMap");
    }

    void FixedUpdate()
    {
        
    }


    public void PlayBtn()
    {
        StartCoroutine(grabStats());

    }

    public void CustomizeBtn()
    {
        fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerCustomization");
        //SceneManager.LoadScene("PlayerCustomization");
    }

    public void LeaderboardsBtn()
    {
        fader.GetComponent<Scene_Fade>().FadeToLevel("Leaderboards");
        //SceneManager.LoadScene("Leaderboards");
    }

    public void LogoutBtn()
    {
        fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerLogin");
        // SceneManager.LoadScene("PlayerLogin");
    }

    public void BackBtn()
    {
        fader.GetComponent<Scene_Fade>().FadeToLevel("PlayerMenu");
        // SceneManager.LoadScene("PlayerMenu");
    }

    public void CreditsBtn()
    {
        fader.GetComponent<Scene_Fade>().FadeToLevel("Credits");
    }

}