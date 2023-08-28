using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayer : MonoBehaviour
{
    public float speed;

    void Start()
    {
        if(GameManager.manager.currentLevel != "")
        {
            //Jokin taso on p��sty l�pi
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().LevelCleared(true);

            //currentLevel on jotain muuta kuin tyhj�, jolloin ollaan siis tultu pois levelist� scenest�.
            //Asetetaan pelaajalle uusi sijainti
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontalMove, verticalMove, 0);
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
