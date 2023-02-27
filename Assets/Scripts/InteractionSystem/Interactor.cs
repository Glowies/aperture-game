using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public RectTransform InteractIndicator;

    private CharacterController _controller;
    private List<Interactable> _closeInteractables;
    private Interactable _targetInteractable;

    void Awake()
    {
        TryGetComponent(out _controller);
        _closeInteractables = new();
        _targetInteractable = null;
    }

    void Update()
    {
        FindClosestInteractable();
    }

    void LateUpdate()
    {
        UpdateIndicatorPosition();
    }

    private void FindClosestInteractable()
    {
        if(_closeInteractables.Count == 0)
        {
            return;
        }

        var prevTarget = _targetInteractable;

        var minDistance = float.PositiveInfinity;
        foreach(var interactable in _closeInteractables)
        {
            var position = interactable.transform.position;
            var currDistance = Vector3.Distance(transform.position, position);
            if(currDistance < minDistance)
            {
                minDistance = currDistance;
                _targetInteractable = interactable;
            }
        }

        // Update highlights if new target
        if(prevTarget != _targetInteractable)
        {
            prevTarget?.ToggleHighlight(false);
            _targetInteractable.ToggleHighlight(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only register entry if it's an interactable
        if(!other.TryGetComponent(out Interactable interactable))
        {
            return;
        }
        
        _closeInteractables.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        // Only register exit if it's an interactable
        if(!other.TryGetComponent(out Interactable interactable))
        {
            return;
        }
        
        interactable.ToggleHighlight(false);
        _targetInteractable = null;
        _closeInteractables.Remove(interactable);
    }

    public void OnInteract()
    {
        if(_targetInteractable == null)
        {
            return;
        }

        _targetInteractable.Interact(_controller);
    }

    private void UpdateIndicatorPosition()
    {
        var screenPosition = Vector3.one * -999;

        if(_targetInteractable != null)
        {
            var position = _targetInteractable.transform.position;
            position += _targetInteractable.IndicatorOffset;
            screenPosition = Camera.main.WorldToScreenPoint(position);
        }

        InteractIndicator.position = screenPosition;
    }
}
