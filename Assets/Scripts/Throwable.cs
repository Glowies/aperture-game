using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    public bool IsGrabbed;
    public float Height;

    private Rigidbody _rigidbody;
    private RigidbodyConstraints _startConstraints;
    private Interactable[] _interactables;

    void Awake()
    {
        TryGetComponent(out _rigidbody);
        _startConstraints = _rigidbody.constraints;
        _interactables = GetComponentsInChildren<Interactable>();

        if(TryGetComponent(out Collider collider))
        {
            Height = collider.bounds.size.y;
        }
        else
        {
            Height = 1;
        }
    }

    public void Grab()
    {
        IsGrabbed = true;
        // _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.useGravity = false;
        _rigidbody.drag = 5;
        ToggleInteractables(false);
    }

    public void Throw(Vector3 force)
    {
        IsGrabbed = false;
        // _rigidbody.constraints = _startConstraints;
        _rigidbody.useGravity = true;
        _rigidbody.drag = 0;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        ToggleInteractables(true);
    }

    public void OnInteract(CharacterController controller)
    {
        if(!controller.TryGetComponent(out Thrower thrower))
        {
            return;
        }

        thrower.GrabThrowable(this);
    }

    private void ToggleInteractables(bool value)
    {
        foreach(var interactable in _interactables)
        {
            interactable.enabled = value;
        }
    }
}
