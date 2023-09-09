using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _health;
    [SerializeField] private Text _score;

    [SerializeField] private Text _gameOver;

    private void Start()
    {
        StartGame();        
    }

    public void EndGame()
    {
        _gameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        _gameOver.gameObject.SetActive(false);
    }

    public void ChangeHealth(int value)
    {
        _health.text = value.ToString();
    }

    public void ChangeScore(int value)
    {
        _score.text = value.ToString();
    }
}
