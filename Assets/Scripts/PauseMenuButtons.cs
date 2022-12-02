using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickRestart()
    {
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SceneManager.LoadScene("SampleScene");
    }
    public void OnClickQuit()
    {
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      SceneManager.LoadScene("MainMenu");
    }
}
