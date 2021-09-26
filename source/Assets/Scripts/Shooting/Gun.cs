using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private int _bulletsPerSecond;
    [SerializeField] private Bullet[] _bulletPrefabs;

    private int _updatesPerBullet;
    private int _counter;

    private void Start()
    {
        var updatesPerSecond = (int) (1 / Time.fixedDeltaTime);
        _updatesPerBullet = updatesPerSecond / _bulletsPerSecond;
    }

    private void FixedUpdate()
    {
        if(CheckForShot()) Shoot();
    }

    private bool CheckForShot()
    {
        ++_counter;
        if (_counter != _updatesPerBullet) return false;
        _counter = 0;
        return true;
    }

    protected virtual Bullet ChooseBullet()
    {
        return _bulletPrefabs[Random.Range(0, _bulletPrefabs.Length)];
    }

    public void Shoot()
    {
        Bullet bulletToShootPrefab = ChooseBullet();
        Bullet shotBullet = Instantiate(bulletToShootPrefab, _bulletStartPosition.position, _bulletStartPosition.rotation);
        StartCoroutine(shotBullet.OnShot());
    }
}
