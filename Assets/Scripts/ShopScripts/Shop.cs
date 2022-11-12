using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;
    public GameObject weaponCont;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buySlingShoy(){
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= 10){
            //todo
        } else {
            Debug.Log("kick rocks you broke ass hoe");
        }
    }

    public void buySword(){
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= 10){
            //todo
        } else {
            Debug.Log("kick rocks you broke ass hoe");
        }
    }
}
