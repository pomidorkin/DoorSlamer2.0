using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthDisplay : MonoBehaviour
{
    [SerializeField] Zombie zombie;
    [SerializeField] Image healthBarImage;
    private int health;
    private void Awake()
    {
        zombie.OnHealthUpdated += HandleHealthUpdated;
    }
    private void OnDestroy()
    {
        zombie.OnHealthUpdated -= HandleHealthUpdated;
    }
    private void Start()
    {
        health = zombie.GetHealth();
    }

    private void HandleHealthUpdated(int currentHealth)
    {
        healthBarImage.fillAmount = (float) currentHealth / health;
        Debug.Log("Initial health: " + health + " current health: " + currentHealth);
    }
}
