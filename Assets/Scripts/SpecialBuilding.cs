using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialBuilding : MonoBehaviour
{
    [SerializeField] GameObject[] weaponPrefabs; // Лист вякого мусора, который будет скидываться на зомби
    [SerializeField] GameObject specialBuildingPrefab;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] Button spawnSpecialButton;
    [SerializeField] int energyPrice = 50;
    [SerializeField] EnergyCounter energyCounter;
    [SerializeField] GameObject specialBuildingObject;
    [SerializeField] GameObject weaponSpawnPoint;
    SaveManager saveManager;
    private bool spawned = false;

    private float timeCounter;
    [SerializeField] float maxCounter = 1.5f;

    private void Start()
    {
        // Проверить куплен ли слот, если да, то делаем кнопку постройки здания видимойвидимой
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        //SpawnBuilding(saveManager);
        ActivateButton(saveManager);
        timeCounter = 0;

        spawnSpecialButton.GetComponentInChildren<TMP_Text>().text = ("Spawn Special " + energyPrice.ToString());
    }

    private void SpawnDoor()
    {
        Instantiate(doorPrefab, this.transform.position, Quaternion.identity);
    }

    private void ActivateButton(SaveManager saveManager)
    {
        if (saveManager.State.speacialBuildigPurchased)
        {
            spawnSpecialButton.gameObject.SetActive(true);
        }

    }

    public void SpawnBuilding()
    {
        if (energyCounter.slider.value >= energyPrice)
        {
            energyCounter.slider.value -= energyPrice;
            //Instantiate(specialBuildingPrefab, this.transform.position, Quaternion.identity);
            //SpawnDoor();
            specialBuildingObject.SetActive(true);
            spawned = true;
            spawnSpecialButton.interactable = false;

        }
        else
        {
            // Not enough energy
        }
        if(energyCounter.slider.value < energyPrice)
        {
            energyCounter.SetButtonsInteractable(false);
        }
        if (energyCounter.slider.value < 100)
        {
            energyCounter.SetVehicleButtonInteractable(false);
        }

    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && spawned)
        {
            Attack();
        }*/

        if (timeCounter < maxCounter)
        {
            timeCounter += Time.deltaTime;
            //Debug.Log(timeCounter);
        }
    }

    public void Attack()
    {
        if (timeCounter >= maxCounter && spawned)
        {
            int weaponNumber = Random.Range(0, (weaponPrefabs.Length - 1));
            GameObject barrel;
            //barrel = Instantiate(weaponPrefabs[weaponNumber], new Vector3(-21.9f, 2.75f, 1.6f), Quaternion.Euler(0, 0, 90f));
            barrel = Instantiate(weaponPrefabs[weaponNumber], weaponSpawnPoint.transform.position, Quaternion.Euler(0, 0, 90f));
            Destroy(barrel, 2f);
            timeCounter = 0;
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
