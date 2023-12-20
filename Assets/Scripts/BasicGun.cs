using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : Weapon
{
    public override void Shoot(Vector2 aimDir, Quaternion rot) {

        Bolt bullet = Instantiate<Bolt>(ammo, shootingPoint.transform.position, rot);
        if (aimDir.x > 0) {
            bullet.Flip();
        }
        bullet.Launch(aimDir, projectileSpeed);
    }


    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

}
