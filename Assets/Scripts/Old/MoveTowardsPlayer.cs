﻿using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour {

	// Переменная для координат объекта player
	private Transform player;

	// Скорость движения врага
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("playerShip").transform;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 delta = player.position - transform.position;
		delta.Normalize();
		float moveSpeed = speed * Time.deltaTime;
		transform.position = transform.position + (delta * moveSpeed);
	}
}
