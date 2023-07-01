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
    public bool canTakeDmg = true;
    
    void OnEnable()
    {
        health = maxHealth;
        healthFill.fillAmount = health / maxHealth;
        EndGameManager.endManager.gameOver = false;
        StartCoroutine(DamageProtection());
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerShooting = GetComponent<PlayerShooting>();
        EndGameManager.endManager.RegisterPlayerStats(this);
        EndGameManager.endManager.possibileWin = false;
    }

    IEnumerator DamageProtection()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(1.5f);
        canTakeDmg = true;
    }

    public void PlayerTakeDamage(float damage)
    {
        if(canTakeDmg == false)
            return;
        
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
            // Destroy(gameObject);
            gameObject.SetActive(false);
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
