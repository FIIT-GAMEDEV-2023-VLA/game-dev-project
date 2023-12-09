// Author: Leonard Puškáč

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TorchPathScript : MonoBehaviour
{
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform pathContainerTransform;

    private GameObject player;
    private PlayerScript playerScript;

    private List<TorchPathTriggerScript> triggerScripts;

    public void Start()
    {
        // GET PLAYER AND PLAYER SCRIPT OBJECTS
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }
        // GET TORCH TRIGGER POINTS IN THIS PATH CONTAINER
        triggerScripts = new List<TorchPathTriggerScript>();
        foreach (Transform triggerPoint in pathContainerTransform)
        {   
            triggerScripts.Add(triggerPoint.GetComponent<TorchPathTriggerScript>());
        }
    }

    public void SpawnTorch(Vector3 originPos)
    {
        Transform firstPoint = pathContainerTransform.GetChild(0);
        Transform lastPoint = pathContainerTransform.GetChild(pathContainerTransform.childCount - 1);
        if (firstPoint)
        {   
            GameObject newTorch = Instantiate(original: torchPrefab, parent: transform);
            TorchThrowScript newTorchThrowScript = newTorch.GetComponent<TorchThrowScript>();
            newTorch.transform.position = new Vector3(firstPoint.position.x, originPos.y, 0f);
            newTorchThrowScript.SetMaxDepth(lastPoint.position.y);
            newTorchThrowScript.SetTorchPathScript(this);
            
            playerScript.LockInput();
        }
    }

    public void ResetTriggerPoints()
    {
        foreach (TorchPathTriggerScript triggerScript in triggerScripts)
        {
            triggerScript.ResetVisitedFlag();
        }
    }
}
