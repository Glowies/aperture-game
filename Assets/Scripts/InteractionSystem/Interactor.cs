using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private CharacterController _controller;
    private List<Interactable> _closeInteractables;

    void Awake()
    {
        TryGetComponent(out _controller);
        _closeInteractables = new();
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
        
        _closeInteractables.Remove(interactable);
    }

    public void OnInteract()
    {
        foreach(var interactable in _closeInteractables)
        {
            interactable.Interact(_controller);
        }
    }
}
