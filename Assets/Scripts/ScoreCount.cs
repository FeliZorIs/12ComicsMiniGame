using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{

    public static int scoreValue = 0;
    public Text scoreText;
    public Text multiplierText;
    public static int currentMulti;
    // Start is called before the first frame update
    void Start()
    {
        currentMulti = PlayerStats.multiLevel;
        multiplierText.text = "Multiplier: x" + currentMulti;
        scoreText.text = "Score: " + scoreValue;
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score: " + scoreValue;
    }


}
