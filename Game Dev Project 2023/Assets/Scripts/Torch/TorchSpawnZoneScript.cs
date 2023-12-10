using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchSpawnZoneScript : MonoBehaviour
{
    [SerializeField] private Animator torchAnim;
    private ResourceManagerScript resourceManagerScript;
    void Start()
    {
        resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && resourceManagerScript.GetTorchCount() > 0)
        {
            torchAnim.SetTrigger("turn_on");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {
            torchAnim.SetTrigger("turn_off");
        }
    }
}
