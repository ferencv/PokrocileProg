using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ApeEnemySounds : MonoBehaviour
{
    private Enemy enemy;
    private float footstepTimer = 0;
    private float footstepTimerMax = 0.2f;

    private void Awake() 
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update() 
    {
        if (enemy.IsWalking())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer < 0f)
            {
                footstepTimer = footstepTimerMax;
                SoundManager.Instance.PlayApeEnemyFootstepsSound();
            }
        }
        else 
        {
            footstepTimer = footstepTimerMax;
        }
    }
}
