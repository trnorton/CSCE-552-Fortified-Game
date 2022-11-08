using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform spawnObject;
    public int spawnTotal = 3;
    public float timeBetweenSpawns;
    private int roundNumber = 1;
    void Start()
    {
        StartCoroutine(SpawnGameObject());
    }

    void Update()
    {
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemiesLeft.Length);
        if((enemiesLeft.Length-1) == 0)
        {
            StopCoroutine(SpawnGameObject());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObject());
            }
        }
    }
    IEnumerator SpawnGameObject()
    {
        spawnTotal *= roundNumber;
        for(var i = 0; i < spawnTotal; i++)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spawnObject, randomSpawn.position, randomSpawn.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        roundNumber++;
    }
}
