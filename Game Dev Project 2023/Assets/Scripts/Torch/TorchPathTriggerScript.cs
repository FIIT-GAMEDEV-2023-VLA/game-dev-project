using System;
using UnityEngine;


public class TorchPathTriggerScript : MonoBehaviour
{

    private bool visited;

    private void Start()
    {
        visited = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!visited)
        {
            TorchThrowScript torchThrowScript = other.GetComponentInParent<TorchThrowScript>();
            if (!IsLastChild())
            {
                Vector3 nextCoords = GetNextCoords();
                Debug.Log("sending torch to next coords: " + nextCoords);
                torchThrowScript.Bounce(nextCoords);
            }
            else
            {
                torchThrowScript.TorchEnd();
                Debug.Log("Last Path Point!");
            }
            visited = true;
        }
    }
    Vector3 GetNextCoords()
    {
        return transform.parent.transform.GetChild(transform.GetSiblingIndex() + 1).position;
    }

    private bool IsLastChild()
    {
        if (transform.GetSiblingIndex() == transform.parent.transform.childCount - 1)
        {
            return true;
        }
        return false;
    }
}
