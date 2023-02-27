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

    private int _startLayer;
    private int _outlineLayer;
    private Collider _collider;
    private Interactor _interactor;

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

    public void Interact(CharacterController controller, Interactor interactor)
    {
        _interactor = interactor;
        OnInteract.Invoke(controller);
    }

    void OnDisable()
    {
        _interactor?.RemoveInteractable(this);
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
