using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public GameObject player;
    WeaponController weaponcontrol;
    public GameObject waveSpawner;
    public GameObject treasure;
    buyUpgradedWall wallControl;
    TurretHandler turretController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        treasure = GameObject.FindWithTag("Treasure");
        waveSpawner = GameObject.FindWithTag("Spawner");
        weaponcontrol = FindObjectOfType<WeaponController>();
        wallControl = FindObjectOfType<buyUpgradedWall>();
        turretController = FindObjectOfType<TurretHandler>();
    }

    public void Load()
    {
        SceneManager.LoadScene("SampleScene");

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
            playerMoneyComponent.AddMoney(PlayerPrefs.GetInt("PlayerMoneySaved"));
        }

        //Weapons
        if(PlayerPrefs.HasKey("PrimaryWeaponSaved"))
        {
            weaponcontrol.upgradePrim(PlayerPrefs.GetString("PrimaryWeaponSaved"));
        }

        if(PlayerPrefs.HasKey("SecondaryWeaponSaved"))
        {
            weaponcontrol.upgradeSec(PlayerPrefs.GetString("SecondaryWeaponSaved"));
        }

        //Round Num
        var waveSpawnerComponent = waveSpawner.GetComponent<WaveSpawner>();
        if(PlayerPrefs.HasKey("RoundNumberSaved"))
        {
            waveSpawnerComponent.roundNumber = PlayerPrefs.GetInt("RoundNumberSaved");
        }

        //Wall Level
        if(PlayerPrefs.HasKey("WallLevelSaved"))
        {
            if(PlayerPrefs.GetInt("WallLevelSaved") == 1)
            {
                wallControl.upgradeWoodWalls();
            }
            if(PlayerPrefs.GetInt("WallLevelSaved") == 2)
            {
                wallControl.upgradeStoneWalls();
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
