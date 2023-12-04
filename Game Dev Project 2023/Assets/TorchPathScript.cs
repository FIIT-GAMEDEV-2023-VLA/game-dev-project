using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPathScript : MonoBehaviour
{
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private Transform pathContainerTransform;
    
    public void SpawnTorch(Vector3 originPos)
    {
        Transform firstPoint = pathContainerTransform.GetChild(0);
        Transform lastPoint = pathContainerTransform.GetChild(pathContainerTransform.childCount - 1);
        if (firstPoint)
        {   
            Debug.Log("Spawning Torch!");
            GameObject newTorch = Instantiate(original: torchPrefab, parent: transform);
            newTorch.transform.position = new Vector3(firstPoint.position.x, originPos.y, 0f);
            
            
            newTorch.SendMessage("SetMaxDepth", lastPoint.position.y);
        }
    }
}
