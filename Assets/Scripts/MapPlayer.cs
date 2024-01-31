using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public Animator animator;
    public Rigidbody2D rb2D;

    public Transform groundCheckPosition;
    public float groundCheckRadius;
    public LayerMask groundCheckLayer;
    public bool grounded;

  
    void Start()
    {
        if(GameManager.manager.currentLevel != "")
        {

            //currentLevel on jotain muuta kuin tyhj‰, jolloin ollaan siis tultu pois levelist‰ scenest‰.
            //Asetetaan pelaajalle uusi sijainti
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position; // GETCHILD(1-2)

            //Jokin taso on p‰‰sty l‰pi
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().LevelCleared(true);
        }

        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //Groundtest eli ollaanko kosketuksissa maahan
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundCheckLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Meill‰ on joko a tai d pohjassa


            float xScale = Mathf.Sign(Input.GetAxisRaw("Horizontal")) * 0.35f;
            transform.localScale = new Vector3(xScale, 0.35f, 1); // Muuttaa x arvon 0.35 ja -0.35 riippuen kumpaan suuntaan pelaaja kulkee.
            animator.SetBool("Walk", true);  //sitten kun animaatio valmis ja bool laitettuna animaattoriin.
        }
        else
        {
            //ei liikuta
            animator.SetBool("Walk", false);  // Jos walk=false niin idle animaatio toistuu.
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2D.velocity = new Vector2(0, jumpForce);
            animator.SetTrigger("Jump"); // Kun hypylle animaatio.
            SoundManager.PlaySound("OBJump");
        }
        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
 
    }
      

    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);

        }
     }
}
