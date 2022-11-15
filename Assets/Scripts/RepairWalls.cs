using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairWalls : MonoBehaviour
{
    public GameObject player;
    public GameObject ShopHUD;
    public GameObject stoneWallPrefab;
    public GameObject woodenWallPrefab;
    public int repairCost;
    private int money;
    private GameObject[] walls;
    private GameObject[] destroyedWalls;
    private int wallLevel;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onButtonPress()
    {
        var HUD = ShopHUD.GetComponent<buyUpgradedWall>();
        wallLevel = HUD.getWallLevel();
        Debug.Log("Button Pressed");
        //Getting Player cash
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();

        //if the walls are wooden
        if(wallLevel == 0)
        {
            if(money >= repairCost && GameObject.FindGameObjectsWithTag("destroyedWall").Length != 0)
            {
                playercash.SubMoney(repairCost);
                repairWoodWalls();

            }
            else
            {
                Debug.Log("Not enough funds");
            }
        }
        if(wallLevel == 1 && GameObject.FindGameObjectsWithTag("destroyedWall").Length != 0)
        {
            if(money >= repairCost)
            {
                playercash.SubMoney(repairCost);
                repairStoneWalls();
            }
            else
            {
                Debug.Log("Not enough funds");
            }
        }
    }

    void repairWoodWalls()
    {
        destroyedWalls = GameObject.FindGameObjectsWithTag("destroyedWall");
        walls = GameObject.FindGameObjectsWithTag("Wall");
         for(int i = 0; i < walls.Length; i++)
        {
            Instantiate(woodenWallPrefab, walls[i].transform.position, walls[i].transform.rotation);
            Destroy(walls[i]);
        }
        for(int j = 0; j < destroyedWalls.Length; j++)
        {
          Instantiate(woodenWallPrefab, destroyedWalls[j].transform.position, destroyedWalls[j].transform.rotation);
          Destroy(destroyedWalls[j]);

        }
    }
    void repairStoneWalls()
    {
        destroyedWalls = GameObject.FindGameObjectsWithTag("destroyedWall");
        walls = GameObject.FindGameObjectsWithTag("Wall");
         for(int i = 0; i < walls.Length; i++)
        {
            Instantiate(stoneWallPrefab, walls[i].transform.position, walls[i].transform.rotation);
            Destroy(walls[i]);

        }
        for(int j = 0; j < destroyedWalls.Length; j++)
        {
          Instantiate(stoneWallPrefab, destroyedWalls[j].transform.position, destroyedWalls[j].transform.rotation);
          Destroy(destroyedWalls[j]);
        }
    }
}
