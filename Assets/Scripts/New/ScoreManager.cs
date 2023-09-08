using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    public int Score { get => score; }
    public Action<int> changeScoreEvent; 
    public void Init()
    {
        score = 0;
    }

    public void AddScore(int value)
    {
        score += value;
        changeScoreEvent?.Invoke(score);
    }
}
