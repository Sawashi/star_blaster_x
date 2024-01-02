using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] protected GameObject shootingPoint;
    [SerializeField] protected GunPickup weaponPickup;
    public SpriteRenderer renderer;
    public float projectileSpeed = 5f;
    public Bullet ammo;
    public float cooldown = 0.5f;
    protected float cooldownTimer = 0;
    public bool canShoot = true;


    public abstract void Shoot(Vector2 aimDirection, Quaternion spriteRotation);
    public abstract void Stop();
    public abstract GunPickup GetPickup();

}
