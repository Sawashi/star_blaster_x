using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAttackRange : MonoBehaviour
{
    public bool playerInAttackRange = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInAttackRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInAttackRange = false;

        }
    }
}
