using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject player;

    private int moneyAmount;

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = 0; 
        score.text = "$: " + moneyAmount;
    }

    // Update is called once per frame
    void Update()
    {
        var playerCash = player.GetComponent<Money>();
        moneyAmount = playerCash.MoneyToInt();
        score.text = "$: " + moneyAmount;
    }
}
