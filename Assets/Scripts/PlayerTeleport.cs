using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;

public class PlayerTeleport : MonoBehaviour
{
     //add headers and tooltips to give information in inspector 
    [Header("Teleport Settings")]
    [Tooltip("Adjust Teleport Time and Distance here")]
    [SerializeField] float upDistance = 32f;
    [SerializeField] float downDistance = -28f;
    [SerializeField] float waitTime = 0.2f;
    [SerializeField] ApertureAnimator apertureAnimator;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject PastVolume;
    public AudioSource CameraShutter;

    private CharacterController controller;

    private Thrower thrower;

    bool inPast = false;
    //bool to state if on bottom/past level or not 
    bool isTransitioning = false;
    // Add transitioning variable to prevent calling teleport during teleport

    // used this tutorial https://youtu.be/xmhm5jGwonc
    // Need to apply the script to the player armature which hs character controlle component

    void Start()
    {
        // update time indicator
        UpdateTimeIndicator();
    }

    IEnumerator TeleportPlayer(){
        // Play aperture animation
        if(apertureAnimator != null)
        {
            CameraShutter.Play();
            apertureAnimator.ShutterDuration = waitTime + 0.1f;
            apertureAnimator.ClosedPauseDuration = .8f;
            apertureAnimator.PlayShutterAnimation();
        }

        isTransitioning = true;
        // Set transitioning to true 
        
        controller = gameObject.GetComponent<CharacterController>();
        // get character controller from player armature 
        
        
        float yOffset;
        if (inPast == true){
            yOffset = upDistance;
            inPast = false;
        }
        else{ 
            yOffset = downDistance;
            inPast = true;
        }
        // teleport up or down depending if on bottom or past level or not 
        // new variable for new y position, seperate from transform.localPosition
        float newYPos =  transform.localPosition.y + yOffset;
        Debug.Log("New Position Is" + newYPos);
        // disable controller to allow for teleport, wait before teleporting according to tutorial above
        // disable third person controller script to avoid controlller.move being called when controller disabled
        GetComponent<ThirdPersonController>().enabled = false;
        controller.enabled = false;

        yield return new WaitForSeconds(waitTime);

        // toggle the Past Volume visual effects
        PastVolume.SetActive(inPast);
        // update time indicator
        UpdateTimeIndicator();
        // keep current x and z position but add to or subtract from current y position
        transform.localPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
        
        yield return new WaitForSeconds(waitTime);
        // enable controller and set transitioning to false
        controller.enabled = true;
        GetComponent<ThirdPersonController>().enabled = true;
        isTransitioning = false;
    }
    
    void OnTeleport()
    {
        if (isTransitioning){return;}
        if (PauseMenu.isPaused){return;} //if game paused, do not teleport
        // check if player is grabbing an object, if so, do not teleport
        ThirdPersonController third_person = gameObject.GetComponent<ThirdPersonController>();
        if (!third_person.Grounded){ 
            Debug.Log("Cannot teleport in air");
            return;}
        thrower = gameObject.GetComponent<Thrower>();
        if (thrower.IsGrabbing){
            Debug.Log("Cannot teleport while holding object");
            return;
        }
        //otherwise start teleport sequence
        StartCoroutine("TeleportPlayer");
    }

    void UpdateTimeIndicator()
    {
        if(timeText == null)
        {
            return;
        }

        timeText.text = inPast ? "Past" : "Future";
    }
}
