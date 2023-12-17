// Author: Leonard Puskac & Alica Urbanova
using System.Collections;
using UnityEngine;

public class BossRoomScript : MonoBehaviour
{
    [SerializeField] private float winScreenDelay = 2.5f;

    private GameObject winScreen;
    // Start is called before the first frame update
    void Start()
    {
        GameObject hiddenObjects = GameObject.FindGameObjectWithTag("Hidden");
        winScreen = hiddenObjects.transform.Find("WinScreen")?.gameObject;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WinCoroutine());
        }
    }
    
    private IEnumerator WinCoroutine()
    {
        // WAIT FOR OFFSET
        yield return new WaitForSeconds(winScreenDelay);
        ShowWinScreen();
    }

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }
}
