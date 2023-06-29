using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    
    protected float speed;
    
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb.velocity = Vector2.down * speed;
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
