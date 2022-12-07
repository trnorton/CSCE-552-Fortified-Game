using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public GameObject player;
    public GameObject stoneWallPrefab;
    public GameObject metalWallPrefab;
    public GameObject weaponcontrol;
    public GameObject waveSpawner;
    public GameObject treasure;
    public GameObject[] wallControl;
    TurretHandler turretController;

    void awake()
    {
        player = GameObject.FindWithTag("Player");
        treasure = GameObject.FindWithTag("Treasure");
        waveSpawner = GameObject.FindWithTag("Spawner");
        // weaponcontrol = GameObject.FindWithTag("weaponcontrol");
        turretController = FindObjectOfType<TurretHandler>();
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

        //Wall Level
        wallControl = GameObject.FindGameObjectsWithTag("Wall");
        if(PlayerPrefs.HasKey("WallLevelSaved"))
        {
            if(PlayerPrefs.GetInt("WallLevelSaved") == 1)
            {
                for(int i = 0; i < wallControl.Length; i++)
                {
                    Instantiate(stoneWallPrefab, wallControl[i].transform.position, wallControl[i].transform.rotation);
                    Destroy(wallControl[i]);
                }
            }
            if(PlayerPrefs.GetInt("WallLevelSaved") == 2)
            {
                for(int i = 0; i < wallControl.Length; i++)
                {
                    Instantiate(metalWallPrefab, wallControl[i].transform.position, wallControl[i].transform.rotation);
                    Destroy(wallControl[i]);
                }
            }
        }

        //Turrets
        if(PlayerPrefs.HasKey("TurretActiveSaved"))
        {
            if(PlayerPrefs.GetInt("TurretActiveSaved") == 1)
            {
                turretController.deployTurrets();
            }
        }
    }
}

