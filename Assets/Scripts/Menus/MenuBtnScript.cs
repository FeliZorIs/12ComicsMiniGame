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

    void Start()
    {
        
    }
    public void LoadMenu()
    {
        username = userInput.GetComponent<Text>().text;
        password = passInput.GetComponent<Text>().text;
        //Call this function to check the DB for valid credentials.
        StartCoroutine(Login(username, password));
        

    }

    //Gonna needa do some SQL stuff here to search through the DB for user and pass to confirm or deny entry.
    IEnumerator Login(string user, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", user);
        form.AddField("password", pass);
        WWW www = new WWW("https://web.njit.edu/~mrk38/MiniLogin.php", form);
        yield return www;

        if (www.text == "1")
        {
            validLogin = true;
            failText.SetActive(false);
            currentUser = username;
            SceneManager.LoadScene("PlayerMenu"); //Loads PlayerMenu Scene
            Debug.Log("Login successful! PHP: " + www.text);
        }
        else
        {
            Debug.Log("oh no , login failed. php message: " + www.text);
            Debug.Log("User or pass is incorrect!");
            failText.SetActive(true);
        }

    }


    public void PlayBtn()
    {
        
        SceneManager.LoadScene("TestMap");
    }

    public void CustomizeBtn()
    {
        
        SceneManager.LoadScene("PlayerCustomization");
    }

    public void LeaderboardsBtn()
    {
        
        SceneManager.LoadScene("Leaderboards");
    }

    public void LogoutBtn()
    {
        
        SceneManager.LoadScene("PlayerLogin");
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("PlayerMenu");
    }
}
