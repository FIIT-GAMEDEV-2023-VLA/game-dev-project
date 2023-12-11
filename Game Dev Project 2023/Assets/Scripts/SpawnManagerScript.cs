// Author: Leonard Puškáč
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    private GameObject[] safeZones;
    private GameObject activeSafeZone;
    private GameObject startingSafeZone;
    private PlayerScript playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        safeZones = GameObject.FindGameObjectsWithTag("SafeZone");
        if (safeZones.Length > 0)
        {
            startingSafeZone = FindStartingSafeZone();
            SetActiveSafeZone(startingSafeZone);
            SpawnPlayer();
        }
    }

    private GameObject FindStartingSafeZone()
    {
        GameObject newStartingSafeZone = null;
        foreach (var safeZone in safeZones)
        {
            if (safeZone.GetComponent<SafeZoneScript>().IsStartingSafeZone())
            {
                newStartingSafeZone = safeZone;
            }
        }
        return newStartingSafeZone;
    }

    public void SpawnPlayer()
    {
        if (playerScript)
        {
            Debug.Log("Spawn Manager is spawning the player!");
            playerScript.Spawn(activeSafeZone.GetComponent<SafeZoneScript>().GetSpawnPointPosition());
        }
    }

    public void ResetGame()
    {
        SetActiveSafeZone(startingSafeZone);
        ResetSafeZones();
        SpawnPlayer();
    }

    private void ResetSafeZones()
    {
        foreach (var safeZone in safeZones)
        {
            SafeZoneScript safeZoneScript = safeZone.GetComponent<SafeZoneScript>();
            safeZoneScript.ResetVisitedFlag();
        }
    }

    public void SetActiveSafeZone(GameObject safeZone)
    {
        if (activeSafeZone != safeZone)
        {
            Debug.Log("Setting activeSafeZone to: " + safeZone);
            activeSafeZone = safeZone;  
        }
    }
}
