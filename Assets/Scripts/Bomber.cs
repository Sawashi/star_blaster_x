using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomber : Enemy
{
    [SerializeField] private PlayerInAttackRange attackRange;
    [SerializeField] private DetectPlayer detectRange;
    [SerializeField] private CircleCollider2D damageZone;
    private CircleCollider2D hitbox;
    public Animator animator;
    private Rigidbody2D rigidbody;
    private Player player;
    private bool canMove = true;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        hitbox = transform.GetChild(0).GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackRange.playerInAttackRange && health > 0)
        {
            Die();

        }
        if (detectRange.playerDetected && canMove)
        {
            if (player != null)
            {
                Vector2 dir = player.transform.position - transform.position;
                dir.Normalize();
                rigidbody.velocity = dir * speed; ;

            }


        }
    }

    public void DisableMovement()
    {
        canMove = false;
        rigidbody.velocity = Vector2.zero;
    }


    public void EnableDamageZone()
    {
        damageZone.enabled = true;
    }

    public void DisableDamageZone()
    {
        damageZone.enabled = false;
    }

    public override void Hit()
    {
        health -= 1;
        if (health > 0)
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            Die();
        }
    }

    public override void Die()
    {
        hitbox.enabled = false;
        animator.SetBool("Dead", true);
        animator.SetTrigger("Hurt");
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(2000);
    }

    public override void DestroyEnemy()
    {

        Destroy(gameObject);
    }
}
