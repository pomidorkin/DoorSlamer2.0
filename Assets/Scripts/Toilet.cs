using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Toilet : MonoBehaviour
{
    [SerializeField] GameObject toiletPrefab;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] Button spawnToiletButton;
    [SerializeField] int energyPrice = 50;
    [SerializeField] EnergyCounter energyCounter;
    [SerializeField] GameObject toiletObject;
    [SerializeField] GameObject fartArea;
    [SerializeField] ParticleSystem fartSmoke;
    private float fartCounter;
    private bool spawned = false;

    private IEnumerator coroutine;

    void Start()
    {
        var saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        ActivateButton(saveManager);
        spawnToiletButton.GetComponentInChildren<TMP_Text>().text = ("Spawn Toilet " + energyPrice.ToString());
    }



    private void ActivateButton(SaveManager saveManager)
    {
        if (saveManager.State.toiletPurchased)
        {
            spawnToiletButton.gameObject.SetActive(true);
        }
    }

    public void SpawnToilet()
    {
        if (energyCounter.slider.value >= energyPrice)
        {
            energyCounter.slider.value -= energyPrice;
            toiletObject.SetActive(true);
            spawned = true;
            spawnToiletButton.interactable = false;
        }
        if (energyCounter.slider.value < energyPrice)
        {
            energyCounter.SetButtonsInteractable(false);
        }
        if (energyCounter.slider.value < 100)
        {
            energyCounter.SetVehicleButtonInteractable(false);
        }
    }

    private void SpawnDoor()
    {
        Instantiate(doorPrefab, this.transform.position, Quaternion.identity);
    }


    void Update()
    {
        if (fartCounter < 5f)
        {
            fartCounter += Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (spawned && fartCounter >= 1.5f)
        {
            fartArea.SetActive(true);
            fartCounter = 0;
            coroutine = SetTriggerActiveFalse(2f);
            StartCoroutine(coroutine);
            fartSmoke.Play();
        }
        Debug.Log("Toilet is attacking");
    }

    private IEnumerator SetTriggerActiveFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        fartArea.SetActive(false);
    }

    public int GetEnerdyPrice()
    {
        return energyPrice;
    }
    public bool GetSpawned()
    {
        return spawned;
    }
}
