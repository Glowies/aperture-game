using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This class represents a Plant.
    It is used as a check for PlantActivationRegions.
*/
public class Plant : MonoBehaviour
{
    public GameObject DeadPlant;

    public void ToggleDeadPlant(bool value) => DeadPlant?.SetActive(value);
}
