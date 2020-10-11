using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public float enemy_health;
    public float time_between_attack;
    private float cache;
    public Transform attack_position;
    public LayerMask what_is_player;
    public Slider health_bar;

    public void Awake()
    {
        cache = time_between_attack;
        health_bar.maxValue = enemy_health;
    }
    

    public void Update()
    {
        if (cache <= 0)
        {
            cache = time_between_attack;

            //code for attack
            Collider2D[] enemy_reach = Physics2D.OverlapCircleAll(attack_position.position, .5f, what_is_player);
            for (int i = 0; i < enemy_reach.Length; i++)
            {
                enemy_reach[i].GetComponent<move>().health -= 1;
                Debug.Log(enemy_reach[i].GetComponent<move>().health);
            }
            
        }
        else
        {
            cache -= Time.deltaTime;
        }
    }
    public void Take_damage()
    {
        enemy_health -= 1;
        health_bar.value = enemy_health;
        
        if (enemy_health <= 0)
        {
            Debug.Log("Died");
            Destroy(gameObject, 1);
        }
        else
        {
            Debug.Log("hit");
        }
    }
}
