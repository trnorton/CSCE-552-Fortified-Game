using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon : MonoBehaviour
{
    public GameObject Prim; 
    public GameObject Secondary;
    public GameObject weaponcontrol;
    public GameObject[] Weapons;
    // Start is called before the first frame update
    void Start()
    {
        var weaponControlComponent = weaponcontrol.GetComponent<WeaponController>();
        GameObject Bat = weaponControlComponent.getWeaponByTag("Bat");
        GameObject Hands = weaponControlComponent.getWeaponByTag("PH");
        Bat.SetActive(true);
        weaponControlComponent.Prim = Bat;
        weaponControlComponent.Secondary = Hands;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
