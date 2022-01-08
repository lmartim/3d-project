using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaCheck : MonoBehaviour
{
    public string tagToCheck = "Player";

    public GameObject bossCamera;

    public Color gizmosColor = Color.yellow;

    private void Awake()
    {
        bossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToCheck)
        {
            TurnOnCamera();
        }
    }

    private void TurnOnCamera()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
