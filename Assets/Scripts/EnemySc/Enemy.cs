using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] protected float damage;
    [SerializeField] protected GameObject explosionPrefab;

    [Header("Score"), SerializeField] protected int scoreValue;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        HurtSequence();
        if (health <= 0)
        {
            DeathSequence();
        }
    }

    public virtual void HurtSequence()
    {
    }

    public virtual void DeathSequence()
    {
        EndGameManager.endManager.UpdateScore(scoreValue);
    }
}