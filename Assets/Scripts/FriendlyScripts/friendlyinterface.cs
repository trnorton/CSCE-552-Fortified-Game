using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class friendlyinterface : MonoBehaviour
{
    private GameObject friendly;
    private GameObject target;
    private GameObject[] friendlies;
    public GameObject insidePosition;
    public GameObject northPosition;
    public GameObject southPosition;
    public GameObject westPosition;
    public GameObject eastPosition;
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
        friendly.GetComponent<FriendlyAi>().Go(insidePosition.transform.position);
        

    }
    public void OnClickNorth()
    {
        friendly = getClosestFriendly();
        friendly.GetComponent<FriendlyAi>().Go(northPosition.transform.position);
        
    }
    public void OnClickSouth()
    {
        friendly = getClosestFriendly();
        friendly.GetComponent<FriendlyAi>().Go(southPosition.transform.position);
    }
    public void OnClickWest()
    {
        friendly = getClosestFriendly();
        friendly.GetComponent<FriendlyAi>().Go(westPosition.transform.position);
    }
    public void OnClickEast()
    {
        friendly = getClosestFriendly();
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
