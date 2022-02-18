using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public bool firstStart = true;

    public int coins = 0;
    public int gems = 0;

    public int doorDamage = 15;
    public int doorDamageUpgradeCounter = 1;
    public int buildingHealth = 500;
    public int buildingHealthUpgradeCounter = 1;
    //public int buildingHealth { set; get; }

    public bool toiletPurchased { set; get; }
    public bool speacialBuildigPurchased { set; get; }
    public bool vehiclePruchased { set; get; }
    public bool healingObjectPurchased { set; get; }
    public bool removeAddPurchased { set; get; }

    public float musicVolume = 1.0f;
    public float soundVolume = 1.0f;
}
