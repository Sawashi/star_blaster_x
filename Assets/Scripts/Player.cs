using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public ParticleSystem deathParticle;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private DamageFlash flashEffect;
    private bool isRunning = false;

    private bool isShooting = false;
    public bool isFacingRight = true;
    public bool isFalling = false;
    private bool isInvincible = false;

    public Vector2 boxSize;
    public float raycastDistance;
    public LayerMask groundLayer;

    public Vector2 pickupSize;
    public float pickupDistance;
    public LayerMask pickupLayer;

    public int maxHealth = 100;
    public int currentHealth; 
    public float invincibleDuration = 0.5f;
    private float invincibleTimer = 0;
    public HealthBar healthBar;
    public ArmShooting arm;
   

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        flashEffect = GetComponent<DamageFlash>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.y < 0.1) {
            isFalling = true;
        } else {
            isFalling = false;
        }
        if (IsGrounded()) {
            animator.SetBool("isFalling", false);
        } else {
            animator.SetBool("isFalling", true);
        }

        if (isShooting) {
            arm.Shoot();
        }
        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0) {
                isInvincible = false;
            }
        } 
        if (Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(10);
        }
    }
    

    private void FixedUpdate()
    {
        Vector2 dir = rigidbody.velocity;
        float y_speed = Mathf.Clamp(dir.y, -7, 100);
        dir.y = y_speed;
        if (isRunning)
        {
            if (isFacingRight)
            {
                dir.x = 2;
                rigidbody.velocity = dir;

            }
            else
            {
                dir.x = -2;
                rigidbody.velocity = dir;

            }

        }
        else
        {
            dir.x = 0;
            rigidbody.velocity = dir;

        }

    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, raycastDistance, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamgeZone")
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * raycastDistance, boxSize);
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
            arm.StopShooting();
        }
    }

    public void Interact(InputAction.CallbackContext context) {
        if (context.started) {
            Debug.Log("Interact");

            RaycastHit2D hit = Physics2D.BoxCast(transform.position, pickupSize, 0, -transform.up, pickupDistance, pickupLayer);
            if (hit.collider != null) {
                GunPickup pickup = hit.collider.GetComponent<GunPickup>();
                arm.SwitchWeapon(pickup.GetWeapon());
                pickup.DestroyPickup();
            }

        }


    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        if (currentHealth > 0) {
            isInvincible = true;
            invincibleTimer = invincibleDuration;
        } else {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        healthBar.SetHealth(currentHealth);
        flashEffect.Flash();
    }
}
