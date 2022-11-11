using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    public GameObject player;
    // public GameObject enemyDmgEffect;
    public WeaponController wc;
    public GameObject weaponHolder;
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
    System.DateTime invincibleFrames = System.DateTime.Now;

    public int enemyMoneyValue;
    public Transform firePoint;
    public float fireSpeed;
    public float fireTimer;
    public bool isFiring;

    private GameObject[] allTargets;
    private GameObject[] walls;
    private GameObject[] defaultTargets;
    private GameObject target;

    private NavMeshPath path;

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
        player = GameObject.FindWithTag("Player");
        weaponHolder = GameObject.FindWithTag("weaponcontrol");
        wc = weaponHolder.GetComponent<WeaponController>();
        Wall = getClosestWall();
        treasure = GameObject.FindWithTag("Treasure");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Wall = getClosestWall();
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        distanceToWall = Vector3.Distance(Wall.transform.position, this.transform.position);

        if(!treasureDes)
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
            if(wallHealthComponent.currentHealth == 1)
            {
                agent.isStopped = false;
            }
            StartCoroutine(fire());
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

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealCompoent = GetComponent<Health>();
        if(other.tag == "Weapon" && wc.isAttacking)
        {
            if(invincibleFrames <= System.DateTime.Now)
            {
                enemyHealCompoent.TakeDamage(1);
                // Instantiate(enemyDmgEffect, transform.position, Quaternion.identity);
                isElim(enemyHealCompoent.currentHealth);
                Reset();
            }

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

    IEnumerator fire()
    {
            isFiring = true;
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * fireSpeed);
            yield return new WaitForSeconds(fireTimer);
            isFiring = false;
    }

    public GameObject getClosestWall()
    {

        walls = GameObject.FindGameObjectsWithTag("Wall");
        float closest = 0;
        for(int i = 0; i < walls.Length; i++)
        {
            float dist = Vector3.Distance(walls[i].transform.position, this.transform.position);
            if(closest == 0)
            {
                closest = dist;
            }
            else if(dist < closest)
            {
                closest = dist;
                target = walls[i];
            }
        }
        return target;

    }
    //2 seconds of invincibility
    void Reset()
    {
        invincibleFrames = System.DateTime.Now.AddSeconds(1);
    }

}



