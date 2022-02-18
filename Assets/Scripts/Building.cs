using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Building : MonoBehaviour
{
    [SerializeField] int maxHealth; // Надо в сейв менеджер как-то назначит начальное значение здоровья
    [SerializeField] int health;
    //private GameObject spawnedBuilding;
    SaveManager saveManager;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    [SerializeField] Button watchAddButton;
    [SerializeField] GameManagerScript gameManagerScript;
    [SerializeField] Button restoreHealthButton;
    //[SerializeField] DoorController doorObject;

    [SerializeField] InterAd interAd;
    public bool rewardedAddWatched = false;

    [SerializeField] HealthDisplay healthSlider;
    void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        maxHealth = saveManager.State.buildingHealth;
        health = maxHealth;
    }


    public void DecreaseHealth(int amount)
    {
        health -= amount;

        healthSlider.DecreaseValue(health);
        if (health <= 0)
        {
            gameManagerScript.PauseGame();

            if (!rewardedAddWatched && !saveManager.State.removeAddPurchased)
            {
                interAd.ShowAd();
            }

            if (!rewardedAddWatched)
            {
                gameOverMenu.gameObject.SetActive(true);
                hoodMenu.gameObject.SetActive(false);
                watchAddButton.interactable = true;
                if (saveManager.State.gems < gameManagerScript.GetHealthRestorePrice())
                {
                    restoreHealthButton.interactable = false;
                }
            }
            else
            {
                gameOverMenu.gameObject.SetActive(true);
                hoodMenu.gameObject.SetActive(false);
                watchAddButton.interactable = false;
                if (saveManager.State.gems < gameManagerScript.GetHealthRestorePrice())
                {
                    restoreHealthButton.interactable = false;
                }
            }
        }
        Debug.Log("Got hit!");
    }

    public void IncreaseHealth(int amount)
    {
        if (health > 0 && health < maxHealth)
        {
            health += amount;
            healthSlider.DecreaseValue(health);
        }
        
    }

    public int FillHealth()
    {
        health = maxHealth;
        return health;
    }

    public int GetHealth()
    {
        return health;
    }
}
