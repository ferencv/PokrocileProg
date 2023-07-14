using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";
    private const string IS_HIT = "IsHit";
    private const string IS_DEAD = "IsDead";
    private const string IS_VICTORY = "IsVictory";

    [SerializeField]
    private Enemy enemy;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemy != null) 
        {
            animator.SetBool(IS_WALKING, enemy.IsWalking());
            animator.SetBool(IS_ATTACKING, enemy.IsAttacking());
            animator.SetBool(IS_HIT, enemy.IsHit());
            animator.SetBool(IS_DEAD, enemy.IsDead());
            animator.SetBool(IS_VICTORY, enemy.IsVictory());
        }

    }
}
