// Author: Leonard Puškáč
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    private GameObject[] safeZones;
    private GameObject activeSafeZone;
    private GameObject startingSafeZone;
    private PlayerScript playerScript;

    private Vector3 originalStartingPlayerPosition; 
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        originalStartingPlayerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        safeZones = GameObject.FindGameObjectsWithTag("SafeZone");
        if (safeZones.Length > 0)
        {
            startingSafeZone = FindStartingSafeZone();
            SetActiveSafeZone(startingSafeZone);
            SpawnPlayer();
        }
    }

    public void LoadSavedGameSpawn(Data data)
    {
        SetActiveSafeZone(safeZones[data.saveZoneIndex]);
        SafeZoneScript safeZoneScript = activeSafeZone.GetComponent<SafeZoneScript>();
        if (safeZoneScript)
        {
            safeZoneScript.SetVisitedFlag(true);
            Debug.Log("Setting Saved Safe Zone to Visited!");
        }

        playerScript.Spawn(new Vector3(data.positionX, data.positionY, data.positionZ));
        Debug.Log("SpawnManager Loaded Saved Game Data!");
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

    public int GetActiveSafeZoneIndex()
    {
        int index = 0;
        for (int i = 0; i < safeZones.Length; i++)
        {
            if (safeZones[i] == activeSafeZone)
            {
                index = i;
            }
        }
        Debug.Log("Active Safe Zone Index: " + index);
        return index;
    }

    public void SpawnPlayer()
    {
        if (playerScript)
        {
            if (activeSafeZone)
            {
                playerScript.Spawn(activeSafeZone.GetComponent<SafeZoneScript>().GetSpawnPointPosition());
            }
            else
            {
                playerScript.Spawn(originalStartingPlayerPosition);
            }

            Debug.Log("Spawn Manager is spawning the player!");
            
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
