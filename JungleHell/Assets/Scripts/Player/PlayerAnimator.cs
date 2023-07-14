using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_SHOOTING = "IsShooting";
    private const string IS_HIT = "IsHit";
    private const string IS_DEAD = "IsDead";

    [SerializeField]
    private Player player;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
            animator.SetBool(IS_SHOOTING, player.IsShooting());
            animator.SetBool(IS_HIT, player.IsHit());
            animator.SetBool(IS_DEAD, player.IsDead());
        }
    }
}
