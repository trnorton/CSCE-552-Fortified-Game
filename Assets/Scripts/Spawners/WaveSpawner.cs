using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform[] spawnObjectMeleeRanged;
    public Transform spawnObjectGhost;
    public bool inRound;
    public int spawnTotal = 3;
    public float timeBetweenSpawns;
    private int roundNumber = 1;
    public GameObject roundIndicator;
    public TextMeshProUGUI roundText;

    void Start()
    {
        //inRound = false;
        roundIndicator.SetActive(true);
    }

    void Update()
    {
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemiesLeft.Length <= 0){
            //inRound = false;
            roundIndicator.SetActive(true);
        } else {
            roundIndicator.SetActive(false);
            //inRound = true;
        }

        Debug.Log("ROUND: " + roundNumber);
        Debug.Log("REM ENEMIES: " + enemiesLeft.Length);
        Debug.Log("In Round: " + inRound);
        // Debug.Log(spawnObject.name);
        if((enemiesLeft.Length) == 0 && roundNumber != 3 && roundNumber != 5)
        {
            //Load Some Mid-round UI here
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectMelee());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 3)
        {
            //Load Some Mid-round UI here
            // StopCoroutine(SpawnGameObjectMelee());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectGhost());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 4)
        {
            //Load Some Mid-round UI here
            // StopCoroutine(SpawnGameObjectGhost());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectMelee());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber > 4)
        {
            //Load Some Mid-round UI here
            // StopCoroutine(SpawnGameObjectMelee());
            SceneManager.LoadScene("WinScene");
        }
        //Boss round would come after this

    }
    IEnumerator SpawnGameObjectMelee()
    {
        spawnTotal = spawnTotal + (roundNumber*2);
        for(var i = 0; i < spawnTotal; i++)
        {
            Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Transform randomEnemy = spawnObjectMeleeRanged[Random.Range(0, spawnObjectMeleeRanged.Length)];
            Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);
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

    public bool isPlaying(){
        return inRound;
    }
}
