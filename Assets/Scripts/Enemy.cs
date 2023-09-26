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

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {       
        currentHealth -= damage;
        animator.SetTrigger("Hurt");//play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetBool("IsDead", true);//Die animation
        GetComponent<Rigidbody2D>().simulated = false;// disabloi rigidbodyn niin liike loppuu.
        GetComponent<Collider2D>().enabled = false;// disabloi colliderin niin pelaaja voi kävellä "läpi"
        this.enabled = false;//disabloi tämän scriptin
    }

    
}
