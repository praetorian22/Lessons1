using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShot : MonoBehaviour
{    
    [SerializeField] private float _speed;
    private Rigidbody2D _RBShot;

    void Start()
    {
        _RBShot = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_RBShot != null) _RBShot.velocity = transform.up * _speed;
    }
}
