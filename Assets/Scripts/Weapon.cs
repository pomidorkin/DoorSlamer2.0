using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int weaponDamage = 15;
    public int GetWeaponDamage()
    {
        return weaponDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
        }
    }
}
