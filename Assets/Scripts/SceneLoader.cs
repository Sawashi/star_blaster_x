using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    FadeInOut fade;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        fade = GetComponent<FadeInOut>();
        fade.FadeOut();
    }

    IEnumerator ReloadCoroutine(float timer, string name) {
        fade.FadeIn();
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(name);
    }

    IEnumerator ReloadCoroutine(float timer, int idx) {
        fade.FadeIn();
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(idx);
    }

    public void ReloadScene() {
        StartCoroutine(ReloadCoroutine(fade.duration, SceneManager.GetActiveScene().name));
    }
	
	public void LoadScene(string name) {
        StartCoroutine(ReloadCoroutine(fade.duration, name));
    }
    public void LoadScene(int idx) {
        StartCoroutine(ReloadCoroutine(fade.duration, idx));
    }
}
