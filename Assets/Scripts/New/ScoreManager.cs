using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;

    public int Score { get => _score; }
    public Action<int> changeScoreEvent; 
    public void Init()
    {
        _score = 0;
    }

    public void AddScore(int value)
    {
        _score += value;
        changeScoreEvent?.Invoke(_score);
    }
}
