using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private bool _isEnemy;
    [SerializeField] private int _points;

    public int Health { get => _health; }
    public bool IsEnemy { get => _isEnemy; }
    public int Points { get => _points; }

    public Action<int> changeHealthEvent;
    public Action<GameObject> deadEvent;

    private void Start()
    {
        changeHealthEvent?.Invoke(_health);
    }

    public void Damage(int value)
    {
        _health -= value;        
        if (_health <= 0)
        {
            _health = 0;            
            deadEvent?.Invoke(gameObject);
        }
        changeHealthEvent?.Invoke(_health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�������� ��� � ��� ����������
        ShotScript shotscript = collision.gameObject.GetComponent<ShotScript>();
        HealthScript healthScript = collision.gameObject.GetComponent<HealthScript>();
        //���� ��� ������, �� ������� ����������� ������� � ���������� ������
        if (shotscript != null && shotscript.IsEnemyShot != _isEnemy)
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
