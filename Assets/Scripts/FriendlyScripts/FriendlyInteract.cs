using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyInteract : MonoBehaviour
{
    
    public GameObject warriorIndicator;
    public GameObject warriorInterface;
    public KeyCode interactKey = KeyCode.F;
    public KeyCode closeInteractMenu = KeyCode.X;
    
    
    
   
    private GameObject friendly;
    private GameObject target;
    private GameObject[] friendlies;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
        warriorInterface = GameObject.Find("warriorInterface");
        warriorIndicator = GameObject.Find("PressFWarriors");
        warriorInterface.SetActive(false);
        warriorIndicator.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        FriendlyDistance();
        if(warriorIndicator.activeInHierarchy == true && Input.GetKeyDown(interactKey))
        {   
            
            warriorInterface.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(closeInteractMenu))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            warriorInterface.SetActive(false);

        }
    }
    void FriendlyDistance()
    {
        friendly = getClosestFriendly();
        if(friendly != null)
        {
            float distanceFromPlayer = Vector3.Distance(friendly.transform.position, player.transform.position);
            if(distanceFromPlayer < 2.0f)
            {
                warriorIndicator.SetActive(true);
                
            }
            else
            {
                warriorIndicator.SetActive(false);
                
            }
        }

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
        return target;
    }
}
