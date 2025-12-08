using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;        
    public TMP_Text finalScoreText;  

    private float score;
    private bool isAlive = true;

    void Update()
    {
        if (!isAlive) return;

        score += 1 * Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    public void StopScore()
    {
        isAlive = false;


        if (finalScoreText != null)
        {
            finalScoreText.text = ((int)score).ToString();
        }
    }
}
