using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private float _timeReload;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private Transform _pointShotPosition;

    private float _ttimeReload;

    private void Start()
    {
        _ttimeReload = 0;
    }

    public GameObject Shot()
    {
        if (_ttimeReload <= 0)
        {
            GameObject shot = Instantiate(_shotPrefab, _pointShotPosition.position, Quaternion.identity, gameObject.transform.parent);
            _ttimeReload = _timeReload;
            return shot;            
        }
        return null;
    }

    private void Update()
    {
        if (_ttimeReload > 0) _ttimeReload -= Time.deltaTime;
    }
}
