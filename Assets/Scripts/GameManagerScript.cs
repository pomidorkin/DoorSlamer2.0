using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManagerScript : MonoBehaviour
{
    //[SerializeField] Building mainBuilding;
    [SerializeField] Door mainBuildingDoor;
    [SerializeField] SpecialBuilding specialBuilding;
    [SerializeField] Door specialBuildingDoor;
    [SerializeField] Toilet toiletBuilding;
    [SerializeField] Door toiletBuildingDoor;

    [SerializeField] Building building;
    [SerializeField] HealthDisplay healthDisplay;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject hoodMenu;
    SaveManager saveManager;

    private BalanceDisplay balanceDisplay;

    [SerializeField] int healthRestorePrice = 5;
    private void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        balanceDisplay = FindObjectOfType<BalanceDisplay>();
        ResumeGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Attack()
    {
        mainBuildingDoor.Attack();
        specialBuilding.Attack();
        specialBuildingDoor.Attack();
        toiletBuilding.Attack();
        toiletBuildingDoor.Attack();
    }

    public void RestoreBuilgingHealth()
    {
        if (saveManager.State.gems >= healthRestorePrice)
        {
            saveManager.State.gems -= healthRestorePrice;
            SaveManager.Instance.Save();

            int health = building.FillHealth();
            healthDisplay.SetDisplay(health);

            gameOverMenu.gameObject.SetActive(false);
            hoodMenu.gameObject.SetActive(true);
            ResumeGame();
        }
        balanceDisplay.DisplayBalance();
    }

    public int GetHealthRestorePrice()
    {
        return healthRestorePrice;
    }
}
