using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCounter : MonoBehaviour
{
    [SerializeField] Button spawnSpecialButton;
    [SerializeField] Button spawnToiletButton;
    [SerializeField] Button vehicleButton;
    [SerializeField] Button healingObjectButton;
    [SerializeField] SpecialBuilding specialBuilding;
    [SerializeField] Toilet toilet;
    [SerializeField] VehicleController vehicle;
    [SerializeField] HealingObject healingObject;
    private int maxEnergy = 100;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnergy(int amount)
    {
        if ((slider.value + amount) < maxEnergy)
        {
            slider.value += amount;
        }else if((slider.value + amount) >= maxEnergy)
        {
            slider.value = maxEnergy;
        }
        if (slider.value >= specialBuilding.GetEnerdyPrice() && !specialBuilding.GetSpawned())
        {
            spawnSpecialButton.interactable = true;
        }
        if (slider.value >= toilet.GetEnerdyPrice() && !toilet.GetSpawned())
        {
            spawnToiletButton.interactable = true;
        }
        if (slider.value >= vehicle.GetEnerdyPrice())
        {
            vehicleButton.interactable = true;
        }
        if (slider.value >= healingObject.GetEnerdyPrice() && !healingObject.GetSpawned())
        {
            healingObjectButton.interactable = true;
        }
    }

    public void SetButtonsInteractable(bool value)
    {
        spawnSpecialButton.interactable = value;
        spawnToiletButton.interactable = value;
        vehicleButton.interactable = value;
        healingObjectButton.interactable = value;
    }
    public void SetVehicleButtonInteractable(bool value)
    {
        vehicleButton.interactable = value;
    }
}
