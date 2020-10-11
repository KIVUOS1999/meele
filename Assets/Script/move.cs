using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask what_is_ground, enemy;
    public float mov_speed, jump_speed;
    public Transform ground_check, attack_position;
    private bool face_right = true;
    public Animator animator;
    public float time_between_attack;
    private float cache;
    public float health;
    public Slider health_bar;


    private void Start()
    {
        cache = time_between_attack;
        health_bar.maxValue = health;
    }


    private void Update()
    {
        health_bar.value = health;
        bool isgrounded = Physics2D.OverlapCircle(ground_check.position, 0.5f, what_is_ground);
        
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
            rb.velocity = new Vector2(0f, jump_speed);

        if(cache <= 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //animation if attack
                animator.SetBool("isattack", true);
                cache = time_between_attack;

                //code for attack
                Collider2D[] enemy_reach = Physics2D.OverlapCircleAll(attack_position.position, 1, enemy); 
                for(int i=0; i<enemy_reach.Length; i++)
                {
                    enemy_reach[i].GetComponent<enemy>().Take_damage();
                }
            }
        }
        else
        {
            cache -= Time.deltaTime;
            animator.SetBool("isattack", false);
        }
        
    }

    private void FixedUpdate()
    {
        float player_input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(player_input * mov_speed, rb.velocity.y);

        if (player_input != 0)
            animator.SetBool("isrunning", true);
        else
            animator.SetBool("isrunning", false);

        if (face_right == false && player_input > 0)
            flip();
        else if (face_right == true && player_input < 0)
            flip();
    }

    void flip()
    {
        face_right = !face_right;
        Vector3 p = transform.localScale;
        p.x *= -1;
        transform.localScale = p;
    }

    public void take_damage()
    {
        health -= 1;
    }
}
