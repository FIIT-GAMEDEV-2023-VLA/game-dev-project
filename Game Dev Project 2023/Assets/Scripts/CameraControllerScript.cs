using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cameraMoveSpeed = 5f;
    private bool isLocked;
    private Transform moveTargetTransform;
    private Transform lockedTargetTransform; 
    
    void Start()
    {
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
            var step =  cameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveTargetTransform.position, step);
            
            if (Vector3.Distance(transform.position, moveTargetTransform.position) < 0.01f)
            {
                LockToPlayer();
            }
        }
    }

    public void MoveToPlayer()
    {
        isLocked = false;
        moveTargetTransform = playerTransform;
    }
}
