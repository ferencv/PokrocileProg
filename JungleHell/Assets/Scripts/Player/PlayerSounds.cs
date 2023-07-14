using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private static float footstepTimerMax = 0.38f;
    private float footstepTimer = footstepTimerMax;

    // pøi zapoèetí chuze se spusti dva skripty, ktere spousteji zvuk chuze. Prvni prehraje cely zvuk jednou pro pripad, ze jen klepneme do klavesnice. 
    //Druhy je tento a ten prehrava zvuk kontinualne dokud je chuze aktivni. Aby nebyl prvni "krok" prehran dvakrat a tedy hlasiteji, zde prvni krok neprehravam 

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    private void Update() 
    {
        if (player.IsWalking())
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer < 0f)
            {
                footstepTimer = footstepTimerMax;
                SoundManager.Instance.PlayPlayerFootstepsSound();
            }
        }
        else 
        {
            footstepTimer = footstepTimerMax;
        }
    }
}
