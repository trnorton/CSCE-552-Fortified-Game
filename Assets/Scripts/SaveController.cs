using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public GameObject player;
    WeaponController weaponcontrol;
    public GameObject waveSpawner;
    public GameObject treasure;
    public GameObject wallControl;
    TurretHandler turretController;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        treasure = GameObject.FindWithTag("Treasure");
        waveSpawner = GameObject.FindWithTag("Spawner");
        weaponcontrol = FindObjectOfType<WeaponController>();
        wallControl = GameObject.FindWithTag("Shop");
        turretController = FindObjectOfType<TurretHandler>();
    }

    public void OnClickSave()
    {
        Debug.Log("Game Saved");
        //Save Player's HP
        var playerHealthComponent = player.GetComponent<Health>();
        PlayerPrefs.SetFloat("PlayerHealthSaved", playerHealthComponent.currentHealth);

        //Save Player's Money
        var playerMoneyComponent = player.GetComponent<Money>();
        PlayerPrefs.SetInt("PlayerMoneySaved", playerMoneyComponent.MoneyToInt());

        //Save Treasure's HP
        var treasureHealthComponent = treasure.GetComponent<Health>();
        PlayerPrefs.SetFloat("TreasureHealthSaved", treasureHealthComponent.currentHealth);

        //Save Weapons Bought
        PlayerPrefs.SetString("PrimaryWeaponSaved", weaponcontrol.getPrim().ToString());
        PlayerPrefs.SetString("SecondaryWeaponSaved", weaponcontrol.getSec().ToString());

        //Save Current Round
        var waveSpawnerComponent = waveSpawner.GetComponent<WaveSpawner>();
        PlayerPrefs.SetInt("RoundNumberSaved", waveSpawnerComponent.roundNumber);

        //Save Wall Level
        // var wallControlComponent = wallControl.GetComponent<BuyUpgradedWall>();
        // PlayerPrefs.SetInt("WallLevelSaved", wallControlComponent.getWallLevel());

        //Save Turret Activity
        PlayerPrefs.SetInt("TurretActiveSaved", turretController.boolToInt());

        PlayerPrefs.Save();
    }

}
