using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public float attackRate = 1f;
    float nextAttackTime = 0;

    public float KBforce;

    void Update()
    {
        if (!UIManager.isPaused)
        {

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    Debug.Log("Attacked!");
                    SoundManager.PlaySound("OBAttack");
                    Attack();
                    nextAttackTime = Time.time + 0.3f / attackRange;
                }
            }
        }   

    }

        void Attack()
    {

        animator.SetTrigger("Attack");//Play an attack animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); //Detect enemies in range of attack

       foreach(Collider2D enemy in hitEnemies)//Damage them
        {
            bool facingRight = (transform.position.x < enemy.transform.position.x);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage,facingRight, KBforce);
            Debug.Log("We hit" + enemy.name);
        } 

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}


