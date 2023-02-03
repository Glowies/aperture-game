using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // pick up event
    public event Action onPlantTriggerPickUp;
    public void PlantTriggerPickUp() 
    {
        if (onPlantTriggerPickUp != null)
        {
            onPlantTriggerPickUp();
        }
    }
    // throw event 
    public event Action onPlantTriggerThrow;
    public void PlantTriggerThrow() 
    {
        if (onPlantTriggerThrow != null)
        {
            onPlantTriggerThrow();
        }
    }

}
