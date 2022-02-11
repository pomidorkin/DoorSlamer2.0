using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] GameObject[] zombieModels;
    private Building buildingSlot;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 15;
    [SerializeField] int energyValue = 3;
    [SerializeField] int minCoinValue = 1;
    [SerializeField] int maxCoinValue = 5;
    private Animator animator;
    private bool isAttacking = false;
    [SerializeField] float attackSpeed = 2.0f;
    private float attackCounter = 0;
    SaveManager saveManager;
    private float currentMoveSpeed = 0;

    private bool isSlowed = false;

    private EnergyCounter energyCounter;
    private BalanceDisplay balanceDisplay;

    private void Start()
    {
        saveManager = SaveManager.Instance;
        SaveManager.Instance.Load();
        buildingSlot = FindObjectOfType<Building>();
        energyCounter = FindObjectOfType<EnergyCounter>();
        balanceDisplay = FindObjectOfType<BalanceDisplay>();
        currentMoveSpeed = moveSpeed;
        SpawnRandomZombieModel();
        animator = GetComponentInChildren<Animator>();
    }

    private void SpawnRandomZombieModel()
    {
        int zombieNumber = Random.Range(0, (zombieModels.Length - 1));
        GameObject zombiePrefab = Instantiate(zombieModels[zombieNumber], new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), Quaternion.Euler(0, -90, 0) , transform);
        zombiePrefab.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * (currentMoveSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Euler(0, 0, 0);
        /* if (isAttacking)
         {
             Attack();
         }*/



        attackCounter += Time.deltaTime;
        if (attackCounter >= attackSpeed)
        {
            attackCounter = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Debug.Log("Collision with the door");
            if (collision.gameObject.GetComponent<Door>())
            {
                health -= collision.gameObject.GetComponent<Door>().GetDoorDamage();
                Debug.Log("My health: " + health);
                if(health <= 0)
                {
                    DestroySelf();
                }
            }
        }

        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("Collision with the weapon");
            if (collision.gameObject.GetComponent<Weapon>())
            {
                health -= collision.gameObject.GetComponent<Weapon>().GetWeaponDamage();
                Debug.Log("My health: " + health);
                if (health <= 0)
                {
                    DestroySelf();
                }
            }
        }
    }


    private void DestroySelf()
    {
        saveManager.State.coins += Random.Range(minCoinValue, maxCoinValue);
        SaveManager.Instance.Save();
        if(energyCounter == null)
        {
            energyCounter = FindObjectOfType<EnergyCounter>();
        }
        energyCounter.AddEnergy(energyValue);
        balanceDisplay.DisplayBalance();
        Destroy(gameObject);
    }

    public void Attack()
    {
        currentMoveSpeed = 0f;
        this.isAttacking = true;
        Debug.Log("Attacking...");
        InvokeRepeating("DealDamage", 0f, attackSpeed);
        // deal damage
        // attacking animation
        animator.SetBool("isAttacking", true);
    }

    public void StartMoving()
    {
        currentMoveSpeed = moveSpeed;
        animator.SetBool("isAttacking", false);

    }

    private void DealDamage()
    {
        buildingSlot.DecreaseHealth(damage);
    }

    public void AddHealth(int amount)
    {
        health += amount;
    }

    public float GetMoveSpeed()
    {
        return currentMoveSpeed;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        this.currentMoveSpeed = newMoveSpeed;
    }

    public bool IsSlowed()
    {
        return isSlowed;
    }

    public void SetSlowed(bool isSlowed)
    {
        this.isSlowed = isSlowed;
    }
}
