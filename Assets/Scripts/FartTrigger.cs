using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (!zombie.IsSlowed())
            {
                zombie.SetMoveSpeed(zombie.GetMoveSpeed() * 0.5f);
            }
            Debug.Log("Zombie entered the fart area");
        }
    }
}
