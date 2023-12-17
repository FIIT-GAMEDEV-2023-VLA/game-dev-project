// Author: Viktor Szabo
using System.Collections;
using UnityEngine;

public class TrapDynamicSpikesScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private Transform dynamicSpikes;
    [SerializeField] private EdgeCollider2D edgeCollider;

    [SerializeField] private Transform checkPoint1;
    [SerializeField] private Transform checkPoint2;

    [SerializeField] private float dynamicSpikesMoveSpeed = 5f;
    [SerializeField] private bool isPaused = false;
    private Transform currentCheckPoint;
    void Start()
    {
        currentCheckPoint = checkPoint2;
        edgeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused){
            dynamicSpikes.position = Vector3.MoveTowards(
                current: dynamicSpikes.position, 
                target: currentCheckPoint.position, 
                maxDistanceDelta: dynamicSpikesMoveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(dynamicSpikes.position, currentCheckPoint.position) < 0.01f)
            {
                isPaused = true;
                StartCoroutine(pauseMovement());
            }
        }
    }

    private IEnumerator pauseMovement(){
        if (currentCheckPoint == checkPoint1){
            edgeCollider.enabled = true;
        }
        yield return new WaitForSeconds(3f);
        SwapCheckpoints();
        dynamicSpikes.position = Vector3.MoveTowards(
                current: dynamicSpikes.position, 
                target: currentCheckPoint.position, 
                maxDistanceDelta: dynamicSpikesMoveSpeed * Time.deltaTime
            );
    }

    private void SwapCheckpoints()
    {
        isPaused = false;
        edgeCollider.enabled = false;
        currentCheckPoint = currentCheckPoint == checkPoint1 ? checkPoint2 : checkPoint1;
    }
    
    void OnDrawGizmos()
    {
        if (checkPoint1 && checkPoint2)
        {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(checkPoint1.position, checkPoint2.position);
            Gizmos.DrawSphere(checkPoint1.position, 0.2f);
            Gizmos.DrawSphere(checkPoint2.position, 0.2f);
            Gizmos.color = prevColor;
        }
    }
}
