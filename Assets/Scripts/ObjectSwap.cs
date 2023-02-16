using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwap : MonoBehaviour
{
    //array of objects - future object states to enable or disable
    [SerializeField] GameObject[] futureStates;

    public void EnableState(int index){
        //takes in state index and sets futurestates[index].SetActive = enabled
        if (index < 0 || index > futureStates.Length){
            Debug.Log("invalid index");
            return;
        }
        foreach (GameObject state in futureStates){
                state.SetActive(false);
                //disable all other states first
        }
        futureStates[index].SetActive(true);
        Debug.Log(index);
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
