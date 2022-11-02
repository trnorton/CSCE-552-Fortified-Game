using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;
    public GameObject treasure;
    public LayerMask isTreasure, isPlayer, isWall;
    public GameObject Wall;
    public float playerRange;
    public float attackRange;
    public GameObject projectile;
    public bool playerInRange;
    public bool playerInAttackRange;
    public bool treasureInAttackRange;
    public bool wallInFront;
    public float distanceToPlayer;
    public float distanceToTreasure;
    public float distanceToWall;
    public bool treasureDes;
    public bool wallDes;

    public Transform firePoint;
    public float fireSpeed;
    public float fireTimer;
    public bool isFiring;


    public float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        treasureDes = false;
        wallDes = false;
        isFiring = false;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        distanceToTreasure = Vector3.Distance(treasure.transform.position, this.transform.position);
        distanceToWall = Vector3.Distance(Wall.transform.position, this.transform.position);

        if(distanceToPlayer <= playerRange) 
            playerInRange = true;
        else
            playerInRange = false;

        if(distanceToPlayer <= attackRange)
            playerInAttackRange = true;
        else
            playerInAttackRange = false;

        if(distanceToTreasure <= attackRange)
            treasureInAttackRange = true;
        else
            treasureInAttackRange = false;
        
        if(distanceToWall <= attackRange && wallDes == false)
            wallInFront = true;
        else
            wallInFront = false;

        if(playerInRange && !playerInAttackRange && !wallInFront) ChasePlayer();
        if(playerInRange && playerInAttackRange && !treasureInAttackRange && !wallInFront) AttackPlayer();
        if(!playerInRange && !playerInAttackRange && !treasureInAttackRange && !wallInFront) ChaseTreasure();
        if(treasureInAttackRange && !wallInFront) AttackTreasure();
        if(wallInFront) AttackWall();

    }
    //Enemy goes after treasure
    private void ChaseTreasure()
    {
        agent.isStopped = false;
        agent.SetDestination(treasure.transform.position);
    }
    //Enemy goes after player
    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
    //Atack Treasure
    private void AttackTreasure()
    {
        var treasureHealthComponent = treasure.GetComponent<Health>();
        agent.isStopped = true;
        transform.LookAt(treasure.transform);
        if(!isFiring)
        {
            StartCoroutine(fire());
        }
        if(treasureHealthComponent.currentHealth == 0)
        {
            treasure.SetActive(false);
            agent.isStopped = false;
        }
    }
    //Atack Wall
    private void AttackWall()
    {
        var wallHealthComponent = Wall.GetComponent<Health>();
        agent.isStopped = true;
        transform.LookAt(Wall.transform);
        if(!isFiring)
        {
            StartCoroutine(fire());
        }
        if(wallHealthComponent.currentHealth == 0)
        {
            Wall.SetActive(false);
            agent.isStopped = false;
        }
    }
    //Attack Player
    private void AttackPlayer()
    {
        var playerHealthComponent = player.GetComponent<Health>();
        agent.isStopped = true;
        transform.LookAt(player.transform);
        if(!isFiring)
        {
            StartCoroutine(fire());
        }
        if(playerHealthComponent.currentHealth == 0)
        {
            player.SetActive(false);
            agent.isStopped = false;
        }
    }

    IEnumerator fire()
    {
            isFiring = true;
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * fireSpeed);
            yield return new WaitForSeconds(fireTimer);
            isFiring = false;

    }

}



