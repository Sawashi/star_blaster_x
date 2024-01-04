
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mixer;

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
		if (PlayerPrefs.HasKey("Volume") || PlayerPrefs.HasKey("SFX") || PlayerPrefs.HasKey("Music")) {
            mixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));

            mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));

            mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));

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
            case "Win_Scene":
                PlayMusic("win");
                break;
            default:
                PlayMusic("menu");
                break;
        }
    }

}
