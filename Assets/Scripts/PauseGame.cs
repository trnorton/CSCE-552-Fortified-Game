using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    //public MusicController MC;
    public GameObject PauseUI;
    private bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
      PauseUI = GameObject.Find("PauseUI");
      PauseUI.SetActive(false);
      Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape) && !paused)
      {
          Time.timeScale = 0;
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          PauseUI.SetActive(true);
          Debug.Log("Game Paused");
          paused = !paused;
          //MC.pauseMusic();
      }
      else if (Input.GetKeyDown(KeyCode.Escape) && paused)
      {
          Time.timeScale = 1;
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          PauseUI.SetActive(false);
          Debug.Log("Game Resumed");
          paused = !paused;
          //MC.playMusic();
      }
    }
}
