// Author: Leonard Puškáč

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformsScript : MonoBehaviour
{
    [SerializeField] private float reenableColliderTime = 0.5f;
    [SerializeField] private TilemapCollider2D tilemapCollider2D;
    private bool playerOnPlatform = false;


    private void Update()
    {
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0f)
        {
            tilemapCollider2D.enabled = false;
            StartCoroutine(EnableCollider());

        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(reenableColliderTime);
        tilemapCollider2D.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
