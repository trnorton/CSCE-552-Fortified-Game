using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurrets : MonoBehaviour
{
    public GameObject player;
    public int turretCost;
    private int money;
    private GameObject[] turrets;

    void Start()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        disableTurrets();
        player = GameObject.FindWithTag("Player");
    }
    public void onButtonPress()
    {
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();

        if(money >= turretCost)
        {
            playercash.SubMoney(turretCost);
            deployTurrets();
        }
        else
        {
            Debug.Log("You are poor");
        }
    }

    void deployTurrets()
    {
        foreach(GameObject t in turrets)
        {
            t.SetActive(true);
        }
    }

    void disableTurrets()
    {
        foreach(GameObject t in turrets)
        {
            t.SetActive(false);
        }
    }
}
