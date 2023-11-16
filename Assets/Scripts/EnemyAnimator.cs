using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAi;
    private Animator animator;

    private string isWalking = "IsWalking";
    private string isRunning = "IsRunning";
    private string isAttacking = "IsAttacking";
    private string isLookingAround = "IsLookingAround";
    private string isJumpScare = "IsJumpScare";
    bool isPlayerDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Player.OnJumpScare += Player_OnJumpScare;
    }

    private void Player_OnJumpScare(object sender, Player.JumpScareEventArgs e)
    {
        isPlayerDead = true;
        Debug.Log("Dead, Jump Scare!");
        animator.SetBool(isJumpScare, true);
    }

    private void Update()
    {
        if (!isPlayerDead)
        {
            animator.SetBool(isWalking, enemyAi.IsWalking());
            animator.SetBool(isRunning, enemyAi.IsRunning());
            animator.SetBool(isAttacking, enemyAi.IsAttacking());
            animator.SetBool(isLookingAround, enemyAi.IsLookingAround());
        }
    }
}
