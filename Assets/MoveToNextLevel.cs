using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;
    AudioManager audioManager;
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.StopMusic();
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextSceneLoad);
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }
    }
}
