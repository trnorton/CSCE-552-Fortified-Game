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
    private Animator ObjectAnimator;

    private GameObject[] allTargets;
    private GameObject[] walls;
    private GameObject[] defaultTargets;
    private GameObject target;

    private NavMeshPath path;
    public AudioSource damageSound;
    public AudioSource shootSound;
    
    bool alreadyAttacked;

    void Start()
    {
        treasureDes = false;
        wallDes = false;
        isFiring = false;
        ObjectAnimator = this.GetComponent<Animator>();
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
        
        if(distanceToWall <= attackRange)
            wallInFront = true;
        else
            wallInFront = false;

        if(playerInRange && !playerInAttackRange) ChasePlayer();
        if(playerInRange && playerInAttackRange && !treasureInAttackRange) AttackPlayer();
        if(!playerInRange && !playerInAttackRange && !treasureInAttackRange) ChaseTreasure();
        if(treasureInAttackRange && !wallInFront) AttackTreasure();
        if(wallInFront && !wallDes) AttackWall();
        
        

    }
    //Enemy goes after treasure
    private void ChaseTreasure()
    {
        ObjectAnimator.SetBool("IsAttacking", false);
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
        ObjectAnimator.SetBool("IsAttacking", true);
        transform.LookAt(treasure.transform);
        firePoint.LookAt(treasure.transform);
        if(!isFiring)
        { 
            StartCoroutine(fire());
        }
       
    }
    //Atack Wall
    private void AttackWall()
    {
        var wallHealthComponent = Wall.GetComponent<Health>();
        agent.isStopped = true;
        ObjectAnimator.SetBool("IsAttacking", true);
        Vector3 lookPos = Wall.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        firePoint.LookAt(Wall.transform);
        if(!isFiring)
        {
            
            StartCoroutine(fire());
        }
    }
    //Attack Player
    private void AttackPlayer()
    {
        
        agent.isStopped = true;
        ObjectAnimator.SetBool("IsAttacking", true);
        Vector3 lookPos = player.transform.position;
        lookPos.y = transform.position.y;
        firePoint.LookAt(player.transform);
        transform.LookAt(lookPos);
        if(!isFiring)
        {
            StartCoroutine(fire());
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealCompoent = GetComponent<Health>();
        if(other.tag == "Bat" && wc.isAttacking || other.tag == "PlayerProjectile" && wc.isAttacking || other.tag == "TurretProjectile")
        {
            if(invincibleFrames <= System.DateTime.Now)
            {
                enemyHealCompoent.TakeDamage(1);
                damageSound.Play(0);
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
        Debug.Log(currHealth);
        if(currHealth == 0)
        {
            var playermoney = player.GetComponent<Money>();
            playermoney.AddMoney(enemyMoneyValue);
            Destroy(gameObject, 0.25f);
        }
    }

    IEnumerator fire()
    {
            isFiring = true;
            yield return new WaitForSeconds(fireTimer);
            GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            shootSound.Play(0);
            newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * fireSpeed);
            
            //Adding this snip of code here to beat a bug where they thought wall was already broken
            var wallHealthComponent = Wall.GetComponent<Health>();
            if(wallHealthComponent.currentHealth == 1)
            {
                ObjectAnimator.SetBool("IsAttacking", false);
                agent.isStopped = false;
                wallDes = true;
                agent.SetDestination(treasure.transform.position);
            }

            //Trying to match my animation to the shooting of projectile
            yield return new WaitForSeconds(0.25f);
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



