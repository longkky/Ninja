using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Pattern
    [SerializeField] Text coinText;
    [SerializeField] private int totalScore; // Biến lưu tổng điểm

    private void Awake()
    {
        // Đảm bảo chỉ có một GameManager tồn tại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        totalScore = PlayerPrefs.GetInt("coin", 0);
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        totalScore += value; // Thêm điểm
        PlayerPrefs.SetInt("coin", totalScore);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        coinText.text = totalScore.ToString();
    }

    public int GetScore()
    {
        return totalScore; // Lấy tổng điểm hiện tại
    }
}
