using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSetting : MonoBehaviour
{
    Animator animator;
    BoxCollider2D box2d;
    Rigidbody2D rb2d;
    EnemyController enemyController;

    GameObject player;
    Vector3 playerPosition;

    bool isShooting;
    bool doAction;
    float actionTimer;
    [SerializeField] bool enableAI;
    [SerializeField] float actionDelay = 0f;
    void Awake()
    {
        // get components from EnemyController
        enemyController = GetComponent<EnemyController>();
        animator = enemyController.GetComponent<Animator>();
        box2d = enemyController.GetComponent<BoxCollider2D>();
        rb2d = enemyController.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doAction = true;
        actionTimer = actionDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.freezeEnemy)
        {
            // add anything here to happen while frozen i.e. time compensations
            return;
        }
        if (enableAI)
        {
            if (player != null) playerPosition = player.transform.position;
                Shoot();
        }
    }
    public void EnableAI(bool enable)
    {
        // enable enemy ai logic
        this.enableAI = enable;
    }
    public void SetActionDelay(float delay)
    {
        // override default action delay
        this.actionDelay = delay;
    }
    private void ShootBullet()
    {
        Vector2 bulletVector = new Vector2(-1f, 0);
        GameObject bullet = Instantiate(enemyController.bulletPrefab);
        bullet.name = enemyController.bulletPrefab.name;
        bullet.transform.position = enemyController.bulletShootPos.transform.position;
        bullet.GetComponent<BulletScript>().SetBulletType(BulletScript.BulletTypes.MiniGreen);
        bullet.GetComponent<BulletScript>().SetDamageValue(enemyController.bulletDamage);
        bullet.GetComponent<BulletScript>().SetBulletSpeed(enemyController.bulletSpeed);
        bullet.GetComponent<BulletScript>().SetBulletDirection(bulletVector);
        bullet.GetComponent<BulletScript>().SetCollideWithTags("Player");
        bullet.GetComponent<BulletScript>().SetDestroyDelay(5f);
        bullet.GetComponent<BulletScript>().Shoot();

        // play only one bullet sound
        SoundManager.Instance.Play(enemyController.shootBulletClip);
    }
    private void Shoot()
    {
        animator.Play("shoot", -1, 0);
        isShooting = true;
        doAction = false;
    }
}
