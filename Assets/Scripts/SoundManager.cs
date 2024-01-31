using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jumpSound, playerHitSound, dashSound, deathSound, enemyDeathSound, enemyGotHitSound, playerGotHitSound;
    static AudioSource audioSrc;
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("OBJump");
        playerHitSound = Resources.Load<AudioClip>("OBAttack");
        dashSound = Resources.Load<AudioClip>("OBDash");
        deathSound = Resources.Load<AudioClip>("OBDeath");
        enemyDeathSound = Resources.Load<AudioClip>("EnemyDeath");
        enemyGotHitSound = Resources.Load<AudioClip>("EnemyHurt");
        playerGotHitSound = Resources.Load<AudioClip>("OBHit");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "OBAttack":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "OBJump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "OBDash":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "OBHit":
                audioSrc.PlayOneShot(playerGotHitSound);
                break;
            case "OBDeath":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "EnemyHurt":
                audioSrc.PlayOneShot(enemyGotHitSound);
                break;
            case "EnemyDeath":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
        }
    }
}
