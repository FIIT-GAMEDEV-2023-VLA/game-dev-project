// Author: Leonard Puškáč

using System.Collections;
using UnityEngine;


public class TrapBouldersTriggerZoneScript : MonoBehaviour
{
    
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private GameObject boulderContainer;
    [SerializeField] private float screenShakeTime = 3f;
    [SerializeField] private float screenShakeAmount = 0.5f;
    [SerializeField] private float releaseTriggerOffset = 0.8f;
    
    [SerializeField] private float bouldersTimeToLive = 15f;
    [SerializeField] private float bouldersMinCollisionVelocity = 1f;
    
    private bool bouldersReleased;
    private TrapBouldersScript trapBouldersScript;
    private CameraControllerScript cameraControllerScript;

    private void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera)
        {
            cameraControllerScript = mainCamera.GetComponent<CameraControllerScript>();
        }
        trapBouldersScript = gameObject.transform.parent.gameObject.GetComponent<TrapBouldersScript>();
        bouldersReleased = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !bouldersReleased)
        {
            
            Debug.Log("Releasing Boulders!");
            if (cameraControllerScript)
            {
                cameraControllerScript.ShakeCamera(screenShakeTime, screenShakeAmount);
            }

            StartCoroutine(ReleaseBoulders());
            StartCoroutine(TrapResetTimer());
        }
    }

    
    public void ResetFlag()
    {
        bouldersReleased = false;
    }
    
    private IEnumerator ReleaseBoulders()
    {
        // WAIT FOR OFFSET
        yield return new WaitForSeconds(releaseTriggerOffset);
        // DISABLE THE CONTAINER
        edgeCollider2D.enabled = false;
        // SET FLAG
        bouldersReleased = true;
        // SET EACH BOULDER TO RELEASED
        foreach (Transform boulderTransform in boulderContainer.transform)
        {
            TrapBouldersCollisionScript boulderScript = boulderTransform.gameObject.GetComponent<TrapBouldersCollisionScript>();
            if (boulderScript)
            {
                boulderScript.Release(bouldersTimeToLive, bouldersMinCollisionVelocity);
            }
        }
    }
    private IEnumerator TrapResetTimer()
    {
        yield return new WaitForSeconds(10f);
        trapBouldersScript.ResetTrap();
    }
}
