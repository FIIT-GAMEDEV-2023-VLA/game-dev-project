// Author: Leonard Puškáč
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
        // IF THE OTHER OBJECT IS A TORCH, IF ITS PARENT IS THIS PATH, IF IS NOT VISITED
        if (other.gameObject.CompareTag("Torch") && other.gameObject.transform.parent == transform.parent.transform.parent && !visited)
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

    public void ResetVisitedFlag()
    {
        visited = false;
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
