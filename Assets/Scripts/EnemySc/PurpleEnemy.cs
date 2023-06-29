using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEnemy : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float shootInterval;
    private float shootTimer = 0;
    [SerializeField] private Transform leftCanon;
    [SerializeField] private Transform rightCanon;
    
    [SerializeField] private GameObject bulletPrefab;
    
    private void Start()
    {
        rb.velocity = Vector2.down * speed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Instantiate(bulletPrefab, leftCanon.position, Quaternion.identity);
            Instantiate(bulletPrefab, rightCanon.position, Quaternion.identity);
            shootTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public override void HurtSequence()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
            return;
        anim.SetTrigger("Damage");
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}