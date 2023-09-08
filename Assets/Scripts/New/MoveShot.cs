using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShot : MonoBehaviour
{    
    [SerializeField] private float speed;
    private Rigidbody2D RBShot;

    void Start()
    {
        RBShot = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RBShot.velocity = transform.up * speed;
    }
}
