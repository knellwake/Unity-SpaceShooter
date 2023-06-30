using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //加血
            PlayerStats player = col.GetComponent<PlayerStats>();
            player.AddHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
