using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    SaveManager saveManager;
    AudioSource backgroundMusic;
    [SerializeField] GameObject muteImageMusic;
    [SerializeField] GameObject loudImageMusic;
    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();


        backgroundMusic = FindObjectOfType<AudioController>().GetAudioSource();
        musicVolumeSlider.value = saveManager.State.musicVolume;
        musicVolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

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
    }
}
