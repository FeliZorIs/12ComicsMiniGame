using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginDisp : MonoBehaviour
{

    public GameObject userText;
    // Start is called before the first frame update
    void Start()
    {
        userText.GetComponent<Text>().text = MenuBtnScript.currentUser + "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
