using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoHealth : MonoBehaviour, IDamageable
{
    private float maxHealth = 3f;
    private float currentHealth;

    private Animator anim;
    public bool HasTakenDamage { get; set; }




    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;
        currentHealth -= damageAmount;

        SoundManager.PlaySound("EnemyHurt");
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Destroy(gameObject);

        Debug.Log("Enemy died!");
        SoundManager.PlaySound("EnemyDeath");// ei toimi tässä kun disabloi scriptin ...
        anim.SetBool("IsDead", true);//Die animation
        GetComponent<Rigidbody2D>().simulated = false;// disabloi rigidbodyn niin liike loppuu.
        GetComponent<Collider2D>().enabled = false;// disabloi colliderin niin pelaaja voi kävellä "läpi"
        //this.enabled = false;//disabloi tämän scriptin
    }
}
