using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    public Slider health;
    public GameObject Player;
 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var PlayerHealth = Player.GetComponent<Health>();
        health.value = PlayerHealth.HealthtoInt();
    }
}
