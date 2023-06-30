using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerShieldActivator shieldActivator = col.GetComponent<PlayerShieldActivator>();
            shieldActivator.ActivateShield();
            Destroy(gameObject);
        }
    }
}