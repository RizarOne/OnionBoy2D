using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntro : MonoBehaviour
{
    
    
        public void NextScene()
        {
            SceneManager.LoadScene("MainMenu");
        }

    
}
