using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrapBouldersTriggerZoneScript : MonoBehaviour
{
    
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    private bool bouldersReleased = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !bouldersReleased)
        {
            edgeCollider2D.enabled = false;
            bouldersReleased = true;
            Debug.Log("Releasing Boulders!");
        }
    }
}
