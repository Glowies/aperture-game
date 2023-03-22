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
    public GameObject gameUI;

    void OnPause(){
        if (isPaused){
            Resume();
        }
        else{
            Pause();
        }

    }
    private void Pause(){
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        //disable the controller so you cannot move player or controller when paused
        gameObject.GetComponent<ThirdPersonController>().enabled = false;
        //stop time in game
        Time.timeScale = 0f;
        isPaused = true;
        //unlock cursor so it is unconfined and make cursor visible, from gamedev.tv course
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        //enable controls for third person controller
        gameObject.GetComponent<ThirdPersonController>().enabled = true;
        //resume time in game
        Time.timeScale = 1f;
        isPaused = false;
        // make cursur invisible and re confine to center of screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadControls(){
        //set pause menu to false and enable controls canvas
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }
    public void LoadPause(){
        //disable controls canvas and restore pause menu
        controlsMenuUI.SetActive(false);
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