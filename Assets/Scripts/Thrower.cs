using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    // This is the variable to store object currently being grabbed
    public Throwable GrabbedThrowable;
    private Rigidbody grabbedRigidBody;

    
    public bool IsGrabbing; //bool for currently grabbing or not
    public float GrabMargin = 0.2f;
    public float GrabForwardOffset = 1f;
    public float ThrowForce = 4f;
    public AudioSource PickUpSource;
    public AudioSource DropItemSource;

    private CharacterController _characterController;
    private float _throwTimeout = 0;

    //Location where picked up object is held 
    [SerializeField] Transform pickupTarget;
    //tuning the object carry physics 
    private float objectSpeed = 12f;
    //maximum distance the Throwable can be away from the player
    [SerializeField] float maxHoldRange = 8f;

    void Awake()
    {
        TryGetComponent(out _characterController);
    }

    void Update()
    {
        if(_throwTimeout < 0)
        {
            _throwTimeout += Time.deltaTime;
        }
    }

    void FixedUpdate()
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
        // var offset = Vector3.up * _characterController.height;
        // offset += Vector3.up * GrabbedThrowable.Height / 2f;
        // offset += Vector3.up * GrabMargin;
        // offset += GrabForwardOffset * transform.forward;
        // GrabbedThrowable.transform.position = transform.position + offset;
        Vector3 DirectionToPoint = pickupTarget.position - grabbedRigidBody.position;
        float distanceToPoint = DirectionToPoint.magnitude;
        grabbedRigidBody.velocity = DirectionToPoint * objectSpeed * distanceToPoint;
        // Debug.Log(distanceToPoint);
        // if the distance from the Throwable to the pickup target position in front of the player is too big,
        // teleport the Throwable to the position in front of the player
        if (distanceToPoint > maxHoldRange){
            GrabbedThrowable.transform.position = pickupTarget.position;
        }
    }

    public void GrabThrowable(Throwable throwable)
    {
        // Make sure only one object is grabbed at a time
        if(IsGrabbing)
        {
            return;
        }
        _throwTimeout = -0.1f;
        
        // Play the pick-up sound
        PickUpSource.Play();
        
        // This function is triggered by Throwable script when throwable object is
        // interacted with 
        // assign grabbed object to GrabbedThrowable variable on grab
        GrabbedThrowable = throwable;
        grabbedRigidBody = GrabbedThrowable.GetComponent<Rigidbody>();
        // freeze rigidbody contraints on grabbed object
        throwable.Grab();
        IsGrabbing = true;
    }

    public void OnThrow() => Throw();

    [ContextMenu("throw")]
    public void Throw()
    {
        if (PauseMenu.isPaused) {return;} //if game paused, do not throw
        if (GrabbedThrowable == null) {return;}
        if (_throwTimeout < 0) {return;}
        var throwDireciton = transform.forward + transform.up * -8;
        throwDireciton.Normalize();

        // Play the drop item sound
        DropItemSource.Play();
        
        IsGrabbing = false;
        GrabbedThrowable.Throw(throwDireciton * ThrowForce);
        GrabbedThrowable = null;
        grabbedRigidBody = null;
    }
}
