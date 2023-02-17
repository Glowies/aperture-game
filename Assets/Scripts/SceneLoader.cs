using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingImage;

    private float _fillTarget;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        _loadingImage.fillAmount = 0;
        _fillTarget = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loadingCanvas.SetActive(true);

        // Update progress bar as the scene loads
        do
        {
            _fillTarget = scene.progress;
            UpdateProgressBar();
            yield return new WaitForEndOfFrame();
        } while(scene.progress < .9f);

        // Let the progress bar catch up if load was too fast
        _fillTarget = 1f;
        yield return FillBarForSeconds(1f);

        scene.allowSceneActivation = true;
        _loadingCanvas.SetActive(false);

        StopAllCoroutines();
    }

    IEnumerator FillBarForSeconds(float seconds)
    {
        float time = 0;
        while(time < seconds)
        {
            time += Time.deltaTime;

            UpdateProgressBar();

            yield return new WaitForEndOfFrame();
        }
    }

    private void UpdateProgressBar()
    {
        var currFill = _loadingImage.fillAmount;
        var fill = Mathf.MoveTowards(currFill, _fillTarget, Time.deltaTime);
        _loadingImage.fillAmount = fill;
    }
}
