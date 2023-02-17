using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    public void LoadScene(string sceneName) => SceneLoader.Instance.LoadScene(sceneName);
}
