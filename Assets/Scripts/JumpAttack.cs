using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WeakSpot"))
        {
            Debug.Log("Weak spot landed!");
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(10);
            }
            else
            {
                Debug.Log("BossHealth component not found on the collided object.");
            }

            

        }
    }
}
