using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;

    private Vector3 _target;
    private void Start()
    {
        transform.position = aPoint.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            _target = bPoint.position;
        }
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            _target = aPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision == null) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}