using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<CharacterController> OnInteract;
    public GameObject HighlightTarget;

    private int _startLayer;
    private int _outlineLayer;

    void Awake()
    {
        if(HighlightTarget != null)
        {
            _startLayer = HighlightTarget.layer;
        }
        
        _outlineLayer = LayerMask.NameToLayer("Outlined");
    }

    public void Interact(CharacterController controller)
    {
        OnInteract.Invoke(controller);
    }

    public void ToggleHighlight(bool value)
    {
        if(HighlightTarget == null)
        {
            return;
        }

        var layer = value ? _outlineLayer : _startLayer;
        SetLayerRecursively(HighlightTarget, layer);
    }

    private void SetLayerRecursively(GameObject target, int layer)
    {
        target.layer = layer;

        foreach(Transform child in target.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
