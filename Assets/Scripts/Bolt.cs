using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rigidbody;
    private CapsuleCollider2D collider;
    public float timeToLive = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    public void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    public void Launch(Vector2 dir, float force)
    {
        rigidbody.AddForce(dir * force, ForceMode2D.Impulse);
        StartCoroutine(CoDestroyTimer(timeToLive));
    }

    public void DestroyBolt()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("Hit", true);
            collider.enabled = false;

        }
    }

    IEnumerator CoDestroyTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        rigidbody.velocity = Vector2.zero;
        animator.SetBool("Hit", true);

    }
}
