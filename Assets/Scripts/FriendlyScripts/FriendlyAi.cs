using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAi : MonoBehaviour
{
    public GameObject defaultLocation;
    public float attackinrange;
    private Vector3 idleLocation;
    private float timeBetweenAttacks;
    public NavMeshAgent agent;
    private NavMeshPath path;

    private GameObject[] allTargets;
    private GameObject[] enemies;
    private GameObject[] defaultTargets;
    private GameObject target;


    private float attackdistance;
    private GameObject closestEnemy;
    private float distanceToEnemy;
    private bool alreadyAttacked;
    private bool enemyattacktrigger;

    private float elapsed;

    private Animator ObjectAnimator; 

    // Start is called before the first frame update
    void Start()
    {
      agent = GetComponent<NavMeshAgent>();
      attackdistance = 5;
      timeBetweenAttacks = 1;
      path = new NavMeshPath();
      ObjectAnimator = GetComponent<Animator>();
      ObjectAnimator.SetInteger("Action", 0);
      Physics.IgnoreLayerCollision(9, 14);
      Physics.IgnoreLayerCollision(9,15);
     
      
      idleLocation = this.transform.position;
      
      

      
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = getClosestEnemy();
        
        //Little script to avoid an error
        if(closestEnemy != null)
        {
            distanceToEnemy= Vector3.Distance(closestEnemy.transform.position, this.transform.position);
        }
        else
        {
            distanceToEnemy = 1000;
        }

        //Get Path every second
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;

            if(closestEnemy !=null)
            NavMesh.CalculatePath(transform.position, closestEnemy.transform.position, NavMesh.AllAreas, path);
           
        }
        //Drawing the nav path
        /*for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }*/
        float distanceToIdle = Vector3.Distance(idleLocation, this.transform.position);

       if(distanceToEnemy <= attackinrange && distanceToEnemy > attackdistance)
        {
            agent.isStopped = false;
            agent.SetPath(path);
            ObjectAnimator.SetInteger("Action", 1);

        }
        else if(distanceToEnemy <= attackdistance)
        {
            var script = closestEnemy.GetComponent<MeleeEnemyAI>();
            if(script == null)
            {
                var rangedscript = closestEnemy.GetComponent<RangedEnemyAI>();
                rangedscript.beingAttacked = true;
            }
            else
            {
                script.beingAttacked = true;
            }
            AttackEnemy();
        }
        else if(distanceToIdle > 3.0f)
        {
            
            agent.isStopped = false;
            agent.SetDestination(idleLocation);
            ObjectAnimator.SetInteger("Action", 1);
        }
        else
        {
            
            agent.isStopped = true;
            ObjectAnimator.SetInteger("Action", 0);
        }
        
    }



    public void AttackEnemy()
    {
        ObjectAnimator.SetInteger("Action", 2);
        if(!alreadyAttacked)
        { 
            agent.isStopped = true;

            //getting health component
            var enemyhealth = closestEnemy.GetComponent<Health>();
            enemyhealth.TakeDamage(1);
            
            //Have to pull this script to access this method
            var meleeScript = closestEnemy.GetComponent<MeleeEnemyAI>();
            if(meleeScript == null)
            {
                var rangedmeleeScript = closestEnemy.GetComponent<RangedEnemyAI>();
                rangedmeleeScript.isElim(enemyhealth.currentHealth);
            }
            else
            {
                meleeScript.isElim(enemyhealth.currentHealth);
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void Go(Vector3 position)
    {
        idleLocation = position;
    }
    public void StopMoving()
    {
        idleLocation = transform.position;
    }
    public GameObject getClosestEnemy()
    {
        target = null;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector3.Distance(enemies[i].transform.position, this.transform.position);
            if(closest == 0)
            {
                closest = dist;
                target = enemies[i];
                
            }
            else if(dist < closest)
            {
                closest = dist;
                target = enemies[i];
            }
        }
        
        return target;

    }
}
