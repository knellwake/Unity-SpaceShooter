using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private float timer;
    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawner;

    private void Update()
    {
        if (EndGameManager.endManager.gameOver == true)
            return;
        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].SetActive(false);
            }
            EndGameManager.endManager.StartResolveSequence();
            gameObject.SetActive(false);
        }
    }
}