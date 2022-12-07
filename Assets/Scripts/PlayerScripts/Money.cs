using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int money = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int value)
    {
        money += value;

    }
    public void SubMoney(int value)
    {
        money -= value;
    }
    public int MoneyToInt()
    {
        return money;
    }
}
