using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    enter,
    fire,
    special,
    death
}

public class BossController : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void ChangeState(BossState state)
    {
        switch (state)
        {
            case BossState.enter:
                Debug.Log("enter");
                break;
            case BossState.fire:
                Debug.Log("fire");
                break;
            case BossState.special:
                Debug.Log("special");
                break;
            case BossState.death:
                Debug.Log("death");
                break;
        }
    }
}