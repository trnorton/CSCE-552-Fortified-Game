using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
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
    public void OnClickStart()
    {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickQuit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit(0);
    }
    
    public void OnClickAbout()
    {
        SceneManager.LoadScene("AboutMenu");
    }
    public void OnClickHowTo()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void OnClickLoad()
    {
      if(PlayerPrefs.HasKey("PlayerHealthSaved"))
      {
        SceneManager.LoadScene("LoadedSave");
      }
    }
}
