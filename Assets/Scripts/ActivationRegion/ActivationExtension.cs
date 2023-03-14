using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    This component is an extension for Activation Regions.
    The selected targets will get disabled when region is activated and 
    enabled when it's deactivated. Another set of targets will be enabled 
    when region is activated and disabled when deactivated. 
*/
[RequireComponent(typeof(PlantActivationRegion))]

public class ActivationExtension : MonoBehaviour
{
    public GameObject[] activeTargets;
    public GameObject[] deactiveTargets;

    void Awake()
    {
        TryGetComponent(out PlantActivationRegion _region);
        
        _region.OnEnter.AddListener(() => {
            
            //activate objects in this list upon collision with plant
            foreach (GameObject target in activeTargets){
                if (target != null){
                    target.SetActive(true);
                }
            }
            //deactivate objects in the list of deactiveTargets
            foreach (GameObject target in deactiveTargets){
                if (target != null){
                    target.SetActive(false);
                }
            }
        });
        
        _region.OnExit.AddListener(() => {
            //deactivate objects in this list upon plant exit 
            foreach (GameObject target in activeTargets){
                if (target != null){
                    target.SetActive(false);
                }
            }

            //activate objects in this list upon plant exit 
            foreach (GameObject target in deactiveTargets){
                if (target != null){
                    target.SetActive(true);
                }
            }
        });
    }   
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
