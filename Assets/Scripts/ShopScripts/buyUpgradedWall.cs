using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buyUpgradedWall : MonoBehaviour
{
    public GameObject player;
    public GameObject stoneWallPrefab;
    public GameObject metalWallPrefab;
    public GameObject UpgradeButton;
    private TextMeshProUGUI UpgradeButtonText;

    public GameObject RepairButton;
    private TextMeshProUGUI RepairButtonText;
    public int StoneWallCost;
    public int MetalWallCost;
    private int money;
    private GameObject[] walls;
    public int wallLevel;
    // Start is called before the first frame update
    void Start()
    {
        wallLevel = 0;
        RepairButtonText = RepairButton.GetComponentInChildren<TextMeshProUGUI>();
        RepairButtonText.text = "Repair Walls $20";
        UpgradeButtonText = UpgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        UpgradeButtonText.text = "Upgrade Walls $50";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPress()
    {
        Debug.Log("Button Pressed");
        //Getting Player cash
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        
        //if the walls are wooden
        if(wallLevel == 0)
        {
            if(money >= StoneWallCost)
            {
                playercash.SubMoney(StoneWallCost);
                upgradeWoodWalls();
                wallLevel++;
                UpgradeButtonText.text = "Upgrade Walls $100";
                RepairButtonText.text = "Repair Walls $30";

            }
            else
            {
                Debug.Log("Not enough funds");
            }
        }
        else if(wallLevel == 1)
        {
            if(money >= MetalWallCost)
            {
                playercash.SubMoney(MetalWallCost);
                upgradeStoneWalls();
                wallLevel++;
                UpgradeButtonText.text = "Walls Max Upgrade";
                RepairButtonText.text = "Repair Walls $50";
            }
            else
            {
                Debug.Log("Not enough funds");
            }
        }
    }

    public void upgradeWoodWalls()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
         for(int i = 0; i < walls.Length; i++)
        {
            Instantiate(stoneWallPrefab, walls[i].transform.position, walls[i].transform.rotation);
            Destroy(walls[i]);
        }
    }
    public void upgradeStoneWalls()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        for(int i = 0; i < walls.Length; i++)
        {
            Instantiate(metalWallPrefab, walls[i].transform.position, walls[i].transform.rotation);
            Destroy(walls[i]);
        }
        
    }
    public int getWallLevel()
    {
        return wallLevel;
    }
}
