using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;
    public int hp;
    public int hpPool;
    public int xpToGive;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    private Player player;

    [Header("Attack")]
    public int dmg;
    public float attackRate;
    private float lastAttackTime;


    //components
    private Rigidbody2D rig;
    private ParticleSystem hitFX;



    private void Awake()
    {
        //runs getcomponent on every object; only use once per scene
        player = FindObjectOfType<Player>();
        rig = GetComponent<Rigidbody2D>();
        hitFX = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector2.Distance(transform.position, player.transform.position);

        //if able to atk, stop moving and atk player
        //if able to chase, chase
        //otherwise, stop moving
        if(playerDist <= attackRange)
        {
            //atk
            if(Time.time - lastAttackTime >= attackRate)
            {
                Attack();
            }
            rig.velocity = Vector2.zero;
        } else if (playerDist <= chaseRange)
        {
            Chase();
        } else
        {
            rig.velocity = Vector2.zero;
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        player.TakeDamage(dmg);
    }

    void Chase()
    {
        //finds remainder of player distance and enemy distance, and equates magnitude to 1 all around
        Vector2 dir = (player.transform.position - transform.position).normalized;

        rig.velocity = dir * moveSpeed;
    }

    public void TakeDamage(int dmg)
    {
        hitFX.Play();
        hpPool -= dmg;

        if(hpPool <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        player.AddXP(xpToGive);
        Destroy(gameObject);
    }


}
