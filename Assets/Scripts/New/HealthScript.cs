using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool isEnemy;
    [SerializeField] private int points;

    public int Health { get => health; }
    public bool IsEnemy { get => isEnemy; }
    public int Points { get => points; }

    public Action<int> changeHealthEvent;
    public Action<GameObject> deadEvent;

    private void Start()
    {
        changeHealthEvent?.Invoke(health);
    }

    public void Damage(int value)
    {
        health -= value;        
        if (health <= 0)
        {
            health = 0;            
            deadEvent?.Invoke(gameObject);
        }
        changeHealthEvent?.Invoke(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�������� ��� � ��� ����������
        ShotScript shotscript = collision.gameObject.GetComponent<ShotScript>();
        HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
        //���� ��� ������, �� ������� ����������� ������� � ���������� ������
        if (shotscript != null && shotscript.IsEnemyShot != isEnemy)
        {
            Damage(shotscript.Damage);
            Destroy(collision.gameObject);
        }
        //���� ��� �������, ����� ������� �� ����� ������� �����������
        if (healthScript != null && healthScript.IsEnemy != IsEnemy)
        {
            Damage(1);
            healthScript.Damage(1);
        }
    }
}
