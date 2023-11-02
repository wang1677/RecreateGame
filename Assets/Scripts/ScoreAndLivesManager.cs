using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAndLivesManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Drag your Score Text object here in the inspector
    public GameObject[] lifeIndicators; // Drag your Life Indicator objects here in the inspector
    private int score = 0;
    private int lives = 3;  // assuming the player starts with 3 lives

    void Start()
    {
        UpdateScoreDisplay();
        UpdateLivesDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateLivesDisplay()
    {
        for (int i = 0; i < lifeIndicators.Length; i++)
        {
            lifeIndicators[i].SetActive(i < lives);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    public void LoseLife()
    {
        lives = Mathf.Max(lives - 1, 0);
        UpdateLivesDisplay();

        if (lives == 0)
        {
            // Game over logic 
        }
    }
}