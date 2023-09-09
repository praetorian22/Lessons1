using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField] private float _lifeTime;    
    [SerializeField] private int _damage;
    [SerializeField] private bool _isEnemyShot;

    public bool IsEnemyShot { get => _isEnemyShot; }
    public int Damage { get => _damage; }

    void Start()
    {        
        // ”ничтожение по окончанию таймера
        Destroy(gameObject, _lifeTime);
    }
   
}
