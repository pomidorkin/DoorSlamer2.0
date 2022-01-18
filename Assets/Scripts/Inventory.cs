using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public int coins = 3000;
    public int gems = 1000;

    public int doorDamage = 15;
    public int doorDamageUpgradeCounter = 1;
    public int buildingHealth = 500;
    public int buildingHealthUpgradeCounter = 1;
    //public int buildingHealth { set; get; }

    public bool toiletPurchased { set; get; }
    public bool speacialBuildigPurchased { set; get; }
    public bool removeAddPurchased { set; get; }

    public float musicVolume = 1.0f;
}
