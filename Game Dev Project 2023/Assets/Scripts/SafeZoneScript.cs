// Author: Leonard Puškáč

using System;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{

    [SerializeField] private int torchesToAdd = 2;
    [SerializeField] private Transform spawnPoint;
    private SpawnManagerScript spawnManagerScript;
    private ResourceManagerScript resourceManagerScript;

    private bool visited = false;

    private void Start()
    {
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManagerScript>();
        resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
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
                resourceManagerScript.AddTorch(torchesToAdd);
                spawnManagerScript.SetActiveSafeZone(gameObject);
            }
        }
    }

    public void ResetVisitedFlag()
    {
        visited = false;
    }

    public void SetVisitedFlag(bool flag)
    {
        visited = flag;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited a Safe Zone!");
        }
    }
}
