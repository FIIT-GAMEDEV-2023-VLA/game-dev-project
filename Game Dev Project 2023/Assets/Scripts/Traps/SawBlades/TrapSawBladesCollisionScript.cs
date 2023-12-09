// Author: Leonard Puškáč
using UnityEngine;

public class TrapSawBladesCollisionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            other.gameObject.SendMessage("CollideWithTrap");
        }
    }
}
