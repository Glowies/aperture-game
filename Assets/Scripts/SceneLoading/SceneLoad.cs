using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 0f;
    public void TriggerLoadNextLevel(){
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    
    public void OpenLevel(int levelInt){
        //make mouse invisible upon loading 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(levelInt);
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        PauseMenu pause = gameObject.GetComponent<PauseMenu>();
        pause.Resume();
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
