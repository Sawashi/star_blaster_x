using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTele : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(-5.36f, 1f, 0);
        }
    }
}
