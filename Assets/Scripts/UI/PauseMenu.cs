using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;

    void OnPause(){
        if (isPaused){
            Resume();
        }
        else{
            Pause();
        }

    }
    private void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void Resume(){
        Debug.Log("resume");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadControls(){
        Debug.Log("Loading Controls Menu");
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }
    public void LoadPause(){
        pauseMenuUI.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
