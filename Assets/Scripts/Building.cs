using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Building : MonoBehaviour
{
    [SerializeField] int maxHealth; // Надо в сейв менеджер как-то назначит начальное значение здоровья
    private int health;
    //private GameObject spawnedBuilding;
    SaveManager saveManager;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    [SerializeField] Button watchAddButton;
    [SerializeField] GameManagerScript gameManagerScript;
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
            // TODO: Game Lost Logic
            //sceneLoader.LoadScene(0); // commented out for testing

            //gameOverMenu.gameObject.SetActive(true);
            //hoodMenu.gameObject.SetActive(false);
            //zombieSpawner.SetSpawnBool(false);
            gameManagerScript.PauseGame();

            if (!rewardedAddWatched && !saveManager.State.removeAddPurchased)
            {
                interAd.ShowAd();
            }else if (!rewardedAddWatched)
            {
                gameOverMenu.gameObject.SetActive(true);
                hoodMenu.gameObject.SetActive(false);
                watchAddButton.interactable = true;
            }
            else
            {
                gameOverMenu.gameObject.SetActive(true);
                hoodMenu.gameObject.SetActive(false);
                watchAddButton.interactable = false;
            }
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
