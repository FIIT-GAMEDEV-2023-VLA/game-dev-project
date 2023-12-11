// Author: Leonard Puškáč

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapBouldersScript : MonoBehaviour
{

    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private TrapBouldersTriggerZoneScript trapBouldersTriggerZoneScript;
    [SerializeField] private Transform boulderContainerTransform;
    [SerializeField] private GameObject boulderPrefab;

    private List<Vector3> startingBoulderPositions;
    private List<Vector3> startingBoulderScales;
    
    private void Start()
    {
        // LOAD boulder transforms
        foreach (Transform child in boulderContainerTransform)
        {
            startingBoulderPositions.Add(new Vector3(child.position.x, child.position.y, child.position.z));
            startingBoulderScales.Add(new Vector3(child.localScale.x, child.localScale.y, child.localScale.z));
        }
    }

    public void ResetTrap()
    {
        trapBouldersTriggerZoneScript.ResetFlag();
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
