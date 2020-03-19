using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginDisp : MonoBehaviour
{

    public GameObject userText;
    public Image heroImg;
    public string curr;
    // Start is called before the first frame update
    void Start()
    {
        curr = MenuBtnScript.currentUser;
        showStats();
        userText.GetComponent<Text>().text = curr + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Info will be pulled via SQL request.
    void showStats() 
    {
        if (curr == "katherine")
        {
            heroImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Katheryne Kat Warior");
        }
        else if (curr == "msLinder")
        {
            heroImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ms  Linder copy");
        }
        else
        {
            heroImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Katheryne Kat Warior");
        }
    }
    
}
