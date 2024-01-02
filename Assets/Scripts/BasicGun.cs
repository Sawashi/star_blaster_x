using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Weapon
{
    public override void Shoot(Vector2 aimDir, Quaternion rot) {
        if (!canShoot) return;
        AudioManager.Instance.PlaySFX("Player Shooting");

        Bullet bullet = Instantiate<Bullet>(ammo, shootingPoint.transform.position, rot);
        if (aimDir.x > 0) {
            bullet.Flip();
        }
        bullet.Launch(aimDir, projectileSpeed);
        cooldownTimer = cooldown;
        canShoot = false;
    }

    public override void Stop() {
        
    }

    void Update() {
        if (cooldownTimer > 0) {
            cooldownTimer -= Time.deltaTime;

        } else {
            canShoot = true;
        }
    }

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    public override GunPickup GetPickup() {
        return weaponPickup;
    }
}
