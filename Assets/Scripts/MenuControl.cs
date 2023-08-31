using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadMap()
    {
        //Play painiketta painettu 
        SceneManager.LoadScene("Map");
    }

    public void Save()
    {
        //Tämä ajetaan menusta, kun painetaan Save painiketta
        GameManager.manager.Save();
    }

    public void Load()
    {
        //Main menun load painike
        GameManager.manager.Load();

    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
