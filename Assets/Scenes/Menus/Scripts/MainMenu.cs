using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject mainMenuUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
    	Time.timeScale = 1.0f;
    	SceneManager.LoadScene("SimiScene");
    }

    public void LoadSettingsMenu()
    {
    	Debug.Log("Loading settings menu...");
    }

    public void QuitGame()
    {
    	Debug.Log("Quitting game...");
    	Application.Quit();
    }
}
