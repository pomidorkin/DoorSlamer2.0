using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorPrefab;
    [SerializeField] int doorDamage;
    public Animator animator;

    void Start()
    {
        var saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        doorDamage = saveManager.State.doorDamage;
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

    public int GetDoorDamage()
    {
        return doorDamage;
    }
}
