using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This component is an extension for Activation Regions.
    The selected targets will get disabled when region is activated and 
    enabled when it's deactivated.
*/
[RequireComponent(typeof(PlantActivationRegion))]
public class DisablerActivationExtension : MonoBehaviour
{
    public GameObject[] Targets;

    void Awake()
    {
        TryGetComponent(out PlantActivationRegion _region);
        _region.OnEnter.AddListener(() => {
            foreach (GameObject target in Targets){
                if (target != null){
                    target.SetActive(false);
                }
            }
        });
        _region.OnExit.AddListener(() => {
            foreach (GameObject target in Targets){
                if (target != null){
                    target.SetActive(true);
                }
            }
        });
    }
}
