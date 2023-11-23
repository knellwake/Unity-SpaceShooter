using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool<LaserBullet> referencePool;

    void OnEnable()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void SetPool(ObjectPool<LaserBullet> pool)
    {
        referencePool = pool;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        if(gameObject.activeSelf)
            referencePool.Release(this);
        // Destroy(gameObject);
    }

    public void SetDirectionAndSpeed()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        // Destroy(gameObject);
        if(gameObject.activeSelf)
            referencePool.Release(this);
    }
}