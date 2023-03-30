using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    // This is the variable to store object currently being grabbed
    public Throwable GrabbedThrowable;
    public bool IsGrabbing; //bool for currently grabbing or not
    public float GrabMargin = 0.2f;
    public float GrabForwardOffset = 1f;
    public float ThrowForce = 4f;
    public AudioSource PickUpSource;
    public AudioSource DropItemSource;

    private CharacterController _characterController;

    void Awake()
    {
        TryGetComponent(out _characterController);
    }

    void LateUpdate()
    {
        // Like update, called on every frame after update functions
        if(IsGrabbing)
        {
            // If grabbing an object, update the grabbed object's location
            FollowThrower();
        }
    }

    private void FollowThrower()
    {
        // change position of grabbed object (GrabbedThrowable) based on player with height offset
        var offset = Vector3.up * _characterController.height;
        offset += Vector3.up * GrabbedThrowable.Height / 2f;
        offset += Vector3.up * GrabMargin;
        offset += GrabForwardOffset * transform.forward;
        GrabbedThrowable.transform.position = transform.position + offset;
    }

    public void GrabThrowable(Throwable throwable)
    {
        // Make sure only one object is grabbed at a time
        if(IsGrabbing)
        {
            return;
        }
        
        // Play the pick-up sound
        PickUpSource.Play();
        
        // This function is triggered by Throwable script when throwable object is
        // interacted with 
        // assign grabbed object to GrabbedThrowable variable on grab
        GrabbedThrowable = throwable;
        // freeze rigidbody contraints on grabbed object
        throwable.Grab();
        IsGrabbing = true;
    }

    public void OnThrow() => Throw();

    [ContextMenu("throw")]
    public void Throw()
    {
        if (PauseMenu.isPaused){return;} //if game paused, do not throw
        var throwDireciton = transform.forward + transform.up * -8;
        throwDireciton.Normalize();

        // Play the drop item sound
        DropItemSource.Play();
        
        IsGrabbing = false;
        GrabbedThrowable.Throw(throwDireciton * ThrowForce);
        GrabbedThrowable = null;
    }
}
