using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApertureAnimator : MonoBehaviour
{
    public float OpenHeight = 3;
    public float ClosedHeight = 0;

    public float ShutterDuration = .5f;
    public float ClosedPauseDuration = .2f;

    public List<Transform> PanelParents;
    
    [ContextMenu("Play Shutter Animation")]
    public void PlayShutterAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(ShutterAnimation());
    }

    private IEnumerator ShutterAnimation()
    {
        float timeScale = 1f / ShutterDuration;

        // Close Animation
        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime * timeScale;

            float height = Mathf.SmoothStep(OpenHeight, ClosedHeight, time);
            SetPanelHeights(height);

            yield return new WaitForEndOfFrame();
        }
        
        // Pause While Closed
        yield return new WaitForSeconds(ClosedPauseDuration);
        
        // Open Animation
        time = 0;
        while(time < 1)
        {
            time += Time.deltaTime * timeScale;

            float height = Mathf.SmoothStep(ClosedHeight, OpenHeight, time);
            SetPanelHeights(height);

            yield return new WaitForEndOfFrame();
        }
    }

    private void SetPanelHeights(float height)
    {
        foreach(var panel in PanelParents)
        {
            var oldPos = panel.localPosition;
            panel.localPosition = new Vector3(oldPos.x, height, oldPos.z);
        }
    }
}
