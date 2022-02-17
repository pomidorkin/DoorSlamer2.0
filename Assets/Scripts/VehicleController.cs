using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VehicleController : MonoBehaviour
{
    
    [SerializeField] GameObject vehicle;
    [SerializeField] GameObject rightWeelOne;
    [SerializeField] GameObject rightWeelTwo;
    [SerializeField] GameObject rightWeelThree;
    [SerializeField] EnergyCounter energyCounter;

    [SerializeField] Button vehicleButton;
    [SerializeField] int energyPrice = 100;


    private Vector3 initialPosition;
    private bool isMoving = false;
    private void Start()
    {
        initialPosition = vehicle.transform.position;
        var saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        SpawnVehicle(saveManager);

        vehicleButton.GetComponentInChildren<TMP_Text>().text = ("Launch Truck " + energyPrice.ToString());
    }

    public void SpawnVehicle(SaveManager sm)
    {
        if (sm.State.vehiclePruchased)
        {
            vehicle.SetActive(true);
            vehicleButton.gameObject.SetActive(true);
        }

    }
    private void MoveVehicle()
    {
        vehicle.transform.Translate(new Vector3(1, 0, 0) * 20 * Time.deltaTime);
        rightWeelOne.transform.Rotate(250 * Time.deltaTime, 0, 0);
        rightWeelTwo.transform.Rotate(250 * Time.deltaTime, 0, 0);
        rightWeelThree.transform.Rotate(250 * Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveVehicle();
            if (vehicle.transform.position.x >= 40)
            {
                vehicle.transform.position = initialPosition;
                isMoving = false;
            }
        }
        
    }

    public void StartMovingCar()
    {
        energyCounter.slider.value -= 100;
        energyCounter.SetButtonsInteractable(false);
        isMoving = true;
    }

    public int GetEnerdyPrice()
    {
        return energyPrice;
    }

}
