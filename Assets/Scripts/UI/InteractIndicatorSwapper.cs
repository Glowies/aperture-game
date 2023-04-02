using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIndicatorSwapper : MonoBehaviour
{
    public GameObject InteractIndicator;
    public GameObject GrabIndicator;

    public void SwapToIndicator(InteractionType type)
    {
        bool isGrab = type == InteractionType.Grab;
        InteractIndicator.SetActive(!isGrab);
        GrabIndicator.SetActive(isGrab);
    }
}
