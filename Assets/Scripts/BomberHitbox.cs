using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberHitbox : MonoBehaviour
{
    public Bomber bomber;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerAttack") {
            bomber.Hit();
        }
    }
}
