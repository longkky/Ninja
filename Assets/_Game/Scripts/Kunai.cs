using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject hitVFX;
    private float _speed = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitVFX = Resources.Load<GameObject>("Prefabs/Hit_VFX");
    }

    void Start()
    {
        OnHit();
    }

    public void OnHit()
    {
        rb.velocity = transform.right * _speed;
        Invoke(nameof(OnDespawn), 4f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}