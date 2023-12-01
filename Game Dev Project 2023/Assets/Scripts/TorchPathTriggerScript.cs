using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorchPathTriggerScript : MonoBehaviour
{

    private bool visited = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!visited)
        {
            TorchThrowScript torchThrowScript = other.GetComponentInParent<TorchThrowScript>();
            Vector3 nextCoords = GetNextCoords();

            Debug.Log("sending torch to next coords: " + nextCoords);
            torchThrowScript.Bounce(nextCoords);
            visited = true;
        }
    }

    Vector3 GetNextCoords()
    {
        Vector3 targetPos = new Vector3();
        //Check if this isnt the last child
        int idx = transform.GetSiblingIndex();
        if (idx < transform.parent.transform.childCount - 1)
        {   
            Transform nextChild = transform.parent.transform.GetChild(transform.GetSiblingIndex() + 1);
            targetPos = nextChild.position;
        }
        return targetPos;
    }

    //[ExecuteInEditMode]
    //void OnDrawGizmosSelected()
    //{
    //    transform.parent.SendMessage("OnDrawGizmosSelected");
    //}
}
