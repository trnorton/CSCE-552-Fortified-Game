using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretAI : MonoBehaviour
{
    public GameObject projectile;
    public bool enemyInRange;
    public Transform firePoint;
    public float fireSpeed;
    public float fireTimer;
    public bool isFiring;
    public float distnaceToTarget;
    public float attackRange;
 
    private GameObject[] enemies;
    private GameObject target;
    private GameObject[] areEnemies;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
    }

    // private void Awake()
    // {
    //     target = getClosestEnemy();
    // }
    // Update is called once per frame
    void Update()
    {
        areEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(areEnemies.Length != 0)
        {
            // Debug.Log("POS: " + this.transform.position);
            target = getClosestEnemy();
            if(target != null)
            {
                distnaceToTarget = Vector3.Distance(target.transform.position, this.transform.position);
            }
            if(distnaceToTarget <= attackRange)
                shootTarget();
        }
    }

    //Shoot at enemy
    private void shootTarget()
    {
        if(target != null)
        {
            Vector3 lookPos = target.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            if(!isFiring)
            {
                StartCoroutine(fire());
            }
        }
    }

    //Get the closest enemy to the turret    
    public GameObject getClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector3.Distance(enemies[i].transform.position, this.transform.position);
            if(closest == 0)
            {
                closest = dist;
            }
            else if(dist < closest)
            {
                closest = dist;
                target = enemies[i];
            }
        }
        return target;
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
