using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;
    public GameObject weaponCont;
    WeaponController WeapCont;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buySword(){
        WeapCont = FindObjectOfType<WeaponController>();
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= 10){
            GameObject currPrim = WeapCont.getPrim();
            if(currPrim.activeSelf == true){
                currPrim.SetActive(false);
                WeapCont.upgradePrim("Sword");
                GameObject newPrim = WeapCont.getPrim();
                newPrim.SetActive(true);
            } else {
                WeapCont.upgradePrim("Sword");
                GameObject newPrim = WeapCont.getPrim();
            }
            
            playercash.SubMoney(10);
        } else {
            Debug.Log("Not enough (get a job)");
        }
    }

        public void buySlingShot(){
        WeapCont = FindObjectOfType<WeaponController>();
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= 10){
            GameObject currSec = WeapCont.getSec();
            if(currSec.activeSelf == true){
                currSec.SetActive(false);
                WeapCont.upgradeSec("Slingshot");
                GameObject newSec = WeapCont.getSec();
                newSec.SetActive(true);
            } else {
                WeapCont.upgradeSec("Slingshot");
                GameObject newSec = WeapCont.getSec();
            }
            playercash.SubMoney(10);
        } else {
            Debug.Log("Not enough (get a job)");
        }
    }
}
