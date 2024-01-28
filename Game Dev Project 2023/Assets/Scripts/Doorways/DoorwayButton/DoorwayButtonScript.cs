// Author: Viktor Szabo

using System.Collections;
using UnityEngine;

public class DoorwayButtonScript : MonoBehaviour
{
    private DoorwayBScript doorway;
    private bool lockState;

    private void Start()
    {   
        doorway = gameObject.GetComponentInParent<DoorwayBScript>();
        lockState = false;
    }

    public void Open()
    {
        if (!lockState){
            doorway.Open();
            lockState = true;
        }
    }

    public void Reset()
    {
        lockState = false;
    }
}