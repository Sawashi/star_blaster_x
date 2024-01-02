using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float healAmount = 5;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null) {


                DestroyPickup();
            }
        }
    }

    public void DestroyPickup() {
        Destroy(gameObject);
    }
}
