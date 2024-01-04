using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] lvButtons;

    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for (int i = 0; i < lvButtons.Length; i++)
        {
            if (i + 2 > levelAt)
            {
                lvButtons[i].interactable = false;
            }
        }
    }
    public void changeScene1()
    {
        SceneManager.LoadScene("Map 2");
    }
    public void changeScene2()
    {
        SceneManager.LoadScene("level_2");
    }
    public void changeScene3()
    {
        SceneManager.LoadScene("level_3");
    }
    public void changeScene4()
    {
        SceneManager.LoadScene("boss_fight");
    }
    public void changeSceneo()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
