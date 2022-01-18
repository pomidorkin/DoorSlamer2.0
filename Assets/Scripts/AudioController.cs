using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    SaveManager saveManager;

    private static AudioController instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }


    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.volume = saveManager.State.musicVolume;
        m_MyAudioSource.Play();
    }

    public AudioSource GetAudioSource()
    {
        return m_MyAudioSource;
    }
}
