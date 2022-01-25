using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;
    SaveManager saveManager;
    AudioSource backgroundMusic;
    [SerializeField] GameObject muteImageMusic;
    [SerializeField] GameObject loudImageMusic;
    [SerializeField] GameObject muteImageSound;
    [SerializeField] GameObject loudImageSound;
    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();


        backgroundMusic = FindObjectOfType<AudioController>().GetAudioSource();
        musicVolumeSlider.value = saveManager.State.musicVolume;
        musicVolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        soundVolumeSlider.value = saveManager.State.soundVolume;
        soundVolumeSlider.onValueChanged.AddListener(delegate { SoundValueChancgeCheck(); });

        ControlImage();
    }

    public void SoundValueChancgeCheck()
    {
        saveManager.State.soundVolume = soundVolumeSlider.value;
        saveManager.Save();
        ControlImage();
    }

    public void ValueChangeCheck()
    {
        backgroundMusic.volume = musicVolumeSlider.value;
        saveManager.State.musicVolume = musicVolumeSlider.value;
        saveManager.Save();
        ControlImage();
    }

    private void ControlImage()
    {
        if (saveManager.State.musicVolume > 0)
        {
            loudImageMusic.SetActive(true);
            muteImageMusic.SetActive(false);
        }
        else
        {
            loudImageMusic.SetActive(false);
            muteImageMusic.SetActive(true);
        }

        if (saveManager.State.soundVolume > 0)
        {
            loudImageSound.SetActive(true);
            muteImageSound.SetActive(false);
        }
        else
        {
            loudImageSound.SetActive(false);
            muteImageSound.SetActive(true);
        }
    }
}
