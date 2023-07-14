using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private bool isWalking;
    private bool isAttaking;
    private bool isHit;
    private bool isDead;
    private bool isVictory;
    private float moveSpeed = 6.5f; // 5

    //[SerializeField]
    private float maxPlayerDistance = 16f;// 15f;
    //[SerializeField]
    private float minPlayerDistanceAttack = 3.0f;
    private float minPlayerDistanceKill = 2.2f;
    private float attackSoundTimer = 1f;
    private float maxAttackSoundTimer = 1f;

    private void Start() 
    {
        Player.Instance.OnShoot += HandlePlayerShooting;
        Player.Instance.OnDie += HandlePlayerDying;
        Player.Instance.OnLevelFinishReached += PlayerCompletedLevel;
    }

    private void PlayerCompletedLevel(object sender, EventArgs e)
    {
        isHit = true;
        Destroy(gameObject, 1f);
    }


    // Update is called once per frame
    private void Update()
    {
        if (isHit || isVictory)
            return;
        var player = Player.Instance;
        var playerPosition = player.transform.position;
        var magnitude = (transform.position - playerPosition).magnitude;
        //Debug.Log(magnitude);
        //if (isAttaking) 
        //{
        //    //is = true;
        //    isVictory = true;
        //    isAttaking = false;
        //    return;
        //}
        if (!isWalking && !isAttaking) 
        {
            if (magnitude < maxPlayerDistance) 
            {
                isWalking = true;
            }
        }
        if (magnitude < minPlayerDistanceKill)
        {
            isWalking = false;
            if (!player.IsDead())
            {
                isAttaking = false;
                isVictory = true;
                //SoundManager.Instance.PlayApeEnemyAttackSound();
                player.SetIsHit();
                return;
            }
        }
        //isAttaking = false;
        if (!isAttaking && magnitude < minPlayerDistanceAttack)
        {
            //isWalking = false;
            if (!player.IsDead())
            {
                isAttaking = true;
                SoundManager.Instance.PlayApeEnemyAttackSound();
                //player.SetIsHit();
            }
        }
        else 
        {
            if (isAttaking)
            {
                if (magnitude > minPlayerDistanceAttack)
                {
                    isAttaking = false;
                    attackSoundTimer = maxAttackSoundTimer;
                }
                else
                {
                    if (attackSoundTimer <= 0)
                    {
                        attackSoundTimer = maxAttackSoundTimer;
                        SoundManager.Instance.PlayApeEnemyAttackSound();
                    }
                    attackSoundTimer -= Time.deltaTime;
                }
            }
        }
        
        if (isWalking)
        {
            var moveDir = -(transform.position - playerPosition).normalized;
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    private bool HandleAttack()
    {
        return false;
    }

    private void HandlePlayerShooting(object sender, Player.OnShootEventArgs e) 
    {
        if (e.hasAmmo && Utils.IsHit(transform.position, e.playerPosition, e.shootPosition, e.gunRange, e.gunSpread))
        {
            Kill();
        }
    }
    private void HandlePlayerDying(object sender, EventArgs e)
    {
        if (!isHit) 
        {
            isAttaking = false;
            isWalking = false;
            isVictory = true;
        }
    }

    public void Kill() 
    {
        SoundManager.Instance.PlayApeEnemyHitSound();
        isWalking = false;
        isAttaking = false;
        isHit = true;
        Destroy(gameObject, 1f);
    }

    private void OnDestroy()
    {
        Player.Instance.OnShoot -= HandlePlayerShooting;
        Player.Instance.OnDie -= HandlePlayerDying;
        Player.Instance.OnLevelFinishReached -= PlayerCompletedLevel;
    }

    public bool IsWalking() 
    {
        return isWalking;
    }
    public bool IsAttacking()
    {
        return isAttaking;
    }
    public bool IsHit()
    {
        return isHit;
    }
    public bool IsDead()
    {
        return isDead;
    }
    public bool IsVictory()
    {
        return isVictory;
    }
}
