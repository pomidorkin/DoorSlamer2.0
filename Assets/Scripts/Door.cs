using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorPrefab;
    [SerializeField] int doorDamage;
    public Animator animator;
    [SerializeField] AudioSource doorOpeningSound;
    SaveManager saveManager;
    [SerializeField] bool mainDoor = false;

    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        doorDamage = saveManager.State.doorDamage;
        doorOpeningSound.volume = saveManager.State.soundVolume;
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenDoor();
        }*/
    }

    public void Attack()
    {
            OpenDoor();
    }

    public void OpenDoor()
    {
        animator.Play("DoorOpeningAnimation");
        //animator.Play("DoorOpeningAnimationNormal");
    }

    public void PlayDoorSound()
    {
        if (mainDoor)
        {
            doorOpeningSound.Play();
        }
    }

    public int GetDoorDamage()
    {
        return doorDamage;
    }
}
