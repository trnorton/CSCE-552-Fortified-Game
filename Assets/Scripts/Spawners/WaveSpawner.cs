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
    public Transform spawnObjectBoss;
    public bool inRound;
    public int spawnTotal = 3;
    public int elimCount = 0;
    public float timeBetweenSpawns;
    public int roundNumber;
    public GameObject roundIndicator;
    public TextMeshProUGUI roundText;
    public bool check;

    void Start()
    {
        //inRound = false;
        roundIndicator.SetActive(true);
    }

    void Update()
    {
        GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesDefeated();

        if(enemiesLeft.Length <= 0){
            //inRound = false;
            roundIndicator.SetActive(true);
        } else {
            roundIndicator.SetActive(false);
            //inRound = true;
        }
        // Debug.Log("ROUND: " + roundNumber);
        // Debug.Log("REM ENEMIES: " + enemiesLeft.Length);
        // Debug.Log("In Round: " + inRound);
        // Debug.Log(spawnObject.name);
        if((enemiesLeft.Length) == 0 && roundNumber != 3 && roundNumber != 5)
        {
            elimCount = 0;
            //Load Some Mid-round UI here
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectMelee());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 3)
        {
            elimCount = 0;
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
            elimCount = 0;
            //Load Some Mid-round UI here
            // StopCoroutine(SpawnGameObjectGhost());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectMelee());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber == 5)
        {
            elimCount = 0;
            //Load Some Mid-round UI here
            // StopCoroutine(SpawnGameObjectGhost());
            if(Input.GetKey(KeyCode.Tab))
            {
                StartCoroutine(SpawnGameObjectBoss());
                roundText.text = "Round " + roundNumber;
            }
        }
        if((enemiesLeft.Length) == 0 && roundNumber > 5)
        {
            elimCount = 0;
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
    }

    IEnumerator SpawnGameObjectBoss()
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(spawnObjectBoss, randomSpawn.position, randomSpawn.rotation);
        spawnTotal = spawnTotal + (roundNumber*2);
        for(var i = 0; i < spawnTotal; i++)
        {
            randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Transform randomEnemy = spawnObjectMeleeRanged[Random.Range(0, spawnObjectMeleeRanged.Length)];
            Instantiate(randomEnemy, randomSpawn.position, randomSpawn.rotation);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void setRound(int num)
    {
        roundNumber = num;
    }

    public bool isPlaying(){
        return inRound;
    }

    public void enemiesDefeated()
    {
        if(check == false && elimCount == spawnTotal)
            if(check == true)
                check = false;
            else
            {
                roundNumber++;
                check = true;
            }

    }
}
