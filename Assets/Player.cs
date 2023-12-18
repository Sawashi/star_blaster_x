using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool isRunning = false;
    private bool isShooting = false;
    private bool canShoot = true;

    public bool isFacingRight = true;
    public bool isFalling = false;

    public float shootCooldown = 0.3f;
    public Vector2 boxSize;
    public float raycastDistance;
    public LayerMask groundLayer;
    public LayerMask platformLayer;


    public ArmShooting arm; 


    // Start is called before the first frame update
    void Start() {
        animator = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(rigidbody.velocity.y < 0.1) {
            isFalling = true;
        } else {
            isFalling = false;
        }
        if (IsGrounded()) {
            animator.SetBool("isFalling", false);
        } else {
            animator.SetBool("isFalling", true);
        }

        if(isShooting && canShoot) {
            arm.Shoot();
            StartCoroutine(CoFireDelay(shootCooldown));
        }

    }


    private void FixedUpdate() {
        Vector2 dir = rigidbody.velocity;
        float y_speed = Mathf.Clamp(dir.y, -7, 100);
        dir.y = y_speed;
        if (isRunning) {
            if (isFacingRight) {
                dir.x = 2;
                rigidbody.velocity = dir;

            } else {
                dir.x = -2;
                rigidbody.velocity = dir;

            }

        } else {
            dir.x = 0;
            rigidbody.velocity = dir;

        }

    }

    public bool IsGrounded() {
        if(Physics2D.BoxCast(transform.position, boxSize, 0 , -transform.up, raycastDistance, groundLayer)) {
            return true;
        }
        return false;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "DamgeZone") {

        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position-transform.up * raycastDistance, boxSize);
    }

    IEnumerator CoFireDelay(float timer) {
        canShoot = false;
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }

    public void Movement(InputAction.CallbackContext context) {
        float val = context.ReadValue<float>();
        if (val == 0) {
            isRunning = false;
            animator.SetBool("isRunning", isRunning);
        } else {
            isRunning = true;
            animator.SetBool("isRunning", isRunning);
            if (val < 0) {
                if (isFacingRight) {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                isFacingRight = false;

            }

            if (val > 0) {
                if (!isFacingRight) {
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                }
                isFacingRight = true;

            }
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && IsGrounded()) {
            Vector2 dir = rigidbody.velocity;
            dir.y = 5;
            rigidbody.velocity = dir;
            animator.SetTrigger("Jump");
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.started) {
            isShooting = true;
        }

        if (context.canceled) {
            isShooting = false;
        }
    }
}
