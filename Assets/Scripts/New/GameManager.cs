using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// �������� ���������� �����
	[SerializeField] private GameObject _enemy;
	[SerializeField] private UIManager _uiManager;
	[SerializeField] private ScoreManager _scoreManager;
	[SerializeField] private GameObject _player;

	// ��������� ���������� ����� ���������, ���-�� ������
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

	// ��������� ���� ������
	IEnumerator SpawnEnemies()
	{
		// ��������� �������� ����� ������ ���������� ������
		yield return new WaitForSeconds(_timeBeforeSpawning);
		// ����� ������ ������, �������� ����������� ��� ��������
		while (true)
		{
			// �� ��������� ����� ������, ���� �� ���������� ������
			if (_currentNumberOfEnemies <= 0)
			{
				float randDirection;
				float randDistance;
				// ������� 10 ������ � ��������� ������ �� �������
				for (int i = 0; i < _enemiesPerWave; i++)
				{
					// ����� ��������� ���������� ��� ���������� � �����������
					randDistance = Random.Range(10, 25);
					randDirection = Random.Range(0, 360);
					// ���������� ���������� ��� ������� ��������� ��������� �����
					float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this.transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					// ������ ����� �� �������� �����������
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
			// �������� �� ��������� ��������
			yield return new WaitForSeconds(_timeBeforeWaves);
		}
	}

	// ��������� ���������� ���������� ������ � ����������
	public void KilledEnemy(GameObject enemy)
	{
		_currentNumberOfEnemies--;
		_allEnemy.Remove(enemy);
		if (_scoreManager != null) _scoreManager.AddScore(enemy.GetComponent<HealthScript>().Points);
		enemy.GetComponent<HealthScript>().deadEvent -= KilledEnemy;
		Destroy(enemy);
	}
}
