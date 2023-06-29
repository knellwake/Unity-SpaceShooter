using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        int score = PlayerPrefs.GetInt("Score" + SceneManager.GetActiveScene().name, 0);
        scoreText.text = "Score: " + score;

        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        highScoreText.text = "HighScore: " + highScore;
    }
}