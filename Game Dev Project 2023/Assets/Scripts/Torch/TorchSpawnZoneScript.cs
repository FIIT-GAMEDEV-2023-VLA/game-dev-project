using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSpawnZoneScript : MonoBehaviour
{
    private ResourceManagerScript resourceManagerScript;
    void Start()
    {
        resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && resourceManagerScript.GetTorchCount() > 0)
        {
            //TODO: ADD VISUAL CUE THAT THE PLAYER CAN THROW A TORCH FROM HERE
        }
    }
}
