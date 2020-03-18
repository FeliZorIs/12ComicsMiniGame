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
        login(username, password);
        if (validLogin == true)
        {
            failText.SetActive(false);
            currentUser = username;
            SceneManager.LoadScene("PlayerMenu"); //Loads PlayerMenu Scene
        }
        else
        {
            Debug.Log("User or pass is incorrect!");
            failText.SetActive(true);
            //Do nothing.
        }

    }

    //Gonna needa do some SQL stuff here to search through the DB for user and pass to confirm or deny entry.
    public bool login(string username, string password)
    {
        if ((username == "katherine") && (password == "password"))
        {
            validLogin = true;
        }
        else
        {
            validLogin = false;
        }
        return validLogin;
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
