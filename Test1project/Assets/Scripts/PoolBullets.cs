using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBullets : MonoBehaviour
{
    [SerializeField] private GameObject _bulletprefab;
    [SerializeField] private int _minCapacity;

    private List<GameObject> _bullets = new List<GameObject>();

    private void Start()
    {
        PoolCreation();
    }

    private void PoolCreation()
    {
        for (int i = 0; i < _minCapacity; i++)
        {
            CreateBullet();
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletprefab);
        bullet.SetActive(false);
        _bullets.Add(bullet);
    }

    public GameObject GetFreeBullet()
    {
        foreach (var bullet in _bullets)
        {
            if (bullet.activeSelf == false)
            {
                return bullet;
            }
        }

        CreateBullet();
        GameObject newbullet = _bullets[_bullets.Count - 1];
        return newbullet;

    }
}
