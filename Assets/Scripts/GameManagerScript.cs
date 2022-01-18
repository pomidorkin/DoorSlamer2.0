using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //[SerializeField] Building mainBuilding;
    [SerializeField] Door mainBuildingDoor;
    [SerializeField] SpecialBuilding specialBuilding;
    [SerializeField] Door specialBuildingDoor;
    [SerializeField] Toilet toiletBuilding;
    [SerializeField] Door toiletBuildingDoor;
    private void Start()
    {
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
}
