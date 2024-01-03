using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Bullet
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
                    enemy.Hit(damage);
                }
            }
        }
        if (collision.gameObject.tag == "NewEnemy")
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("Hit", true);
            collision.gameObject.GetComponent<EnemyController>().takeEnemyHealth(2, "Bolt");
            collider.enabled = false;
        }
    }
}
