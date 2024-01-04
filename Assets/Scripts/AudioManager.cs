
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public Sound[] musicSounds, sfxSounds;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //PlayMusic("BG");
        selectMusic();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s != null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    //Stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void selectMusic()
    {
        StopMusic();
        string nameNow = SceneManager.GetActiveScene().name;
        switch (nameNow)
        {
            case "MainMenu":
                PlayMusic("menu");
                break;
            case "LevelSelect":
                PlayMusic("LevelSelect");
                break;
            case "Map 2":
                PlayMusic("BG");
                break;
            case "level_2":
                PlayMusic("lv2");
                break;
            case "level_3":
                PlayMusic("lv3");
                break;
            case "boss_fight":
                PlayMusic("boss");
                break;
            default:
                PlayMusic("menu");
                break;
        }
    }

}
