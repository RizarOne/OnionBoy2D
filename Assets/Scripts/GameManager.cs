using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager manager; //static niin p��st��n k�siksi joka paikasta

    public string currentLevel;

    public float health;
    public float previousHealth;
    public float maxHealth;

    //Jokaista leveli� varten on muuttuja. Muuttujan nimen pit�� olla sama kuin LoadLevel scriptiss� olevan levelToLoad muuttujan arvo.

    public bool Level_1;
    public bool Level_2;
    public bool Level_3;   

    private void Awake()
    {
        //singleton

        //Tsekataan, onko manageria jo olemassa
        if(manager == null)
        {
            //Jos manageria ei ole, kerrotaan ett� t�m� luokka on se manageri
            //Kerrotaan my�s, ett� t�m� manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;

        }

        else
        {
            //T�m� ajetaan silloin jos on olemassa jo manageri ja ollaan luomassa toinen manageri (duplicate) joka on liikaa!
            //T�ll�in t�m� manageri tuhotaan pois, jolloin j�� vain se ensimm�inen

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
