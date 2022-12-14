using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buywarrior : MonoBehaviour
{
    public GameObject player;
    public int warriorcost;
    private int money;
    private GameObject[] warriors;
    public GameObject warriorprefab;
    private Transform spawn;
    public Transform[] spawnpositions;
    public int warriorcount;
    public TextMeshProUGUI buyWarriorText;
    public int maxWarriors;
    public AudioSource bought;
    public AudioSource notEnoughCash;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }
    void Update()
    {
        if(warriorcount < 6)
        {
            buyWarriorText.text = "Buy Warrior $50";
        
        }
    }
    // Update is called once per frame
    public void onClickBuyWarriors()
    {
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= warriorcost && warriorcount < maxWarriors)
        {
            playercash.SubMoney(warriorcost);
            spawn = spawnpositions[Random.Range(0, spawnpositions.Length)];
            Instantiate(warriorprefab, spawn.transform.position, spawn.transform.rotation);
            bought.Play();
            warriorcount++;
        } else if(money < warriorcost){
            Debug.Log("Not enough cash");
            notEnoughCash.Play();
        }
        if(warriorcount >= maxWarriors)
        {
            buyWarriorText.text = "Max Warriors";
            notEnoughCash.Play();
        }
        
    }
}
