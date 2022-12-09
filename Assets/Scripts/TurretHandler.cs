using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurretHandler : MonoBehaviour
{
    private GameObject[] turrets;
    // Start is called before the first frame update
    void Start()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");

        if(PlayerPrefs.HasKey("TurretActiveSaved") && SceneManager.GetActiveScene().name == "LoadedSave")
        {
            if(PlayerPrefs.GetInt("TurretActiveSaved") == 1)
                deployTurrets();
            else
                disableTurrets();
        }
        else
        {
            disableTurrets();
        }
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

    public bool isTurretsActive(){
      foreach(GameObject t in turrets)
      {
          if(t.activeSelf == false){
            return false;
          }
      }
      return true;
    }

    public int boolToInt(){
        if(isTurretsActive())
            return 1;
        else
            return 0;
    }
}
