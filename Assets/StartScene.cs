using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Load your game scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}