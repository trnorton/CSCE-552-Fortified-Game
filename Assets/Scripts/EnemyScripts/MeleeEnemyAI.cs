using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{
    public GameObject player;
    // public GameObject enemyDmgEffect;
    public GameObject weaponHolder;
    public WeaponController wc;
    public NavMeshAgent agent;
    private GameObject treasure;
    public LayerMask isTreasure, isPlayer, isWall;
    public GameObject Wall;
    public float playerRange;
    public float attackRange;
    private bool playerInRange;
    private bool playerInAttackRange;
    private bool treasureInAttackRange;
    private bool wallInFront;
    private float distanceToPlayer;
    private float distanceToTreasure;
    private float distanceToWall;
    private bool treasureDes;
    System.DateTime invincibleFrames = System.DateTime.Now;

    public int enemyMoneyValue;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    private Animator ObjectAnimator;

    private GameObject[] allTargets;
    private GameObject[] walls;
    private GameObject[] defaultTargets;
    private GameObject target;

    private NavMeshPath path;
    private float elapsed;

    void Start()
    {
        treasureDes = false;
        //ObjectAnimator = this.GetComponent<Animator>();
       
    }
    private void Awake()
    {
        elapsed = 0.0f;
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

        //Get Path every second
        /*elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            if(NavMesh.CalculatePath(transform.position, treasure.transform.position, NavMesh.AllAreas, path))
            {
                //agent.SetPath(path);
            }
            

        }
        //Drawing the nav path
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
        */



        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        
        if(!treasureDes)
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
        
        if(distanceToWall <= attackRange && Wall.activeSelf)
            wallInFront = true;
        else
            wallInFront = false;

        if(playerInRange && !playerInAttackRange && !wallInFront) ChasePlayer();
        if(playerInRange && playerInAttackRange && !treasureInAttackRange && !wallInFront) AttackPlayer();
        if(!playerInRange && !playerInAttackRange && !treasureInAttackRange && !wallInFront && !treasureDes) ChaseTreasure();
        if(treasureInAttackRange && !wallInFront) AttackTreasure();
        if(wallInFront) AttackWall();
        
        

    }
    //Enemy goes after treasure
    private void ChaseTreasure()
    {
        agent.isStopped = false;
        //ObjectAnimator.SetBool("IsAttacking", false);
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
            //ObjectAnimator.SetBool("IsAttacking", true);

            var treasureHealthComponent = treasure.GetComponent<Health>();
            if(treasureHealthComponent != null)
            {
                if(treasureHealthComponent.currentHealth == 1)
                {
                    agent.isStopped = false;
                    treasureDes = true;
                    //ObjectAnimator.SetBool("IsAttacking", false);
                }

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
    //Attack Wall
    private void AttackWall()
    {
        agent.isStopped = true;

        

        if(!alreadyAttacked)
        {
            //Animation would go here
            //ObjectAnimator.SetBool("IsAttacking", true);

            var wallHealthComponent = Wall.GetComponent<Health>();
            if(wallHealthComponent != null)
            {
                if(wallHealthComponent.currentHealth == 1)
                {
                    agent.isStopped = false;
                    //ObjectAnimator.SetBool("IsAttacking", false);
                }

                wallHealthComponent.TakeDamage(1);
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
            //ObjectAnimator.SetBool("IsAttacking", true);


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
        if(other.tag == "Bat" && wc.isAttacking || other.tag == "PlayerProjectile" && wc.isAttacking || other.tag == "TurretProjectile")
        {
            if(invincibleFrames <= System.DateTime.Now)
            {
                enemyHealCompoent.TakeDamage(1);
                // Instantiate(enemyDmgEffect, transform.position, Quaternion.identity);
                isElim(enemyHealCompoent.currentHealth);
                Reset();
            }
        }
        if(other.tag == "Sword" && wc.isAttacking)
        {
            if(invincibleFrames <= System.DateTime.Now)
            {
                enemyHealCompoent.TakeDamage(2);
                isElim(enemyHealCompoent.currentHealth);
                Reset();
            }
        }
    }

    public void isElim(float currHealth) 
    {
        // Debug.Log(currHealth);
        if(currHealth == 0)
        {
            var playermoney = player.GetComponent<Money>();
            playermoney.AddMoney(enemyMoneyValue);
            Destroy(gameObject);
        }
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
