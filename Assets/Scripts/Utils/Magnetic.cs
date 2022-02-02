using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float dist = .2f;
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > dist)
        {
            speed++;
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, Time.deltaTime * speed);
        }
    }
}
