using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadHome()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
}
