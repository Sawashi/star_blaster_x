using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody;
    protected CapsuleCollider2D collider;
    public float timeToLive = 1f;
    public float damage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    public virtual void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    public virtual void Launch(Vector2 dir, float force)
    {
        rigidbody.AddForce(dir * force, ForceMode2D.Impulse);
        StartCoroutine(CoDestroyTimer(timeToLive));
    }

    public virtual void DestroyBolt()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }

    protected virtual IEnumerator CoDestroyTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        rigidbody.velocity = Vector2.zero;
        animator.SetBool("Hit", true);

    }


}
