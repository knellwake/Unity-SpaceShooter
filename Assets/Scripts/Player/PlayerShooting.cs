using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject laserBullet;
    [SerializeField] private Transform basicShootPoint;
    [SerializeField] private float shootingInterval;//射击间隔
    private float intervalReset;//重置
    
    void Start()
    {
        intervalReset = shootingInterval;
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

    private void InstantiateShoot()
    {
        Instantiate(laserBullet, basicShootPoint.position, quaternion.identity);
    }
}
