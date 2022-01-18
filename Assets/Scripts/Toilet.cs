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
    private bool spawned = false;
    // Start is called before the first frame update
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
            //Instantiate(toiletPrefab, this.transform.position, Quaternion.identity);
            //SpawnDoor();
            toiletObject.SetActive(true);
            spawned = true;
            spawnToiletButton.interactable = false;
        }
        else
        {
            // Not enough energy
        }
        if (energyCounter.slider.value < energyPrice)
        {
            energyCounter.SetButtonsInteractable(false);
        }
    }

    private void SpawnDoor()
    {
        Instantiate(doorPrefab, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && spawned)
        {
            Attack();
        }*/
    }

    public void Attack()
    {
        if (spawned)
        {
            // Выпускать облако едкого дыма, дебафающего зомби
        }
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
