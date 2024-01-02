using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserShot : Bullet {




    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Enemy enemy = collision.transform.parent.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.Hit(damage);

            }


        }
    }

}
