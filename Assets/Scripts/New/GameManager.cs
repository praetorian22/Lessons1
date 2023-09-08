using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// �������� ���������� �����
	[SerializeField] private GameObject enemy;
	[SerializeField] private UIManager uiManager;
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private GameObject player;

	// ��������� ���������� ����� ���������, ���-�� ������
	[SerializeField] private float timeBeforeSpawning = 1.5f;
	[SerializeField] private float timeBetweenEnemies = 0.25f;
	[SerializeField] private float timeBeforeWaves = 2.0f;
	[SerializeField] private int enemiesPerWave = 10;

	private int currentNumberOfEnemies = 0;
	private List<GameObject> allEnemy = new List<GameObject>();

	void Start()
	{
		player.GetComponent<HealthScript>().changeHealthEvent += uiManager.ChangeHealth;
		player.GetComponent<HealthScript>().deadEvent += GameOver;
		scoreManager.Init();
		scoreManager.changeScoreEvent += uiManager.ChangeScore;

		StartCoroutine(SpawnEnemies());
	}

	public void GameOver(GameObject player)
	{
		player.SetActive(false);
		uiManager.EndGame();
		Time.timeScale = 0;
	}

	// ��������� ���� ������
	IEnumerator SpawnEnemies()
	{
		// ��������� �������� ����� ������ ���������� ������
		yield return new WaitForSeconds(timeBeforeSpawning);
		// ����� ������ ������, �������� ����������� ��� ��������
		while (true)
		{
			// �� ��������� ����� ������, ���� �� ���������� ������
			if (currentNumberOfEnemies <= 0)
			{
				float randDirection;
				float randDistance;
				// ������� 10 ������ � ��������� ������ �� �������
				for (int i = 0; i < enemiesPerWave; i++)
				{
					// ����� ��������� ���������� ��� ���������� � �����������
					randDistance = Random.Range(10, 25);
					randDirection = Random.Range(0, 360);
					// ���������� ���������� ��� ������� ��������� ��������� �����
					float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this.transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					// ������ ����� �� �������� �����������
					GameObject enemyGO = Instantiate(enemy, new Vector3(posX, posY, 0), this.transform.rotation);
					allEnemy.Add(enemyGO);
					enemyGO.GetComponent<HealthScript>().deadEvent += KilledEnemy;					 
					currentNumberOfEnemies++;
					yield return new WaitForSeconds(timeBetweenEnemies);
				}
			}
			// �������� �� ��������� ��������
			yield return new WaitForSeconds(timeBeforeWaves);
		}
	}

	// ��������� ���������� ���������� ������ � ����������
	public void KilledEnemy(GameObject enemy)
	{
		currentNumberOfEnemies--;
		allEnemy.Remove(enemy);
		scoreManager.AddScore(enemy.GetComponent<HealthScript>().Points);
		enemy.GetComponent<HealthScript>().deadEvent -= KilledEnemy;
		Destroy(enemy);
	}
}
