using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour
{
    private GameObject[] turrets;
    // Start is called before the first frame update
    void Start()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        disableTurrets();
        // SetActive(false);
    }

    public void disableTurrets()
    {
        foreach(GameObject t in turrets)
        {
            t.SetActive(false);
        }
    }

    public void deployTurrets()
    {
        foreach(GameObject t in turrets)
        {
            t.SetActive(true);
        }
    }
}
