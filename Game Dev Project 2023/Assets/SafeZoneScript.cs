using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SpawnManagerScript spawnManagerScript;

    private bool visited = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Vector3 GetSpawnPointPosition()
    {
        return spawnPoint.transform.position;
    }

    public bool IsStartingSafeZone()
    {
        if (spawnPoint.gameObject.CompareTag("StartingSpawnPoint"))
        {
            return true;
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered a Safe Zone!");
            if (!visited)
            {
                visited = true;
                spawnManagerScript.SetActiveSafeZone(gameObject);
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited a Safe Zone!");
        }
    }
}
