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
    private Animator animator;
    private Rigidbody2D rigidbody;
    private Player player;
    private bool canMove = true;
    private SpriteRenderer renderer;
    private DamageFlash flash;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = transform.GetChild(0).GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        renderer = GetComponent<SpriteRenderer>();
        flash = GetComponent<DamageFlash>();
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

    public override void Hit(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            flash.Flash();
        }
        else
        {
            Die();
        }
    }

    public void PlaySound()
    {
        AudioManager.Instance.PlaySFX("Enemy Death");
    }

    public override void Die()
    {
        hitbox.enabled = false;
        renderer.color = Color.white;

        animator.SetBool("Dead", true);
        animator.SetTrigger("Hurt");
    }

    public override void DestroyEnemy()
    {

        Destroy(gameObject);
    }
}
