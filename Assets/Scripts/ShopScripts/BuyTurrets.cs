using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurrets : MonoBehaviour
{
    public GameObject player;
    public int turretCost;
    private int money;
    private GameObject[] turrets;
    public GameObject turretManager;

    void Start()
    {
        turretManager = GameObject.FindWithTag("TurretManager");
        player = GameObject.FindWithTag("Player");
    }
    public void onButtonPress()
    {
        var manager = turretManager.GetComponent<TurretHandler>();
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();

        if(money >= turretCost)
        {
            playercash.SubMoney(turretCost);
            manager.deployTurrets();
        }
        else
        {
            Debug.Log("You are poor");
        }
    }

}
