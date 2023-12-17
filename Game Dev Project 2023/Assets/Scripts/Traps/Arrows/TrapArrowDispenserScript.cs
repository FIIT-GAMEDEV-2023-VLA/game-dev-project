using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowDispenserScript : MonoBehaviour
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float shootDelay = 2f;
    [SerializeField] private Transform spawnZone;
    [SerializeField] private float shootDirection = -1f;
    [SerializeField] private float arrowSpeed = 6f;

    private bool isSpawning;

    private void Awake()
    {
        isSpawning = false;
    }
    
    void Update()
    {
        if (!isSpawning)
        {
            Invoke("SpawnArrow", shootDelay);
            isSpawning = true;
        }
    }
    void SpawnArrow()
    {
        GameObject newArrow = Instantiate(original: arrowPrefab, parent: spawnZone);
        newArrow.transform.position = spawnZone.transform.position;
        TrapArrowScript arrowScript = newArrow.gameObject.GetComponent<TrapArrowScript>();
        if (arrowScript)
        {
            arrowScript.SetDirection(shootDirection);
            arrowScript.SetSpeed(arrowSpeed);
        }
        isSpawning = false;
    }
}
