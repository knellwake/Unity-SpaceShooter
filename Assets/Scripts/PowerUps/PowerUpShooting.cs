using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShooting : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerShooting player = col.GetComponent<PlayerShooting>();
            player.IncreaseUpgrade(1);
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            Destroy(gameObject);
        }
    }
}
