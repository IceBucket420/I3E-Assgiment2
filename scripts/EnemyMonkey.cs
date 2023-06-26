using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMonkey : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Health of enemies
    public int EnemyHealth = 3;

    //Player Attack
    public int PlayerAttack = 2;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5;
    public GameObject projectiles;

    //Attacking
    public float timeBetweenAttacks = 1;
    bool alreadyAttacked;

    //States
    public float sightRange = 20, attackRange = 12;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //Check for sight and attack range
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;

        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // attack code here
            Rigidbody rb = Instantiate(projectiles, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 12f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Hurt()
    {
        //enemy gets hurt when this function is called
        EnemyHealth -= PlayerAttack;
        Debug.Log(" Enemy health: " + EnemyHealth);

        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject); // destroy enemy when player kills it
        }

    }

    private void OnDrawnGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }



}
