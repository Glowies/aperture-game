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

    void Awake()
    {
        TryGetComponent(out _rigidbody);
        _startConstraints = _rigidbody.constraints;

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
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Throw(Vector3 force)
    {
        IsGrabbed = false;
        _rigidbody.constraints = _startConstraints;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
