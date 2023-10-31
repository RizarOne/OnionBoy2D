using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager manager; //static niin p‰‰st‰‰n k‰siksi joka paikasta

    public string currentLevel;

    public float health;
    public float previousHealth;
    public float maxHealth;

    public string previousLevel;

    public float historyHealth;
    public float historyPreviousHealth;
    public float historyMaxHealth;

    //Jokaista leveli‰ varten on muuttuja. Muuttujan nimen pit‰‰ olla sama kuin LoadLevel scriptiss‰ olevan levelToLoad muuttujan arvo.

    public bool Level_1;
    public bool Level_2;
    public bool Level_3;   

    private void Awake()
    {
        //singleton

        //Tsekataan, onko manageria jo olemassa
        if(manager == null)
        {
            //Jos manageria ei ole, kerrotaan ett‰ t‰m‰ luokka on se manageri
            //Kerrotaan myˆs, ett‰ t‰m‰ manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;

        }

        else
        {
            //T‰m‰ ajetaan silloin jos on olemassa jo manageri ja ollaan luomassa toinen manageri (duplicate) joka on liikaa!
            //T‰llˆin t‰m‰ manageri tuhotaan pois, jolloin j‰‰ vain se ensimm‰inen

            Destroy(gameObject);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Map")
            {

                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void Save()
    {
        Debug.Log("Game Saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        //Syˆtet‰‰n tallennettava tieto data-objekstiin, joka lopuksi serialisoidaan.
        data.health = health;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.Level1 = Level_1;
        data.Level2 = Level_2;
        data.Level3 = Level_3;
        data.currentLevel = currentLevel;
        bf.Serialize(file, data); //File ilmoitettu yll‰ ja tallentaa sinne data. alkuiset.
        file.Close();

    }

    public void Load()
    {
        // Tarkastetaan onko olemassa tallennustiedostoa. Jos on niin sitten voidaan ladata tiedot.
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game Loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //siirret‰‰n tiedot playerdatasta meid‰n gameManageriin

            health = data.health;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            Level_1 = data.Level1;
            Level_2 = data.Level2;
            Level_3 = data.Level3;
            currentLevel = data.currentLevel;

        }
    }
}

//Toinen luokka, joka voidaan serialisoida. Pit‰‰ sis‰ll‰‰n vain sen datan mit‰ serialisoidaan.
[Serializable]
class PlayerData
{
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;
    public bool Level1;
    public bool Level2;
    public bool Level3;


}