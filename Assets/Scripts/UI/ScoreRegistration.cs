using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRegistration : MonoBehaviour
{
    // public TextMeshProUGUI textForRegistration;
    private void Start()
    {
        TextMeshProUGUI textForRegistration = GetComponent<TextMeshProUGUI>();
        EndGameManager.endManager.RegisterScoreText(textForRegistration);
        textForRegistration.text = "Score: 0";
    }
}