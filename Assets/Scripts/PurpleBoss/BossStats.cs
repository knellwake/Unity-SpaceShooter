using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossStats : Enemy
{
    [SerializeField] private BossController bossController;

    public override void HurtSequence()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
            return;
        anim.SetTrigger("Damage");
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        bossController.ChangeState(BossState.death);
        Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
        }
    }
}