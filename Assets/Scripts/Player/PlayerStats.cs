using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private Animator anim;
    [SerializeField] private Image healthFill;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Shield shield;

    private PlayerShooting playerShooting;
    private bool canPlayAnim = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        health = maxHealth;
        healthFill.fillAmount = health / maxHealth;
        EndGameManager.endManager.gameOver = false;
        playerShooting = GetComponent<PlayerShooting>();
    }

    public void PlayerTakeDamage(float damage)
    {
        if(shield.protection)
            return;
        
        health -= damage;
        healthFill.fillAmount = health / maxHealth;
        
        if (canPlayAnim)
        {
            anim.SetTrigger("Damage");
            StartCoroutine("AntiSpamAnimation");
        }
        playerShooting.DecreaseUpgrade();
        if (health <= 0)
        {
            EndGameManager.endManager.gameOver = true;
            EndGameManager.endManager.StartResolveSequence();
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;
        healthFill.fillAmount = health / maxHealth;
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }
}
