using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSawBladesCollisionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            Debug.Log("Saw Trap Triggered On Player!");
            other.gameObject.SendMessage("CollideWithTrap");
        }
    }
}
