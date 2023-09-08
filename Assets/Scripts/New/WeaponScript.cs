using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private float timeReload;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform pointShotPosition;

    private float ttimeReload;

    private void Start()
    {
        ttimeReload = 0;
    }

    public GameObject Shot()
    {
        if (ttimeReload <= 0)
        {
            GameObject shot = Instantiate(shotPrefab, pointShotPosition.position, Quaternion.identity, gameObject.transform.parent);
            ttimeReload = timeReload;
            return shot;            
        }
        return null;
    }

    private void Update()
    {
        if (ttimeReload > 0) ttimeReload -= Time.deltaTime;
    }
}
