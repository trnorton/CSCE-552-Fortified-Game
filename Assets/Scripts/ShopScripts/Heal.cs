using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject player;
    public GameObject treasure;
    public int healCost = 5;
    private int money;
    private float pHealth;
    private float tHealth;
    public AudioSource bought;
    public AudioSource notEnoughCash;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void healPlayer() {
      var playercash = player.GetComponent<Money>();
      money = playercash.MoneyToInt();
      if(money >= 5){
          var PlayerHealth = player.GetComponent<Health>();
          pHealth = PlayerHealth.HealthtoInt();
          if(pHealth < PlayerHealth.MaxHealthtoInt()){
            PlayerHealth.HealToMax();
            playercash.SubMoney(5);
            bought.Play();
          }
          else {
            Debug.Log("Already at max health");
          }
      } else {
          Debug.Log("Not enough (get a job)");
          notEnoughCash.Play();
      }
    }

    public void healTreasure() {
      var playercash = player.GetComponent<Money>();
      money = playercash.MoneyToInt();
      if(money >= 5){
          var TreasureHealth = treasure.GetComponent<Health>();
          tHealth = TreasureHealth.HealthtoInt();
          if(tHealth < TreasureHealth.MaxHealthtoInt()){
            TreasureHealth.HealToMax();
            playercash.SubMoney(5);
            bought.Play();
          }
          else {
            Debug.Log("Already at max health");
          }
      } else {
          Debug.Log("Not enough (get a job)");
          notEnoughCash.Play();
      }
    }
}
