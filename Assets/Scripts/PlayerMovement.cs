using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 50f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;


    public Animator animator;
    public Rigidbody2D rb2D;
    private TrailRenderer trailRenderer;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

    public Image hearts;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); 
        trailRenderer = GetComponent<TrailRenderer>();

        GameManager.manager.historyHealth = GameManager.manager.health;
        GameManager.manager.historyPreviousHealth = GameManager.manager.previousHealth;
        GameManager.manager.historyMaxHealth = GameManager.manager.maxHealth;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        //Groundtest eli ollaanko kosketuksissa maahan
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
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


            float xScale = Mathf.Sign(Input.GetAxisRaw("Horizontal")) * 0.35f;
            transform.localScale = new Vector3(xScale, 0.35f, 1); // Muuttaa x arvon 0.35 ja -0.35 riippuen kumpaan suuntaan pelaaja kulkee.


            animator.SetBool("Walk", true); //sitten kun animaatio valmis ja bool laitettuna animaattoriin.
        }
        else
        {
            //ei liikuta
            animator.SetBool("Walk", false);  // Jos walk=false niin idle animaatio toistuu.
        }    

        if (Input.GetButtonDown("Jump")&& grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump"); // Kun hypylle animaatio.
        }
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            animator.SetTrigger("Dash");
        }

        hearts.fillAmount = GameManager.manager.health/ GameManager.manager.maxHealth;

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb2D.velocity.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Got hit by trap!");
            TakeDamage(1); // 3 sydäntä joista 1dmg tiputtaa yhden pois
        }
              
        if (collision.gameObject.CompareTag("Potato"))
        {
            Debug.Log("Player got hit by potato!");
            TakeDamage(1); // 3 sydäntä joista 1dmg tiputtaa yhden pois
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
      


        if (collision.CompareTag("EnemyDamager"))
        {
            Debug.Log("Got hit by CarrotDude!");
            TakeDamage(1); // 3 sydäntä joista 1dmg tiputtaa yhden pois
        }
        
        //if (collision.CompareTag("Potato"))
        //{
            
        //    Debug.Log("Got hit by Potato!");
        //    TakeDamage(1); // 3 sydäntä joista 1dmg tiputtaa yhden pois
        //}


        if (collision.CompareTag("HealthPotion"))
        {
            Destroy(collision.gameObject);
            Heal(1);
        }

        if (collision.CompareTag("LevelEnd"))
        {
            GameManager.manager.previousLevel = GameManager.manager.currentLevel;
            SceneManager.LoadScene("Map");
        }

        if (collision.CompareTag("DeadZone"))
        {
            Debug.Log("Out of area!");
            Die();
        }

    }


   

    void Heal(float amount)
    {
        GameManager.manager.previousHealth = hearts.fillAmount * GameManager.manager.maxHealth;
        GameManager.manager.health += amount;
        if(GameManager.manager.health > GameManager.manager.maxHealth)
        {
            GameManager.manager.health = GameManager.manager.maxHealth;
        }
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("TakeDamage function working");
        GameManager.manager.previousHealth = hearts.fillAmount * GameManager.manager.maxHealth;
        GameManager.manager.health -= dmg;

        if (GameManager.manager.health == 0)
        {

            Debug.Log("Death!");
            Die();
            
        }

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;
        rb2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void Die()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("Map");
    }



}
