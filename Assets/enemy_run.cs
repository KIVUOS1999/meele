using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_run : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    public float speed = 1.5f;
    public float attack_range=3f;
    public float time_between_attack;
    private float cache;
    bool face_right = true;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        cache = time_between_attack;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newposition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newposition);

        if (face_right == false && rb.transform.position.x - player.position.x > 0)
            flip();
        else if (face_right == true && rb.transform.position.x - player.position.x < 0)
            flip();

        if(Vector2.Distance(player.position, rb.position) <= attack_range )
        {
            if(cache <= 0)
            {
                animator.SetTrigger("Attack");
                cache = time_between_attack;
            }

            else
            {
                cache -= Time.deltaTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    void flip()
    {
        face_right = !face_right;
        Vector3 p = rb.transform.localScale;
        p.x *= -1;
        rb.transform.localScale = p;
    }
}
