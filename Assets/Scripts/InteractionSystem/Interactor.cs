using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Interactor : MonoBehaviour
{
    public RectTransform InteractIndicator;

    public bool tooClose = false;
    private CharacterController _controller;
    private List<Interactable> _closeInteractables;
    private Interactable _targetInteractable;
    private CharacterController _characterController;
    private InteractIndicatorSwapper _indicatorSwapper;

    public AudioSource PickUpSource;
    
    void Awake()
    {
        TryGetComponent(out _controller);
        TryGetComponent(out _characterController);
        InteractIndicator.TryGetComponent(out _indicatorSwapper);
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
            // set tooClose to false if not in range of interactable (targetInteractable is null)
            tooClose = false;
            return;
        }

        var minDistance = float.PositiveInfinity;
        foreach(var interactable in _closeInteractables)
        {
            if(!interactable.isActiveAndEnabled)
            {
                continue;
            }
            
            var position = interactable.transform.position;
            var currDistance = Vector3.Distance(transform.position, position);
            if(currDistance < minDistance)
            {
                minDistance = currDistance;
                _targetInteractable = interactable;
                //if the target interactable has a parent with Plant or DoorRegion script component, tooClose = true
                if (_targetInteractable.transform.parent.TryGetComponent(out Plant plant) || _targetInteractable.transform.parent.TryGetComponent(out DoorRegion door)){
                    tooClose = true;
                }
                
            }
        }

        // Update highlights if new target
        if(prevTarget != _targetInteractable)
        {
            prevTarget?.ToggleHighlight(false);
            _targetInteractable.ToggleHighlight(true);
        }
    }

    void OnThrow() => TriggerInteraction(InteractionType.Grab);

    public void OnInteract() => TriggerInteraction(InteractionType.Interact);

    public void TriggerInteraction(InteractionType interactionType)
    {
        if(_targetInteractable == null || 
            _targetInteractable.Interaction != interactionType)
        {
            return;
        }
        if (PauseMenu.isPaused){return;} //if game paused, do not interact

        PickUpSource.Play();
        
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
            _indicatorSwapper.SwapToIndicator(_targetInteractable.Interaction);
        }

        InteractIndicator.position = screenPosition;
    }
}
