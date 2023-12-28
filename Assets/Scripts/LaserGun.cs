using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Weapon
{
    private Bullet beam = null;

    public override GunPickup GetPickup() {
        return weaponPickup;
    }

    public override void Shoot(Vector2 aimDir, Quaternion rot) {
        if (beam != null) {
            return;
        }

        AudioManager.Instance.PlaySFX("Player Shooting");

        beam = Instantiate<Bullet>(ammo, shootingPoint.transform);
        beam.Flip();
        
    }

    public override void Stop() {
        Destroy(beam.gameObject);

        beam = null;
    }

    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

}
