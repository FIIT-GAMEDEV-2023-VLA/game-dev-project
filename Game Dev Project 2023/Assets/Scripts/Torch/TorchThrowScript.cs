// Author: Leonard Puškáč
// The function called GetBounceVelocity was take from: https://discussions.unity.com/t/how-can-i-solve-ballistic-angle-and-velocity-to-hit-a-specific-point-after-a-specific-amount-of-time/179059/3

using UnityEngine;

public class TorchThrowScript : MonoBehaviour
{
    [SerializeField] private Transform torchTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private CameraControllerScript cameraControllerScript;
    private TorchPathScript torchPathScript;
    private Transform pathContainerTransform;
    private float maxDepth = -100f;
    // Start is called before the first frame update
    void Start()
    {
        cameraControllerScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControllerScript>();
        Debug.Log("Torch is Alive!");
        cameraControllerScript.LockTo(transform);
        rb.AddTorque(Random.Range(-0.2f, 0.2f), ForceMode2D.Impulse);
    }

    public void Update()
    {
        if (transform.position.y <= maxDepth)
        {
            TorchEnd();
        }
    }

    private Vector3 GetBounceVelocity(Vector3 source, Vector3 target, float t)
    {
        // CODE TAKEN FROM: https://discussions.unity.com/t/how-can-i-solve-ballistic-angle-and-velocity-to-hit-a-specific-point-after-a-specific-amount-of-time/179059/3
        float vx = (target.x - source.x) / t;
        float vz = (target.z - source.z) / t;
        float vy = ((target.y - source.y) - 0.5f * Physics.gravity.y * t * t) / t;
        return new Vector3(vx, vy, vz);
    }
    

    public void TorchEnd()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("torch_death");
    }

    public void TorchDestruct()
    {
        cameraControllerScript.MoveToPlayer();
        Destroy(gameObject);
        torchPathScript.ResetTriggerPoints();
    }

    public void SetMaxDepth(float y)
    {
        maxDepth = y;
    }

    public void SetTorchPathScript(TorchPathScript script)
    {
        torchPathScript = script;
    }

    public void Bounce(Vector3 targetPos)
    {
        rb.angularVelocity = 0f;
        rb.velocity = GetBounceVelocity(torchTransform.position, targetPos, 1.5f);
        float dirX = torchTransform.position.x - targetPos.x;
        float torqueMulti = dirX > 0f ? 1 : -1;
        float torque = Random.Range(1f, 2.5f);
        rb.AddTorque(torque * torqueMulti, ForceMode2D.Impulse);
    }
}
