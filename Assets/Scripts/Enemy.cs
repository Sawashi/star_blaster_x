using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float health = 2;
    public float speed = 0.5f;
    public abstract void Hit();
    public abstract void Die();
    public abstract void DestroyEnemy();
}
