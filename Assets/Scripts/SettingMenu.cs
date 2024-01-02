using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sfxSlider;
    public Slider musicSlider;
    public AudioMixer mixer;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume")|| PlayerPrefs.HasKey("SFX")|| PlayerPrefs.HasKey("Music"))
        {
            loadVolume();
        }
        else
        {
            SetVolume();
            SetSFX();
            SetMusic();
        }

    }
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        mixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);

    }
    public void SetSFX()
    {
        float SFX = sfxSlider.value;
        mixer.SetFloat("SFX", SFX);
        PlayerPrefs.SetFloat("SFX", SFX);
    }

    public void SetMusic()
    {
        float Music = musicSlider.value;
        mixer.SetFloat("Music", Music);
        PlayerPrefs.SetFloat("Music", Music);
    }

    private void loadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        SetMusic();
        SetVolume();
        SetSFX();
    }

}