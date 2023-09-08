using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;    
    [SerializeField] private int damage;
    [SerializeField] private bool isEnemyShot;

    public bool IsEnemyShot { get => isEnemyShot; }
    public int Damage { get => damage; }

    void Start()
    {        
        // ”ничтожение по окончанию таймера
        Destroy(gameObject, lifeTime);
    }
   
}
