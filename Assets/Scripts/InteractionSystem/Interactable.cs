using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public UnityEvent<CharacterController> OnInteract;
    public GameObject HighlightTarget;
    public Vector3 IndicatorOffset;
    public AudioSource PickUpChime;

    private int _startLayer;
    private int _outlineLayer;
    private Collider _collider;

    void Awake()
    {
        TryGetComponent(out _collider);
        FindIndicatorOffset();

        if(HighlightTarget != null)
        {
            _startLayer = HighlightTarget.layer;
        }
        
        _outlineLayer = LayerMask.NameToLayer("Outlined");
    }

    public void Interact(CharacterController controller)
    {
        if (PickUpChime != null)
        {
            PickUpChime.Play();
        }
        
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

    private void FindIndicatorOffset()
    {
        IndicatorOffset = Vector3.up * _collider.bounds.extents.y;
    }
}
