// Author: Leonard Puškáč

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrapBouldersScript : MonoBehaviour
{

    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private GameObject triggerZone;
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private Transform boulderContainerTransform;

    private List<Vector3> startingBoulderPositions;
    private List<Vector3> startingBoulderScales;
    private TrapBouldersTriggerZoneScript triggerZoneScript;
    
    private void Start()
    {
        triggerZoneScript = triggerZone.GetComponent<TrapBouldersTriggerZoneScript>();
        
        startingBoulderPositions = new List<Vector3>();
        startingBoulderScales = new List<Vector3>();
        // LOAD boulder transforms
        foreach (Transform child in boulderContainerTransform)
        {
            startingBoulderPositions.Add(new Vector3(child.position.x, child.position.y, child.position.z));
            startingBoulderScales.Add(new Vector3(child.localScale.x, child.localScale.y, child.localScale.z));
        }
    }

    public void ResetTrap()
    {   
        Debug.Log("Resetting Boulder Trap!");
        triggerZoneScript.ResetFlag();
        if (!edgeCollider2D.enabled)
        {
            SpawnBoulders();
        }
        edgeCollider2D.enabled = true;
    }

    private void SpawnBoulders()
    {
        for (var i=0; i < startingBoulderPositions.Count; i++)
        {
            GameObject newBoulder = Instantiate(original: boulderPrefab, parent: boulderContainerTransform);
            newBoulder.transform.position = startingBoulderPositions[i];
            newBoulder.transform.localScale = startingBoulderScales[i];
        }
    }
}
