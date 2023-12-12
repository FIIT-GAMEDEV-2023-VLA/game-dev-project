// Author: Leonard Puškáč
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    [SerializeField] private float unlockedCameraMoveSpeed = 25f;
    private bool isLocked;
    
    private Transform moveTargetTransform;
    private Transform lockedTargetTransform;
    private Transform playerTransform;

    private GameObject playerGameObject;
    private PlayerScript playerScript;

    private float shakeTime = 0f;
    private float shakeAmount = 0.5f;
    
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<PlayerScript>();
        playerTransform = playerGameObject.transform;
        LockToPlayer();
        moveTargetTransform = playerTransform;
    }
    public void LockToPlayer()
    {
        isLocked = true;
        lockedTargetTransform = playerTransform;
    }
    
    public void LockTo(Transform targetTransform)
    {
        isLocked = true;
        lockedTargetTransform = targetTransform;
        Debug.Log("Locking to: " + lockedTargetTransform + " Position: " + lockedTargetTransform.position);
    }

    void Update()
    {
        if (isLocked)
        {   
            Vector3 lockedTargetPosition = lockedTargetTransform.position;
            transform.position = new Vector3(lockedTargetPosition.x, lockedTargetPosition.y, lockedTargetPosition.z);
            
            // SCREEN SHAKE
            if (shakeTime > 0f) {
                Vector3 shakeOffset = (Random.insideUnitCircle * shakeAmount);
                transform.position += shakeOffset;
                shakeTime -= Time.deltaTime;
 
            } else {
                shakeTime = 0.0f;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTargetTransform.position, unlockedCameraMoveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, moveTargetTransform.position) < 0.01f)
            {
                LockToPlayer();
            }
            if (Vector3.Distance(transform.position, moveTargetTransform.position) < 3f)
            {
                playerScript.UnlockInput();
            }
        }
    }

    public void MoveToPlayer()
    {
        isLocked = false;
        moveTargetTransform = playerTransform;
    }

    public void ShakeCamera(float t, float intensity)
    {
        shakeTime = t;
        shakeAmount = intensity;
    }
}
