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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(isWalking, enemyAi.IsWalking());
        animator.SetBool(isRunning, enemyAi.IsRunning());
        animator.SetBool(isAttacking, enemyAi.IsAttacking());
        animator.SetBool(isLookingAround, enemyAi.IsLookingAround());
    }
}
