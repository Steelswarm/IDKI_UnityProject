using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int playerScore;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        //Reset Player Score
        playerScore = 0;
    }

    public void UpdateScore(int points)
    {
        //Update Player Score and UI
        playerScore += points;
	    scoreText.text = "Score: " +playerScore.ToString();
    }

}
