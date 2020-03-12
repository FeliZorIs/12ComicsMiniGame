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

    public void LoadMenu()
    {
        
        SceneManager.LoadScene("PlayerMenu"); //Loads PlayerMenu Scene
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
