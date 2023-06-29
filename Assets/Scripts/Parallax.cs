using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景移动
/// </summary>
public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;
    private float spriteHeight;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);
        if (transform.position.y < startPos.y - spriteHeight)
        {
            transform.position = startPos;
        }
    }
}
