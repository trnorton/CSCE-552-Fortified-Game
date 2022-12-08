using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] warrior;
    public GameObject woodWall;
    public GameObject damagedWoodWall;
    public GameObject destroyedWoodWall;
    public GameObject stoneWall;
    public GameObject damagedStoneWall;
    public GameObject destroyedStoneWall;
    public GameObject metalWall;
    public GameObject damagedMetalWall;
    public GameObject destroyedMetalWall;
    public GameObject weaponcontrol;
    public GameObject waveSpawner;
    public GameObject treasure;
    public GameObject[] wallControl;
    public GameObject turretController;
    public Transform[] spawnPoints;
    private Transform spawn;
    public GameObject warriorPrefab;
    public Transform[] spawnpositions;

    void awake()
    {
        player = GameObject.FindWithTag("Player");
        treasure = GameObject.FindWithTag("Treasure");
        waveSpawner = GameObject.FindWithTag("Spawner");
        // warrior = GameObject.FindGameObjectsWithTag("Friendly");
        // weaponcontrol = GameObject.FindWithTag("weaponcontrol");
        // turretController = GameObject.FindWithTag("TurretManager");
    }
    void Start()
    {
        Load();
    }
    public void Load()
    {
        Debug.Log("Load Save");
        Debug.Log("PRIMARY: " + PlayerPrefs.GetString("PrimaryWeaponSaved").ToString());
        Debug.Log("SECONDARY: " + PlayerPrefs.GetString("SecondaryWeaponSaved").ToString());

        //Load in all of our saved data

        //HP
        var playerHealthComponent = player.GetComponent<Health>();
        if(PlayerPrefs.HasKey("PlayerHealthSaved"))
        {
            playerHealthComponent.currentHealth = PlayerPrefs.GetFloat("PlayerHealthSaved");
        }
        
        var treasureHealthComponent = treasure.GetComponent<Health>();
        if(PlayerPrefs.HasKey("TreasureHealthSaved"))
        {
            treasureHealthComponent.currentHealth = PlayerPrefs.GetFloat("TreasureHealthSaved");
        }

        //Money
        var playerMoneyComponent = player.GetComponent<Money>();
        if(PlayerPrefs.HasKey("PlayerMoneySaved"))
        {
            playerMoneyComponent.money = PlayerPrefs.GetInt("PlayerMoneySaved");
        }

        //Weapons
        var weaponControlComponent = weaponcontrol.GetComponent<WeaponController>();
        if(PlayerPrefs.HasKey("PrimaryWeaponSaved"))
        {
            if(PlayerPrefs.GetString("PrimaryWeaponSaved").ToString() == "sword2 (UnityEngine.GameObject)" && weaponControlComponent.getWeaponByTag("Sword"))
            {
                GameObject Bat = weaponControlComponent.getWeaponByTag("Bat");
                Bat.SetActive(false);
                GameObject Sword = weaponControlComponent.getWeaponByTag("Sword");
                Debug.Log("Made it");
                weaponControlComponent.Prim = Sword;
            }
            else
            {
                GameObject Bat = weaponControlComponent.getWeaponByTag("Bat");
                weaponControlComponent.Prim = Bat;
            }
        }
        if(PlayerPrefs.HasKey("SecondaryWeaponSaved"))
        {
            if(PlayerPrefs.GetString("SecondaryWeaponSaved").ToString() == "slingshot (UnityEngine.GameObject)")
            {
                Debug.Log("Made it 2");
                GameObject Slingshot = weaponControlComponent.getWeaponByTag("Slingshot");
                weaponControlComponent.Secondary = Slingshot;
            }
            else
            {
                GameObject Hands = weaponControlComponent.getWeaponByTag("PH");
                weaponControlComponent.Secondary = Hands;
            }
        }

        //Round Num
        var waveSpawnerComponent = waveSpawner.GetComponent<WaveSpawner>();
        if(PlayerPrefs.HasKey("RoundNumberSaved"))
        {
            waveSpawnerComponent.setRound(PlayerPrefs.GetInt("RoundNumberSaved"));
        }

        //Number of warriors
        if(PlayerPrefs.HasKey("NumberFriendsSaved"))
        {
            
            for(int i = 0; i < PlayerPrefs.GetInt("NumberFriendsSaved");i++)
            {
                spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(warriorPrefab, spawn.transform.position, spawn.transform.rotation);
            }
            warrior = GameObject.FindGameObjectsWithTag("Friendly");
            for(int i = 0; i < PlayerPrefs.GetInt("NumberFriendsSaved");i++)
            {
                Debug.Log(PlayerPrefs.GetFloat("Warrior" +i +" HP"));
                var warriorHealth = warrior[i].GetComponent<Health>();
                warriorHealth.currentHealth = PlayerPrefs.GetFloat("Warrior" +i +" HP");
                Debug.Log(warriorHealth.currentHealth);
            }
        }

        //Wall Level
        wallControl = GameObject.FindGameObjectsWithTag("Wall");
        for(int i = 0; i < wallControl.Length; i++)
        {
            string wallType = PlayerPrefs.GetString("Wall[" + i + "]save");
            // Debug.Log(wallType);
            if(wallType == woodWall.name+"(Clone)") {
                Instantiate(woodWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == damagedWoodWall.name+"(Clone)") {
                Instantiate(damagedWoodWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == destroyedWoodWall.name+"(Clone)") {
                Instantiate(destroyedWoodWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == stoneWall.name+"(Clone)") {
                Instantiate(stoneWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == damagedStoneWall.name+"(Clone)") {
                Instantiate(damagedStoneWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == destroyedStoneWall.name+"(Clone)") {
                Instantiate(destroyedStoneWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == metalWall.name+"(Clone)") {
                Instantiate(metalWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == damagedMetalWall.name+"(Clone)") {
                Instantiate(damagedMetalWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }
            if(wallType == destroyedMetalWall.name+"(Clone)") {
                Instantiate(destroyedMetalWall, wallControl[i].transform.position, wallControl[i].transform.rotation);
                Destroy(wallControl[i]);
            }

        }


        // if(PlayerPrefs.HasKey("WallLevelSaved"))
        // {
        //     if(PlayerPrefs.GetInt("WallLevelSaved") == 1)
        //     {
        //         for(int i = 0; i < wallControl.Length; i++)
        //         {
        //             Instantiate(stoneWallPrefab, wallControl[i].transform.position, wallControl[i].transform.rotation);
        //             Destroy(wallControl[i]);
        //         }
        //     }
        //     if(PlayerPrefs.GetInt("WallLevelSaved") == 2)
        //     {
        //         for(int i = 0; i < wallControl.Length; i++)
        //         {
        //             Instantiate(metalWallPrefab, wallControl[i].transform.position, wallControl[i].transform.rotation);
        //             Destroy(wallControl[i]);
        //         }
        //     }
        // }

        //Turrets
        // var turretComponent = turretController.GetComponent<TurretHandler>();
        // if(PlayerPrefs.HasKey("TurretActiveSaved"))
        // {
        //     if(PlayerPrefs.GetInt("TurretActiveSaved") == 1)
        //     {
        //         turretComponent.deployTurrets();
        //     }
        // }
    }
}

