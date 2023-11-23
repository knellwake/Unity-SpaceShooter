using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private LaserBullet laserBullet;
    [SerializeField] private float shootingInterval; //射击间隔

    [Header("Basic Attack")] [SerializeField]
    private Transform basicShootPoint;

    [Header("Upgrade Points")] [SerializeField]
    private Transform leftCanon;

    [SerializeField] private Transform rightCanon;
    [SerializeField] private Transform secondLeftCanon;
    [SerializeField] private Transform secondRightCanon;

    [Header("Upgrade Rotation Points")] [SerializeField]
    private Transform leftRotationCanon;

    [SerializeField] private Transform rightRotationCanon;

    [SerializeField] private AudioSource source;

    private int upgradeLevel = 0;

    private ObjectPool<LaserBullet> pool;

    private float intervalReset; //重置

    private void Awake()
    {
        pool = new ObjectPool<LaserBullet>(CreatePoolObj, OnTakeBulletFromPool, OnReturnBulletFromPool,
            OnDestroyPoolObj, true, 10, 30);
    }

    void Start()
    {
        intervalReset = shootingInterval;
    }

    private void OnDestroyPoolObj(LaserBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private LaserBullet CreatePoolObj()
    {
        LaserBullet bullet = Instantiate(laserBullet, transform.position, transform.rotation);
        bullet.SetPool(pool);
        return bullet;
    }

    private void OnTakeBulletFromPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletFromPool(LaserBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void Update()
    {
        shootingInterval -= Time.deltaTime;
        if (shootingInterval < 0)
        {
            InstantiateShoot();
            shootingInterval = intervalReset;
        }
    }

    public void IncreaseUpgrade(int increaseAmount)
    {
        upgradeLevel += increaseAmount;
        if (upgradeLevel > 4)
            upgradeLevel = 4;
    }

    public void DecreaseUpgrade()
    {
        upgradeLevel -= 1;
        if (upgradeLevel < 0)
            upgradeLevel = 0;
    }

    private void InstantiateShoot()
    {
        source.Play();
        switch (upgradeLevel)
        {
            case 0:
                // Instantiate(laserBullet, basicShootPoint.position, quaternion.identity);
                pool.Get().transform.position = basicShootPoint.position;
                break;
            case 1:
                // Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                pool.Get().transform.position = leftCanon.position;
                pool.Get().transform.position = rightCanon.position;
                break;
            case 2:
                // Instantiate(laserBullet, basicShootPoint.position, Quaternion.identity);
                // Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                pool.Get().transform.position = basicShootPoint.position;
                pool.Get().transform.position = leftCanon.position;
                pool.Get().transform.position = rightCanon.position;
                break;
            case 3:
                // Instantiate(laserBullet, basicShootPoint.position, Quaternion.identity);
                // Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, secondLeftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, secondRightCanon.position, Quaternion.identity);
                pool.Get().transform.position = basicShootPoint.position;
                pool.Get().transform.position = leftCanon.position;
                pool.Get().transform.position = rightCanon.position;
                pool.Get().transform.position = secondLeftCanon.position;
                pool.Get().transform.position = secondRightCanon.position;
                break;
            case 4:
                // Instantiate(laserBullet, basicShootPoint.position, Quaternion.identity);
                // Instantiate(laserBullet, leftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, rightCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, secondLeftCanon.position, Quaternion.identity);
                // Instantiate(laserBullet, secondRightCanon.position, Quaternion.identity);
                pool.Get().transform.position = basicShootPoint.position;
                pool.Get().transform.position = leftCanon.position;
                pool.Get().transform.position = rightCanon.position;
                pool.Get().transform.position = secondLeftCanon.position;
                pool.Get().transform.position = secondRightCanon.position;

                // Instantiate(laserBullet, leftRotationCanon.position, leftRotationCanon.rotation);
                // Instantiate(laserBullet, rightRotationCanon.position, rightRotationCanon.rotation);
                LaserBullet bulletOne = pool.Get();
                bulletOne.transform.position = leftRotationCanon.position;
                bulletOne.transform.rotation = leftRotationCanon.rotation;
                bulletOne.SetDirectionAndSpeed();
                LaserBullet bulletTwo = pool.Get();
                bulletTwo.transform.position = rightRotationCanon.position;
                bulletTwo.transform.rotation = rightRotationCanon.rotation;
                bulletTwo.SetDirectionAndSpeed();
                break;
            default:
                break;
        }
        // Instantiate(laserBullet, basicShootPoint.position, quaternion.identity);
    }
}