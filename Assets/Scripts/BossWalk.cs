using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float rango = 3f;
    private int numero;
    Transform player;
    private string[] ataques = {"atacar", "atacar2", "atacar3"};
    Rigidbody2D rb;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb=animator.GetComponent<Rigidbody2D>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x,rb.position.y);
        Vector2 newpos=Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newpos);

        if (Vector2.Distance(player.position, rb.position) <= rango) {
            numero = Random.Range(0, 3);
            animator.SetTrigger(ataques[numero]);
        };
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(ataques[numero]);
    }

}
