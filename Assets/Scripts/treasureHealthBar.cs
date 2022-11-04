using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treasureHealthBar : MonoBehaviour
{
    public Slider treasureHealth;
    public GameObject treasure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var treasureHealthValue = treasure.GetComponent<Health>();
        treasureHealth.value = treasureHealthValue.HealthtoInt();
    }
}
