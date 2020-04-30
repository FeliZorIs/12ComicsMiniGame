using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject Panel;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Panel.SetActive(false);
        }
    }
    
    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
            
            
        }

    }

    
}
    
