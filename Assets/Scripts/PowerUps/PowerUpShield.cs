using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerShieldActivator shieldActivator = col.GetComponent<PlayerShieldActivator>();
            shieldActivator.ActivateShield();
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position, 1f);
            Destroy(gameObject);
        }
    }
}