using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10; // Giá trị điểm của Coin

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu Player va chạm
        {
            Collect();
        }
    }
    private void Collect()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
    
}
