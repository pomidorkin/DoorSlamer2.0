using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            other.GetComponent<Zombie>().Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            other.GetComponent<Zombie>().StartMoving();
        }
    }
}
