using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;   
    public int maxHealth = 3;
    public int currentHealth;

    public Rigidbody2D enemyRb;
    public float delayTime = 0.15f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, bool facingRight, float KBforce)
    {
        if (facingRight)
        {
            enemyRb.AddForce(Vector2.right * KBforce, ForceMode2D.Impulse);

        }
        else
        {
            enemyRb.AddForce(Vector2.left * KBforce, ForceMode2D.Impulse);
        }
        SoundManager.PlaySound("EnemyHurt");
        currentHealth -= damage;
        animator.SetTrigger("Hurt");//play hurt animation

        if(currentHealth <= 0)
        {
            SoundManager.PlaySound("EnemyDeath");

            Die();
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        enemyRb.velocity = Vector2.zero;
    }
    

    void Die()
    {
        Debug.Log("Enemy died!");
        SoundManager.PlaySound("EnemyDeath");// ei toimi tässä kun disabloi scriptin ...
        animator.SetBool("IsDead", true);//Die animation
        GetComponent<Rigidbody2D>().simulated = false;// disabloi rigidbodyn niin liike loppuu.
        GetComponent<Collider2D>().enabled = false;// disabloi colliderin niin pelaaja voi kävellä "läpi"
        this.enabled = false;//disabloi tämän scriptin
    }

    
}
