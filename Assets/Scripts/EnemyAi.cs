using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private EnemyAudio enemyAudio;

    [Header("Movement Speeds")]
    [SerializeField] private float walkingSpeed = 4.5f;
    [SerializeField] private float runningSpeed = 5.5f;

    [Header("Detection Ranges")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float doorOpenRange = 1.3f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float minimumDetectionRadiusAngle = -40f;
    [SerializeField] private float maximumDetectionRadiusAngle = 65f;
    [SerializeField] private float ChasingPLayerAfterUnSeeingSeconds = 4f;
    private bool playerSeen = false;
    private bool startSettingPlayerSeen = false;

    [Header("Melee")]
    [SerializeField] private int meleeDamage = 2;
    [SerializeField] private float waitTimeToDamage = 1f; // Do attack but need to wait until animation is in correct timing to do damage and it's effects.
    private float countingAttack; // starts counting when attack is done, and attacks when its = or more than "waitTimeUntilNextPunch" below
    [SerializeField] private float waitTimeUntilNextPunch = 4f;
    private float rotateToPlayerSpeed = 10f;
    private bool hasAttackingSet = false;

    [Header("Patrol")]
    [SerializeField] private Vector3[] patrolPoints;
    private Vector3 patrolPoint;
    private bool patrolPointSet = false;

    [Header("NavMeshAgent")]
    private NavMeshAgent navMeshAgent;

    [Header("Action States")]
    private bool isAttacking;
    private bool isWalking;
    private bool isRunning;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    [System.Obsolete]
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(attackPoint.position, player.position);
        Vector3 targetDirection = transform.position - player.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, -transform.forward);

        float characterHeight = 2.5f;
        // Raycast now won't start from the floor
        Vector3 playerStartPoint = new Vector3(player.position.x, characterHeight, player.position.z);
        Vector3 enemyStartPoint = new Vector3(transform.position.x, characterHeight, transform.position.z);

        Debug.DrawLine(playerStartPoint, enemyStartPoint, Color.yellow);

        countingAttack += Time.deltaTime; // counting seconds
        if (!Physics.Linecast(playerStartPoint, enemyStartPoint, gameObject.layer) && distanceToPlayer <= detectionRange && viewableAngle > minimumDetectionRadiusAngle && viewableAngle < maximumDetectionRadiusAngle)
        {
            enemyAudio.PlayChaseAudios();
            if (distanceToPlayer <= attackRange)
            {
                Vector3 playerDirection = player.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateToPlayerSpeed * Time.deltaTime);
                navMeshAgent.Stop(true);
                isWalking = false;
                isRunning = false;
                playerSeen = true;
                Attack();
            }
            else
            {
                playerSeen = true;
                navMeshAgent.Resume();
                isRunning = true;
                isAttacking = false;
                isWalking = false;
                Chase();
            }
        }
        else if (playerSeen)
        {
            if (distanceToPlayer <= attackRange)
            {
                Vector3 playerDirection = player.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateToPlayerSpeed * Time.deltaTime);
                navMeshAgent.Stop(true);
                isWalking = false;
                isRunning = false;
                playerSeen = true;
                Attack();
            }
            else
            {
                playerSeen = true;
                navMeshAgent.Resume();
                isRunning = true;
                isAttacking = false;
                isWalking = false;
                Chase();
            }
            if (!startSettingPlayerSeen)
            {
                StartCoroutine(PlayerSeenToFalse());
            }
            
        }
        else
        {
            enemyAudio.StopChaseAudios();
            navMeshAgent.Resume();
            isRunning = false;
            isAttacking = false;
            Patrol();
        }
    }

    private void Chase()
    {
        navMeshAgent.speed = runningSpeed;
        navMeshAgent.destination = player.position;
    }

    private void Attack()
    {
        if (countingAttack >= waitTimeUntilNextPunch) // when count is more than waitTimeUntilNextPunch you can punch
        {
            Debug.Log("Attack");
            // start of attack
            isAttacking = true;

            countingAttack = 0; // set countin to 0, and starts counting again for punch availabilty

            Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.TryGetComponent(out Player player))
                {
                    StartCoroutine(DoDamageToPlayer(player));
                }
            }

            // end of attack
        }
        else
        {
            // set it false after half a second
            if (!hasAttackingSet)
            {
                StartCoroutine(SetAttackingFalse());
            }
        }

    }

    IEnumerator PlayerSeenToFalse()
    {
        startSettingPlayerSeen = true;
        yield return new WaitForSeconds(ChasingPLayerAfterUnSeeingSeconds);
        startSettingPlayerSeen = false;
        playerSeen = false;
    }

    IEnumerator SetAttackingFalse()
    {
        hasAttackingSet = true;
        yield return new WaitForSeconds(0.5f);
        hasAttackingSet = false;
        isAttacking = false;
    }

    private IEnumerator DoDamageToPlayer(Player player)
    {
        yield return new WaitForSeconds(waitTimeToDamage); // Wait for animation to be in correct point to do damage
        Debug.Log("Damage done to player");
        player.TakeDamage(meleeDamage, gameObject.tag);
    }

    private void Patrol()
    {
        navMeshAgent.speed = walkingSpeed;
        isWalking = true;
        // If Not Looking Around, Start Walking
        if (!patrolPointSet)
        {
            patrolPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
            patrolPointSet = true;
        }
        else
        {
            navMeshAgent.destination = patrolPoint;
        }

        Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

        if (distanceToPatrolPoint.magnitude < 3f)
        {
            patrolPointSet = false;
        }
    }

    public bool IsWalking() { return isWalking; }

    public bool IsAttacking() { return isAttacking; }

    public bool IsRunning() { return isRunning;}


    private void OnDrawGizmos()
    {
        // Draw Player Detection Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw Attack Range, When Player Is Inside This Range, Enemy Attacks
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, doorOpenRange);

        // Draw Patrol Points
        Gizmos.color = Color.red;
        foreach(Vector3 point in patrolPoints)
        {
            Gizmos.DrawSphere(point, 1f);
        }
    }

}