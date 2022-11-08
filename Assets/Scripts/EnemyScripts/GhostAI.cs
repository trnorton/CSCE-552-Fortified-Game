using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyDmgEffect;
    public WeaponController wc;
    public NavMeshAgent agent;
    public GameObject treasure;
    public LayerMask isTreasure, isPlayer;
    public float playerRange;
    public float attackRange;
    public bool playerInRange;
    public bool playerInAttackRange;
    public bool treasureInAttackRange;
    public float distanceToPlayer;
    public float distanceToTreasure;
    public bool treasureDes;
    public bool wallDes;

    public int enemyMoneyValue;
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        treasureDes = false;
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreLayerCollision(10,9);
        Physics.IgnoreLayerCollision(10,8);
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        distanceToTreasure = Vector3.Distance(treasure.transform.position, this.transform.position);

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

        if(playerInRange && !playerInAttackRange) ChasePlayer();
        if(playerInRange && playerInAttackRange && !treasureInAttackRange) AttackPlayer();
        if(!playerInRange && !playerInAttackRange && !treasureInAttackRange) ChaseTreasure();
        if(treasureInAttackRange) AttackTreasure();
        

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
            enemyHealCompoent.TakeDamage(1);
            Instantiate(enemyDmgEffect, transform.position, Quaternion.identity);
            isElim(enemyHealCompoent.currentHealth);

        }


    }

    public void isElim(int currHealth)
    {
        Debug.Log(currHealth);
        if(currHealth == 0)
        {
            var playermoney = player.GetComponent<Money>();
            playermoney.AddMoney(enemyMoneyValue);
            Destroy(gameObject);
        }
    }

}