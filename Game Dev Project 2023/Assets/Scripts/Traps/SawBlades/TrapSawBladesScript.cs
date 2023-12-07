using UnityEngine;

public class TrapSawBladesScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private Transform sawBlade;
    [SerializeField] private Transform checkPoint1;
    [SerializeField] private Transform checkPoint2;
    [SerializeField] private Animator anim;

    [SerializeField] private float sawBladeMoveSpeed = 5f;
    private Transform currentCheckPoint;
    void Start()
    {
        currentCheckPoint = checkPoint1;
        anim.Play("TrapSawBlades_On");

    }

    // Update is called once per frame
    void Update()
    {
        sawBlade.position = Vector3.MoveTowards(current: sawBlade.position, target: currentCheckPoint.position, maxDistanceDelta: sawBladeMoveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(sawBlade.position, currentCheckPoint.position) < 0.01f)
        {
            SwapCheckpoints();
        }
    }

    private void SwapCheckpoints()
    {
        currentCheckPoint = currentCheckPoint == checkPoint1 ? checkPoint2 : checkPoint1;
    }
    
    void OnDrawGizmos()
    {
        if (checkPoint1 && checkPoint2)
        {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(checkPoint1.position, checkPoint2.position);
            Gizmos.DrawSphere(checkPoint1.position, 0.5f);
            Gizmos.DrawSphere(checkPoint2.position, 0.5f);
            Gizmos.color = prevColor;
        }
    }
}
