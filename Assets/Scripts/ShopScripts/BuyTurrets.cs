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
    public AudioSource Buy;
    public AudioSource notEnoughCash;

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

        if(money >= turretCost && manager.isTurretsActive() == false)
        {
            playercash.SubMoney(turretCost);
            manager.deployTurrets();
            Buy.Play();
        }
        else
        {
            notEnoughCash.Play();
            Debug.Log("You are poor");
        }
    }

}
