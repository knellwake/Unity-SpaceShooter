using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField] private int healAmount;
    [SerializeField] private AudioClip clipToPlay;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //加血
            PlayerStats player = col.GetComponent<PlayerStats>();
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            player.AddHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
