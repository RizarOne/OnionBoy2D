using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager manager; //static niin päästään käsiksi joka paikasta

    public string currentLevel;

    public float health;
    public float previousHealth;
    public float maxHealth;

    //Jokaista leveliä varten on muuttuja. Muuttujan nimen pitää olla sama kuin LoadLevel scriptissä olevan levelToLoad muuttujan arvo.

    public bool Level_1;
    public bool Level_2;
    public bool Level_3;   

    private void Awake()
    {
        //singleton

        //Tsekataan, onko manageria jo olemassa
        if(manager == null)
        {
            //Jos manageria ei ole, kerrotaan että tämä luokka on se manageri
            //Kerrotaan myös, että tämä manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;

        }

        else
        {
            //Tämä ajetaan silloin jos on olemassa jo manageri ja ollaan luomassa toinen manageri (duplicate) joka on liikaa!
            //Tällöin tämä manageri tuhotaan pois, jolloin jää vain se ensimmäinen

            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
