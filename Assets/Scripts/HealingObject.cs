using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealingObject : MonoBehaviour
{
    [SerializeField] GameObject healingObject;
    [SerializeField] GameObject objectToHide;
    [SerializeField] Building buildingSlot;
    [SerializeField] Button spawnHealingObjectButton;
    [SerializeField] int energyPrice = 50;
    [SerializeField] EnergyCounter energyCounter;
    [SerializeField] float healSpeed = 3f;
    [SerializeField] int healingAmount = 3;

    private bool spawned = false;

    void Start()
    {
        var saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        ActivateButton(saveManager);
        spawnHealingObjectButton.GetComponentInChildren<TMP_Text>().text = ("Spawn Tree " + energyPrice.ToString());
    }



    private void ActivateButton(SaveManager saveManager)
    {
        if (saveManager.State.healingObjectPurchased)
        {
            spawnHealingObjectButton.gameObject.SetActive(true);
        }
    }

    public void SpawnHealingObject()
    {
        if (energyCounter.slider.value >= energyPrice)
        {
            energyCounter.slider.value -= energyPrice;
            healingObject.SetActive(true);
            objectToHide.SetActive(false);
            spawned = true;
            spawnHealingObjectButton.interactable = false;
            InvokeRepeating("Heal", 0f, healSpeed);
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

    private void Heal()
    {
            AddHealth(healingAmount);
    }

    private void AddHealth(int amount)
    {
        buildingSlot.IncreaseHealth(amount);
        Debug.Log("AddHealth(); called");
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
