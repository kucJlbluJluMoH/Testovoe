using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public SaveManager saveManager;
    void Start()
    {
        
    }
    
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClearSaves()
    {
        PlayerPrefs.DeleteAll();
        print("Cleared");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        saveManager.SaveData();
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else
            {
                Continue();
            }
        }
    }
}
