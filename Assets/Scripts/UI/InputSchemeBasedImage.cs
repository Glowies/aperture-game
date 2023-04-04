using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputSchemeBasedImage : MonoBehaviour
{
    public GameObject KeyboardImage;
    public GameObject GamepadImage;

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    void OnEnable()
    {
        InputUser.onChange += UpdateImage;
    }

    void OnDisable()
    {
        InputUser.onChange -= UpdateImage;
    }
    
    void UpdateImage(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if(inputUserChange != InputUserChange.ControlSchemeChanged)
        {
            return;
        }
        bool isGamepad = _playerInput.currentControlScheme == "Gamepad";
        
        if(KeyboardImage != null)
        {
            KeyboardImage.SetActive(!isGamepad);
        }
        if(GamepadImage != null)
        {
            GamepadImage.SetActive(isGamepad);
        }
    }
}
