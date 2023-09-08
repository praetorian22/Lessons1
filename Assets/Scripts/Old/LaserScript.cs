using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	// Как долго существует лазер
	public float lifetime = 2.0f;
	
	// Как быстро движется лазер
	public float speed = 5.0f;
	
	// Как много наносит урона лазер при соприкосновении с врагами
	public int damage = 1;

	// Use this for initialization
	void Start () {
		// Уничтожение лазера по окончанию таймера
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed);	
	}
}
