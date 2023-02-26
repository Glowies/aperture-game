using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This component is an extension for Activation Regions.
    The selected target will get enabled when region is activated and 
    disabled when it's deactivated.
*/
[RequireComponent(typeof(PlantActivationRegion))]
public class EnablerActivationExtension : MonoBehaviour
{
    public GameObject Target;

    void Awake()
    {
        TryGetComponent(out PlantActivationRegion _region);

        if(Target != null)
        {
            _region.OnEnter.AddListener(() => {Target.SetActive(true);});
            _region.OnExit.AddListener(() => {Target.SetActive(false);});
        }
    }
}
