using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Interactor : MonoBehaviour
{
    public RectTransform InteractIndicator;

    private CharacterController _controller;
    private List<Interactable> _closeInteractables;
    private Interactable _targetInteractable;
    private CharacterController _characterController;

    void Awake()
    {
        TryGetComponent(out _controller);
        TryGetComponent(out _characterController);
        _closeInteractables = new();
        _targetInteractable = null;
    }

    void Update()
    {
        FindInteractablesInRange();
        FindClosestInteractable();
    }

    void LateUpdate()
    {
        UpdateIndicatorPosition();
    }

    private void FindInteractablesInRange()
    {
        var offset = _characterController.height / 2f;
        var center = _characterController.center + transform.position;
        var capsuleBottom = center + Vector3.down * offset;
        var capsuleTop = center + Vector3.up * offset;
        var capsuleRadius = _characterController.radius;

        // Find All Colliders That Hit Capsule
        var colliders = Physics.OverlapCapsule(
            capsuleBottom, 
            capsuleTop, 
            capsuleRadius,
            Physics.AllLayers,
            QueryTriggerInteraction.Collide);

        // Update Close Interactables
        _closeInteractables.Clear();
        foreach(var collider in colliders)
        {
            if(collider.TryGetComponent(out Interactable interactable))
            {
                _closeInteractables.Add(interactable);
            }
        }
    }

    private void FindClosestInteractable()
    {
        var prevTarget = _targetInteractable;

        if(_closeInteractables.Count == 0)
        {
            prevTarget?.ToggleHighlight(false);
            _targetInteractable = null;
            return;
        }

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
