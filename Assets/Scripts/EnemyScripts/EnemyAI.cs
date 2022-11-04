using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public WeaponController wc;
    public NavMeshAgent agent;
    public GameObject treasure;
    public LayerMask isTreasure, isPlayer, isWall;
    public GameObject Wall;
    public float playerRange;
    public float attackRange;
    public bool playerInRange;
    public bool playerInAttackRange;
    public bool treasureInAttackRange;
    public bool wallInFront;
    public float distanceToPlayer;
    public float distanceToTreasure;
    public float distanceToWall;
    public bool treasureDes;
    public bool wallDes;

    public int enemyMoneyValue;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        treasureDes = false;
        wallDes = false;
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
        agent.isStopped = true;
        if(!alreadyAttacked)
        {
            //Animation would go here
            var treasureHealthComponent = treasure.GetComponent<Health>();
            if(treasureHealthComponent != null)
            {
                treasureHealthComponent.TakeDamage(1);
            }
            if(treasureHealthComponent.currentHealth == 0)
            {
                treasure.SetActive(false);
                agent.isStopped = false;
                treasureDes = true;
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    //Atack Wall
    private void AttackWall()
    {
        agent.isStopped = true;

        //transform.LookAt(Wall.transform);

        if(!alreadyAttacked)
        {
            //Animation would go here
            var wallHealthComponent = Wall.GetComponent<Health>();
            if(wallHealthComponent != null)
            {
                wallHealthComponent.TakeDamage(1);
            }
            if(wallHealthComponent.currentHealth == 0)
            {
                Wall.SetActive(false);
                wallDes = true;
                agent.isStopped = false;
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    //Attack Player
    private void AttackPlayer()
    {
        agent.isStopped = true;
        transform.LookAt(player.transform);

        if(!alreadyAttacked)
        {
            //Animation would go here
            var playerHealthComponent = player.GetComponent<Health>();
            if(playerHealthComponent != null)
            {
                playerHealthComponent.TakeDamage(1);
            }
            if(playerHealthComponent.currentHealth == 0)
            {
                player.SetActive(false);
                agent.isStopped = false;
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var enemyHealCompoent = GetComponent<Health>();
        if(other.tag == "Weapon" && wc.isAttacking)
        {
            Debug.Log(enemyHealCompoent.currentHealth);
            enemyHealCompoent.TakeDamage(1);

            if(enemyHealCompoent.currentHealth <= 0)
            {
                var playermoney = player.GetComponent<Money>();
                playermoney.AddMoney(enemyMoneyValue);
                Destroy(gameObject);
            }
        }


    }

}
