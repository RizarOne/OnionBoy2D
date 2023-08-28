using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad;

    public bool levelCleared;

    void Start()
    {
        //Katsotaan aina Map Scene avattaessa, ett‰ onko GameManagerissa merkattu, ett‰ kyseinen taso on l‰p‰isty
        //Jos on l‰p‰isty, ajetaan LevelCleared funktio, joka tekee tarpeelliset muutokset t‰h‰n objektiin, eli n‰ytt‰‰ Cleared tekstin ja disabloi colliderin.
        if (GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            LevelCleared(true);
        }
    }

    void Update()
    {
        
    }

    public void LevelCleared(bool levelIsClear)
    {
        if(levelIsClear == true)
        {
            levelCleared = true;
            //Asetetaan gamemanagerissa oikea boolean arvo trueksi

            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            //Laitetaan LevelClear icon/teksti n‰kyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(2).gameObject.GetComponent<Canvas>().enabled = true;
            //Koska taso on l‰pi, poistetaan level trigger box colliderista ettei tasoon p‰‰se takaisin.
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
