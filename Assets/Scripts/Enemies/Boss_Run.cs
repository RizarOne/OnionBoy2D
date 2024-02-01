using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 4f;
    public float attackRange = 3f;

    Transform player;
    Rigidbody2D rb2D;
    Boss boss;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb2D = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb2D.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb2D.position, target ,speed * Time.fixedDeltaTime);
        rb2D.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb2D.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
    
}
