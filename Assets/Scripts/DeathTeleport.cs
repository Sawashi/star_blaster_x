using UnityEngine;

public class MovePlayerOnTouch : MonoBehaviour
{
    //when trigger 2D, move player to target position
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(-7.36f, -1.81f, 0);
        }
    }

}
