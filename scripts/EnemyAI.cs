/*
 * Author: Pang Le Xin (with reference of dave/ GameDevelopment on YT
 * Date: 28/06/2023
 * Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Navmesh agent 
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// Transform of player
    /// </summary>
    public Transform player;

    /// <summary>
    /// Layer mask of ground and player
    /// </summary>
    public LayerMask whatIsGround, whatIsPlayer;

    //Health of enemies

    /// <summary>
    /// Maximum of the enemy 
    /// </summary>
    public int MaxEnemyHealth = 5;
    /// <summary>
    /// the current health of the enemy
    /// </summary>
    public int CurrentEnemyHealth = 5;
    /// <summary>
    /// reference the healthbar script
    /// </summary>
    public HealthBar healthBar;

    //Player Attack
    /// <summary>
    /// interger of the player attack dmg
    /// </summary>
    public int PlayerAttack = 2;

    //gun sound
    /// <summary>
    /// Audio source of the gun sound
    /// </summary>
    public AudioSource shoot;

    //patrolling
    /// <summary>
    /// Vector 3 of walk point
    /// </summary>
    public Vector3 walkPoint;
    /// <summary>
    /// bool of walk point set
    /// </summary>
    bool walkPointSet;
    /// <summary>
    /// float of walk point range
    /// </summary>
    public float walkPointRange = 5;
    /// <summary>
    /// Gameobject of  projectiles
    /// </summary>
    public GameObject projectiles;

    //Attacking
    /// <summary>
    /// float of time Between attacks
    /// </summary>
    public float timeBetweenAttacks = 1;
    /// <summary>
    /// float of already attacked
    /// </summary>
    bool alreadyAttacked;

    //States
    /// <summary>
    /// float of the sight range and attack range of enemies
    /// </summary>
    public float sightRange = 20, attackRange = 12;
    /// <summary>
    /// bool of player in sight range, and player in attack range
    /// </summary>
    public bool playerInSightRange, playerInAttackRange;

    /// <summary>
    /// awake function
    /// </summary>
    private void Awake()
    {
        //player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
    
    }

    /// <summary>
    /// update function
    /// </summary>
    private void Update()
    {
        if (player != null)
        {
            //Check for sight and attack range
            player = GameObject.FindObjectOfType<PlayerMovement>().transform;

            playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInSightRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        

    }

    /// <summary>
    /// patrolling function
    /// </summary>
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);     
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude< 1f)
        {
            walkPointSet = false;
        }
    }

    /// <summary>
    /// Search walk point fucntion
    /// </summary>
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
    /// <summary>
    /// chase player function
    /// </summary>
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    /// <summary>
    /// attack player function
    /// </summary>
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // attack code here
            Rigidbody rb = Instantiate(projectiles, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 24f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);
            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    /// <summary>
    /// rest attack function
    /// </summary>
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /// <summary>
    /// Hurt function 
    /// </summary>
    public void Hurt()
    {
        //enemy gets hurt when this function is called
        CurrentEnemyHealth -= PlayerAttack;
        healthBar.SetHealth(CurrentEnemyHealth);
        Debug.Log(" Enemy health: " + CurrentEnemyHealth);
        shoot.Play(); 

        if (CurrentEnemyHealth <= 0)
        {
            Destroy(gameObject); // destroy enemy when player kills it
        }

        if (gameObject.tag == "boss" && CurrentEnemyHealth == 0)
        {
            Debug.Log("boss defeated");
            FindObjectOfType<PlayerMovement>().canCollect = true;
        }

    }

    /// <summary>
    /// start function
    /// </summary>
    private void Start()
    {
        CurrentEnemyHealth = MaxEnemyHealth;
        healthBar.SetMaxHealth(MaxEnemyHealth);
    }


}
