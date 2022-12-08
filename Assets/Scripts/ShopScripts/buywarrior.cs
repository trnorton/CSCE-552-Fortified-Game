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
    public GameObject spawn;
    private int warriorcount;
    public TextMeshProUGUI buyWarriorText;
    public int maxWarriors;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    public void onClickBuyWarriors()
    {
        var playercash = player.GetComponent<Money>();
        money = playercash.MoneyToInt();
        if(money >= warriorcost && warriorcount < maxWarriors)
        {
            playercash.SubMoney(warriorcost);
            
            Instantiate(warriorprefab, spawn.transform.position, spawn.transform.rotation);
            
            warriorcount++;
        }
        if(warriorcount >= maxWarriors)
        {
            buyWarriorText.text = "Max Warriors";
        }
    }
}
