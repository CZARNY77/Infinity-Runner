using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    float leftEnge;
    void Start()
    {
        leftEnge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * GameManager.instance.speedWorld * Time.deltaTime;
        if(transform.position.x < leftEnge) Destroy(gameObject);
    }
}
