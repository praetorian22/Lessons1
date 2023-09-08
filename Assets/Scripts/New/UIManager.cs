using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text health;
    [SerializeField] private Text score;

    [SerializeField] private Text gameOver;

    private void Start()
    {
        StartGame();        
    }

    public void EndGame()
    {
        gameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        gameOver.gameObject.SetActive(false);
    }

    public void ChangeHealth(int value)
    {
        health.text = value.ToString();
    }

    public void ChangeScore(int value)
    {
        score.text = value.ToString();
    }
}
