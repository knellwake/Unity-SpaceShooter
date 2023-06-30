using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField]private Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
