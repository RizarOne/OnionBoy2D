using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 10;
    public int currentHealth;
    public GameObject levelComplete;

    

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //animator.SetTrigger("Hurt");    //play hurt animation NEED TO CREATE ONE!

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        levelComplete.SetActive(true);
        Debug.Log("Boss died!");
       // animator.SetBool("IsDead", true);//Die animation NEED TO CREATE ONE!
        GetComponent<Rigidbody2D>().simulated = false;// disabloi rigidbodyn niin liike loppuu.
        GetComponent<Collider2D>().enabled = false;// disabloi colliderin niin pelaaja voi kävellä "läpi"
        GetComponent<Animator>().enabled = false;// disabloi colliderin niin pelaaja voi kävellä "läpi"
        this.enabled = false;//disabloi tämän scriptin

    }


}
