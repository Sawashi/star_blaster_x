using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceShot : Bullet
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("Hit", true);
            collider.enabled = false;

            if (collision.gameObject.tag == "Enemy")
            {

                Enemy enemy = collision.transform.parent.GetComponent<Enemy>();
                if (enemy != null)
                {
                    StatusEffect stsScript = enemy.GetComponent<StatusEffect>();
                    if (stsScript != null)
                    {
                        stsScript.Slow();
                    }
                    enemy.Hit(damage);
                }
                else
                {
                    EnemyController e = collision.transform.parent.GetComponent<EnemyController>();
                    if (e != null)
                    {
                        StatusEffect sts = e.GetComponent<StatusEffect>();
                        if (sts != null)
                        {
                            sts.Slow();
                        }
                        e.TakeDamage((int)damage);
                    }

                }
            }
        }
        if (collision.gameObject.tag == "NewEnemy")
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("Hit", true);
            collision.gameObject.GetComponent<EnemyController>().takeEnemyHealth(3, "Ice");
            collider.enabled = false;
        }
    }
}
