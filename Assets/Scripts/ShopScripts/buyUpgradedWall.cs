using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyUpgradedWall : MonoBehaviour
{
    public GameObject player;
    public GameObject stoneWallPrefab;
    public int StoneWallCost;
    private int money;
    private GameObject[] walls;
    private int wallLevel;
    // Start is called before the first frame update
    void Start()
    {
        wallLevel = 0;
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

            }
            else
            {
                Debug.Log("Not enough funds");
            }
        }
    }

    void upgradeWoodWalls()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
         for(int i = 0; i < walls.Length; i++)
        {
            Instantiate(stoneWallPrefab, walls[i].transform.position, walls[i].transform.rotation);
            Destroy(walls[i]);
        }
    }
    public int getWallLevel()
    {
        return wallLevel;
    }
}
