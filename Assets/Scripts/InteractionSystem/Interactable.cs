using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<CharacterController> OnInteract;

    public void Interact(CharacterController controller)
    {
        OnInteract.Invoke(controller);
    }
}
