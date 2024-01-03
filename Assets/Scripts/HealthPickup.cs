using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healAmount);
                DestroyPickup();
            }
        }
    }

    public void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
