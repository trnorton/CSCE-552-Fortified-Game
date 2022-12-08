using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class friendlyinterface : MonoBehaviour
{
    private GameObject friendly;
    private GameObject target;
    private GameObject[] friendlies;
    private Transform randominsidePosition;
    public Transform[] insidePositions;
    private Transform northPosition;
    public Transform[] northPositions;
    private Transform southPosition;
    public Transform[] southPositions;
    private Transform eastPosition;
    public Transform[] eastPositions;
    private Transform westPosition;
    public Transform[] westPositions;
    private GameObject player;
    public TextMeshProUGUI friendlyHealthText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        friendlyHealthText.text = "5";
        
    }

    // Update is called once per frame
    void Update()
    {
        getClosestFriendly();
    }
    public void OnClickStay()
    {
        friendly = getClosestFriendly();
        randominsidePosition = insidePositions[Random.Range(0, insidePositions.Length)];
        
        friendly.GetComponent<FriendlyAi>().Go(randominsidePosition.transform.position);
        

    }
    public void OnClickNorth()
    {
        friendly = getClosestFriendly();
        northPosition = northPositions[Random.Range(0, northPositions.Length)];
        friendly.GetComponent<FriendlyAi>().Go(northPosition.transform.position);
        
    }
    public void OnClickSouth()
    {
        friendly = getClosestFriendly();
        southPosition = southPositions[Random.Range(0, southPositions.Length)];
        friendly.GetComponent<FriendlyAi>().Go(southPosition.transform.position);
    }
    public void OnClickWest()
    {
        friendly = getClosestFriendly();
        westPosition = westPositions[Random.Range(0, westPositions.Length)];
        friendly.GetComponent<FriendlyAi>().Go(westPosition.transform.position);
    }
    public void OnClickEast()
    {
        friendly = getClosestFriendly();
        eastPosition = eastPositions[Random.Range(0, eastPositions.Length)];
        friendly.GetComponent<FriendlyAi>().Go(eastPosition.transform.position);
    }
    public void OnClickHalt()
    {
        friendly = getClosestFriendly();
        friendly.GetComponent<FriendlyAi>().StopMoving();
    }
    public GameObject getClosestFriendly()
    {
        friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        player = GameObject.FindGameObjectWithTag("Player");
        float closest = 0;
        target = null;
        for(int i = 0; i < friendlies.Length; i++)
        {
            float dist = Vector3.Distance(friendlies[i].transform.position, player.transform.position);
            if(closest == 0)
            {
                closest = dist;
                target = friendlies[i];
            }
            else if(dist < closest)
            {
                closest = dist;
                target = friendlies[i];
            }
        }
        if(target != null)
        {
            var friendlyHealth = target.GetComponent<Health>();
            float friendlyHP = friendlyHealth.HealthtoInt();
            friendlyHealthText.text = friendlyHP.ToString();

        }
        return target;
    }
}
