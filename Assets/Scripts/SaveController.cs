using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveController : MonoBehaviour
{
    public GameObject player;
    WeaponController weaponcontrol;
    public GameObject waveSpawner;
    public GameObject treasure;
    public GameObject[] wallControl;
    public GameObject[] warrior;
    public GameObject turretController;
    public GameObject woodWall;
    public GameObject damagedWoodWall;
    public GameObject destroyedWoodWall;
    public GameObject stoneWall;
    public GameObject damagedStoneWall;
    public GameObject destroyedStoneWall;
    public GameObject metalWall;
    public GameObject damagedMetalWall;
    public GameObject destroyedMetalWall;
    private GameObject[] FriendliesControl;
    private int numFriends;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        treasure = GameObject.FindWithTag("Treasure");
        waveSpawner = GameObject.FindWithTag("Spawner");
        weaponcontrol = FindObjectOfType<WeaponController>();
        turretController = GameObject.FindWithTag("TurretManager");
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
        wallControl = FindGameObjectsWithTags(new string[]{"Wall", "destroyedWall"});
        // var wallControlComponent = wallControl[0].GetComponent<Health>();
        // if((wallControlComponent.maxHealth == 3 && wallControlComponent.destructionLevel == 0) || (wallControlComponent.maxHealth == 1 && wallControlComponent.destructionLevel == 1))
        //     PlayerPrefs.SetInt("WallLevelSaved", 0);
        // if((wallControlComponent.maxHealth == 10 && wallControlComponent.destructionLevel == 0) || (wallControlComponent.maxHealth == 5 && wallControlComponent.destructionLevel == 1))
        //     PlayerPrefs.SetInt("WallLevelSaved", 1);
        // if((wallControlComponent.maxHealth == 20 && wallControlComponent.destructionLevel == 0) || (wallControlComponent.maxHealth == 10 && wallControlComponent.destructionLevel == 1))
        //     PlayerPrefs.SetInt("WallLevelSaved", 2);
        for(int i = 0; i < wallControl.Length; i++)
        {
            string nameString = wallControl[i].ToString();
            string sub = nameString.Substring(0,nameString.IndexOf(' '));
            PlayerPrefs.SetString("Wall[" + i + "]save", sub);
            Debug.Log(PlayerPrefs.GetString("Wall[" + i + "]save"));
        }

        //Save Turret Activity
        if(turretController.GetComponent<LoadedTurret>())
        {
            var turretComponent = turretController.GetComponent<LoadedTurret>();
            PlayerPrefs.SetInt("TurretActiveSaved", turretComponent.boolToInt());
        }
        else
        {
            var turretComponent = turretController.GetComponent<TurretHandler>();
            PlayerPrefs.SetInt("TurretActiveSaved", turretComponent.boolToInt());
        }
        
        FriendliesControl = FindGameObjectsWithTags(new string[]{"Friendly"});
        numFriends = FriendliesControl.Length;
        PlayerPrefs.SetInt("NumberFriendsSaved", numFriends);

        for(int i = 0; i < numFriends; i++)
        {
            var warriorHealth = FriendliesControl[i].GetComponent<Health>();
            PlayerPrefs.SetFloat("Warrior" +i +" HP", warriorHealth.currentHealth);
        }

        PlayerPrefs.Save();
    }

    GameObject[] FindGameObjectsWithTags(params string[] tags)
    {
         var all = new List<GameObject>() ;
         
         foreach(string tag in tags)
         {
             var temp = GameObject.FindGameObjectsWithTag(tag).ToList() ;
             all = all.Concat(temp).ToList() ;
         }
         
         return all.ToArray() ;
     }

}
