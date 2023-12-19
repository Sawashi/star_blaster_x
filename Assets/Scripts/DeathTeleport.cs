using UnityEngine;

public class MovePlayerOnTouch : MonoBehaviour
{
    //when trigger 2D, move player to target position
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
<<<<<<< HEAD
            collision.transform.position = new Vector3(-7.36f, -1.81f, 0);
=======
            collision.transform.position = new Vector3(-9.36f, -0.81f, 0);
>>>>>>> ffa61fb250491614af977dd8a376523f0fd567fe
        }
    }

}
