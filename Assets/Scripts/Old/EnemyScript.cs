using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Сколько раз нужно попасть во врага, чтобы уничтожить его
	public int health = 2;

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		//Проверяем коллизию с объектом типа «лазер»
		if(theCollision.gameObject.name.Contains("laser"))
		{
			LaserScript laser = theCollision.gameObject.GetComponent("LaserScript") as LaserScript;
			health -= laser.damage;
			Destroy (theCollision.gameObject);
		}
		if (health <= 0)
		{
			Destroy (this.gameObject);
			GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController") as GameController;
			controller.KilledEnemy();

		}
	}
}
