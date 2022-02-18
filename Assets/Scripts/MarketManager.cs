using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    [SerializeField] int builgindHealthUpgradeValue = 20;
    [SerializeField] int doorDamageUpgradeValue = 3;
    [SerializeField] int healthUpgradePrice = 100;
    [SerializeField] int doorDamageUpgradegPrice = 20; //gems
    [SerializeField] int specialSlotPrice = 2000;
    [SerializeField] int toiletSlotPrice = 100; // gems
    [SerializeField] int vehiclePrice = 3000;
    [SerializeField] int healingObjectPrice = 2500;

    // Pop Ups
    [SerializeField] GameObject marketMenu;
    [SerializeField] GameObject upgradeMenu;
    [SerializeField] GameObject footer;
    [SerializeField] GameObject settingsMenu;

    // Buttons
    [SerializeField] Button buySpecialSlotButton;
    [SerializeField] Button buyToiletSlotButton;
    [SerializeField] Button buyVehicleButton;
    [SerializeField] Button buyHealingObjectButton;
    [SerializeField] TMP_Text healthUpgradeButtonText;
    [SerializeField] TMP_Text doorDamageUpgradeButtonText;
    [SerializeField] Button upgradeDoorDamageButton;
    [SerializeField] Button upgradeBuildingHealthButton;
    [SerializeField] Button disableAdsButton;

    [SerializeField] TMP_Text buySecnodBuildingText;
    [SerializeField] TMP_Text buyToiletText;
    [SerializeField] TMP_Text buyVehicleText;
    [SerializeField] TMP_Text buyHealingObjectText;

    //
    [SerializeField] BalanceDisplay balanceDisplay;

    SaveManager saveManager;


    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();


        ManageButtons();

        
    }

    public void ManageButtons()
    {
        SaveManager.Instance.Load();
        balanceDisplay.DisplayBalance();

        buySecnodBuildingText.text = ("Buy Second Building " + specialSlotPrice);
        if (saveManager.State.coins < specialSlotPrice || saveManager.State.speacialBuildigPurchased)
        {
            buySpecialSlotButton.interactable = false;
        }
        else
        {
            buySpecialSlotButton.interactable = true;
        }

        buyToiletText.text = ("Buy Toilet  " + toiletSlotPrice);
        if (saveManager.State.gems < toiletSlotPrice || saveManager.State.toiletPurchased)
        {
            buyToiletSlotButton.interactable = false;
        }
        else
        {
            buyToiletSlotButton.interactable = true;
        }

        buyVehicleText.text = ("Buy Vehicle  " + vehiclePrice);
        if (saveManager.State.coins < vehiclePrice || saveManager.State.vehiclePruchased)
        {
            buyVehicleButton.interactable = false;
        }
        else
        {
            buyVehicleButton.interactable = true;
        }

        buyHealingObjectText.text = ("Buy Tree  " + healingObjectPrice);
        if (saveManager.State.coins < healingObjectPrice || saveManager.State.healingObjectPurchased)
        {
            buyHealingObjectButton.interactable = false;
        }
        else
        {
            buyHealingObjectButton.interactable = true;
        }

        // Disable buttons if purchased
        if (saveManager.State.speacialBuildigPurchased)
        {
            buySpecialSlotButton.interactable = false;
        }
        if (saveManager.State.toiletPurchased)
        {
            buyToiletSlotButton.interactable = false;
        }
        if (saveManager.State.removeAddPurchased)
        {
            disableAdsButton.interactable = false;
        }
        if (saveManager.State.vehiclePruchased)
        {
            buyVehicleButton.interactable = false;
        }
        if (saveManager.State.healingObjectPurchased)
        {
            buyHealingObjectButton.interactable = false;
        }

        healthUpgradeButtonText.text = (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter).ToString();
        doorDamageUpgradeButtonText.text = (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter).ToString();

        if ((saveManager.State.coins - (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter)) >= 0 && saveManager.State.buildingHealthUpgradeCounter < 10)
        {
            healthUpgradeButtonText.text = (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter).ToString();
            upgradeBuildingHealthButton.interactable = true;
        }
        else if (saveManager.State.buildingHealthUpgradeCounter >= 10)
        {
            healthUpgradeButtonText.text = "Max";
            upgradeBuildingHealthButton.interactable = false;
        }
        else
        {
            upgradeBuildingHealthButton.interactable = false;
        }

        if ((saveManager.State.gems - (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter)) >= 0 && saveManager.State.doorDamageUpgradeCounter < 7) // max 6 upgrades
        {
            doorDamageUpgradeButtonText.text = (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter).ToString();
            upgradeDoorDamageButton.interactable = true;
            Debug.Log(saveManager.State.doorDamageUpgradeCounter.ToString());
        }
        else if (saveManager.State.doorDamageUpgradeCounter >= 7)
        {
            doorDamageUpgradeButtonText.text = "Max";
            upgradeDoorDamageButton.interactable = false;
        }
        else
        {
            upgradeDoorDamageButton.interactable = false;
        }
    }

    public void OpenMarketMenu(bool isOpen)
    {
        marketMenu.gameObject.SetActive(isOpen);
        footer.gameObject.SetActive(!isOpen);
    }

    public void OpenUpgradeMenu(bool isOpen)
    {
        ManageButtons();
        upgradeMenu.gameObject.SetActive(isOpen);
        footer.gameObject.SetActive(!isOpen);
    }

    public void OpenSettingsMenu(bool isOpen)
    {
        settingsMenu.gameObject.SetActive(isOpen);
        footer.gameObject.SetActive(!isOpen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpgradeBuildingHealth()
    {
        if ((saveManager.State.coins - (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter)) >= 0 && saveManager.State.buildingHealthUpgradeCounter < 10)
        {
            saveManager.State.coins -= (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter);
            saveManager.State.buildingHealth += builgindHealthUpgradeValue;
            saveManager.State.buildingHealthUpgradeCounter += 1;
            SaveManager.Instance.Save();

            healthUpgradeButtonText.text = (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter).ToString();
            if ((saveManager.State.coins - (healthUpgradePrice * saveManager.State.buildingHealthUpgradeCounter)) < 0)
            {
                upgradeBuildingHealthButton.interactable = false;
            }
        }
        else if(saveManager.State.buildingHealthUpgradeCounter >= 10)
        {
            healthUpgradeButtonText.text = "Max";
            upgradeBuildingHealthButton.interactable = false;
        }
        else
        {
            upgradeBuildingHealthButton.interactable = false;
        }
        balanceDisplay.DisplayBalance();
    }
    public void UpgradeDoorDamage()
    {
        if ((saveManager.State.gems - (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter)) >= 0 && saveManager.State.doorDamageUpgradeCounter < 7 ) // max 6 upgrades
        {
            saveManager.State.gems -= (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter);
            saveManager.State.doorDamage += doorDamageUpgradeValue;
            saveManager.State.doorDamageUpgradeCounter += 1;
            SaveManager.Instance.Save();

            doorDamageUpgradeButtonText.text = (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter).ToString();
            if ((saveManager.State.gems - (doorDamageUpgradegPrice * saveManager.State.doorDamageUpgradeCounter)) < 0)
            {
                upgradeDoorDamageButton.interactable = false;
            }
            Debug.Log(saveManager.State.doorDamageUpgradeCounter.ToString());
        }
        else if(saveManager.State.doorDamageUpgradeCounter >= 7)
        {
            doorDamageUpgradeButtonText.text = "Max";
            upgradeDoorDamageButton.interactable = false;
        }
        else
        {
            upgradeDoorDamageButton.interactable = false;
        }
        balanceDisplay.DisplayBalance();
    }

    public void BuySpecialBuildingSlot()
    {
        if ((saveManager.State.coins - specialSlotPrice) >= 0)
        {
            saveManager.State.coins -= specialSlotPrice;
            saveManager.State.speacialBuildigPurchased = true;
            buySpecialSlotButton.interactable = false;
            SaveManager.Instance.Save();
            Debug.Log(saveManager.State.coins);
        }
        else
        {
            // Not enough money pop up
        }
        balanceDisplay.DisplayBalance();
        Debug.Log(saveManager.State.coins);
    }

    public void BuyToiletingSlot()
    {
        if ((saveManager.State.gems - toiletSlotPrice) >= 0)
        {
            saveManager.State.gems -= toiletSlotPrice;
            saveManager.State.toiletPurchased = true;
            buyToiletSlotButton.interactable = false;
            SaveManager.Instance.Save();
            Debug.Log(saveManager.State.gems);
        }
        else
        {
            // Not enough money pop up
        }
        balanceDisplay.DisplayBalance();
        Debug.Log(saveManager.State.coins);
    }

    public void BuyVehicle()
    {
        if ((saveManager.State.coins - vehiclePrice) >= 0)
        {
            saveManager.State.coins -= vehiclePrice;
            saveManager.State.vehiclePruchased = true;
            buyVehicleButton.interactable = false;
            SaveManager.Instance.Save();
            Debug.Log(saveManager.State.coins);
        }
        else
        {
            // Not enough money pop up
        }
        balanceDisplay.DisplayBalance();
    }

    public void BuyHealingObject()
    {
        if ((saveManager.State.coins - healingObjectPrice) >= 0)
        {
            saveManager.State.coins -= healingObjectPrice;
            saveManager.State.healingObjectPurchased = true;
            buyHealingObjectButton.interactable = false;
            SaveManager.Instance.Save();
            Debug.Log(saveManager.State.coins);
        }
        else
        {
            // Not enough money pop up
        }
        balanceDisplay.DisplayBalance();
    }

    public SaveManager GetSaveManager()
    {
        return saveManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
