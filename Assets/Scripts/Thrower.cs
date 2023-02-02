using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public Throwable GrabbedThrowable;
    public bool IsGrabbing;
    public float GrabMargin = 0.2f;
    public float ThrowForce = 4f;

    private CharacterController _characterController;

    void Awake()
    {
        TryGetComponent(out _characterController);
    }

    void Start()
    {
        // GrabThrowable(GrabbedThrowable);
    }

    void LateUpdate()
    {
        if(IsGrabbing)
        {
            FollowThrower();
        }
    }

    private void FollowThrower()
    {
        var heightOffset = Vector3.up * _characterController.height;
        heightOffset += Vector3.up * GrabbedThrowable.Height / 2f;
        heightOffset += Vector3.up * GrabMargin;
        GrabbedThrowable.transform.position = transform.position + heightOffset;
    }

    public void GrabThrowable(Throwable throwable)
    {
        GrabbedThrowable = throwable;
        throwable.Grab();
        IsGrabbing = true;
    }

    public void OnThrow() => Throw();

    [ContextMenu("throw")]
    public void Throw()
    {
        var throwDireciton = transform.forward;
        throwDireciton.y = 1;
        throwDireciton.Normalize();

        IsGrabbing = false;
        GrabbedThrowable.Throw(throwDireciton * ThrowForce);
        GrabbedThrowable = null;
    }
}
