using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private float timeBtwAttacks = 0.15f;

    public bool ShouldBeDamaging { get; private set; } = false;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    private RaycastHit2D[] hits;

    private Animator anim;
    public Animator effectAnim;

    public GameObject AttackPoint; // Attackpoint child object.

    private float attackTimeCounter;

    private void Start()
    {
        anim = GetComponent<Animator>();
        effectAnim = AttackPoint.gameObject.GetComponent<Animator>(); //Attackpoint objektin animaattori

        //Allows you to attack right away
        attackTimeCounter = timeBtwAttacks;
    }
    private void Update()
    {


        if(Input.GetKeyDown(KeyCode.RightControl)&& attackTimeCounter >= timeBtwAttacks)
        {
            //reset the counter
            attackTimeCounter = 0f;

            //Attack();
            anim.SetTrigger("Attack");

            Debug.Log("Attacked!");
            SoundManager.PlaySound("OBAttack");

        }

        attackTimeCounter += Time.deltaTime;
    }


    //private void Attack()
    //{
    //    hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

    //    for (int i = 0; i < hits.Length; i++)
    //    {
    //        IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

    //        //if we found an iDamageable
    //        if (iDamageable != null)
    //        {
    //            //apply damage
    //            iDamageable.Damage(damageAmount);
    //        }
    //    }
    //}

    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;


        while (ShouldBeDamaging)
        {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            //if we found an iDamageable
            if (iDamageable != null && !iDamageable.HasTakenDamage)
            {
                //apply damage
                iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
            }
        }

            yield return null;

        }

        ReturnAttackablesToDamageable();
            
        
    }

    private void ReturnAttackablesToDamageable()
    {
        foreach (IDamageable thingThatWasDamaged in iDamageables)
        {
            thingThatWasDamaged.HasTakenDamage = false;
        }

        iDamageables.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    #region Animation Triggers

    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }

    public void PlayEffectAnimation()
    {
        effectAnim.SetTrigger("SlashEffect");
    }


    #endregion
}

