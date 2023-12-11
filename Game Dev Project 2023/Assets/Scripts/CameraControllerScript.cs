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
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTargetTransform.position, unlockedCameraMoveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, moveTargetTransform.position) < 0.01f)
            {
                LockToPlayer();
            }
            if (Vector3.Distance(transform.position, moveTargetTransform.position) < 10f)
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
}
