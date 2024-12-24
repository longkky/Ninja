using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : Character
{
    [SerializeField] private bool isDeath = false;

    private Vector3 savePoint;
    
    public override void OnInit()
    {
        base.OnInit();
        transform.position = savePoint;
        SavePoint();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        if (isDeath) return;
        Debug.Log("Player Die");
        isDeath = true;

        animator.SetTrigger("die");
        StartCoroutine(HandleDeath());
    }

    public bool IsDead()
    {
        return isDeath;
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1f); //Tạm chờ trong thời gian animation chạy
        transform.position = savePoint;
        isDeath = false;
    }

    public void SavePoint()
    {
        savePoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeath) return;
        if (collision.CompareTag("Deadly"))
        {
            OnDeath();
        }
    }
}