using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    [SerializeField] protected GameObject shootingPoint;
    public SpriteRenderer renderer;
    public float projectileSpeed = 5f;
    public Bolt ammo;


    public abstract void Shoot(Vector2 aimDirection, Quaternion spriteRotation);
}
