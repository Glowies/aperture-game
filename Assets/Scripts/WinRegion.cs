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
        // if the parent is a TAV
        if(other.transform.parent.name.Contains("TAV")){
            // Debug.Log(true);
            // get the parent component
            Transform tavParent = other.transform.parent;
            // get the future object in the TAV and get the ObjectSwap script to call function
            tavParent.GetComponentInChildren<ObjectSwap>().EnableState(1);
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
         if(other.transform.parent.name.Contains("TAV")){
            // Debug.Log(true);
            // get the parent component
            Transform tavParent = other.transform.parent;
            // get the future object in the TAV and get the ObjectSwap script to call function
            tavParent.GetComponentInChildren<ObjectSwap>().EnableState(0);
        }
        OnExit.Invoke();
    }
}
