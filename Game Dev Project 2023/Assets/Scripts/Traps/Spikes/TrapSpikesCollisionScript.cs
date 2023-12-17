// Author: Viktor Szabo
using UnityEngine;

public class TrapSpikesCollisionScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            other.gameObject.SendMessage("CollideWithTrap");
        }
    }
}