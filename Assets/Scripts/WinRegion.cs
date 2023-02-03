using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinRegion : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        // Only register entry if it's a throwable
        if(!other.TryGetComponent(out Throwable throwable))
        {
            return;
        }
        
        OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        // Only register entry if it's a throwable
        if(!other.TryGetComponent(out Throwable throwable))
        {
            return;
        }
        
        OnExit.Invoke();
    }
}
