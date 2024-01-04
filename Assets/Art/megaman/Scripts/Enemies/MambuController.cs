using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MambuController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    EnemyController enemyController;

    bool isFacingRight;
    bool isShooting;

    float openTimer;
    float closedTimer;
    float shootTimer;

    
    [SerializeField] bool enableAI;

    public float moveSpeed = 1f;
    public float openDelay = 1f;
    public float closedDelay = 1.5f;
    public float shootDelay = 0.5f;

    public enum MambuState { Closed, Open };
    public MambuState mambuState = MambuState.Closed;

    public enum MoveDirections { Left, Right };
    [SerializeField] MoveDirections moveDirection = MoveDirections.Left;

    void Awake()
    {
       
        enemyController = GetComponent<EnemyController>();
        animator = enemyController.GetComponent<Animator>();
        rb2d = enemyController.GetComponent<Rigidbody2D>();
    }

    
    void Start()
    {
        
        isFacingRight = true;
        if (moveDirection == MoveDirections.Left)
        {
            isFacingRight = false;
            enemyController.Flip();
        }

        
        SetState(mambuState);
    }

    
    void Update()
    {
        if (enemyController.freezeEnemy)
        {
            
            return;
        }

       
        if (enableAI)
        {
           
            switch (mambuState)
            {
                case MambuState.Closed:
                    animator.Play("Mambu_Closed");
                    rb2d.velocity = new Vector2(((isFacingRight) ? moveSpeed : -moveSpeed), rb2d.velocity.y);
                    closedTimer -= Time.deltaTime;
                    if (closedTimer < 0)
                    {
                        mambuState = MambuState.Open;
                        openTimer = openDelay;
                        shootTimer = shootDelay;
                    }
                    break;
                case MambuState.Open:
                    animator.Play("Mambu_Open");
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    shootTimer -= Time.deltaTime;
                    if (shootTimer < 0 && !isShooting)
                    {
                        ShootBullet();
                        isShooting = true;
                    }
                    openTimer -= Time.deltaTime;
                    if (openTimer < 0)
                    {
                        mambuState = MambuState.Closed;
                        closedTimer = closedDelay;
                        isShooting = false;
                    }
                    break;
            }
        }
    }

    public void EnableAI(bool enable)
    {
        
        this.enableAI = enable;
    }

    public void SetMoveDirection(MoveDirections direction)
    {
        
        moveDirection = direction;
        
        if (moveDirection == MoveDirections.Left)
        {
            if (isFacingRight)
            {
                isFacingRight = !isFacingRight;
                enemyController.Flip();
            }
        }
        else
        {
            if (!isFacingRight)
            {
                isFacingRight = !isFacingRight;
                enemyController.Flip();
            }
        }
    }

    public void SetState(MambuState state)
    {
        
        mambuState = state;

        
        isShooting = false;

        
        if (mambuState == MambuState.Closed)
        {
            closedTimer = closedDelay;
        }
        else if (mambuState == MambuState.Open)
        {
            openTimer = openDelay;
        }
    }

    private void ShootBullet()
    {
        
        GameObject[] bullets = new GameObject[8];
        Vector2[] bulletVectors = {
            new Vector2(-1f, 0),            // Left
            new Vector2(1f, 0),             // Right
            new Vector2(0, -1f),            // Down
            new Vector2(0, 1f),             // Up
            new Vector2(-0.75f, -0.75f),    // Left-Down
            new Vector2(-0.75f, 0.75f),     // Left-Up
            new Vector2(0.75f, -0.75f),     // Right-Down
            new Vector2(0.75f, 0.75f)       // Right-Up
        };
        
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(enemyController.bulletPrefab);
            bullets[i].name = enemyController.bulletPrefab.name;
            bullets[i].transform.position = enemyController.bulletShootPos.transform.position;
            bullets[i].GetComponent<BulletScript>().SetBulletType(BulletScript.BulletTypes.MiniPink);
            bullets[i].GetComponent<BulletScript>().SetDamageValue(enemyController.bulletDamage);
            bullets[i].GetComponent<BulletScript>().SetBulletSpeed(enemyController.bulletSpeed);
            bullets[i].GetComponent<BulletScript>().SetBulletDirection(bulletVectors[i]);
            bullets[i].GetComponent<BulletScript>().SetCollideWithTags("Player");
            bullets[i].GetComponent<BulletScript>().SetDestroyDelay(5f);
            bullets[i].GetComponent<BulletScript>().Shoot();
        }
        
        SoundManager.Instance.Play(enemyController.shootBulletClip);
    }

    
    private void StartInvincibleAnimation()
    {
        enemyController.Invincible(true);
    }

    
    private void StopInvincibleAnimation()
    {
        enemyController.Invincible(false);
    }
}