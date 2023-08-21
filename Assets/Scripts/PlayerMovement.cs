using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public Image hearts;
    public float health;
    public float previousHealth;
    public float maxHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {

        //Groundtest eli ollaanko kosketuksissa maahan
        if(Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded=false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if(Input.GetAxisRaw("Horizontal")!= 0)
        {
            //Meillä on joko a tai d pohjassa
            //transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1,1 ); // X scalen muutos kun liikkuu, päälle sitten kun on hahmo valmis.

            //animator.SetBool("Walk", true); //sitten kun animaatio valmis ja bool laitettuna animaattoriin.
        }
        else
        {
            //ei liikuta
            //animator.SetBool("Walk", false);  // Jos walk=false niin idle animaatio toistuu.
        }

        if (Input.GetButtonDown("Jump")&& grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            //animator.SetTrigger("Jump"); // Kun hypylle animaatio.
        }

        hearts.fillAmount = health/maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Got hit by trap!");
            TakeDamage(1); // 3 sydäntä joisa 1dmg tiputtaa yhden pois
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPotion"))
        {
            Destroy(collision.gameObject);
            Heal(1);
        }
    }

    void Heal(float amount)
    {
        previousHealth = hearts.fillAmount * maxHealth;
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;

    }
}
