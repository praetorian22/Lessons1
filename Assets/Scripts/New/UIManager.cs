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
        if (_gameOver != null) _gameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        if (_gameOver != null) _gameOver.gameObject.SetActive(false);
    }

    public void ChangeHealth(int value)
    {
        if (_health != null) _health.text = value.ToString();
    }

    public void ChangeScore(int value)
    {
        if (_health != null) _score.text = value.ToString();
    }
}
