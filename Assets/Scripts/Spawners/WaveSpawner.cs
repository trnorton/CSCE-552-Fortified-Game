using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform spawnObjectMelee;
    public Transform spawnObjectGhost;
    public int spawnTotal = 3;
    public float timeBetweenSpawns;
    private int roundNumber = 1;

    void Start()
    {

    }

    void Update()
    {
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("ROUND: " + roundNumber);
        Debug.Log("REM ENEMIES: " + enemiesLeft.Length);
        // Debug.Log(spawnObject.name);
        if((enemiesLeft.Length) == 0 && roundNumber != 3 && roundNumber != 5)
        {
            //Load Some Mid-round UI here
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectMelee());
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 3)
        {
            //Load Some Mid-round UI here
            StopCoroutine(SpawnGameObjectMelee());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectGhost());
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 4)
        {
            //Load Some Mid-round UI here
            StopCoroutine(SpawnGameObjectGhost());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectGhost());
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 5)
        {
            //Load Some Mid-round UI here
            StopCoroutine(SpawnGameObjectMelee());
        }
        //Boss round would come after this
       
    }
    IEnumerator SpawnGameObjectMelee()
    {
        spawnTotal = spawnTotal + (roundNumber*2);
        for(var i = 0; i < spawnTotal; i++)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnObjectMelee, randomSpawn.position, randomSpawn.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        roundNumber++;
    }

    IEnumerator SpawnGameObjectGhost()
    {
        spawnTotal = spawnTotal + (roundNumber*2);
        for(var i = 0; i < spawnTotal; i++)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnObjectGhost, randomSpawn.position, randomSpawn.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        roundNumber++;
    }
}
