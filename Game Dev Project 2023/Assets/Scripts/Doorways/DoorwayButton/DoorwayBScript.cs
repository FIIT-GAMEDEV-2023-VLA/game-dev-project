// Author: Viktor Szabo
using System.Collections;
using UnityEngine;

public class DoorwayBScript : MonoBehaviour
{
    [SerializeField] private Transform doorway;
    [SerializeField] private Transform checkPointOpened;
    [SerializeField] private Transform checkPointClosed;    

    private BoxCollider2D boxCollider;
    private EdgeCollider2D bottomCollider;
    private Transform currentCheckPoint;

    private float moveSpeed = 2f;
    private bool isClosed = true;
    private bool isPaused = true;

    void Start()
    {
        currentCheckPoint = checkPointClosed;
        boxCollider = doorway.GetComponent<BoxCollider2D>();
        bottomCollider = doorway.GetComponent<EdgeCollider2D>();
        boxCollider.enabled = true;
    }
    

    void Update()
    {
        if (!isClosed && !isPaused){
            doorway.position = Vector3.MoveTowards(
                current: doorway.position, 
                target: currentCheckPoint.position, 
                maxDistanceDelta: moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(doorway.position, checkPointClosed.position) < 0.01f)
            {
                isPaused = true;
                isClosed = true;
                boxCollider.enabled = true;
                bottomCollider.enabled = false;
                doorway.GetComponent<DoorwayBottomCollisionScript>().SetClosingFalse();
                gameObject.GetComponentInChildren<DoorwayButtonScript>().Reset();
            }

            if (Vector3.Distance(doorway.position, checkPointOpened.position) < 0.01f)
            {
                isPaused = true;
                StartCoroutine(waitThenClose());
            }
        }
    }

    private IEnumerator waitThenClose()
    {
        yield return new WaitForSeconds(5f);
        currentCheckPoint = checkPointClosed;
        isPaused = false;
        bottomCollider.enabled = true;
        doorway.GetComponent<DoorwayBottomCollisionScript>().SetClosingTrue();
        doorway.position = Vector3.MoveTowards(
            current: doorway.position, 
            target: currentCheckPoint.position, 
            maxDistanceDelta: moveSpeed * Time.deltaTime
        );
    }

    public void Open(){
        isPaused = false;
        isClosed = false;
        boxCollider.enabled = false;
        currentCheckPoint = checkPointOpened;
    }

    void OnDrawGizmos()
    {
        if (checkPointOpened && checkPointClosed)
        {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(checkPointOpened.position, checkPointClosed.position);
            Gizmos.DrawSphere(checkPointOpened.position, 0.2f);
            Gizmos.DrawSphere(checkPointClosed.position, 0.2f);
            Gizmos.color = prevColor;
        }
    }
}