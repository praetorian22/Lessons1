using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Создание переменной «враг»
	[SerializeField] private GameObject _enemy;
	[SerializeField] private UIManager _uiManager;
	[SerializeField] private ScoreManager _scoreManager;
	[SerializeField] private GameObject _player;

	// Временные промежутки между событиями, кол-во врагов
	[SerializeField] private float _timeBeforeSpawning = 1.5f;
	[SerializeField] private float _timeBetweenEnemies = 0.25f;
	[SerializeField] private float _timeBeforeWaves = 2.0f;
	[SerializeField] private int _enemiesPerWave = 10;

	private int _currentNumberOfEnemies = 0;
	private List<GameObject> _allEnemy = new List<GameObject>();

	void Start()
	{
		if (_player != null)
        {
			_player.GetComponent<HealthScript>().changeHealthEvent += _uiManager.ChangeHealth;
			_player.GetComponent<HealthScript>().deadEvent += GameOver;
		}
		if (_scoreManager != null)
        {
			_scoreManager.Init();
			_scoreManager.changeScoreEvent += _uiManager.ChangeScore;
        }
        StartCoroutine(SpawnEnemies());
	}

	public void GameOver(GameObject player)
	{
		player.SetActive(false);
		_uiManager.EndGame();
		Time.timeScale = 0;
	}

	// Появление волн врагов
	IEnumerator SpawnEnemies()
	{
		// Начальная задержка перед первым появлением врагов
		yield return new WaitForSeconds(_timeBeforeSpawning);
		// Когда таймер истекёт, начинаем производить эти действия
		while (true)
		{
			// Не создавать новых врагов, пока не уничтожены старые
			if (_currentNumberOfEnemies <= 0)
			{
				float randDirection;
				float randDistance;
				// Создать 10 врагов в случайных местах за экраном
				for (int i = 0; i < _enemiesPerWave; i++)
				{
					// Задаём случайные переменные для расстояния и направления
					randDistance = Random.Range(10, 25);
					randDirection = Random.Range(0, 360);
					// Используем переменные для задания координат появления врага
					float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this.transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					// Создаём врага на заданных координатах
					if (_enemy != null)
                    {
						GameObject enemyGO = Instantiate(_enemy, new Vector3(posX, posY, 0), this.transform.rotation);
						_allEnemy.Add(enemyGO);
						enemyGO.GetComponent<HealthScript>().deadEvent += KilledEnemy;
						_currentNumberOfEnemies++;
					}					
					yield return new WaitForSeconds(_timeBetweenEnemies);
				}
			}
			// Ожидание до следующей проверки
			yield return new WaitForSeconds(_timeBeforeWaves);
		}
	}

	// Процедура уменьшения количества врагов в переменной
	public void KilledEnemy(GameObject enemy)
	{
		_currentNumberOfEnemies--;
		_allEnemy.Remove(enemy);
		if (_scoreManager != null) _scoreManager.AddScore(enemy.GetComponent<HealthScript>().Points);
		enemy.GetComponent<HealthScript>().deadEvent -= KilledEnemy;
		Destroy(enemy);
	}
}
