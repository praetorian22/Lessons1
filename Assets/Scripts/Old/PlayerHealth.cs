using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMesh healthLabel;
    [SerializeField] private GameObject enemy;
    public int health=5;

    void Start()
    {
        healthLabel.text = "Heath: " + health;
    }

     void OnCollisionEnter2D(Collision2D theCollision)
    {
        //Проверяем коллизию с объектом типа enemy
        if (theCollision.gameObject.name.Contains("enemy"))
        {
            health --;
            healthLabel.text = "Heath: " + health;       
        }
        if (health<=0)
        {
            Destroy(this.gameObject);
        }

    }




}
